using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.FundManagements
{
    public class ReleaseFundCommand : IRequest<ApiResponse>
    {
        public long UserId { get; set; }
        public decimal Amount { get; set; }
        public string? Remark { get; set; }
        public string? ReferenceId { get; set; }
    }
}
