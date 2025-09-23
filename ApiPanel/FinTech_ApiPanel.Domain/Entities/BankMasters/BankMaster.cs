using FinTech_ApiPanel.Domain.Shared.Audits;

namespace FinTech_ApiPanel.Domain.Entities.BankMasters
{
    public class BankMaster : AuditedEntity
    {
        public long Id { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public byte Type { get; set; }
        public string IFSCCode { get; set; } = string.Empty;
        public string AccountHolderName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string UPIHandle { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}