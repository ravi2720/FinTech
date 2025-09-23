using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_MemberList : System.Web.UI.Page
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
                BindDropDown.FillDropDown(dllRole, "RoleBasePermission", "Name", "ID", "Select Role", dtMember.Rows[0]["RoleID"].ToString());
                FillMember();
            }
        }
        else
        {
            Response.Redirect("default.aspx");
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillMember();
    }
    private void FillMember()
    {
        DataTable dt;
        if (!string.IsNullOrEmpty(txtCityName.Text))
        {
            dt = ObjConnection.select_data_dt("ManageMemberList '" + txtCityName.Text + "'");
        }
        else
        {
            dt = ObjConnection.select_data_dt("ManageMemberList '" + txtfromdate.Text + "','" + txttodate.Text + "','" + txtCityName.Text + "'," + dtMember.Rows[0]["ID"].ToString() + ",'" + dllRole.SelectedValue + "'");

        }
        rptData.DataSource = dt;
        rptData.DataBind();
    }


    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Active")
        {
            Active(ID: e.CommandArgument.ToString());
        }

    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageMember @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            FillMember();
        }
    }



}