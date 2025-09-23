using FinTech_ApiPanel.Domain.Shared.Audits;

namespace FinTech_ApiPanel.Domain.Entities.Auth
{
    public class AuthToken : AuditedEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;

        public long ServiceId { get; set; }

        public string AccessToken { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }
    }
}
