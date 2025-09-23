using FinTech_ApiPanel.Application.Commands.ServiceMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.ServiceMasters;
using FinTech_ApiPanel.Domain.Entities.UserServices;
using FinTech_ApiPanel.Domain.Interfaces.IServiceMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserServices;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.ServiceMasters
{
    public class CreateServiceMasterHandler : IRequestHandler<CreateServiceMasterCommand, ApiResponse>
    {
        private readonly IServiceMasterRepository _serviceMasterRepository;
        private readonly IUserServiceRepository _userServiceRepository;
        private readonly IUserRepository _userRepository;

        public CreateServiceMasterHandler(IServiceMasterRepository serviceMasterRepository,
            IUserServiceRepository userServiceRepository,
            IUserRepository userRepository)
        {
            _serviceMasterRepository = serviceMasterRepository;
            _userServiceRepository = userServiceRepository;
            _userRepository = userRepository;
        }

        public async Task<ApiResponse> Handle(CreateServiceMasterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var exist = Enum.IsDefined(typeof(ServiceType), request.Type);

                if (!exist)
                    return ApiResponse.BadRequestResponse("Invalid type");

                var ifExists = await _serviceMasterRepository.GetAllAsync();

                if (ifExists.Any(x => x.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase)))
                    return ApiResponse.ForbiddenResponse("Service with this title already exists");
                if (ifExists.Any(x => x.Type == request.Type))
                    return ApiResponse.ForbiddenResponse("Service with this type already exists");

                var id = await _serviceMasterRepository.AddAsync(new ServiceMaster { Title = request.Title, Type = request.Type });

                //add service to every user in user service
                var users = await _userRepository.GetAllAsync();

                foreach (var user in users)
                {
                    if (user.IsAdmin)
                        continue;

                    await _userServiceRepository.AddAsync(new UserService
                    {
                        UserId = user.Id,
                        ServiceId = id,
                        IsActive = false
                    });
                }

                return ApiResponse.SuccessResponse("Service created successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ForbiddenResponse($"Error creating service: {ex.Message}");
            }
        }
    }

}
