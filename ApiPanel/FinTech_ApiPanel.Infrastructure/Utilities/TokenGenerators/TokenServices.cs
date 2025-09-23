using FinTech_ApiPanel.Application.Abstraction.ITokenServices;
using FinTech_ApiPanel.Domain.DTOs.Auth;
using FinTech_ApiPanel.Domain.DTOs.Locations;
using FinTech_ApiPanel.Domain.DTOs.Tokens;
using FinTech_ApiPanel.Domain.DTOs.UserMasters;
using FinTech_ApiPanel.Domain.Entities.Auth;
using FinTech_ApiPanel.Domain.Entities.UserMasters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace FinTech_ApiPanel.Infrastructure.Utilities.TokenGenerators
{
    public class TokenServices : ITokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSettingDto _jwtSettings;
        private const string ApiKey = "3444b35770997f8c65cba44c97fac8e27ffbab9c161b23d0950d2b15e8900ace";
        private readonly HttpClient _httpClient;

        public TokenServices(IHttpContextAccessor httpContextAccessor,
            IOptions<JwtSettingDto> options,
            HttpClient httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = options.Value;
            _httpClient = httpClient;
        }

        public TokenDto GenerateToken(UserMaster user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
                new Claim("role", user.IsAdmin ? "admin" : "user"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expireAt = DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes); // If expiry is in minutes

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expireAt,
                signingCredentials: credentials
            );

            return new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpireAt = expireAt
            };
        }

        public AuthTokenDto GenerateToken(AuthToken authToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, authToken.UserId.ToString()),
                new Claim("role", "user"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("service_id", authToken.ServiceId.ToString())
            };

            var seconds = 5 * 60; // Convert minutes to seconds
            var expireAt = DateTime.Now.AddMinutes(seconds);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expireAt,
                signingCredentials: credentials
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Optionally assign back to authToken if needed for saving
            authToken.AccessToken = accessToken;
            authToken.ExpiresAt = expireAt;

            return new AuthTokenDto
            {
                AccessToken = accessToken,
                TokenType = "Bearer",
                ExpiresIn = seconds,
                IssuedAt = DateTime.Now
            };
        }

        public LoggedInUserDto GetLoggedInUserinfo()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null)
                    return null;

                var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
                if (string.IsNullOrEmpty(authHeader))
                    return null;

                if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    authHeader = authHeader.Substring("Bearer ".Length).Trim();

                var handler = new JwtSecurityTokenHandler();
                if (!handler.CanReadToken(authHeader))
                    return null;

                var jwtToken = handler.ReadJwtToken(authHeader);

                // Claims based on your JWT payload
                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !long.TryParse(userIdClaim, out var userId))
                    return null;

                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
                if (string.IsNullOrEmpty(role))
                    return null;

                return new LoggedInUserDto
                {
                    UserId = userId,
                    Role = role,
                    IsAdmin = role == "admin" ? true : false
                };
            }
            catch (Exception)
            {
                throw new Exception("Error while getting logged in user information.");
            }
        }

        public string? HostIpAddress()
        {
            try
            {
                string hostName = Dns.GetHostName();
                var addresses = Dns.GetHostAddresses(hostName)
                                   .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork)
                                   .ToList();

                return addresses.FirstOrDefault()?.ToString() ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string? RequestIpAddress()
        {
            try
            {
                var context = _httpContextAccessor.HttpContext;
                if (context == null) return null;

                // Check X-Forwarded-For first (proxy support)
                var forwardedIp = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (!string.IsNullOrEmpty(forwardedIp))
                    return forwardedIp;

                // Fallback to RemoteIpAddress
                return context.Connection.RemoteIpAddress?.ToString() ?? null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<LocationDto?> GetLocationDataAsync(string ip)
        {
            var url = $"http://api.ipinfodb.com/v3/ip-city/?key={ApiKey}&ip={ip}&format=json";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var location = JsonSerializer.Deserialize<LocationDto>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return location;
        }
    }
}
