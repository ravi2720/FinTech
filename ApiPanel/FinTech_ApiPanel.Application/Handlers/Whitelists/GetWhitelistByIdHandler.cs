using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.Whitelists;
using FinTech_ApiPanel.Domain.DTOs.Whitelists;
using FinTech_ApiPanel.Domain.Interfaces.IWhitelists;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.Whitelists
{
    public class GetWhitelistByIdHandler : IRequestHandler<GetWhitelistByIdQuery, ApiResponse<WhitelistDto>>
    {
        private readonly IWhitelistRepository _whitelistRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        public GetWhitelistByIdHandler(IWhitelistRepository whitelistRepository,
            ITokenService tokenService,
            IMapper mapper)
        {
            _whitelistRepository = whitelistRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<WhitelistDto>> Handle(GetWhitelistByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();

                if (loggedInUser == null)
                    return ApiResponse<WhitelistDto>.UnauthorizedResponse("User not authenticated");

                var whitelistInfo = await _whitelistRepository.GetByIdAsync(request.Id);

                if (whitelistInfo == null)
                    return ApiResponse<WhitelistDto>.NoContentResponse("Not found.");

                if (whitelistInfo.CreatedBy != loggedInUser.UserId)
                    return ApiResponse<WhitelistDto>.ForbiddenResponse("Access denied.");

                var responseDto = _mapper.Map<WhitelistDto>(whitelistInfo);

                return ApiResponse<WhitelistDto>.SuccessResponse(responseDto);
            }
            catch (Exception ex)
            {
                return ApiResponse<WhitelistDto>.BadRequestResponse($"Error getting by id: {ex.Message}");
            }
        }
    }
}
