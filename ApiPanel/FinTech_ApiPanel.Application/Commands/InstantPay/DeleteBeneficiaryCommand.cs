using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class DeleteBeneficiaryCommand : IRequest<object>
    {
        [JsonProperty("remitterMobileNumber")]
        public string RemitterMobileNumber { get; set; }

        [JsonProperty("beneficiaryId")]
        public string BeneficiaryId { get; set; }

    }
}
