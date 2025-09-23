using AutoMapper;
using FinTech_ApiPanel.Application.Commands.UserBanks;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IUserBanks;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserBanks
{
    public class UpdateUserBankHandler : IRequestHandler<UpdateUserBankCommand, ApiResponse>
    {
        private readonly IUserBankRepository _userBankRepository;
        private readonly IMapper _mapper;

        public UpdateUserBankHandler(IUserBankRepository userBankRepository,
            IMapper mapper)
        {
            _userBankRepository = userBankRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UpdateUserBankCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var existingBank = await _userBankRepository.GetByAccountNumberAsync(request.AccountNumber);
                if (existingBank != null && existingBank.Id != request.Id)
                    return ApiResponse.ForbiddenResponse("A bank with this account number already exists.");

                var bankMaster = await _userBankRepository.GetByIdAsync(request.Id);
                if (bankMaster == null)
                    return ApiResponse.NotFoundResponse("Bank not found");

                _mapper.Map(request, bankMaster);
                await _userBankRepository.UpdateAsync(bankMaster);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while updating the bank: {ex.Message}");
            }
        }
    }
}
