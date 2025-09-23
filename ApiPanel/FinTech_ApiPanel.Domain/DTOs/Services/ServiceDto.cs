namespace FinTech_ApiPanel.Domain.DTOs.Services
{
    public class ServiceDto
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public byte Type { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
