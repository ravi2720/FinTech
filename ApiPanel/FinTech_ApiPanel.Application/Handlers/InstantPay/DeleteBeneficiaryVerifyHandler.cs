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
    public class DeleteBeneficiaryVerifyHandler : IRequestHandler<DeleteBeneficiaryVerifyCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IHeaderService _headerService;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public DeleteBeneficiaryVerifyHandler(
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

        public async Task<object> Handle(DeleteBeneficiaryVerifyCommand request, CancellationToken cancellationToken)
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

                var response = await PerformDeleteVerificationAsync(request, ip, outletId);
                var responseDto = JsonConvert.DeserializeObject<DeleteBeneficiaryVerifyDto>(response);

                await LogTransaction(responseDto, request, endpointIp, outletId, userId, response);

                return responseDto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> PerformDeleteVerificationAsync(DeleteBeneficiaryVerifyCommand command, string endpointIp, string outletId)
        {
            var url = baseUrl + InstantPayEndpoints.Remittance.DomesticV2.BeneficiaryDeleteVerify;

            var requestBody = new
            {
                remitterMobileNumber = command.RemitterMobileNumber,
                beneficiaryId = command.BeneficiaryId,
                otp = command.Otp,
                referenceKey = command.ReferenceKey
            };

            var json = JsonConvert.SerializeObject(requestBody);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

            // Headers
            await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

            var response = await _httpClient.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task LogTransaction(DeleteBeneficiaryVerifyDto dto, DeleteBeneficiaryVerifyCommand command, string endpointIp, string outletId, string userId, string response)
        {
            var log = new TransactionLog
            {
                UserId = long.Parse(userId),
                LogType = (byte)LogType.BeneficiaryDeleteVerify_IPay,
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
                Status = (byte)(dto.StatusCode == "TXN" ? Status.Success : Status.Failed),
                Remark = dto.Status ?? "Beneficiary deleted.",
                Ipay_Response = response,
            };

            await _transactionLogRepository.AddAsync(log);
        }
    }
}
