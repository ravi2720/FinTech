using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class EKYCValidateDto
    {
        [JsonProperty("statuscode")]
        public string StatusCode { get; set; }

        [JsonProperty("actcode")]
        public string ActCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public EKYCValidateData Data { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("ipay_uuid")]
        public string IpayUuid { get; set; }

        [JsonProperty("orderid")]
        public string OrderId { get; set; }

        [JsonProperty("environment")]
        public string Environment { get; set; }
    }

    public class EKYCValidateData
    {
        [JsonProperty("outletId")]
        public int OutletId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("pincode")]
        public string Pincode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("districtName")]
        public string DistrictName { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("profilePic")]
        public string ProfilePic { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }
    }
}
