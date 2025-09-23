using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.UserBanks;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserBanks;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserBanks
{
    public class GetAllUserBankHandler : IRequestHandler<GetAllUserBankQuery, ApiResponse<List<UserBank>>>
    {
        private readonly IUserBankRepository _userBankRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public GetAllUserBankHandler(IUserBankRepository userBankRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userBankRepository = userBankRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<List<UserBank>>> Handle(GetAllUserBankQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInuser = _tokenService.GetLoggedInUserinfo();

                if (loggedInuser == null)
                    return ApiResponse<List<UserBank>>.UnauthorizedResponse("User not authenticated");
                if (loggedInuser.IsAdmin && !request.UserId.HasValue)
                    return ApiResponse<List<UserBank>>.BadRequestResponse("Please provide userId");
                if (!loggedInuser.IsAdmin && request.UserId.HasValue && loggedInuser.UserId != request.UserId)
                    return ApiResponse<List<UserBank>>.ForbiddenResponse("You do not have permission to access this resource");

                var userBanks = await _userBankRepository.GetAllAsync();

                if (loggedInuser.IsAdmin && request.UserId.HasValue)
                    userBanks = userBanks.Where(x => x.UserId == request.UserId.Value);
                else if (!loggedInuser.IsAdmin)
                    userBanks = userBanks.Where(x => x.UserId == loggedInuser.UserId);

                if (userBanks == null || !userBanks.Any())
                    return ApiResponse<List<UserBank>>.NotFoundResponse("No banks found");

                return ApiResponse<List<UserBank>>.SuccessResponse(userBanks.ToList());
            }
            catch (Exception ex)
            {
                return ApiResponse<List<UserBank>>.BadRequestResponse($"An error occurred while retrieving the banks: {ex.Message}");
            }
        }
    }
}
