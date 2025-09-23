namespace FinTech_ApiPanel.Domain.Entities.Auth
{
    public class Client
    {
        public long UserId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string EncryptionKey { get; set; }
    }
}
