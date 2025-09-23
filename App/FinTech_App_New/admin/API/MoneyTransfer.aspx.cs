using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Configuration;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

public partial class API_MoneyTransfer : System.Web.UI.Page
{
    cls_connection objConnection = new cls_connection();
    DataTable dt = new DataTable();
    DMTPaytm ObjMahagramDMT = new DMTPaytm();
    DataTable dtDMTMaha = new DataTable();
    public static string MerchantID = "";
    public static string DMRMerchantKey = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

                NameValueCollection nvc = Request.Form;
                if (nvc["Request"] != null)
                {
                    JObject jObject = JObject.Parse(nvc["Request"]);
                    string DeviceID = Request.Headers["DeviceID"].ToString().Trim();
                    if (!string.IsNullOrEmpty(DeviceID))
                    {
                        string methodName = Convert.ToString(jObject["MethodName"]).Trim().ToLower();


                        #region [ 7. GetSurchargeList ]

                        if (methodName == "getsurchargelist")
                        {
                            if (jObject["UserID"] != null && jObject["Password"] != null && jObject["PackageID"] != null)
                            {
                                try
                                {
                                    string userID = Convert.ToString(jObject["UserID"]).Trim();
                                    string password = Convert.ToString(jObject["Password"]).Trim();
                                    string param1 = Convert.ToString(jObject["PackageID"]).Trim();
                                    //  GetSurchargeMaster(userID, password, param1);
                                    return;
                                }
                                catch
                                {
                                    ErrorMessage("Technical Error");
                                    return;
                                }
                            }
                            else
                            {
                                ErrorMessage("Insufficient Parameter");
                                return;
                            }
                        }

                        #endregion


                        #region [ 11.GETCUSTOMERDETAILSNEW ]

                        if (methodName == "specialgetcustomerdetails")
                        {
                            if (jObject["UserID"] != null && jObject["Password"] != null && jObject["MobileNo"] != null)
                            {
                                string userID = Convert.ToString(jObject["UserID"]).Trim();
                                string password = Convert.ToString(jObject["Password"]).Trim();
                                string MobileNo = Convert.ToString(jObject["MobileNo"]).Trim();
                                string deviceID = DeviceID;
                                string AppType = Convert.ToString(jObject["AppType"]).Trim();
                                GetCustomerDetailsSpecial(userID, password, deviceID, MobileNo);
                            }
                            else
                            {
                                ErrorMessage("Insufficient Parameter");
                            }
                        }
                        #endregion

                        #region [ 12. Customer Registration Special ]
                        else if (methodName == "customerregistrationspecial")
                        {
                            if (jObject["UserID"] != null && jObject["Password"] != null && jObject["Name"] != null && jObject["MobileNo"] != null && jObject["Address"] != null && jObject["Pan"] != null
                                && jObject["Aadhar"] != null && jObject["OTP"] != null)
                            {
                                string userID = Convert.ToString(jObject["UserID"]).Trim();
                                string password = Convert.ToString(jObject["Password"]).Trim();
                                string MobileNo = Convert.ToString(jObject["MobileNo"]).Trim();
                                string Address = Convert.ToString(jObject["Address"]).Trim();
                                string Pan = Convert.ToString(jObject["Pan"]).Trim();
                                string Aadhar = Convert.ToString(jObject["Aadhar"]).Trim();
                                string OTP = Convert.ToString(jObject["OTP"]).Trim();
                                string Name = Convert.ToString(jObject["Name"]).Trim();

                                string deviceID = DeviceID;
                                string AppType = Convert.ToString(jObject["AppType"]).Trim();
                                customerregistrationspecial(userID, password, deviceID, MobileNo, Name, Address, Pan, Aadhar, OTP);
                            }
                            else
                            {
                                ErrorMessage("Insufficient Parameter");
                            }
                        }
                        #endregion


                        #region [ 12.GETBENEFICIARYDETAILS SPECIAL ]

                        else if (methodName == "getbeneficiarydetailsspecial")
                        {
                            if (jObject["UserID"] != null && jObject["Password"] != null && jObject["MobileNo"] != null)
                            {
                                string userID = Convert.ToString(jObject["UserID"]).Trim();
                                string password = Convert.ToString(jObject["Password"]).Trim();
                                string MobileNo = Convert.ToString(jObject["MobileNo"]).Trim();
                                string deviceID = DeviceID;
                                string AppType = Convert.ToString(jObject["AppType"]).Trim();
                                GetBeneficiaryDetailsSpecial(userID, password, deviceID, MobileNo);
                            }
                            else
                            {
                                ErrorMessage("Insufficient Parameter");
                            }
                        }
                        #endregion

                        #region [ 14.ADDBENEFICIARY SPECIAL ]

                        else if (methodName == "addbeneficiaryspecial")
                        {
                            if (jObject["UserID"] != null && jObject["Password"] != null && jObject["senderMobileNo"] != null && jObject["MobileNo"] != null && jObject["Name"] != null && jObject["AccountNo"] != null && jObject["IFSC"] != null)
                            {
                                string userID = Convert.ToString(jObject["UserID"]).Trim();
                                string password = Convert.ToString(jObject["Password"]).Trim();
                                string MobileNo = Convert.ToString(jObject["MobileNo"]).Trim();
                                string senderMobileNo = Convert.ToString(jObject["senderMobileNo"]).Trim();
                                string AccountNo = Convert.ToString(jObject["AccountNo"]).Trim();
                                string Name = Convert.ToString(jObject["Name"]).Trim();
                                string IFSC = Convert.ToString(jObject["IFSC"]).Trim();
                                string deviceID = DeviceID;
                                string AppType = Convert.ToString(jObject["AppType"]).Trim();
                                AddBeneficiarySpecial(userID, password, deviceID, MobileNo, AccountNo, senderMobileNo, Name, IFSC);
                            }
                            else
                            {
                                ErrorMessage("Insufficient Parameter");
                            }
                        }
                        #endregion


                        #region [ 15.VerifyAccount Special ]

                        else if (methodName == "verifyaccountspecial")
                        {
                            if (jObject["UserID"] != null && jObject["Password"] != null && jObject["CustomerMobileNo"] != null && jObject["MobileNo"] != null && jObject["beneficiaryIFSC"] != null && jObject["beneficiaryAccount"] != null)
                            {
                                string userID = Convert.ToString(jObject["UserID"]).Trim();
                                string password = Convert.ToString(jObject["Password"]).Trim();
                                string MobileNo = Convert.ToString(jObject["MobileNo"]).Trim();
                                string CustomerMobileNo = Convert.ToString(jObject["CustomerMobileNo"]).Trim();
                                string beneficiaryIFSC = Convert.ToString(jObject["beneficiaryIFSC"]).Trim();
                                string beneficiaryAccount = Convert.ToString(jObject["beneficiaryAccount"]).Trim();
                                string deviceID = DeviceID;
                                string AppType = Convert.ToString(jObject["AppType"]).Trim();
                                VerifyAccountSpecial(userID, password, deviceID, MobileNo, CustomerMobileNo, beneficiaryIFSC, beneficiaryAccount);
                            }
                            else
                            {
                                ErrorMessage("Insufficient Parameter");
                            }
                        }
                        #endregion

                        #region [ 16. sendmoney Special ]

                        if (methodName == "sendmoneyspecial")
                        {
                            if (jObject["UserID"] != null && jObject["Password"] != null && jObject["CustomerMobile"] != null && jObject["amount"] != null && jObject["beneficiaryIFSC"] != null && jObject["beneficiaryAccount"] != null
                              && jObject["BeneName"] != null && jObject["TransactionPassword"] != null && jObject["BenID"] != null)
                            {
                                try
                                {
                                    string userID = Convert.ToString(jObject["UserID"]).Trim();
                                    string password = Convert.ToString(jObject["Password"]).Trim();
                                    string CustomerMobile = Convert.ToString(jObject["CustomerMobile"]).Trim();
                                    string amount = Convert.ToString(jObject["amount"]).Trim();
                                    string beneficiaryIFSC = Convert.ToString(jObject["beneficiaryIFSC"]).Trim();
                                    string beneficiaryAccount = Convert.ToString(jObject["beneficiaryAccount"]).Trim();
                                    string orderId = System.Guid.NewGuid().ToString().Replace('-', '0');
                                    string BeneName = Convert.ToString(jObject["BeneName"]).Trim();
                                    string BenID = jObject["BenID"].ToString();
                                    DataTable dtMemberMaster = objConnection.select_data_dt("select * from Member where mobile='" + userID + "'");
                                    if (dtMemberMaster.Rows.Count > 0)
                                    {
                                        Int32 HoldCount = objConnection.select_data_scalar_int("select count(*) from Member where Msrno=" + dtMemberMaster.Rows[0]["msrno"].ToString() + " and OnHold=1");
                                        if (HoldCount == 0)
                                        {
                                            if (1 == 1)
                                            {
                                                if (Convert.ToDouble(amount) <= ConstantsData.DMTSetLimitHigh)
                                                {
                                                    string transactionpassword = Convert.ToString(jObject["TransactionPassword"]).Trim();
                                                    if (dtMemberMaster.Rows.Count > 0)
                                                    {
                                                        if (transactionpassword == dtMemberMaster.Rows[0]["Loginpin"].ToString().Trim())
                                                        {
                                                            string narration = "";
                                                            string hddGUID = System.Guid.NewGuid().ToString();
                                                            string hddGroupTransID = MoneyTransfer_EkoAPI.GetUniqueSessionID();
                                                            DataTable dtWalletBalance = new DataTable();
                                                            decimal MyBalance = 0;
                                                            int success_count = 0;
                                                            dtWalletBalance = objConnection.select_data_dt("select * from VIEW_MEMBERBALANCE where Msrno=" + dtMemberMaster.Rows[0]["Msrno"] + "");
                                                            if (dtWalletBalance.Rows.Count > 0)
                                                            {
                                                                MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["Balance"]);
                                                            }

                                                            if (MyBalance >= Convert.ToDecimal(amount))
                                                            {
                                                                if (1 == 1)
                                                                {
                                                                    decimal mySurcharge = 0;// MoneyTransfer_EkoAPI.GetSurchargeDMT2(dtMemberMaster.Rows[0]["PackageID"].ToString(), Convert.ToDecimal(txtSendMoney.Text));
                                                                    Int32 factorAmount = Convert.ToInt32(amount);
                                                                    Int32 totAmount = Convert.ToInt32(amount);
                                                                    Int32 remainingAmount = totAmount % factorAmount;
                                                                    Int32 loop = totAmount / factorAmount;
                                                                    for (int i = 0; i < loop; i++)
                                                                    {
                                                                        mySurcharge = mySurcharge + MoneyTransfer_EkoAPI.GetSurchargeDMT(dtMemberMaster.Rows[0]["PackageID"].ToString(), Convert.ToDecimal(factorAmount));
                                                                    }
                                                                    if (remainingAmount > 0)
                                                                    {
                                                                        mySurcharge = mySurcharge + MoneyTransfer_EkoAPI.GetSurchargeDMT(dtMemberMaster.Rows[0]["PackageID"].ToString(), Convert.ToDecimal(remainingAmount));
                                                                    }

                                                                    string agentID = System.Guid.NewGuid().ToString().Replace('-', 'D');
                                                                    decimal deductAmount = Convert.ToDecimal(amount) + mySurcharge;
                                                                    string Dec = "DMT Topup with Charges Rs." + Convert.ToDecimal(amount).ToString() + " + " + mySurcharge + " + Txn:-" + agentID;//"DMT2 Fund Transfer(Account: " + lblSendMoneyAccountNo.Text + ")";                                       
                                                                    narration = "DMT TransID-" + agentID;
                                                                    if (MyBalance >= Convert.ToDecimal(deductAmount))
                                                                    {
                                                                        DataTable dtPayment = new DataTable();
                                                                        string result = "";
                                                                        RootInstantPayout rootInstantPayout = new RootInstantPayout();
                                                                        Payee payee = new Payee();
                                                                        payee.name = dtMemberMaster.Rows[0]["Name"].ToString();
                                                                        payee.bankIfsc = beneficiaryIFSC;
                                                                        payee.accountNumber = beneficiaryAccount;
                                                                        rootInstantPayout.payee = payee;
                                                                        Payer payer = new Payer();
                                                                        payer.accountNumber = "204005010472";
                                                                        payer.bankId = "15910";
                                                                        payer.bankProfileId = "19392195560";    
                                                                        rootInstantPayout.payer = payer;
                                                                        Location objLocation = new Location();
                                                                        objLocation = objLocation.GetLocationDataCurrent("103.73.190.82");
                                                                        rootInstantPayout.externalRef = agentID;
                                                                        rootInstantPayout.latitude = objLocation.Latitude;
                                                                        rootInstantPayout.longitude = objLocation.Longitude;
                                                                        rootInstantPayout.transferMode = "IMPS";
                                                                        rootInstantPayout.transferAmount = amount;
                                                                        rootInstantPayout.remarks = "AEPS Settlement";
                                                                        rootInstantPayout.alertEmail = "bobo.ksh@gmail.com";

                                                                        {

                                                                            DataTable dtval = objConnection.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMemberMaster.Rows[0]["loginid"].ToString() + "','" + deductAmount + "','DR','" + narration + "','" + Dec + "'," + ConstantsData.SMoneyTransfer + ",'" + agentID + "'");
                                                                            if (dtval.Rows[0]["msrno"].ToString() == "1")
                                                                            {
                                                                                success_count = 0;
                                                                                int Val = objConnection.update_data("AddEditMoneyTransfer " + dtMemberMaster.Rows[0]["Msrno"].ToString() + "," + amount + "," + mySurcharge + "," + BenID + ",'Pending','IMPS','','" + agentID + "','',''");
                                                                                if (Val > 0)
                                                                                {
                                                                                    result = ApiPostCallInstantPay.PostCall(rootInstantPayout.GetJson(), "payout", Request.Headers["User-Agent"].ToString());
                                                                                    JObject TransferData = JObject.Parse(result);
                                                                                    if (InstantPayout.arrTemplate.Contains(TransferData["statuscode"].ToString()))
                                                                                    {
                                                                                        if (TransferData["statuscode"].ToString().ToUpper() == "TXN" || TransferData["statuscode"].ToString().ToUpper() == "TUP")
                                                                                        {
                                                                                            if (TransferData["statuscode"].ToString().ToUpper() == "TUP")
                                                                                            {
                                                                                                objConnection.update_data("Update MoneyTransfer set Status='Pending',VendorID='" + TransferData["data"]["poolReferenceId"].ToString() + "',Response='" + result + "',Message='" + TransferData["status"].ToString() + "',RRN='' where OrderID='" + agentID + "'");
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                objConnection.update_data("Update MoneyTransfer set Status='Success',VendorID='" + TransferData["data"]["poolReferenceId"].ToString() + "',Response='" + result + "',Message='" + TransferData["status"].ToString() + "',RRN='"+ TransferData["data"]["txnReferenceId"].ToString() + "' where OrderID='" + agentID + "'");
                                                                                            }
                                                                                        }
                                                                                        else if (TransferData["data"]["Status"].ToString().ToUpper() == "FAILED")
                                                                                        {
                                                                                            objConnection.update_data("Update MoneyTransfer set Status='Refund',Message='" + TransferData["status"].ToString() + "',Response='" + result + "' where OrderID='" + agentID + "'");
                                                                                            DataTable dtHistory = new DataTable();
                                                                                            dtHistory = objConnection.select_data_dt("select * from MoneyTransfer where OrderID='" + agentID + "'");
                                                                                            success_count = 0;
                                                                                            Int32 CountCR = 0;
                                                                                            CountCR = objConnection.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like '%" + agentID + "%' and factor='cr'");
                                                                                            if (CountCR == 0)
                                                                                            {
                                                                                                string Narration = "DMT TransID-" + agentID;
                                                                                                objConnection.update_data("Exec [PROC_EWALLETTRANSACTION] '" + dtMemberMaster.Rows[0]["loginid"].ToString() + "'," + Convert.ToDecimal(dtHistory.Rows[0]["FinalAmount"]) + ", 'CR','Refund - " + Narration + "',''," + ConstantsData.SMoneyTransfer + ",'" + dtHistory.Rows[0]["OrderID"].ToString() + "'");
                                                                                            }
                                                                                        }
                                                                                        SuccessMessage(SONITechnoCredentrial.WebsiteURL + "DMTRecipt.aspx?GUID=" + agentID + "");
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        objConnection.update_data("Update MoneyTransfer set Status='Refund',Response='" + result + "' where OrderID='" + agentID + "'");
                                                                                        DataTable dtHistory = new DataTable();
                                                                                        dtHistory = objConnection.select_data_dt("select * from MoneyTransfer where OrderID='" + agentID + "'");
                                                                                        success_count = 0;
                                                                                        Int32 CountCR = 0;
                                                                                        CountCR = objConnection.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like '%" + agentID + "%' and factor='cr'");
                                                                                        if (CountCR == 0)
                                                                                        {
                                                                                            string Narration = "DMT TransID-" + agentID;
                                                                                            objConnection.update_data("Exec [PROC_EWALLETTRANSACTION] '" + dtMemberMaster.Rows[0]["loginid"].ToString() + "'," + Convert.ToDecimal(dtHistory.Rows[0]["FinalAmount"]) + ", 'CR','Refund - " + Narration + "',''," + ConstantsData.SMoneyTransfer + ",'" + dtHistory.Rows[0]["OrderID"].ToString() + "'");
                                                                                        }
                                                                                        ErrorMessage(TransferData["status"].ToString());
                                                                                    }
                                                                                }

                                                                            }
                                                                            else
                                                                            {
                                                                                ErrorMessage("Low Amount");
                                                                                return;
                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ErrorMessage("Low Balance! Minimum balance limit exceed");
                                                                        return;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ErrorMessage("Low Balance! Minimum balance limit exceed");
                                                                    return;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                ErrorMessage("Insufficient Balance");
                                                                return;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ErrorMessage("Invalid Transaction Password");
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ErrorMessage(Convert.ToString(dtMemberMaster.Rows[0]["Error Description"]));
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    ErrorMessage("ShowAlertMessage('You Can only send 50000 in one time");
                                                }
                                            }
                                            else
                                            {
                                                ErrorMessage("Your monthly limit of 1 Lakh Rs is exhausted, kindly try next month");
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            ErrorMessage("try after some time");
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Member Not Found");
                                        return;
                                    }
                                }
                                catch
                                {
                                    ErrorMessage("Technical Error");
                                    return;
                                }
                            }
                            else
                            {
                                ErrorMessage("Insufficient Parameter");
                                return;
                            }
                        }

                        #endregion

                        #region [ 35.deleteben ]

                        else if (methodName == "deleteben")
                        {
                            if (jObject["UserID"] != null && jObject["Password"] != null && jObject["BenID"] != null)
                            {
                                string userID = Convert.ToString(jObject["UserID"]).Trim();
                                string password = Convert.ToString(jObject["Password"]).Trim();
                                string BenID = Convert.ToString(jObject["BenID"]).Trim();
                                deleteben(userID, password, BenID);
                            }
                            else
                            {
                                ErrorMessage("Insufficient Parameter");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        ErrorMessage("Invalid Request");
                    }
                }
                else
                {
                    ErrorMessage("Invalid Request");
                }

            }
            catch (Exception ex)
            {
            }
        }
    }

    private Boolean CheckSendLimitInMonth(string CustomerMobile, double Sendamt)
    {
        //Int32 LIMITAMT = 0;
        //try
        //{
        //    LIMITAMT = objConnection.select_data_scalar_int("PROC_CHECK_SENDAMOUNT_LIMIT_MONTH '" + CustomerMobile + "'," + Sendamt + "");

        //}
        //catch (Exception ex)
        //{
        //}
        //return (LIMITAMT == 1 ? true : false);
        return true;
    }

    #region [ Convertor DataTable to Json ]

    public string ConvertDataTabletoString(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return SuccessArray(serializer.Serialize(rows).ToString());
    }

    #endregion
    private string SuccessArray(string json)
    {
        string response = "{\"statuscode\":\"TXN\",\"status\":\"Transaction Successful\",\"data\": " + json + "}";
        return response;
    }

    private string SuccessPayWord(string Code, string ResponseMessage, string json)
    {
        string response = "{\"responseCode\":\"" + Code + "\",\"responseMessage\":\"" + ResponseMessage + "\",\"data\": " + json + "}";
        return response;
    }

    private void ErrorMessage(string ErrorMessage)
    {
        string response = "{\"statuscode\":\"ERR\",\"status\":\"" + ErrorMessage + "\"}";
        Response.Write(response);
    }

    private void SuccessMessage(string ErrorMessage)
    {
        string response = "{\"statuscode\":\"TXN\",\"status\":\"" + ErrorMessage + "\"}";
        Response.Write(response);
    }
    private Boolean CheckSendLimitInMonth(double Sendamt)
    {
        Int32 LIMITAMT = 1;
        //try
        //{
        //    string CustomerMobile = Session["CustomerMobile"].ToString();
        //    LIMITAMT = objConnection.select_data_scalar_int("PROC_CHECK_SENDAMOUNT_LIMIT_MONTH '" + CustomerMobile + "'," + Sendamt + "");

        //}
        //catch (Exception ex)
        //{
        //}
        return (LIMITAMT == 1 ? true : false);
    }
    private int FundTransfer(decimal transferAmount, decimal TotalAmount, decimal SurchargeAmount, decimal NetAmount, string narrtion, DataTable dtMemberMaster, int success_count, int loopCount, string agentID, string lblSendMoneyIFSC, string lblSendMoneyAccountNo, string txtComment)
    {
        try
        {
            //objConnection.update_data("INSERT INTO RECHARGEURL(URL,TRANSID)VALUES('" + narrtion + "','') ");

            //objConnection.select_data_dt("insert into  tblDMTPaytm_Transaction(Msrno,SenderName,BeneficiaryName,BeneficiaryAccountNo,TotalAmount,FactorAmount,SurchargeAmount,NetAmount,Narration,TransID,GUID, Status,GroupTransID,BankName,IFSC,SenderMobileNO)             Values('" + dtMemberMaster.Rows[0]["Msrno"].ToString() + "','" + lblDataCName.Text + "','" + lblSendMoneyName.Text + "','" + lblSendMoneyAccountNo + "','" + TotalAmount + "','" + transferAmount + "','" + SurchargeAmount + "','" + NetAmount + "','" + narrtion + "','" + agentID + "','" + hddGUID + "','ON HOLD','" + hddGroupTransID + "', '" + lblSendMoneyBankName.Text + "','" + lblSendMoneyIFSC.Text + "','" + Session["CustomerMobile"].ToString() + "')");
            //string result = "";
            //var Values = new NameValueCollection();
            //Values["CustomerMobile"] = Session["CustomerMobile"].ToString();
            //Values["MerchantID"] = MerchantID;
            //Values["MerchantKey"] = DMRMerchantKey;
            //Values["MethodName"] = "sendmoney";
            //Values["amount"] = transferAmount.ToString();
            //Values["beneficiaryIFSC"] = lblSendMoneyIFSC;
            //Values["beneficiaryAccount"] = lblSendMoneyAccountNo;
            //Values["orderId"] = agentID;
            //Values["comments"] = txtComment;

            //result = ObjMahagramDMT.SendAmount(Values);
            //objConnection.update_data("INSERT INTO RECHARGEURL(URL,TRANSID)VALUES('" + result + "','') ");
            //{\"statuscode\":\"ERR\"
            //JObject jObject = JObject.Parse(result);
            //    string statusCode = jObject["statuscode"].ToString().Trim();
            //    string status = jObject["status"].ToString().Trim();
            //    string datastr = jObject["data"].ToString().Trim();
            //    if (statusCode.ToUpper() != "ERR")
            //    {
            //        { "statuscode":"DE_002","status":"ACCEPTED","data":{ "orderId":"1105202016323214715","cyrusOrderId":"","cyrus_id":"TPMM11052020163109807106335","opening_bal":"980.60","locked_amt":"20","charged_amt":"29.50"} }
            //        if (statusCode == "DE_001" || statusCode == "DE_002" || statusCode == "DE_101")
            //        {
            //            JObject Data = JObject.Parse(datastr);
            //            if (statusCode == "DE_001")
            //            {
            //                success_count = 1;
            //                objConnection.update_data("Update tblDMTPaytm_Transaction set charged_amt=" + Data["charged_amt"].ToString() + ", Status='SUCCESS',BankRefNo='" + Data["cyrusOrderId"].ToString() + "',clientreferno='" + Data["cyrus_id"].ToString() + "',Response='" + result.Replace("'", "") + "' where TransID='" + agentID + "'");
            //            }
            //            else if (statusCode == "DE_002")
            //            {
            //                success_count = 1;
            //                objConnection.update_data("Update tblDMTPaytm_Transaction set charged_amt=" + Data["charged_amt"].ToString() + ",Status='ACCEPTED',clientreferno='" + Data["cyrus_id"].ToString() + "',Response='" + result.Replace("'", "") + "' where TransID='" + agentID + "'");
            //            }
            //            else if (statusCode == "DE_101")
            //            {
            //                success_count = 1;
            //                objConnection.update_data("Update tblDMTPaytm_Transaction set charged_amt=" + Data["charged_amt"].ToString() + ",Status='PENDING',clientreferno='" + Data["cyrus_id"].ToString() + "',Response='" + result.Replace("'", "") + "' where TransID='" + agentID + "'");
            //            }
            //        }
            //        else
            //        {

            //            objConnection.update_data("Update tblDMTPaytm_Transaction set Status='Failed',narration=concat(narration,' ','Failed Due to downtime') where TransID='" + agentID + "'");

            //        }
            //    }
            //    else
            //    {
            //        DataTable dtHistory = new DataTable();

            //        dtHistory = objConnection.select_data_dt("select * from tblDMTPaytm_Transaction where TransID='" + agentID + "'");
            //        if (dtHistory.Rows[0]["status"].ToString().ToLower() != "refund")
            //        {
            //            objConnection.update_data("Update tblDMTPaytm_Transaction set Status='Refund' where TransID='" + agentID + "'");
            //            objConnection.update_data("Exec [ProcMLM__EWalletTransaction] '" + dtMember.Rows[0]["Memberid"].ToString() + "'," + Convert.ToDecimal(dtHistory.Rows[0]["NetAmount"]) + ", 'Cr','Refund - " + dtHistory.Rows[0]["Narration"].ToString() + "', 'WEB',''");
            //        }
            //    }
            return 0;
        }
        catch
        {
            return success_count;
        }
    }



    private void GetCustomerDetailsSpecial(string UserID, string Password, string DeviceID, string MobileNO)
    {
        try
        {
            //CustomerDetails customerRegistration = new CustomerDetails();
            //customerRegistration.Mobile = MobileNO;
            //customerRegistration.MethodName = "customerdetails";
            //var context = new System.ComponentModel.DataAnnotations.ValidationContext(customerRegistration, serviceProvider: null, items: null);
            //var results = new List<ValidationResult>();
            //var isValid = Validator.TryValidateObject(customerRegistration, context, results, true);
            //if (!isValid)
            //{
            //    foreach (ValidationResult validationResult in results)
            //    {
            //        ErrorMessage(validationResult.ErrorMessage);
            //        return;
            //    }
            //}
            //else
            {
                //string Result = ApiPostCall.PostCall(customerRegistration.GetJson(), SONITechnoCredentrial.MoneyURL);
                //JObject Data = JObject.Parse(Result);
                //if (Data["code"].ToString().ToUpper() != "ERR")
                {
                    //  JArray jSTDetails = JArray.Parse(Data["data"].ToString());

                    DataTable dtcus = new DataTable();
                    dtcus = objConnection.select_data_dt("select ID,NAME,MOBILE,ADDRESS,PAN,AADHAR,convert(nvarchar(30),Adddate,109)ADDDATE,IsKYC from CustomerDMT where mobile='" + MobileNO + "'");

                    if (dtcus.Rows.Count > 0)
                    {
                        Response.Write(ConvertDataTabletoString(dtcus));
                    }
                    else
                    {
                        ErrorMessage("No Record Found!");
                    }
                }
                //else
                //{
                //    ErrorMessage("No Record Found!");
                //}
            }
        }
        catch
        {
            ErrorMessage("Technical Error");
        }
    }


    private void customerregistrationspecial(string UserID, string Password, string DeviceID, string MobileNO, string Name, string Address, string Pan, string Aadhar, string OTP)
    {
        //CustomerRegistration customerRegistration = new CustomerRegistration();
        //customerRegistration.Name = Name;
        //customerRegistration.Mobile = MobileNO;
        //customerRegistration.ADDRESS = Address;
        //customerRegistration.Pan = Pan;
        //customerRegistration.AADHAR = Aadhar;
        //customerRegistration.IsVerify = true;
        //customerRegistration.MethodName = "customerregistration";
        //var context = new System.ComponentModel.DataAnnotations.ValidationContext(customerRegistration, serviceProvider: null, items: null);
        //var results = new List<ValidationResult>();
        //var isValid = Validator.TryValidateObject(customerRegistration, context, results, true);

        //if (!isValid)
        //{
        //    foreach (ValidationResult validationResult in results)
        //    {
        //        ErrorMessage(validationResult.ErrorMessage);
        //        return;
        //    }
        //}
        //else
        {
            //string Result = ApiPostCall.PostCall(customerRegistration.GetJson(), SONITechnoCredentrial.MoneyURL);
            //JObject Data = JObject.Parse(Result);
            //if (Data["code"].ToString().ToUpper() == "ERR")
            //{
            //    ErrorMessage(Data["mess"].ToString());
            //}
            //else
            {
                DataTable dtMember = objConnection.select_data_dt("select * from Member where mobile='" + UserID + "'");
                DataTable dt = new DataTable();
                dt = objConnection.select_data_dt("AddEditCustomerDMT '" + Name + "','" + MobileNO + "','" + Address + "','" + Pan + "','" + Aadhar + "'," + dtMember.Rows[0]["Msrno"].ToString() + "");
                dt = objConnection.select_data_dt("PROC_CUSTOMERREG_SPECIALDMT '" + Name + "','" + MobileNO + "','" + Address + "','" + Pan + "','" + Aadhar + "'");
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["status"].ToString() == "1")
                    {
                        SuccessMessage("Registration Successfully");
                    }
                    else
                    {
                        ErrorMessage(dt.Rows[0]["MESS"].ToString());
                    }
                }
            }
        }
    }

    private void GetBeneficiaryDetailsSpecial(string UserID, string Password, string DeviceID, string MobileNO)
    {
        try
        {
            dt = objConnection.select_data_dt("select * from CustomerDMT where mobile='" + MobileNO + "'");
            if (dt.Rows.Count > 0)
            {
                DataTable bsBenBindData = new DataTable();
                bsBenBindData = objConnection.select_data_dt("select ID,MSRNO,name,Mobileno,accountNumber,ifsc,accountType,otpVerify,convert(nvarchar(30),AddDate,106)ADDDATE,IsActive from BeneficiaryDMT where msrno='" + dt.Rows[0]["ID"] + "' and IsActive=1 order by id desc");
                if (bsBenBindData.Rows.Count > 0)
                {
                    Response.Write(ConvertDataTabletoString(bsBenBindData));
                }
                else
                {
                    ErrorMessage("No Record Found!");
                }
            }
            else
            {
                ErrorMessage("User not found");
            }
        }
        catch
        {
            ErrorMessage("Technical Error");
        }
    }


    private void AddBeneficiarySpecial(string userID, string password, string deviceID, string MobileNo, string AccountNo, string senderMobileNo, string Name, string IFSC)
    {
        try
        {
            AddAccount addAccount = new AddAccount();
            addAccount.accountNumber = AccountNo;
            addAccount.accountType = "Saving";
            addAccount.CustomerMobile = senderMobileNo;
            addAccount.IFSC = IFSC;
            addAccount.Mobile = MobileNo;
            addAccount.Name = Name;
            addAccount.MethodName = "addaccount";
            var context = new System.ComponentModel.DataAnnotations.ValidationContext(addAccount, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(addAccount, context, results, true);


            if (!isValid)
            {
                foreach (ValidationResult validationResult in results)
                {
                    ErrorMessage(validationResult.ErrorMessage);
                    return;
                }
            }
            else
            {
                //string Result = ApiPostCall.PostCall(addAccount.GetJson(), SONITechnoCredentrial.MoneyURL);
                //JObject Data = JObject.Parse(Result);
                //if (Data["code"].ToString().ToUpper() == "ERR")
                //{
                //    ErrorMessage(Data["mess"].ToString());
                //}
                //else
                {
                    dt = objConnection.select_data_dt("select * from CustomerDMT where mobile='" + senderMobileNo + "'");

                    Int32 Val = 0;
                    Val = objConnection.update_data("AddEditBeneficiaryDMT " + dt.Rows[0]["ID"] + ",'" + Name + "','" + MobileNo + "','" + AccountNo + "','" + IFSC + "'");
                    if (Val > 0)
                    {
                        SuccessMessage("Add Beneficiary Successfully");
                    }
                    else
                    {
                        ErrorMessage("Already exists");
                    }
                }
            }
        }
        catch
        {
            ErrorMessage("Technical Error");
        }
    }


    private void VerifyAccountSpecial(string userID, string password, string deviceID, string MobileNo, string CustomerMobileNo, string beneficiaryIFSC, string beneficiaryAccount)
    {
        try
        {
            dt = objConnection.select_data_dt("select * from CUSTOMERREG_SPECIALDMT where mobile='" + CustomerMobileNo + "'");
            if (dt.Rows.Count > 0)
            {
                if (CheckSendLimitInMonth(Convert.ToDouble(MoneyTransfer_EkoAPI.Beneficiary_VerificationFee)))
                {
                    DataTable dtMemberMaster = new DataTable();
                    dtMemberMaster = objConnection.select_data_dt("select * from Member where mobile='" + userID + "' and password='" + password + "'");

                    decimal MyBalance = 0;
                    string agentID = System.Guid.NewGuid().ToString().Replace('-', '0');



                    DataTable dtWalletBalance = new DataTable();
                    dtWalletBalance = objConnection.select_data_dt("select * from VIEW_MEMBERBALANCE where Msrno=" + dtMemberMaster.Rows[0]["Msrno"] + "");
                    if (dtWalletBalance.Rows.Count > 0)
                    {
                        MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["Balance"]);
                    }

                    if (MyBalance >= Convert.ToDecimal(MoneyTransfer_EkoAPI.Beneficiary_VerificationFee)) // Verification fee
                    {
                        if (MoneyTransfer_EkoAPI.CheckMinimumBalance(Convert.ToInt32(dtMemberMaster.Rows[0]["Msrno"]), Convert.ToDecimal(MoneyTransfer_EkoAPI.Beneficiary_VerificationFee), MyBalance))
                        {
                            string Desc = "" + beneficiaryAccount + " Account Verification With TransID-" + agentID;
                            string narration = "Account Verification TransID-" + agentID;
                            decimal deductAmount = (Convert.ToDecimal(MoneyTransfer_EkoAPI.Beneficiary_VerificationFee));

                            AccountVerifyInstantPay accountVerifyInstantPay = new AccountVerifyInstantPay();
                            Payee payee = new Payee();
                            payee.accountNumber = beneficiaryAccount;
                            payee.bankIfsc = beneficiaryIFSC;
                            accountVerifyInstantPay.payee = payee;
                            accountVerifyInstantPay.externalRef = agentID;
                            string iPAddress = cls_myMember.GetIPAddress();
                            Location objLocation = new Location();
                            objLocation = objLocation.GetLocationDataCurrent(iPAddress);
                            accountVerifyInstantPay.latitude = objLocation.Latitude;
                            accountVerifyInstantPay.longitude = objLocation.Longitude;
                            string result = ApiPostCallInstantPay.AccountVerifyPostCall(accountVerifyInstantPay.GetJson(), "verifyBankAccount", Request.Headers["User-Agent"].ToString());
                            JObject jObject = JObject.Parse(result);
                            if (jObject["statuscode"].ToString().ToUpper() == "TXN")
                            {
                                int Val = objConnection.update_data("AddEditMoneyTransfer " + dtMemberMaster.Rows[0]["Msrno"].ToString() + "," + MoneyTransfer_EkoAPI.Beneficiary_VerificationFee + ",0,0,'Success','IMPS','','" + agentID + "','','" + jObject["data"]["txnReferenceId"].ToString() + "','" + result + "'");
                                if (Val > 0)
                                {
                                    objConnection.select_data_dt("exec [PROC_EWALLETTRANSACTION]  '" + dtMemberMaster.Rows[0]["LoginID"].ToString() + "', " + deductAmount + ", 'Dr','" + narration + "','" + Desc + "'," + ConstantsData.SAccountVerfication + ",'" + agentID + "'");
                                    SuccessMessage(jObject["data"]["payee"]["name"].ToString());
                                }

                            }
                            else
                            {
                                ErrorMessage(jObject["statusMessage"].ToString());
                            }
                        }
                        else
                        {
                            ErrorMessage("Low Balance! Minimum balance limit exceed.");
                        }
                    }
                    else
                    {
                        ErrorMessage("You have Insufficient Balance.");
                    }
                }
                else
                {
                    ErrorMessage("Your monthly limit of 1 Lakh Rs is exhausted, kindly try next month");
                }

            }
            else
            {
                ErrorMessage("Customer Not register");
            }
        }
        catch
        {
            ErrorMessage("Technical Error");
        }
    }

    #region [ 35.deleteben ]

    public void deleteben(string userID, string password, string BenID)
    {
        try
        {
            Int32 Val = objConnection.update_data("update ADD_BENE_ACCOUNT_HYPTO set IsACtive=iif(isactive=1,0,1) where ID=" + BenID + "");
            if (Val > 0)
            {
                SuccessMessage("Successfully Delete");
            }
            else
            {
                ErrorMessage("No Record Found!");
            }
        }
        catch
        {
            ErrorMessage("Technical Error");
        }
    }

    #endregion
}