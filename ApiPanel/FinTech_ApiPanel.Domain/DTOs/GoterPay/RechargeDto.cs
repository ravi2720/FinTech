using System.Text.Json.Serialization;

namespace FinTech_ApiPanel.Domain.DTOs.GoterPay
{
    public class RechargeDto
    {
        [JsonPropertyName("statusCode")]
        public string StatusCode { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("data")]
        public RechargeData Data { get; set; }

        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }

        [JsonPropertyName("orderid")]
        public string OrderId { get; set; }
    }

    public class RechargeData
    {
        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("transactionValue")]
        public string TransactionValue { get; set; }

        [JsonPropertyName("referenceId")]
        public string ReferenceId { get; set; }

        [JsonPropertyName("operator")]
        public string Operator { get; set; }

        [JsonPropertyName("circle")]
        public string Circle { get; set; }

        [JsonPropertyName("service")]
        public string Service { get; set; }

        [JsonPropertyName("payableValue")]
        public string PayableValue { get; set; }

        [JsonPropertyName("commission")]
        public string Commission { get; set; }
    }

}
