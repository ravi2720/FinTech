using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.InstantPay
{
    public class ManagePendingRequestCommand : IRequest<ApiResponse>
    {
        public long TransactionId { get; set; }
        public byte Status { get; set; }
    }
}
