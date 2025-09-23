using FinTech_ApiPanel.Application.Common;
using FinTech_ApiPanel.Domain.DTOs.Tokens;
using MediatR;

namespace FinTech_ApiPanel.Application.Commands.UserMasters
{
    public class LoginUserCommand : IRequest<ApiResponse<TokenDto>>
    {
        public string UserNameOrEmail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}
