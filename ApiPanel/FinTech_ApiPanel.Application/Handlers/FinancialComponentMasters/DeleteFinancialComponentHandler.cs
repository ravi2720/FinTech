using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.FinancialComponentMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IFinancialComponentMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.FinancialComponentMasters
{
    public class DeleteFinancialComponentHandler : IRequestHandler<DeleteFinancialComponentCommand, ApiResponse>
    {
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly ITokenService _tokenService;

        public DeleteFinancialComponentHandler(IFinancialComponentMasterRepository financialComponentMasterRepository,
            ITokenService tokenService)
        {
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(DeleteFinancialComponentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("User not authorized.");

                if (!loggedInUser.IsAdmin)
                    return ApiResponse.UnauthorizedResponse("Access denied.");

                var exist = await _financialComponentMasterRepository.GetByIdAsync(request.Id);

                if (exist == null)
                    return ApiResponse.NotFoundResponse("Not found");

                await _financialComponentMasterRepository.DeleteAsync(exist.Id);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while deleting: {ex.Message}");
            }
        }
    }
}
