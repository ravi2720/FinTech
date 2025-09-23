using FinTech_ApiPanel.Domain.Shared.Audits;

namespace FinTech_ApiPanel.Domain.Entities.Whitelists
{
    public class WhitelistEntry : AuditedEntity
    {
        public long Id { get; set; }
        public byte EntryType { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}