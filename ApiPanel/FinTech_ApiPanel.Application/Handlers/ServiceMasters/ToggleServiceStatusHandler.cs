using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.ServiceMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IServiceMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserServices;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.ServiceMasters
{
    public class ToggleServiceStatusHandler : IRequestHandler<ToggleServiceStatusCommand, ApiResponse>
    {
        private readonly IServiceMasterRepository _serviceMasterRepository;
        private readonly IUserServiceRepository _userServiceRepository;
        private readonly ITokenService _tokenService;

        public ToggleServiceStatusHandler(IServiceMasterRepository serviceMasterRepository,
            IUserServiceRepository userServiceRepository,
            ITokenService tokenService)
        {
            _serviceMasterRepository = serviceMasterRepository;
            _userServiceRepository = userServiceRepository;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(ToggleServiceStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("User not authorized.");

                if (loggedInUser.IsAdmin && request.UserId == loggedInUser.UserId)
                {
                    var serviceExists = await _serviceMasterRepository.GetByIdAsync(request.ServiceId);
                    if (serviceExists == null)
                        return ApiResponse.NotFoundResponse("Service not found.");

                    // Toggle the service status
                    serviceExists.IsActive = !serviceExists.IsActive;
                    await _serviceMasterRepository.UpdateAsync(serviceExists);
                }
                else if ((loggedInUser.IsAdmin && request.UserId != loggedInUser.UserId) || request.UserId == loggedInUser.UserId)
                {
                    var userServices = await _userServiceRepository.GetByUserIdAndServiceIdAsync(request.UserId, request.ServiceId);

                    if (userServices == null)
                        return ApiResponse.NotFoundResponse("User service not found.");

                    // Toggle the user service status
                    userServices.IsActive = !userServices.IsActive;
                    await _userServiceRepository.UpdateAsync(userServices);
                }
                else
                    return ApiResponse.NotFoundResponse("Access denied.");

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.ForbiddenResponse($"Error toggling service status: {ex.Message}");
            }
        }
    }
}
