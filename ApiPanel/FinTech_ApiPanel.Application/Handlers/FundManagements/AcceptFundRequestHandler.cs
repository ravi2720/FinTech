using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.FundManagements;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.FundRequests;
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
    public class AcceptFundRequestHandler : IRequestHandler<AcceptFundRequestCommand, ApiResponse>
    {
        public readonly IFundRequestRepository _fundRequestRepository;
        public readonly IWalletRepository _walletRepository;
        public readonly ITransactionLogRepository _transactionLogRepository;
        public readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AcceptFundRequestHandler(IFundRequestRepository fundRequestRepository,
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

        public async Task<ApiResponse> Handle(AcceptFundRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("User not authenticated or not authorized to deduct funds.");

                // Load user's wallet
                var adminWallet = await _walletRepository.GetByUserIdAsync(loggedInUser.UserId);
                if (adminWallet == null)
                    throw new Exception("Admin wallet not found.");

                var txOptions = new TransactionOptions
                {
                    IsolationLevel = IsolationLevel.ReadCommitted,
                    Timeout = TransactionManager.DefaultTimeout
                };

                using (var scope = new TransactionScope(TransactionScopeOption.Required, txOptions, TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Load fund request
                    var fundRequest = await _fundRequestRepository.GetByIdAsync(request.Id);
                    if (fundRequest == null)
                        return ApiResponse.BadRequestResponse("Fund request not found.");

                    // Update fund request status
                    fundRequest.Status = (byte)FundRequestStatus.Approved;
                    fundRequest.AdminRemark = request.Remark;

                    await _fundRequestRepository.UpdateAsync(fundRequest);

                    // Load user's wallet
                    var userWallet = await _walletRepository.GetByUserIdAsync(fundRequest.UserId);
                    if (userWallet == null)
                        throw new Exception("User wallet not found.");

                    // Create wallet transactions for admin and user
                    await CreateLog(request, fundRequest, loggedInUser.UserId);
                    await ManageAdminWallet(adminWallet, userWallet, request, fundRequest, loggedInUser.UserId);
                    await ManageUserWallet(adminWallet, userWallet, request, fundRequest);

                    scope.Complete();
                    return ApiResponse.SuccessResponse("Fund request accepted.");
                }
            }
            catch (Exception ex)
            {
                // TODO: optionally log ex
                return ApiResponse.BadRequestResponse(ex.Message);
            }
        }

        async Task CreateLog(AcceptFundRequestCommand command, FundRequest fundRequest, long loggedInUserId)
        {
            try
            {
                // Create wallet transaction
                var walletTransaction = new TransactionLog
                {
                    UserId = loggedInUserId,
                    RefUserId = fundRequest.UserId,
                    Amount = fundRequest.Amount,
                    ReferenceId = fundRequest.ReferenceId,
                    AuditType = (byte)AuditType.Other,
                    Remark = $"Fund request accepted by admin",
                    LogType = (byte)LogType.AcceptFundRequest,
                    Status = (byte)Status.Success
                };

                await _transactionLogRepository.AddAsync(walletTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating transaction log for fund request rejection.", ex);
            }
        }

        async Task ManageAdminWallet(Wallet adminWallet, Wallet userWallet, AcceptFundRequestCommand command, FundRequest fundRequest, long loggedInUserId)
        {
            try
            {
                if (adminWallet.TotalBalance < fundRequest.Amount)
                    throw new Exception("Insufficient admin wallet balance to process this fund request.");

                // Update wallet balance
                adminWallet.TotalBalance -= fundRequest.Amount;

                await _walletRepository.UpdateAsync(adminWallet);

                // Create wallet transaction
                var walletTransaction = new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    RefUserId = userWallet.UserId,
                    Amount = fundRequest.Amount,
                    RemainingAmount = adminWallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    ReferenceId = fundRequest.ReferenceId,
                    Remark = !string.IsNullOrEmpty(command.Remark) ? command.Remark : $"Amount {fundRequest.Amount} deducted and added to user with id: {fundRequest.UserId}.",
                    LogType = (byte)LogType.AcceptFundRequest,
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

        async Task ManageUserWallet(Wallet adminWallet, Wallet userWallet, AcceptFundRequestCommand request, FundRequest fundRequest)
        {
            try
            {
                // Update wallet balance
                userWallet.TotalBalance += fundRequest.Amount;

                await _walletRepository.UpdateAsync(userWallet);

                // Create wallet transaction
                var walletTransaction = new TransactionLog
                {
                    UserId = fundRequest.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = fundRequest.Amount,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Credit,
                    ReferenceId = fundRequest.ReferenceId,
                    Remark = !string.IsNullOrEmpty(request.Remark) ? request.Remark : $"Fund request accepted by admin.",
                    LogType = (byte)LogType.AcceptFundRequest,
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
