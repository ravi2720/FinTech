using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.Dashboard
{
    public class DashboardQuery : IRequest<ApiResponse<object>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    };
}
