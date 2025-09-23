using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class RemitterRegistrationCommand : IRequest<object>
    {
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("encryptedAadhaar")]
        public string EncryptedAadhaar { get; set; }

        [JsonProperty("referenceKey")]
        public string ReferenceKey { get; set; }
    }
}
