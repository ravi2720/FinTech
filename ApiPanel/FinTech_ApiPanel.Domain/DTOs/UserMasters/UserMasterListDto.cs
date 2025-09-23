using FinTech_ApiPanel.Domain.DTOs.BankMasters;
using FinTech_ApiPanel.Domain.DTOs.Services;
using FinTech_ApiPanel.Domain.Entities.ServiceMasters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTech_ApiPanel.Domain.DTOs.UserMasters
{
    public class UserMasterListDto
    {
        public UserMasterListDto()
        {
            UserBanks = new List<BankDto>();
        }
        public long Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public bool EmailVerified { get; set; }
        public byte? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? PAN { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool PhoneVerified { get; set; }
        public bool KYCVerified { get; set; }
        public string? Address { get; set; }
        public string? ProfilePicture { get; set; } = string.Empty;
        public decimal WalletBalance { get; set; } 
        public decimal HoldAmount { get; set; }
        public bool IsActive { get; set; }

        //Company information
        public string? GSTIN { get; set; }
        public string? CompanyPAN { get; set; }
        public string? CompanyTradeName { get; set; }
        public string? CompanyLegalName { get; set; }
        public string? BusinessCategory { get; set; }
        public string? CompanyAddress { get; set; }
        public string? CompanyLogo { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<BankDto> UserBanks { get; set; }
        public List<ServiceDto> Services { get; set; }
    }
}
