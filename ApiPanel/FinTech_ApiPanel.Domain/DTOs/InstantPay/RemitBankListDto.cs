using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class RemitBankListDto
    {
        [JsonProperty("statuscode")]
        public string StatusCode { get; set; }

        [JsonProperty("actcode")]
        public string ActCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<RemitBankData> Data { get; set; }

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

    public class RemitBankData
    {
        [JsonProperty("bankId")]
        public string BankId { get; set; }

        [JsonProperty("name")]
        public string BankName { get; set; }

        [JsonProperty("ifscAlias")]
        public string IfscAlias { get; set; }

        [JsonProperty("ifscGlobal")]
        public string IfscGlobal { get; set; }

        [JsonProperty("neftEnabled")]
        public int NeftEnabled { get; set; }

        [JsonProperty("neftFailureRate")]
        public string NeftFailureRate { get; set; }

        [JsonProperty("impsEnabled")]
        public int ImpsEnabled { get; set; }

        [JsonProperty("impsFailureRate")]
        public string ImpsFailureRate { get; set; }

        [JsonProperty("upiEnabled")]
        public int UpiEnabled { get; set; }

        [JsonProperty("upiFailureRate")]
        public string UpiFailureRate { get; set; }
    }

}
