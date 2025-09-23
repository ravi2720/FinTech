using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace FinTech_ApiPanel.Domain.DTOs.GoterPay
{
    public class BillPayDto
    {
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public BillPayData Data { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("orderid")]
        public string OrderId { get; set; }
    }

    public class BillPayData
    {
        [JsonProperty("TxnId")]
        public string TxnId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("Number")]
        public string Number { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("operator")]
        public string Operator { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("bal")]
        public string Balance { get; set; }

        [JsonProperty("resText")]
        public string ResText { get; set; }

        [JsonProperty("billAmount")]
        public string BillAmount { get; set; }
    }
}
