using System.Text.Json.Serialization;

namespace FinTech_ApiPanel.Domain.DTOs.GoterPay
{
    public class DthInfoDto
    {
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public DthInfoData Data { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("orderid")]
        public string OrderId { get; set; }
    }

    public class DthInfoData
    {
        [JsonPropertyName("customerName")]
        public string CustomerName { get; set; }

        [JsonPropertyName("balance")]
        public string Balance { get; set; }

        [JsonPropertyName("monthly")]
        public string Monthly { get; set; }

        [JsonPropertyName("nextRecharge")]
        public string NextRecharge { get; set; }

        [JsonPropertyName("planname")]
        public string Planname { get; set; }
    }
}
