using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Member_BBPS : System.Web.UI.Page
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
                DataTable dtOpHis = ObjData.select_data_dt("select * from tblRecharge_OperatorCode where APIID=1 and OperatorCode='" + OperatorID + "'");
                if (dtOpHis.Rows.Count > 0)
                {
                    Int32 Msrno = Convert.ToInt32(dtMember.Rows[0]["msrno"].ToString());
                    double Balance = ObjData.select_data_scalar_double("select balance from TBL_EWALLETBALANCE where msrno='" + Msrno + "'");
                    if (Balance > Convert.ToDouble(Amount))
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
                        DataTable DtUrl = ObjData.select_data_dt("BBpsRecharge '" + billFetchRequest.Number + "','" + Msrno + "','" + Amount + "','" + dtOpHis.Rows[0]["OperatorID"].ToString() + "','" + billFetchRequest.GetJson() + "'," + ConstantsData.OfflineAPIID + ",'" + ConstantsData.getipAddress() + "','Web','" + jsonData + "'");
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
                                JObject Data = JObject.Parse(Result);
                                if (Data["code"].ToString().ToUpper() == "TXN")
                                {
                                    if (Data["data"]["status"].ToString().ToUpper() == "SUCCESS")
                                    {
                                        DataTable i = ObjData.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + "," + Msrno + ", 0, 0, 0, 0, '', '', 'Success','" + Data["data"]["TransactionID"].ToString() + "', '','" + Data["data"]["OperatorTransactionId"].ToString() + "'");
                                        ErrorShow.AlertMessageWithRedirect(page, "Recharge Successful !!", "BillPayReceipt.aspx?TransID=" + DtUrl.Rows[0]["TransID"].ToString(), ConstantsData.CompanyName);
                                    }
                                    else if (Data["data"]["status"].ToString().ToUpper() == "PENDING")
                                    {
                                        DataTable i = ObjData.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + ", 0, 0, 0, 0, 0, '', '','Pending','" + Data["data"]["TransactionID"].ToString() + "','',''");
                                        ErrorShow.AlertMessageWithRedirect(page, "Recharge Successful !!", "BillPayReceipt.aspx?TransID=" + DtUrl.Rows[0]["TransID"].ToString(), ConstantsData.CompanyName);
                                    }
                                    else
                                    {
                                        try
                                        {
                                            DataTable i = ObjData.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + ", 0, 0, 0, 0, 0, '','', 'Failed', '','" + Data["mess"].ToString() + "',''");
                                        }
                                        catch (Exception ex) { }
                                        DataTable dtNarration = ObjData.select_data_dt("select * from TBL_EWALLETTRANSACTION where TransactionID='" + DtUrl.Rows[0]["TransID"].ToString() + "' and Factor='DR'");
                                        if (dtNarration.Rows.Count > 0)
                                        {
                                            ObjData.update_data("PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["LoginID"].ToString() + "'," + Amount + ",'CR','" + ("Refund " + dtNarration.Rows[0]["Narration"].ToString()) + "','Refund-" + dtNarration.Rows[0]["Description"].ToString() + "'," + dtNarration.Rows[0]["ServiceID"].ToString() + ",'" + DtUrl.Rows[0]["TransID"].ToString() + "'");
                                        }
                                        ErrorShow.AlertMessage(page, Data["data"]["mess"].ToString(), ConstantsData.CompanyName);
                                    }
                                }
                                else
                                {
                                    try
                                    {
                                        DataTable i = ObjData.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + ", 0, 0, 0, 0, 0, '','', 'Failed', '','" + Data["mess"].ToString() + "',''");
                                    }
                                    catch (Exception ex) { }
                                    
                                    DataTable dtNarration = ObjData.select_data_dt("select * from TBL_EWALLETTRANSACTION where TransactionID='" + DtUrl.Rows[0]["TransID"].ToString() + "' and Factor='DR'");

                                    if (dtNarration.Rows.Count > 0)
                                    {
                                        ObjData.update_data("PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["LoginID"].ToString() + "'," + Amount + ",'CR','" + ("Refund " + dtNarration.Rows[0]["Narration"].ToString()) + "','Refund-" + dtNarration.Rows[0]["Description"].ToString() + "'," + dtNarration.Rows[0]["ServiceID"].ToString() + ",'" + DtUrl.Rows[0]["TransID"].ToString() + "'");
                                    }
                                    ErrorShow.AlertMessage(page, Data["mess"].ToString(), ConstantsData.CompanyName);
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
    public void BillFetch(string Op, Repeater rptData, TextBox txtAMount)
    {
        PostGetCallMSWIFT postGetCallMSWIFT = new PostGetCallMSWIFT();
        DataTable dtBBPS = ObjData.select_data_dt("select distinct spkey,IsBillFetch from BBPSData where SpKey='" + Op + "'");
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

                    string Result = postGetCallMSWIFT.PostCall(billFetchRequest.GetJson());
                    if (!string.IsNullOrEmpty(Result))
                    {
                        JObject Data = JObject.Parse(Result);

                        //{"code":"ERR","status":"failed","mess":"Invalid IP and Token"}
                        if (Data["code"].ToString().ToUpper() == "TXN")
                        {
                            BillFetchResult bsBenBindData = JsonConvert.DeserializeObject<BillFetchResult>(Data["data"].ToString());
                            lblcustomername.Text = bsBenBindData.customername;
                            Billamount.Text = bsBenBindData.dueamount.ToString();
                            billdate.Text = bsBenBindData.billdate;
                            duedate.Text = bsBenBindData.duedate;
                            txtAMount.Text = bsBenBindData.dueamount.ToString();
                            txtAMount.Enabled = false;

                        }
                        else
                        {
                            ErrorShow.AlertMessage(page, Data["mess"].ToString(), ConstantsData.CompanyName);

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
}