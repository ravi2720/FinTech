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
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace FinTech_ApiPanel.Application.Handlers.GoterPay
{
    public class ROfferHandler : IRequestHandler<ROfferQuery, object>
    {
        private readonly HttpClient _httpClient;
        private readonly GoterPayConfig _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly bool _isMaster;
        private readonly string baseUrl;

        public ROfferHandler(HttpClient httpClient,
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

        public async Task<object> Handle(ROfferQuery request, CancellationToken cancellationToken)
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

                if (string.IsNullOrWhiteSpace(request.OperatorCode))
                    throw new ArgumentException("Operator code is required");
                if (string.IsNullOrWhiteSpace(request.Number))
                    throw new ArgumentException("Mobile number is required");

                if (_isMaster)
                {
                    var responseContent = await GetROfferAsync(request);

                    var response = JsonConvert.DeserializeObject<ROfferDto>(responseContent);

                    return response;
                }
                else
                {
                    var responseContent = await GetROfferAsync(request, outletId, endpointIp);

                    var response = JsonConvert.DeserializeObject<ROfferDto>(responseContent);

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

        private async Task<string> GetROfferAsync(ROfferQuery request)
        {
            try
            {
                var url = $"https://api.goterpay.com/Roffer" +
                          $"?mid={Uri.EscapeDataString(_config.Mid)}" +
                          $"&mkey={Uri.EscapeDataString(_config.MKey)}" +
                          $"&operator={Uri.EscapeDataString(request.OperatorCode)}" +
                          $"&number={Uri.EscapeDataString(request.Number)}";

                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                var rawJson = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(rawJson);
                var root = doc.RootElement;

                var status = root.GetProperty("Status").GetString();
                var message = root.GetProperty("Message").GetString();

                // Extract Data array (if present, otherwise fallback to empty array)
                JsonElement dataElement = root.TryGetProperty("Data", out var d) ? d : JsonDocument.Parse("[]").RootElement;

                // Build custom JSON
                var custom = new
                {
                    statuscode = status == "SUCCESS" ? "TXN" : "ERR",
                    status = status == "SUCCESS" ? "Transaction Successful" : message,
                    data = dataElement, // inject array directly
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

        private async Task<string> GetROfferAsync(ROfferQuery query, string outletId, string endpointIp)
        {
            try
            {
                var clientConfig = await _clientRepository.GetAdminClientAsync();

                string url = $"{baseUrl}/Recharge/offer?operatorCode={query.OperatorCode}&number={query.Number}";

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
