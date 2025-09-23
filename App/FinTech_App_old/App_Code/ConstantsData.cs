using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Weborb.Message;

/// <summary>
/// Summary description for ConstantsData
/// </summary>
public class ConstantsData
{
    public static int OfflineAPIID { get; set; } = 4;
    //public static bool IsSession { get; set; }
    public static string CompanyName = "PNK InfoTech Private Limited";
    public static string PackageMessage = "Package Update Successfully";
    public static string SMSCategory = "SMSCategory Update Successfully";
    public static string SMSCategoryActive = "SMSCategory Active/DeActive Successfully";
    public static string RedirectUrlM = "Default.aspx";


    //TDS Part
    public static bool _TDS { get; set; } = false;
    public static decimal _TDSVal { get; set; } = 2;  


    //ICON Path
    public static string ActiveIcon = "../images/Active.png";
    public static string DeActiveIcon = "../images/Delete.png";
    public static string DeleteIcon = "../images/DeleteIcon.png";
    public static string EditIcon = "../images/Edit.png";
    public static string EyeOpen = "../images/eyeopen.png";
    public static string EyeClose = "../images/eyeclose.png";
    public static string NoImage = "NoImage.png";
    public static string SMS = "../images/sms.png";
    public static string kycA = "../images/vkyca.png";
    public static string kycP = "../images/vkycp.png";
    public static string gmail = "../images/gmail.png";
    public static string lockM = "../images/lock.png";
    public static string unlocked = "../images/unlocked.png";
    public static string checkstatus = "../images/checkstatus.png";
    public static string fs = "../images/forcesuccess.png";
    public static string News = "../images/news.png";
    public static string plan = "../images/plan.png";



    //Page ID
    public static Int32 Role = 1;
    public static Int32 Package = 2;
    public static Int32 AssignRole = 3;
    public static Int32 Service = 4;
    public static Int32 AepsCommission = 5;
    public static Int32 MATMCommission = 6;
    public static Int32 AadharSurcharge = 7;
    public static Int32 DMTSurcharge = 8;
    public static Int32 PayoutSurcharge = 9;
    public static Int32 UPISurcharge = 10;
    public static Int32 UPITransferSurcharge = 11;
    public static Int32 VirtualSurcharge = 12;
    public static Int32 RechargeCommission = 13;
    public static Int32 CashDepositCommission = 14;
    public static Int32 MiniStatement = 15;
    public static Int32 DMTHistory = 16;
    public static Int32 AEPSHistory = 17;
    public static Int32 Payout = 18;
    public static Int32 UPI = 19;
    public static Int32 UPITransfer = 20;
    public static Int32 Recharge = 21;
    public static Int32 MATM = 22;
    public static Int32 Pan = 23;
    public static Int32 AadharPay = 24;
    public static Int32 FundRequestPage = 25;
    public static Int32 AddFund = 26;
    public static Int32 DeductFund = 27;
    public static Int32 AddAEPSFund = 28;
    public static Int32 DeductAEPSFund = 29;
    public static Int32 MemberReport = 30;

    //Amount Limit
    public static Int32 AepsSetLimitLow = 100;
    public static Int32 AepsSetLimitHigh = 10000;
    public static Int32 DMTSetLimitLow = 9;
    public static Int32 DMTSetLimitHigh = 500000;
    public static Int32 MATMSetLimit = 10000;
    public static Int32 AadharSetLimit = 10000;


    //Payment Array
    public static string[] PaymentType = new string[] { "IMPS", "NEFT" };

    //ServiceCategory 
    public static Int32 RechargeServiceCategory = 1;
    public static Int32 BBPSServiceCategory = 2;
    public static Int32 BankingServiceCategory = 3;
    public static Int32 TravelServiceCategory = 4;


    //Payment Mode
    public static string[] PaymentMode = new string[] { "Select Payment Mode", "NEFT/IMPS/RTGS", "Cheque/DD", "Cash", "UPI" };

    //SMS ID
    public static Int32 Admin_Login_OTP_SMS = 1;
    public static Int32 Admin_Fund_Add_OTP_SMS = 2;
    public static Int32 Admin_Deduct_Fund_OTP_SMS = 3;
    public static Int32 Registration_SMS = 4;
    public static Int32 Login_OTP_SMS = 5;
    public static Int32 Transation_OTP_SMS = 6;
    public static Int32 Forgot_Password_SMS = 7;
    public static Int32 Bank_Add_OTP_SMS = 8;
    public static Int32 Sign_Up_OTP_SMS = 9;
    public static Int32 FundRequest = 10;
    public static Int32 FundApprove = 11;
    public static Int32 KYCReject = 12;
    public static Int32 KYCApprove = 13;
    public static Int32 LoginOtherDevice = 14;


    //ServiceID
    public static Int32 SRecharge = 1;
    public static Int32 SPostPaid = 2;
    public static Int32 SDTH = 3;
    public static Int32 SElectricity = 4;
    public static Int32 SWater = 5;
    public static Int32 SGas = 6;
    public static Int32 SGasBooking = 7;
    public static Int32 SInsurance = 8;
    public static Int32 SInternet = 9;
    public static Int32 SLandline = 10;
    public static Int32 SEmi = 11;
    public static Int32 SFastTag = 12;
    public static Int32 SEducation = 13;
    public static Int32 SCableTv = 14;
    public static Int32 SMunicipalTax = 15;
    public static Int32 SMoneyTransfer = 16;
    public static Int32 SAeps = 17;
    public static Int32 SAadharPay = 18;
    public static Int32 SUPI = 19;
    public static Int32 SUPITransfer = 20;
    public static Int32 SPanCard = 21;
    public static Int32 SMatm = 22;
    public static Int32 SSettlement = 23;
    public static Int32 SBus = 24;
    public static Int32 SFlight = 25;
    public static Int32 SIrctc = 26;
    public static Int32 SOffline = 35;
    public static Int32 SVan = 36;
    public static Int32 SFundAdd = 101;
    public static Int32 SRCommission = 102;
    public static Int32 SServiceActive = 103;
    public static Int32 SAEPSCommission = 104;
    public static Int32 SAEPSOnBoard = 105;
    public static Int32 SAccountVerfication = 106;
    public static Int32 SDMTCommission = 107;
    public static Int32 SMATMCommission = 108;
    public static Int32 SMemberPage = 109;
    public static Int32 SAadharCommission = 110;
    public static Int32 SUPICommission = 111;
    public static Int32 SPlanPurchase= 109;

    //Email ID
    public static string RegistrationEmailTemplate = "welcome.htm";
    public static string ForgetmailTemplate = "Forget.htm";
    public static string TransactionTemplate = "Transaction.htm";
    public static string VerifyTemplate = "Verify.htm";
    public static string AccountHoldTemplate = "AccountHold.htm";
    public static string AccountActiveTemplate = "AccountActive.htm";


    public static string getipAddress()
    {
        string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        return ipAddress;
    }





    public static void Clear(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).ClearSelection();
            else if (ctrl is Repeater)
            {
                ((Repeater)ctrl).Dispose();
                ((Repeater)ctrl).DataBind();
            }


            Clear(ctrl.Controls);
        }
    }
    public static string SerializeToJSon(DataTable dt)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        List<Dictionary<string, object>> dataRows = new List<Dictionary<string, object>>();
        dt.Rows.Cast<DataRow>().ToList().ForEach(dataRow =>
        {
            var row = new Dictionary<string, object>();
            dt.Columns.Cast<DataColumn>().ToList().ForEach(column =>
            {
                row.Add(column.ColumnName, dataRow[column]);
            });
            dataRows.Add(row);
        });
        return ser.Serialize(dataRows);
    }

    public static DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }

    public static string GetBranchCode(string PinCode)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        var client = new RestClient("https://api.postalpincode.in/pincode/"+ PinCode);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", "", ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }





    public ConstantsData()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}