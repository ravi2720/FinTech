using Newtonsoft.Json;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_AEPSWithdrawRequestShow : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        if (!IsPostBack)
        {
            txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");

            FillData();
            //if (Session["dtEmployee"] != null)
            //{
            //    FillData();
            //}
            //else
            //{
            //    Response.Redirect("logout.aspx");
            //}
        }
    }

    protected void FillData()
    {
        dt = cls.select_data_dt("EXEC PROC_AEPS_WITHDRAW_REPORT 'WITHDRAW_REPORT_ALL','','"+dllStatus.SelectedValue+"','" + txtfromdate.Text+"','"+txttodate.Text+"'");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();
        }
        else
        {
            repeater1.DataSource = null;
            repeater1.DataBind();

        }
    }


    protected void repeater1_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        //HiddenField hdnID = (HiddenField)e.Item.FindControl("hdnID");
        //if (e.CommandName == "IsApprove")
        //{
        //    cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + hdnID.Value);
        //    FillData();
        //}
        //if (e.CommandName == "IsDelete")
        //{
        //    cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE','" + hdnID.Value + "','','','','','',''");
        //    FillData();
        //}
        if (e.CommandName == "ViewBankDetails")
        {
            FillBankDetails(e.CommandArgument.ToString());
        }
        ////if (e.CommandName == "CheckStatus")
        ////{
        ////    DataTable dtHistory = new DataTable();
        ////    dtHistory = cls.select_data_dt("select * from AEPS_WithdrwalRequest where RequestID='" + e.CommandArgument.ToString().Trim() + "'");
        ////    if (dtHistory.Rows.Count > 0)
        ////    {
        ////        PayoutPaytmStatusCheckRequest ObjRequest = new PayoutPaytmStatusCheckRequest();
        ////        ObjRequest.orderId = e.CommandArgument.ToString();
        ////        string JsonRequest = ObjRequest.GetJson();
        ////        string result = ApiPostCallPaytmPayout.PostCall(JsonRequest, "https://dashboard.paytm.com/bpay/api/v1/disburse/order/query");
        ////        PayoutPaytmResultCheckStatusRoot bsObj = JsonConvert.DeserializeObject<PayoutPaytmResultCheckStatusRoot>(result);
        ////        //{"status":"FAILURE","statusCode":"DE_704","statusMessage":"Sub wallet not found","result":{"mid":"Quickp85977383514425","orderId":"b1d949190cc65048080bed80f8fd94aaffce","paytmOrderId":"202007022205524894971260","amount":"10.00","commissionAmount":"0.20","tax":"0.04","rrn":null,"beneficiaryName":null}}
        ////        //if (bsObj.status.ToUpper() == "SUCCESS")
        ////        //{
        ////        //    cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + hdnID.Value + ",'" + bsObj.result.paytmOrderId + "','" + bsObj.status + "','" + bsObj.statusCode + "','" + bsObj.statusMessage + "','" + bsObj.result.rrn + "','" + result + "'");

        ////        //    ErrorShow.AlertMessage(page, "Transaction Successfully Done");
        ////        //}
        ////        //else if (bsObj.status.ToUpper() == "PENDING")
        ////        //{
        ////        //    cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + hdnID.Value + ",'" + bsObj.result.paytmOrderId + "','" + bsObj.status + "','" + bsObj.statusCode + "','" + bsObj.statusMessage + "','" + bsObj.result.rrn + "','" + result + "'");

        ////        //    ErrorShow.AlertMessage(page, "Transaction in PENDING");
        ////        //}
        ////        //else if (bsObj.status.ToUpper() == "ACCEPTED")
        ////        //{
        ////        //    cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + hdnID.Value + ",'" + bsObj.result.paytmOrderId + "','" + bsObj.status + "','" + bsObj.statusCode + "','" + bsObj.statusMessage + "','" + bsObj.result.rrn + "','" + result + "'");

        ////        //    ErrorShow.AlertMessage(page, "Transaction in ACCEPTED");
        ////        //}
        ////        //else if (bsObj.status.ToUpper() == "FAILURE")
        ////        //{
        ////        //    cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE'," + hdnID.Value + ",'','" + bsObj.status + "','" + bsObj.statusCode + "','" + bsObj.statusMessage + "','','" + result + "'");

        ////        //    ErrorShow.AlertMessage(page, "Transaction FAILURE Successfully. Your Amount -" + dtHistory.Rows[0]["Amount"] + " refund . aeps wallet. with Charge - '" + dtHistory.Rows[0]["Charge"] + "'");
        ////        //}
        ////    }
        ////    else
        ////    {
        ////        ErrorShow.AlertMessage(page, "Record not found");
        ////    }
        ////}
        //FillData();
    }

    private void FillBankDetails(string msrno)
    {
        try
        {
            dt = cls.select_data_dt("select * from View_MemberBankList where ID = " + msrno + "");
            rptBankData.DataSource = dt;
            rptBankData.DataBind();

        }
        catch (Exception ex)
        {
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
        //HiddenField hdnID = new HiddenField(); 
        //for (int i = 0; i < repeater1.Items.Count; i++)
        //{
        //    var item = repeater1.Items[i];
        //    hdnID = item.FindControl("hdnID") as HiddenField;
        //}
        if (!string.IsNullOrEmpty(hdnIDValue.Value))
        {
            int check = cls.select_data_scalar_int("select count(*) from AEPS_WithdrwalRequest where TRANSACTIONID = '" + txtBenkRefID.Text.Trim() + "'");
            if (!string.IsNullOrEmpty(txtBenkRefID.Text) && check == 0 && Convert.ToInt16(hdnIDValue.Value) > 0)
            {
                cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + hdnIDValue.Value + ",'" + txtBenkRefID.Text.Trim() + "'");
                FillData();
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }
}