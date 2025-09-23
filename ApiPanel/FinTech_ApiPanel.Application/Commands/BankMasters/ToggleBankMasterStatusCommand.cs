using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.BankMasters
{
    public record ToggleBankMasterStatusCommand(long Id) : IRequest<ApiResponse>;
}
