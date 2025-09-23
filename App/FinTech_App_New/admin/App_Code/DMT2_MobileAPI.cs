using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for DMT2_MobileAPI
/// </summary>
public class DMT2_MobileAPI
{
    private static string API_URL = "https://cyrusrecharge.in/api/CyrusDMRAPI.aspx";
    private static string MerchantID = "";
    private static string MerchantKey = "";
    public static decimal Beneficiary_VerificationFee = Convert.ToDecimal(1);
    public static string DMR_Limit = "25,000";

    public DMT2_MobileAPI()
    {
        DataTable dtAPI = new DataTable();
        cls_connection objConnection = new cls_connection();
        dtAPI = objConnection.select_data_dt("select * from API_SettingKey where id=1");
        if (dtAPI.Rows.Count == 1)
        {
            MerchantID = dtAPI.Rows[0]["APIID"].ToString().Trim();
            MerchantKey = dtAPI.Rows[0]["APIPIN"].ToString().Trim();
        }
    }

    public string Getcustomerdetails(string MOBILENO)
    {
        var Values = new NameValueCollection();
        Values["MOBILENO"] = MOBILENO;
        Values["MerchantID"] = DMT2_MobileAPI.MerchantID;
        Values["MerchantKey"] = DMT2_MobileAPI.MerchantKey;
        Values["MethodName"] = "getcustomerdetails";
        DMTPaytm  ObjDMT2 = new DMTPaytm();
        var response = "";// ObjDMT2.Getcustomerdetails(Values);
        return response;
    }

    public string SentOTP(string MOBILENO)
    {
        var Values = new NameValueCollection();
        Values["MOBILENO"] = MOBILENO;
        Values["MerchantID"] = DMT2_MobileAPI.MerchantID;
        Values["MerchantKey"] = DMT2_MobileAPI.MerchantKey;
        Values["MethodName"] = "sendotp";
        DMTPaytm ObjDMT2 = new DMTPaytm();
        var response = "";// ObjDMT2.Getcustomerdetails(Values);
        return response;
    }
    public string BankList()
    {
        var Values = new NameValueCollection();
        Values["MerchantID"] = DMT2_MobileAPI.MerchantID;
        Values["MerchantKey"] = DMT2_MobileAPI.MerchantKey;
        Values["MethodName"] = "banklist";
        DMTPaytm ObjDMT2 = new DMTPaytm();
        var response = "";// ObjDMT2.Getcustomerdetails(Values);
        return response;
    }
    public string GetBeneficiaryDetails(string MOBILENO)
    {
        var Values = new NameValueCollection();
        Values["MobileNo"] = MOBILENO;
        Values["MerchantID"] = DMT2_MobileAPI.MerchantID;
        Values["MerchantKey"] = DMT2_MobileAPI.MerchantKey;
        Values["MethodName"] = "getbeneficiarydetails";
        DMTPaytm ObjDMT2 = new DMTPaytm();
        var response = "";// ObjDMT2.Getcustomerdetails(Values);
        return response;
    }

    public string RemoveBeneficiaryAccount(string BENEFICIARYID)
    {
        var Values = new NameValueCollection();
        Values["BENEFICIARYID"] = BENEFICIARYID;
        Values["MerchantID"] = DMT2_MobileAPI.MerchantID;
        Values["MerchantKey"] = DMT2_MobileAPI.MerchantKey;
        Values["MethodName"] = "Removebeneficiaryaccount";
        DMTPaytm ObjDMT2 = new DMTPaytm();
        var response = ObjDMT2.DeletBeneficiaryAccount(Values);
        return response;
    }

    public string AddBeneficiary(string CustomerMobileNo, string BankId, string AccountNo, string MobileNo, string Name, string IFSC)
    {
        var Values = new NameValueCollection();
        Values["CustomerMobileNo"] = CustomerMobileNo;
        Values["MerchantID"] = DMT2_MobileAPI.MerchantID;
        Values["MerchantKey"] = DMT2_MobileAPI.MerchantKey;
        Values["MethodName"] = "addbeneficiary";
        Values["BankId"] = BankId;
        Values["AccountNo"] = AccountNo;
        Values["MobileNo"] = MobileNo;
        Values["Name"] = Name;
        Values["IFSC"] = IFSC;
        DMTPaytm ObjDMT2 = new DMTPaytm();
        var response = "";// ObjDMT2.AddBeneficiary(Values);
        return response;
    }
    public string CustomerRegistration(string FNAME, string LNAME, string OTP, string DOB, string PINCODE, string MOBILENO, string ADDRESS)
    {
        var Values = new NameValueCollection();
        Values["MerchantID"] = DMT2_MobileAPI.MerchantID;
        Values["MerchantKey"] = DMT2_MobileAPI.MerchantKey;
        Values["MethodName"] = "customerregistration";
        Values["FNAME"] = FNAME;
        Values["LNAME"] = LNAME;
        Values["OTP"] = OTP;
        Values["DOB"] = DOB;
        Values["PINCODE"] = PINCODE;
        Values["MOBILENO"] = MOBILENO;
        Values["ADDRESS"] = ADDRESS;
        DMTPaytm ObjDMT2 = new DMTPaytm();
        var response = "";// ObjDMT2.CustomerRegistration(Values);
        return response;
    }
    public string FundTransfer(string CustomerMobile, string amount, string beneficiaryIFSC, string beneficiaryAccount, string orderId)
    {
        var Values = new NameValueCollection();
        Values["CustomerMobile"] = CustomerMobile;
        Values["MerchantID"] = DMT2_MobileAPI.MerchantID;
        Values["MerchantKey"] = DMT2_MobileAPI.MerchantKey;
        Values["MethodName"] = "sendmoney";
        Values["amount"] = amount;
        Values["beneficiaryIFSC"] = beneficiaryIFSC;
        Values["beneficiaryAccount"] = beneficiaryAccount;
        Values["comments"] = "";
        Values["orderId"] = orderId;
        DMTPaytm ObjDMT2 = new DMTPaytm();
        var response = "";// ObjDMT2.SendAmount(Values);
        return response;
    }
    public string CheckStatus(string orderId)
    {
        var Values = new NameValueCollection();
        Values["MerchantID"] = DMT2_MobileAPI.MerchantID;
        Values["MerchantKey"] = DMT2_MobileAPI.MerchantKey;
        Values["MethodName"] = "checkstatus";
        Values["orderId"] = orderId;
        DMTPaytm ObjDMT2 = new DMTPaytm();
        var response = "";// ObjDMT2.CheckStatus(Values);
        return response;
    }
    public string BeneficiaryAccount_Verification(string orderId, string CustomerMobileNo, string beneficiaryIFSC, string beneficiaryAccount)
    {
        var Values = new NameValueCollection();
        Values["CustomerMobileNo"] = CustomerMobileNo;
        Values["MerchantID"] = DMT2_MobileAPI.MerchantID;
        Values["MerchantKey"] = DMT2_MobileAPI.MerchantKey;
        Values["MethodName"] = "beneficiaryaccount_verification";
        Values["beneficiaryIFSC"] = beneficiaryIFSC;
        Values["beneficiaryAccount"] = beneficiaryAccount;
        Values["orderId"] = orderId;
        DMTPaytm ObjDMT2 = new DMTPaytm();
        var response = ObjDMT2.BeneficiaryAccount_Verification(Values);
        return response;
    }
    private string APICall(string URI, NameValueCollection values)
    {
        try
        {
            using (var client = new WebClient())
            {
                var response = client.UploadValues(DMT2_MobileAPI.API_URL, values);
                var responseString = Encoding.Default.GetString(response);
                return responseString;
            }
        }
        catch (Exception ex)
        {
            return "{\"statuscode\":\"ERR\",\"status\":\"Server Downtime. Try After Some Time!\",\"data\":\"\"}";
        }
    }
    private string Transfer_APICall(string URI, NameValueCollection values)
    {
        cls_connection cls = new cls_connection();
        try
        {

            using (var client = new WebClient())
            {
                var response = client.UploadValues(DMT2_MobileAPI.API_URL, values);
                var responseString = Encoding.Default.GetString(response);
                //try
                //{
                //    cls.insert_data("insert into tblTrackRecharge_New(URL,Result,Status) Values('" + URI + "','" + responseString + "','Hit')");
                //}
                //catch { }
                return responseString;
            }
        }
        catch (Exception ex)
        {
            //try
            //{
            //    cls.insert_data("insert into tblTrackRecharge_New(URL,Result,Status) Values('" + URI + "','" + ex.Message.Replace("'", "") + "','Error')");
            //}
            //catch { }
            return "{\"statuscode\":\"ERR\",\"status\":\"Server Downtime. Try After Some Time!\",\"data\":\"\"}";
        }
    }

}