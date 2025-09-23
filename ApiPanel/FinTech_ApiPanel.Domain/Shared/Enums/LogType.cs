namespace FinTech_ApiPanel.Domain.Shared.Enums
{
    public enum LogType
    {
        // Authentication & User Actions
        OutletLogin_IPay = 2,

        // Fund Management
        AddFund = 3,
        DeductFund = 4,
        HoldFund = 5,
        ReleaseFund = 6,
        PayIn = 7,
        Payout = 8,
        FundRequest = 9,
        AcceptFundRequest = 66,
        RejectFundRequest = 67,

        // AEPS
        AEPOnboardInitiate_IPay = 17,
        AEPSOnboardValidate_IPay = 16,
        AEPSOnboarding_Surcharge = 32,
        AEPSWithdrawal = 10,
        AEPSWithdrawal_IPay = 11,
        AEPSDeposit = 12,
        AEPSDeposit_IPay = 13,
        AEPSSettlement = 14,
        AEPSSettlement_IPay = 15,
        AEPSBalanceEnquiry_IPay = 18,
        AEPSMiniStatement_IPay = 19,
        AEPS_MiniStatement_Commission = 22,
        AEPSCommission = 21,

        // DMT - Remitter
        Outlet_Login_Surcharge = 33,
        Verify_BankAccount_Surcharge = 35,
        RemitterRegistration_IPay = 23,
        RemitterRegistrationVerify_IPay = 24,
        RemitterKyc_IPay = 25,
        Remitter_KYC_Surcharge = 34,
        DMTTransactionOtp_IPay = 27,
        BioRemitTxn = 28,
        DMTTransaction = 29,
        DMTTransaction_IPay = 30,
        DMTSurcharge = 31,

        // DMT - Beneficiary
        BeneficiaryRegistration_IPay = 36,
        BeneficiaryRegistrationVerify = 37,
        BeneficiaryRegistrationVerify_IPay = 38,
        BeneficiaryDelete_IPay = 39,
        BeneficiaryDeleteVerify_IPay = 40,

        // PAN / Aadhaar / Identity
        PANOnboard = 41,
        PanToken = 42,
        PanVerification = 43,
        NSDLPan = 44,
        AadhaarVerification = 45,
        UTIPAN = 46,

        // Services
        Recharge = 47,
        Recharge_Goter = 68,
        BillPayment = 48,
        BillPayment_Goter = 70,
        BillPayment_Surcharge = 71,
        UPI = 49,

        // Financial Adjustments
        Refund = 50,

        // Commission
        MATM_Commission = 51,
        Recharge_Commission = 52,
        PAN_Commission = 53,
        BBPS_Commission = 54,

        // Surcharge
        PayOut_Surcharge = 55,
        Aadhar_PaySurcharge = 56,
        UPI_Surcharge = 57,
        CreditCard_Surcharge = 58,

        // Status & Disputes
        TxnStatus = 59,
        Dispute = 60,

        // Miscellaneous
        DMTPPI = 61,
        AadharVerification = 62,
        TDSGST = 63,
        AadharPay = 64,
        BankAccountVerification_IPay = 65,
        Other = 100
    }
}