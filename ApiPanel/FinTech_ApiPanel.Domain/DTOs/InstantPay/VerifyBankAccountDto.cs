using Newtonsoft.Json;

namespace FinTech_ApiPanel.Domain.DTOs.InstantPay
{
    public class VerifyBankAccountDto
    {
        [JsonProperty("statuscode")]
        public string StatusCode { get; set; }

        [JsonProperty("actcode")]
        public string ActCode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public VerifyBankAccountData Data { get; set; }

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

    public class VerifyBankAccountData
    {
        [JsonProperty("externalRef")]
        public string ExternalRef { get; set; }

        [JsonProperty("poolReferenceId")]
        public string PoolReferenceId { get; set; }

        [JsonProperty("txnValue")]
        public string TxnValue { get; set; }

        [JsonProperty("txnReferenceId")]
        public string TxnReferenceId { get; set; }

        [JsonProperty("pool")]
        public PoolBV Pool { get; set; }

        [JsonProperty("payer")]
        public BankAccountInfo Payer { get; set; }

        [JsonProperty("payee")]
        public PayeeInfo Payee { get; set; }

        [JsonProperty("isCached")]
        public bool IsCached { get; set; }

        [JsonProperty("isPennyDrop")]
        public bool IsPennyDrop { get; set; }
    }

    public class PoolBV
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("openingBal")]
        public string OpeningBal { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("closingBal")]
        public string ClosingBal { get; set; }
    }

    public class BankAccountInfo
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class PayeeInfo : BankAccountInfo
    {
        [JsonProperty("ifsc")]
        public string IFSC { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("mccCode")]
        public string MccCode { get; set; }

        [JsonProperty("merchantOnboardingType")]
        public string MerchantOnboardingType { get; set; }

        [JsonProperty("merchantGenre")]
        public string MerchantGenre { get; set; }

        [JsonProperty("accountType")]
        public string AccountType { get; set; }

        [JsonProperty("iin")]
        public string IIN { get; set; }

        [JsonProperty("nameMatchPercent")]
        public decimal? NameMatchPercent { get; set; }
    }
}
