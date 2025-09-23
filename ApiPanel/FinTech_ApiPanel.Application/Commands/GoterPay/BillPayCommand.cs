using MediatR;

namespace FinTech_ApiPanel.Application.Commands.GoterPay
{
    public class BillPayCommand : IRequest<object>
    {
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public string OperatorCode { get; set; }
        public string TxnId { get; set; }
        public string Optional1 { get; set; }
    }
}
