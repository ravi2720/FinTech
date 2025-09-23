using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ChangeRole : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt, dtMember;
    protected void Page_Load(object sender, EventArgs e)
    {
        dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
        if (!IsPostBack)
        {
            BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", "1");

            BindDropDown.FillDropDown(ddlRole, "RoleBasePermission", "Name", "ID", "Select Role", dtMember.Rows[0]["RoleID"].ToString());
        }
    }
   

    private void FillMember()
    {
        DataTable dtMember = ObjConnection.select_data_dt("select (LoginID + ' ( ' + Name + ' ) ')as Name,msrno from Member where isactive=1");
        Common.BindDropDown(ddlMember, dtMember, "Name", "msrno");
        ddlMember.Items.Insert(0, new ListItem("-- Select Member --", "0"));
    }

    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = ObjConnection.select_data_dt("select * from [dbo].[VIEW_MEMBERDETAILS] where msrno='" + ddlMember.SelectedValue + "'");
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

   
    protected void btnChangeRole_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = ObjConnection.select_data_dt("select * from [dbo].[VIEW_MEMBERDETAILS] where msrno='" + ddlMember.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            DataTable dtVal;
            dtVal = ObjConnection.select_data_dt("ChangeRole '" + dt.Rows[0]["Msrno"].ToString() + "'," + ddlRole.SelectedValue + ","+ dt.Rows[0]["RoleID"].ToString() + ",'" + dt.Rows[0]["LoginID"].ToString() + "'");
            if (dtVal.Rows.Count > 0)
            {
                string[] ValueArray = new string[4];
                ValueArray[0] = dt.Rows[0]["Name"].ToString();
                ValueArray[1] = dtVal.Rows[0]["LoginID"].ToString();
                ValueArray[2] = dt.Rows[0]["Password"].ToString();
                ValueArray[3] = dt.Rows[0]["LoginPin"].ToString();
               

            }
        }
    }
}