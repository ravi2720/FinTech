using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class RemitterKycDto
    {
        [JsonProperty("statuscode")]
        public string StatusCode { get; set; }

        [JsonProperty("actcode")]
        public string ActCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public RemitterBiometricKycData Data { get; set; }

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

    public class RemitterBiometricKycData
    {
        [JsonProperty("poolReferenceId")]
        public string PoolReferenceId { get; set; }

        [JsonProperty("pool")]
        public PoolKYC Pool { get; set; }
    }

    public class PoolKYC
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
