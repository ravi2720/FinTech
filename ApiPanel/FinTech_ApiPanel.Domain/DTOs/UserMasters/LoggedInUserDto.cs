using System.ComponentModel.DataAnnotations;

namespace FinTech_ApiPanel.Domain.DTOs.UserMasters
{
    public class LoggedInUserDto
    {
        public long UserId { get; set; }
        public string Role { get; set; }
        public bool IsAdmin { get; set; }
    }
}
