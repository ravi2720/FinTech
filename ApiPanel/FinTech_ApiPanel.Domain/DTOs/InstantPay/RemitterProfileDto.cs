using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class RemitterProfileDto
    {
        [JsonProperty("statuscode")]
        public string StatusCode { get; set; }

        [JsonProperty("actcode")]
        public string ActCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public RemitterProfileData Data { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }

        [JsonProperty("ipay_uuid")]
        public string IpayUuid { get; set; }

        [JsonProperty("orderid")]
        public string OrderId { get; set; }

        [JsonProperty("environment")]
        public string Environment { get; set; }

        [JsonProperty("internalCode")]
        public string InternalCode { get; set; }
    }

    public class RemitterProfileData
    {
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("pincode")]
        public string Pincode { get; set; }

        [JsonProperty("limitPerTransaction")]
        public decimal LimitPerTransaction { get; set; }

        [JsonProperty("limitTotal")]
        public string LimitTotal { get; set; }

        [JsonProperty("limitConsumed")]
        public string LimitConsumed { get; set; }

        [JsonProperty("limitAvailable")]
        public string LimitAvailable { get; set; }

        [JsonProperty("limitDetails")]
        public LimitDetails LimitDetails { get; set; }

        [JsonProperty("beneficiaries")]
        public List<Beneficiary> Beneficiaries { get; set; }

        [JsonProperty("isTxnOtpRequired")]
        public bool IsTxnOtpRequired { get; set; }

        [JsonProperty("isTxnBioAuthRequired")]
        public bool IsTxnBioAuthRequired { get; set; }

        [JsonProperty("isFaceAuthAvailable")]
        public bool? IsFaceAuthAvailable { get; set; }

        [JsonProperty("pidOptionWadh")]
        public string PidOptionWadh { get; set; }

        [JsonProperty("validity")]
        public string Validity { get; set; }

        [JsonProperty("referenceKey")]
        public string ReferenceKey { get; set; }
    }

    public class LimitDetails
    {
        [JsonProperty("maximumDailyLimit")]
        public string MaximumDailyLimit { get; set; }

        [JsonProperty("consumedDailyLimit")]
        public string ConsumedDailyLimit { get; set; }

        [JsonProperty("availableDailyLimit")]
        public string AvailableDailyLimit { get; set; }

        [JsonProperty("maximumMonthlyLimit")]
        public string MaximumMonthlyLimit { get; set; }

        [JsonProperty("consumedMonthlyLimit")]
        public string ConsumedMonthlyLimit { get; set; }

        [JsonProperty("availableMonthlyLimit")]
        public string AvailableMonthlyLimit { get; set; }
    }

    public class Beneficiary
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("ifsc")]
        public string IFSC { get; set; }

        [JsonProperty("bank")]
        public string Bank { get; set; }

        [JsonProperty("beneficiaryMobileNumber")]
        public string BeneficiaryMobileNumber { get; set; }

        [JsonProperty("verificationDt")]
        public string VerificationDate { get; set; }
    }

}
