using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.GoterPay
{
    public class RechargeCommand : IRequest<object>
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("operatorCode")]
        public string OperatorCode { get; set; }

        [JsonProperty("circleCode")]
        public string CircleCode { get; set; }

        [JsonProperty("externalRef")]
        public string ExternalRef { get; set; } = string.Empty;

        [JsonProperty("latitude")]
        public string Latitude { get; set; } = string.Empty;

        [JsonProperty("longitude")]
        public string Longitude { get; set; } = string.Empty;

        [JsonProperty("subService")]
        public string SubService { get; set; } = string.Empty;
    }
}
