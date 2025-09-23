using FinTech_ApiPanel.Application.Commands.GoterPay;
using FinTech_ApiPanel.Domain.DTOs.GoterPay;
using FinTech_ApiPanel.Domain.DTOs.URLs;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Entities.UserWallets;
using FinTech_ApiPanel.Domain.Interfaces.IAuth;
using FinTech_ApiPanel.Domain.Interfaces.IFinancialComponentMasters;
using FinTech_ApiPanel.Domain.Interfaces.IOperatorMasters;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.IUserFinancialComponents;
using FinTech_ApiPanel.Domain.Interfaces.IUserMasters;
using FinTech_ApiPanel.Domain.Interfaces.IUserWallets;
using FinTech_ApiPanel.Domain.Shared.Common;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.Goterpay;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

namespace FinTech_ApiPanel.Application.Handlers.GoterPay
{
    public class RechargeHandler : IRequestHandler<RechargeCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly GoterPayConfig _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;
        private readonly IClientRepository _clientRepository;
        private readonly bool _isMaster;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly string baseUrl;
        private readonly IUserFinancialComponentRepository _userFinancialComponentRepository;
        private readonly IFinancialComponentMasterRepository _financialComponentMasterRepository;
        private readonly IOperatorRepository _operatorRepository;

        public RechargeHandler(
            HttpClient httpClient,
            IOptions<GoterPayConfig> config,
            IHttpContextAccessor httpContextAccessor,
            IWalletRepository walletRepository,
            IUserRepository userRepository,
            IClientRepository clientRepository,
            IOptions<PanelSettings> panelSettings,
            ITransactionLogRepository transactionLogRepository,
            IOptions<ApiSetting> baseUrl,
            IUserFinancialComponentRepository userFinancialComponentRepository,
            IFinancialComponentMasterRepository financialComponentMasterRepository,
            IOperatorRepository operatorRepository)
        {
            _httpClient = httpClient;
            _config = config.Value;
            _httpContextAccessor = httpContextAccessor;
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _clientRepository = clientRepository;
            _isMaster = panelSettings.Value.IsMasterPanel;
            _transactionLogRepository = transactionLogRepository;
            this.baseUrl = baseUrl.Value.BaseUrl;
            _userFinancialComponentRepository = userFinancialComponentRepository;
            _financialComponentMasterRepository = financialComponentMasterRepository;
            _operatorRepository = operatorRepository;
        }

        public async Task<object> Handle(RechargeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User ID not found in the context.");

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
                    throw new Exception("Outlet ID not found in the context.");

                ValidateRequest(request);

                var operatorInfo = await _operatorRepository.GetByCodeAsync(request.OperatorCode);
                if (operatorInfo == null)
                    throw new Exception("Invalid operator code.");

                if (_isMaster)
                {
                    var responseContent = await PerformRechargeAsync(request);
                    var response = JsonConvert.DeserializeObject<RechargeDto>(responseContent);

                    await LogTransaction(adminWallet, userWallet, request, response, responseContent, outletId, endpointIp, byte.Parse(request.SubService), operatorInfo.Id);
                    return response;
                }
                else
                {
                    var responseContent = await PerformRechargeAsync(request, outletId, endpointIp);
                    var response = JsonConvert.DeserializeObject<RechargeDto>(responseContent);

                    await LogTransaction(adminWallet, userWallet, request, response, responseContent, outletId, endpointIp, byte.Parse(request.SubService), operatorInfo.Id);
                    return response;
                }
            }
            catch (Exception ex)
            {
                var errorJson = new
                {
                    statusCode = "ERR",
                    status = $"Exception: {ex.Message}",
                    data = new { },
                    timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    orderid = string.Empty,
                };

                return System.Text.Json.JsonSerializer.Serialize(errorJson);
            }
        }

        private void ValidateRequest(RechargeCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Number))
                throw new ArgumentException("Mobile number is required");
            if (decimal.Parse(request.Amount) <= 0)
                throw new ArgumentException("Recharge amount must be greater than zero");
            if (string.IsNullOrWhiteSpace(request.OperatorCode))
                throw new ArgumentException("Operator code is required");
            if (string.IsNullOrWhiteSpace(request.CircleCode))
                throw new ArgumentException("Circle code is required");
            if (string.IsNullOrWhiteSpace(request.ExternalRef))
                throw new ArgumentException("Transaction ID is required");
            if (request.ExternalRef.Length > 10)
                throw new ArgumentException("Transaction ID must not exceed 10 characters");
        }

        private async Task<string> PerformRechargeAsync(RechargeCommand request)
        {
            try
            {
                var url = $"https://dashboard.goterpay.com/api/Recharge" +
                          $"?mid={Uri.EscapeDataString(_config.Mid)}" +
                          $"&mkey={Uri.EscapeDataString(_config.MKey)}" +
                          $"&subwallet={Uri.EscapeDataString(_config.SubWallet)}" +
                          $"&txnid={Uri.EscapeDataString(request.ExternalRef)}" +
                          $"&number={Uri.EscapeDataString(request.Number)}" +
                          $"&amount={Uri.EscapeDataString(request.Amount.ToString())}" +
                          $"&operator={Uri.EscapeDataString(request.OperatorCode)}" +
                          $"&circle={Uri.EscapeDataString(request.CircleCode)}";

                using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _httpClient.SendAsync(requestMessage);
                response.EnsureSuccessStatusCode();

                var rawJson = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(rawJson);
                var root = doc.RootElement;
                var status = root.GetProperty("status").GetString();
                var orderId = root.GetProperty("orderId").GetString();
                var mobile = root.GetProperty("mobile").GetString();
                var amount = root.GetProperty("amount").GetString();
                var optId = root.GetProperty("optId").GetString();
                var resText = root.GetProperty("resText").GetString();
                var service = root.GetProperty("service").GetString();
                var billAmount = root.GetProperty("billAmount").GetString();
                var comi = root.GetProperty("Comi").GetString();

                var operatorValue = root.TryGetProperty("operator", out var op) ? op.GetString() : "";
                var circleValue = root.TryGetProperty("circle", out var cir) ? cir.GetString() : "";


                // Build custom JSON
                var custom = new
                {
                    statuscode = status == "SUCCESS" ? "TXN" : "ERR",
                    status = resText,
                    data = new
                    {
                        Mobile = mobile,
                        TransactionValue = amount,
                        ReferenceId = optId,
                        Operator = operatorValue,
                        Circle = circleValue,
                        Service = service,
                        PayableValue = billAmount,
                        Commission = comi,
                    },
                    timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    orderid = orderId,
                };

                return System.Text.Json.JsonSerializer.Serialize(custom);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<string> PerformRechargeAsync(RechargeCommand command, string outletId, string endpointIp)
        {
            try
            {
                var clientConfig = await _clientRepository.GetAdminClientAsync();

                string url = $"{baseUrl}/Recharge";

                using var request = new HttpRequestMessage(HttpMethod.Post, url);

                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Auth-Code", "1");
                request.Headers.Add("Client-Id", clientConfig.ClientId);
                request.Headers.Add("Client-Secret", clientConfig.ClientSecret);
                request.Headers.Add("Endpoint-Ip", endpointIp);
                request.Headers.Add("Outlet-Id", outletId);

                // Request Body
                var requestBody = new
                {
                    number = command.Number,
                    amount = command.Amount,
                    operatorCode = command.OperatorCode,
                    circleCode = command.CircleCode,
                    ExternalRef = command.ExternalRef,
                };

                var json = JsonConvert.SerializeObject(requestBody);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task LogTransaction(Wallet adminWallet, Wallet userWallet, RechargeCommand request, RechargeDto dto, string rawResponse, string outletId, string endpointIp, byte subService, long operatorId)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User ID not found in context.");

                var status = dto.Status?.ToUpperInvariant();
                var parsedUserId = long.TryParse(userId, out var uid) ? uid : 0;

                byte txnStatus;
                string remark;

                switch (status)
                {
                    case "TXN":
                        txnStatus = (byte)Status.Success;
                        remark = $"Recharge Successful | Mobile: {request.Number} | Amount: {request.Amount} | " +
                                 $"Operator: {dto.Data?.Operator} | OrderId: {dto.OrderId}";
                        break;

                    case "ERR":
                        txnStatus = (byte)Status.Failed;
                        var reason = string.IsNullOrWhiteSpace(dto.Status) ? "Unknown Error" : dto.Status;
                        remark = $"Recharge Failed | Reason: {reason} | Mobile: {request.Number} | " +
                                 $"Amount: {request.Amount} | Operator: {dto.Data?.Operator}";
                        break;

                    default:
                        txnStatus = (byte)Status.Pending;
                        remark = $"Recharge Pending | Mobile: {request.Number} | Amount: {request.Amount} | " +
                                 $"Operator: {dto.Data?.Operator}";
                        break;
                }

                var transactionLog = new TransactionLog
                {
                    UserId = parsedUserId,
                    LogType = (byte)LogType.Recharge_Goter,
                    Amount = decimal.Parse(request.Amount),
                    AuditType = (byte)AuditType.Other,
                    CreatedBy = parsedUserId,
                    ReferenceId = status == "SUCCESS" ? dto.Data?.ReferenceId : null,
                    MobileNumber = request.Number,
                    Ipay_Response = rawResponse,
                    Status = txnStatus,
                    Remark = remark,
                    RefUserId = parsedUserId,
                    Ipay_OrderId = status == "SUCCESS" ? dto.OrderId : null,
                    Ipay_StatusCode = dto.StatusCode,
                    Ipay_Environment = "LIVE",
                    Ipay_OutletId = outletId,
                    Ipay_Latitude = request.Latitude,
                    Ipay_Longitude = request.Longitude,
                    EndPointIP = endpointIp,
                };

                var id = await _transactionLogRepository.AddAsync(transactionLog);

                if (string.Equals(dto.StatusCode, "TXN", StringComparison.OrdinalIgnoreCase) || string.Equals(dto.StatusCode, "TUP", StringComparison.OrdinalIgnoreCase))
                    await LogTransactionForWallet(adminWallet, userWallet, dto, id, subService, transactionLog.Ipay_StatusCode, operatorId);
            }
            catch
            {
                // Log internally if needed
            }
        }

        private async Task LogTransactionForWallet(Wallet adminWallet, Wallet userWallet, RechargeDto dto, long logId, byte subService, string statusCode, long operatorId)
        {
            try
            {
                var transactionValue = decimal.Parse(dto.Data.TransactionValue);
                var payableValue = decimal.Parse(dto.Data.PayableValue);
                decimal adminCommission = decimal.Parse(dto.Data.Commission);

                decimal userCommission = await GetComission(userWallet, transactionValue, subService, operatorId);

                if (statusCode == "TXN")
                {
                    // Admin commission
                    adminWallet.TotalBalance += adminCommission;
                    await _transactionLogRepository.AddAsync(new TransactionLog
                    {
                        UserId = adminWallet.UserId,
                        Amount = adminCommission,
                        RemainingAmount = adminWallet.TotalBalance,
                        RefUserId = userWallet.UserId,
                        AuditType = (byte)AuditType.Credit,
                        LogType = (byte)LogType.Recharge_Commission,
                        CreatedBy = adminWallet.UserId,
                        Status = (byte)Status.Success,
                        Remark = $"Admin commission credited: {adminCommission:F2} for recharge OrderId: {dto.OrderId}",
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
                        LogType = (byte)LogType.Recharge_Commission,
                        CreatedBy = adminWallet.UserId,
                        Status = (byte)Status.Success,
                        Remark = $"User commission share debited: {userCommission:F2} for recharge OrderId: {dto.OrderId}",
                        WalletUpdated = true,
                        ReferenceId = logId.ToString()
                    });
                    await _walletRepository.UpdateAsync(adminWallet);
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
                    LogType = (byte)LogType.Recharge,
                    CreatedBy = userWallet.UserId,
                    Status = statusCode == "TXN" ? (byte)Status.Success : (byte)Status.Pending,
                    Remark = $"Recharge amount debited: {transactionValue:F2} for mobile {dto.Data.Mobile} | OrderId: {dto.OrderId}",
                    WalletUpdated = true,
                    ReferenceId = logId.ToString()
                });

                if (statusCode == "TXN")
                {
                    userWallet.TotalBalance += userCommission;
                    await _transactionLogRepository.AddAsync(new TransactionLog
                    {
                        UserId = userWallet.UserId,
                        RefUserId = adminWallet.UserId,
                        Amount = userCommission,
                        RemainingAmount = userWallet.TotalBalance,
                        AuditType = (byte)AuditType.Credit,
                        LogType = (byte)LogType.Recharge_Commission,
                        CreatedBy = userWallet.UserId,
                        Status = (byte)Status.Success,
                        Remark = $"Commission credited: {userCommission:F2} for recharge OrderId: {dto.OrderId}",
                        WalletUpdated = true,
                        ReferenceId = logId.ToString()
                    });
                }

                await _walletRepository.UpdateAsync(userWallet);

                dto.Data.PayableValue = (transactionValue + userCommission).ToString("F2");
            }
            catch
            {
                throw;
            }
        }

        private async Task<decimal> GetComission(Wallet userWallet, decimal transactionValue, byte subService, long operatorId)
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
                                    x.ServiceType == (byte)FinancialComponenService.Recharge &&
                                    x.ServiceSubType == subService &&
                                    x.OperatorId == operatorId);

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
                                    x.ServiceType == (byte)FinancialComponenService.Recharge &&
                                    x.ServiceSubType == subService &&
                                    x.OperatorId == operatorId);

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