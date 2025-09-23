using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.BankMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.BankMasters
{
    public record GetBankMasterForDropdownQuery(byte Type) : IRequest<ApiResponse<List<BankDto>>>;
}
