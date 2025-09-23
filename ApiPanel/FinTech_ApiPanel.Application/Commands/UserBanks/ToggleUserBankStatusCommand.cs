using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.UserBanks
{
    public record ToggleUserBankStatusCommand(long Id) : IRequest<ApiResponse>;
}
