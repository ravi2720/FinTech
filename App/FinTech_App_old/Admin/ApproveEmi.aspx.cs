using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ApproveEmi : System.Web.UI.Page
{
    DataAccess objaccess = new DataAccess();
    DataTable dt = new DataTable();
    Page page;

    //protected void Page_Preint(object sender, EventArgs e)
    //{
    //    page = HttpContext.Current.CurrentHandler as Page;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            PAYMENTMODE();
            getdata();
        }
    }
    public void getdata()
    {
        dt = objaccess.GetDataTable("select * from View_unapproved_emi where isapprove=0 AND IS_REJECT=0");
        rptdata.DataSource = dt;
        rptdata.DataBind();
    }
    public void PAYMENTMODE()
    {
        DataTable dtpmode = new DataTable();
        dtpmode = objaccess.GetDataTable("Select Paymentmodeid,paymentmode From LOAN_PAYMENTMODE where isactive=1");
        ddlpaymode.DataSource = dtpmode;
        ddlpaymode.DataTextField = "paymentmode";
        ddlpaymode.DataValueField = "Paymentmodeid";
        ddlpaymode.DataBind();
        ddlpaymode.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void rptdata_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Page page = HttpContext.Current.CurrentHandler as Page;

        string transactionId = e.CommandArgument.ToString();

        if (e.CommandName == "Approve")
        {
            // Approve EMI via stored procedure
            string sql = $"EXEC sp_transaction_master_dummy_bulk '{transactionId}', 1";
            int result = objaccess.ExecuteQuery(sql);

            if (result > 0)
            {
                ErrorShow.AlertMessageWithRedirect(page, "EMI Approved Successfully!", "ApproveEMI.aspx", "Approval");
            }
            else
            {
                ErrorShow.AlertMessage(page, "Approval failed. Please try again.", "Error");
            }
        }
        else if (e.CommandName == "Reject")
        {
            // Reject EMI
            string rejectDate = DateTime.Now.ToString("yyyy-MM-dd");
            string updateSql = $"UPDATE LOAN_TRANSACTION_MASTER_DUMMY SET is_Reject = 1, Reject_BY = 1, REJECT_DATE = '{rejectDate}' WHERE transaction_id_temp = '{transactionId}'";
            int rows = objaccess.ExecuteQuery(updateSql);

            if (rows > 0)
            {
                ErrorShow.AlertMessageWithRedirect(page, "EMI Rejected Successfully!", "ApproveEMI.aspx", "Rejection");
            }
            else
            {
                ErrorShow.AlertMessage(page, "Rejection failed. Please try again.", "Error");
            }
        }
    }

}