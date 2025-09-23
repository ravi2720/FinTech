using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.FundManagements;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.FundRequests;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IFundRequests;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.Utils;
using MediatR;
using System.Net.Mail;

namespace FinTech_ApiPanel.Application.Handlers.FundManagements
{
    public class CreateFundRequestHandler : IRequestHandler<CreateFundRequestCommand, ApiResponse>
    {
        public readonly IFundRequestRepository _fundRequestRepository;
        public readonly IMapper _mapper;
        public readonly ITokenService _tokenService;
        public readonly ITransactionLogRepository _transactionLogRepository;

        public CreateFundRequestHandler(IFundRequestRepository fundRequestRepository,
            IMapper mapper,
            ITokenService tokenService,
            ITransactionLogRepository transactionLogRepository)
        {
            _fundRequestRepository = fundRequestRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _transactionLogRepository = transactionLogRepository;
        }

        public async Task<ApiResponse> Handle(CreateFundRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();

                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("Unauthorized user.");
                if (request.Amount <= 0)
                    return ApiResponse.ValidationErrorResponse("Amount must be greater than zero.");
                if (!loggedInUser.IsAdmin && request.UserId != loggedInUser.UserId)
                    return ApiResponse.ValidationErrorResponse("Not allowed.");

                var fundRequest = _mapper.Map<FundRequest>(request);
                fundRequest.Status = (byte)FundRequestStatus.Pending;
                fundRequest.UserRemark = request.Remark;

                //upload new profile picture
                if (request.AttachmentFile != null)
                    fundRequest.Attachment = await FileUtils.FileUpload(request.AttachmentFile, FilePath.FundRequestAttachment);
                else
                    throw new Exception("Attachment file is null or empty.");

                var result = await _fundRequestRepository.AddAsync(fundRequest);

                await CreateLog(request, loggedInUser.UserId);

                return ApiResponse.SuccessResponse("Fund request created.");
            }
            catch (Exception)
            {
                return ApiResponse.BadRequestResponse("Error creating fund request. Please try again later or contact support if the issue persists.");
            }
        }

        async Task CreateLog(CreateFundRequestCommand command, long loggedInUserId)
        {
            try
            {
                // Create wallet transaction
                var walletTransaction = new TransactionLog
                {
                    UserId = loggedInUserId,
                    RefUserId = command.UserId,
                    Amount = command.Amount,
                    ReferenceId = command.ReferenceId,
                    AuditType = (byte)AuditType.Other,
                    Remark = command.Remark?? "Fund request created",
                    LogType = (byte)LogType.FundRequest,
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
