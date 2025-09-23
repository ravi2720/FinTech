using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.FinancialComponentMasters
{
    public record DeleteFinancialComponentCommand(long Id) : IRequest<ApiResponse>;
}
