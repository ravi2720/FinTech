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
using FinTech_ApiPanel.Domain.Shared.Common;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.InstantPay;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Text;

namespace FinTech_ApiPanel.Application.Handlers.InstantPay
{
    public class EKYCValidateHandler : IRequestHandler<EKYCValidateCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        public readonly IWalletRepository _walletRepository;
        private readonly IHeaderService _headerService;
        private readonly bool _isMaster;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public EKYCValidateHandler(HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ITransactionLogRepository transactionLogRepository,
            IUserRepository userRepository,
            IFinancialComponentMasterRepository financialComponentMasterRepository,
            IUserFinancialComponentRepository userFinancialComponentRepository,
            IWalletRepository walletRepository,
            IHeaderService headerService,
            IOptions<PanelSettings> panelSettings,
            IOptions<ApiSetting> baseUrl,
            ITokenService tokenService)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _transactionLogRepository = transactionLogRepository;
            _userRepository = userRepository;
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _userFinancialComponentRepository = userFinancialComponentRepository;
            _walletRepository = walletRepository;
            _headerService = headerService;
            _isMaster = panelSettings.Value.IsMasterPanel;
            this.baseUrl = baseUrl.Value.BaseUrl;
            _tokenService = tokenService;
        }

        public async Task<object> Handle(EKYCValidateCommand request, CancellationToken cancellationToken)
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
                if (string.IsNullOrEmpty(endpointIp))
                    throw new Exception("Endpoint IP not found in the context.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await ValidateEKYCAsync(request.OtpReferenceID, request.Otp, request.Hash, ip);

                // Deserialize the response into EKYCInitiateDto
                var ekycDto = JsonConvert.DeserializeObject<EKYCValidateDto>(response);

                // Log the transaction
                await LogTransaction(adminWallet, userWallet, ekycDto, request, endpointIp, userId, response, userSurcharge);

                // Update user outletId if successful
                if (string.Equals(ekycDto.StatusCode?.Trim(), "TXN", StringComparison.OrdinalIgnoreCase) && ekycDto.Data?.OutletId is not null)
                {
                    await UpdateUserOutletIdAsync(ekycDto.Data.OutletId.ToString(), userId);
                }

                return ekycDto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> ValidateEKYCAsync(string otpReferenceId, string otp, string hash, string endpointIp, string? outletId = null)
        {
            var url = baseUrl + InstantPayEndpoints.UserOutlet.SignupValidate;

            if (_isMaster)
                url = baseUrl + "/user" + InstantPayEndpoints.UserOutlet.SignupValidate;

            var body = new
            {
                otpReferenceID = otpReferenceId,
                otp = otp,
                hash = hash
            };

            var json = JsonConvert.SerializeObject(body);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            // Add dynamic headers via shared header service
            await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

            var response = await _httpClient.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task LogTransaction(Wallet adminWallet, Wallet userWallet, EKYCValidateDto ekycDto, EKYCValidateCommand command, string endpointIp, string userId, string response, decimal userSurcharge)
        {
            try
            {
                var transactions = await _transactionLogRepository.GetByUserIdAsync(long.Parse(userId));

                var lastTransactions = transactions
                    .FirstOrDefault(t => t.LogType == (byte)LogType.AEPOnboardInitiate_IPay && t.ReferenceId == command.OtpReferenceID);

                // Log the transaction
                var transactionLog = new TransactionLog()
                {
                    UserId = long.Parse(userId),
                    LogType = (byte)LogType.AEPSOnboardValidate_IPay,
                    EndPointIP = endpointIp,
                    Ipay_OrderId = ekycDto.OrderId,
                    Ipay_Uuid = ekycDto.IpayUuid,
                    AuditType = (byte)AuditType.Other,
                    Ipay_Environment = ekycDto.Environment,
                    Ipay_ActCode = ekycDto.ActCode,
                    Ipay_StatusCode = ekycDto.StatusCode,
                    Ipay_Timestamp = DateTime.Parse(ekycDto.Timestamp),
                    Ipay_Latitude = lastTransactions?.Ipay_Latitude,
                    Ipay_Longitude = lastTransactions?.Ipay_Longitude,
                    ReferenceId = lastTransactions?.Id.ToString(),
                    CreatedBy = long.Parse(userId),
                    Ipay_OutletId = lastTransactions?.Ipay_OutletId,
                    Ipay_Response = response,
                    MobileNumber = lastTransactions?.MobileNumber,
                    Email = lastTransactions?.Email,
                };

                var statusCode = ekycDto?.StatusCode?.Trim();

                if (string.Equals(statusCode, "TXN", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Success;

                    if (ekycDto.Data?.OutletId != null)
                        transactionLog.Ipay_OutletId = ekycDto.Data.OutletId.ToString();

                    transactionLog.Remark = ekycDto.Status ?? "EKYC Validation Successful";

                    if (_isMaster)
                        transactionLog.Amount = 2.18m;
                    else
                        transactionLog.Amount = decimal.Parse(ekycDto.Data.Amount);
                }
                else if (string.Equals(statusCode, "TUP", StringComparison.OrdinalIgnoreCase))
                {
                    transactionLog.Status = (byte)Status.Pending;
                    transactionLog.Remark = ekycDto.Status ?? "EKYC Validation Pending";
                    if (_isMaster)
                        transactionLog.Amount = 2.18m;
                    else
                        transactionLog.Amount = decimal.Parse(ekycDto.Data.Amount);
                }
                else
                {
                    transactionLog.Status = (byte)Status.Failed;
                    transactionLog.Remark = ekycDto.Status ?? $"EKYC Validation Failed";
                    transactionLog.Amount = 0;
                }

                var logId = await _transactionLogRepository.AddAsync(transactionLog);

                if (string.Equals(statusCode, "TXN", StringComparison.OrdinalIgnoreCase))
                    await LogTransactionForWallet(adminWallet, userWallet, ekycDto, logId, userSurcharge);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task LogTransactionForWallet(Wallet adminWallet, Wallet userWallet, EKYCValidateDto dto, long logId, decimal userSurcharge)
        {
            try
            {
                decimal adminSurcharge = 0;

                if (_isMaster)
                    adminSurcharge = 2.18m;
                else
                    adminSurcharge = decimal.Parse(dto.Data.Amount);

                // Admin wallet credited
                adminWallet.TotalBalance -= adminSurcharge;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = adminWallet.UserId,
                    Amount = adminSurcharge,
                    RemainingAmount = adminWallet.TotalBalance,
                    RefUserId = userWallet.UserId,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.AEPSOnboarding_Surcharge,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Debit - Merchant Onboarding Surcharge | Amount: {adminSurcharge} | For UserId: {userWallet.UserId} | Ref Txn: {logId}",
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
                    LogType = (byte)LogType.AEPSOnboarding_Surcharge,
                    CreatedBy = userWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"User Wallet Debit - Merchant Onboarding Surcharge | Amount: {userSurcharge} | Ref Txn: {logId}",
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
                    LogType = (byte)LogType.AEPSOnboarding_Surcharge,
                    CreatedBy = adminWallet.UserId,
                    Status = (byte)Status.Success,
                    Remark = $"Admin Wallet Credit - Merchant Onboarding Surcharge Received from UserId: {userWallet.UserId} | Amount: {userSurcharge} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });
                await _walletRepository.UpdateAsync(adminWallet);

                dto.Data.Amount = userSurcharge.ToString("F2");
            }
            catch
            {
                throw;
            }
        }

        //update user outletId
        public async Task UpdateUserOutletIdAsync(string outletId, string userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(long.Parse(userId));
                if (user == null)
                {
                    throw new Exception("User not found.");
                }
                user.OutletId = outletId;
                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating user outlet ID: {ex.Message}");
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
                        x.ServiceSubType == (byte)FinancialComponenSubService.Merchant_Onboarding);

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
                        x.ServiceSubType == (byte)FinancialComponenSubService.Merchant_Onboarding);

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
