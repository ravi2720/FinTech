namespace FinTech_ApiPanel.Domain.DTOs.UserMasters
{
    public class UserBankDto
    {
        public long Id { get; set; }
        public string BankName { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string IFSCCode { get; set; } = string.Empty;
        public string AccountHolderName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
    }
}
