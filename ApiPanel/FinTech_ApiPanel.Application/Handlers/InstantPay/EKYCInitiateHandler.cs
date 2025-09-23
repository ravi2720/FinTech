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
using FinTech_ApiPanel.Domain.Shared.Common;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.InstantPay;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace FinTech_ApiPanel.Application.Handlers.InstantPay
{
    public class EKYCInitiateHandler : IRequestHandler<EKYCInitiateCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IHeaderService _headerService;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly string baseUrl;
        private readonly bool _isMaster;
        private readonly ITokenService _tokenService;

        public EKYCInitiateHandler(HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ITransactionLogRepository transactionLogRepository,
            ICryptoService cryptoService,
            IHeaderService headerService,
            IUserRepository userRepository,
            IClientRepository clientRepository,
            IOptions<ApiSetting> baseUrl,
            IOptions<PanelSettings> panelSettings,
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
            _isMaster = panelSettings.Value.IsMasterPanel;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(EKYCInitiateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User ID not found in the context.");
                }

                var adminUser = await _userRepository.GetAdminAsync();
                if (adminUser == null)
                    throw new Exception("Something went wrong.");

                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                if (string.IsNullOrEmpty(endpointIp))
                {
                    throw new Exception("Endpoint IP not found in the context.");
                }

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await SignupEKYCInitiateAsync(request, ip, adminUser.Id, request.Latitude, request.Longitude);

                // Deserialize the response into EKYCInitiateDto
                var ekycDto = JsonConvert.DeserializeObject<EKYCInitiateDto>(response);

                // Log the transaction
                await LogTransaction(ekycDto, request, endpointIp, userId, response);

                return ekycDto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> SignupEKYCInitiateAsync(EKYCInitiateCommand request, string endpointIp, long adminId, string latitude, string longitude, string? outletId = null)
        {
            try
            {
                var url = baseUrl + InstantPayEndpoints.UserOutlet.SignupInitiate;

                if (_isMaster)
                    url = baseUrl + "/user" + InstantPayEndpoints.UserOutlet.SignupInitiate;

                // Encode Aadhaar
                var aadhar = await RecryptedAadhar(request.Aadhaar, adminId);

                var body = new
                {
                    mobile = request.Mobile,
                    email = request.Email,
                    aadhaar = aadhar,
                    pan = request.Pan,
                    bankAccountNo = request.BankAccountNo,
                    bankIfsc = request.BankIfsc,
                    latitude = latitude,
                    longitude = longitude,
                    consent = request.Consent
                };

                var json = JsonConvert.SerializeObject(body);
                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
                {
                    Content = new StringContent(json, Encoding.UTF8, "application/json")
                };

                // Header service
                await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

                var response = await _httpClient.SendAsync(requestMessage);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task LogTransaction(EKYCInitiateDto ekycDto, EKYCInitiateCommand request, string endpointIp, string userId, string response)
        {
            try
            {
                // Log the transaction
                var transactionLog = new TransactionLog()
                {
                    UserId = long.Parse(userId),
                    LogType = (byte)LogType.AEPOnboardInitiate_IPay,
                    EndPointIP = endpointIp,
                    AuditType = (byte)AuditType.Other,
                    Amount = 0,
                    Ipay_OrderId = ekycDto.OrderId,
                    Ipay_Uuid = ekycDto.IpayUuid,
                    Ipay_Environment = ekycDto.Environment,
                    Ipay_ActCode = ekycDto.ActCode,
                    Ipay_StatusCode = ekycDto.StatusCode,
                    Ipay_Timestamp = DateTime.Parse(ekycDto.Timestamp),
                    Ipay_Latitude = request.Latitude,
                    Ipay_Longitude = request.Longitude,
                    CreatedBy = long.Parse(userId),
                    Ipay_Response = response,
                    MobileNumber = request.Mobile,
                    Email = request.Email,
                };

                var statusCode = ekycDto?.StatusCode?.Trim();

                if (string.Equals(statusCode, "TXN", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Success;
                    transactionLog.Remark = ekycDto.Status ?? "EKYC Initiation Successful";
                    transactionLog.ReferenceId = ekycDto.Data?.OtpReferenceId;
                }
                else if (string.Equals(statusCode, "TUP", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Pending;
                    transactionLog.Remark = ekycDto.Status ?? "EKYC Initiation Pending";
                }
                else
                {
                    transactionLog.Status = (byte)Status.Failed;
                    transactionLog.Remark = ekycDto.Status ?? $"EKYC Validation Failed";
                }

                await _transactionLogRepository.AddAsync(transactionLog);

            }
            catch (Exception)
            {
                throw;
            }
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
                var aadharNumber = _cryptoService.DecryptAadhaar(encryptedAadhar, encryptionKey);
                var recryptedAadhar = _cryptoService.EncryptAadhaar(aadharNumber, masterEncryptionKey);
                return recryptedAadhar;
            }
            catch (Exception ex)
            {
                throw new Exception("Error recrypting Aadhaar number", ex);
            }
        }
    }
}
