using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;

public partial class Admin_AEPSListPendingWithdrwal : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                fillTransactionDetails();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void fillTransactionDetails()
    {
        DataTable dtNew = new DataTable();
        dtNew = cls.select_data_dt("EXEC PROC_AEPS_WITHDRAW_REPORT 'PENDING_WITHDRAW_REPORT'");
        if (dtNew.Rows.Count > 0)
        {
            //gvTransactionHistory.DataSource = dtNew;
            //gvTransactionHistory.DataBind();
            //litrecordcount.Text = dtNew.Rows.Count.ToString();
            //ViewState["dtExport"] = dtNew;
        }
        else
        {
            //gvTransactionHistory.DataSource = null;
            //gvTransactionHistory.DataBind();
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
        fillTransactionDetails();
    }

    protected void btnOrderID_Click(object sender, EventArgs e)
    {
        cls_connection cls = new cls_connection();
        Button btnEdit = (Button)sender;
        GridViewRow Grow = (GridViewRow)btnEdit.NamingContainer;

        TextBox txtDepositBankDetails = (TextBox)Grow.FindControl("txtDepositBankDetails");
        TextBox txtTransID = (TextBox)Grow.FindControl("txtTransID");
        TextBox txtRemark = (TextBox)Grow.FindControl("txtRemark");
        HiddenField hddMsrNo = (HiddenField)Grow.FindControl("hddMsrNo");
        HiddenField ID = (HiddenField)Grow.FindControl("hddID");
        cls.update_data("[ProcAEPS_AddEditWithdrawalRequest] '" + ID.Value + "','" + hddMsrNo.Value + "',0, '" + txtDepositBankDetails.Text + "', '" + txtTransID.Text + "', '', '" + txtRemark.Text + "',''");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Update successfully');location.replace('AEPS_ListPendingWithdrwal.aspx')", true);
    }

}