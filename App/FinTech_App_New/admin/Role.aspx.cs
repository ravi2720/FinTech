using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_Role : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    Role role = new Role();
    DataTable dtMember;
    int Val = 0; Page page;
    ActionButtonPermission actionButtonPermission;
    Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            company = Company.GetCompanyInfo();
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.Role, dtMember.Rows[0]["RoleID"].ToString());
            if (!IsPostBack)
            {
                GetRole();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }


    public void GetRole()
    {
        dt = ObjConnection.select_data_dt("ManageRole 'GetAll',0,'" + company.MemberID + "'");
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (actionButtonPermission.Submit)
        {
            role.Name = txtRoleName.Text;
            role.ID = Convert.ToInt32(hdnRoleID.Value);
            role.Prefix = txtPrefix.Text;
            role.StartID = txtStartID.Text;
            role.IsActive = chkActive.Checked;
            role.ResCount = Convert.ToInt32(txtResCount.Text);
            if (role.IsAnyNullOrEmpty(role))
            {
                Val = ObjConnection.update_data("AddEditRole " + role.ID + ",'" + role.Name + "','" + role.Prefix + "'," + role.IsDelete + "," + role.IsActive + ",'" + role.StartID + "'," + role.ResCount + ",'" + txtPrice.Text + "','" + dtMember.Rows[0]["ID"].ToString() + "','" + chkOutSide.Checked + "','"+ company.MemberID + "'");
                if (Val > 0)
                {
                    GetRole();
                    Clear();
                    ErrorShow.AlertMessage(page, "Role Update Successfully", ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Fill All Data", ConstantsData.CompanyName);
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Not Permission For Add Role", ConstantsData.CompanyName);
        }
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {
            if (actionButtonPermission.Edit)
            {
                FillData(ID: e.CommandArgument.ToString());
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Edit Role", ConstantsData.CompanyName);
            }
        }

        else if (e.CommandName == "Active")
        {
            if (actionButtonPermission.Active)
            {
                Active(ID: e.CommandArgument.ToString());
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Active Role", ConstantsData.CompanyName);
            }
        }

        else if (e.CommandName == "OutSide")
        {
            OutSide(ID: e.CommandArgument.ToString());
        }
    }

    private void FillData(string ID)
    {
        dt = ObjConnection.select_data_dt("ManageRole 'GetAll'," + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnRoleID.Value = ID;
            txtRoleName.Text = dt.Rows[0]["Name"].ToString();
            txtPrefix.Text = dt.Rows[0]["Prefix"].ToString();
            txtStartID.Text = dt.Rows[0]["IDStarting"].ToString();
            txtResCount.Text = dt.Rows[0]["ResCount"].ToString();
            txtPrice.Text = dt.Rows[0]["Price"].ToString();
            chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
            chkOutSide.Checked = Convert.ToBoolean(dt.Rows[0]["OutSide"].ToString());
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageRole 'IsActive'," + ID + "");
        if (Val > 0)
        {
            GetRole();
            ErrorShow.AlertMessage(page, "Role Active/DeActive Successfully", ConstantsData.CompanyName);
        }
    }

    private void OutSide(string ID)
    {
        Val = ObjConnection.update_data("ManageRole 'IsOutSide'," + ID + "");
        if (Val > 0)
        {
            GetRole();
            ErrorShow.AlertMessage(page, "Role OutSide Active/DeActive Successfully", ConstantsData.CompanyName);
        }
    }
    private void Clear()
    {
        txtPrefix.Text = txtRoleName.Text = "";
        chkActive.Checked = false;
        hdnRoleID.Value = "0";
        txtPrice.Text = "0";
    }
}