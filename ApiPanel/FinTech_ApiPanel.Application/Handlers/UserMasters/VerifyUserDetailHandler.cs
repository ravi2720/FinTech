using AutoMapper;
using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class VerifyUserDetailHandler : IRequestHandler<VerifyUserDetailCommand, ApiResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public VerifyUserDetailHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(VerifyUserDetailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //Get user
                var user = await _userRepository.GetByIdAsync(request.UserId);

                if (user == null)
                    return ApiResponse.NoContentResponse();

                // check if username exists
                if (user == null)
                    throw new Exception("User don't exist!");

                _mapper.Map(request, user);

                var success = await _userRepository.UpdateAsync(user);

                return success
                    ? ApiResponse.SuccessResponse("User updated successfully.")
                    : ApiResponse.BadRequestResponse("Failed to update user.");
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.BadRequestResponse(ex.Message);
            }
        }
    }
}
