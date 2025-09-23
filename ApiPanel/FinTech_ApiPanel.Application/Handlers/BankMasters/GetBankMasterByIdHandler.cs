using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.BankMasters;
using FinTech_ApiPanel.Domain.Entities.BankMasters;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.BankMasters
{
    public class GetBankByIdQueryHandler : IRequestHandler<GetBankMasterByIdQuery, ApiResponse<BankMaster>>
    {
        private readonly IBankMasterRepository _bankMasterRepository;

        public GetBankByIdQueryHandler(IBankMasterRepository bankMasterRepository)
        {
            _bankMasterRepository = bankMasterRepository;
        }

        public async Task<ApiResponse<BankMaster>> Handle(GetBankMasterByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bankMaster = await _bankMasterRepository.GetByIdAsync(request.Id);
                if (bankMaster == null)
                    return ApiResponse<BankMaster>.NotFoundResponse("Bank not found");

                return ApiResponse<BankMaster>.SuccessResponse(bankMaster);
            }
            catch (Exception ex)
            {
                return ApiResponse<BankMaster>.BadRequestResponse($"An error occurred while retrieving the bank: {ex.Message}");
            }
        }
    }
}
