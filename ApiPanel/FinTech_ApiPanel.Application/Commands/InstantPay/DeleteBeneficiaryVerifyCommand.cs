using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class DeleteBeneficiaryVerifyCommand : IRequest<object>
    {
        [JsonProperty("remitterMobileNumber")]
        public string RemitterMobileNumber { get; set; }

        [JsonProperty("beneficiaryId")]
        public string BeneficiaryId { get; set; }

        [JsonProperty("otp")]
        public string Otp { get; set; }

        [JsonProperty("referenceKey")]
        public string ReferenceKey { get; set; }
    }
}
