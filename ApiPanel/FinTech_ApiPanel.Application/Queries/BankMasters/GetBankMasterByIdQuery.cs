using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.Entities.BankMasters;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.BankMasters
{
    public record GetBankMasterByIdQuery(long Id) : IRequest<ApiResponse<BankMaster>>;
}
