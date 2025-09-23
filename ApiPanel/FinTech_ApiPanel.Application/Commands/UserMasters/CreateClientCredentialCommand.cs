using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.UserMasters
{
    public class CreateClientCredentialCommand : IRequest<ApiResponse>
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string EncryptionKey { get; set; }
    };
}
