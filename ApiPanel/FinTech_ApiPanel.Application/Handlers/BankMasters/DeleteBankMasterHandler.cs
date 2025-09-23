using FinTech_ApiPanel.Application.Commands.BankMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IBankMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.BankMasters
{
    public class DeleteBankMasterHandler : IRequestHandler<DeleteBankMasterCommand, ApiResponse>
    {
        private readonly IBankMasterRepository _bankMasterRepository;

        public DeleteBankMasterHandler(IBankMasterRepository bankMasterRepository)
        {
            _bankMasterRepository = bankMasterRepository;
        }

        public async Task<ApiResponse> Handle(DeleteBankMasterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _bankMasterRepository.DeleteAsync(request.Id);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while deleting the bank master.{ex}");
            }
        }
    }
}
