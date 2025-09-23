using FinTech_ApiPanel.Application.Commands.GoterPay;
using FinTech_ApiPanel.Domain.DTOs.GoterPay;
using FinTech_ApiPanel.Domain.Entities.TransactionLogs;
using FinTech_ApiPanel.Domain.Interfaces.ITransactionLogs;
using FinTech_ApiPanel.Domain.Shared.Enums;
using FinTech_ApiPanel.Domain.Shared.Goterpay;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Handlers.GoterPay
{
    public class BillPayHandler : IRequestHandler<BillPayCommand, object>
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITransactionLogRepository _transactionLogRepository;
        private readonly GoterPayConfig _config;
        private readonly ILogger<BillPayHandler> _logger;

        public BillPayHandler(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor,
            ITransactionLogRepository transactionLogRepository,
            IOptions<GoterPayConfig> config,
            ILogger<BillPayHandler> logger)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _transactionLogRepository = transactionLogRepository;
            _config = config.Value;
            _logger = logger;
        }

        public async Task<object> Handle(BillPayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Starting BillPay request for Number: {Number}, Amount: {Amount}", request.Number, request.Amount);

                ValidateRequest(request);

                var responseContent = await PerformBillPayAsync(request);

                BillPayDto parsedResponse;
                try
                {
                    parsedResponse = JsonConvert.DeserializeObject<BillPayDto>(responseContent);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to deserialize BillPay API response: {Response}", responseContent);
                    throw new Exception("Invalid API response format.");
                }

                await LogTransaction(request, parsedResponse, responseContent);

                _logger.LogInformation("BillPay completed successfully for TxnId: {TxnId}", request.TxnId);

                return parsedResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BillPay request failed for Number: {Number}", request.Number);
                return new { status = "ERROR", message = ex.Message };
            }
        }

        private void ValidateRequest(BillPayCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Number))
                throw new ArgumentException("Number is required");
            if (request.Amount <= 0)
                throw new ArgumentException("Bill amount must be greater than zero");
            if (string.IsNullOrWhiteSpace(request.OperatorCode))
                throw new ArgumentException("Operator code is required");
            if (string.IsNullOrWhiteSpace(request.TxnId))
                throw new ArgumentException("Transaction ID is required");
        }

        private async Task<string> PerformBillPayAsync(BillPayCommand request)
        {
            var url = $"https://dashboard.goterpay.com/api/BillPay" +
                      $"?mid={Uri.EscapeDataString(_config.Mid)}" +
                      $"&mkey={Uri.EscapeDataString(_config.MKey)}" +
                      $"&subwallet={Uri.EscapeDataString(_config.SubWallet)}" +
                      $"&txnid={Uri.EscapeDataString(request.TxnId)}" +
                      $"&number={Uri.EscapeDataString(request.Number)}" +
                      $"&amount={Uri.EscapeDataString(request.Amount.ToString())}" +
                      $"&operator={Uri.EscapeDataString(request.OperatorCode)}";

            if (!string.IsNullOrEmpty(request.Optional1))
                url += $"&optional1={Uri.EscapeDataString(request.Optional1)}";

            _logger.LogDebug("BillPay API request URL: {Url}", url);

            using var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            _logger.LogDebug("BillPay API raw response: {Response}", content);

            return content;
        }

        private async Task LogTransaction(BillPayCommand request, BillPayDto dto, string rawResponse)
        {
            try
            {
                //var userId = _httpContextAccessor.HttpContext?.Items["UserId"]?.ToString();
                //if (string.IsNullOrEmpty(userId))
                //    throw new Exception("User ID not found in context.");

                //var transactionLog = new TransactionLog
                //{
                //    UserId = long.Parse(userId),
                //    LogType = (byte)LogType.BillPayment_Goter,
                //    Amount = request.Amount,
                //    AuditType = (byte)AuditType.Other,
                //    CreatedBy = long.Parse(userId),
                //    ReferenceId = request.TxnId,
                //    MobileNumber = request.Number,
                //    Ipay_Response = rawResponse,
                //    Status = dto.Status?.ToUpper() == "SUCCESS" ? (byte)Status.Success :
                //             dto.Status?.ToUpper() == "PENDING" ? (byte)Status.Pending : (byte)Status.Failed,
                //    Remark = dto.ResText ?? "BillPay Transaction"
                //};

                //await _transactionLogRepository.AddAsync(transactionLog);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to log BillPay transaction for TxnId: {TxnId}", request.TxnId);
            }
        }
    }
}
