using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class BeneficiaryRegistrationVerifyCommand : IRequest<object>
    {
        [JsonProperty("remitterMobileNumber")]
        public string RemitterMobileNumber { get; set; }

        [JsonProperty("otp")]
        public int Otp { get; set; }

        [JsonProperty("beneficiaryId")]
        public string BeneficiaryId { get; set; }

        [JsonProperty("referenceKey")]
        public string ReferenceKey { get; set; }

    }
}
