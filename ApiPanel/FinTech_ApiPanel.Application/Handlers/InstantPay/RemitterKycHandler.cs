using FinTech_ApiPanel.Application.Abstraction.IHeaders;
using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Application.Commands.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.InstantPay;
using FinTech_ApiPanel.Domain.DTOs.URLs;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Entities.UserWallets;
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
    internal class RemitterKycHandler : IRequestHandler<RemitterKycCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHeaderService _headerService;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public RemitterKycHandler(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ITransactionLogRepository transactionLogRepository,
            IWalletRepository walletRepository,
            IFinancialComponentMasterRepository financialComponentMasterRepository,
            IUserFinancialComponentRepository userFinancialComponentRepository,
            IUserRepository userRepository,
            IHeaderService headerService,
            IOptions<ApiSetting> baseUrl,
            ITokenService tokenService)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _transactionLogRepository = transactionLogRepository;
            _walletRepository = walletRepository;
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _userFinancialComponentRepository = userFinancialComponentRepository;
            _userRepository = userRepository;
            _headerService = headerService;
            this.baseUrl = baseUrl.Value.BaseUrl;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(RemitterKycCommand request, CancellationToken cancellationToken)
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

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await PostBiometricKyc(request, ip, outletId, request.Latitude, request.Longitude);
                var dto = JsonConvert.DeserializeObject<RemitterKycDto>(response);

                await LogTransaction(adminWallet, userWallet, dto, request, endpointIp, outletId, userId, response, userSurcharge);

                return dto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> PostBiometricKyc(RemitterKycCommand command, string endpointIp, string outletId, string latitude, string longitude)
        {
            try
            {
                var url = baseUrl + InstantPayEndpoints.Remittance.DomesticV2.RemitterKyc;

                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

                // Headers
                await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

                // Build request body to match what InstantPay expects
                var requestBody = new
                {
                    mobileNumber = command.MobileNumber,
                    referenceKey = command.ReferenceKey,
                    latitude = latitude,
                    longitude = longitude,
                    externalRef = command.ExternalRef,
                    captureType = command.CaptureType,
                    consentTaken = command.ConsentTaken,
                    biometricData = new
                    {
                        pidData = command.BiometricData.PidData,
                        ts = command.BiometricData.Ts,
                        ci = command.BiometricData.Ci,
                        hmac = command.BiometricData.Hmac,
                        dc = command.BiometricData.Dc,
                        mi = command.BiometricData.Mi,
                        dpId = command.BiometricData.DpId,
                        mc = command.BiometricData.Mc,
                        rdsId = command.BiometricData.RdsId,
                        rdsVer = command.BiometricData.RdsVer,
                        Skey = command.BiometricData.Skey,
                        srno = command.BiometricData.SrNo,
                        pidOptionWadh = command.BiometricData.PidOptionWadh
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
                throw;
            }
        }

        private async Task LogTransaction(Wallet adminWallet, Wallet userWallet, RemitterKycDto dto, RemitterKycCommand request, string endpointIp, string outletId, string userId, string response, decimal userSurcharge)
        {
            var transactionLog = new TransactionLog
            {
                UserId = long.Parse(userId),
                LogType = (byte)LogType.RemitterKyc_IPay,
                EndPointIP = endpointIp,
                Ipay_OrderId = dto.OrderId,
                AuditType = (byte)AuditType.Other,
                Ipay_Uuid = dto.IpayUuid,
                Amount = dto.Data != null ? decimal.Parse(dto.Data.Pool.Amount) : 0,
                Ipay_Environment = dto.Environment,
                Ipay_ActCode = dto.ActCode,
                Ipay_StatusCode = dto.StatusCode,
                Ipay_Timestamp = DateTime.Parse(dto.Timestamp),
                CreatedBy = long.Parse(userId),
                Ipay_OutletId = outletId,
                ReferenceId = request.ExternalRef,
                CaptureType = request.CaptureType,
                Ipay_Response = response,
                MobileNumber = request.MobileNumber,
                Ipay_Latitude = request.Latitude,
                Ipay_Longitude = request.Longitude,

            };

            var statusCode = dto?.StatusCode?.Trim();
            if (string.Equals(statusCode, "TXN", StringComparison.OrdinalIgnoreCase))
            {
                transactionLog.Status = (byte)Status.Success;
                transactionLog.Remark = dto.Status ?? "Biometric KYC completed.";
                transactionLog.Amount = decimal.Parse(dto.Data.Pool.Amount);
            }
            else
            {
                transactionLog.Status = (byte)Status.Failed;
                transactionLog.Remark = dto.Status ?? "Biometric KYC failed.";
            }

            var logId = await _transactionLogRepository.AddAsync(transactionLog);

            if (string.Equals(statusCode, "TXN", StringComparison.OrdinalIgnoreCase))
                await LogTransactionForWallet(adminWallet, userWallet, dto, logId, userSurcharge);
        }

        public async Task LogTransactionForWallet(Wallet adminWallet, Wallet userWallet, RemitterKycDto dto, long logId, decimal userSurcharge)
        {
            try
            {
                decimal adminSurcharge = decimal.Parse(dto.Data.Pool.Amount);

                // Admin wallet credited
                adminWallet.TotalBalance -= adminSurcharge;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    Amount = adminSurcharge,
                    RemainingAmount = adminWallet.TotalBalance,
                    RefUserId = userWallet.UserId,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.Remitter_KYC_Surcharge,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Debit - Remitter KYC Surcharge | Amount: {adminSurcharge} | For UserId: {userWallet.UserId} | Ref Txn: {logId}",
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
                    LogType = (byte)LogType.Remitter_KYC_Surcharge,
                    CreatedBy = userWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"User Wallet Debit - Remitter KYC Surcharge | Amount: {userSurcharge} | Ref Txn: {logId}",
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
                    LogType = (byte)LogType.Remitter_KYC_Surcharge,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Credit - Remitter KYC Surcharge Received from UserId: {userWallet.UserId} | Amount: {userSurcharge} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });
                await _walletRepository.UpdateAsync(adminWallet);

                dto.Data.Pool.Amount = userSurcharge.ToString("F2");
            }
            catch
            {
                throw;
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
                        x.ServiceSubType == (byte)FinancialComponenSubService.Remitter_KYC);

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
                        x.ServiceSubType == (byte)FinancialComponenSubService.Remitter_KYC);

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
