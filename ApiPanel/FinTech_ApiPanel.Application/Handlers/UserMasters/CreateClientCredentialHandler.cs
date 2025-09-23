using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.UserMasters;
using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.Auth;
using FinTech_ApiPanel.Domain.Interfaces.IAuth;
using MediatR;

namespace FinTech_ApiPanel.Application.Handlers.UserMasters
{
    public class CreateClientCredentialHandler : IRequestHandler<CreateClientCredentialCommand, ApiResponse>
    {
        private readonly IClientRepository _clientRepository;
        public readonly ITokenService _tokenService;
        public CreateClientCredentialHandler(IClientRepository clientRepository,
            ITokenService tokenService)
        {
            _clientRepository = clientRepository;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse> Handle(CreateClientCredentialCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedInUser = _tokenService.GetLoggedInUserinfo();
                if (loggedInUser == null)
                    return ApiResponse<object>.UnauthorizedResponse("User not authorized.");
                if (!loggedInUser.IsAdmin)
                    return ApiResponse<object>.UnauthorizedResponse("Access denied.");

                var existingClient = await _clientRepository.GetByUserIdAsync(loggedInUser.UserId);
                if (existingClient != null)
                {
                    existingClient.ClientId = request.ClientId;
                    existingClient.ClientSecret = request.ClientSecret;
                    existingClient.EncryptionKey = request.EncryptionKey;
                    await _clientRepository.UpdateAsync(existingClient);
                }
                else
                {
                    var bankMaster = await _clientRepository.AddAsync(new Client()
                    {
                        UserId = loggedInUser.UserId,
                        ClientId = request.ClientId,
                        ClientSecret = request.ClientSecret,
                        EncryptionKey = request.EncryptionKey
                    });
                }
                return ApiResponse.SuccessResponse();
            }
            catch (Exception ex)
            {
                return ApiResponse<object>.InternalServerErrorResponse(ex.Message);
            }
        }
    }
}
