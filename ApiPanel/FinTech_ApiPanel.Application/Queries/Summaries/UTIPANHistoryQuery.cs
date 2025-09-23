using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.TransactionLogs;
using FinTech_ApiPanel.Domain.Shared.Pagination;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.Summaries
{
    public class UTIPANHistoryQuery : PaginationFilter, IRequest<ApiResponse<PagedResponse<TransactionLogDto>>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

}
