using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddAEPSCommission : System.Web.UI.Page
{
    cls_connection objconnection = new cls_connection();
    DataTable dt, dtMember;
    Page page;
    public Company company;
    ActionButtonPermission actionButtonPermission;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.AepsCommission, dtMember.Rows[0]["RoleID"].ToString());
            company = Company.GetCompanyInfo();
            page = HttpContext.Current.CurrentHandler as Page;
            if (!IsPostBack)
            {
                FillData();
                BindDropDown.FillDropDown(ddlPackage, "ActivePackage", "Name", "ID", "Select Package", company.MemberID);
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }
    protected void FillData()
    {
        dt = objconnection.select_data_dt("EXEC ManageAEPSCommission 'Get','" + 0 + "' ,'" + ddlPackage.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            rptAepsCommission.DataSource = dt;
            rptAepsCommission.DataBind();
        }
        else
        {
            rptAepsCommission.DataSource = null;
            rptAepsCommission.DataBind();

        }
    }

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (actionButtonPermission.Submit)
        {
            if (!string.IsNullOrEmpty(txtStartVal.Text) && !string.IsNullOrEmpty(txtEndVal.Text) && !string.IsNullOrEmpty(txtsurcharged.Text))
            {
                #region [Insert]
                Int32 Val = objconnection.update_data("AddEditAEPSCommission '" + hdnRoleID.Value + "','" + ddlPackage.SelectedValue + "','" + txtStartVal.Text + "'," + txtEndVal.Text + "," + txtsurcharged.Text + ",'" + chkflatd.Checked + "','" + dtMember.Rows[0]["ID"].ToString() + "'");
                if (Val > 0)
                {
                    FillData();
                    ConstantsData.Clear(page.Controls);
                    ErrorShow.AlertMessage(page, "Updated Successfully", ConstantsData.CompanyName);
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Already Updated", ConstantsData.CompanyName);
                }
                #endregion
            }
            else
            {
                ErrorShow.AlertMessage(page, "Fill All Values", ConstantsData.CompanyName);
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Not Permission For Add AEPS Commission", ConstantsData.CompanyName);
        }
    }
    #endregion

    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillData();
    }


    private void EditData(string ID)
    {
        dt = objconnection.select_data_dt("EXEC ManageAEPSCommission 'Get','" + ID + "'");
        if (dt.Rows.Count > 0)
        {
            hdnRoleID.Value = ID;
            txtStartVal.Text = dt.Rows[0]["StartVal"].ToString();
            txtEndVal.Text = dt.Rows[0]["EndVal"].ToString();
            ddlPackage.SelectedValue = dt.Rows[0]["PackageID"].ToString();
            txtsurcharged.Text = dt.Rows[0]["Surcharge"].ToString();
            chkflatd.Checked = Convert.ToBoolean(dt.Rows[0]["IsFlat"].ToString());
        }
    }


    protected void rptAepsCommission_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            if (actionButtonPermission.Edit)
            {
                EditData(e.CommandArgument.ToString());
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Edit Aeps Commission", ConstantsData.CompanyName);
            }
        }
        else if (e.CommandName == "Delete")
        {
            if (actionButtonPermission.Delete)
            {
                Delete(id: Convert.ToInt32(e.CommandArgument.ToString()));
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Delete Commission", ConstantsData.CompanyName);
            }
        }
    }

    private void Delete(Int32 id)
    {
        int val = objconnection.update_data("ManageAEPSCommission 'IsDelete'," + id + "");
        if (val > 0)
        {
            FillData();
        }

    }
}