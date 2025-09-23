using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.FinancialComponents;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.FinancialComponentMasters
{
    public record GetFinancialComponentByIdQuery(long Id) : IRequest<ApiResponse<FinancialComponentDto>>;
}
