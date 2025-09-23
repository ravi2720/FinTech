using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class OutletLoginDto
    {
        [JsonProperty("statuscode")]
        public string StatusCode { get; set; }

        [JsonProperty("actcode")]
        public string ActCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public OutletLoginStatusData Data { get; set; }

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

    public class OutletLoginStatusData
    {
        [JsonProperty("isFaceAuthAvailable")]
        public bool IsFaceAuthAvailable { get; set; }

        [JsonProperty("aadhaarLastFour")]
        public string AadhaarLastFour { get; set; }

        [JsonProperty("isTxnBioLoginRequired")]
        public bool IsTxnBioLoginRequired { get; set; }

        [JsonProperty("poolReferenceId")]
        public string PoolReferenceId { get; set; }

        [JsonProperty("txnReferenceId")]
        public string TxnReferenceId { get; set; }

        [JsonProperty("pool")]
        public PoolDetails Pool { get; set; }
    }

    public class PoolDetails
    {
        [JsonProperty("openingBalance")]
        public string OpeningBalance { get; set; }

        [JsonProperty("closingBalance")]
        public string ClosingBalance { get; set; }

        [JsonProperty("transactionValue")]
        public string TransactionValue { get; set; }

        [JsonProperty("payableValue")]
        public string PayableValue { get; set; }
    }
}
