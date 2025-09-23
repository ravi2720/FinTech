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
using System;
using System.Text;

namespace FinTech_ApiPanel.Application.Handlers.InstantPay
{
    public class AEPSCashDepositHandler : IRequestHandler<AEPSCashDepositCommand, object>
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

        public AEPSCashDepositHandler(
            HttpClient httpClient,
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

        public async Task<object> Handle(AEPSCashDepositCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                if (string.IsNullOrEmpty(userId))
                {
                    throw new Exception("User ID not found in the context.");
                }

                var adminUser = await _userRepository.GetAdminAsync();
                // check wallet
                var adminWallet = await _walletRepository.GetByUserIdAsync(adminUser.Id);
                var userWallet = await _walletRepository.GetByUserIdAsync(long.Parse(userId));
                if (userWallet == null || adminWallet == null)
                    throw new Exception("Wallets not found.");

                if ((userWallet.TotalBalance - userWallet.HeldAmount) < decimal.Parse(request.Amount))
                    throw new Exception("Insufficient wallet balance to process this request.");

                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();

                if (string.IsNullOrEmpty(outletId))
                {
                    throw new Exception("Outlet ID not found in the context.");
                }

                if (string.IsNullOrEmpty(request.ExternalRef))
                    throw new Exception("External reference is required for cash deposit.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await PerformCashDepositAsync(request, ip, outletId, adminUser.Id, request.ExternalRef, request.Latitude, request.Longitude);

                var cashDepositDto = JsonConvert.DeserializeObject<CashDepositDto>(response);

                await LogTransaction(adminWallet, userWallet, cashDepositDto, request, endpointIp, outletId, request.ExternalRef, response);

                return cashDepositDto;
            }
            catch (Exception ex)
            {
                // You might want to log the exception here as well
                return new { status = ex.Message };
            }
        }

        private async Task<string> PerformCashDepositAsync(AEPSCashDepositCommand command, string endpointIp, string outletId, long adminId, string externalRef, string latitude, string longitude)
        {
            try
            {
                var url = baseUrl + InstantPayEndpoints.AEPS.CashDeposit;

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

        private async Task LogTransaction(Wallet adminWallet, Wallet userWallet, CashDepositDto depositDto, AEPSCashDepositCommand request, string endpointIp, string outletId, string externalRef, string response)
        {
            try
            {
                var transactionLog = new TransactionLog()
                {
                    UserId = userWallet.UserId,
                    AuditType = (byte)AuditType.Other,
                    LogType = (byte)LogType.AEPSDeposit_IPay,
                    EndPointIP = endpointIp,
                    Ipay_OrderId = depositDto.OrderId,
                    Ipay_Uuid = depositDto.IpayUuid,
                    Ipay_Environment = depositDto.Environment,
                    Ipay_ActCode = depositDto.ActCode,
                    Ipay_StatusCode = depositDto.StatusCode,
                    Ipay_Timestamp = DateTime.Parse(depositDto.Timestamp),
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
                    transactionLog.Remark = depositDto.Status ?? "Cash Deposit Successful";
                    transactionLog.Amount = decimal.Parse(depositDto.Data.TransactionValue);
                }
                else if (string.Equals(transactionLog.Ipay_StatusCode, "TUP", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Pending;
                    transactionLog.Remark = depositDto.Status ?? "Cash Deposit Pending";
                    transactionLog.Amount = decimal.Parse(depositDto.Data?.TransactionValue ?? request.Amount);
                }
                else
                {
                    transactionLog.Status = (byte)Status.Failed;
                    transactionLog.Remark = depositDto.Status ?? "Cash Deposit Failed";
                    transactionLog.Amount = 0;
                }

                var id = await _transactionLogRepository.AddAsync(transactionLog);

                if (string.Equals(transactionLog.Ipay_StatusCode, "TXN", StringComparison.OrdinalIgnoreCase) || string.Equals(transactionLog.Ipay_StatusCode, "TUP", StringComparison.OrdinalIgnoreCase))
                    await LogTransactionForWallet(adminWallet, userWallet, depositDto, id, transactionLog.Ipay_StatusCode);
            }
            catch
            {
                throw;
            }
        }

        private async Task LogTransactionForWallet(Wallet adminWallet, Wallet userWallet, CashDepositDto depositDto, long logId, string statusCode)
        {
            try
            {
                var transactionValue = decimal.Parse(depositDto.Data.TransactionValue);
                var payableValue = decimal.Parse(depositDto.Data.PayableValue);
                decimal adminCommission = transactionValue - payableValue;

                var userCommission = await GetComission(userWallet, transactionValue);

                if (statusCode == "TXN")
                {
                    // Admin wallet commission credit
                    // Admin commission credit
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
                        Status = statusCode == "TXN"
                                   ? (byte)Status.Success
                                   : (byte)Status.Pending,
                        WalletUpdated = true,
                        Remark = $"Admin Wallet Credit - AEPS Deposit Commission | Amount: {adminCommission} | For UserId: {userWallet.UserId} | Ref Txn: {logId}",
                        ReferenceId = logId.ToString()
                    });

                    // Admin commission debit (payout to user)
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
                        Status = statusCode == "TXN"
                                   ? (byte)Status.Success
                                   : (byte)Status.Pending,
                        WalletUpdated = true,
                        Remark = $"Admin Wallet Debit - AEPS Deposit User Commission Payout | Amount: {userCommission} | To UserId: {userWallet.UserId} | Ref Txn: {logId}",
                        ReferenceId = logId.ToString()
                    });
                    await _walletRepository.UpdateAsync(adminWallet);
                }

                // User wallet debit for deposit
                userWallet.TotalBalance -= transactionValue;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = userWallet.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = transactionValue,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.AEPSDeposit,
                    CreatedBy = userWallet.UserId,
                    Status = statusCode == "TXN"
                               ? (byte)Status.Success
                               : (byte)Status.Pending,
                    WalletUpdated = true,
                    Remark = $"User Wallet Debit - AEPS Deposit Transaction | Amount: {transactionValue} | Ref Txn: {logId}",
                    ReferenceId = logId.ToString()
                });

                if (statusCode == "TXN")
                {
                    // User commission credit
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
                        Status = statusCode == "TXN"
                                   ? (byte)Status.Success
                                   : (byte)Status.Pending,
                        WalletUpdated = true,
                        Remark = $"User Wallet Credit - AEPS Deposit Commission | Amount: {userCommission} | Ref Txn: {logId}",
                        ReferenceId = logId.ToString()
                    });
                }

                await _walletRepository.UpdateAsync(userWallet);

                depositDto.Data.PayableValue = (transactionValue - userCommission).ToString("F2");
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