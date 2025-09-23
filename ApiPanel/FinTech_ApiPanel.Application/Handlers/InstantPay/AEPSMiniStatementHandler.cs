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
    public class AEPSMiniStatementHandler : IRequestHandler<AEPSMiniStatementCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICryptoService _cryptoService;
        private readonly ITransactionLogRepository _transactionLogRepository;
        public readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IHeaderService _headerService;
        private readonly IClientRepository _clientRepository;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public AEPSMiniStatementHandler(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ICryptoService cryptoService,
            ITransactionLogRepository transactionLogRepository,
            IWalletRepository walletRepository,
            IUserRepository userRepository,
            IFinancialComponentMasterRepository financialComponentMasterRepository,
            IUserFinancialComponentRepository userFinancialComponentRepository,
            IHeaderService headerService,
            IClientRepository clientRepository,
            IOptions<ApiSetting> baseUrl,
            ITokenService tokenService)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _cryptoService = cryptoService;
            _transactionLogRepository = transactionLogRepository;
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _userFinancialComponentRepository = userFinancialComponentRepository;
            _headerService = headerService;
            _clientRepository = clientRepository;
            this.baseUrl = baseUrl.Value.BaseUrl;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(AEPSMiniStatementCommand request, CancellationToken cancellationToken)
        {
            try
            {
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

                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();

                if (string.IsNullOrEmpty(outletId))
                    throw new Exception("Outlet ID not found in the context.");

                if (string.IsNullOrEmpty(request.ExternalRef))
                    throw new Exception("External reference is required for mini statement.");

                var userCommission = await GetComission(userWallet);

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await PerformMiniStatementAsync(request, ip, outletId, request.ExternalRef, adminUser.Id, request.Latitude, request.Longitude);

                var miniStatementDto = JsonConvert.DeserializeObject<MiniStatementDto>(response);

                // Log the transaction
                await LogTransaction(adminWallet, userWallet, miniStatementDto, request, endpointIp, outletId, request.ExternalRef, response, userCommission);

                return miniStatementDto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> PerformMiniStatementAsync(AEPSMiniStatementCommand command, string endpointIp, string outletId, string externalRef, long adminId, string latitude, string longitude)
        {
            try
            {
                var url = baseUrl + InstantPayEndpoints.AEPS.MiniStatement;

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

                // Headers
                await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

                //encode aadhaar
                var aadhar = await RecryptedAadhar(command.BiometricData.EncryptedAadhaar, adminId);

                // Prepare request body
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
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task LogTransaction(Wallet adminWallet, Wallet userWallet, MiniStatementDto dto, AEPSMiniStatementCommand request, string endpointIp, string outletId, string externalRef, string? response, decimal userCommission)
        {
            try
            {
                //check  user
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User ID not found in the context.");
                }

                // Log the transaction
                var transactionLog = new TransactionLog()
                {
                    UserId = long.Parse(userId),
                    LogType = (byte)LogType.AEPSMiniStatement_IPay,
                    Amount = decimal.Parse(dto.Data?.TransactionValue ?? "0"),
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
                    transactionLog.Remark = dto.Status ?? "Mini Statement Success";
                    transactionLog.Amount = userCommission;
                }
                else if (string.Equals(statusCode, "TUP", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Pending;
                    transactionLog.Remark = dto.Status ?? "Mini Statement Pending";
                    transactionLog.Amount = userCommission;
                }
                else
                {
                    transactionLog.Status = (byte)Status.Failed;
                    transactionLog.Remark = dto.Status ?? "Mini Statement Failed";
                    transactionLog.Amount = 0;
                }

                var logId = await _transactionLogRepository.AddAsync(transactionLog);

                if (string.Equals(statusCode, "TXN", StringComparison.OrdinalIgnoreCase))
                    await LogTransactionForWallet(adminWallet, userWallet, dto, logId, userCommission);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task LogTransactionForWallet(Wallet adminWallet, Wallet userWallet, MiniStatementDto dto, long logId, decimal userCommission)
        {
            try
            {
                var adminCommission = decimal.Parse(dto.Data.PayableValue);

                // Admin wallet credited
                adminWallet.TotalBalance += adminCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    Amount = adminCommission,
                    RemainingAmount = adminWallet.TotalBalance,
                    RefUserId = userWallet.UserId,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.AEPS_MiniStatement_Commission,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Credit - AEPS Mini Statement Commission | Amount: {adminCommission} | From Transaction of UserId: {userWallet.UserId} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });

                adminWallet.TotalBalance -= userCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    RefUserId = userWallet.UserId,
                    Amount = userCommission,
                    RemainingAmount = adminWallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.AEPS_MiniStatement_Commission,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Debit - AEPS Mini Statement Commission Payout to UserId: {userWallet.UserId} | Amount: {userCommission} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });
                await _walletRepository.UpdateAsync(adminWallet);

                // User's wallet
                userWallet.TotalBalance += userCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = userWallet.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = userCommission,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.AEPS_MiniStatement_Commission,
                    CreatedBy = userWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"User Wallet Credit - AEPS Mini Statement Commission | Amount: {userCommission} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });
                await _walletRepository.UpdateAsync(userWallet);

                dto.Data.PayableValue = userCommission.ToString("F2");
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

        private async Task<decimal> GetComission(Wallet userWallet)
        {
            try
            {
                decimal userCommission = 0;

                // Get user commission slab
                var userFinancialComponents = await _userFinancialComponentRepository.GetByUserIdAsync(userWallet.UserId);
                if (userFinancialComponents != null && userFinancialComponents.Any())
                {
                    var commissionSlab = userFinancialComponents
                        .FirstOrDefault(x => x.Type == (byte)FinancialComponentType.Commission &&
                        x.ServiceType == (byte)FinancialComponenService.Other_Commission &&
                        x.ServiceSubType == (byte)FinancialComponenSubService.Mini_Statement);

                    if (commissionSlab != null)
                    {
                        userCommission = commissionSlab.Value;
                    }
                }
                else
                {
                    var financialComponents = await _financialComponentMasterRepository.GetAllAsync();

                    var commissionSlab = financialComponents
                        .FirstOrDefault(x => x.Type == (byte)FinancialComponentType.Commission &&
                        x.ServiceType == (byte)FinancialComponenService.Other_Commission &&
                        x.ServiceSubType == (byte)FinancialComponenSubService.Mini_Statement);

                    if (commissionSlab != null)
                    {
                        userCommission = commissionSlab.Value;
                    }
                }

                return userCommission;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
