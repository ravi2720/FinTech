namespace FinTech_ApiPanel.Domain.DTOs.Dashboard
{
    public class AdminDashboardDto
    {
        public AdminDashboardDto()
        {
            RecentlyUsed = new List<RecentlyUsed>();
        }

        // Tab1
        public decimal WalletBalance { get; set; }
        public decimal UserTotalBalance { get; set; }
        public decimal TotalIn { get; set; }
        public decimal TotalOut { get; set; }
        public List<RecentlyUsed> RecentlyUsed { get; set; }

        //Tab2
        public decimal OpeningBalance { get; set; }
        public decimal Closingbalance { get; set; }
        public decimal FundRequest { get; set; }
        public decimal FundAdd { get; set; }
        public decimal FundDeduct { get; set; }
        public decimal CashDeposit { get; set; }
        public decimal TotalAEPS { get; set; }
        public decimal TodaysDMT { get; set; }
        public decimal TotalRecharge { get; set; }
        public decimal TotalPayout { get; set; }
        public decimal TodaysMATM { get; set; }
        public decimal TotalAadharPay { get; set; }
        public decimal TotalUPI { get; set; }
        public decimal TotalPAN { get; set; }
        public decimal UPICashout { get; set; }
        public decimal CreditCard { get; set; }
        public decimal VAN { get; set; }
        public decimal TotalDMTPPI { get; set; }
        public decimal TotalWalletLoad { get; set; }
    }

    public class RecentlyUsed
    {
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}