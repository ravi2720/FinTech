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
    public class AEPSCashWithdrawalHandler : IRequestHandler<AEPSCashWithdrawalCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly ICryptoService _cryptoService;
        public readonly IWalletRepository _walletRepository;
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHeaderService _headerService;
        private readonly IClientRepository _clientRepository;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public AEPSCashWithdrawalHandler(HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ITransactionLogRepository transactionLogRepository,
            ICryptoService cryptoService,
            IWalletRepository walletRepository,
            IFinancialComponentMasterRepository financialComponentMasterRepository,
            IUserFinancialComponentRepository userFinancialComponentRepository,
            IUserRepository userRepository,
            IHeaderService headerService,
            IClientRepository clientRepository,
            IOptions<ApiSetting> baseUrl,
            ITokenService tokenService)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _transactionLogRepository = transactionLogRepository;
            _cryptoService = cryptoService;
            _walletRepository = walletRepository;
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _userFinancialComponentRepository = userFinancialComponentRepository;
            _userRepository = userRepository;
            _headerService = headerService;
            _clientRepository = clientRepository;
            this.baseUrl = baseUrl.Value.BaseUrl;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(AEPSCashWithdrawalCommand request, CancellationToken cancellationToken)
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

                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();

                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();
                if (string.IsNullOrEmpty(outletId))
                {
                    throw new Exception("Outlet ID not found in the context.");
                }

                if (string.IsNullOrEmpty(request.ExternalRef))
                    throw new Exception("External reference is required for cash withdrawal.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await PerformCashWithdrawalAsync(request, ip, outletId, request.ExternalRef, adminUser.Id, request.Latitude, request.Longitude);

                // Deserialize the response into EKYCInitiateDto
                var cashWithdrawalDto = JsonConvert.DeserializeObject<CashWithdrawalDto>(response);

                // Log the transaction
                await LogWithdrawalTransaction(adminWallet, userWallet, cashWithdrawalDto, request, endpointIp, outletId, request.ExternalRef, response);

                return cashWithdrawalDto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> PerformCashWithdrawalAsync(AEPSCashWithdrawalCommand command, string endpointIp, string outletId, string externalRef, long adminId, string latitude, string longitude)
        {
            try
            {
                var url = baseUrl + InstantPayEndpoints.AEPS.CashWithdrawal;

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

                // Headers
                await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

                //encode aadhaar
                var aadhar = await RecryptedAadhar(command.BiometricData.EncryptedAadhaar, adminId);

                // Request Body
                var requestBody = new
                {
                    bankiin = command.BankIIN,
                    latitude = latitude,
                    longitude = longitude,
                    mobile = command.Mobile,
                    amount = command.Amount,
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

        private async Task LogWithdrawalTransaction(Wallet adminWallet, Wallet userWallet, CashWithdrawalDto dto, AEPSCashWithdrawalCommand request, string endpointIp, string outletId, string externalRef, string response)
        {
            try
            {
                var transactionLog = new TransactionLog()
                {
                    UserId = userWallet.UserId,
                    AuditType = (byte)AuditType.Other,
                    LogType = (byte)LogType.AEPSWithdrawal_IPay,
                    EndPointIP = endpointIp,
                    Ipay_OrderId = dto.OrderId,
                    Ipay_Uuid = dto.IpayUuid,
                    Ipay_Environment = dto.Environment,
                    Ipay_ActCode = dto.ActCode,
                    Ipay_StatusCode = dto.StatusCode,
                    Ipay_Timestamp = DateTime.Parse(dto.Timestamp),
                    CreatedBy = userWallet.UserId,
                    Ipay_OutletId = outletId,
                    ReferenceId = externalRef,
                    Ipay_Response = response,
                    MobileNumber = request.Mobile,
                    Ipay_Latitude = request.Latitude,
                    Ipay_Longitude = request.Longitude,
                };

                if (string.Equals(transactionLog.Ipay_StatusCode, "TXN", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Success;
                    transactionLog.Remark = dto.Status ?? "Cash Withdrawal Successful";
                    transactionLog.Amount = decimal.Parse(dto.Data.TransactionValue);
                }
                else if (string.Equals(transactionLog.Ipay_StatusCode, "TUP", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Pending;
                    transactionLog.Remark = dto.Status ?? "Cash Withdrawal Pending";
                    transactionLog.Amount = decimal.Parse(dto.Data?.PayableValue ?? request.Amount);
                }
                else
                {
                    transactionLog.Status = (byte)Status.Failed;
                    transactionLog.Remark = dto.Status ?? "Cash Withdrawal Failed";
                    transactionLog.Amount = 0;
                }

                var logId = await _transactionLogRepository.AddAsync(transactionLog);

                if (string.Equals(transactionLog.Ipay_StatusCode, "TXN", StringComparison.OrdinalIgnoreCase))
                    await LogTransactionForWallet(adminWallet, userWallet, dto, logId);
            }
            catch
            {
                throw;
            }
        }

        private async Task LogTransactionForWallet(Wallet adminWallet, Wallet userWallet, CashWithdrawalDto dto, long logId)
        {
            try
            {
                var transactionValue = decimal.Parse(dto.Data.TransactionValue);
                var payableValue = decimal.Parse(dto.Data.PayableValue);
                decimal adminCommission = payableValue - transactionValue;

                decimal userCommission = await GetComission(userWallet, transactionValue);

                // Admin commission
                adminWallet.TotalBalance += adminCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    Amount = adminCommission,
                    RemainingAmount = adminWallet.TotalBalance,
                    RefUserId = userWallet.UserId,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.AEPSCommission,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Commission Credit for AEPS Withdrawal | Amount: {adminCommission} | Ref User: {userWallet.UserId} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });

                // Admin user commission debit
                adminWallet.TotalBalance -= userCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    RefUserId = userWallet.UserId,
                    Amount = userCommission,
                    RemainingAmount = adminWallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.AEPSCommission,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Debit for User AEPS Commission Payout | Amount: {userCommission} | Ref User: {userWallet.UserId} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });
                await _walletRepository.UpdateAsync(adminWallet);

                // User's wallet
                userWallet.TotalBalance += transactionValue;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = userWallet.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = transactionValue,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.AEPSWithdrawal,
                    CreatedBy = userWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"User Wallet Credit for AEPS Cash Withdrawal | Amount: {transactionValue} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });

                userWallet.TotalBalance += userCommission;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = userWallet.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = userCommission,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Credit,
                    LogType = (byte)LogType.AEPSCommission,
                    CreatedBy = userWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"User AEPS Commission Credit for Cash Withdrawal | Amount: {userCommission} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });

                await _walletRepository.UpdateAsync(userWallet);

                dto.Data.PayableValue = (transactionValue + userCommission).ToString("F2");
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

        private async Task<decimal> GetComission(Wallet userWallet, decimal transactionValue)
        {
            try
            {
                decimal userCommission = 0;

                // Get user commission slab
                var userFinancialComponents = await _userFinancialComponentRepository.GetByUserIdAsync(userWallet.UserId);
                if (userFinancialComponents != null && userFinancialComponents.Any())
                {
                    var commissionSlabs = userFinancialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Commission &&
                                    x.ServiceType == (byte)FinancialComponenService.AEPS);

                    var matchingSlab = commissionSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userCommission = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userCommission = matchingSlab.Value;
                    }
                }
                else
                {
                    var financialComponents = await _financialComponentMasterRepository.GetAllAsync();

                    var commissionSlabs = financialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Commission &&
                                    x.ServiceType == (byte)FinancialComponenService.AEPS);

                    var matchingSlab = commissionSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userCommission = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userCommission = matchingSlab.Value;
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
