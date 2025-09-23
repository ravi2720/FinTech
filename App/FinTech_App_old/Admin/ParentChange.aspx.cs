using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ParentChange : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", "1");
                BindDropDown.FillDropDown(ddlRole, "RoleBasePermission", "Name", "ID", "Select Role", dtMember.Rows[0]["RoleID"].ToString());
                BindDropDown.FillDropDown(ddlParentID, "MemberList", "LoginID", "ID", "Select Member", "1");
                FillData();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }
    
    private void FillData()
    {
        rptData.DataSource = cls.select_data_dt("select * from View_ParentChangeInformation");
        rptData.DataBind();

    }
    private void Clear()
    {
        ddlRole.SelectedValue = "0";
        ddlParentID.SelectedValue = "0";
        ddlMember.SelectedValue = "0";

    }


    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where Msrno=" + ddlMember.SelectedValue + "");
        lblCurrentParent.Text = dt.Rows[0]["Parent"].ToString();
        ddlRole.SelectedValue = dt.Rows[0]["RoleID"].ToString();
        hdnParentID.Value = dt.Rows[0]["ParentID"].ToString();
        hdnRoleID.Value = dt.Rows[0]["RoleID"].ToString();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = cls.select_data_dt("Exec ParentChange " + ddlMember.SelectedValue + "," + ddlRole.SelectedValue + ",'" + ddlParentID.Text + "'");

            if (Convert.ToInt16(dt.Rows[0]["ID"].ToString()) > 0)
            {
                ErrorShow.AlertMessage(page, dt.Rows[0]["Status"].ToString(), ConstantsData.CompanyName);
                FillData();
                ConstantsData.Clear(page.Controls);
            }
            else
            {
                ErrorShow.AlertMessage(page, dt.Rows[0]["Status"].ToString(), ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.Error(page1: page, Message: "Something went wrong");
        }

    }

    protected void ddlParentID_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ddlRole.SelectedValue) > 0 && Convert.ToInt16(ddlParentID.SelectedValue) > 0)
        {
            DataTable dt = cls.select_data_dt("Exec CheckChangeParentValid " + ddlMember.SelectedValue + "," + ddlRole.SelectedValue + ",'" + ddlParentID.Text + "'");
            if (!Convert.ToBoolean(dt.Rows[0]["ID"]))
            {
                ddlParentID.SelectedValue = "0";

                ErrorShow.AlertMessage(page, dt.Rows[0]["Status"].ToString(), ConstantsData.CompanyName);
            }
        }

    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt16(ddlRole.SelectedValue) > 0 && Convert.ToInt16(ddlParentID.SelectedValue) > 0)
        {
            DataTable dt = cls.select_data_dt("Exec CheckChangeParentValid " + ddlMember.SelectedValue + "," + ddlRole.SelectedValue + ",'" + ddlParentID.Text + "'");
            if (!Convert.ToBoolean(dt.Rows[0]["ID"]))
            {
                ddlRole.SelectedValue = "0";
                ddlParentID.SelectedValue = "0";
                ErrorShow.AlertMessage(page, dt.Rows[0]["Status"].ToString(), ConstantsData.CompanyName);
            }
        }
    }
}