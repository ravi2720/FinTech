using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class RemitterKycCommand : IRequest<object>
    {
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("referenceKey")]
        public string ReferenceKey { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("captureType")]
        public string CaptureType { get; set; }

        [JsonProperty("externalRef")]
        public string ExternalRef { get; set; }

        [JsonProperty("consentTaken")]
        public string ConsentTaken { get; set; }

        [JsonProperty("biometricData")]
        public BiometricDataRemitterKyc BiometricData { get; set; } = new BiometricDataRemitterKyc();
    }

    public class BiometricDataRemitterKyc
    {
        [JsonProperty("ci")]
        public string Ci { get; set; } = string.Empty;

        [JsonProperty("hmac")]
        public string Hmac { get; set; } = string.Empty;
        [JsonProperty("pidData")]
        public string PidData { get; set; } = string.Empty;
        [JsonProperty("ts")]
        public string Ts { get; set; } = string.Empty;

        [JsonProperty("dc")]
        public string Dc { get; set; } = string.Empty;
        [JsonProperty("mi")]
        public string Mi { get; set; } = string.Empty;
        [JsonProperty("dpId")]
        public string DpId { get; set; } = string.Empty;
        [JsonProperty("mc")]
        public string Mc { get; set; } = string.Empty;
        [JsonProperty("rdsId")]
        public string RdsId { get; set; } = string.Empty;
        [JsonProperty("rdsVer")]
        public string RdsVer { get; set; } = string.Empty;
        [JsonProperty("Skey")]
        public string Skey { get; set; } = string.Empty;
        [JsonProperty("srno")]
        public string SrNo { get; set; } = string.Empty;

        [JsonProperty("pidOptionWadh")]
        public string PidOptionWadh { get; set; }
    }
}
