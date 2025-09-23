using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.UserMaster;
using FinTech_ApiPanel.Domain.DTOs.BankMasters;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IAuth;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserBanks;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class GetLoggedInUserHandler : IRequestHandler<GetLoggedInUserQuery, ApiResponse<UserMasterDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;
        private readonly IUserBankRepository _userBankRepository;
        private readonly IBankMasterRepository _bankMasterRepository;

        public GetLoggedInUserHandler(IUserRepository userRepository,
            ITokenService _tokenService,
            IClientRepository clientRepository,
            IUserBankRepository userBankRepository,
            IBankMasterRepository bankMasterRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            this._tokenService = _tokenService;
            _clientRepository = clientRepository;
            _userBankRepository = userBankRepository;
            _bankMasterRepository = bankMasterRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<UserMasterDto>> Handle(GetLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<UserMasterDto>.NotFoundResponse("User not found.");

                var user = await _userRepository.GetByIdAsync(loggedInUser.UserId);
                if (user == null)
                    return ApiResponse<UserMasterDto>.NotFoundResponse("User not found.");

                var userDto = _mapper.Map<UserMasterDto>(user);
                if (user.IsAdmin)
                    userDto.Role = "Admin";
                else userDto.Role = "User";

                if (user.IsAdmin)
                {
                    //map user bank details
                    var userBank = await _bankMasterRepository.GetAllAsync();

                    foreach (var bank in userBank)
                    {
                        var bankDto = _mapper.Map<BankDto>(bank);
                        userDto.UserBanks.Add(bankDto);
                    }
                }
                else
                {
                    //map user bank details
                    var userBank = await _userBankRepository.GetByUserIdAsync(user.Id);

                    foreach (var bank in userBank)
                    {
                        var bankDto = _mapper.Map<BankDto>(bank);
                        userDto.UserBanks.Add(bankDto);
                    }
                }

                // Map client information
                var client = await _clientRepository.GetByUserIdAsync(user.Id);
                if (client != null)
                {
                    userDto.ClientId = client.ClientId;
                    userDto.EncryptionKey = client.EncryptionKey;
                }

                return ApiResponse<UserMasterDto>.SuccessResponse(userDto, "User retrieved successfully.");
            }
            catch (Exception ex)
            {
                return ApiResponse<UserMasterDto>.BadRequestResponse($"An error occurred while retrieving the user: {ex.Message}");
            }
        }
    }
}
