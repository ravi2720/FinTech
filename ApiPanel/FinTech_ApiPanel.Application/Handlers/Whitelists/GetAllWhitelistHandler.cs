using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.Whitelists;
using FinTech_ApiPanel.Domain.DTOs.Whitelists;
using FinTech_ApiPanel.Domain.Interfaces.IWhitelists;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.Whitelists
{
    public class GetAllWhitelistHandler : IRequestHandler<GetAllWhitelistQuery, ApiResponse<List<WhitelistDto>>>
    {
        private readonly IWhitelistRepository _whitelistRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public GetAllWhitelistHandler(IWhitelistRepository whitelistRepository,
            ITokenService tokenService,
            IMapper mapper)
        {
            _whitelistRepository = whitelistRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<List<WhitelistDto>>> Handle(GetAllWhitelistQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();

                if (loggedInUser == null)
                    return ApiResponse<List<WhitelistDto>>.UnauthorizedResponse("User not authenticated");

                var whitelistEntries = await _whitelistRepository.GetByUserIdAsync(loggedInUser.UserId);
                whitelistEntries = whitelistEntries.Where(x => x.EntryType == request.EntryType).ToList();
                List<WhitelistDto> whitelist = new List<WhitelistDto>();

                foreach (var entry in whitelistEntries)
                {
                    var whitelistDto = _mapper.Map<WhitelistDto>(entry);

                    whitelist.Add(whitelistDto);
                }

                return ApiResponse<List<WhitelistDto>>.SuccessResponse(whitelist);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<WhitelistDto>>.BadRequestResponse($"Error getting whilist: {ex.Message}");
            }
        }
    }
}
