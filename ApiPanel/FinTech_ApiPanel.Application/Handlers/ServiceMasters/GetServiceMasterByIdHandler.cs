using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.ServiceMasters;
using FinTech_ApiPanel.Domain.DTOs.Services;
using FinTech_ApiPanel.Domain.Interfaces.IServiceMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.ServiceMasters
{
    public class GetServiceMasterByIdHandler : IRequestHandler<GetServiceMasterByIdQuery, ApiResponse<ServiceDto>>
    {
        private readonly IServiceMasterRepository _serviceMasterRepository;

        public GetServiceMasterByIdHandler(IServiceMasterRepository serviceMasterRepository)
        {
            _serviceMasterRepository = serviceMasterRepository;
        }

        public async Task<ApiResponse<ServiceDto>> Handle(GetServiceMasterByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var service = await _serviceMasterRepository.GetByIdAsync(request.Id);
                if (service == null)
                    return ApiResponse<ServiceDto>.NotFoundResponse("Service not found");

                var typeName = Enum.GetName(typeof(ServiceType), service.Type);

                ServiceDto serviceDto = new ServiceDto()
                {
                    Id = service.Id,
                    Type = service.Type,
                    TypeName = typeName,
                    Title = service.Title,
                    IsActive = service.IsActive
                };

                return ApiResponse<ServiceDto>.SuccessResponse(serviceDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<ServiceDto>.ForbiddenResponse($"Error fetching service: {ex.Message}");
            }
        }
    }

}
