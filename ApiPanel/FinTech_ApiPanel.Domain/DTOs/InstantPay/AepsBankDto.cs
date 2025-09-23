using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class AepsBankDto
    {
        [JsonProperty("statuscode")]
        public string StatusCode { get; set; }

        [JsonProperty("actcode")]
        public string ActCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<AEPSBankData> Data { get; set; }

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
    public class AEPSBankData
    {
        [JsonProperty("bankId")]
        public int BankId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("iin")]
        public string IIN { get; set; }

        [JsonProperty("aepsEnabled")]
        public bool AEPS_Enabled { get; set; }

        [JsonProperty("aadhaarpayEnabled")]
        public bool AadhaarPay_Enabled { get; set; }

        [JsonProperty("aepsFailureRate")]
        public string AEPS_FailureRate { get; set; }

        [JsonProperty("aadhaarpayFailureRate")]
        public string AadhaarPay_FailureRate { get; set; }
    }
}
