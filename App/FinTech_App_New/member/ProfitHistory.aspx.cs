using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_ProfitHistory : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dtMember = new DataTable();
    int Val = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", dtMember.Rows[0]["ID"].ToString());
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlM);
        }
    }
    private void FillData()
    {
        DataTable dt = new DataTable();
        dt = ObjConnection.select_data_dt("Exec PROC_WALLET_REPORT 'profitEwalletTransaction'," + Convert.ToInt32(dtMember.Rows[0]["msrno"].ToString()) + ",'" + txtfromdate.Text + "','" + txttodate.Text + "','0','" + ddlMember.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            rptData.DataSource = dt;
            rptData.DataBind();
        }
        else
        {
            rptData.DataSource = null;
            rptData.DataBind();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }
}