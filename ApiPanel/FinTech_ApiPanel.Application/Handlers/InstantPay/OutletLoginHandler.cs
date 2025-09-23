using FinTech_ApiPanel.Application.Abstraction.ICryptography;
using FinTech_ApiPanel.Application.Abstraction.IHeaders;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.URLs;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Entities.UserWallets;
using FinTech_ApiPanel.Domain.Interfaces.IAuth;
using FinTech_ApiPanel.Domain.Interfaces.IFinancialComponentMasters;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserFinancialComponents;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.InstantPay;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace FinTech_ApiPanel.Application.Handlers.InstantPay
{
    public class OutletLoginHandler : IRequestHandler<OutletLoginCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly ICryptoService _cryptoService;
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHeaderService _headerService;
        private readonly IClientRepository _clientRepository;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public OutletLoginHandler(HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ITransactionLogRepository transactionLogRepository,
            ICryptoService cryptoService,
            IUserRepository userRepository,
            IFinancialComponentMasterRepository financialComponentMasterRepository,
            IUserFinancialComponentRepository userFinancialComponentRepository,
            IWalletRepository walletRepository,
            IHeaderService headerService,
            IClientRepository clientRepository,
            IOptions<ApiSetting> baseUrl,
            ITokenService tokenService)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _transactionLogRepository = transactionLogRepository;
            _cryptoService = cryptoService;
            _userRepository = userRepository;
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _userFinancialComponentRepository = userFinancialComponentRepository;
            _walletRepository = walletRepository;
            _headerService = headerService;
            _clientRepository = clientRepository;
            this.baseUrl = baseUrl.Value.BaseUrl;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(OutletLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //check  user wallet
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User ID not found in the context.");
                }

                var adminUser = await _userRepository.GetAdminAsync();
                var adminWallet = await _walletRepository.GetByUserIdAsync(adminUser.Id);
                var userWallet = await _walletRepository.GetByUserIdAsync(long.Parse(userId));

                if (adminWallet == null || userWallet == null)
                    throw new Exception("Wallets not found.");

                var userSurcharge = await GetSurcharge(userWallet);
                if ((userWallet.TotalBalance - userWallet.HeldAmount) < userSurcharge)
                    throw new Exception("Insufficient balance in user wallet.");

                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();

                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();
                if (string.IsNullOrEmpty(outletId))
                    throw new Exception("Outlet ID not found in the context.");

                if (string.IsNullOrEmpty(request.ExternalRef))
                    throw new Exception("External reference is required for outlet login.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await PerformOutletLoginAsync(request, ip, outletId, request.ExternalRef, adminUser.Id, request.Latitude, request.Longitude);

                // Deserialize the response into EKYCInitiateDto
                var outletLoginDto = JsonConvert.DeserializeObject<OutletLoginDto>(response);

                // Log the transaction
                await LogTransaction(adminWallet, userWallet, outletLoginDto, request, endpointIp, outletId, request.ExternalRef, userId, response, userSurcharge);

                return outletLoginDto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        public async Task<string> PerformOutletLoginAsync(OutletLoginCommand command, string endpointIp, string outletId, string externalRef, long adminId, string latitude, string longitude)
        {
            try
            {
                var url = baseUrl + InstantPayEndpoints.AEPS.OutletLogin;

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

                // Headers
                await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

                //encode aadhaar
                var aadhar = await RecryptedAadhar(command.BiometricData.EncryptedAadhaar, adminId);

                // JSON body
                var requestBody = new
                {
                    type = command.Type,
                    serviceType = command.ServiceType,
                    latitude = latitude,
                    longitude = longitude,
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
                var responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
            catch
            {
                // Optionally log exception details here
                throw;
            }
        }

        public async Task LogTransaction(Wallet adminWallet, Wallet userWallet, OutletLoginDto dto, OutletLoginCommand request, string endpointIp, string outletId, string externalRef, string userId, string response, decimal userSurcharge)
        {
            try
            {
                // Log the transaction
                var transactionLog = new TransactionLog()
                {
                    UserId = long.Parse(userId),
                    LogType = (byte)LogType.OutletLogin_IPay,
                    EndPointIP = endpointIp,
                    Ipay_OrderId = dto.OrderId,
                    AuditType = (byte)AuditType.Debit,
                    CaptureType = request.CaptureType,
                    Amount = decimal.Parse(dto.Data?.Pool?.PayableValue ?? "0"),
                    Ipay_Uuid = dto.IpayUuid,
                    Ipay_Environment = dto.Environment,
                    Ipay_ActCode = dto.ActCode,
                    Ipay_StatusCode = dto.StatusCode,
                    Ipay_Timestamp = DateTime.Parse(dto.Timestamp),
                    CreatedBy = long.Parse(userId),
                    Ipay_OutletId = outletId,
                    ReferenceId = externalRef,
                    Ipay_Response = response,
                    Ipay_Latitude = request.Latitude,
                    Ipay_Longitude = request.Longitude,
                };

                var statusCode = dto?.StatusCode?.Trim();

                if (string.Equals(statusCode, "TXN", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Success;
                    transactionLog.Remark = dto.Status ?? "Outlet Login Successful";
                    transactionLog.Amount = decimal.Parse(dto.Data?.Pool?.PayableValue ?? "0");
                }
                else if (string.Equals(statusCode, "TUP", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Pending;
                    transactionLog.Remark = dto.Status ?? "Outlet Login Pending";
                    transactionLog.Amount = 0;
                }
                else
                {
                    transactionLog.Status = (byte)Status.Failed;
                    transactionLog.Remark = dto.Status ?? "Outlet Login Failed";
                    transactionLog.Amount = 0;
                }

                var logId = await _transactionLogRepository.AddAsync(transactionLog);

                if (string.Equals(statusCode, "TXN", StringComparison.OrdinalIgnoreCase) && dto.Data?.Pool != null)
                    await LogTransactionForWallet(adminWallet, userWallet, dto, logId, userSurcharge);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task LogTransactionForWallet(Wallet adminWallet, Wallet userWallet, OutletLoginDto dto, long logId, decimal userSurcharge)
        {
            try
            {
                decimal adminSurcharge = decimal.Parse(dto.Data.Pool.PayableValue);

                // Admin wallet credited
                adminWallet.TotalBalance -= adminSurcharge;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    Amount = adminSurcharge,
                    RemainingAmount = adminWallet.TotalBalance,
                    RefUserId = userWallet.UserId,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.Outlet_Login_Surcharge,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Debit - Outlet Login Surcharge | Amount: {adminSurcharge} | For UserId: {userWallet.UserId} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });

                // User's wallet
                userWallet.TotalBalance -= userSurcharge;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = userWallet.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = userSurcharge,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.Outlet_Login_Surcharge,
                    CreatedBy = userWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"User Wallet Debit - Outlet Login Surcharge | Amount: {userSurcharge} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });
                await _walletRepository.UpdateAsync(userWallet);

                // Admin wallet
                adminWallet.TotalBalance += userSurcharge;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    RefUserId = userWallet.UserId,
                    Amount = userSurcharge,
                    RemainingAmount = adminWallet.TotalBalance,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.Outlet_Login_Surcharge,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Credit - Outlet Login Surcharge Received from UserId: {userWallet.UserId} | Amount: {userSurcharge} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });
                await _walletRepository.UpdateAsync(adminWallet);

                dto.Data.Pool.PayableValue = userSurcharge.ToString();
            }
            catch
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

        private async Task<decimal> GetSurcharge(Wallet userWallet)
        {
            try
            {
                decimal userSurcharge = 0;

                // Get user commission slab
                var userFinancialComponents = await _userFinancialComponentRepository.GetByUserIdAsync(userWallet.UserId);

                if (userFinancialComponents != null && userFinancialComponents.Any())
                {
                    var surchargeSlab = userFinancialComponents
                        .FirstOrDefault(x => x.Type == (byte)FinancialComponentType.Surcharge &&
                        x.ServiceType == (byte)FinancialComponenService.Other_Surcharge &&
                        x.ServiceSubType == (byte)FinancialComponenSubService.Outle_Login);

                    if (surchargeSlab != null)
                    {
                        userSurcharge = surchargeSlab.Value;
                    }
                }
                else
                {
                    var financialComponents = await _financialComponentMasterRepository.GetAllAsync();

                    var surchargeSlab = financialComponents
                        .FirstOrDefault(x => x.Type == (byte)FinancialComponentType.Surcharge &&
                        x.ServiceType == (byte)FinancialComponenService.Other_Surcharge &&
                        x.ServiceSubType == (byte)FinancialComponenSubService.Outle_Login);

                    if (surchargeSlab != null)
                    {
                        userSurcharge = surchargeSlab.Value;
                    }
                }

                return userSurcharge;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
