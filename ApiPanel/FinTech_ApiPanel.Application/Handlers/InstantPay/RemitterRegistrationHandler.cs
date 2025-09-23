using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Application.Abstraction.IHeaders;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.URLs;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IAuth;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.InstantPay;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace FinTech_ApiPanel.Application.Handlers.InstantPay
{
    public class RemitterRegistrationHandler : IRequestHandler<RemitterRegistrationCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IHeaderService _headerService;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public RemitterRegistrationHandler(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ITransactionLogRepository transactionLogRepository,
            ICryptoService cryptoService,
            IHeaderService headerService,
            IClientRepository clientRepository,
            IUserRepository userRepository,
            IOptions<ApiSetting> baseUrl,
            ITokenService tokenService)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _transactionLogRepository = transactionLogRepository;
            _cryptoService = cryptoService;
            _headerService = headerService;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            this.baseUrl = baseUrl.Value.BaseUrl;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(RemitterRegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();

                var adminUser = await _userRepository.GetAdminAsync();
                if (string.IsNullOrEmpty(outletId))
                    throw new Exception("Outlet ID not found in the context.");
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User ID not found in the context.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await RegisterRemitterAsync(request, ip, outletId, adminUser.Id);

                var dto = JsonConvert.DeserializeObject<RemitterRegistrationDto>(response);

                await LogTransaction(dto, request, endpointIp, outletId, userId, response);

                return dto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> RegisterRemitterAsync(RemitterRegistrationCommand request, string endpointIp, string outletId, long adminId)
        {
            var url = baseUrl + InstantPayEndpoints.Remittance.DomesticV2.RemitterRegistration;

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

            // Recrypt Aadhaar
            var aadhar = await RecryptedAadhar(request.EncryptedAadhaar, adminId);

            var payload = new
            {
                mobileNumber = request.MobileNumber,
                encryptedAadhaar = aadhar,
                referenceKey = request.ReferenceKey
            };

            // Headers
            await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task LogTransaction(RemitterRegistrationDto dto, RemitterRegistrationCommand request, string endpointIp, string outletId, string userId, string response)
        {
            var transactionLog = new TransactionLog
            {
                UserId = long.Parse(userId),
                LogType = (byte)LogType.BeneficiaryRegistration_IPay,
                EndPointIP = endpointIp,
                Ipay_OrderId = dto.OrderId,
                AuditType = (byte)AuditType.Other,
                Amount = 0,
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

            if (string.Equals(statusCode, "OTP", StringComparison.OrdinalIgnoreCase))
            {
                transactionLog.Status = (byte)Status.Pending;
                transactionLog.Remark = dto.Status ?? "OTP Sent for Registration";
            }
            else if (string.Equals(statusCode, "TXN", StringComparison.OrdinalIgnoreCase))
            {
                transactionLog.Status = (byte)Status.Success;
                transactionLog.Remark = dto.Status ?? "Remitter Registered";
            }
            else
            {
                transactionLog.Status = (byte)Status.Failed;
                transactionLog.Remark = dto.Status ?? "Remitter Registration Failed";
            }

            await _transactionLogRepository.AddAsync(transactionLog);
        }

        async Task<string> RecryptedAadhar(string encryptedAadhar, long adminId)
        {
            try
            {
                string masterEncryptionKey = string.Empty;

                var clientInfo = await _clientRepository.GetByUserIdAsync(adminId);
                if (clientInfo != null)
                    masterEncryptionKey = clientInfo?.EncryptionKey;
                else
                    throw new Exception("Client data missing");

                var encryptionKey = _httpContextAccessor.HttpContext?.Items["Encryption-Key"]?.ToString();
                var aadhaarNumber = _cryptoService.DecryptAadhaar(encryptedAadhar, encryptionKey);
                return _cryptoService.EncryptAadhaar(aadhaarNumber, masterEncryptionKey);
            }
            catch (Exception ex)
            {
                throw new Exception("Error recrypting Aadhaar number", ex);
            }
        }
    }
}
