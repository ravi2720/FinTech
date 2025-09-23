using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class DeleteBeneficiaryResponseDto
    {
        [JsonProperty("statuscode")]
        public string StatusCode { get; set; }

        [JsonProperty("actcode")]
        public string ActCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public DeleteBeneficiaryData Data { get; set; }

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

    public class DeleteBeneficiaryData
    {
        [JsonProperty("beneficiaryId")]
        public string BeneficiaryId { get; set; }

        [JsonProperty("validity")]
        public string Validity { get; set; }

        [JsonProperty("referenceKey")]
        public string ReferenceKey { get; set; }
    }
}
