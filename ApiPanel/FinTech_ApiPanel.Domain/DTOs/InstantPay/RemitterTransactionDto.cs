using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class RemitterTransactionDto
    {
        [JsonProperty("statuscode")]
        public string StatusCode { get; set; }

        [JsonProperty("actcode")]
        public string ActCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public RemitterTransactionData Data { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("ipay_uuid")]
        public string IpayUuid { get; set; }

        [JsonProperty("orderid")]
        public string OrderId { get; set; }

        [JsonProperty("environment")]
        public string Environment { get; set; }

        [JsonProperty("internalCode")]
        public string InternalCode { get; set; }
    }

    public class RemitterTransactionData
    {
        [JsonProperty("externalRef")]
        public string ExternalRef { get; set; }

        [JsonProperty("poolReferenceId")]
        public string PoolReferenceId { get; set; }

        [JsonProperty("txnValue")]
        public string TxnValue { get; set; }

        [JsonProperty("txnReferenceId")]
        public string TxnReferenceId { get; set; }

        [JsonProperty("pool")]
        public TransactionPool Pool { get; set; }

        [JsonProperty("remitterMobile")]
        public string RemitterMobile { get; set; }

        [JsonProperty("beneficiaryAccount")]
        public string BeneficiaryAccount { get; set; }

        [JsonProperty("beneficiaryIfsc")]
        public string BeneficiaryIfsc { get; set; }

        [JsonProperty("beneficiaryName")]
        public string BeneficiaryName { get; set; }
    }

    public class TransactionPool
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("openingBal")]
        public string OpeningBal { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("closingBal")]
        public string ClosingBal { get; set; }
    }

}
