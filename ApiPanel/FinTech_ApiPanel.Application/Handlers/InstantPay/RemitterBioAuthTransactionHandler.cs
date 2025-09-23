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
    public class RemitterBioAuthTransactionHandler : IRequestHandler<RemitterBioAuthTransactionCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IHeaderService _headerService;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public RemitterBioAuthTransactionHandler(
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

        public async Task<object> Handle(RemitterBioAuthTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();

                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User ID not found.");

                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();

                if (string.IsNullOrEmpty(outletId))
                    throw new Exception("Outlet ID not found.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await SendBioTransactionRequest(request, ip, outletId, request.Latitude, request.Longitude);
                var dto = JsonConvert.DeserializeObject<RemitterBioAuthTransactionDto>(response);

                await LogTransaction(dto, request, endpointIp, outletId, userId, response);

                return dto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> SendBioTransactionRequest(RemitterBioAuthTransactionCommand request, string endpointIp, string outletId, string latitude, string longitude)
        {
            var url = baseUrl + InstantPayEndpoints.Remittance.DomesticV2.BioAuthTransaction;

            var requestBody = new
            {
                remitterMobileNumber = request.RemitterMobileNumber,
                referenceKey = request.ReferenceKey,
                latitude = latitude,
                longitude = longitude,
                otp = request.Otp,
                amount = request.Amount,
                externalRef = request.ExternalRef,
                consentTaken = request.ConsentTaken,
                biometricData = request.BiometricData
            };

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

            // Headers
            await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

            var json = JsonConvert.SerializeObject(requestBody);
            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task LogTransaction(RemitterBioAuthTransactionDto dto, RemitterBioAuthTransactionCommand command, string endpointIp, string outletId, string userId, string response)
        {
            var transactionLog = new TransactionLog
            {
                UserId = long.Parse(userId),
                LogType = (byte)LogType.BioRemitTxn,
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
                ReferenceId = command.ExternalRef,
                Remark = dto.Status ?? "Remitter BioAuth Transaction",
                Status = dto.StatusCode == "TXN" ? (byte)Status.Success : (byte)Status.Failed,
                Ipay_Response = response,
                Ipay_Latitude = command.Latitude,
                Ipay_Longitude = command.Longitude,
            };

            await _transactionLogRepository.AddAsync(transactionLog);
        }
    }
}
