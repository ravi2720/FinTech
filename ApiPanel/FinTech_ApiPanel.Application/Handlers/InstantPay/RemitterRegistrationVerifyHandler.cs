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
    public class RemitterRegistrationVerifyHandler : IRequestHandler<RemitterRegistrationVerifyCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IHeaderService _headerService;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public RemitterRegistrationVerifyHandler(
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

        public async Task<object> Handle(RemitterRegistrationVerifyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();

                if (string.IsNullOrEmpty(outletId))
                    throw new Exception("Outlet ID not found in the context.");
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User ID not found in the context.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await VerifyOtpAsync(request, ip, outletId);
                var dto = JsonConvert.DeserializeObject<RemitterRegistrationVerifyDto>(response);

                await LogTransaction(dto, request, endpointIp, outletId, userId, response);

                return dto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> VerifyOtpAsync(RemitterRegistrationVerifyCommand request, string endpointIp, string outletId)
        {
            var url = baseUrl + InstantPayEndpoints.Remittance.DomesticV2.RemitterRegistrationVerify;

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

            // Headers
            await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

            var payload = new
            {
                mobileNumber = request.MobileNumber,
                otp = request.Otp,
                referenceKey = request.ReferenceKey
            };

            var json = JsonConvert.SerializeObject(payload);
            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task LogTransaction(RemitterRegistrationVerifyDto dto, RemitterRegistrationVerifyCommand request, string endpointIp, string outletId, string userId, string response)
        {
            var transactionLog = new TransactionLog
            {
                UserId = long.Parse(userId),
                LogType = (byte)LogType.RemitterRegistrationVerify_IPay,
                EndPointIP = endpointIp,
                Ipay_OrderId = dto.OrderId,
                Amount = 0,
                AuditType = (byte)AuditType.Other,
                Ipay_Uuid = dto.IpayUuid,
                Ipay_Environment = dto.Environment,
                Ipay_ActCode = dto.ActCode,
                Ipay_StatusCode = dto.StatusCode,
                Ipay_Timestamp = DateTime.Parse(dto.Timestamp),
                CreatedBy = long.Parse(userId),
                Ipay_OutletId = outletId,
                ReferenceId = request.ReferenceKey,
                Ipay_Response = response,
                MobileNumber = request.MobileNumber,
            };

            var statusCode = dto?.StatusCode?.Trim();

            if (string.Equals(statusCode, "KYC", StringComparison.OrdinalIgnoreCase))
            {
                transactionLog.Status = (byte)Status.Success;
                transactionLog.Remark = dto.Status ?? "OTP Verified. KYC Required.";
            }
            else
            {
                transactionLog.Status = (byte)Status.Failed;
                transactionLog.Remark = dto.Status ?? "Remitter OTP Verification Failed.";
            }

            await _transactionLogRepository.AddAsync(transactionLog);
        }
    }
}
