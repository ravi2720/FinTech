using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ApiResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;
        private readonly ITokenService _tokenService;

        public ChangePasswordHandler(IUserRepository userRepository, 
            ICryptoService cryptoService,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser= _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null) return ApiResponse.UnauthorizedResponse("User not authenticated.");

                var user = await _userRepository.GetByIdAsync(loggedInUser.UserId);
                if (user == null) return ApiResponse.NotFoundResponse("User not found.");

                var valid = _cryptoService.VerifyPassword(request.OldPassword, user.Password, user.Salt);
                if (!valid) return ApiResponse.UnauthorizedResponse("Old password is incorrect.");

                var result = _cryptoService.GenerateSaltedHash(request.NewPassword);
                user.Password = result.Hash;
                user.Salt = result.Salt;
                user.UpdatedAt = DateTime.Now;

                await _userRepository.UpdateAsync(user);
                return ApiResponse.SuccessResponse("Password changed successfully.");
            }
            catch (Exception)
            {
                return ApiResponse.BadRequestResponse("An error occurred while changing the password.");
            }
        }
    }

}
