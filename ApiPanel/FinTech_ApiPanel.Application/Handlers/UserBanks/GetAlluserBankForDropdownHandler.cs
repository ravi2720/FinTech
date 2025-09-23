using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.UserBanks;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserBanks;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserBanks
{
    public class GetAlluserBankForDropdownHandler : IRequestHandler<GetuserBankForDropdownQuery, ApiResponse<List<UserBankDto>>>
    {
        private readonly IUserBankRepository _userBankRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public GetAlluserBankForDropdownHandler(IUserBankRepository userBankRepository,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userBankRepository = userBankRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }
        public async Task<ApiResponse<List<UserBankDto>>> Handle(GetuserBankForDropdownQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInuser = _tokenService.GetLoggedInUserinfo();

                if (loggedInuser == null)
                    return ApiResponse<List<UserBankDto>>.UnauthorizedResponse("User not authenticated");

                var userBanks = await _userBankRepository.GetAllAsync();

                if (!loggedInuser.IsAdmin)
                    userBanks = userBanks.Where(x => x.UserId == loggedInuser.UserId);

                List<UserBankDto> bankMasterDtos = new List<UserBankDto>();

                foreach (var bank in userBanks)
                {
                    if (!bank.IsActive)
                        continue;

                    var bankDto = _mapper.Map<UserBankDto>(bank);
                    bankMasterDtos.Add(bankDto);
                }

                return ApiResponse<List<UserBankDto>>.SuccessResponse(bankMasterDtos);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<UserBankDto>>.BadRequestResponse($"An error occurred while retrieving the banks: {ex.Message}");
            }
        }
    }
}
