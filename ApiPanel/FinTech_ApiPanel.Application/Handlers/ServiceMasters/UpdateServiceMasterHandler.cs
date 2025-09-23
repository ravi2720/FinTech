using FinTech_ApiPanel.Application.Commands.ServiceMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IServiceMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.ServiceMasters
{
    public class UpdateServiceMasterHandler : IRequestHandler<UpdateServiceMasterCommand, ApiResponse>
    {
        private readonly IServiceMasterRepository _serviceMasterRepository;

        public UpdateServiceMasterHandler(IServiceMasterRepository serviceMasterRepository)
        {
            _serviceMasterRepository = serviceMasterRepository;
        }

        public async Task<ApiResponse> Handle(UpdateServiceMasterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exist = Enum.IsDefined(typeof(ServiceType), request.Type);

                if (!exist)
                    return ApiResponse.BadRequestResponse("Invalid type");

                var serviceList = await _serviceMasterRepository.GetAllAsync();

                var titleExists = serviceList.Any(x => (x.Title == request.Title || x.Type == request.Type) && x.Id != request.Id);

                if (titleExists)
                    return ApiResponse<bool>.ForbiddenResponse("Service with this title or type already exists");

                var service = await _serviceMasterRepository.GetByIdAsync(request.Id);
                if (service == null)
                    return ApiResponse<bool>.NotFoundResponse("Service not found");

                service.Title = request.Title;
                service.Type = request.Type;
                var updated = await _serviceMasterRepository.UpdateAsync(service);

                if (updated)
                    return ApiResponse<bool>.SuccessResponse(true, "Service updated successfully");
                else
                    return ApiResponse<bool>.NotFoundResponse("Service not found");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.ForbiddenResponse($"Error updating service: {ex.Message}");
            }
        }
    }

}
