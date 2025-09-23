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
using System.Buffers.Text;
using System.Text;

namespace FinTech_ApiPanel.Application.Handlers.InstantPay
{
    public class RemitterTransactionHandler : IRequestHandler<RemitterTransactionCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        public readonly IWalletRepository _walletRepository;
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHeaderService _headerService;
        private readonly string baseUrl;
        private readonly ITokenService _tokenService;

        public RemitterTransactionHandler(HttpClient httpClient,
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

        public async Task<object> Handle(RemitterTransactionCommand request, CancellationToken cancellationToken)
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

                var userSurcharge = await GetSurcharge(userWallet, decimal.Parse(request.TransferAmount));
                if ((userWallet.TotalBalance - userWallet.HeldAmount) < (decimal.Parse(request.TransferAmount) + userSurcharge))
                    throw new Exception("Insufficient balance in user wallet.");

                var endpointIp = _httpContextAccessor.HttpContext?.Items["Endpoint-Ip"]?.ToString();
                var outletId = _httpContextAccessor.HttpContext?.Items["Outlet-Id"]?.ToString();

                if (string.IsNullOrEmpty(outletId))
                    throw new Exception("Outlet ID not found in the context.");

                var ip = _tokenService.HostIpAddress();

                if (ip == null || string.IsNullOrEmpty(ip))
                    throw new Exception("Error fatching IP.");

                var response = await PerformTransactionAsync(request, ip, outletId);

                var resultDto = JsonConvert.DeserializeObject<RemitterTransactionDto>(response);

                await LogTransaction(adminWallet, userWallet, resultDto, request, endpointIp, outletId, userId, response, userSurcharge);

                return resultDto;
            }
            catch (Exception ex)
            {
                return new { status = ex.Message };
            }
        }

        private async Task<string> PerformTransactionAsync(RemitterTransactionCommand command, string endpointIp, string outletId)
        {
            string url = baseUrl + InstantPayEndpoints.Remittance.DomesticV2.Transaction;

            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

            // Headers
            await _headerService.AddHeaders(requestMessage, endpointIp, outletId);

            var json = JsonConvert.SerializeObject(command);
            requestMessage.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        private async Task LogTransaction(Wallet adminWallet, Wallet userWallet, RemitterTransactionDto dto, RemitterTransactionCommand request, string endpointIp, string outletId, string userId, string response, decimal userSurcharge)
        {
            try
            {
                var transactions = await _transactionLogRepository.GetByUserIdAsync(long.Parse(userId));

                var lastTransactions = transactions
                    .FirstOrDefault(t => t.LogType == (byte)LogType.DMTTransactionOtp_IPay && t.ReferenceId == request.ReferenceKey);

                var log = new TransactionLog
                {
                    UserId = long.Parse(userId),
                    LogType = (byte)LogType.DMTTransaction_IPay,
                    EndPointIP = endpointIp,
                    Ipay_OrderId = dto.OrderId,
                    Ipay_Uuid = dto.IpayUuid,
                    Ipay_Environment = dto.Environment,
                    Ipay_StatusCode = dto.StatusCode,
                    Ipay_ActCode = dto.ActCode,
                    Ipay_Timestamp = DateTime.Parse(dto.Timestamp),
                    Ipay_OutletId = outletId,
                    ReferenceId = request.ExternalRef,
                    CreatedBy = long.Parse(userId),
                    Status = dto.StatusCode == "TXN" ? (byte)Status.Success : dto.StatusCode == "TUP" ? (byte)Status.Pending : (byte)Status.Failed,
                    Remark = dto.Status,
                    Amount = dto.StatusCode == "TXN" ? decimal.Parse(dto.Data.TxnValue) : 0,
                    AuditType = (byte)AuditType.Other,
                    Ipay_Response = response,
                    MobileNumber = lastTransactions?.MobileNumber,
                    Ipay_Latitude = request.Latitude,
                    Ipay_Longitude = request.Longitude,
                };

                var id = await _transactionLogRepository.AddAsync(log);

                if (string.Equals(dto.StatusCode, "TXN", StringComparison.OrdinalIgnoreCase) || string.Equals(dto.StatusCode, "TUP", StringComparison.OrdinalIgnoreCase))
                    await LogTransactionForWallet(adminWallet, userWallet, dto, id, userSurcharge, dto.StatusCode);
            }
            catch (Exception)
            {
            }
        }

        private async Task LogTransactionForWallet(Wallet adminWallet, Wallet userWallet, RemitterTransactionDto depositDto, long logId, decimal userSurcharge, string statusCode)
        {
            try
            {
                var payableValue = decimal.Parse(depositDto.Data.Pool.Amount);
                var transactionValue = decimal.Parse(depositDto.Data.TxnValue);
                decimal adminSurcharge = payableValue - transactionValue;

                if (statusCode == "TXN")
                {
                    // Admin wallet
                    adminWallet.TotalBalance -= adminSurcharge;
                    await _transactionLogRepository.AddAsync(new TransactionLog
                    {
                        UserId = adminWallet.UserId,
                        Amount = adminSurcharge,
                        RemainingAmount = adminWallet.TotalBalance,
                        RefUserId = userWallet.UserId,
                        AuditType = (byte)AuditType.Debit,
                        LogType = (byte)LogType.DMTSurcharge,
                        CreatedBy = adminWallet.UserId,
                        Status = (byte)Status.Success,
                        Remark = $"Admin Wallet Debit - DMT Surcharge | Amount: {adminSurcharge} | For UserId: {userWallet.UserId} | Ref Txn: {logId}",
                        WalletUpdated = true,
                        ReferenceId = logId.ToString()
                    });
                }

                // User's wallet
                userWallet.TotalBalance -= transactionValue;
                await _transactionLogRepository.AddAsync(new TransactionLog
                {
                    UserId = userWallet.UserId,
                    RefUserId = adminWallet.UserId,
                    Amount = transactionValue,
                    RemainingAmount = userWallet.TotalBalance,
                    AuditType = (byte)AuditType.Debit,
                    LogType = (byte)LogType.DMTTransaction,
                    CreatedBy = userWallet.UserId,
                    Status = statusCode == "TXN" ? (byte)Status.Success : (byte)Status.Pending,
                    Remark = $"User Wallet Debit - DMT Transaction Amount | Amount: {transactionValue} | Ref Txn: {logId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });

                if (statusCode == "TXN")
                {
                    userWallet.TotalBalance -= userSurcharge;
                    await _transactionLogRepository.AddAsync(new TransactionLog
                    {
                        UserId = userWallet.UserId,
                        RefUserId = adminWallet.UserId,
                        Amount = userSurcharge,
                        RemainingAmount = userWallet.TotalBalance,
                        AuditType = (byte)AuditType.Debit,
                        LogType = (byte)LogType.DMTSurcharge,
                        CreatedBy = userWallet.UserId,
                        Status = (byte)Status.Success,
                        Remark = $"User Wallet Debit - DMT Surcharge | Amount: {userSurcharge} | Ref Txn: {logId}",
                        WalletUpdated = true,
                        ReferenceId = logId.ToString()
                    });
                }

                await _walletRepository.UpdateAsync(userWallet);

                if (statusCode == "TXN")
                {
                    // Admin wallet user commission deduction
                    adminWallet.TotalBalance += userSurcharge;
                    await _transactionLogRepository.AddAsync(new TransactionLog
                    {
                        UserId = adminWallet.UserId,
                        RefUserId = userWallet.UserId,
                        Amount = userSurcharge,
                        RemainingAmount = adminWallet.TotalBalance,
                        AuditType = (byte)AuditType.Credit,
                        LogType = (byte)LogType.DMTSurcharge,
                        CreatedBy = adminWallet.UserId,
                        Status = (byte)Status.Success,
                        Remark = $"Admin Wallet Credit - DMT Surcharge Received from UserId: {userWallet.UserId} | Amount: {userSurcharge} | Ref Txn: {logId}",
                        WalletUpdated = true,
                        ReferenceId = logId.ToString()
                    });

                    await _walletRepository.UpdateAsync(adminWallet);
                }

                depositDto.Data.Pool.Amount = (transactionValue + userSurcharge).ToString("F2");
            }
            catch
            {
                throw;
            }
        }

        private async Task<decimal> GetSurcharge(Wallet userWallet, decimal transactionValue)
        {
            try
            {
                decimal userSurcharge = 0;

                // Get user commission slab
                var userFinancialComponents = await _userFinancialComponentRepository.GetByUserIdAsync(userWallet.UserId);
                if (userFinancialComponents != null && userFinancialComponents.Any())
                {
                    var surchargeSlabs = userFinancialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Surcharge &&
                                    x.ServiceType == (byte)FinancialComponenService.DMT);

                    var matchingSlab = surchargeSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userSurcharge = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userSurcharge = matchingSlab.Value;
                    }
                }
                else
                {
                    var financialComponents = await _financialComponentMasterRepository.GetAllAsync();

                    var surchargeSlabs = financialComponents
                        .Where(x => x.Type == (byte)FinancialComponentType.Surcharge &&
                                    x.ServiceType == (byte)FinancialComponenService.DMT);

                    var matchingSlab = surchargeSlabs.FirstOrDefault(x =>
                        x.Start_Value <= transactionValue && x.End_Value >= transactionValue);

                    if (matchingSlab != null)
                    {
                        if (matchingSlab.CalculationType == (byte)CalculationType.Percentage)
                            userSurcharge = Math.Round(transactionValue * matchingSlab.Value / 100, 2);
                        else if (matchingSlab.CalculationType == (byte)CalculationType.Flat)
                            userSurcharge = matchingSlab.Value;
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
