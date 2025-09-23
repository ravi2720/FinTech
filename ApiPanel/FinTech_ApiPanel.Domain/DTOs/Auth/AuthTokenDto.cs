using System.Text.Json.Serialization;

namespace FinTech_ApiPanel.Domain.DTOs.Auth
{
    public class AuthTokenDto
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; } = string.Empty;

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; } = "Bearer";

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; } // in seconds

        // Optional fields
        [JsonPropertyName("issued_at")]
        public DateTime? IssuedAt { get; set; }
    }
}
