using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Xml;

public partial class Member_aepsBank : System.Web.UI.Page
{
    DataTable dtMember = new DataTable();
    cls_connection ObjConnection = new cls_connection();

    Page page; string ipAddress = "";
    public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            ipAddress = ConstantsData.getipAddress();
            if (!IsPostBack)
            {
                var var = Common.GetServiceStatus("17", ObjConnection);
                if (var.Item1 == true)
                {
                    Int32 Valc = ObjConnection.select_data_scalar_int("select count(*) from Member where loginid='" + dtMember.Rows[0]["loginid"].ToString() + "' and ActivedForRnfi=0");
                    //if (Valc == 1)
                    //{
                    //    ErrorShow.AlertMessageWithRedirect(page, "Your KYC Not Complete, Upload Your KYC", "AEPSOnboard.aspx", ConstantsData.CompanyName);
                    //}
                    ObjConnection.update_data("AddEditAEPSOnBoardStatus " + dtMember.Rows[0]["msrno"].ToString() + "");
                }
                else
                {
                    ErrorShow.ErrorNotify(page, var.Item2);
                }
            }
        }
    }

    public DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }

    protected void btnCW_Click(object sender, EventArgs e)
    {

    }
    protected void btnexportPdf_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=500px,width=650px,scrollbars=1');</script>");
    }

    [WebMethod]
    public static string GetCommission(string Amt)
    {
        cls_connection cls = new cls_connection();
        DataTable dtm = (DataTable)HttpContext.Current.Session["dtMember"];
        string com = cls.select_data_scalar_string("select  dbo.GetAEPSCommission(" + Amt + "," + dtm.Rows[0]["MSrno"].ToString() + ")");
        return com;
    }

    [WebMethod]
    public static string GetBankDetails()
    {

        BankList bankList = new BankList();
        string bankListstr = ApiPostCall.PostCall(bankList.GetJson(), SONITechnoCredentrial.AEPSCore);
        JObject jData = JObject.Parse(bankListstr);
        if (jData["code"].ToString().ToUpper() == "TXN")
        {
            return jData["data"].ToString();
        }
        else
        {
            return "";
        }
    }

    [WebMethod]
    public static List<AEPSResponse> DOAEPS(AEPS aEPS, string pipe)
    {
        List<AEPSResponse> dd = new List<AEPSResponse>();
        AEPSResponse aEPSResponse = new AEPSResponse();
        cls_connection ObjConnection = new cls_connection();
        ObjConnection.update_data("insert into logdata(data)values('1')");
        DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
        string aadhar = aEPS.AadharNo.Substring(8, 4);
        string ipAddress = ConstantsData.getipAddress();
        string RefID = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + dtMember.Rows[0]["msrno"].ToString();
        Int32 Val = ObjConnection.update_data("Exec Proc_Add_edit_AEPS " + dtMember.Rows[0]["msrno"].ToString() + ", '" + RefID + "','" + aEPS.Amount + "', '" + aEPS.Type + "', " + aEPS.BankID.id + ",'" + (("xxxx-xxxx-" + aadhar)) + "', '" + aEPS.Mobile + "', 'Pending','','" + aEPS.DeviceName + "','','','Site','','','" + ipAddress + "'");
        if (Val > 0)
        {

            if (pipe.ToUpper() == "BANK6")
            {

                DataTable dthisb = ObjConnection.select_data_dt("select * from AEPSOnBoardStatus where msrno=" + dtMember.Rows[0]["msrno"].ToString() + " and name='bank6'");
                if (dthisb.Rows.Count > 0)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(HttpUtility.HtmlDecode(aEPS.Capture));
                    string jsonText = JsonConvert.SerializeXmlNode(doc);
                    dynamic bsObj;
                    if (aEPS.DeviceName.ToUpper() == "MORPHO")
                        bsObj = JsonConvert.DeserializeObject<RooteeeMorpho>(jsonText);
                    else
                        bsObj = JsonConvert.DeserializeObject<Mantra>(jsonText);


                    if (doc.FirstChild is XmlDeclaration)
                    {
                        // Remove the XmlDeclaration
                        doc.RemoveChild(doc.FirstChild);
                    }
                    string modifiedXml = doc.OuterXml;

                    CWMSBECredopay authc = new CWMSBECredopay();
                    authc.bank_name = aEPS.BankID.id.ToString();
                    authc.amount = "0.00";
                    authc.CRN_U = RefID;
                    authc.custom_field1 = RefID;
                    authc.hashed_aadhar = Credopay.ComputeMD5(aEPS.AadharNo);
                    authc.aadhar = Credopay.EncryptJavaEqualent(Credopay.AepsKey.Replace("-", ""), aEPS.AadharNo);
                    authc.pid_data = modifiedXml;
                    authc.masked_aadhar = aEPS.AadharNo.Substring(0, 6) + "XXXX" + aEPS.AadharNo.Substring(8, 4);
                    authc.cpId = dthisb.Rows[0]["AEPSID"].ToString();
                    authc.transaction_group = "aeps";
                    if (aEPS.Type == "BE")
                        authc.transaction_type = "balance_enquiry";
                    else if (aEPS.Type == "MS")
                        authc.transaction_type = "mini_statement";
                    else if (aEPS.Type == "CW")
                    {
                        authc.amount = aEPS.Amount.ToString();
                        authc.transaction_type = "cash_withdrawal";
                        authc.auth_reference_no = ObjConnection.select_data_scalar_string("select MerAuthTxnId from AEPSOnBoardStatus where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and  Name='" + pipe + "' and IsUserMerAuthTxnId=0");
                    }
                    authc.payment_method = "biometric";

                    authc.biometric_srno = bsObj.PidData.DeviceInfo.RdsId;
                    authc.biometric_rdsVer = bsObj.PidData.DeviceInfo.RdsVer;
                    authc.biometric_rdsId = bsObj.PidData.DeviceInfo.RdsId;
                    authc.biometric_dpId = bsObj.PidData.DeviceInfo.DpId;
                    authc.biometric_mi = bsObj.PidData.DeviceInfo.Mi;
                    authc.operating_system = "windows";
                    authc.transaction_origin_ip = "103.76.250.2";
                    authc.latitude = "19.2563";
                    authc.longitude = "19.2563";
                    authc.customer_mobile = aEPS.Mobile;
                    authc.virtual_ID = "";

                    string Request = Newtonsoft.Json.JsonConvert.SerializeObject(authc);
                    ObjConnection.update_data("insert into logdata(data)values('" + Request + "')");
                    string ddddd = Credopay.EncryptJavaEqualent(Credopay.AepsSecretkey.Replace("-", ""), Request);
                    string Result = Credopay.PostCallWithBody("aeps/send", ddddd);
                    JObject ju = JObject.Parse(Result);
                    ObjConnection.update_data("insert into logdata(data)values('" + Result + "')");
                    if (ju["response_description"].ToString().ToUpper() == "APPROVED" || ju["response_description"].ToString().ToUpper() == "TXN DONE SUCCESSFULLY")
                    {
                        aEPSResponse.Img = "../img/RightMark.png";
                        aEPSResponse.Message = "Cash Withdrawal Success";
                        aEPSResponse.MobileNo = aEPS.Mobile;
                        aEPSResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                        aEPSResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                        aEPSResponse.TransID = RefID;
                        aEPSResponse.Amount = aEPS.Amount.ToString();
                        aEPSResponse.AccountBalance = (ju["balance"] == null ? "0" : ju["balance"].ToString());
                        aEPSResponse.BankName = aEPS.BankID.text;// Data["data"]["bankrrn"].ToString();
                        aEPSResponse.BankNumber = ju["rrn"].ToString();
                        aEPSResponse.Reason = ju["response_status"].ToString();
                        aEPSResponse.AadharNo = ("xxxx-xxxx-" + aadhar);
                        aEPSResponse.Date = System.DateTime.Now.ToString();
                        Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','CW','Success','" + ju["response_status"].ToString() + "'," + aEPS.Amount + ",'" + ju["rrn"].ToString() + "','" + ju["transaction_id"].ToString() + "','" + Result + "'");
                        if (aEPS.Type == "CW")
                        {
                            Result = Credopay.PostCallWithoutEn("aeps/complete", "{\"transaction_id\":\"" + ju["transaction_id"].ToString() + "\"}");
                            ObjConnection.update_data("insert into logdata(data)values('" + Result + "')");

                            ObjConnection.update_data("update AEPSOnBoardStatus set IsUserMerAuthTxnId=1 where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and name='" + pipe + "'");
                            string narration = "", Decs = "";
                            narration = "Aeps TransID-" + RefID;
                            Decs = $"AEPS Amount - {aEPSResponse.Amount}. Done With Aadhar No {("xxxx-xxxx-" + aadhar)}. TransID - {RefID}";
                            ObjConnection.update_data("exec PROC_AEPSWALLETTRANSACTION '" + dtMember.Rows[0]["LoginID"].ToString() + "','" + aEPSResponse.Amount + "','CR','" + narration + "','" + Decs + "','" + ConstantsData.SAeps + "','" + RefID + "'");
                            ObjConnection.update_data("Exec Proc_AEPS_DistributeCommission '" + dtMember.Rows[0]["msrno"].ToString() + "'," + aEPSResponse.Amount + " , '" + RefID + "', '" + ConstantsData._TDS + "','" + ConstantsData.SAEPSCommission + "','" + ConstantsData._TDSVal + "'");

                        }

                        aEPS.Capture = "";
                    }
                    else
                    {
                        aEPSResponse.Img = "../img/Cross.png";
                        aEPSResponse.Message = "Cash Withdrawal failed";
                        aEPSResponse.MobileNo = aEPS.Mobile;
                        aEPSResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                        aEPSResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                        aEPSResponse.TransID = RefID;
                        aEPSResponse.Amount = "0";
                        aEPSResponse.AccountBalance = "";
                        aEPSResponse.BankName = aEPS.BankID.text;
                        aEPSResponse.Reason = ju["response_description"].ToString();
                        aEPSResponse.AadharNo = ("xxxx-xxxx-" + aadhar);
                        aEPSResponse.Date = System.DateTime.Now.ToString();
                        Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','BE','Failed','" + ju["response_description"].ToString() + "',0,'','','" + Result + "'");
                        aEPS.Capture = "";
                    }
                }
                else
                {
                    aEPSResponse.Img = "../img/Cross.png";
                    aEPSResponse.Message = "Cash Withdrawal failed";
                    aEPSResponse.MobileNo = aEPS.Mobile;
                    aEPSResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                    aEPSResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                    aEPSResponse.TransID = "";
                    aEPSResponse.Amount = "";
                    aEPSResponse.AccountBalance = "";
                    aEPSResponse.BankName = aEPS.BankID.text;
                    aEPSResponse.Reason = "TID Not Found";
                    aEPSResponse.AadharNo = ("xxxx-xxxx-" + aadhar);
                    aEPSResponse.Date = System.DateTime.Now.ToString();
                }
            }
            else
            {
                CommonRequestSoni cashWithdraw = new CommonRequestSoni();
                cashWithdraw.mobilenumber = aEPS.Mobile;
                cashWithdraw.adhaarnumber = aEPS.AadharNo;
                cashWithdraw.BankID = aEPS.BankID.id.ToString();
                cashWithdraw.data = HttpUtility.HtmlDecode(aEPS.Capture);
                cashWithdraw.OrderID = RefID;
                cashWithdraw.Amount = aEPS.Amount.ToString();
                cashWithdraw.accessmodetype = "SITE";
                cashWithdraw.submerchantid = SONITechnoCredentrial.Prefix + dtMember.Rows[0]["LoginID"].ToString();

                if (aEPS.Type == "CW")
                    cashWithdraw.MethodName = "cw";
                else if (aEPS.Type == "BE")
                {
                    cashWithdraw.MethodName = "be";
                    cashWithdraw.Amount = "0";
                }
                else if (aEPS.Type == "MS")
                {
                    cashWithdraw.MethodName = "ms";
                    cashWithdraw.Amount = "0";
                }


                if (pipe.ToUpper() == "BANK2" && aEPS.Type == "CW")
                {
                    cashWithdraw.MethodName = "cwauth";
                    cashWithdraw.MerAuthTxnId = ObjConnection.select_data_scalar_string("select MerAuthTxnId from AEPSOnBoardStatus where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and Name='" + pipe + "' and IsUserMerAuthTxnId=0");
                }
                else if (pipe.ToUpper() == "BANK2" && aEPS.Type == "MS")
                    cashWithdraw.MethodName = "ms";
                else if (pipe.ToUpper() == "BANK2" && aEPS.Type == "BE")
                    cashWithdraw.MethodName = "be";

                if (pipe.ToUpper() == "BANK3" && aEPS.Type == "CW")
                {
                    cashWithdraw.MethodName = "cw3auth";
                    cashWithdraw.MerAuthTxnId = ObjConnection.select_data_scalar_string("select MerAuthTxnId from AEPSOnBoardStatus where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and  Name='" + pipe + "' and IsUserMerAuthTxnId=0");
                }
                else if (pipe.ToUpper() == "BANK3" && aEPS.Type == "MS")
                    cashWithdraw.MethodName = "ms3";
                else if (pipe.ToUpper() == "BANK3" && aEPS.Type == "BE")
                    cashWithdraw.MethodName = "be3";


                string result = ApiPostCall.PostCall(cashWithdraw.GetJson(), SONITechnoCredentrial.AEPSCore);
                JObject Data = JObject.Parse(result);
                if (Data["code"].ToString().ToUpper() == "TXN")
                {
                    if (Data["data"]["status"].ToString().ToUpper() == "TRUE")
                    {
                        aEPSResponse.Img = "../img/RightMark.png";
                        aEPSResponse.Message = "Cash Withdrawal Success";
                        aEPSResponse.MobileNo = cashWithdraw.mobilenumber;
                        aEPSResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                        aEPSResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                        aEPSResponse.TransID = cashWithdraw.OrderID;
                        aEPSResponse.Amount = cashWithdraw.Amount;
                        aEPSResponse.AccountBalance = Data["data"]["balanceamount"].ToString();
                        aEPSResponse.BankName = aEPS.BankID.text;// Data["data"]["bankrrn"].ToString();
                        aEPSResponse.BankNumber = Data["data"]["bankrrn"].ToString();
                        aEPSResponse.Reason = Data["data"]["message"].ToString();
                        aEPSResponse.AadharNo = ("xxxx-xxxx-" + aadhar);
                        aEPSResponse.Date = System.DateTime.Now.ToString();
                        if (aEPS.Type == "MS")
                        {
                            List<Ministatement> ministatement = new List<Ministatement>();
                            ministatement = JsonConvert.DeserializeObject<List<Ministatement>>(Data["data"]["ministatement"].ToString());
                            aEPSResponse.ministatement = ministatement;
                        }

                        Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','CW','Success','" + Data["data"]["message"].ToString() + "'," + aEPS.Amount + ",'" + Data["data"]["bankrrn"].ToString() + "','" + Data["data"]["ackno"].ToString() + "','" + result + "'");
                        try
                        {
                            if (aEPS.Type == "CW")
                            {
                                ObjConnection.update_data("update AEPSOnBoardStatus set IsUserMerAuthTxnId=1 where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and name='" + pipe + "'");
                                string narration = "", Decs = "";
                                narration = "Aeps TransID-" + RefID;
                                Decs = $"AEPS Amount - {cashWithdraw.Amount}. Done With Aadhar No {("xxxx-xxxx-" + aadhar)}. TransID - {RefID}";
                                ObjConnection.update_data("exec PROC_AEPSWALLETTRANSACTION '" + dtMember.Rows[0]["LoginID"].ToString() + "','" + cashWithdraw.Amount + "','CR','" + narration + "','" + Decs + "','" + ConstantsData.SAeps + "','" + RefID + "'");
                                ObjConnection.update_data("Exec Proc_AEPS_DistributeCommission '" + dtMember.Rows[0]["msrno"].ToString() + "'," + cashWithdraw.Amount + " , '" + RefID + "', '" + ConstantsData._TDS + "','" + ConstantsData.SAEPSCommission + "','" + ConstantsData._TDSVal + "'");
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        aEPS.Capture = "";

                    }
                    else
                    {
                        ObjConnection.update_data("update AEPSOnBoardStatus set IsUserMerAuthTxnId=1 where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and name='" + pipe + "'");

                        aEPSResponse.Img = "../img/Cross.png";
                        aEPSResponse.Message = "Cash Withdrawal failed";
                        aEPSResponse.MobileNo = cashWithdraw.mobilenumber;
                        aEPSResponse.BCCode = dtMember.Rows[0]["LoginID"].ToString();
                        aEPSResponse.BCName = dtMember.Rows[0]["Name"].ToString();
                        aEPSResponse.TransID = cashWithdraw.OrderID;
                        aEPSResponse.Amount = cashWithdraw.Amount;
                        aEPSResponse.AccountBalance = "";
                        aEPSResponse.BankName = aEPS.BankID.text;
                        aEPSResponse.Reason = Data["data"]["message"].ToString();
                        aEPSResponse.AadharNo = ("xxxx-xxxx-" + aadhar);
                        aEPSResponse.Date = System.DateTime.Now.ToString();
                        Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','CW','Failed','" + Data["data"]["message"].ToString() + "',0,'','','" + result + "'");
                        aEPS.Capture = "";
                    }
                }
                else
                {
                    aEPSResponse.Message = "";
                }
            }
        }

        dd.Add(aEPSResponse);
        return dd;
    }


    [WebMethod]
    public static string MerchantRegistration(AEPSTWORegistration aEPS, string pipe)
    {
        cls_connection ObjConnection = new cls_connection();
        DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
        string ipAddress = ConstantsData.getipAddress();

        string RefID = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + dtMember.Rows[0]["msrno"].ToString();


        aEPS.accessmodetype = "SITE";
        aEPS.mobilenumber = dtMember.Rows[0]["Mobile"].ToString();
        aEPS.adhaarnumber = dtMember.Rows[0]["Aadhar"].ToString();
        aEPS.latitude = dtMember.Rows[0]["Latitude"].ToString();
        aEPS.longitude = dtMember.Rows[0]["Longitude"].ToString();
        aEPS.referenceno = RefID;

        if (pipe.ToUpper() == "BANK2")
            aEPS.MethodName = "registration";
        else
            aEPS.MethodName = "registration3";

        aEPS.timestamp = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
        aEPS.data = HttpUtility.HtmlDecode(aEPS.data);
        aEPS.ipaddress = ipAddress;
        aEPS.submerchantid = SONITechnoCredentrial.Prefix + dtMember.Rows[0]["LoginID"].ToString();

        string result = ApiPostCall.PostCall(aEPS.GetJson(), SONITechnoCredentrial.AEPSCore);
        JObject Data = JObject.Parse(result);
        if (Data["code"].ToString().ToUpper() == "TXN")
        {
            ObjConnection.update_data("update AEPSOnBoardStatus set AEPSTwoWayRegistration=1 where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and name='" + pipe + "'");
            return ErrorShow.SuccessArray(Data["data"].ToString());
        }
        else
        {
            return ErrorShow.ErrorMessage(Data["mess"].ToString());
        }




    }

    [WebMethod]
    public static string AuthenticateRegistration(AEPSTWORegistration aEPS, string pipe)
    {
        cls_connection ObjConnection = new cls_connection();
        DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
        string ipAddress = ConstantsData.getipAddress();
        string RefID = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + dtMember.Rows[0]["msrno"].ToString();

        ObjConnection.update_data("insert into logdata(data)values('1')");
        if (pipe.ToUpper() == "BANK6")
        {
            DataTable dthisb = ObjConnection.select_data_dt("select * from AEPSOnBoardStatus where msrno=" + dtMember.Rows[0]["msrno"].ToString() + " and name='bank6'");
            if (dthisb.Rows.Count > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(HttpUtility.HtmlDecode(aEPS.data));

                ObjConnection.update_data("insert into logdata(data)values('" + HttpUtility.HtmlDecode(aEPS.data) + "')");
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                dynamic bsObj;
                //if (hd.ToUpper() == "MORPHO")
                //bsObj = JsonConvert.DeserializeObject<RooteeeMorpho>(jsonText);
                //else
                bsObj = JsonConvert.DeserializeObject<Mantra>(jsonText);
                if (doc.FirstChild is XmlDeclaration)
                {
                    // Remove the XmlDeclaration
                    doc.RemoveChild(doc.FirstChild);
                }
                string modifiedXml = doc.OuterXml;

                ObjConnection.update_data("insert into logdata(data)values('2')");


                AuthenticationCredopay authc = new AuthenticationCredopay();
                authc.hashed_aadhar = Credopay.ComputeMD5(dtMember.Rows[0]["Aadhar"].ToString());


                authc.aadhar = Credopay.EncryptJavaEqualent(Credopay.AepsKey.Replace("-", ""), dtMember.Rows[0]["Aadhar"].ToString());

                ObjConnection.update_data("insert into logdata(data)values('" + authc.aadhar + "')");
                string Requdddest = Newtonsoft.Json.JsonConvert.SerializeObject(bsObj.PidData);
                authc.pid_data = modifiedXml;
                authc.masked_aadhar = dtMember.Rows[0]["Aadhar"].ToString().Substring(0, 6) + "XXXX" + dtMember.Rows[0]["Aadhar"].ToString().Substring(8, 4);
                authc.cpId = dthisb.Rows[0]["AEPSID"].ToString();
                authc.transaction_group = "aeps";
                authc.transaction_type = "Authentication";
                authc.payment_method = "biometric";

                authc.biometric_srno = bsObj.PidData.DeviceInfo.RdsId;
                authc.biometric_rdsVer = bsObj.PidData.DeviceInfo.RdsVer;
                authc.biometric_rdsId = bsObj.PidData.DeviceInfo.RdsId;
                authc.biometric_dpId = bsObj.PidData.DeviceInfo.DpId;
                authc.biometric_mi = bsObj.PidData.DeviceInfo.Mi;
                authc.operating_system = "windows";
                authc.transaction_origin_ip = "103.76.250.2";
                authc.latitude = "19.2563";
                authc.longitude = "19.2563";
                authc.customer_mobile = dtMember.Rows[0]["mobile"].ToString();
                authc.virtual_ID = "";

                string Request = Newtonsoft.Json.JsonConvert.SerializeObject(authc);
                string dd = Credopay.EncryptJavaEqualent(Credopay.AepsSecretkey.Replace("-", ""), Request);
                string Result = Credopay.PostCallWithBody("aeps/authentication", dd);
                ObjConnection.update_data("insert into logdata(data)values('" + Request + "')");
                ObjConnection.update_data("insert into logdata(data)values('" + dd + "')");
                JObject hh = JObject.Parse(Result);

                ObjConnection.update_data("insert into logdata(data)values('" + Result + "')");
                if (hh["message"] == null)
                {
                    if (hh["response_code"].ToString() == "00")
                    {
                        ObjConnection.update_data("update AEPSOnBoardStatus set DailyAuthentication=1,AuthDate=getdate() where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and Name='" + pipe + "'");
                        return ErrorShow.SuccessArray(hh["response_status"].ToString());
                    }
                    else
                    {
                        return ErrorShow.ErrorMessage(hh["response_description"].ToString());
                    }
                }
                else
                {
                    if (hh["error"] == null)
                        return ErrorShow.ErrorMessage(hh["message"].ToString());
                    else
                        return ErrorShow.ErrorMessage(hh["error"].ToString());
                }
            }
            else
            {
                return ErrorShow.ErrorMessage("TID Not Found");
            }
        }
        else
        {
            aEPS.accessmodetype = "SITE";
            aEPS.mobilenumber = dtMember.Rows[0]["Mobile"].ToString();
            aEPS.adhaarnumber = dtMember.Rows[0]["Aadhar"].ToString();
            aEPS.latitude = dtMember.Rows[0]["Latitude"].ToString();
            aEPS.longitude = dtMember.Rows[0]["Longitude"].ToString();
            aEPS.referenceno = RefID;

            if (pipe.ToUpper() == "BANK2")
                aEPS.MethodName = "authenticate";
            else
                aEPS.MethodName = "authenticate3";

            aEPS.timestamp = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
            aEPS.data = HttpUtility.HtmlDecode(aEPS.data);
            aEPS.ipaddress = ipAddress;
            aEPS.submerchantid = SONITechnoCredentrial.Prefix + dtMember.Rows[0]["LoginID"].ToString();

            string result = ApiPostCall.PostCall(aEPS.GetJson(), SONITechnoCredentrial.AEPSCore);
            ObjConnection.update_data("insert into logdata(data)values('" + result + "')");
            JObject Data = JObject.Parse(result);
            if (Data["code"].ToString().ToUpper() == "TXN")
            {
                ObjConnection.update_data("update AEPSOnBoardStatus set DailyAuthentication=1,AuthDate=getdate() where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and Name='" + pipe + "'");

                return ErrorShow.SuccessArray(Data["data"].ToString());
            }
            else
            {
                return ErrorShow.ErrorMessage(Data["mess"].ToString());
            }
        }
    }

    [WebMethod]
    public static string AuthenticateRegistrationCW(AEPSTWORegistration aEPS, string pipe)
    {
        cls_connection ObjConnection = new cls_connection();
        DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
        string ipAddress = ConstantsData.getipAddress();
        string RefID = DateTime.Now.ToString("ddMMyyyyHHmmssfff") + dtMember.Rows[0]["msrno"].ToString();

        if (pipe.ToUpper() == "BANK6")
        {

            DataTable dthisb = ObjConnection.select_data_dt("select * from AEPSOnBoardStatus where msrno=" + dtMember.Rows[0]["msrno"].ToString() + " and name='bank6'");
            if (dthisb.Rows.Count > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(HttpUtility.HtmlDecode(aEPS.data));
                string jsonText = JsonConvert.SerializeXmlNode(doc);
                dynamic bsObj;
                //if (aEPS.DeviceName.ToUpper() == "MORPHO")
                //  bsObj = JsonConvert.DeserializeObject<RooteeeMorpho>(jsonText);
                //else
                bsObj = JsonConvert.DeserializeObject<Mantra>(jsonText);


                if (doc.FirstChild is XmlDeclaration)
                {
                    // Remove the XmlDeclaration
                    doc.RemoveChild(doc.FirstChild);
                }
                string modifiedXml = doc.OuterXml;

                CWMSBECredopay authc = new CWMSBECredopay();
                authc.bank_name = "100032";
                authc.amount = "0.00";
                authc.CRN_U = RefID;
                authc.custom_field1 = RefID;
                authc.hashed_aadhar = Credopay.ComputeMD5(dtMember.Rows[0]["aadhar"].ToString());
                authc.aadhar = Credopay.EncryptJavaEqualent(Credopay.AepsKey.Replace("-", ""), dtMember.Rows[0]["aadhar"].ToString());
                authc.pid_data = modifiedXml;
                authc.masked_aadhar = dtMember.Rows[0]["aadhar"].ToString().Substring(0, 6) + "XXXX" + dtMember.Rows[0]["aadhar"].ToString().Substring(8, 4);
                authc.cpId = dthisb.Rows[0]["AEPSID"].ToString();
                authc.transaction_group = "aeps";

                authc.transaction_type = "Authentication";
                authc.payment_method = "biometric";

                authc.auth_reference_no = "";
                authc.biometric_srno = bsObj.PidData.DeviceInfo.RdsId;
                authc.biometric_rdsVer = bsObj.PidData.DeviceInfo.RdsVer;
                authc.biometric_rdsId = bsObj.PidData.DeviceInfo.RdsId;
                authc.biometric_dpId = bsObj.PidData.DeviceInfo.DpId;
                authc.biometric_mi = bsObj.PidData.DeviceInfo.Mi;
                authc.operating_system = "windows";
                authc.transaction_origin_ip = "103.76.250.2";
                authc.latitude = "19.2563";
                authc.longitude = "19.2563";
                authc.customer_mobile = dtMember.Rows[0]["mobile"].ToString();
                authc.virtual_ID = "";

                string Request = Newtonsoft.Json.JsonConvert.SerializeObject(authc);
                ObjConnection.update_data("insert into logdata(data)values('" + Request + "')");
                string ddddd = Credopay.EncryptJavaEqualent(Credopay.AepsSecretkey.Replace("-", ""), Request);
                string Result = Credopay.PostCallWithBody("aeps/authentication", ddddd);
                JObject ju = JObject.Parse(Result);
                ObjConnection.update_data("insert into logdata(data)values('" + Result + "')");
                if (ju["response_code"].ToString().ToUpper() == "00")
                {
                    ObjConnection.update_data("update AEPSOnBoardStatus set MerAuthTxnId='" + ju["auth_reference_no"].ToString() + "',IsUserMerAuthTxnId=0,DailyAuthentication=1,AuthDate=getdate() where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and Name='" + pipe + "'");

                    return ErrorShow.SuccessMessage(ju["response_description"].ToString());
                }
                else
                {
                    return ErrorShow.ErrorMessage(ju["response_description"].ToString());
                }
            }
            else
            {
                return ErrorShow.ErrorMessage("fsdfds");
            }
        }
        else
        {
            aEPS.accessmodetype = "SITE";
            aEPS.mobilenumber = dtMember.Rows[0]["Mobile"].ToString();
            aEPS.adhaarnumber = dtMember.Rows[0]["Aadhar"].ToString();
            aEPS.latitude = dtMember.Rows[0]["Latitude"].ToString();
            aEPS.longitude = dtMember.Rows[0]["Longitude"].ToString();
            aEPS.referenceno = RefID;

            if (pipe.ToUpper() == "BANK2")
                aEPS.MethodName = "authenticate2newauth";
            else
                aEPS.MethodName = "authenticate3newauth";

            aEPS.timestamp = DateTime.Now.ToString("yyyy-MM-dd H:mm:ss");
            aEPS.data = HttpUtility.HtmlDecode(aEPS.data);
            aEPS.ipaddress = ipAddress;
            aEPS.submerchantid = SONITechnoCredentrial.Prefix + dtMember.Rows[0]["LoginID"].ToString();

            string result = ApiPostCall.PostCall(aEPS.GetJson(), SONITechnoCredentrial.AEPSCore);
            JObject Data = JObject.Parse(result);
            if (Data["code"].ToString().ToUpper() == "TXN")
            {
                ObjConnection.update_data("update AEPSOnBoardStatus set MerAuthTxnId='" + Data["data"]["MerAuthTxnId"].ToString() + "',IsUserMerAuthTxnId=0,DailyAuthentication=1,AuthDate=getdate() where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and Name='" + pipe + "'");

                return ErrorShow.SuccessArray(Data["data"].ToString());
            }
            else
            {
                return ErrorShow.ErrorMessage(Data["mess"].ToString());
            }
        }
    }

    [WebMethod(EnableSession = true)]
    public static string GetMerchantDetails(string pipe)
    {
        cls_connection ObjConnection = new cls_connection();
        DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
        DataTable dt = ObjConnection.select_data_dt("select '" + dtMember.Rows[0]["loginid"].ToString() + "' as LoginID,'' as AEPSID,AEPSTwoWayRegistration,iif(DailyAuthentication=1 and convert(nvarchar(30),authdate,112)=convert(nvarchar(30),getdate(),112),cast(1 as bit),cast(0 as bit)) as DailyAuthentication from AEPSOnBoardStatus where Name='" + pipe + "' and msrno = '" + dtMember.Rows[0]["msrno"].ToString() + "'");
        return ConstantsData.SerializeToJSon(dt);
    }

    [WebMethod(EnableSession = true)]
    public static string GetBankAEPSStatus()
    {
        cls_connection ObjConnection = new cls_connection();
        DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
        DataTable dt = ObjConnection.select_data_dt("select Msrno,Name,Status,AEPSTwoWayRegistration,iif(DailyAuthentication=1 and convert(nvarchar(30),authdate,112)=convert(nvarchar(30),getdate(),112),cast(1 as bit),cast(0 as bit)) as DailyAuthentication from AEPSOnBoardStatus where Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");
        return ConstantsData.SerializeToJSon(dt);
    }


    [WebMethod(EnableSession = true)]
    public static string CheckStatusPipe(string pipe)
    {
        try
        {
            cls_connection ObjConnection = new cls_connection();
            DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
            if (dtMember.Rows.Count > 0)
            {
                if (pipe.ToUpper() == "BANK6")
                {
                    return ErrorShow.SuccessMessage("");
                }
                else
                {


                    string Request = "{\"APIID\":\"" + SONITechnoCredentrial.APIID + "\",\"Token\":\"" + SONITechnoCredentrial.Token + "\",\"MethodName\":\"checkstatus\",\"merchantcode\":\"" + SONITechnoCredentrial.Prefix + dtMember.Rows[0]["LoginID"].ToString() + "\",\"mobile\":\"" + dtMember.Rows[0]["Mobile"].ToString() + "\",\"is_new\":null,\"email\":null,\"firm\":\"" + pipe + "\",\"callback\":null}";
                    string result = ApiPostCall.PostCall(Request, SONITechnoCredentrial.AEPSCore);


                    JObject Data = JObject.Parse(result);
                    if (Data["code"].ToString().ToUpper() == "TXN")
                    {
                        ObjConnection.update_data("update AEPSOnBoardStatus set Status='" + Data["data"]["is_approved"].ToString() + "' where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "' and Name='" + pipe + "'");
                        return ErrorShow.SuccessMessage(Data["data"]["message"].ToString());
                    }
                    else
                    {
                        return ErrorShow.ErrorMessage(Data["mess"].ToString());
                    }
                }
            }
            else
            {
                return ErrorShow.ErrorMessage("Member Not Login");
            }
        }
        catch (Exception ex)
        {
            return ErrorShow.ErrorMessage(ex.Message);
        }

    }
}

public class AEPS
{
    public string Mobile { get; set; }
    public Int32 Amount { get; set; }
    public BankID BankID { get; set; }
    public string AadharNo { get; set; }
    public string Capture { get; set; }
    public string DeviceName { get; set; }
    public string Type { get; set; }
}

public class BankID
{
    public int id { get; set; }
    public string text { get; set; }
}
public class AEPSResponse
{
    public string Img { get; set; }
    public string Message { get; set; }
    public string Amount { get; set; }
    public string AccountBalance { get; set; }
    public string BankName { get; set; }
    public string MobileNo { get; set; }
    public string BCCode { get; set; }
    public string BCName { get; set; }
    public string TransID { get; set; }
    public string AadharNo { get; set; }
    public string Reason { get; set; }
    public string BankNumber { get; set; }
    public string Date { get; set; }
    public IList<Ministatement> ministatement { get; set; }
}

public class Ministatement
{
    public string date { get; set; }
    public string amount { get; set; }
    public string txnType { get; set; }
    public string narration { get; set; }
}

public class AEPSTWORegistration
{
    public string APIID { get; set; }
    public string Token { get; set; }
    public string MethodName { get; set; }
    public string accessmodetype { get; set; }
    public string adhaarnumber { get; set; }
    public string mobilenumber { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string referenceno { get; set; }
    public string submerchantid { get; set; }
    public string timestamp { get; set; }
    public string data { get; set; }
    public string ipaddress { get; set; }

    public string GetJson()
    {
        AEPSTWORegistration ObjData = new AEPSTWORegistration();
        ObjData.APIID = SONITechnoCredentrial.APIID;
        ObjData.Token = SONITechnoCredentrial.Token;
        ObjData.MethodName = this.MethodName;
        ObjData.accessmodetype = this.accessmodetype;
        ObjData.adhaarnumber = this.adhaarnumber;
        ObjData.mobilenumber = this.mobilenumber;
        ObjData.latitude = this.latitude;
        ObjData.longitude = this.longitude;
        ObjData.referenceno = this.referenceno;
        ObjData.submerchantid = this.submerchantid;
        ObjData.timestamp = this.timestamp;
        ObjData.data = this.data;
        ObjData.ipaddress = this.ipaddress;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }

}