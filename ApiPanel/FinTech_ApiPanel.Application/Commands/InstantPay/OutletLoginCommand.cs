using FinTech_ApiPanel.Domain.DTOs.InstantPay;
using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class OutletLoginCommand : IRequest<object>
    {
        [JsonProperty("type")]
        public string Type { get; set; } = "DAILY_LOGIN";

        [JsonProperty("serviceType", NullValueHandling = NullValueHandling.Ignore)]
        public string? ServiceType { get; set; }

        [JsonProperty("externalRef")]
        public string ExternalRef { get; set; } = string.Empty;

        [JsonProperty("latitude")]
        public string Latitude { get; set; } = string.Empty;

        [JsonProperty("longitude")]
        public string Longitude { get; set; } = string.Empty;

        [JsonProperty("captureType")]
        public string CaptureType { get; set; } = string.Empty;

        [JsonProperty("biometricData")]
        public BiometricDataDto BiometricData { get; set; } = new BiometricDataDto();
    }
}
