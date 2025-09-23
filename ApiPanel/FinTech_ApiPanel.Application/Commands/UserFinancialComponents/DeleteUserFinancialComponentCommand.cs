using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.UserFinancialComponents
{
    public record DeleteUserFinancialComponentCommand(long Id) : IRequest<ApiResponse>;
}
