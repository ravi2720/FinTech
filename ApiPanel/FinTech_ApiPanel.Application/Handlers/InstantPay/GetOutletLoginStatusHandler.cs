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
    public class GetOutletLoginStatusHandler : IRequestHandler<GetOutletLoginStatusCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHeaderService _headerService;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public GetOutletLoginStatusHandler(HttpClient httpClient,
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

        public async Task<object> Handle(GetOutletLoginStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();

                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();
                if (string.IsNullOrEmpty(outletId))
                {
                    throw new Exception("Outlet ID not found in the context.");
                }

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await GetOutletLoginStatusAsync(request.Type, ip, outletId);

                // Deserialize the response into EKYCInitiateDto
                var loginStatusDto = JsonConvert.DeserializeObject<OutletLoginStatusDto>(response);

                return loginStatusDto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        public async Task<string> GetOutletLoginStatusAsync(string? type, string endpointIp, string outletId)
        {
            try
            {
                var url = baseUrl + InstantPayEndpoints.AEPS.OutletLoginStatus;

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

                // Set headers
                // Headers
                await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

                // Prepare JSON body
                string jsonBody = string.IsNullOrWhiteSpace(type)
                    ? "{}"
                    : JsonConvert.SerializeObject(new { type = type });

                requestMessage.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(requestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
