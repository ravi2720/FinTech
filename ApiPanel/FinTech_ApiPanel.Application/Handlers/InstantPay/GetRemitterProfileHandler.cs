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
using System.Buffers.Text;
using System.Text;

namespace FinTech_ApiPanel.Application.Handlers.InstantPay
{
    public class GetRemitterProfileHandler : IRequestHandler<GetRemitterProfileCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHeaderService _headerService;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public GetRemitterProfileHandler(HttpClient httpClient,
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

        public async Task<object> Handle(GetRemitterProfileCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();

                if (string.IsNullOrEmpty(endpointIp))
                    throw new Exception("Endpoint IP not found in the context.");

                if (string.IsNullOrEmpty(outletId))
                    throw new Exception("Outlet ID not found in the context.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await FetchRemitterProfileAsync(request.MobileNumber, ip, outletId);

                var dto = JsonConvert.DeserializeObject<RemitterProfileDto>(response);
                return dto;
            }
            catch (Exception ex)
            {
                return new { status = "error", message = ex.Message };
            }
        }

        private async Task<string> FetchRemitterProfileAsync(string mobileNumber, string endpointIp, string outletId)
        {
            try
            {
                string url = baseUrl + InstantPayEndpoints.Remittance.DomesticV2.RemitterProfile;

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

                // Headers
                await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

                var body = new { mobileNumber = mobileNumber };
                var json = JsonConvert.SerializeObject(body);
                requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(requestMessage);
                string content = await response.Content.ReadAsStringAsync();

                return content;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching remitter profile.", ex);
            }
        }
    }

}
