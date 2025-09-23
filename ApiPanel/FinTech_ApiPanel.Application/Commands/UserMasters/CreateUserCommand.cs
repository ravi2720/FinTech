using FinTech_ApiPanel.Application.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FinTech_ApiPanel.Application.Commands.UserMasters
{
    public class CreateUserCommand : IRequest<ApiResponse>
    {
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public byte? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? PAN { get; set; } = string.Empty;
    }
}
