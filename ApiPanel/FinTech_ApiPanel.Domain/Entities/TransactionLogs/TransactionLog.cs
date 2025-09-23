using FinTech_ApiPanel.Domain.Shared.Audits;

namespace FinTech_ApiPanel.Domain.Entities.TransactionLogs
{
    public class TransactionLog : AuditedEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long? RefUserId { get; set; }
        public string? ReferenceId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? RemainingAmount { get; set; }
        public byte? AuditType { get; set; }
        public byte? LogType { get; set; }
        public byte Status { get; set; }
        public string? Remark { get; set; }
        public string? EndPointIP { get; set; }
        public string? CaptureType { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public bool WalletUpdated { get; set; } = false;

        //Ipay Specific
        public string? Ipay_OrderId { get; set; }
        public string? Ipay_StatusCode { get; set; }
        public string? Ipay_ActCode { get; set; }
        public string? Ipay_Uuid { get; set; }
        public DateTime? Ipay_Timestamp { get; set; }
        public string? Ipay_Environment { get; set; }
        public string? Ipay_OutletId { get; set; } = string.Empty;
        public string? Ipay_Latitude { get; set; }
        public string? Ipay_Longitude { get; set; }
        public string? Ipay_Response { get; set; }
    }
}