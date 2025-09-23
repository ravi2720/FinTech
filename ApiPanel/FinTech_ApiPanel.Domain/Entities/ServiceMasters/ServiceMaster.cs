namespace FinTech_ApiPanel.Domain.Entities.ServiceMasters
{
    public class ServiceMaster
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public byte Type { get; set; }
        public bool IsActive { get; set; }
    }
}
