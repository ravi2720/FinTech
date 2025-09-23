using MediatR;

namespace FinTech_ApiPanel.Application.Queries.GoterPay
{
    public class RechargePlanQuery : IRequest<object>
    {
        public string OperatorCode { get; set; }
        public string Circle { get; set; }
    }
}
