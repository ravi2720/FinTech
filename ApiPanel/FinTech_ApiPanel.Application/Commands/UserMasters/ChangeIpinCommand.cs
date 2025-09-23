using FinTech_ApiPanel.Application.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FinTech_ApiPanel.Application.Commands.UserMasters
{
    public class ChangeIpinCommand : IRequest<ApiResponse>
    {
        [Required]
        public string Token { get; set; }
        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "New IPIN must be exactly 6 digits.")]
        public long NewIpin { get; set; }
    }
}
