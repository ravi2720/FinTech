using MediatR;

namespace FinTech_ApiPanel.Application.Queries.GoterPay
{
    public class MobileInfoQuery : IRequest<object>
    {
        public string Mobile { get; set; }
    }

}
