namespace FinTech_ApiPanel.Domain.DTOs.Whitelists
{
    public class WhitelistDto
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
