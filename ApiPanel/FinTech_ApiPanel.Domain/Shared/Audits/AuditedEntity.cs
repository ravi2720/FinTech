namespace FinTech_ApiPanel.Domain.Shared.Audits
{
    public class AuditedEntity
    {
        public long CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
