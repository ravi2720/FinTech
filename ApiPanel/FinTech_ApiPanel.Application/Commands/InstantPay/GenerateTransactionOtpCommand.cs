using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class GenerateTransactionOtpCommand : IRequest<object>
    {
        [JsonProperty("remitterMobileNumber")]
        public string RemitterMobileNumber { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("referenceKey")]
        public string ReferenceKey { get; set; }
    }
}
