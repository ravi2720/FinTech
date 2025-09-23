using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.FundManagements;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IFundRequests;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;
using System.Transactions;

namespace FinTech_ApiPanel.Application.Handlers.FundManagements
{
    public class AddFundHandler : IRequestHandler<AddFundCommand, ApiResponse>
    {
        public readonly IFundRequestRepository _fundRequestRepository;
        public readonly IWalletRepository _walletRepository;
        public readonly ITransactionLogRepository _transactionLogRepository;
        public readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public AddFundHandler(IFundRequestRepository fundRequestRepository,
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

        public async Task<ApiResponse> Handle(AddFundCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("User not authenticated or not authorized to deduct funds.");

                var txOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, txOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    if (loggedInUser.UserId != request.UserId)
                        await ManageAdminWallet(request, loggedInUser.UserId);
                    await ManageUserWallet(request);

                    // 5. Commit transaction
                    scope.Complete();
                    return ApiResponse.SuccessResponse("Funds added successfully.");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse(ex.Message);
            }
        }

        async Task ManageAdminWallet(AddFundCommand request, long loggedInUserId)
        {
            try
            {
                // Load user's wallet
                var adminWallet = await _walletRepository.GetByUserIdAsync(loggedInUserId);
                if (adminWallet == null)
                    throw new Exception("Admin wallet not found.");

                if (adminWallet.TotalBalance < request.Amount)
                    throw new Exception("Insufficient admin wallet balance to process this fund request.");

                // Update wallet balance
                adminWallet.TotalBalance -= request.Amount;

                await _walletRepository.UpdateAsync(adminWallet);

                // Create wallet transaction
                var walletTransaction = new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    Amount = request.Amount,
                    RemainingAmount = adminWallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    ReferenceId = request.ReferenceId,
                    Remark = !string.IsNullOrEmpty(request.Remark) ? request.Remark : $"Amount {request.Amount} deducted and added to user with id: {request.UserId}.",
                    LogType = (byte)LogType.AddFund,
                    Status = (byte)Status.Success,
                    WalletUpdated = true
                };

                await _transactionLogRepository.AddAsync(walletTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating admin wallet: {ex.Message}");
            }
        }

        async Task ManageUserWallet(AddFundCommand request)
        {
            try
            {
                // Load user's wallet
                var userWallet = await _walletRepository.GetByUserIdAsync(request.UserId);
                if (userWallet == null)
                    throw new Exception("User wallet not found.");

                // Update wallet balance
                userWallet.TotalBalance += request.Amount;

                await _walletRepository.UpdateAsync(userWallet);

                // Create wallet transaction
                var walletTransaction = new TransactionLog
                {
                    UserId = request.UserId,
                    Amount = request.Amount,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Credit,
                    ReferenceId = request.ReferenceId,
                    Remark = !string.IsNullOrEmpty(request.Remark) ? request.Remark : $"Amount added by admin: {request.Amount}",
                    LogType = (byte)LogType.AddFund,
                    Status = (byte)Status.Success,
                    WalletUpdated = true
                };

                await _transactionLogRepository.AddAsync(walletTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user wallet: {ex.Message}");
            }
        }
    }
}