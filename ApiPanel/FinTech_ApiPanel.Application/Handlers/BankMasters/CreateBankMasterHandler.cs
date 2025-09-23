using AutoMapper;
using FinTech_ApiPanel.Application.Commands.BankMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.BankMasters;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.BankMasters
{
    public class CreateBankMasterHandler : IRequestHandler<CreateBankMasterCommand, ApiResponse>
    {
        private readonly IBankMasterRepository _bankMasterRepository;
        private readonly IMapper _mapper;

        public CreateBankMasterHandler(IBankMasterRepository bankMasterRepository,
            IMapper mapper)
        {
            _bankMasterRepository = bankMasterRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(CreateBankMasterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if a bank with the same account number already exists
                var existingBank = await _bankMasterRepository.GetByAccountNumberAsync(request.AccountNumber);
                if (existingBank != null)
                    return ApiResponse.ForbiddenResponse("A bank with this account number already exists.");

                var bankMaster = _mapper.Map<BankMaster>(request);
                await _bankMasterRepository.AddAsync(bankMaster);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while creating the bank: {ex.Message}");
            }
        }
    }
}
