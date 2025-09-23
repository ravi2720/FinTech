namespace FinTech_ApiPanel.Domain.Shared.Enums
{
    public enum FinancialComponenService : byte
    {
        // Commission Services
        AEPS = 0,
        MATM = 1,
        Recharge = 2,
        PAN = 3,
        BBPS = 4,
        Other_Commission = 5,

        // Surcharge Services
        DMT = 6,
        PayOut = 7,
        AadharPay = 8,
        UPITransfer = 9,
        CreditCard = 10,
        Other_Surcharge = 11
    }
}
