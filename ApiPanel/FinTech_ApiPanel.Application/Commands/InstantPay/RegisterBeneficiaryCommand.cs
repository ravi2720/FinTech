using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class RegisterBeneficiaryCommand : IRequest<object>
    {
        [JsonProperty("beneficiaryMobileNumber")]
        public string BeneficiaryMobileNumber { get; set; }

        [JsonProperty("remitterMobileNumber")]
        public string RemitterMobileNumber { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("ifsc")]
        public string IFSC { get; set; }

        [JsonProperty("bankId")]
        public string BankId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
