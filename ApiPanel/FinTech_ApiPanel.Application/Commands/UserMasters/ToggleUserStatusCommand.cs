using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.UserMasters
{
    public record ToggleUserStatusCommand(long Id) : IRequest<ApiResponse>;
}
