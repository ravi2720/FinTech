using System.Text.Json.Serialization;

namespace FinTech_ApiPanel.Domain.DTOs.GoterPay
{
    public class RechargePlanDto
    {
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public List<RechargePlanData> Data { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("orderid")]
        public string OrderId { get; set; }
    }

    public class RechargePlanData
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("talktime")]
        public string Talktime { get; set; }

        [JsonPropertyName("validity")]
        public string Validity { get; set; }
    }
}
