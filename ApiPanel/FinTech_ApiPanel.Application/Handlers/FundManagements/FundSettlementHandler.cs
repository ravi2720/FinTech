using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.FundManagements;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IFundRequests;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;
using System.Transactions;

namespace FinTech_ApiPanel.Application.Handlers.FundManagements
{
    public class FundSettlementHandler : IRequestHandler<FundSettlementCommand, ApiResponse>
    {
        public readonly IUserRepository _userRepository;
        public readonly IFundRequestRepository _fundRequestRepository;
        public readonly IWalletRepository _walletRepository;
        public readonly ITransactionLogRepository _transactionLogRepository;
        public readonly ITokenService _tokenService;
        public readonly IMapper _mapper;
        public FundSettlementHandler(IUserRepository userRepository,
            IFundRequestRepository fundRequestRepository,
            IWalletRepository walletRepository,
            ITransactionLogRepository transactionLogRepository,
            ITokenService tokenService,
            IMapper mapper)
        {
            _fundRequestRepository = fundRequestRepository;
            _walletRepository = walletRepository;
            _transactionLogRepository = transactionLogRepository;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(FundSettlementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var txOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, txOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var loggedInUser = _tokenService.GetLoggedInUserinfo();
                    if (loggedInUser == null)
                        return ApiResponse.UnauthorizedResponse("User not authenticated.");

                    await SattleToBank(request, loggedInUser.UserId);

                    scope.Complete();
                    return ApiResponse.SuccessResponse("Fund request accepted.");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse(ex.Message);
            }
        }

        async Task SattleToBank(FundSettlementCommand request, long loggedInUserId)
        {
            try
            {
                // 1. Load user's wallet
                var wallet = await _walletRepository.GetByUserIdAsync(loggedInUserId);
                if (wallet == null)
                    throw new Exception("Wallets not found.");

                if (wallet.TotalBalance < request.Amount)
                    throw new Exception("Insufficient admin wallet balance to process this fund request.");

                wallet.TotalBalance -= request.Amount;
                await _walletRepository.UpdateAsync(wallet);

                var aepsWalletTransaction = new TransactionLog
                {
                    UserId = wallet.UserId,
                    Amount = request.Amount,
                    RemainingAmount = wallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    ReferenceId = request.ReferenceId,
                    Remark = !string.IsNullOrEmpty(request.Remark) ? request.Remark : $"Amount {request.Amount} sattled from wallet to bank.",
                    LogType = (byte)LogType.AEPSSettlement,
                };

                var drEntry = await _transactionLogRepository.AddAsync(aepsWalletTransaction);

                //logic to settle to bank



                throw new Exception("This service is not available. Please contact support.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error during AEPS fund settlement: {ex.Message}");
            }
        }
    }
}
