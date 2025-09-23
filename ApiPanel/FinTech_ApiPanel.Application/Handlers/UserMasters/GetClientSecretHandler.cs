using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Application.Queries.UserMaster;
using FinTech_ApiPanel.Domain.Interfaces.IAuth;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class GetClientSecretHandler : IRequestHandler<GetClientSecretQuery, ApiResponse<object>>
    {
        private readonly IClientRepository _clientRepository;
        private readonly ITokenService _tokenService;

        public GetClientSecretHandler(IClientRepository clientRepository, ITokenService tokenService)
        {
            _clientRepository = clientRepository;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<object>> Handle(GetClientSecretQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<object>.UnauthorizedResponse("User not authenticated or not authorized to access client secret.");

                // Map client information
                var client = await _clientRepository.GetByUserIdAsync(loggedInUser.UserId);

                if (client == null)
                    return ApiResponse<object>.NotFoundResponse("Secret not found for the authenticated user.");

                return ApiResponse<object>.SuccessResponse(new { Secret = client.ClientSecret });
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.BadRequestResponse($"An error occurred while retrieving the secret: {ex.Message}");
            }
        }
    }
}
