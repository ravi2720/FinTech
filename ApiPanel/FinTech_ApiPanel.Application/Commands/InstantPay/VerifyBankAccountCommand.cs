using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class VerifyBankAccountCommand : IRequest<object>
    {
        [JsonProperty("consent")]
        public string Consent { get; set; } = "Y";

        [JsonProperty("pennyDrop")]
        public string PennyDrop { get; set; } = "YES";

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("payee")]
        public PayeeDto Payee { get; set; }

        [JsonIgnore]
        public string ExternalRef { get; set; } // Optional for logging only
    }
    public class PayeeDto
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("bankIfsc")]
        public string BankIfsc { get; set; }
    }
}
