using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class BiometricDataDto
    {
        [JsonProperty("encryptedAadhaar")]
        public string EncryptedAadhaar { get; set; } = string.Empty;

        [JsonProperty("dc")]
        public string Dc { get; set; } = string.Empty;

        [JsonProperty("ci")]
        public string Ci { get; set; } = string.Empty;

        [JsonProperty("hmac")]
        public string Hmac { get; set; } = string.Empty;

        [JsonProperty("dpId")]
        public string DpId { get; set; } = string.Empty;

        [JsonProperty("mc")]
        public string Mc { get; set; } = string.Empty;

        [JsonProperty("pidDataType")]
        public string PidDataType { get; set; } = string.Empty;

        [JsonProperty("sessionKey")]
        public string SessionKey { get; set; } = string.Empty;

        [JsonProperty("mi")]
        public string Mi { get; set; } = string.Empty;

        [JsonProperty("rdsId")]
        public string RdsId { get; set; } = string.Empty;

        [JsonProperty("errCode")]
        public string ErrCode { get; set; } = string.Empty;

        [JsonProperty("errInfo")]
        public string ErrInfo { get; set; } = string.Empty;

        [JsonProperty("fCount")]
        public string FCount { get; set; } = string.Empty;

        [JsonProperty("fType")]
        public string FType { get; set; } = string.Empty;

        [JsonProperty("iCount")]
        public int ICount { get; set; }

        [JsonProperty("iType")]
        public string IType { get; set; } = string.Empty;

        [JsonProperty("pCount")]
        public int PCount { get; set; }

        [JsonProperty("pType")]
        public string PType { get; set; } = string.Empty;

        [JsonProperty("srno")]
        public string SrNo { get; set; } = string.Empty;

        [JsonProperty("sysid")]
        public string SysId { get; set; } = string.Empty;

        [JsonProperty("ts")]
        public string Timestamp { get; set; } = string.Empty;

        [JsonProperty("pidData")]
        public string PidData { get; set; } = string.Empty;

        [JsonProperty("qScore")]
        public string QScore { get; set; } = string.Empty;

        [JsonProperty("nmPoints")]
        public string NmPoints { get; set; } = string.Empty;

        [JsonProperty("rdsVer")]
        public string RdsVer { get; set; } = string.Empty;
    }
}
