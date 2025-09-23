using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using FinTech_ApiPanel.Domain.Shared.Pagination;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.UserMaster
{
    public class GetAllUsersQuery : PaginationFilter, IRequest<ApiResponse<PagedResponse<UserMasterListDto>>>
    {
        public string? Title { get; set; }
    }

}
