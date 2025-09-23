using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.BankMasters;
using FinTech_ApiPanel.Domain.Shared.Pagination;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.BankMasters
{
    public class GetAllBankMasterQuery : IRequest<ApiResponse<List<BankMaster>>>
    {
        public string? Title { get; set; }
        public string? AccountNumber { get; set; }
    }
}
