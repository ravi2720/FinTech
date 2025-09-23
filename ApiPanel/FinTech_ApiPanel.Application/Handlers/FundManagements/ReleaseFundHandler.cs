using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.FundManagements;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Entities.UserWallets;
using FinTech_ApiPanel.Domain.Interfaces.IFundRequests;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;
using System.Transactions;

namespace FinTech_ApiPanel.Application.Handlers.FundManagements
{
    public class ReleaseFundHandler : IRequestHandler<ReleaseFundCommand, ApiResponse>
    {
        public readonly IFundRequestRepository _fundRequestRepository;
        public readonly IWalletRepository _walletRepository;
        public readonly ITransactionLogRepository _transactionLogRepository;
        public readonly IMapper _mapper;
        public readonly ITokenService _tokenService;

        public ReleaseFundHandler(IFundRequestRepository fundRequestRepository,
            IWalletRepository walletRepository,
            ITransactionLogRepository transactionLogRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _fundRequestRepository = fundRequestRepository;
            _walletRepository = walletRepository;
            _transactionLogRepository = transactionLogRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(ReleaseFundCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("User not authenticated or not authorized to hold funds.");
                if (loggedInUser.UserId == request.UserId)
                    return ApiResponse.BadRequestResponse("You cannot release funds for yourself. Please contact support if you need assistance.");

                var txOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, txOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    // 1. Load user's wallet
                    var userWallet = await _walletRepository.GetByUserIdAsync(request.UserId);
                    if (userWallet == null)
                        return ApiResponse.BadRequestResponse("User wallet not found.");

                    // 3. Update wallet balance
                    userWallet.HeldAmount -= request.Amount;
                    await _walletRepository.UpdateAsync(userWallet);

                    // 4. Create wallet transaction
                    var transaction = new TransactionLog
                    {
                        UserId = userWallet.UserId,
                        Amount = request.Amount,
                        AuditType = (byte)AuditType.Other,
                        ReferenceId = request.ReferenceId,
                        Remark = string.IsNullOrEmpty(request.Remark) ? request.Remark : $"Fund release of {request.Amount} by admin",
                        LogType = (byte)LogType.ReleaseFund,
                    };
                    await _transactionLogRepository.AddAsync(transaction);

                    // 5. Commit transaction
                    scope.Complete();
                    return ApiResponse.SuccessResponse("Funds released successfully.");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse("An error occurred while releasing funds: " + ex.Message);
            }
        }
    }
}
