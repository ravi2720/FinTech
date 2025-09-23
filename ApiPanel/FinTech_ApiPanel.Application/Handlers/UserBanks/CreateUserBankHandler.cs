using AutoMapper;
using FinTech_ApiPanel.Application.Commands.UserBanks;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.BankMasters;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserBanks;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserBanks
{
    public class CreateUserBankHandler : IRequestHandler<CreateUserBankCommand, ApiResponse>
    {
        private readonly IUserBankRepository _userBankRepository;
        private readonly IMapper _mapper;

        public CreateUserBankHandler(IUserBankRepository userBankRepository,
            IMapper mapper)
        {
            _userBankRepository = userBankRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(CreateUserBankCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if a bank with the same account number already exists
                var existingBank = await _userBankRepository.GetByAccountNumberAsync(request.AccountNumber);
                if (existingBank != null)
                    return ApiResponse<BankMaster>.ForbiddenResponse("A bank with this account number already exists.");

                var userBank = _mapper.Map<UserBank>(request);
                userBank.IsActive = false; // Default to inactive
                await _userBankRepository.AddAsync(userBank);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while creating the bank: {ex.Message}");
            }
        }
    }
}
