using FinTech_ApiPanel.Application.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FinTech_ApiPanel.Application.Commands.UserMasters
{
    public class ChangeIpin_VerifyOTPCommand : IRequest<ApiResponse<object>>
    {
        [Required]
        public long OTP { get; set; }
        [Required]
        public string Code { get; set; }
    }
}
