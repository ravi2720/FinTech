using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.ServiceMasters;
using FinTech_ApiPanel.Domain.DTOs.Services;
using FinTech_ApiPanel.Domain.Entities.ServiceMasters;
using FinTech_ApiPanel.Domain.Interfaces.IServiceMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserServices;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.ServiceMasters
{
    public class GetAllServiceMastersHandler : IRequestHandler<GetAllServiceMastersQuery, ApiResponse<IEnumerable<ServiceDto>>>
    {
        private readonly IServiceMasterRepository _serviceMasterRepository;
        private readonly IUserServiceRepository _userServiceRepository;
        private readonly ITokenService _tokenService;

        public GetAllServiceMastersHandler(IServiceMasterRepository serviceMasterRepository,
            IUserServiceRepository userServiceRepository,
            ITokenService tokenService)
        {
            _serviceMasterRepository = serviceMasterRepository;
            _userServiceRepository = userServiceRepository;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<IEnumerable<ServiceDto>>> Handle(GetAllServiceMastersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<IEnumerable<ServiceDto>>.UnauthorizedResponse("User not authorized.");

                List<ServiceDto> services = new List<ServiceDto>();

                var serviceMasters = await _serviceMasterRepository.GetAllAsync();


                if (loggedInUser.IsAdmin)
                {
                    foreach (var serviceMaster in serviceMasters)
                    {
                        var typeName = Enum.GetName(typeof(ServiceType), serviceMaster.Type);
                        services.Add(new ServiceDto()
                        {
                            Id = serviceMaster.Id,
                            Type = serviceMaster.Type,
                            TypeName = Enum.GetName(typeof(ServiceType), serviceMaster.Type),
                            Title = serviceMaster.Title,
                            IsActive = serviceMaster.IsActive
                        });
                    }
                }
                else
                {
                    var userServices = await _userServiceRepository.GetByUserIdAsync(loggedInUser.UserId);

                    foreach (var userService in userServices)
                    {
                        var type = serviceMasters.FirstOrDefault(x => x.Id == userService.ServiceId).Type;
                        var typeName = Enum.GetName(typeof(ServiceType), type);
                        var title = serviceMasters.FirstOrDefault(x => x.Id == userService.ServiceId).Title;

                        services.Add(new ServiceDto()
                        {
                            Id = userService.ServiceId,
                            Type = type,
                            TypeName = typeName,
                            Title = title,
                            IsActive = userService.IsActive
                        });
                    }
                }

                return ApiResponse<IEnumerable<ServiceDto>>.SuccessResponse(services);
            }
            catch (Exception ex)
            {
                return ApiResponse<IEnumerable<ServiceDto>>.ForbiddenResponse($"Error retrieving services: {ex.Message}");
            }
        }
    }

}
