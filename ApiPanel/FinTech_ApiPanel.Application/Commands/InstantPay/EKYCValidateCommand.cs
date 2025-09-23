using FinTech_ApiPanel.Domain.DTOs.InstantPay;
using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class EKYCValidateCommand : IRequest<object>
    {
        [JsonProperty("otpReferenceID")]
        public string OtpReferenceID { get; set; } = string.Empty;

        [JsonProperty("otp")]
        public string Otp { get; set; } = string.Empty;

        [JsonProperty("hash")]
        public string Hash { get; set; } = string.Empty;
    }
}
