using FinTech_ApiPanel.Application.Commands.BankMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.BankMasters
{
    public class ToggleBankMasterStatusHandler : IRequestHandler<ToggleBankMasterStatusCommand, ApiResponse>
    {
        private readonly IBankMasterRepository _bankMasterRepository;

        public ToggleBankMasterStatusHandler(IBankMasterRepository bankMasterRepository)
        {
            _bankMasterRepository = bankMasterRepository;
        }

        public async Task<ApiResponse> Handle(ToggleBankMasterStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var bankMaster = await _bankMasterRepository.GetByIdAsync(request.Id);
                if (bankMaster == null)
                    return ApiResponse.NotFoundResponse("Bank not found");

                bankMaster.IsActive = !bankMaster.IsActive;
                await _bankMasterRepository.UpdateAsync(bankMaster);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while toggling the bank status: {ex.Message}");
            }
        }
    }
}
