using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.FundManagements;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.FundRequests;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IFundRequests;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.FundManagements
{
    public class RejectFundRequestHandler : IRequestHandler<RejectFundRequestCommand, ApiResponse>
    {
        public readonly IFundRequestRepository _fundRequestRepository;
        public readonly IMapper _mapper;
        public readonly ITransactionLogRepository _transactionLogRepository;
        private readonly ITokenService _tokenService;

        public RejectFundRequestHandler(IFundRequestRepository fundRequestRepository,
            IMapper mapper,
            ITransactionLogRepository transactionLogRepository,
            ITokenService tokenService)
        {
            _fundRequestRepository = fundRequestRepository;
            _mapper = mapper;
            _transactionLogRepository = transactionLogRepository;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(RejectFundRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("User not authenticated or not authorized to deduct funds.");

                var fundRequest = await _fundRequestRepository.GetByIdAsync(request.Id);

                if (fundRequest == null)
                    return ApiResponse.BadRequestResponse("Fund request not found.");

                fundRequest.Status = (byte)FundRequestStatus.Rejected;
                fundRequest.AdminRemark = string.IsNullOrEmpty(request.Remark) ? request.Remark : $"Fund request rejected by admin.";

                var result = await _fundRequestRepository.UpdateAsync(fundRequest);
                await CreateLog(request, fundRequest, loggedInUser.UserId);
                return ApiResponse.SuccessResponse("Fund request rejected.");
            }
            catch (Exception)
            {
                return ApiResponse.BadRequestResponse("Error rejecting fund request. Please try again later or contact support if the issue persists.");
            }
        }

        async Task CreateLog(RejectFundRequestCommand command, FundRequest fundRequest, long loggedInUserId)
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
                    Remark = $"Fund request rejected by admin. Amount: {fundRequest.Amount}, Remark: {command.Remark}",
                    LogType = (byte)LogType.RejectFundRequest,
                    Status = (byte)Status.Success,
                };

                await _transactionLogRepository.AddAsync(walletTransaction);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating transaction log for fund request rejection.", ex);
            }
        }
    }
}
