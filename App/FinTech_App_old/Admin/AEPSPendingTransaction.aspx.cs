using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Net;
using System.Text;

public partial class Admin_AEPSPendingTransaction : System.Web.UI.Page
{
    DataTable dtEWalletTransaction = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    clsMahagram ObjData = new clsMahagram();
    DataTable dtAPESMahaAPIDetail = new DataTable();
    public static DataTable dtMemberMaster = new DataTable();
    public static string AEPS_URL = "https://cyrusrecharge.in/api/Api_AEPS2.aspx";
    public static string MerchantID = "";
    public static string MerchantKey = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            fillEWalletTransaction();
        }
    }
    public string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(6, 4) + ddmmyy.Substring(3, 2) + ddmmyy.Substring(0, 2);
        return mmddyy;
    }

    private void fillEWalletTransaction()
    {
        string strFilter = "MsrNo<>0";
        string fromDate = "";
        string toDate = "";
        if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            fromDate = changedatetoYYYYMMDD(txtfromdate.Text);
            toDate = changedatetoYYYYMMDD(txttodate.Text);
        }

        dtEWalletTransaction = cls.select_data_dt("ProcGetPending_mahaAEPS");
        //if (txtSearch.Text != "")
        //{
        //    strFilter = strFilter + "and TransID='" + txtSearch.Text.Trim() + "'";
        //}

        //dtEWalletTransaction.DefaultView.RowFilter = strFilter;
        //DataTable dtNew = new DataTable();
        //dtNew = dtEWalletTransaction.DefaultView.ToTable();
        if (dtEWalletTransaction.Rows.Count > 0)
        {
            gvEWalletTransaction.DataSource = dtEWalletTransaction;
            gvEWalletTransaction.DataBind();
            //litrecordcount.Text = dtNew.Rows.Count.ToString();
        }
        else
        {
            gvEWalletTransaction.DataSource = null;
            gvEWalletTransaction.DataBind();
            ViewState["dtExport"] = null;
            //litrecordcount.Text = "0";
        }
    }


    public string changedatetoYYYYMMDD(string ddmmyyyy)
    {
        string yyyymmdd = "";
        yyyymmdd = ddmmyyyy.Substring(6, 4) + ddmmyyyy.Substring(3, 2) + ddmmyyyy.Substring(0, 2);
        return yyyymmdd;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillEWalletTransaction();
    }


    

    protected void btnForceSuccess_Click(object sender, EventArgs e)
    {
        cls_connection cls = new cls_connection();
        foreach (RepeaterItem grow in gvEWalletTransaction.Items)
        {
            CheckBox chkRow = (CheckBox)grow.FindControl("chkRow");
            HiddenField hddCyrusTranID = (HiddenField)grow.FindControl("hddCyrusTranID");
            HiddenField hdnApiTransID = (HiddenField)grow.FindControl("hdnApiTransID");
            //HiddenField hddTransID = (HiddenField)grow.FindControl("hddTransID");
            if (chkRow.Checked)
            {
                cls.update_data("Update tblAEPS_Mahagram_History set Status='Success', ErrorMessage='Force Success' where ApiTransID='" + hdnApiTransID.Value + "' AND Status='Pending'");
            }
        }
        fillEWalletTransaction();
    }
    protected void btnForceFail_Click(object sender, EventArgs e)
    {
        cls_connection cls = new cls_connection();
        foreach (RepeaterItem grow in gvEWalletTransaction.Items)
        {
            CheckBox chkRow = (CheckBox)grow.FindControl("chkRow");
            HiddenField hddCyrusTranID = (HiddenField)grow.FindControl("hddCyrusTranID");
            HiddenField hdnApiTransID = (HiddenField)grow.FindControl("hdnApiTransID");

            //  HiddenField hddTransID = (HiddenField)grow.FindControl("hddTransID");
            if (chkRow.Checked)
            {
                cls.update_data("update tblAEPS_Mahagram_History set Status='FAILED', ErrorMessage='Force Failed' ApiTransID='" + hdnApiTransID.Value + "' AND Status='Pending'");
            }
        }
        fillEWalletTransaction();
    }

    protected void btnCheckStatus_Click(object sender, EventArgs e)
    {
        try
        {
            cls_connection cls = new cls_connection();
            foreach (RepeaterItem grow in gvEWalletTransaction.Items)
            {
                CheckBox chkRow = (CheckBox)grow.FindControl("chkRow");
                HiddenField hddMsrNo = (HiddenField)grow.FindControl("hddMsrNo");
                HiddenField hdnApiTransID = (HiddenField)grow.FindControl("hdnApiTransID");
                if (chkRow.Checked)
                {
                    int chk = cls.select_data_scalar_int("select count(*) from AEPS_History where status='Pending' and TransactionId='" + hdnApiTransID.Value + "'");
                    if (chk > 0)
                    {
                        string tran = "";
                        string NewTran = "";    

                        int index = 0;

                        using (var client = new WebClient())
                        {
                            var Values = new NameValueCollection();
                            Values["saltkey"] = "MH82#E@IN038#F6230UQ3";
                            Values["secretkey"] = "MH82#E@IN038#F6230UQ3";
                            Values["stanno"] = hdnApiTransID.Value.Split('|')[0];
                            
                           // cls.update_data("INSERT INTO RECHARGEURL(URL,TRANSID)VALUES('" + tran + "','') ");
                           // var response = client.UploadValues(AEPS_URL, Values);

                            var responseString = ObjData.CheckStatus(Values);

                            JObject jObject = JObject.Parse(responseString);


                            if (jObject["statuscode"] != null)
                            {
                                if (jObject["statuscode"].ToString().ToUpper() == "000")
                                {
                                    //{"statuscode":"TXN","status":"Transaction Successful","data":{"statuscode":"000","message":"Success","Data":[{"mobile":"7894808284","stan_no":"MHE8D21B4B99154A75BBD1B153BEADC9A9","rrn":"","iin":"607094","bc_id":"BC712165458","amount":"100.00","status":"PENDING","trnx_date":"16-05-2020 22:19:46","typeoftxn":"CW","bankname":"State Bank of India","bankresponsemessage":"","bankcode":"","bankmessage":"","txnfrom":"ICICI AePS","routetype":"Web","refunddate":"","remarks":"","devicesno":"1636I141000","deviceid":"Morpho.SmartChip"}]}}
                                    if (jObject["Data"][0]["status"].ToString().Trim().ToUpper() == "SUCCESS")
                                    {
                                        string MemberID = cls.select_data_scalar_string("select loginid from Member where BCRegistrationID='" + jObject["Data"][0]["bc_id"].ToString() + "'");
                                        string RRNBankreferno = jObject["Data"][0]["rrn"].ToString();
                                        DataTable dtHistory1 = cls.select_data_dt("Select * from AEPS_History where TransactionId='" + hdnApiTransID.Value + "' and  Status='Pending'");

                                        string narration = "Aeps TransID-" + dtHistory1.Rows[0]["referenceno"].ToString();

                                        cls.update_data("update AEPS_History set Status='SUCCESS', bankrrn='" + RRNBankreferno + "' where TransactionId='" + hdnApiTransID.Value + "'");
                                        string loginid = cls.select_data_scalar_string("select loginid from Member where msrno=" + dtHistory1.Rows[0]["msrno"].ToString() + "");
                                        cls.update_data("Exec [ProcMLM__EWalletTransaction_AEPS2] '" + loginid + "'," + dtHistory1.Rows[0]["Amount"].ToString() + " , 'Cr', '" + narration + "', 'WEB', '" + System.Guid.NewGuid().ToString().Replace("-", "C") + "' ");
                                        cls.update_data("Exec [ProcAEPSMahagram_DistributeCommission] '" + dtHistory1.Rows[0]["msrno"].ToString() + "'," + dtHistory1.Rows[0]["Amount"].ToString() + " ," + dtHistory1.Rows[0]["Amount"].ToString() + ", '" + dtHistory1.Rows[0]["referenceno"].ToString() + "', '" + System.Guid.NewGuid().ToString().Replace("-", "C") + "' ");


                                    }
                                    else if (jObject["Data"][0]["status"].ToString().Trim().ToUpper() == "PENDING")
                                    {
                                        string RRNBankreferno = jObject["Data"][0]["rrn"].ToString();
                                        cls.update_data("update AEPS_History set Status='Pending', bankrrn='' where TransactionId='" + hdnApiTransID.Value + "'");
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('" + jObject["Data"][0]["status"].ToString().Trim().ToUpper() + "');", true);
                                    }
                                    else if (jObject["Data"][0]["status"].ToString().Trim().ToUpper() == "FAILURE")
                                    {
                                        string RRNBankreferno = jObject["Data"][0]["rrn"].ToString();
                                        cls.update_data("update AEPS_History set Status='FAILURE', bankrrn='' where TransactionId='" + hdnApiTransID.Value + "'");
                                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('" + jObject["Data"][0]["status"].ToString().Trim().ToUpper() + "');", true);
                                    }
                                }
                                else if (jObject["statuscode"].ToString().ToUpper() == "ERR")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Key", "alert('" + jObject["status"].ToString() + "');", true);
                                }
                            }
                            else
                            {
                                cls.update_data("INSERT INTO RECHARGEURL(URL,TRANSID)VALUES('" + responseString + "','') ");

                            }
                        }
                    }
                }
            }
            fillEWalletTransaction();
        }
        catch (Exception ex)
        {
            cls.update_data("INSERT INTO RECHARGEURL(URL,TRANSID)VALUES('" + ex.Message + "','') ");
        }
    }
}