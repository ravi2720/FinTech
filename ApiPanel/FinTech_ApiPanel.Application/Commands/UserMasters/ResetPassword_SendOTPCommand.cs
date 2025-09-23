using FinTech_ApiPanel.Application.Common;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.UserMasters
{
    public record ResetPassword_SendOTPCommand(string Email) : IRequest<ApiResponse<object>>;
}
