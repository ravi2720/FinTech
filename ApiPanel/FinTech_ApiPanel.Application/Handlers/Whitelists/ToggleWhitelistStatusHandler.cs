using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.Whitelists;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.Whitelists;
using FinTech_ApiPanel.Domain.Interfaces.IWhitelists;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.Whitelists
{
    public class ToggleWhitelistStatusHandler : IRequestHandler<ToggleWhitelistStatusCommand, ApiResponse>
    {
        private readonly IWhitelistRepository _whitelistRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public ToggleWhitelistStatusHandler(IWhitelistRepository whitelistRepository,
            ITokenService tokenService,
            IMapper mapper)
        {
            _whitelistRepository = whitelistRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task<ApiResponse> Handle(ToggleWhitelistStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();

                if (loggedInUser == null)
                    return ApiResponse<List<WhitelistDto>>.UnauthorizedResponse("User not authenticated");

                var whitelistInfo = await _whitelistRepository.GetByIdAsync(request.Id);

                if (whitelistInfo == null)
                    return ApiResponse<WhitelistDto>.NoContentResponse("Not found.");

                if (whitelistInfo.CreatedBy != loggedInUser.UserId)
                    return ApiResponse<WhitelistDto>.ForbiddenResponse("Access denied.");

                whitelistInfo.IsActive=!whitelistInfo.IsActive;

                await _whitelistRepository.UpdateAsync(whitelistInfo);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"Error deleting: {ex.Message}");
            }
        }
    }
}
