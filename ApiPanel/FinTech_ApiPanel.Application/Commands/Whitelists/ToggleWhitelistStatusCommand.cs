using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.Whitelists
{
    public record ToggleWhitelistStatusCommand(long Id) : IRequest<ApiResponse>;
}
