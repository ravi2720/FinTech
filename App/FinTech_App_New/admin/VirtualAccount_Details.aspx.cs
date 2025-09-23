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
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                if (Request.QueryString["vaid"] != null)
                {
                    fillMemberUpiDetails(Convert.ToInt16(Request.QueryString["vaid"]));
                }
                else
                {
                    Response.Redirect("UPIDetails.aspx");
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void fillMemberUpiDetails(int msrno)
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("exec PROC_MANAGEVirtualAccount " + msrno + ",'GetVirtuaAccount'");
        if (dt.Rows.Count > 0)
        {
            rptVirtualAccountDetails.DataSource = dt;
            rptVirtualAccountDetails.DataBind();
            ViewState["dtExport"] = dt;
            DataTable dtTransaction = cls.select_data_dt("exec PROC_MANAGEVirtualAccount " + msrno + ",'GetVirtua_UPI_Transaction'");
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
            rptVirtualAccountDetails.DataSource = null;
            rptVirtualAccountDetails.DataBind();
        }
    }

    protected void rptVirtualAccountDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hdnVirtualID = (HiddenField)e.Item.FindControl("hdnVirtualID");
            if (hdnVirtualID.Value != null)
            {
                Repeater rptVirtualAccounts = (Repeater)e.Item.FindControl("rptVirtualAccounts");
                DataTable dtAccountDetails = cls.select_data_dt("select * from Member_VirtualAccount_Details where referenceID="+ hdnVirtualID.Value + "");
                if (dtAccountDetails.Rows.Count > 0)
                {
                    rptVirtualAccounts.DataSource = dtAccountDetails;
                    rptVirtualAccounts.DataBind();
                }
            }
        }
    }
    
}