using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.Tokens;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, ApiResponse<TokenDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptoService _cryptoService;
        private readonly ITokenService _tokenService;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginUserHandler(IUserRepository userRepository,
            ICryptoService cryptoService,
            ITokenService tokenService,
            ITransactionLogRepository transactionLogRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _tokenService = tokenService;
            _transactionLogRepository = transactionLogRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<TokenDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByEmailOrUserNameAsync(request.UserNameOrEmail);
                if (user == null) return ApiResponse<TokenDto>.NotFoundResponse("User not found.");

                if (!user.IsActive)
                    return ApiResponse<TokenDto>.UnauthorizedResponse("Your account is inactive. Please contact support.");

                var valid = _cryptoService.VerifyPassword(request.Password, user.Password, user.Salt);
                 var maxAllowedAttempts = 5;
                if (!valid)
                {
                    user.LoginAttempts++;
                    if (user.LoginAttempts >= maxAllowedAttempts)
                    {
                        user.IsActive = false;
                        user.LoginAttempts = 0; // Reset attempts after locking the account
                        await _userRepository.UpdateAsync(user);
                        return ApiResponse<TokenDto>.BadRequestResponse("Your account has been locked due to too many failed login attempts.");
                    }

                    await _userRepository.UpdateAsync(user);
                    var attemptsLeft = maxAllowedAttempts - user.LoginAttempts;
                    return ApiResponse<TokenDto>.BadRequestResponse($"Invalid username or password. ({attemptsLeft} attempts left)");
                }
                else
                {
                    user.LoginAttempts = 0;
                    await _userRepository.UpdateAsync(user);
                }

                var token = _tokenService.GenerateToken(user);

                token.IsAdmin = user.IsAdmin;

                return ApiResponse<TokenDto>.SuccessResponse(token);
            }
            catch (Exception)
            {
                return ApiResponse<TokenDto>.BadRequestResponse("An error occurred while processing your request.");
            }
        }
    }
}
