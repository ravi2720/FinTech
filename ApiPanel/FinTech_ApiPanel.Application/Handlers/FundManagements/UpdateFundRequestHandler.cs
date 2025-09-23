using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.FundManagements;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.FundRequests;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IFundRequests;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FinTech_ApiPanel.Application.Handlers.FundManagements
{
    public class UpdateFundRequestHandler : IRequestHandler<UpdateFundRequestCommand, ApiResponse>
    {
        public readonly IFundRequestRepository _fundRequestRepository;
        public readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UpdateFundRequestHandler(IFundRequestRepository fundRequestRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _fundRequestRepository = fundRequestRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(UpdateFundRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var fundRequest = await _fundRequestRepository.GetByIdAsync(request.Id);

                if (fundRequest == null)
                    return ApiResponse.BadRequestResponse("Fund request not found.");
                if (fundRequest.Status != (byte)FundRequestStatus.Pending)
                    return ApiResponse.BadRequestResponse("Action not allowed.");
                if (request.Amount <= 0)
                    return ApiResponse.ValidationErrorResponse("Amount must be greater than zero.");

                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (!loggedInUser.IsAdmin && fundRequest.UserId != loggedInUser.UserId)
                    return ApiResponse.ValidationErrorResponse("Not allowed.");

                _mapper.Map(request, fundRequest);
                fundRequest.UserRemark = request.Remark;

                // Handle attachment if provided
                if (request.AttachmentFile != null)
                    await ManageAttachment(fundRequest, request.AttachmentFile);

                var result = await _fundRequestRepository.UpdateAsync(fundRequest);

                return ApiResponse.SuccessResponse("Fund request updated.");
            }
            catch (Exception)
            {
                return ApiResponse.BadRequestResponse("Error updating fund request. Please try again later or contact support if the issue persists.");
            }
        }

        async Task ManageAttachment(FundRequest request, IFormFile? attachment)
        {
            try
            {
                //delete old profile picture if exists
                if (!string.IsNullOrEmpty(request.Attachment))
                    await FileUtils.DeleteFile(request.Attachment, FilePath.FundRequestAttachment);

                //upload new profile picture
                if (attachment != null)
                    request.Attachment = await FileUtils.FileUpload(attachment, FilePath.FundRequestAttachment);
                else
                    throw new Exception("Attachment file is null or empty.");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
