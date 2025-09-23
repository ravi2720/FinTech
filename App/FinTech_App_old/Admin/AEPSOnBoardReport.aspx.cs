using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AEPSOnBoardReport : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            if (!IsPostBack)
            {
                fillTransactionDetails();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void fillTransactionDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("AEPSOnBoardReport");
        if (dt.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();
        }
        else
        {
            gvTransactionHistory.DataSource = null;
            gvTransactionHistory.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }


    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName== "BCID")
        {
            ViewState["Mobile"] = e.CommandArgument.ToString();
            BindMemberData(e.CommandArgument.ToString());
        }
    }

    private void BindMemberData(string Data)
    {
        DataTable dtMem = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where Mobile = " + Data + "");
        if (dtMem.Rows.Count > 0)
        {
            rptDatamem.DataSource = dtMem;
            rptDatamem.DataBind();
        }
        else
        {
            rptDatamem.DataSource = null;
            rptDatamem.DataBind();
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(ViewState["Mobile"].ToString()))
        {
            Int32 Val = cls.update_data("update member set BCRegistrationID='" + txtBCID.Text + "' where mobile='" + ViewState["Mobile"].ToString() + "'");
            if (Val > 0)
            {
                ErrorShow.AlertMessage(Page, "Update Successfully", ConstantsData.CompanyName);
                BindMemberData(ViewState["Mobile"].ToString());
                fillTransactionDetails();

            }
        }
        else
        {
            ErrorShow.AlertMessage(Page, "PLease Select Member", ConstantsData.CompanyName);
        }
    }
}