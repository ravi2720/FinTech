using System;
using System.Data;
using System.Web;
using System.Web.UI;

public partial class Member_AEPSHistory : System.Web.UI.Page
{
    DataTable dtMember;
    cls_connection cls = new cls_connection();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)Session["dtMember"];

            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", dtMember.Rows[0]["ID"].ToString());
                txtFromDate.Text = txtToDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlM);
        }
    }

    private void fillTransactionDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Exec ManageAEPS_History '" + txtSeach.Text + "','" + dtMember.Rows[0]["ID"].ToString() + "','" + txtFromDate.Text + "', '" + txtToDate.Text + "','" + ddlStatus.SelectedValue + "'," + ddlMember.SelectedValue + "");
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

    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }

}
