using FinTech_ApiPanel.Domain.Shared.Audits;
using System.ComponentModel.DataAnnotations;

namespace FinTech_ApiPanel.Domain.Entities.UserMasters
{
    public class UserMaster : AuditedEntity
    {
        public long Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public bool EmailVerified { get; set; }        
        public byte? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? PAN { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number")]
        public string? PhoneNumber { get; set; }
        public long IPin { get; set; }
        public bool PhoneVerified { get; set; }
        public bool KYCVerified { get; set; }
        public string? Address { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string? ProfilePicture { get; set; } = string.Empty;
        public int LoginAttempts { get; set; }

        //Company information
        public string? GSTIN { get; set; }
        public string? CompanyPAN { get; set; }
        public string? CompanyTradeName { get; set; }
        public string? CompanyLegalName { get; set; }
        public string? BusinessCategory { get; set; }
        public string? CompanyAddress { get; set; }
        public string? CompanyLogo { get; set; }
        public string? OutletId { get; set; }
        public string? Prefix { get; set; }
    }
}
