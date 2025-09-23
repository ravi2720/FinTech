using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.FundRequests;
using FinTech_ApiPanel.Domain.Shared.Pagination;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.FundManagements
{
    public class GetAllFundRequestsQuery :PaginationFilter, IRequest<ApiResponse<PagedResponse<FundRequestDto>>>
    {
        public long? UserId { get; set; }
        public byte? Status { get; set; }
    };
}
