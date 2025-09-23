using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_BBPSHistory : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();

    DataTable dt = new DataTable();
    int Val = 0;
    DataTable dtMember;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", dtMember.Rows[0]["ID"].ToString());
                BindDropDown.FillDropDown(ddlService, "ActiveServiceBySection", "Name", "ID", "Select Service", ConstantsData.BBPSServiceCategory.ToString());
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlM);
        }
    }

    private void FillTransactions()
    {

        dt = ObjData.select_data_dt("PROC_MANAGE_RECHARGE_HISTORY 'admin'," + ddlService.SelectedValue + ",'" + txtSeach.Text + "','" + txtFromDate.Text + "','" + txtToDate.Text + "','" + ddlStatus.SelectedValue + "'," + dtMember.Rows[0]["ID"].ToString() + "," + ddlMember.SelectedValue + "");
        if (dt.Rows.Count > 0)
        {
            rptDataRecharge.DataSource = dt;
            rptDataRecharge.DataBind();

        }
        else
        {
            rptDataRecharge.DataSource = null;
            rptDataRecharge.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillTransactions();
    }
}