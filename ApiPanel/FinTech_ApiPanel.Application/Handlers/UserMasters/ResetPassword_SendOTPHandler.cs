using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.Mails;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IOTPManagers;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.Utils;
using MediatR;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class ResetPassword_SendOTPHandler : IRequestHandler<ResetPassword_SendOTPCommand, ApiResponse<object>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOTPManagerRepository _oTPManagerRepository;
        private readonly MailSettingDto mailSettings;

        public ResetPassword_SendOTPHandler(IUserRepository userRepository,
            ICryptoService cryptoService,
            IOTPManagerRepository oTPManagerRepository,
            IOptions<MailSettingDto> mailSettings)
        {
            _userRepository = userRepository;
            _oTPManagerRepository = oTPManagerRepository;
            this.mailSettings = mailSettings.Value;
        }

        public async Task<ApiResponse<object>> Handle(ResetPassword_SendOTPCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByEmailOrUserNameAsync(request.Email);

                if (user == null)
                    return ApiResponse<object>.NoContentResponse();

                //delete old OTP
                var oldOTP = await _oTPManagerRepository.GetAllAsync();

                foreach (var item in oldOTP.Where(x => x.Email == request.Email && x.Type == (byte)OTPType.ResetPassword).ToList())
                {
                    await _oTPManagerRepository.DeleteAsync(item.Id);
                }

                //otp
                var otp = EncryptionUtils.GenerateOtp();

                //send email to user
                MailDataDto mailData = new MailDataDto()
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    OTP = otp
                };

                //encrypt and send user email
                long timeStamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                //encode userid in state
                var jsonData = new
                {
                    email = request.Email,
                    timeStamp = timeStamp
                };

                // Convert the anonymous type to a JSON string
                string jsonString = JsonSerializer.Serialize(jsonData);
                var encryptedString = EncryptionUtils.EncryptString(jsonString);

                //saved
                OTPManager passwordReset = new OTPManager();
                passwordReset.Type = (byte)OTPType.ResetPassword;
                passwordReset.Email = request.Email;
                passwordReset.Otp = otp;
                passwordReset.OtpValidTill = DateTime.Now.AddMinutes(10);
                passwordReset.IsOtpVerified = false;
                passwordReset.Token = encryptedString;
                passwordReset.TokenValidTill = DateTime.Now.AddMinutes(10);

                var response = await _oTPManagerRepository.AddAsync(passwordReset);

                await MailUtils.SendEmail(mailSettings, mailData, EmailType.PasswordReset);

                return ApiResponse<object>.SuccessResponse(new
                {
                    Code = encryptedString
                });
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.InternalServerErrorResponse($"An error occurred while sending OTP. {ex.Message}");
            }
        }
    }
}
