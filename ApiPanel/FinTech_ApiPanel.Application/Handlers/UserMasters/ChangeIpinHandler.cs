using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IOTPManagers;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.Utils;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    internal class ChangeIpinHandler : IRequestHandler<ChangeIpinCommand, ApiResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOTPManagerRepository _oTPManagerRepository;
        private readonly ICryptoService _cryptoService;

        public ChangeIpinHandler(IUserRepository userRepository,
            ICryptoService cryptoService,
            IOTPManagerRepository oTPManagerRepository)
        {
            _userRepository = userRepository;
            _cryptoService = cryptoService;
            _oTPManagerRepository = oTPManagerRepository;
        }

        public async Task<ApiResponse> Handle(ChangeIpinCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.NewIpin < 100000 || request.NewIpin > 999999)
                    return ApiResponse.BadRequestResponse("New IPIN must be exactly 6 digits.");

                var responseList = await _oTPManagerRepository.GetAllAsync();

                var responseOtp = responseList.Where(x => x.Token == request.Token
                && x.Type == (byte)OTPType.ChangeIPin
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

                user.IPin = request.NewIpin;

                await _userRepository.UpdateAsync(user);

                //delete old OTP
                foreach (var item in responseList.Where(x => x.Email == responseOtp.Email).ToList())
                {
                    await _oTPManagerRepository.DeleteAsync(item.Id);
                }

                return ApiResponse.SuccessResponse("Ipin changed successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse("An error occurred while change the Ipin.");
            }
        }
    }
}
