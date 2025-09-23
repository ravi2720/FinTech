using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.UserMasters
{
    public record ChangeIpin_SendOTPCommand : IRequest<ApiResponse<object>>;
}
