using FinTech_ApiPanel.Application.Abstraction.IHeaders;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.URLs;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.InstantPay;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace FinTech_ApiPanel.Application.Handlers.InstantPay
{
    public class GenerateTransactionOtpHandler : IRequestHandler<GenerateTransactionOtpCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IHeaderService _headerService;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public GenerateTransactionOtpHandler(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ITransactionLogRepository transactionLogRepository,
            IHeaderService headerService,
            IOptions<ApiSetting> baseUrl,
            ITokenService tokenService)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _transactionLogRepository = transactionLogRepository;
            _headerService = headerService;
            this.baseUrl = baseUrl.Value.BaseUrl;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(GenerateTransactionOtpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User ID not found in context.");

                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();

                if (string.IsNullOrEmpty(outletId))
                    throw new Exception("Outlet ID not found in the context.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await SendOtpRequestAsync(request, ip, outletId);
                var otpResponse = JsonConvert.DeserializeObject<GenerateTransactionOtpDto>(response);

                await LogTransaction(otpResponse, request, endpointIp, outletId, userId, response);

                return otpResponse;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> SendOtpRequestAsync(GenerateTransactionOtpCommand request, string endpointIp, string outletId)
        {
            var url = baseUrl + InstantPayEndpoints.Remittance.DomesticV2.GenerateTransactionOtp;

            var body = new
            {
                remitterMobileNumber = request.RemitterMobileNumber,
                amount = request.Amount,
                referenceKey = request.ReferenceKey
            };

            var json = JsonConvert.SerializeObject(body);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Headers
            await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

            var response = await _httpClient.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task LogTransaction(GenerateTransactionOtpDto dto, GenerateTransactionOtpCommand command, string endpointIp, string outletId, string userId, string response)
        {
            try
            {
                var log = new TransactionLog
                {
                    UserId = long.Parse(userId),
                    LogType = (byte)LogType.DMTTransactionOtp_IPay,
                    EndPointIP = endpointIp,
                    Ipay_OrderId = dto.OrderId,
                    AuditType = (byte)AuditType.Other,
                    Ipay_Uuid = dto.IpayUuid,
                    Ipay_Environment = dto.Environment,
                    Ipay_ActCode = dto.ActCode,
                    Ipay_StatusCode = dto.StatusCode,
                    Ipay_Timestamp = DateTime.Parse(dto.Timestamp),
                    CreatedBy = long.Parse(userId),
                    Ipay_OutletId = outletId,
                    ReferenceId = dto.Data?.ReferenceKey,
                    Status = dto.StatusCode == "OTP" ? (byte)Status.Pending : (byte)Status.Failed,
                    Remark = dto.Status ?? "OTP Triggered",
                    Amount = 0,
                    Ipay_Response = response,
                    MobileNumber = command.RemitterMobileNumber,
                };

                await _transactionLogRepository.AddAsync(log);
            }
            catch (Exception)
            {
            }
        }
    }
}
