using FinTech_ApiPanel.Domain.Shared.Audits;

namespace FinTech_ApiPanel.Domain.Entities.FundRequests
{
    public class FundRequest : AuditedEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long BankId { get; set; }

        public decimal Amount { get; set; }
        public string? AdminRemark { get; set; }
        public string? UserRemark { get; set; }

        public string ReferenceId { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public byte ModeOfPayment { get; set; }
        public byte Status { get; set; }
        public string Attachment { get; set; } = string.Empty;
    }
}
