using FinTech_ApiPanel.Domain.DTOs.InstantPay;
using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class AEPSCashDepositCommand : IRequest<object>
    {
        [JsonProperty("bankiin")]
        public string BankIIN { get; set; } = string.Empty;

        [JsonProperty("latitude")]
        public string Latitude { get; set; } = string.Empty;

        [JsonProperty("longitude")]
        public string Longitude { get; set; } = string.Empty;

        [JsonProperty("externalRef")]
        public string ExternalRef { get; set; } = string.Empty;

        [JsonProperty("mobile")]
        public string Mobile { get; set; } = string.Empty;

        [JsonProperty("amount")]
        public string Amount { get; set; } = string.Empty;

        [JsonProperty("biometricData")]
        public BiometricDataDto BiometricData { get; set; } = new BiometricDataDto();
    }
}
