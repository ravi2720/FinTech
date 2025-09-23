using System.Text.Json.Serialization;

namespace FinTech_ApiPanel.Domain.DTOs.GoterPay
{
    public class MobileInfoDto
    {
        [JsonPropertyName("statusCode")] 
        public string StatusCode { get; set; }

        [JsonPropertyName("status")] 
        public string Status { get; set; }

        [JsonPropertyName("data")] 
        public MobileInfoData Data { get; set; }

        [JsonPropertyName("timestamp")] 
        public string Timestamp { get; set; }

        [JsonPropertyName("orderid")] 
        public string OrderId { get; set; }
    }

    public class MobileInfoData
    {
        [JsonPropertyName("operatorName")] 
        public string OperatorName { get; set; }

        [JsonPropertyName("circle")] 
        public string Circle { get; set; }
    }
}
