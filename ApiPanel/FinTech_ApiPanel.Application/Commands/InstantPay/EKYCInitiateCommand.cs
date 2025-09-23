using FinTech_ApiPanel.Domain.DTOs.InstantPay;
using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class EKYCInitiateCommand : IRequest<object>
    {
        [JsonProperty("mobile")]
        public string Mobile { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("aadhaar")]
        public string Aadhaar { get; set; } = string.Empty;

        [JsonProperty("pan")]
        public string Pan { get; set; } = string.Empty;

        [JsonProperty("bankAccountNo")]
        public string BankAccountNo { get; set; } = string.Empty;

        [JsonProperty("bankIfsc")]
        public string BankIfsc { get; set; } = string.Empty;

        [JsonProperty("latitude")]
        public string Latitude { get; set; } = string.Empty;

        [JsonProperty("longitude")]
        public string Longitude { get; set; } = string.Empty;

        [JsonProperty("consent")]
        public string Consent { get; set; } = string.Empty;
    }
}
