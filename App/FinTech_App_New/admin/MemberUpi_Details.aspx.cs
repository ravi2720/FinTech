using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UPIDetails : System.Web.UI.Page
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
                    fillMemberUpiDetails(Convert.ToInt16(Request.QueryString["upiid"]));
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
        dt = cls.select_data_dt("exec PROC_MANAGEUPI " + msrno + ",'GetMemberUpi'");
        if (dt.Rows.Count > 0)
        {
            rptUPIDetails.DataSource = dt;
            rptUPIDetails.DataBind();
            ViewState["dtExport"] = dt;
            DataTable dtTransaction = cls.select_data_dt("exec PROC_MANAGEUPI " + msrno + ",'GetUpiTransaction'");
            if (dtTransaction.Rows.Count > 0)
            {
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