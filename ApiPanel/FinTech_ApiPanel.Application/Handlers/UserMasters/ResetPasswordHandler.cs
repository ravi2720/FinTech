using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IOTPManagers;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, ApiResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOTPManagerRepository _oTPManagerRepository;
        private readonly ICryptoService _cryptoService;

        public ResetPasswordHandler(IUserRepository userRepository,
            ICryptoService cryptoService,
            IOTPManagerRepository oTPManagerRepository)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _oTPManagerRepository = oTPManagerRepository;
        }

        public async Task<ApiResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var responseList = await _oTPManagerRepository.GetAllAsync();

                var responseOtp = responseList.Where(x => x.Token == request.Token
                && x.Type == (byte)OTPType.ResetPassword
                && x.OtpValidTill >= DateTime.Now
                && x.IsOtpVerified == true)
                .FirstOrDefault();

                if (responseOtp == null)
                    return ApiResponse.ForbiddenResponse();

                //Get user
                var user = await _userRepository.GetByEmailOrUserNameAsync(responseOtp.Email);

                // check if user exists
                if (user == null)
                    return ApiResponse.NoContentResponse("User not found!");

                var result = _cryptoService.GenerateSaltedHash(request.NewPassword);
                user.Password = result.Hash;
                user.Salt = result.Salt;

                await _userRepository.UpdateAsync(user);

                //delete old OTP
                foreach (var item in responseList.Where(x => x.Email == responseOtp.Email).ToList())
                {
                    await _oTPManagerRepository.DeleteAsync(item.Id);
                }

                return ApiResponse.SuccessResponse("Password reset successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse("An error occurred while resetting the password.");
            }
        }
    }
}
