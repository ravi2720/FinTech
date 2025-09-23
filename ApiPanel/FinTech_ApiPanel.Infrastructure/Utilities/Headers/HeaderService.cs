using FinTech_ApiPanel.Application.Abstraction.IHeaders;
using FinTech_ApiPanel.Domain.Entities.Auth;
using FinTech_ApiPanel.Domain.Interfaces.IAuth;
using FinTech_ApiPanel.Domain.Shared.Common;
using Microsoft.Extensions.Options;

namespace FinTech_ApiPanel.Infrastructure.Utilities.Headers
{
    public class HeaderService : IHeaderService
    {
        private readonly IClientRepository _clientRepository;
        private readonly bool _isMaster;

        public HeaderService(IClientRepository clientRepository,
            IOptions<PanelSettings> panelSettings)
        {
            _clientRepository = clientRepository;
            _isMaster = panelSettings.Value.IsMasterPanel;
        }

        public async Task AddHeaders(HttpRequestMessage request, string endpointIp, string? outletId = null, Dictionary<string, string>? additionalHeaders = null)
        {
            var clientConfig = await _clientRepository.GetAdminClientAsync();

            if (_isMaster)
                MasterPanel(clientConfig, request, endpointIp, outletId, additionalHeaders);
            else
                ClientPanel(clientConfig, request, endpointIp, outletId, additionalHeaders);
        }

        public void MasterPanel(Client clientConfig, HttpRequestMessage request, string endpointIp, string? outletId = null, Dictionary<string, string>? additionalHeaders = null)
        {

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("X-Ipay-Auth-Code", "1");
            request.Headers.Add("X-Ipay-Client-Id", clientConfig.ClientId);
            request.Headers.Add("X-Ipay-Client-Secret", clientConfig.ClientSecret);
            request.Headers.Add("X-Ipay-Endpoint-Ip", endpointIp);

            if (!string.IsNullOrWhiteSpace(outletId))
            {
                request.Headers.Add("X-Ipay-Outlet-Id", outletId);
            }

            if (additionalHeaders != null)
            {
                foreach (var header in additionalHeaders)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }

        public void ClientPanel(Client clientConfig, HttpRequestMessage request, string endpointIp, string? outletId = null, Dictionary<string, string>? additionalHeaders = null)
        {
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Auth-Code", "1");
            request.Headers.Add("Client-Id", clientConfig.ClientId);
            request.Headers.Add("Client-Secret", clientConfig.ClientSecret);
            request.Headers.Add("Endpoint-Ip", endpointIp);

            if (!string.IsNullOrWhiteSpace(outletId))
            {
                request.Headers.Add("Outlet-Id", outletId);
            }

            if (additionalHeaders != null)
            {
                foreach (var header in additionalHeaders)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }
        }
    }
}
