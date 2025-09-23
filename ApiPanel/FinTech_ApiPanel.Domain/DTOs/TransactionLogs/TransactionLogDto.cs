using FinTech_ApiPanel.Domain.Shared.Audits;

namespace FinTech_ApiPanel.Domain.DTOs.TransactionLogs
{
    public class TransactionLogDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string? RefUser { get; set; }
        public string? ReferenceId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public byte? AuditType { get; set; }
        public byte Status { get; set; }
        public string? Remark { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? AEPSType { get; set; }

        //Ipay Specific
        public string? OrderId { get; set; }
        public DateTime? Timestamp { get; set; }
        public string? OutletId { get; set; } 
    }
}
