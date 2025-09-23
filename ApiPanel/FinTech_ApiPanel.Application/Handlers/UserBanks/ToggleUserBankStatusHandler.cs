using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.UserBanks;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserBanks;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserBanks
{
    public class ToggleUserBankStatusHandler : IRequestHandler<ToggleUserBankStatusCommand, ApiResponse>
    {
        private readonly IUserBankRepository _userBankRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public ToggleUserBankStatusHandler(IUserBankRepository userBankRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userBankRepository = userBankRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(ToggleUserBankStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInuser = _tokenService.GetLoggedInUserinfo();

                if (loggedInuser == null)
                    return ApiResponse<List<UserBankDto>>.UnauthorizedResponse("User not authenticated");

                var userBank = await _userBankRepository.GetByIdAsync(request.Id);

                if (!loggedInuser.IsAdmin && loggedInuser.UserId != userBank.UserId)
                    return ApiResponse.ForbiddenResponse("You do not have permission to access this resource");

                if (userBank == null)
                    return ApiResponse.NotFoundResponse("Bank not found");

                userBank.IsActive = !userBank.IsActive;
                await _userBankRepository.UpdateAsync(userBank);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while toggling the bank status: {ex.Message}");
            }
        }
    }
}
