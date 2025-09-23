namespace FinTech_ApiPanel.Domain.DTOs.Tokens
{
    public class TokenDto
    {
        public string Token { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
