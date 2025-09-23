using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class RemitterBioAuthTransactionCommand : IRequest<object>
    {
        [JsonProperty("remitterMobileNumber")]
        public string RemitterMobileNumber { get; set; }

        [JsonProperty("referenceKey")]
        public string ReferenceKey { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("otp")]
        public string Otp { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("externalRef")]
        public string ExternalRef { get; set; }

        [JsonProperty("consentTaken")]
        public string ConsentTaken { get; set; }

        [JsonProperty("biometricData")]
        public BiometricDataForBioAuthTransaction BiometricData { get; set; }
    }

    public class BiometricDataForBioAuthTransaction
    {
        public string Ci { get; set; }
        public string Hmac { get; set; }
        public string PidData { get; set; }
        public string Ts { get; set; }
        public string Dc { get; set; }
        public string Mi { get; set; }
        public string DpId { get; set; }
        public string Mc { get; set; }
        public string RdsId { get; set; }
        public string RdsVer { get; set; }
        public string Skey { get; set; }
        public string SrNo { get; set; }
    }
}
