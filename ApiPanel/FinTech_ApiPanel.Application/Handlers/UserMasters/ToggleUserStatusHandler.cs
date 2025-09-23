using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class ToggleUserStatusHandler : IRequestHandler<ToggleUserStatusCommand, ApiResponse>
    {
        private readonly IUserRepository _userRepository;

        public ToggleUserStatusHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApiResponse> Handle(ToggleUserStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.Id);
                if (user == null)
                    return ApiResponse.NotFoundResponse("User not found.");
                if (user.IsAdmin)
                    return ApiResponse.ForbiddenResponse("Action not allowed.");

                // Toggle the user's active status
                user.IsActive = !user.IsActive;

                await _userRepository.UpdateAsync(user);

                return ApiResponse.SuccessResponse("User status updated successfully.");
            }
            catch (Exception)
            {
                return ApiResponse.BadRequestResponse("An error occurred while updating user status.");
            }
        }
    }

}
