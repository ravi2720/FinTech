using FinTech_ApiPanel.Application.Commands.UserBanks;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IUserBanks;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserBanks
{
    public class DeleteUserBankHandler : IRequestHandler<DeleteUserBankCommand, ApiResponse>
    {
        private readonly IUserBankRepository _userBankRepository;

        public DeleteUserBankHandler(IUserBankRepository userBankRepository)
        {
            _userBankRepository = userBankRepository;
        }
        public async Task<ApiResponse> Handle(DeleteUserBankCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _userBankRepository.DeleteAsync(request.Id);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while deleting the user bank: {ex.Message}");
            }
        }
    }
}
