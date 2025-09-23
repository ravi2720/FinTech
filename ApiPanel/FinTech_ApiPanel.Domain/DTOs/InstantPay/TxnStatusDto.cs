using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class TxnStatusDto
    {
        [JsonProperty("statuscode")]
        public string StatusCode { get; set; }

        [JsonProperty("actcode")]
        public string? ActCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

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

        [JsonProperty("data")]
        public TxnStatusDataDto Data { get; set; }
    }
    public class TxnStatusDataDto
    {
        [JsonProperty("externalRef")]
        public string ExternalRef { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("serviceCode")]
        public string ServiceCode { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("beneficiaryName")]
        public string? BeneficiaryName { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("ifsc")]
        public string Ifsc { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("transactionTime")]
        public string TransactionTime { get; set; }

        [JsonProperty("utr")]
        public string? Utr { get; set; }

        [JsonProperty("operatorRef")]
        public string? OperatorRef { get; set; }

        [JsonProperty("bankName")]
        public string BankName { get; set; }

        [JsonProperty("outletId")]
        public string OutletId { get; set; }

        [JsonProperty("ipayId")]
        public string IpayId { get; set; }
    }
}
