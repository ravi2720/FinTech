using FinTech_ApiPanel.Application.Abstraction.IHeaders;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.URLs;
using FinTech_ApiPanel.Domain.Shared.InstantPay;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace FinTech_ApiPanel.Application.Handlers.InstantPay
{
    public class TxnStatusHandler : IRequestHandler<TxnStatusCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHeaderService _headerService;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public TxnStatusHandler(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            IHeaderService headerService,
            IOptions<ApiSetting> baseUrl,
            ITokenService tokenService)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _headerService = headerService;
            this.baseUrl = baseUrl.Value.BaseUrl;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(TxnStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                if (string.IsNullOrEmpty(endpointIp)) throw new Exception("Endpoint IP missing");

                if (string.IsNullOrEmpty(request.ExternalRef))
                    throw new Exception("External reference is required for transaction status.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await PerformTxnStatusAsync(request.TransactionDate, request.ExternalRef, ip);

                var txnStatusDto = JsonConvert.DeserializeObject<TxnStatusDto>(response);

                return txnStatusDto;
            }
            catch (Exception ex)
            {
                return new { status = "Failed", message = ex.Message };
            }
        }

        private async Task<string> PerformTxnStatusAsync(string transactionDate, string externalRef, string endpointIp)
        {
            try
            {
                var url = baseUrl + InstantPayEndpoints.Reports.TransactionStatus;

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

                // Headers
                await _headerService.AddHeaders(requestMessage, endpointIp);

                // Body
                var requestBody = new
                {
                    transactionDate = transactionDate,
                    externalRef = externalRef,
                    source = "ORDER"
                };

                var json = JsonConvert.SerializeObject(requestBody);
                requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(requestMessage);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
