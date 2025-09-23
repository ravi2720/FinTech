using FinTech_ApiPanel.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FinTech_ApiPanel.Application.Commands.UserMasters
{
    public class UpdateUserCommand : IRequest<ApiResponse>
    {
        public long Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public string? PAN { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public IFormFile? Profile { get; set; }

        //Company information
        public string? GSTIN { get; set; }
        public string? CompanyPAN { get; set; }
        public string? CompanyTradeName { get; set; }
        public string? CompanyLegalName { get; set; }
        public string? BusinessCategory { get; set; }
        public string? CompanyAddress { get; set; }
        public IFormFile? logo { get; set; }
    }
}