using AutoMapper;
using FinTech_ApiPanel.Application.Commands.BankMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.BankMasters;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.BankMasters
{
    public class UpdateBankMasterHandler : IRequestHandler<UpdateBankMasterCommand, ApiResponse>
    {
        private readonly IBankMasterRepository _bankMasterRepository;
        private readonly IMapper _mapper;

        public UpdateBankMasterHandler(IBankMasterRepository bankMasterRepository,
            IMapper mapper)
        {
            _bankMasterRepository = bankMasterRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(UpdateBankMasterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingBank = await _bankMasterRepository.GetByAccountNumberAsync(request.AccountNumber);
                if (existingBank != null && existingBank.Id != request.Id)
                    return ApiResponse.ForbiddenResponse("A bank with this account number already exists.");

                var bankMaster = await _bankMasterRepository.GetByIdAsync(request.Id);
                if (bankMaster == null)
                    return ApiResponse.NotFoundResponse("Bank not found");

                _mapper.Map(request, bankMaster);
                await _bankMasterRepository.UpdateAsync(bankMaster);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while updating the bank: {ex.Message}");
            }
        }
    }
}
