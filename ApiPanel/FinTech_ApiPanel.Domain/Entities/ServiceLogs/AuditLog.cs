using FinTech_ApiPanel.Domain.Shared.Audits;

namespace FinTech_ApiPanel.Domain.Entities.ServiceLogs
{
    public class AuditLog : AuditedEntity
    {
        public long Id { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public decimal EndPointIP { get; set; }
        public string Remarks { get; set; } = string.Empty;
    }
}
