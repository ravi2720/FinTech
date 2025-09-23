using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_VanHistory : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["upiid"] != null)
                {
                    fillMemberUpiDetails(Convert.ToInt32(Request.QueryString["upiid"]));
                }
                else
                {
                    Response.Redirect("UPIDetails.aspx");
                }
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void fillMemberUpiDetails(int msrno)
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("exec PROC_MANAGEVan " + msrno + ",'GetMemberVan'");
        if (dt.Rows.Count > 0)
        {
            rptUPIDetails.DataSource = dt;
            rptUPIDetails.DataBind();
            DataTable dtTransaction = cls.select_data_dt("exec PROC_MANAGEVan " + msrno + ",'GetUpiTransaction'");
            if (dtTransaction.Rows.Count > 0)
            {
                lblTotalAmount.Text = dtTransaction.Compute("SUM(Amount)", "status='Success'").ToString();
                gvTransactionHistory.DataSource = dtTransaction;
                gvTransactionHistory.DataBind();
            }
            else
            {
                gvTransactionHistory.DataSource = null;
                gvTransactionHistory.DataBind();
            }
        }
        else
        {
            rptUPIDetails.DataSource = null;
            rptUPIDetails.DataBind();
        }
    }
}