using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.FinancialComponents;
using MediatR;

namespace FinTech_ApiPanel.Application.Queries.UserFinancialComponents
{
    public record GetAllUserFinancialComponentQuery(long UserId, byte Type, byte ServiceType) : IRequest<ApiResponse<List<FinancialComponentDto>>>;
}
