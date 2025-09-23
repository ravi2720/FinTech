using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.Utils;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, ApiResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public UpdateUserHandler(IUserRepository userRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (!loggedInUser.IsAdmin && loggedInUser.UserId != request.Id)
                    return ApiResponse.UnauthorizedResponse("You are not authorized to update this user.");

                var user = await _userRepository.GetByIdAsync(request.Id);
                if (user == null)
                    return ApiResponse.NotFoundResponse("User not found.");
                if (user.KYCVerified && !loggedInUser.IsAdmin)
                    return ApiResponse.NotFoundResponse("Access denied.");

                _mapper.Map(request, user);

                // Handle profile picture and company logo updates
                await ManageProfile(user, request.Profile);
                await ManageLogo(user, request.logo);

                var success = await _userRepository.UpdateAsync(user);

                return success
                    ? ApiResponse.SuccessResponse("User updated successfully.")
                    : ApiResponse.BadRequestResponse("Failed to update user.");
            }
            catch (Exception)
            {
                return ApiResponse.BadRequestResponse("An error occurred while updating the user.");
            }
        }

        async Task ManageProfile(UserMaster user, IFormFile? profile)
        {
            try
            {
                //delete old profile picture if exists
                if (!string.IsNullOrEmpty(user.ProfilePicture))
                    await FileUtils.DeleteFile(user.ProfilePicture, FilePath.ProfilePicture);

                //upload new profile picture
                if (profile != null)
                    user.ProfilePicture = await FileUtils.FileUpload(profile, FilePath.ProfilePicture);
                else
                    user.ProfilePicture = null; // Clear profile picture if no file is provided
            }
            catch (Exception)
            {

                throw;
            }
        }

        async Task ManageLogo(UserMaster user, IFormFile? logo)
        {
            try
            {
                //delete old profile picture if exists
                if (!string.IsNullOrEmpty(user.CompanyLogo))
                    await FileUtils.DeleteFile(user.CompanyLogo, FilePath.CompanyLogo);

                //upload new profile picture
                if (logo != null)
                    user.CompanyLogo = await FileUtils.FileUpload(logo, FilePath.CompanyLogo);
                else
                    user.CompanyLogo = null; // Clear company logo if no file is provided
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
