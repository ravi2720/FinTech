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
    public class BeneficiaryRegistrationVerifyHandler : IRequestHandler<BeneficiaryRegistrationVerifyCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IHeaderService _headerService;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public BeneficiaryRegistrationVerifyHandler(
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

        public async Task<object> Handle(BeneficiaryRegistrationVerifyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User ID not found in context.");

                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();

                if (string.IsNullOrEmpty(outletId))
                    throw new Exception("Outlet ID not found in context.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await PerformVerificationAsync(request, ip, outletId);

                var dto = JsonConvert.DeserializeObject<BeneficiaryRegistrationVerifyDto>(response);

                await LogTransaction(dto, request, endpointIp, outletId, userId, response);

                return dto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> PerformVerificationAsync(BeneficiaryRegistrationVerifyCommand command, string endpointIp, string outletId)
        {
            var url = baseUrl + InstantPayEndpoints.Remittance.DomesticV2.BeneficiaryRegistrationVerify;

            var payload = new
            {
                remitterMobileNumber = command.RemitterMobileNumber,
                otp = command.Otp,
                beneficiaryId = command.BeneficiaryId,
                referenceKey = command.ReferenceKey
            };

            var json = JsonConvert.SerializeObject(payload);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Headers
            await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

            var response = await _httpClient.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task LogTransaction(BeneficiaryRegistrationVerifyDto dto, BeneficiaryRegistrationVerifyCommand command, string endpointIp, string outletId, string userId, string response)
        {
            var transactions = await _transactionLogRepository.GetByUserIdAsync(long.Parse(userId));

            var lastTransactions = transactions
                .FirstOrDefault(t => t.LogType == (byte)LogType.BeneficiaryRegistration_IPay && t.ReferenceId == command.ReferenceKey);

            var log = new TransactionLog
            {
                UserId = long.Parse(userId),
                LogType = (byte)LogType.BeneficiaryRegistrationVerify_IPay,
                EndPointIP = endpointIp,
                AuditType = (byte)AuditType.Other,
                Amount = 0,
                Ipay_OrderId = dto.OrderId,
                Ipay_Uuid = dto.IpayUuid,
                Ipay_Environment = dto.Environment,
                Ipay_ActCode = dto.ActCode,
                Ipay_StatusCode = dto.StatusCode,
                Ipay_Timestamp = DateTime.Parse(dto.Timestamp),
                CreatedBy = long.Parse(userId),
                Ipay_OutletId = outletId,
                ReferenceId = dto.Data?.BeneficiaryId,
                Status = (byte)(dto.StatusCode == "TXN" ? Status.Success : Status.Failed),
                Remark = dto.Status ?? "Beneficiary registration completed.",
                Ipay_Response = response,
                MobileNumber = lastTransactions?.MobileNumber,
            };

            await _transactionLogRepository.AddAsync(log);
        }
    }
}
