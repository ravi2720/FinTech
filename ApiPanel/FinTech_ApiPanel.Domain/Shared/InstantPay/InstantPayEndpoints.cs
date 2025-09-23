namespace FinTech_ApiPanel.Domain.Shared.InstantPay
{
    public static class InstantPayEndpoints
    {
        public static class Identity
        {
            public const string VerifyBankAccount = "/identity/verifyBankAccount";
        }

        public static class Reports
        {
            public const string TransactionStatus = "/reports/txnStatus";
        }

        public static class Remittance
        {
            public static class DomesticV2
            {
                public const string Transaction = "/fi/remit/out/domestic/v2/transaction";
                public const string RemitterRegistration = "/fi/remit/out/domestic/v2/remitterRegistration";
                public const string RemitterRegistrationVerify = "/fi/remit/out/domestic/v2/remitterRegistrationVerify";
                public const string RemitterKyc = "/fi/remit/out/domestic/v2/remitterKyc";
                public const string BioAuthTransaction = "/fi/remit/out/domestic/v2/bioAuthTransaction";
                public const string RemitterProfile = "/fi/remit/out/domestic/v2/remitterProfile";
                public const string GenerateTransactionOtp = "/fi/remit/out/domestic/v2/generateTransactionOtp";
                public const string BeneficiaryRegistration = "/fi/remit/out/domestic/v2/beneficiaryRegistration";
                public const string BeneficiaryRegistrationVerify = "/fi/remit/out/domestic/v2/beneficiaryRegistrationVerify";
                public const string BeneficiaryDeleteVerify = "/fi/remit/out/domestic/v2/beneficiaryDeleteVerify";
                public const string BeneficiaryDelete = "/fi/remit/out/domestic/v2/beneficiaryDelete";
            }
        }

        public static class AEPS
        {
            public const string OutletLogin = "/fi/aeps/outletLogin";
            public const string OutletLoginStatus = "/fi/aeps/outletLoginStatus";
            public const string MiniStatement = "/fi/aeps/miniStatement";
            public const string CashWithdrawal = "/fi/aeps/cashWithdrawal";
            public const string CashDeposit = "/fi/aeps/cashDeposit";
            public const string BalanceInquiry = "/fi/aeps/balanceInquiry";
            public const string AEPSBanks = "/fi/aeps/banks";
        }

        public static class Banks
        {
            public const string RemittanceBanks = "/fi/remit/out/domestic/v2/banks";
            public const string BankList = "/identity/verifyBankAccount/banks";
        }

        public static class UserOutlet
        {
            public const string SignupValidate = "/outlet/signup/validate";
            public const string SignupInitiate = "/outlet/signup/initiate";
        }
    }


}
