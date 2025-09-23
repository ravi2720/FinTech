using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.FundManagements
{
    public class AcceptFundRequestCommand : IRequest<ApiResponse>
    {
        public long Id { get; set; }
        public string? Remark { get; set; }
    };
}
