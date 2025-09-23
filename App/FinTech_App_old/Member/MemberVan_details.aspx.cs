using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Member_memberupi_details : System.Web.UI.Page
{
    DataTable dtMember = new DataTable();
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                fillMemberVanDetails();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlM);
        }
    }

    private void fillMemberVanDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("exec PROC_MANAGE_MemberVan " + dtMember.Rows[0]["Msrno"].ToString() + ",'GetMemberVan'");
        if (dt.Rows.Count > 0)
        {
            rptVANDetails.DataSource = dt;
            rptVANDetails.DataBind();
            ViewState["dtExport"] = dt;
            DataTable dtTransaction = cls.select_data_dt("exec PROC_MANAGE_MemberVan " + dtMember.Rows[0]["Msrno"].ToString() + ",'GetVanTransaction'");
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
            rptVANDetails.DataSource = null;
            rptVANDetails.DataBind();
        }
    }
}