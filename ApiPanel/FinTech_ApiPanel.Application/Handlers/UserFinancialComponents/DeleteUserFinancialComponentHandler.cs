using AutoMapper;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.UserFinancialComponents;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Interfaces.IFinancialComponentMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserFinancialComponents;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserFinancialComponents
{
    public class DeleteUserFinancialComponentHandler : IRequestHandler<DeleteUserFinancialComponentCommand, ApiResponse>
    {
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly ITokenService _tokenService;

        public DeleteUserFinancialComponentHandler(IUserFinancialComponentRepository userFinancialComponentRepository,
            ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userFinancialComponentRepository = userFinancialComponentRepository;
        }

        public async Task<ApiResponse> Handle(DeleteUserFinancialComponentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse.UnauthorizedResponse("User not authorized.");

                if (!loggedInUser.IsAdmin)
                    return ApiResponse.UnauthorizedResponse("Access denied.");

                var exist = await _userFinancialComponentRepository.GetByIdAsync(request.Id);

                if (exist == null)
                    return ApiResponse.NotFoundResponse("Not found");

                await _userFinancialComponentRepository.DeleteAsync(exist.Id);

                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse.BadRequestResponse($"An error occurred while deleting: {ex.Message}");
            }
        }
    }
}
