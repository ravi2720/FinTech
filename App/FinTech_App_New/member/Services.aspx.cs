using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ab_part : System.Web.UI.Page
{

    cls_connection ObjData = new cls_connection();
    Page page;
    DataTable dtMember = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        dtMember = (DataTable)Session["dtMember"];
        if (!IsPostBack)
        {
            if (Request.QueryString["SType"] != null)
            {
                BindDropDown.FillDropDown(ddlOperatorELECTRICITY, "NewBBPS", "Name", "ID", "Select Operator", Request.QueryString["SType"].ToLower());

            }
        }
    }

    protected void ddlOperatorELECTRICITY_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataFiled(ddlOperatorELECTRICITY.SelectedValue, rptData);
    }

    protected void btnBillFetch_Click(object sender, EventArgs e)
    {
        BillFetch(ddlOperatorELECTRICITY.SelectedValue, rptData, txtAmountELECTRICITY);
    }
    protected void btnElePay_Click(object sender, EventArgs e)
    {
        PayBill(ddlOperatorELECTRICITY, rptData, txtAmountELECTRICITY);
    }

    public void PayBill(DropDownList dllOperator, Repeater rptData, TextBox txtAmount)
    {
        if (dllOperator.SelectedIndex > 0)
        {
            var date = DateTime.Now;
            string OperatorID = Convert.ToString(dllOperator.SelectedValue).Trim();
            DataTable dtOp = ObjData.select_data_dt("ManageOperator 'GetDetaisID',0,'" + OperatorID + "'");
            if (date.Hour >= Convert.ToInt32(dtOp.Rows[0]["OnTime"].ToString()) && date.Hour <= Convert.ToInt32(dtOp.Rows[0]["OffTime"].ToString()))
            {
                string Amount = txtAmount.Text;
                DataTable dtOpHis = ObjData.select_data_dt("select * from tblRecharge_OperatorCode where APIID=4 and OperatorCode='" + OperatorID + "'");
                if (dtOpHis.Rows.Count > 0)
                {
                    Int32 Msrno = Convert.ToInt32(dtMember.Rows[0]["msrno"].ToString());
                    double Balance = ObjData.select_data_scalar_double("select balance from TBL_EWALLETBALANCE where msrno='" + Msrno + "'");
                    if (Balance > (Convert.ToDouble(Amount) + Convert.ToDouble(dtMember.Rows[0]["HoldAmt"].ToString())))
                    {
                        PostGetCallMSWIFT postGetCallMSWIFT = new PostGetCallMSWIFT();
                        BillPayRequest billFetchRequest = new BillPayRequest();
                        billFetchRequest.TelecomCircleID = "0";
                        billFetchRequest.APIID = SONITechnoCredentrial.APIID;
                        billFetchRequest.Token = SONITechnoCredentrial.Token;
                        billFetchRequest.MethodName = "billpay";

                        billFetchRequest.SPKey = OperatorID;
                        for (Int32 i = 0; i < rptData.Items.Count; i++)
                        {
                            TextBox txtVal = (TextBox)rptData.Items[i].FindControl("txtVal");
                            if (i == 0)
                            {
                                billFetchRequest.Number = txtVal.Text;
                            }

                            if (i == 1)
                            {
                                billFetchRequest.Optional1 = txtVal.Text;
                            }
                            if (i == 2)
                            {
                                billFetchRequest.Optional2 = txtVal.Text;
                            }
                            if (i == 3)
                            {
                                billFetchRequest.Optional3 = txtVal.Text;
                            }
                            if (i == 4)
                            {
                                billFetchRequest.Optional4 = txtVal.Text;
                            }
                            if (i == 5)
                            {
                                billFetchRequest.Optional5 = txtVal.Text;
                            }
                            if (i == 6)
                            {
                                billFetchRequest.Optional6 = txtVal.Text;
                            }
                            if (i == 7)
                            {
                                billFetchRequest.Optional7 = txtVal.Text;
                            }
                            if (i == 9)
                            {
                                billFetchRequest.Optional8 = txtVal.Text;
                            }

                        }
                        var myData = new
                        {
                            Name = lblcustomername.Text,
                            Amount = Billamount.Text,
                            DueDate = duedate.Text,
                            BillDate = billdate.Text,
                        };
                        string jsonData = JsonConvert.SerializeObject(myData);

                        billFetchRequest.Amount = txtAmount.Text;
                        DataTable DtUrl;
                        if (dtOp.Rows[0]["IsPending"].ToString().ToBoolen())
                        {
                            DtUrl = ObjData.select_data_dt("BBpsRecharge '" + billFetchRequest.Number + "','" + Msrno + "','" + Amount + "','" + dtOpHis.Rows[0]["OperatorID"].ToString() + "','" + billFetchRequest.GetJson() + "'," + ConstantsData.OfflineAPIID + ",'" + ConstantsData.getipAddress() + "','Web','" + jsonData + "',1");
                        }
                        else
                        {
                            DtUrl = ObjData.select_data_dt("BBpsRecharge '" + billFetchRequest.Number + "','" + Msrno + "','" + Amount + "','" + dtOpHis.Rows[0]["OperatorID"].ToString() + "','" + billFetchRequest.GetJson() + "'," + ConstantsData.OfflineAPIID + ",'" + ConstantsData.getipAddress() + "','Web','" + jsonData + "',0");
                        }
                        if (DtUrl.Rows[0]["Yes"].ToString() == "Yes")
                        {
                            billFetchRequest.ClientRefId = DtUrl.Rows[0]["TransID"].ToString();// "TransID";
                            if (dtOp.Rows[0]["IsPending"].ToString().ToBoolen())
                            {
                                DataTable i = ObjData.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + ", " + Msrno + ", 0, 0, 0, 0, '', '','Success','" + DtUrl.Rows[0]["TransID"].ToString() + "','','" + DtUrl.Rows[0]["TransID"].ToString() + "'");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Recharge Successful !!');", true);
                            }
                            else
                            {
                                string Result = postGetCallMSWIFT.PostCallPay(billFetchRequest.GetJson());
                                BillPayResult bsBenBindData = JsonConvert.DeserializeObject<BillPayResult>(Result);

                                if (bsBenBindData.ResponseCode.ToString() == "000")
                                {
                                    DataTable i = ObjData.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + "," + Msrno + ", 0, 0, 0, 0, '', '', 'Success','" + bsBenBindData.TransactionId + "', '','" + bsBenBindData.OperatorTransactionId + "'");
                                    ErrorShow.AlertMessageWithRedirect(page, "Recharge Successful !!", "BillPayReceipt.aspx?TransID=" + DtUrl.Rows[0]["TransID"].ToString(), ConstantsData.CompanyName);
                                }
                                else if (bsBenBindData.ResponseCode.ToString() == "999")
                                {
                                    DataTable i = ObjData.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + ", 0, 0, 0, 0, 0, '', '','Pending','" + bsBenBindData.TransactionId + "','',''");
                                    ErrorShow.AlertMessageWithRedirect(page, "Recharge Successful !!", "BillPayReceipt.aspx?TransID=" + DtUrl.Rows[0]["TransID"].ToString(), ConstantsData.CompanyName);
                                }
                                else
                                {
                                    try
                                    {
                                        DataTable i = ObjData.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + ", 0, 0, 0, 0, 0, '','', 'Failed', '','" + bsBenBindData.ResponseMessage + "',''");
                                    }
                                    catch (Exception ex) { }
                                    DataTable dtNarration = ObjData.select_data_dt("select * from TBL_EWALLETTRANSACTION where TransactionID='" + DtUrl.Rows[0]["TransID"].ToString() + "' and Factor='DR'");
                                    if (dtNarration.Rows.Count > 0)
                                    {
                                        ObjData.update_data("PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["LoginID"].ToString() + "'," + Amount + ",'CR','" + ("Refund " + dtNarration.Rows[0]["Narration"].ToString()) + "','Refund-" + dtNarration.Rows[0]["Description"].ToString() + "'," + dtNarration.Rows[0]["ServiceID"].ToString() + ",'" + DtUrl.Rows[0]["TransID"].ToString() + "'");
                                    }
                                    ErrorShow.AlertMessage(page, bsBenBindData.ResponseMessage, ConstantsData.CompanyName);
                                }

                            }
                        }
                    }
                    else
                    {
                        ErrorShow.AlertMessage(page, "Low balance", ConstantsData.CompanyName);
                    }
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Opeartor not support", ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Service Working Time " + dtOp.Rows[0]["OnTime"].ToString() + " to " + dtOp.Rows[0]["OffTime"].ToString() + "", ConstantsData.CompanyName);
            }
        }

    }

    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        try
        {
            httpreq.Timeout = System.Threading.Timeout.Infinite;
            httpreq.KeepAlive = true;
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }
        catch (Exception ex)
        {
            return "0";
        }
    }

    public void BillFetch(string Op, Repeater rptData, TextBox txtAMount)
    {
        PostGetCallMSWIFT postGetCallMSWIFT = new PostGetCallMSWIFT();
        DataTable dtBBPS = ObjData.select_data_dt("select distinct ServiceType,spkey,IsBillFetch from BBPSData where SpKey='" + Op + "'");
        if (dtBBPS.Rows.Count > 0)
        {

            if (Convert.ToBoolean(dtBBPS.Rows[0]["IsBillFetch"].ToString()))
            {
                DataTable dtDownHistory = ObjData.select_data_dt("select distinct SpKey,[Index],Labels,FieldMinLen,FieldMaxLen from BBPSDataDown where SpKey='" + Op + "'");
                if (dtDownHistory.Rows.Count > 0)
                {
                    BillFetchRequest billFetchRequest = new BillFetchRequest();
                    billFetchRequest.TelecomCircleID = "0";
                    billFetchRequest.ClientRefId = "0";
                    billFetchRequest.APIID = SONITechnoCredentrial.APIID;
                    billFetchRequest.Token = SONITechnoCredentrial.Token;
                    billFetchRequest.MethodName = "billfetch";
                    billFetchRequest.SPKey = ddlOperatorELECTRICITY.SelectedValue;
                    for (Int32 i = 0; i < rptData.Items.Count; i++)
                    {
                        TextBox txtVal = (TextBox)rptData.Items[i].FindControl("txtVal");
                        if (i == 0)
                        {
                            billFetchRequest.Number = txtVal.Text;
                        }

                        if (i == 1)
                        {
                            billFetchRequest.Optional1 = txtVal.Text;
                        }
                        if (i == 2)
                        {
                            billFetchRequest.Optional2 = txtVal.Text;
                        }
                        if (i == 3)
                        {
                            billFetchRequest.Optional3 = txtVal.Text;
                        }
                        if (i == 4)
                        {
                            billFetchRequest.Optional4 = txtVal.Text;
                        }
                        if (i == 5)
                        {
                            billFetchRequest.Optional5 = txtVal.Text;
                        }
                        if (i == 6)
                        {
                            billFetchRequest.Optional6 = txtVal.Text;
                        }
                        if (i == 7)
                        {
                            billFetchRequest.Optional7 = txtVal.Text;
                        }
                        if (i == 9)
                        {
                            billFetchRequest.Optional8 = txtVal.Text;
                        }

                    }
                    string Result = "";
                    if (dtBBPS.Rows[0]["ServiceType"].ToString().ToUpper() == "FASTAG")
                    {
                        Result = apicall("https://unikpe.com/api/Ambika.aspx?uid=354&pin=0dd54dabb110a7814a746e4d2ae4e618&reqType=billfetch&operator=" + billFetchRequest.SPKey + "&number=" + billFetchRequest.Number + "");
                    }
                    else if (Op == "PHED")
                    {
                        var client = new RestClient("https://www.mplan.in/api/Water.php?apikey=f3f4e67ca66eb3c7e6044cabc834b052&offer=roffer&tel=" + billFetchRequest.Number + "&operator=PHED");
                        client.Timeout = -1;
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/json", "", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        JObject dd = JObject.Parse(response.Content);
                        if (dd["status"].ToString() == "1")
                        {
                            Result = "{\"ResponseCode\":\"000\",\"ResponseMessage\":\"Transaction Successful\",\"dueamount\": " + dd["records"][0]["Billamount"].ToString() + ",\"duedate\":\"\",\"customername\": \"" + dd["records"][0]["CustomerName"].ToString() + "\",\"billnumber\": \"" + dd["tel"].ToString() + "\",\"billdate\": \"\",\"acceptPartPay\": \"N\",\"BBPSCharges\": \"0.00\",\"BillUpdate\": \"T + 1\",\"RequestID\":\"4000000508814032\",\"ClientRefId\": \"555558565688\",\"BBPSComm\": \"0.00\"}";
                        }
                    }
                    else
                    {
                        Result = postGetCallMSWIFT.PostCall(billFetchRequest.GetJson());
                    }

                    BillFetchResult bsBenBindData = JsonConvert.DeserializeObject<BillFetchResult>(Result);
                    if (!string.IsNullOrEmpty(Result))
                    {
                        if (bsBenBindData.ResponseCode.ToString() == "000")
                        {
                            lblcustomername.Text = bsBenBindData.customername;
                            Billamount.Text = bsBenBindData.dueamount.ToString();
                            billdate.Text = bsBenBindData.billdate;
                            duedate.Text = bsBenBindData.duedate;
                            txtAMount.Text = bsBenBindData.dueamount.ToString();
                            txtAMount.Enabled = false;

                        }
                        else
                        {
                            ErrorShow.AlertMessage(page, bsBenBindData.ResponseMessage, ConstantsData.CompanyName);

                        }
                    }

                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Bill fetch not allow", ConstantsData.CompanyName);

            }
        }
    }

    public void BindDataFiled(string OP, Repeater rptDataEMI)
    {
        imgop.ImageUrl = "./images/icon/" + ObjData.select_data_scalar_string("select class from BBPSData where spkey='" + OP + "'");
        DataTable dtH = ObjData.select_data_dt("select distinct spkey,[index],Labels,FieldMinLen,FieldMaxLen from bbpsdatadown where spkey='" + OP + "'");
        if (dtH.Rows.Count > 0)
        {
            rptDataEMI.DataSource = dtH;
            rptDataEMI.DataBind();
        }
        else
        {
            rptDataEMI.DataSource = null;
            rptDataEMI.DataBind();
        }
    }

    [WebMethod]
    public static string GetData(string MethodName)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("FillDropDown 'NewBBPS','" + MethodName.Replace("%20", " ") + "'");
        return ConstantsData.SerializeToJSon(dt);
    }


    [WebMethod]
    public static string BBPSF(string MethodName)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("select distinct spkey,[index],Labels,FieldMinLen,FieldMaxLen from bbpsdatadown where spkey='" + MethodName + "'");
        return ConstantsData.SerializeToJSon(dt);
    }


    [WebMethod]
    public static string BiFetch(string OTID, string Json)
    {
        PostGetCallMSWIFT postGetCallMSWIFT = new PostGetCallMSWIFT();
        BillFetchRequest billFetchRequest = new BillFetchRequest();
        billFetchRequest.TelecomCircleID = "0";
        billFetchRequest.ClientRefId = "0";
        billFetchRequest.APIID = SONITechnoCredentrial.APIID;
        billFetchRequest.Token = SONITechnoCredentrial.Token;
        billFetchRequest.MethodName = "billfetch";
        billFetchRequest.SPKey = OTID;
        JObject objdata = JObject.Parse(Json);

        for (Int32 i = 0; i < objdata.Count; i++)
        {
            if (i == 0)
            {
                billFetchRequest.Number = objdata["optional0"].ToString();
            }

            if (i == 1)
            {
                billFetchRequest.Optional1 = objdata["optional1"].ToString();
            }
            if (i == 2)
            {
                billFetchRequest.Optional2 = objdata["optional2"].ToString();
            }
            if (i == 3)
            {
                billFetchRequest.Optional3 = objdata["optional3"].ToString();
            }
            if (i == 4)
            {
                billFetchRequest.Optional4 = objdata["optional4"].ToString();
            }
            if (i == 5)
            {
                billFetchRequest.Optional5 = objdata["optional5"].ToString();
            }
            if (i == 6)
            {
                billFetchRequest.Optional6 = objdata["optional6"].ToString();
            }
            if (i == 7)
            {
                billFetchRequest.Optional7 = objdata["optional7"].ToString();
            }
            if (i == 9)
            {
                billFetchRequest.Optional8 = objdata["optional8"].ToString();
            }
        }
        string Result = postGetCallMSWIFT.PostCall(billFetchRequest.GetJson());
        return Result;
    }


    [WebMethod]
    public static void biPay(string OTID, string Json, string BillFetch)
    {
        cls_connection cls = new cls_connection();

        PostGetCallMSWIFT postGetCallMSWIFT = new PostGetCallMSWIFT();
        BillPayRequest billFetchRequest = new BillPayRequest();
        billFetchRequest.TelecomCircleID = "0";
        billFetchRequest.ClientRefId = "0";
        billFetchRequest.APIID = SONITechnoCredentrial.APIID;
        billFetchRequest.Token = SONITechnoCredentrial.Token;
        billFetchRequest.MethodName = "billpay";
        billFetchRequest.SPKey = OTID;
        JObject objdata = JObject.Parse(Json);
        DataTable dtcus = (DataTable)HttpContext.Current.Session["dtMember"];
        billFetchRequest.Amount = objdata["Amount"].ToString();
        double Balance = cls.select_data_scalar_double("select balance from TBL_EWALLETBALANCE where msrno='" + dtcus.Rows[0]["Msrno"].ToString() + "'");
        if (Balance > (Convert.ToDouble(billFetchRequest.Amount) + Convert.ToDouble(dtcus.Rows[0]["HoldAmt"].ToString())))
        {
            for (Int32 i = 0; i < objdata.Count; i++)
            {
                if (i == 0)
                {
                    billFetchRequest.Number = objdata["optional0"].ToString();
                }

                if (i == 1)
                {
                    billFetchRequest.Optional1 = objdata["optional1"].ToString();
                }
                if (i == 2)
                {
                    billFetchRequest.Optional2 = objdata["optional2"].ToString();
                }
                if (i == 3)
                {
                    billFetchRequest.Optional3 = objdata["optional3"].ToString();
                }
                if (i == 4)
                {
                    billFetchRequest.Optional4 = objdata["optional4"].ToString();
                }
                if (i == 5)
                {
                    billFetchRequest.Optional5 = objdata["optional5"].ToString();
                }
                if (i == 6)
                {
                    billFetchRequest.Optional6 = objdata["optional6"].ToString();
                }
                if (i == 7)
                {
                    billFetchRequest.Optional7 = objdata["optional7"].ToString();
                }
                if (i == 9)
                {
                    billFetchRequest.Optional8 = objdata["optional8"].ToString();
                }
            }


            Int32 oid = cls.select_data_scalar_int("select id from Operator where OperatorCode='" + OTID + "'");
            DataTable DtUrl;
            var date = DateTime.Now;
            DataTable dtOp = cls.select_data_dt("ManageOperator 'GetDetaisID',0,'" + OTID + "'");
            if (date.Hour >= Convert.ToInt32(dtOp.Rows[0]["OnTime"].ToString()) && date.Hour <= Convert.ToInt32(dtOp.Rows[0]["OffTime"].ToString()))
            {
                if (dtOp.Rows[0]["IsPending"].ToString().ToBoolen())
                {
                    DtUrl = cls.select_data_dt("BBpsRecharge '" + billFetchRequest.Number + "','" + dtcus.Rows[0]["Msrno"].ToString() + "','" + billFetchRequest.Amount + "','" + oid + "','" + billFetchRequest.GetJson() + "'," + ConstantsData.OfflineAPIID + ",'" + ConstantsData.getipAddress() + "','Web','" + BillFetch + "',1");
                }
                else
                {
                    DtUrl = cls.select_data_dt("BBpsRecharge '" + billFetchRequest.Number + "','" + dtcus.Rows[0]["Msrno"].ToString() + "','" + billFetchRequest.Amount + "','" + oid + "','" + billFetchRequest.GetJson() + "'," + ConstantsData.OfflineAPIID + ",'" + ConstantsData.getipAddress() + "','Web','" + BillFetch + "',0");
                }


                if (DtUrl.Rows[0]["Yes"].ToString() == "Yes")
                {
                    billFetchRequest.ClientRefId = DtUrl.Rows[0]["TransID"].ToString();// "TransID";
                    if (dtOp.Rows[0]["IsPending"].ToString().ToBoolen())
                    {
                        DataTable i = cls.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + ", " + dtcus.Rows[0]["Msrno"].ToString() + ", 0, 0, 0, 0, '', '','Success','" + DtUrl.Rows[0]["TransID"].ToString() + "','','" + DtUrl.Rows[0]["TransID"].ToString() + "'");
                       HttpContext.Current.Response.Write(ErrorShow.SuccessMessage("Recharge Successful !!. 2-4 Hour Bill Will Auto Update On Biller End"));
                    }
                    else
                    {
                        string Result = postGetCallMSWIFT.PostCallPay(billFetchRequest.GetJson());
                        BillPayResult bsBenBindData = JsonConvert.DeserializeObject<BillPayResult>(Result);

                        if (bsBenBindData.ResponseCode.ToString() == "000")
                        {
                            DataTable i = cls.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + "," + dtcus.Rows[0]["Msrno"].ToString() + ", 0, 0, 0, 0, '', '', 'Success','" + bsBenBindData.TransactionId + "', '','" + bsBenBindData.OperatorTransactionId + "'");
                           HttpContext.Current.Response.Write(ErrorShow.SuccessMessage("Recharge Successful !!. 2-4 Hour Bill Will Auto Update On Biller End"));
                        }
                        else if (bsBenBindData.ResponseCode.ToString() == "999")
                        {
                            DataTable i = cls.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + ", 0, 0, 0, 0, 0, '', '','Pending','" + bsBenBindData.TransactionId + "','',''");
                           HttpContext.Current.Response.Write(ErrorShow.SuccessMessage("Recharge Pending !!. 2-4 Hour Bill Will Auto Update On Biller End"));
                        }
                        else
                        {
                            try
                            {
                                DataTable i = cls.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + ", 0, 0, 0, 0, 0, '','', 'Failed', '','" + bsBenBindData.ResponseMessage + "',''");
                            }
                            catch (Exception ex) { }
                            DataTable dtNarration = cls.select_data_dt("select * from TBL_EWALLETTRANSACTION where TransactionID='" + DtUrl.Rows[0]["TransID"].ToString() + "' and Factor='DR'");
                            if (dtNarration.Rows.Count > 0)
                            {
                                cls.update_data("PROC_EWALLETTRANSACTION '" + dtcus.Rows[0]["LoginID"].ToString() + "'," + billFetchRequest.Amount + ",'CR','" + ("Refund " + dtNarration.Rows[0]["Narration"].ToString()) + "','Refund-" + dtNarration.Rows[0]["Description"].ToString() + "'," + dtNarration.Rows[0]["ServiceID"].ToString() + ",'" + DtUrl.Rows[0]["TransID"].ToString() + "'");
                            }
                            HttpContext.Current.Response.Write(ErrorShow.ErrorMessage("Failed"));
                        }

                    }
                }
            }
            else
            {
               HttpContext.Current.Response.Write(ErrorShow.ErrorMessage("Service Working Time " + dtOp.Rows[0]["OnTime"].ToString() + " to " + dtOp.Rows[0]["OffTime"].ToString() + ""));
            }
        }
        else
        {
            HttpContext.Current.Response.Write(ErrorShow.ErrorMessage("Low Balance"));
        }
    }
}