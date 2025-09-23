using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class RemitterRegistrationVerifyCommand : IRequest<object>
    {
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("otp")]
        public int Otp { get; set; }

        [JsonProperty("referenceKey")]
        public string ReferenceKey { get; set; }
    }
}
