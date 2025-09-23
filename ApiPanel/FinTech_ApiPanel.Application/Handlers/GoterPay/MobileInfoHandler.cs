using FinTech_ApiPanel.Application.Queries.GoterPay;
using FinTech_ApiPanel.Domain.DTOs.GoterPay;
using FinTech_ApiPanel.Domain.DTOs.URLs;
using FinTech_ApiPanel.Domain.Interfaces.IAuth;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using FinTech_ApiPanel.Domain.Shared.Common;
using FinTech_ApiPanel.Domain.Shared.Goterpay;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json;

namespace FinTech_ApiPanel.Application.Handlers.GoterPay
{
    public class MobileInfoHandler : IRequestHandler<MobileInfoQuery, object>
    {
        private readonly HttpClient _httpClient;
        private readonly GoterPayConfig _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly bool _isMaster;
        private readonly string baseUrl;

        public MobileInfoHandler(HttpClient httpClient,
            IOptions<GoterPayConfig> config,
            IHttpContextAccessor httpContextAccessor,
            IWalletRepository walletRepository,
            IUserRepository userRepository,
            IClientRepository clientRepository,
            IOptions<PanelSettings> panelSettings,
            IOptions<ApiSetting> baseUrl)
        {
            _httpClient = httpClient;
            _config = config.Value;
            _httpContextAccessor = httpContextAccessor;
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
            _isMaster = panelSettings.Value.IsMasterPanel;
            this.baseUrl = baseUrl.Value.BaseUrl;
        }

        public async Task<object> Handle(MobileInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User ID not found in the context.");

                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();

                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();
                if (string.IsNullOrEmpty(outletId))
                    throw new Exception("Outlet ID not found in the context.");

                if (string.IsNullOrWhiteSpace(request.Mobile))
                    throw new ArgumentException("Mobile number is required");

                if (_isMaster)
                {
                    var responseContent = await GetMobileInfoAsync(request.Mobile);

                    var response = JsonConvert.DeserializeObject<MobileInfoDto>(responseContent);

                    return response;

                }
                else
                {
                    var responseContent = await GetMobileInfoAsync(request, outletId, endpointIp);

                    var response = JsonConvert.DeserializeObject<MobileInfoDto>(responseContent);

                    return response;
                }

            }
            catch (Exception ex)
            {
                var errorJson = new
                {
                    statusCode = "ERR",
                    status = $"Exception: {ex.Message}",
                    data = new { },
                    timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    orderid = string.Empty,
                };

                return System.Text.Json.JsonSerializer.Serialize(errorJson);
            }
        }

        private async Task<string> GetMobileInfoAsync(string mobile)
        {
            try
            {
                var url = $"https://api.goterpay.com/mobileinfo" +
                          $"?mid={Uri.EscapeDataString(_config.Mid)}" +
                          $"&mkey={Uri.EscapeDataString(_config.MKey)}" +
                          $"&mobile={Uri.EscapeDataString(mobile)}";

                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                var rawJson = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(rawJson);
                var root = doc.RootElement;

                var status = root.GetProperty("status").GetString();
                var message = root.GetProperty("message").GetString();
                var operatorValue = root.TryGetProperty("operator", out var op) ? op.GetString() : "";
                var circleValue = root.TryGetProperty("circle", out var cir) ? cir.GetString() : "";

                // Build custom JSON
                var custom = new
                {
                    statuscode = status == "SUCCESS" ? "TXN" : "ERR",
                    status = status == "SUCCESS" ? "Transaction Successful" : message,
                    data = new
                    {
                        operatorName = operatorValue,
                        circle = circleValue
                    },
                    timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    orderid = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(),
                };

                return System.Text.Json.JsonSerializer.Serialize(custom);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> GetMobileInfoAsync(MobileInfoQuery query, string outletId, string endpointIp)
        {
            try
            {
                var clientConfig = await _clientRepository.GetAdminClientAsync();

                string url = $"{baseUrl}/Recharge/mobile-info?mobile={Uri.EscapeDataString(query.Mobile)}";

                using var request = new HttpRequestMessage(HttpMethod.Get, url);

                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Auth-Code", "1");
                request.Headers.Add("Client-Id", clientConfig.ClientId);
                request.Headers.Add("Client-Secret", clientConfig.ClientSecret);
                request.Headers.Add("Endpoint-Ip", endpointIp);
                request.Headers.Add("Outlet-Id", outletId);

                // Send request
                var response = await _httpClient.SendAsync(request);
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
