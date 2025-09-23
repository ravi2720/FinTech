using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.BankMasters;
using FinTech_ApiPanel.Domain.Entities.BankMasters;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.BankMasters
{
    public class GetAllBankMasterHandler : IRequestHandler<GetAllBankMasterQuery, ApiResponse<List<BankMaster>>>
    {
        private readonly IBankMasterRepository _bankMasterRepository;

        public GetAllBankMasterHandler(IBankMasterRepository bankMasterRepository)
        {
            _bankMasterRepository = bankMasterRepository;
        }

        public async Task<ApiResponse<List<BankMaster>>> Handle(GetAllBankMasterQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bankMasters = await _bankMasterRepository.GetAllAsync();
                if (bankMasters == null || !bankMasters.Any())
                    return ApiResponse<List<BankMaster>>.SuccessResponse();


                if (!string.IsNullOrEmpty(request.Title))
                    bankMasters = bankMasters.Where(b => b.BankName.Contains(request.Title));

                return ApiResponse<List<BankMaster>>.SuccessResponse(bankMasters.ToList());
            }
            catch (Exception ex)
            {
                return ApiResponse<List<BankMaster>>.BadRequestResponse($"An error occurred while retrieving the banks: {ex.Message}");
            }
        }
    }
}
