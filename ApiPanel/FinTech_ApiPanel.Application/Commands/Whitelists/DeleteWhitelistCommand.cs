using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.Whitelists
{
    public record DeleteWhitelistCommand(long Id) : IRequest<ApiResponse>;
}
