using FinTech_ApiPanel.Application.Abstraction.IHeaders;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Queries.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.URLs;
using FinTech_ApiPanel.Domain.Shared.InstantPay;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Handlers.InstantPay
{
    public class GetAEPSBankHandler : IRequestHandler<GetAEPSBankQuery, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHeaderService _headerService;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public GetAEPSBankHandler(HttpClient httpClient,
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


        public async Task<object> Handle(GetAEPSBankQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();

                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User ID not found in the context.");
                }

                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();
                if (string.IsNullOrEmpty(outletId))
                {
                    throw new Exception("Outlet ID not found in the context.");
                }

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await GetAEPSBanksAsync(ip, outletId);

                // Deserialize the response into EKYCInitiateDto
                var ekycDto = JsonConvert.DeserializeObject<AepsBankDto>(response);

                return ekycDto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        public async Task<string> GetAEPSBanksAsync(string endpointIp, string outletId)
        {
            try
            {
                string url = baseUrl + InstantPayEndpoints.AEPS.AEPSBanks;

                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);

                // Headers
                await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

                // Send request
                var response = await _httpClient.SendAsync(requestMessage);
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching AEPS banks.", ex);
            }
        }
    }
}
