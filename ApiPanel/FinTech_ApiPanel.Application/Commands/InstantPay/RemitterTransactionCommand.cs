using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class RemitterTransactionCommand : IRequest<object>
    {
        [JsonProperty("remitterMobileNumber")]
        public string RemitterMobileNumber { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("ifsc")]
        public string IFSC { get; set; }

        [JsonProperty("transferMode")]
        public string TransferMode { get; set; }

        [JsonProperty("transferAmount")]
        public string TransferAmount { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("referenceKey")]
        public string ReferenceKey { get; set; }

        [JsonProperty("otp")]
        public int Otp { get; set; }

        [JsonProperty("externalRef")]
        public string ExternalRef { get; set; }
    }

}
