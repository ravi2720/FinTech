using FinTech_ApiPanel.Application.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FinTech_ApiPanel.Application.Commands.UserMasters
{
    public class ResetPasswordCommand : IRequest<ApiResponse>
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
