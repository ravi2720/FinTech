using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class CashDepositDto
    {
        [JsonProperty("statuscode")]
        public string StatusCode { get; set; }

        [JsonProperty("actcode")]
        public string? ActCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public CashDepositData Data { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("ipay_uuid")]
        public string IpayUuid { get; set; }

        [JsonProperty("orderid")]
        public string? OrderId { get; set; }

        [JsonProperty("environment")]
        public string Environment { get; set; }

        [JsonProperty("internalCode")]
        public string? InternalCode { get; set; }
    }
    public class CashDepositData
    {
        [JsonProperty("externalRef")]
        public string ExternalRef { get; set; }

        [JsonProperty("bankName")]
        public string BankName { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("ipayId")]
        public string IpayId { get; set; }

        [JsonProperty("transactionMode")]
        public string TransactionMode { get; set; }

        [JsonProperty("payableValue")]
        public string PayableValue { get; set; }

        [JsonProperty("transactionValue")]
        public string TransactionValue { get; set; }

        [JsonProperty("openingBalance")]
        public string? OpeningBalance { get; set; }

        [JsonProperty("closingBalance")]
        public string? ClosingBalance { get; set; }

        [JsonProperty("operatorId")]
        public string OperatorId { get; set; }

        [JsonProperty("walletIpayId")]
        public string? WalletIpayId { get; set; }

        [JsonProperty("bankAccountBalance")]
        public string? BankAccountBalance { get; set; }

        [JsonProperty("miniStatement")]
        public List<MiniStatementItemDto> MiniStatement { get; set; } = new();
    }
}
