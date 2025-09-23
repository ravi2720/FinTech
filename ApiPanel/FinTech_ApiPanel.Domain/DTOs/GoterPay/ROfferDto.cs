using System.Text.Json.Serialization;

namespace FinTech_ApiPanel.Domain.DTOs.GoterPay
{
    public class ROfferDto
    {
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public List<ROfferData> Data { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("orderid")]
        public string OrderId { get; set; }
    }

    public class ROfferData
    {
        [JsonPropertyName("amount")]
        public string Amount { get; set; }

        [JsonPropertyName("commi")]
        public string Commi { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }
    }
}
