using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.UserMaster;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class ValidateIpinHandler : IRequestHandler<ValidateIpinQuery, ApiResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public ValidateIpinHandler(IUserRepository userRepository,
            ITokenService _tokenService)
        {
            _userRepository = userRepository;
            this._tokenService = _tokenService;
        }

        public async Task<ApiResponse> Handle(ValidateIpinQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<UserMasterDto>.NotFoundResponse("User not found.");
                var user = await _userRepository.GetByIdAsync(loggedInUser.UserId);
                if (user == null)
                    return ApiResponse<UserMasterDto>.NotFoundResponse("User not found.");

                if (user.IPin == request.Ipin)
                    return ApiResponse<UserMasterDto>.SuccessResponse("IPin is valid.");
                else
                    return ApiResponse<UserMasterDto>.BadRequestResponse("Invalid IPin.");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserMasterDto>.BadRequestResponse($"An error occurred while retrieving the user: {ex.Message}");
            }
        }
    }
}
