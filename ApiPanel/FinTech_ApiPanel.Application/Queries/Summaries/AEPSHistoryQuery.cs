using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.TransactionLogs;
using FinTech_ApiPanel.Domain.Shared.Pagination;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.Summaries
{
    public class AEPSHistoryQuery : PaginationFilter, IRequest<ApiResponse<PagedResponse<TransactionLogDto>>>
    {
        public byte? AuditType { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
