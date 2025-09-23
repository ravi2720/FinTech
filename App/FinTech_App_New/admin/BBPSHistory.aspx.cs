using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_BBPSHistory : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();

    DataTable dt = new DataTable();
    int Val = 0;
    DataTable dtMember;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", "1");
                BindDropDown.FillDropDown(ddlService, "ActiveServiceBySection", "Name", "ID", "Select Service", ConstantsData.BBPSServiceCategory.ToString());
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void FillTransactions()
    {

        dt = ObjData.select_data_dt("PROC_MANAGE_RECHARGE_HISTORY 'admin'," + ddlService.SelectedValue + ",'" + txtSeach.Text + "','" + txtFromDate.Text + "','" + txtToDate.Text + "','" + ddlStatus.SelectedValue + "'," + 1 + ","+ddlMember.SelectedValue+"");
        if (dt.Rows.Count > 0)
        {
            rptDataRecharge.DataSource = dt;
            rptDataRecharge.DataBind();

            lblSCount.InnerText = dt.Compute("Count(Amount)", "Status='Success'").ToString();
            lblPCount.InnerText = dt.Compute("Count(Amount)", "Status='Pending'").ToString();
            lblFCount.InnerText = dt.Compute("Count(Amount)", "Status='Failed'").ToString();
            lblSsum.InnerText = dt.Compute("SUM(Amount)", "Status='Success'").ToString();
            lblPSum.InnerText = dt.Compute("SUM(Amount)", "Status='Pending'").ToString();
            lblFSum.InnerText = dt.Compute("SUM(Amount)", "Status='Failed'").ToString();
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