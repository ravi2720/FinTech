namespace FinTech_ApiPanel.Domain.Shared.Enums
{
    public enum FinancialComponenSubService : byte
    {
        // Commission
        Mini_Statement = 0,

        // Surcharge
        Merchant_Onboarding = 1,
        Outle_Login = 2,
        Remitter_KYC = 3,
        Verify_BankAccount = 4,
        Prepaid = 5,
        Postpaid = 6,
        DTH = 7,
    }
}
