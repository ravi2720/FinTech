using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IOTPManagers;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.Utils;
using MediatR;
using Newtonsoft.Json.Linq;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    internal class ChangeIpin_VerifyOTPHandler : IRequestHandler<ChangeIpin_VerifyOTPCommand, ApiResponse<object>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOTPManagerRepository _oTPManagerRepository;

        public ChangeIpin_VerifyOTPHandler(IUserRepository userRepository,
            ICryptoService cryptoService,
            IOTPManagerRepository oTPManagerRepository)
        {
            _userRepository = userRepository;
            _oTPManagerRepository = oTPManagerRepository;
        }

        public async Task<ApiResponse<object>> Handle(ChangeIpin_VerifyOTPCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var decryptedString = EncryptionUtils.DecryptString(request.Code);
                var jsonObject = JObject.Parse(decryptedString);

                string email = null;

                //check if email or phone number exist in json
                if (jsonObject.TryGetValue("email", out var emailToken))
                    email = emailToken?.ToString();
                else return ApiResponse<object>.ForbiddenResponse();

                var responseOtp = _oTPManagerRepository.GetAllAsync().Result
                    .Where(x => x.Otp == request.OTP
                    && x.Type == (byte)OTPType.ChangeIPin
                    && x.IsOtpVerified == false
                    && x.OtpValidTill >= DateTime.Now
                    && x.Token == request.Code
                    && x.TokenValidTill >= DateTime.Now
                    && (x.Email == email))
                    .FirstOrDefault();

                if (responseOtp == null)
                    return ApiResponse<object>.ForbiddenResponse();

                //Get user
                var user = await _userRepository.GetByEmailOrUserNameAsync(email);

                // check if username exists
                if (user == null)
                    return ApiResponse<object>.NoContentResponse();

                responseOtp.IsOtpVerified = true;

                //generate password reset token
                var token = EncryptionUtils.EncryptString(user.Password);

                responseOtp.Token = token;
                responseOtp.TokenValidTill = DateTime.Now.AddMinutes(10);

                //update 
                await _oTPManagerRepository.UpdateAsync(responseOtp);

                return ApiResponse<object>.SuccessResponse(new { Token = token });
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.BadRequestResponse(ex.Message);
            }
        }
    }
}
