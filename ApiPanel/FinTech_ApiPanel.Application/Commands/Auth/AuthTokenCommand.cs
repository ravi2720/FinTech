using FinTech_ApiPanel.Domain.DTOs.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FinTech_ApiPanel.Application.Commands.Auth
{
    public class AuthTokenCommand : IRequest<AuthTokenDto>
    {
        [Required]
        [FromForm(Name = "grant_type")]
        public string GrantType { get; set; } // e.g., "client_credentials"

        [FromForm(Name = "client_id")]
        public string ClientId { get; set; }

        [FromForm(Name = "client_secret")]
        public string ClientSecret { get; set; } // Optional for public clients or PKCE

        [FromForm(Name = "service_type")]
        public string Type { get; set; }
    }
}
