using FinTech_ApiPanel.Domain.DTOs.BankMasters;

namespace FinTech_ApiPanel.Domain.DTOs.FundRequests
{
    public class FundRequestDto
    {
        public FundRequestDto()
        {
            BankInfo = new BankDto();
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public string FullName { get; set; }

        public decimal Amount { get; set; }
        public string? AdminRemark { get; set; }
        public string? UserRemark { get; set; }

        public string ReferenceId { get; set; } = string.Empty;
        public DateTime TransactionDate { get; set; }
        public DateTime Timestamp { get; set; }
        public byte ModeOfPayment { get; set; }
        public byte Status { get; set; }
        public string Attachment { get; set; } = string.Empty;
        public BankDto BankInfo { get; set; }
    }
}
