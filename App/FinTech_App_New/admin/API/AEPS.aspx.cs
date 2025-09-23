using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class API_AEPS : System.Web.UI.Page
{

    cls_connection objConnection = new cls_connection();
    DataTable dt = new DataTable();
    cls_myMember clsm = new cls_myMember();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            NameValueCollection nvc = new NameValueCollection();// = Request.Form;
            var ss = HttpUtility.HtmlDecode(Request.Unvalidated["Request"]);
            nvc["Request"] = HttpUtility.HtmlDecode(Request.Unvalidated["Request"]);
            if (HttpUtility.HtmlDecode(nvc["Request"]) != null)
            {
                JObject jObject = JObject.Parse(nvc["Request"]);
                string DeviceID = Request.Headers["DeviceID"].ToString().Trim();
                string strUser = "";
                string strPassword = "";
                string methodName = Convert.ToString(jObject["MethodName"]).Trim().ToLower();

                //if (!string.IsNullOrEmpty(DeviceID))
                //{
                //    if (CheckUser(strUser, strPassword, DeviceID, methodName))
                //    {
                #region [ 1.GetCompanyDetails]
                if (methodName == "balanceenquiry")
                {
                    if (jObject["UserID"] != null && jObject["mobilenumber"] != null && jObject["adhaarnumber"] != null && jObject["data"] != null && jObject["bankno"] != null)
                    {
                        string mobilenumber = Convert.ToString(jObject["mobilenumber"]).Trim();
                        string adhaarnumber = Convert.ToString(jObject["adhaarnumber"]).Trim();
                        string UserID = Convert.ToString(jObject["UserID"]).Trim();
                        string bankno = Convert.ToString(jObject["bankno"]).Trim();
                        string data = Convert.ToString(jObject["data"]).Trim();

                        //BALANCEENQUIRY bALANCEENQUIRY = new BALANCEENQUIRY();
                        //bALANCEENQUIRY.mobilenumber = mobilenumber;
                        //bALANCEENQUIRY.adhaarnumber = adhaarnumber;
                        //bALANCEENQUIRY.data = data;
                        //bALANCEENQUIRY.accessmodetype = "APP";

                        //bALANCEENQUIRY.nationalbankidentification = bankno;
                        //bALANCEENQUIRY.referenceno = System.Guid.NewGuid().ToString().Replace("-", "0");
                        //bALANCEENQUIRY.transcationtype = "BE";
                        //AEPSBankBodyRequest aEPSBankBodyRequest = new AEPSBankBodyRequest();
                        //objConnection.update_data("insert into tbl_Apeslog(url)values('" + bALANCEENQUIRY.GetJson() + "')");
                        balanceenquiry(UserID, mobilenumber, adhaarnumber, bankno, data);

                        //Response.Write(aEPSBankBodyRequest.BalanceEnquiry(bALANCEENQUIRY));
                    }
                }
                #endregion

                #region [ 1.MiniStatement]
                if (methodName == "ministatement")
                {
                    if (jObject["UserID"] != null && jObject["mobilenumber"] != null && jObject["adhaarnumber"] != null && jObject["data"] != null && jObject["bankno"] != null)
                    {
                        string mobilenumber = Convert.ToString(jObject["mobilenumber"]).Trim();
                        string adhaarnumber = Convert.ToString(jObject["adhaarnumber"]).Trim();
                        string bankno = Convert.ToString(jObject["bankno"]).Trim();
                        string data = Convert.ToString(jObject["data"]).Trim();
                        string UserID = Convert.ToString(jObject["UserID"]).Trim();

                        //MiniStatement miniStatement = new MiniStatement();
                        //miniStatement.mobilenumber = mobilenumber;
                        //miniStatement.adhaarnumber = adhaarnumber;
                        //miniStatement.data = data;
                        //miniStatement.accessmodetype = "APP";
                        //miniStatement.nationalbankidentification = bankno;
                        //miniStatement.submerchantid = "R400101";
                        //miniStatement.referenceno = System.Guid.NewGuid().ToString().Replace("-", "0");
                        //AEPSBankBodyRequest aEPSBankBodyRequest = new AEPSBankBodyRequest();
                        //objConnection.update_data("insert into tbl_Apeslog(url)values('" + miniStatement.GetJson() + "')");

                        ministatement(UserID, mobilenumber, adhaarnumber, bankno, data);
                    }
                }
                #endregion

                else if (methodName == "cashwithdraw")
                {
                    if (jObject["UserID"] != null && jObject["mobilenumber"] != null && jObject["adhaarnumber"] != null && jObject["data"] != null && jObject["bankno"] != null && jObject["amount"] != null)
                    {
                        string mobilenumber = Convert.ToString(jObject["mobilenumber"]).Trim();
                        string adhaarnumber = Convert.ToString(jObject["adhaarnumber"]).Trim();
                        Int32 amount = Convert.ToInt32(jObject["amount"]);
                        string bankno = Convert.ToString(jObject["bankno"]).Trim();
                        string data = Convert.ToString(jObject["data"]).Trim();
                        string UserID = Convert.ToString(jObject["UserID"]).Trim();

                        //CashWithdraw cashWithdraw = new CashWithdraw();
                        //cashWithdraw.mobilenumber = mobilenumber;
                        //cashWithdraw.adhaarnumber = adhaarnumber;
                        //cashWithdraw.amount = amount;
                        //cashWithdraw.data = data;
                        //cashWithdraw.submerchantid = "R400101";
                        //cashWithdraw.nationalbankidentification = bankno;
                        //cashWithdraw.referenceno = System.Guid.NewGuid().ToString().Replace("-", "0");
                        //cashWithdraw.transcationtype = "CW";
                        //AEPSBankBodyRequest aEPSBankBodyRequest = new AEPSBankBodyRequest();
                        //objConnection.update_data("insert into tbl_Apeslog(url)values('" + cashWithdraw.GetJson() + "')");

                        cashwithdraw(UserID, mobilenumber, adhaarnumber, bankno, data, amount.ToString());
                    }
                }

                else if (methodName == "acashwithdraw")
                {
                    if (jObject["UserID"] != null && jObject["mobilenumber"] != null && jObject["adhaarnumber"] != null && jObject["data"] != null && jObject["bankno"] != null && jObject["amount"] != null)
                    {
                        string mobilenumber = Convert.ToString(jObject["mobilenumber"]).Trim();
                        string adhaarnumber = Convert.ToString(jObject["adhaarnumber"]).Trim();
                        Int32 amount = Convert.ToInt32(jObject["amount"]);
                        string bankno = Convert.ToString(jObject["bankno"]).Trim();
                        string data = Convert.ToString(jObject["data"]).Trim();
                        string UserID = Convert.ToString(jObject["UserID"]).Trim();

                        //AADHARPAY aADHARPAY = new AADHARPAY();
                        //aADHARPAY.mobilenumber = mobilenumber;
                        //aADHARPAY.adhaarnumber = adhaarnumber;
                        //aADHARPAY.amount = amount;
                        //aADHARPAY.data = data;
                        //aADHARPAY.accessmodetype = "SITE";

                        //aADHARPAY.nationalbankidentification = bankno;
                        //aADHARPAY.referenceno = System.Guid.NewGuid().ToString().Replace("-", "0");
                        //aADHARPAY.transcationtype = "CW";
                        //AEPSBankBodyRequest aEPSBankBodyRequest = new AEPSBankBodyRequest();
                        //objConnection.update_data("insert into tbl_Apeslog(url)values('" + aADHARPAY.GetJson() + "')");

                        acashwithdraw(UserID, mobilenumber, adhaarnumber, bankno, data, amount.ToString());

                        //Response.Write(aEPSBankBodyRequest.AADHARPAY(aADHARPAY));
                    }
                }

                #region [ 3.GetBankList]
                else if (methodName == "getbanklist")
                {
                    BankList bankList = new BankList();
                    string bankListstr = ApiPostCall.PostCall(bankList.GetJson(), SONITechnoCredentrial.AEPSCore);
                    JObject jData = JObject.Parse(bankListstr);

                    string Respone = "{\"status\": true,\"response_code\": 1,\"banklist\": {\"status\": true,\"message\":\"Request Completed\",\"data\":" + jData["data"].ToString() + "}}";
                    Response.Write(Respone);

                }
                #endregion

                #region [ 3.CashDepositInitiate]
                else if (methodName == "cashdepositinitiate")
                {
                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["MobileNo"] != null && jObject["AccountNumber"] != null && jObject["Amount"] != null)
                    {
                        string UserID = Convert.ToString(jObject["UserID"]).Trim();
                        string Password = Convert.ToString(jObject["Password"]).Trim();
                        string MobileNo = Convert.ToString(jObject["MobileNo"]).Trim();
                        string AccountNumber = Convert.ToString(jObject["AccountNumber"]).Trim();
                        string Amount = Convert.ToString(jObject["Amount"]).Trim();
                    }

                }
                #endregion

                #region [ 4.Deposit]
                else if (methodName == "deposit")
                {
                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["AccountNumber"] != null && jObject["MobileNo"] != null && jObject["Amount"] != null && jObject["merchanttxnid"] != null && jObject["txnreferenceno"] != null && jObject["onetimetoken"] != null && jObject["OTP"] != null)
                    {
                        string UserID = Convert.ToString(jObject["UserID"]).Trim();
                        string Password = Convert.ToString(jObject["Password"]).Trim();
                        string merchanttxnid = Convert.ToString(jObject["merchanttxnid"]).Trim();
                        string txnreferenceno = Convert.ToString(jObject["txnreferenceno"]).Trim();
                        string onetimetoken = Convert.ToString(jObject["onetimetoken"]).Trim();
                        string OTP = Convert.ToString(jObject["OTP"]).Trim();
                        string Amount = Convert.ToString(jObject["Amount"]).Trim();
                        string AccountNumber = Convert.ToString(jObject["AccountNumber"]).Trim();
                        string MobileNo = Convert.ToString(jObject["MobileNo"]).Trim();
                    }

                }
                #endregion

                else if (methodName == "athenticate")
                {
                    if (jObject["UserID"] != null && jObject["data"] != null)
                    {
                        string data = Convert.ToString(jObject["data"]).Trim();
                        string UserID = Convert.ToString(jObject["UserID"]).Trim();

                        DataTable dtMember = objConnection.select_data_dt("select * from Member where mobile='" + UserID + "'");
                        string ipAddress = ConstantsData.getipAddress();
                        string RefID = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + dtMember.Rows[0]["msrno"].ToString();
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(HttpUtility.HtmlDecode(data));
                        string jsonText = JsonConvert.SerializeXmlNode(doc);
                        dynamic bsObj;
                        DataTable dtValc = objConnection.select_data_dt("select * from aepskyc where Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");
                        if (dtValc.Rows.Count > 0)
                        {

                            bsObj = JsonConvert.DeserializeObject<Mantra>(jsonText);


                            ActionKYVRoot bALANCEENQUIRY = new ActionKYVRoot();
                            string iPAddress = cls_myMember.GetIPAddress();
                            Location location = new Location();
                            location = location.GetLocationDataCurrent(iPAddress);

                            bALANCEENQUIRY.latitude = location.Latitude;
                            bALANCEENQUIRY.longitude = location.Longitude;

                            BiometricData thumbRequest = new BiometricData();
                            thumbRequest.encryptedAadhaar = InstantPayout.GetEncryptAadhar(dtMember.Rows[0]["Aadhar"].ToString());// bsObj.PidData.Skey.Ci;
                            thumbRequest.dc = bsObj.PidData.DeviceInfo.Dc;
                            thumbRequest.ci = bsObj.PidData.Skey.Ci;
                            thumbRequest.hmac = bsObj.PidData.Hmac;
                            thumbRequest.dpId = bsObj.PidData.DeviceInfo.DpId;
                            thumbRequest.mc = bsObj.PidData.DeviceInfo.Mc;
                            thumbRequest.pidDataType = bsObj.PidData.Data.Type;
                            thumbRequest.sessionKey = bsObj.PidData.Skey.Text;
                            thumbRequest.mi = bsObj.PidData.DeviceInfo.Mi;
                            thumbRequest.rdsId = bsObj.PidData.DeviceInfo.RdsId;
                            thumbRequest.errCode = bsObj.PidData.Resp.ErrCode;
                            thumbRequest.errInfo = bsObj.PidData.Resp.ErrInfo;
                            thumbRequest.fCount = bsObj.PidData.Resp.FCount;
                            thumbRequest.fType = bsObj.PidData.Resp.FType;
                            thumbRequest.iCount = 0;
                            thumbRequest.iType = "0";
                            thumbRequest.pCount = 0;
                            thumbRequest.pType = bsObj.PidData.DeviceInfo.RdsId;
                            thumbRequest.srno = bsObj.PidData.DeviceInfo.RdsId;
                            thumbRequest.sysid = "";
                            thumbRequest.ts = bsObj.PidData.DeviceInfo.RdsId;
                            thumbRequest.pidData = bsObj.PidData.Data.Text;
                            thumbRequest.qScore = bsObj.PidData.Resp.QScore;
                            thumbRequest.nmPoints = bsObj.PidData.Resp.NmPoints;
                            thumbRequest.rdsVer = bsObj.PidData.DeviceInfo.RdsVer;
                            bALANCEENQUIRY.biometricData = thumbRequest;

                            string Result = ApiPostCallInstantPay.PostCallWithFullURL(bALANCEENQUIRY.GetJson(), "https://api.instantpay.in/fi/aeps/outletLogin", "", dtValc.Rows[0]["OutLetID"].ToString());

                            JObject Data = JObject.Parse(Result);
                            if (Data["statuscode"].ToString() == "TXN")
                            {
                                objConnection.update_data("update aepskyc set Authentication=1,AuthDate=getdate() where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "'");
                                SuccessMessage(Data["status"].ToString());
                            }
                            else
                            {
                                ErrorMessage(Data["status"].ToString());
                            }
                        }

                    }
                }

                else if (methodName == "aepsbalance")
                {
                    if (jObject["UserID"] != null)
                    {
                        string UserID = Convert.ToString(jObject["UserID"]).Trim();

                        DataTable dt = new DataTable();
                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

                        dt = objConnection.select_data_dt("select cast(1 as bit) as registration,iif(aehy.Authentication=1 and convert(nvarchar(30),aehy.authdate,112)=convert(nvarchar(30),getdate(),112),cast(1 as bit),cast(0 as bit)) as Authntication, balance,mm.DeviceRegister as  Device from TBL_EWALLETBALANCE inner join Member mm on mm.msrno = TBL_EWALLETBALANCE.MsrNo inner join AEPSKYC aehy on aehy.msrno = mm.MsrNo where TBL_EWALLETBALANCE.msrno = " + Msrno + "");
                        if (dt.Rows.Count > 0)
                        {
                            Response.Write(ConvertDataTabletoString(dt));
                        }
                        else
                        {
                            ErrorMessage("Not Available");
                        }
                    }
                }
                else if (methodName == "devicelist")
                {
                    DataTable dt = new DataTable();
                    dt = objConnection.select_data_dt("select ID,Name from DeviceList");
                    if (dt.Rows.Count > 0)
                    {
                        Response.Write(ConvertDataTabletoString(dt));
                    }
                    else
                        ErrorMessage("Service not available");
                }
                else if (methodName == "deviceregister")
                {
                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["DeviceName"] != null)
                    {
                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                        string DeviceName = Convert.ToString(jObject["DeviceName"]).Trim();
                        Int32 Val = 0;
                        Val = objConnection.update_data("update member set DeviceRegister='" + DeviceName + "' where mobile='" + userID + "'");
                        if (Val > 0)
                        {
                            SuccessMessage("Register Successfully");
                        }
                    }
                }
            }
            //    }
            //}
            //}
            //catch (Exception ex)
            //{
            //}
        }

    }

    public string BindBank()
    {
        JWTBankSendRequestNewIntegration jWTBankSendRequest = new JWTBankSendRequestNewIntegration();
        cls_connection objConnection = new cls_connection();


        string Result = GetCall("balance/balance/banklist", jWTBankSendRequest.GetToken());
        return Result;
    }
    public string GetCall(string url, string Token)
    {
        var client = new RestClient("https://api.paysprint.in/api/v1/service/aeps/banklist/index");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Token", Token);
        //request.AddHeader("Authorisedkey", "ZjY1MjkzNjc0ZGUxMTMwOGQxNWQyYjU4NDMyZTVhMGI=");

        IRestResponse response = client.Execute(request);

        return response.Content;
    }
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
    private bool CheckUser(string UserID, string Password, string DeviceID, string MethodName)
    {
        if (MethodName.ToLower() == "getcompanydetails" || MethodName.ToLower() == "verifyuserinotherphone")
        {
            return true;
        }
        else
        {
            DataTable dt = new DataTable();
            dt = objConnection.select_data_dt("ProcRecharge_AndroidService 'CheckUser','" + UserID + "','" + Password + "','" + DeviceID + "'");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    private string SuccessArray(string json)
    {
        string response = "{\"statuscode\":\"TXN\",\"status\":\"Transaction Successful\",\"data\": " + json + "}";
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

    private void balanceenquiry(string UserID, string mobilenumber, string adhaarnumber, string bankno, string data)
    {
        DataTable dtMember = objConnection.select_data_dt("select * from Member where mobile='" + UserID + "'");
        if (dtMember.Rows.Count > 0)
        {
            DataTable dtValc = objConnection.select_data_dt("select * from aepskyc where Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");
            if (dtValc.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(mobilenumber) && !string.IsNullOrEmpty(adhaarnumber))
                {
                    string RefID = System.Guid.NewGuid().ToString().Replace("-", "0");
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(HttpUtility.HtmlDecode(data));
                    string jsonText = JsonConvert.SerializeXmlNode(doc);
                    dynamic bsObj;

                    bsObj = JsonConvert.DeserializeObject<Mantra>(jsonText);
                    string aadhar = adhaarnumber.Substring(8, 4);
                    Int32 Val = objConnection.update_data("Exec Proc_Add_edit_AEPS " + dtMember.Rows[0]["msrno"].ToString() + ", '" + RefID + "','0', 'BE', " + bankno + ",'" + (("xxxx-xxxx-" + aadhar)) + "', '" + mobilenumber + "', 'Pending','','','','','App','','',''");
                    if (Val > 0)
                    {
                        InstantPayBERoot bALANCEENQUIRY = new InstantPayBERoot();
                        string iPAddress = cls_myMember.GetIPAddress();
                        Location location = new Location();
                        location = location.GetLocationDataCurrent(iPAddress);

                        bALANCEENQUIRY.latitude = location.Latitude;
                        bALANCEENQUIRY.longitude = location.Longitude;
                        bALANCEENQUIRY.bankiin = bankno;
                        bALANCEENQUIRY.mobile = mobilenumber;
                        bALANCEENQUIRY.externalRef = RefID;
                        InstantPayBEBody thumbRequest = new InstantPayBEBody();
                        thumbRequest.encryptedAadhaar = InstantPayout.GetEncryptAadhar(adhaarnumber);// bsObj.PidData.Skey.Ci;
                        thumbRequest.dc = bsObj.PidData.DeviceInfo.Dc;
                        thumbRequest.ci = bsObj.PidData.Skey.Ci;
                        thumbRequest.hmac = bsObj.PidData.Hmac;
                        thumbRequest.dpId = bsObj.PidData.DeviceInfo.DpId;
                        thumbRequest.mc = bsObj.PidData.DeviceInfo.Mc;
                        thumbRequest.pidDataType = bsObj.PidData.Data.Type;
                        thumbRequest.sessionKey = bsObj.PidData.Skey.Text;
                        thumbRequest.mi = bsObj.PidData.DeviceInfo.Mi;
                        thumbRequest.rdsId = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.errCode = bsObj.PidData.Resp.ErrCode;
                        thumbRequest.errInfo = bsObj.PidData.Resp.ErrInfo;
                        thumbRequest.fCount = bsObj.PidData.Resp.FCount;
                        thumbRequest.fType = bsObj.PidData.Resp.FType;
                        thumbRequest.iCount = 0;
                        thumbRequest.iType = "0";
                        thumbRequest.pCount = 0;
                        thumbRequest.pType = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.srno = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.sysid = "";
                        thumbRequest.ts = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.pidData = bsObj.PidData.Data.Text;
                        thumbRequest.qScore = bsObj.PidData.Resp.QScore;
                        thumbRequest.nmPoints = bsObj.PidData.Resp.NmPoints;
                        thumbRequest.rdsVer = bsObj.PidData.DeviceInfo.RdsVer;
                        bALANCEENQUIRY.biometricData = thumbRequest;

                        string Result = ApiPostCallInstantPay.PostCallWithFullURL(bALANCEENQUIRY.GetJson(), "https://api.instantpay.in/fi/aeps/balanceInquiry", "", dtValc.Rows[0]["OutLetID"].ToString());

                        JObject Data = JObject.Parse(Result);

                        if (Data["statuscode"].ToString() == "TXN")
                        {
                            Val = objConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','BE','Success','" + Data["status"].ToString() + "',0,'" + Data["data"]["operatorId"].ToString() + "','','" + Result + "'");
                            if (Val > 0)
                            {
                            }
                            BEMobileResponse bEMobileResponse = new BEMobileResponse();
                            bEMobileResponse.status = true;
                            bEMobileResponse.message = Data["status"].ToString();
                            bEMobileResponse.ackno = Data["orderid"].ToString();
                            bEMobileResponse.amount = Data["data"]["openingBalance"].ToString();
                            bEMobileResponse.balanceamount = Data["data"]["bankAccountBalance"].ToString();
                            bEMobileResponse.bankrrn = Data["data"]["operatorId"].ToString();
                            bEMobileResponse.bankiin = bankno;
                            bEMobileResponse.response_code = Data["statuscode"].ToString();
                            bEMobileResponse.errorcode = "00";
                            bEMobileResponse.clientrefno = Data["data"]["ipayId"].ToString();
                            bEMobileResponse.last_aadhar = Data["data"]["accountNumber"].ToString();
                            bEMobileResponse.name = Data["data"]["bankName"].ToString();
                            bEMobileResponse.OrderID = RefID;
                            bEMobileResponse.STID = Data["data"]["walletIpayId"].ToString();
                            bEMobileResponse.Commission = "0";
                            bEMobileResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                            bEMobileResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                            bEMobileResponse.Aadhar = adhaarnumber;
                            bEMobileResponse.TransDate = DateTime.Now.ToString();
                            bEMobileResponse.TransactionID = RefID;

                            Response.Write(bEMobileResponse.GetJson());
                        }
                        else
                        {
                            Val = objConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','BE','Failed','" + Data["status"].ToString() + "',0,'','','" + Result + "'");
                            BEMobileResponse bEMobileResponse = new BEMobileResponse();
                            bEMobileResponse.status = false;
                            bEMobileResponse.message = Data["status"].ToString();
                            bEMobileResponse.ackno = "";
                            bEMobileResponse.amount = "0";
                            bEMobileResponse.balanceamount = "0";
                            bEMobileResponse.bankrrn = "";
                            bEMobileResponse.bankiin = bankno;
                            bEMobileResponse.response_code = Data["statuscode"].ToString();
                            bEMobileResponse.errorcode = "00";
                            bEMobileResponse.clientrefno = "";
                            bEMobileResponse.last_aadhar = "";
                            bEMobileResponse.name = "";
                            bEMobileResponse.OrderID = RefID;
                            bEMobileResponse.STID = "";
                            bEMobileResponse.Commission = "0";
                            bEMobileResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                            bEMobileResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                            bEMobileResponse.Aadhar = adhaarnumber;
                            bEMobileResponse.TransDate = DateTime.Now.ToString();
                            bEMobileResponse.TransactionID = RefID;

                            Response.Write(bEMobileResponse.GetJson());
                        }


                    }
                }
                else
                {
                    ErrorMessage("Enter Value");
                }
            }
            else
            {
                BEMobileResponse bEMobileResponse = new BEMobileResponse();
                bEMobileResponse.status = false;
                bEMobileResponse.message = "Onborading Not Complete";
                bEMobileResponse.ackno = "";
                bEMobileResponse.amount = "0";
                bEMobileResponse.balanceamount = "0";
                bEMobileResponse.bankrrn = "";
                bEMobileResponse.bankiin = bankno;
                bEMobileResponse.response_code = "";
                bEMobileResponse.errorcode = "00";
                bEMobileResponse.clientrefno = "";
                bEMobileResponse.last_aadhar = "";
                bEMobileResponse.name = "";
                bEMobileResponse.OrderID = "";
                bEMobileResponse.STID = "";
                bEMobileResponse.Commission = "0";
                bEMobileResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                bEMobileResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                bEMobileResponse.Aadhar = adhaarnumber;
                bEMobileResponse.TransDate = DateTime.Now.ToString();
                bEMobileResponse.TransactionID = "";

                Response.Write(bEMobileResponse.GetJson());
            }
        }
    }

    private void cashwithdraw(string UserID, string mobilenumber, string adhaarnumber, string bankno, string data, string Amount)
    {
        DataTable dtMember = objConnection.select_data_dt("select * from Member where mobile='" + UserID + "'");
        if (dtMember.Rows.Count > 0)
        {
            DataTable dtValc = objConnection.select_data_dt("select * from aepskyc where Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");
            if (dtValc.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(mobilenumber) && !string.IsNullOrEmpty(adhaarnumber))
                {
                    string RefID = System.Guid.NewGuid().ToString().Replace("-", "0");
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(HttpUtility.HtmlDecode(data));
                    string jsonText = JsonConvert.SerializeXmlNode(doc);
                    dynamic bsObj;

                    bsObj = JsonConvert.DeserializeObject<Mantra>(jsonText);
                    string aadhar = adhaarnumber.Substring(8, 4);
                    Int32 Val = objConnection.update_data("Exec Proc_Add_edit_AEPS " + dtMember.Rows[0]["msrno"].ToString() + ", '" + RefID + "','" + Amount + "', 'CW', " + bankno + ",'" + (("xxxx-xxxx-" + aadhar)) + "', '" + mobilenumber + "', 'Pending','','','','','App','','',''");
                    if (Val > 0)
                    {
                        InstantPayCWRoot bALANCEENQUIRY = new InstantPayCWRoot();
                        string iPAddress = cls_myMember.GetIPAddress();
                        Location location = new Location();
                        location = location.GetLocationDataCurrent(iPAddress);

                        bALANCEENQUIRY.latitude = location.Latitude;
                        bALANCEENQUIRY.longitude = location.Longitude;
                        bALANCEENQUIRY.amount = Amount;
                        bALANCEENQUIRY.bankiin = bankno;
                        bALANCEENQUIRY.mobile = mobilenumber;
                        bALANCEENQUIRY.externalRef = RefID;
                        InstantPayBEBody thumbRequest = new InstantPayBEBody();
                        thumbRequest.encryptedAadhaar = InstantPayout.GetEncryptAadhar(adhaarnumber);// bsObj.PidData.Skey.Ci;
                        thumbRequest.dc = bsObj.PidData.DeviceInfo.Dc;
                        thumbRequest.ci = bsObj.PidData.Skey.Ci;
                        thumbRequest.hmac = bsObj.PidData.Hmac;
                        thumbRequest.dpId = bsObj.PidData.DeviceInfo.DpId;
                        thumbRequest.mc = bsObj.PidData.DeviceInfo.Mc;
                        thumbRequest.pidDataType = bsObj.PidData.Data.Type;
                        thumbRequest.sessionKey = bsObj.PidData.Skey.Text;
                        thumbRequest.mi = bsObj.PidData.DeviceInfo.Mi;
                        thumbRequest.rdsId = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.errCode = bsObj.PidData.Resp.ErrCode;
                        thumbRequest.errInfo = bsObj.PidData.Resp.ErrInfo;
                        thumbRequest.fCount = bsObj.PidData.Resp.FCount;
                        thumbRequest.fType = bsObj.PidData.Resp.FType;
                        thumbRequest.iCount = 0;
                        thumbRequest.iType = "0";
                        thumbRequest.pCount = 0;
                        thumbRequest.pType = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.srno = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.sysid = "";
                        thumbRequest.ts = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.pidData = bsObj.PidData.Data.Text;
                        thumbRequest.qScore = bsObj.PidData.Resp.QScore;
                        thumbRequest.nmPoints = bsObj.PidData.Resp.NmPoints;
                        thumbRequest.rdsVer = bsObj.PidData.DeviceInfo.RdsVer;
                        bALANCEENQUIRY.biometricData = thumbRequest;

                        string Result = ApiPostCallInstantPay.PostCallWithFullURL(bALANCEENQUIRY.GetJson(), "https://api.instantpay.in/fi/aeps/cashWithdrawal", "", dtValc.Rows[0]["OutLetID"].ToString());

                        JObject Data = JObject.Parse(Result);

                        if (Data["statuscode"].ToString() == "TXN")
                        {
                            Val = objConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','CW','Success','" + Data["status"].ToString() + "',0,'" + Data["data"]["operatorId"].ToString() + "','','" + Result + "'");
                            if (Val > 0)
                            {
                                try
                                {
                                    string narration = "", Decs = "";
                                    narration = "Aeps TransID-" + RefID;
                                    Decs = $"AEPS Amount - {bALANCEENQUIRY.amount}. Done With Aadhar No {("xxxx-xxxx-" + aadhar)}. TransID - {RefID}";
                                    objConnection.update_data("exec PROC_AEPSWALLETTRANSACTION '" + dtMember.Rows[0]["LoginID"].ToString() + "','" + bALANCEENQUIRY.amount + "','CR','" + narration + "','" + Decs + "','" + ConstantsData.SAeps + "','" + RefID + "'");
                                    objConnection.update_data("Exec Proc_AEPS_DistributeCommission '" + dtMember.Rows[0]["msrno"].ToString() + "'," + bALANCEENQUIRY.amount + ", '" + RefID + "', '" + ConstantsData._TDS + "','" + ConstantsData.SAEPSCommission + "','" + ConstantsData._TDSVal + "'");
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            BEMobileResponse bEMobileResponse = new BEMobileResponse();
                            bEMobileResponse.status = true;
                            bEMobileResponse.message = Data["status"].ToString();
                            bEMobileResponse.ackno = Data["orderid"].ToString();
                            bEMobileResponse.amount = Amount;
                            bEMobileResponse.balanceamount = Data["data"]["bankAccountBalance"].ToString();
                            bEMobileResponse.bankrrn = Data["data"]["operatorId"].ToString();
                            bEMobileResponse.bankiin = bankno;
                            bEMobileResponse.response_code = Data["statuscode"].ToString();
                            bEMobileResponse.errorcode = "00";
                            bEMobileResponse.clientrefno = Data["data"]["ipayId"].ToString();
                            bEMobileResponse.last_aadhar = Data["data"]["accountNumber"].ToString();
                            bEMobileResponse.name = Data["data"]["bankName"].ToString();
                            bEMobileResponse.OrderID = RefID;
                            bEMobileResponse.STID = Data["data"]["walletIpayId"].ToString();
                            bEMobileResponse.Commission = "0";
                            bEMobileResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                            bEMobileResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                            bEMobileResponse.Aadhar = adhaarnumber;
                            bEMobileResponse.TransDate = DateTime.Now.ToString();
                            bEMobileResponse.TransactionID = RefID;

                            Response.Write(bEMobileResponse.GetJson());
                        }
                        else
                        {
                            Val = objConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','CW','Failed','" + Data["status"].ToString() + "',0,'','','" + Result + "'");
                            BEMobileResponse bEMobileResponse = new BEMobileResponse();
                            bEMobileResponse.status = false;
                            bEMobileResponse.message = Data["status"].ToString();
                            bEMobileResponse.ackno = "";
                            bEMobileResponse.amount = "0";
                            bEMobileResponse.balanceamount = "0";
                            bEMobileResponse.bankrrn = "";
                            bEMobileResponse.bankiin = bankno;
                            bEMobileResponse.response_code = Data["statuscode"].ToString();
                            bEMobileResponse.errorcode = "00";
                            bEMobileResponse.clientrefno = "";
                            bEMobileResponse.last_aadhar = "";
                            bEMobileResponse.name = "";
                            bEMobileResponse.OrderID = RefID;
                            bEMobileResponse.STID = "";
                            bEMobileResponse.Commission = "0";
                            bEMobileResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                            bEMobileResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                            bEMobileResponse.Aadhar = adhaarnumber;
                            bEMobileResponse.TransDate = DateTime.Now.ToString();
                            bEMobileResponse.TransactionID = RefID;

                            Response.Write(bEMobileResponse.GetJson());
                        }

                    }
                }
                else
                {
                    ErrorMessage("Enter Value");
                }
            }
            else
            {
                BEMobileResponse bEMobileResponse = new BEMobileResponse();
                bEMobileResponse.status = false;
                bEMobileResponse.message = "Onborading Not Complete";
                bEMobileResponse.ackno = "";
                bEMobileResponse.amount = "0";
                bEMobileResponse.balanceamount = "0";
                bEMobileResponse.bankrrn = "";
                bEMobileResponse.bankiin = bankno;
                bEMobileResponse.response_code = "";
                bEMobileResponse.errorcode = "00";
                bEMobileResponse.clientrefno = "";
                bEMobileResponse.last_aadhar = "";
                bEMobileResponse.name = "";
                bEMobileResponse.OrderID = "";
                bEMobileResponse.STID = "";
                bEMobileResponse.Commission = "0";
                bEMobileResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                bEMobileResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                bEMobileResponse.Aadhar = adhaarnumber;
                bEMobileResponse.TransDate = DateTime.Now.ToString();
                bEMobileResponse.TransactionID = "";

                Response.Write(bEMobileResponse.GetJson());
            }
        }
    }

    private void acashwithdraw(string UserID, string mobilenumber, string adhaarnumber, string bankno, string data, string Amount)
    {
        DataTable dtMember = objConnection.select_data_dt("select * from Member where mobile='" + UserID + "'");
        if (dtMember.Rows.Count > 0)
        {
            DataTable dtValc = objConnection.select_data_dt("select * from aepskyc where Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");
            if (dtValc.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(mobilenumber) && !string.IsNullOrEmpty(adhaarnumber))
                {
                    string RefID = System.Guid.NewGuid().ToString().Replace("-", "0");
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(HttpUtility.HtmlDecode(data));
                    string jsonText = JsonConvert.SerializeXmlNode(doc);
                    dynamic bsObj;

                    bsObj = JsonConvert.DeserializeObject<Mantra>(jsonText);
                    string aadhar = adhaarnumber.Substring(8, 4);
                    Int32 Val = objConnection.update_data("Exec Proc_Add_edit_Aadhar " + dtMember.Rows[0]["msrno"].ToString() + ", '" + RefID + "','" + Amount + "', 'CW', " + bankno + ",'" + ("xxxx-xxxx-" + aadhar) + "', '" + mobilenumber + "', 'Pending','','','','','Site','','',''");
                    if (Val > 0)
                    {
                        InstantPayCWRoot bALANCEENQUIRY = new InstantPayCWRoot();
                        string iPAddress = cls_myMember.GetIPAddress();
                        Location location = new Location();
                        location = location.GetLocationDataCurrent(iPAddress);

                        bALANCEENQUIRY.latitude = location.Latitude;
                        bALANCEENQUIRY.longitude = location.Longitude;
                        bALANCEENQUIRY.amount = Amount;
                        bALANCEENQUIRY.bankiin = bankno;
                        bALANCEENQUIRY.mobile = mobilenumber;
                        bALANCEENQUIRY.externalRef = RefID;
                        InstantPayBEBody thumbRequest = new InstantPayBEBody();
                        thumbRequest.encryptedAadhaar = InstantPayout.GetEncryptAadhar(adhaarnumber);// bsObj.PidData.Skey.Ci;
                        thumbRequest.dc = bsObj.PidData.DeviceInfo.Dc;
                        thumbRequest.ci = bsObj.PidData.Skey.Ci;
                        thumbRequest.hmac = bsObj.PidData.Hmac;
                        thumbRequest.dpId = bsObj.PidData.DeviceInfo.DpId;
                        thumbRequest.mc = bsObj.PidData.DeviceInfo.Mc;
                        thumbRequest.pidDataType = bsObj.PidData.Data.Type;
                        thumbRequest.sessionKey = bsObj.PidData.Skey.Text;
                        thumbRequest.mi = bsObj.PidData.DeviceInfo.Mi;
                        thumbRequest.rdsId = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.errCode = bsObj.PidData.Resp.ErrCode;
                        thumbRequest.errInfo = bsObj.PidData.Resp.ErrInfo;
                        thumbRequest.fCount = bsObj.PidData.Resp.FCount;
                        thumbRequest.fType = bsObj.PidData.Resp.FType;
                        thumbRequest.iCount = 0;
                        thumbRequest.iType = "0";
                        thumbRequest.pCount = 0;
                        thumbRequest.pType = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.srno = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.sysid = "";
                        thumbRequest.ts = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.pidData = bsObj.PidData.Data.Text;
                        thumbRequest.qScore = bsObj.PidData.Resp.QScore;
                        thumbRequest.nmPoints = bsObj.PidData.Resp.NmPoints;
                        thumbRequest.rdsVer = bsObj.PidData.DeviceInfo.RdsVer;
                        bALANCEENQUIRY.biometricData = thumbRequest;

                        string Result = ApiPostCallInstantPay.PostCallWithFullURL(bALANCEENQUIRY.GetJson(), "https://api.instantpay.in/fi/aeps/aadhaarPay", "", dtValc.Rows[0]["OutLetID"].ToString());
                        JObject Data = JObject.Parse(Result);

                        if (Data["statuscode"].ToString() == "TXN")
                        {
                            Val = objConnection.update_data("proc_Credit_Aadhar_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','CW','Success','" + Data["status"].ToString() + "',0,'" + Data["data"]["operatorId"].ToString() + "','','" + Result + "','" + dtMember.Rows[0]["PackageID"].ToString() + "'");
                            if (Val > 0)
                            {
                                objConnection.update_data("exec aadharpaycredit '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','" + dtMember.Rows[0]["loginid"].ToString() + "','" + dtMember.Rows[0]["PackageID"].ToString() + "'," + ConstantsData.SAadharPay + "");
                            }
                            BEMobileResponse bEMobileResponse = new BEMobileResponse();
                            bEMobileResponse.status = true;
                            bEMobileResponse.message = Data["status"].ToString();
                            bEMobileResponse.ackno = Data["orderid"].ToString();
                            bEMobileResponse.amount = Amount;
                            bEMobileResponse.balanceamount = Data["data"]["closingBalance"].ToString();
                            bEMobileResponse.bankrrn = Data["data"]["operatorId"].ToString();
                            bEMobileResponse.bankiin = bankno;
                            bEMobileResponse.response_code = Data["statuscode"].ToString();
                            bEMobileResponse.errorcode = "00";
                            bEMobileResponse.clientrefno = Data["data"]["ipayId"].ToString();
                            bEMobileResponse.last_aadhar = Data["data"]["accountNumber"].ToString();
                            bEMobileResponse.name = Data["data"]["bankName"].ToString();
                            bEMobileResponse.OrderID = RefID;
                            bEMobileResponse.STID = Data["data"]["walletIpayId"].ToString();
                            bEMobileResponse.Commission = "0";
                            bEMobileResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                            bEMobileResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                            bEMobileResponse.Aadhar = adhaarnumber;
                            bEMobileResponse.TransDate = DateTime.Now.ToString();
                            bEMobileResponse.TransactionID = RefID;

                            Response.Write(bEMobileResponse.GetJson());
                        }
                        else
                        {
                            Val = objConnection.update_data("proc_Credit_Aadhar_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','CW','Failed','" + Data["status"].ToString() + "',0,'','','" + Result + "','" + dtMember.Rows[0]["PackageID"].ToString() + "'");
                            BEMobileResponse bEMobileResponse = new BEMobileResponse();
                            bEMobileResponse.status = false;
                            bEMobileResponse.message = Data["status"].ToString();
                            bEMobileResponse.ackno = "";
                            bEMobileResponse.amount = "0";
                            bEMobileResponse.balanceamount = "0";
                            bEMobileResponse.bankrrn = "";
                            bEMobileResponse.bankiin = bankno;
                            bEMobileResponse.response_code = Data["statuscode"].ToString();
                            bEMobileResponse.errorcode = "00";
                            bEMobileResponse.clientrefno = "";
                            bEMobileResponse.last_aadhar = "";
                            bEMobileResponse.name = "";
                            bEMobileResponse.OrderID = RefID;
                            bEMobileResponse.STID = "";
                            bEMobileResponse.Commission = "0";
                            bEMobileResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                            bEMobileResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                            bEMobileResponse.Aadhar = adhaarnumber;
                            bEMobileResponse.TransDate = DateTime.Now.ToString();
                            bEMobileResponse.TransactionID = RefID;

                            Response.Write(bEMobileResponse.GetJson());
                        }


                    }
                }
                else
                {
                    ErrorMessage("Enter Value");
                }
            }
            else
            {
                BEMobileResponse bEMobileResponse = new BEMobileResponse();
                bEMobileResponse.status = false;
                bEMobileResponse.message = "Onborading Not Complete";
                bEMobileResponse.ackno = "";
                bEMobileResponse.amount = "0";
                bEMobileResponse.balanceamount = "0";
                bEMobileResponse.bankrrn = "";
                bEMobileResponse.bankiin = bankno;
                bEMobileResponse.response_code = "";
                bEMobileResponse.errorcode = "00";
                bEMobileResponse.clientrefno = "";
                bEMobileResponse.last_aadhar = "";
                bEMobileResponse.name = "";
                bEMobileResponse.OrderID = "";
                bEMobileResponse.STID = "";
                bEMobileResponse.Commission = "0";
                bEMobileResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                bEMobileResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                bEMobileResponse.Aadhar = adhaarnumber;
                bEMobileResponse.TransDate = DateTime.Now.ToString();
                bEMobileResponse.TransactionID = "";

                Response.Write(bEMobileResponse.GetJson());
            }
        }
    }

    private void ministatement(string UserID, string mobilenumber, string adhaarnumber, string bankno, string data)
    {
        DataTable dtMember = objConnection.select_data_dt("select * from Member where mobile='" + UserID + "'");
        if (dtMember.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(mobilenumber) && !string.IsNullOrEmpty(adhaarnumber))
            {
                string RefID = System.Guid.NewGuid().ToString().Replace("-", "0");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(HttpUtility.HtmlDecode(data));
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                dynamic bsObj;

                bsObj = JsonConvert.DeserializeObject<Mantra>(jsonText);
                string aadhar = adhaarnumber.Substring(8, 4);
                Int32 Val = objConnection.update_data("Exec Proc_Add_edit_AEPS " + dtMember.Rows[0]["msrno"].ToString() + ", '" + RefID + "','0', 'MS', " + bankno + ",'" + (("xxxx-xxxx-" + aadhar)) + "', '" + mobilenumber + "', 'Pending','','','','','App','','',''");
                if (Val > 0)
                {
                    InstantPayBERoot bALANCEENQUIRY = new InstantPayBERoot();
                    string iPAddress = cls_myMember.GetIPAddress();
                    Location location = new Location();
                    location = location.GetLocationDataCurrent(iPAddress);

                    bALANCEENQUIRY.latitude = location.Latitude;
                    bALANCEENQUIRY.longitude = location.Longitude;
                    bALANCEENQUIRY.bankiin = bankno;
                    bALANCEENQUIRY.mobile = mobilenumber;
                    bALANCEENQUIRY.externalRef = RefID;
                    InstantPayBEBody thumbRequest = new InstantPayBEBody();
                    thumbRequest.encryptedAadhaar = InstantPayout.GetEncryptAadhar(adhaarnumber);// bsObj.PidData.Skey.Ci;
                    thumbRequest.dc = bsObj.PidData.DeviceInfo.Dc;
                    thumbRequest.ci = bsObj.PidData.Skey.Ci;
                    thumbRequest.hmac = bsObj.PidData.Hmac;
                    thumbRequest.dpId = bsObj.PidData.DeviceInfo.DpId;
                    thumbRequest.mc = bsObj.PidData.DeviceInfo.Mc;
                    thumbRequest.pidDataType = bsObj.PidData.Data.Type;
                    thumbRequest.sessionKey = bsObj.PidData.Skey.Text;
                    thumbRequest.mi = bsObj.PidData.DeviceInfo.Mi;
                    thumbRequest.rdsId = bsObj.PidData.DeviceInfo.RdsId;
                    thumbRequest.errCode = bsObj.PidData.Resp.ErrCode;
                    thumbRequest.errInfo = bsObj.PidData.Resp.ErrInfo;
                    thumbRequest.fCount = bsObj.PidData.Resp.FCount;
                    thumbRequest.fType = bsObj.PidData.Resp.FType;
                    thumbRequest.iCount = 0;
                    thumbRequest.iType = "0";
                    thumbRequest.pCount = 0;
                    thumbRequest.pType = bsObj.PidData.DeviceInfo.RdsId;
                    thumbRequest.srno = bsObj.PidData.DeviceInfo.RdsId;
                    thumbRequest.sysid = "";
                    thumbRequest.ts = bsObj.PidData.DeviceInfo.RdsId;
                    thumbRequest.pidData = bsObj.PidData.Data.Text;
                    thumbRequest.qScore = bsObj.PidData.Resp.QScore;
                    thumbRequest.nmPoints = bsObj.PidData.Resp.NmPoints;
                    thumbRequest.rdsVer = bsObj.PidData.DeviceInfo.RdsVer;
                    bALANCEENQUIRY.biometricData = thumbRequest;
                    DataTable dtValc = objConnection.select_data_dt("select * from aepskyc where Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");
                    if (dtValc.Rows.Count > 0)
                    {
                        string Result = ApiPostCallInstantPay.PostCallWithFullURL(bALANCEENQUIRY.GetJson(), "https://api.instantpay.in/fi/aeps/miniStatement", "", dtValc.Rows[0]["OutLetID"].ToString());

                        JObject Data = JObject.Parse(Result);

                        if (Data["statuscode"].ToString() == "TXN")
                        {
                            Val = objConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','BE','Success','" + Data["status"].ToString() + "',0,'" + Data["data"]["operatorId"].ToString() + "','','" + Result + "'");
                            if (Val > 0)
                            {
                            }
                            MinistatementBackResponsemobileRoot bEMobileResponse = new MinistatementBackResponsemobileRoot();
                            bEMobileResponse.status = true;
                            bEMobileResponse.message = Data["status"].ToString();
                            bEMobileResponse.ackno = Data["orderid"].ToString();

                            bEMobileResponse.balanceamount = Data["data"]["bankAccountBalance"].ToString();
                            bEMobileResponse.bankrrn = Data["data"]["operatorId"].ToString();
                            bEMobileResponse.bankiin = bankno;
                            bEMobileResponse.response_code = Data["statuscode"].ToString();
                            bEMobileResponse.errorcode = "00";
                            MinistatementBackResponsemobileRoot st = JsonConvert.DeserializeObject<MinistatementBackResponsemobileRoot>(Data["data"].ToString());
                            bEMobileResponse.ministatement = st.ministatement;
                            bEMobileResponse.clientrefno = Data["data"]["ipayId"].ToString();
                            bEMobileResponse.last_aadhar = Data["data"]["accountNumber"].ToString();
                            bEMobileResponse.name = Data["data"]["bankName"].ToString();
                            bEMobileResponse.OrderID = RefID;
                            bEMobileResponse.STID = Data["data"]["walletIpayId"].ToString();
                            bEMobileResponse.Commission = "0";
                            bEMobileResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                            bEMobileResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                            bEMobileResponse.Aadhar = adhaarnumber;
                            bEMobileResponse.TransDate = DateTime.Now.ToString();
                            bEMobileResponse.TransactionID = RefID;

                            Response.Write(bEMobileResponse.GetJson());
                        }
                        else
                        {
                            Val = objConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','MS','Failed','" + Data["status"].ToString() + "',0,'','','" + Result + "'");
                            BEMobileResponse bEMobileResponse = new BEMobileResponse();
                            bEMobileResponse.status = false;
                            bEMobileResponse.message = Data["status"].ToString();
                            bEMobileResponse.ackno = "";
                            bEMobileResponse.amount = "0";
                            bEMobileResponse.balanceamount = "0";
                            bEMobileResponse.bankrrn = "";
                            bEMobileResponse.bankiin = bankno;
                            bEMobileResponse.response_code = Data["statuscode"].ToString();
                            bEMobileResponse.errorcode = "00";
                            bEMobileResponse.clientrefno = "";
                            bEMobileResponse.last_aadhar = "";
                            bEMobileResponse.name = "";
                            bEMobileResponse.OrderID = RefID;
                            bEMobileResponse.STID = "";
                            bEMobileResponse.Commission = "0";
                            bEMobileResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                            bEMobileResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                            bEMobileResponse.Aadhar = adhaarnumber;
                            bEMobileResponse.TransDate = DateTime.Now.ToString();
                            bEMobileResponse.TransactionID = RefID;

                            Response.Write(bEMobileResponse.GetJson());
                        }
                    }

                }
            }
            else
            {
                ErrorMessage("Enter Value");
            }
        }
    }

}