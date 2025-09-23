using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.FinancialComponents;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.UserFinancialComponents
{
    public record GetUserFinancialComponentByIdQuery(long Id) : IRequest<ApiResponse<FinancialComponentDto>>;
}
