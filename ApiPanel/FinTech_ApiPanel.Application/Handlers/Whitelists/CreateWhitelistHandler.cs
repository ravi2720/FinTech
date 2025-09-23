using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.Whitelists;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.Whitelists;
using FinTech_ApiPanel.Domain.Entities.Whitelists;
using FinTech_ApiPanel.Domain.Interfaces.IWhitelists;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.Whitelists
{
    public class CreateWhitelistHandler : IRequestHandler<CreateWhitelistCommand, ApiResponse>
    {
        private readonly IWhitelistRepository _whitelistRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public CreateWhitelistHandler(IWhitelistRepository whitelistRepository,
            ITokenService tokenService,
            IMapper mapper)
        {
            _whitelistRepository = whitelistRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(CreateWhitelistCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();

                if (loggedInUser == null)
                    return ApiResponse<List<WhitelistDto>>.UnauthorizedResponse("User not authenticated");

                var whitelistEntries = await _whitelistRepository.GetByUserIdAsync(loggedInUser.UserId);

                if (whitelistEntries.Any(x => x.Value == request.Value))
                    return ApiResponse<WhitelistDto>.ForbiddenResponse("Already exist");

                var whiteListEntry = _mapper.Map<WhitelistEntry>(request);

                await _whitelistRepository.AddAsync(whiteListEntry);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"Error deleting: {ex.Message}");
            }
        }
    }
}
