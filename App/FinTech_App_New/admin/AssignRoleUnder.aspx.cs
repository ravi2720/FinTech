using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AssignRoleUnder : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtMember;
    DataTable dt = new DataTable();
    Page page;
    ActionButtonPermission actionButtonPermission;
    public Company company;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.AssignRole, dtMember.Rows[0]["RoleID"].ToString());
            company = Company.GetCompanyInfo();

            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(dllRole, "ActiveRoleFilter", "Name", "ID", "Select Role", company.MemberID);
                BindDropDown.FillDropDown(ddlDownRole, "ActiveRoleFilter", "Name", "ID", "Select Role",  company.MemberID);
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }

    }
    protected void btnAssign_Click(object sender, EventArgs e)
    {
        if (actionButtonPermission.Submit)
        {
            string str = "";
            if (ddlDownRole.SelectedIndex > 0 && dllRole.SelectedIndex > 0)
            {
                int dt = cls.update_data("AddEditRoleRegisterPermission '" + dllRole.SelectedValue + "','" + ddlDownRole.SelectedValue + "'");
                if (dt > 0)
                {
                    BindData();
                    ErrorShow.AlertMessage(page, "Assign Succesfully", ConstantsData.CompanyName);
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Already Assigned", ConstantsData.CompanyName);

                }
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Not Permission For Assign Role", ConstantsData.CompanyName);
        }

    }



    protected void dllRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    public void BindData()
    {
        DataTable dt = cls.select_data_dt("ManageRoleRegisterPermission 'GetByID','" + dllRole.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            rptAllPackage.DataSource = dt;
            rptAllPackage.DataBind();
        }
        else
        {
            rptAllPackage.DataSource = null;
            rptAllPackage.DataBind();
        }
    }

    protected void rptAllPackage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Delete")
        {
            if (actionButtonPermission.Delete)
            {
                Int32 Val = cls.update_data("ManageRoleRegisterPermission 'IsDelete','" + e.CommandArgument.ToString() + "'");
                if (Val > 0)
                {
                    BindData();
                    ErrorShow.AlertMessage(page, "Delete Succesfully", ConstantsData.CompanyName);
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Already Delete", ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Delete Assign Role", ConstantsData.CompanyName);
            }
        }
    }
}