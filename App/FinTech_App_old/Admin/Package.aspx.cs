using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Package : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    Package package = new Package();
    DataTable dtMember, dt;
    int Val = 0;
    Page page;
    Company company;
    ActionButtonPermission actionButtonPermission;
    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            company = Company.GetCompanyInfo();
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.Package, dtMember.Rows[0]["RoleID"].ToString());

            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(dllRole, "RoleBasePermission", "Name", "ID", "Select Role", dtMember.Rows[0]["RoleID"].ToString());
                GetPackage();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    public void GetPackage()
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManagePackage 'GetByMSrno'," + company.MemberID + "");
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
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (actionButtonPermission.Submit)
            {
                package.Name = txtPackageName.Text;
                package.ID = Convert.ToInt32(hdnPackageID.Value);
                package.Price = Convert.ToDecimal(txtPrice.Text);
                package.IsActive = chkActive.Checked;
                if (package.IsAnyNullOrEmpty(package))
                {
                    Val = ObjConnection.update_data("AddEditPackage " + package.ID + ",'" + package.Name + "','" + package.Price + "'," + package.IsActive + "," + dtMember.Rows[0]["ID"].ToString() + ",'" + txtCapping.Text + "','" + dllRole.SelectedValue + "','"+company.MemberID+"'");
                    if (Val > 0)
                    {
                        ObjConnection.update_data("InsertOperatorPackage " + Val + "");
                        ErrorShow.AlertMessage(page, ConstantsData.PackageMessage, ConstantsData.CompanyName);
                        GetPackage(); Clear();
                    }
                    else
                        ErrorShow.AlertMessage(page, ConstantsData.PackageMessage, ConstantsData.CompanyName);
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Enter All Required Field", ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Add Package", ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
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
                ErrorShow.AlertMessage(page, "Not Permission For Edit Package", ConstantsData.CompanyName);
            }
        }

        if (e.CommandName == "Active")
        {
            if (actionButtonPermission.Active)
            {
                Active(ID: e.CommandArgument.ToString());
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Active Package", ConstantsData.CompanyName);
            }
        }
    }

    private void FillData(string ID)
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManagePackage @Action='GetAll',@ID=" + ID + "");
            if (dt.Rows.Count > 0)
            {
                hdnPackageID.Value = ID;
                dllRole.SelectedValue = dt.Rows[0]["RoleID"].ToString();
                txtPackageName.Text = dt.Rows[0]["Name"].ToString();
                txtPrice.Text = dt.Rows[0]["Price"].ToString();
                txtCapping.Text = dt.Rows[0]["Capping"].ToString();
                chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManagePackage @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            GetPackage();
        }
    }

    private void Clear()
    {
        txtPackageName.Text = "";
        txtPrice.Text = "0";
        chkActive.Checked = false;
        hdnPackageID.Value = "0";
    }
}