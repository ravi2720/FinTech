using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class TxnStatusCommand : IRequest<object>
    {
        [JsonProperty("transactionDate")]
        public string TransactionDate { get; set; } = string.Empty; // Format: yyyy-MM-dd

        [JsonProperty("externalRef")]
        public string ExternalRef { get; set; } = string.Empty;
    }
}
