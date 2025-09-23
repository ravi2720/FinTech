using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.FundManagements
{
    public class FundSettlementCommand : IRequest<ApiResponse>
    {
        public decimal Amount { get; set; }
        public long? UserBankId { get; set; }
        public string ReferenceId { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }
}
