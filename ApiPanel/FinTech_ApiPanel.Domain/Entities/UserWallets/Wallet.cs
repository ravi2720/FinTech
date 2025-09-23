using FinTech_ApiPanel.Domain.Shared.Audits;

namespace FinTech_ApiPanel.Domain.Entities.UserWallets
{
    public class Wallet : AuditedEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public decimal TotalBalance { get; set; }   
        public decimal HeldAmount { get; set; }
    }
}