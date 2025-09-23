using MediatR;
using Newtonsoft.Json;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class GetRemitterProfileCommand : IRequest<object>
    {
        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; } = string.Empty;
    }
}
