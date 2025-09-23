using MediatR;

namespace FinTech_ApiPanel.Application.Queries.GoterPay
{
    public class ROfferQuery : IRequest<object>
    {
        public string OperatorCode { get; set; }
        public string Number { get; set; }
    }
}
