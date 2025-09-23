using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
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
    internal class ChangeIpin_SendOTPHandler : IRequestHandler<ChangeIpin_SendOTPCommand, ApiResponse<object>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOTPManagerRepository _oTPManagerRepository;
        private readonly MailSettingDto mailSettings;
        private readonly ITokenService _tokenService;

        public ChangeIpin_SendOTPHandler(IUserRepository userRepository,
            ICryptoService cryptoService,
            IOTPManagerRepository oTPManagerRepository,
            IOptions<MailSettingDto> mailSettings,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _oTPManagerRepository = oTPManagerRepository;
            this.mailSettings = mailSettings.Value;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<object>> Handle(ChangeIpin_SendOTPCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();

                if (loggedInUser == null)
                    return ApiResponse<object>.NoContentResponse();

                var user = await _userRepository.GetByIdAsync(loggedInUser.UserId);

                if (user == null)
                    return ApiResponse<object>.UnauthorizedResponse("Access denied.");

                //delete old OTP
                var oldOTP = await _oTPManagerRepository.GetAllAsync();

                foreach (var item in oldOTP.Where(x => x.Email == user.Email && x.Type == (byte)OTPType.ChangeIPin).ToList())
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
                    email = user.Email,
                    timeStamp = timeStamp
                };

                // Convert the anonymous type to a JSON string
                string jsonString = JsonSerializer.Serialize(jsonData);
                var encryptedString = EncryptionUtils.EncryptString(jsonString);

                //saved
                OTPManager passwordReset = new OTPManager();
                passwordReset.Type = (byte)OTPType.ChangeIPin;
                passwordReset.Email = user.Email;
                passwordReset.Otp = otp;
                passwordReset.OtpValidTill = DateTime.Now.AddMinutes(10);
                passwordReset.IsOtpVerified = false;
                passwordReset.Token = encryptedString;
                passwordReset.TokenValidTill = DateTime.Now.AddMinutes(10);

                var response = await _oTPManagerRepository.AddAsync(passwordReset);

                await MailUtils.SendEmail(mailSettings, mailData, EmailType.ChangeIPin);

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
