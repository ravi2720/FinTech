using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EMPActivity : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dtMember;
    int Val = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", "1");
                BindDropDown.FillDropDown(ddlService, "ServiceIDList", "Name", "ServiceCode", "Select Service");
                BindDropDown.FillDropDown(ddlEmp, "emplist","Name", "ID","Select Emp");
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }
    private void FillData()
    {
        DataTable dt = new DataTable();
        dt = ObjConnection.select_data_dt("Exec ManageActivityEmp 'GetReport','" + txtfromdate.Text + "','" + txttodate.Text + "','" + ddlEmp.SelectedValue + "','" + ddlMember.SelectedValue + "','" + ddlService.SelectedValue + "'");
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

    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillData();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }
}