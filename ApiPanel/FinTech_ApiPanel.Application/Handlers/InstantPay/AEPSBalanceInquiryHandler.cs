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
    public class AEPSBalanceInquiryHandler : IRequestHandler<AEPSBalanceInquiryCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICryptoService _cryptoService;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IHeaderService _headerService;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public AEPSBalanceInquiryHandler(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ICryptoService cryptoService,
            ITransactionLogRepository transactionLogRepository,
            IHeaderService headerService,
            IClientRepository clientRepository,
            IUserRepository userRepository,
            IOptions<ApiSetting> baseUrl,
            ITokenService tokenService)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _cryptoService = cryptoService;
            _transactionLogRepository = transactionLogRepository;
            _headerService = headerService;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
            this.baseUrl = baseUrl.Value.BaseUrl;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(AEPSBalanceInquiryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();

                var adminUser = await _userRepository.GetAdminAsync();
                if (adminUser == null)
                    throw new Exception("Something went wrong.");

                if (string.IsNullOrEmpty(outletId))
                    throw new Exception("Outlet ID not found in the context.");

                if (string.IsNullOrEmpty(request.ExternalRef))
                    throw new Exception("External reference is required for balance inquiry.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await PerformBalanceInquiryAsync(request, ip, outletId, request.ExternalRef, adminUser.Id, request.Latitude, request.Longitude);

                var balanceInquiryDto = JsonConvert.DeserializeObject<BalanceInquiryDto>(response);

                // Log the transaction
                await LogTransaction(balanceInquiryDto, request, endpointIp, outletId, request.ExternalRef, response);

                return balanceInquiryDto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> PerformBalanceInquiryAsync(AEPSBalanceInquiryCommand command, string endpointIp, string outletId, string externalRef, long adminId, string latitude, string longitude)
        {
            try
            {
                var url = baseUrl + InstantPayEndpoints.AEPS.BalanceInquiry;

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

                // Headers
                await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

                //encode aadhaar
                var aadhar = await RecryptedAadhar(command.BiometricData.EncryptedAadhaar, adminId);

                // Request body
                var requestBody = new
                {
                    bankiin = command.BankIIN,
                    latitude = latitude,
                    longitude = longitude,
                    mobile = command.Mobile,
                    externalRef = externalRef,
                    captureType = command.CaptureType,
                    biometricData = new
                    {
                        encryptedAadhaar = aadhar,
                        dc = command.BiometricData.Dc,
                        ci = command.BiometricData.Ci,
                        hmac = command.BiometricData.Hmac,
                        dpId = command.BiometricData.DpId,
                        mc = command.BiometricData.Mc,
                        pidDataType = command.BiometricData.PidDataType,
                        sessionKey = command.BiometricData.SessionKey,
                        mi = command.BiometricData.Mi,
                        rdsId = command.BiometricData.RdsId,
                        errCode = command.BiometricData.ErrCode,
                        errInfo = command.BiometricData.ErrInfo,
                        fCount = command.BiometricData.FCount,
                        fType = command.BiometricData.FType,
                        iCount = command.BiometricData.ICount,
                        iType = command.BiometricData.IType,
                        pCount = command.BiometricData.PCount,
                        pType = command.BiometricData.PType,
                        srno = command.BiometricData.SrNo,
                        sysid = command.BiometricData.SysId,
                        ts = command.BiometricData.Timestamp,
                        pidData = command.BiometricData.PidData,
                        qScore = command.BiometricData.QScore,
                        nmPoints = command.BiometricData.NmPoints,
                        rdsVer = command.BiometricData.RdsVer
                    }
                };

                var json = JsonConvert.SerializeObject(requestBody);
                requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(requestMessage);
                var content = await response.Content.ReadAsStringAsync();

                return content;
            }
            catch
            {
                throw;
            }
        }

        public async Task LogTransaction(BalanceInquiryDto dto, AEPSBalanceInquiryCommand request, string endpointIp, string outletId, string externalRef, string response)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User ID not found in the context.");
                }

                // Log the transaction
                var transactionLog = new TransactionLog()
                {
                    UserId = long.Parse(userId),
                    LogType = (byte)LogType.AEPSBalanceEnquiry_IPay,
                    Amount = 0,
                    AuditType = (byte)AuditType.Other,
                    EndPointIP = endpointIp,
                    Ipay_OrderId = dto.OrderId,
                    Ipay_Uuid = dto.IpayUuid,
                    Ipay_Environment = dto.Environment,
                    Ipay_ActCode = dto.ActCode,
                    Ipay_StatusCode = dto.StatusCode,
                    Ipay_Timestamp = DateTime.Parse(dto.Timestamp),
                    CreatedBy = long.Parse(userId),
                    Ipay_OutletId = outletId,
                    ReferenceId = externalRef,
                    CaptureType = request.CaptureType,
                    Ipay_Response = response,
                    MobileNumber = request.Mobile,
                    Ipay_Latitude = request.Latitude,
                    Ipay_Longitude = request.Longitude,
                };

                var statusCode = dto?.StatusCode?.Trim();

                if (string.Equals(statusCode, "TXN", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Success;
                    transactionLog.Remark = dto.Status ?? "Balance Inquiry Successful";
                }
                else if (string.Equals(statusCode, "TUP", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Pending;
                    transactionLog.Remark = dto.Status ?? "Balance Inquiry Pending";
                }
                else
                {
                    transactionLog.Status = (byte)Status.Failed;
                    transactionLog.Remark = dto.Status ?? "Balance Inquiry Failed";
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
