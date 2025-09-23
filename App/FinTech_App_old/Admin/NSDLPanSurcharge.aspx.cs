using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_NSDLPanSurcharge : System.Web.UI.Page
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
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.Pan, dtMember.Rows[0]["RoleID"].ToString());
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
        dt = objconnection.select_data_dt("EXEC ManageNSDLPanSurcharge 'Get','" + 0 + "' ,'" + ddlPackage.SelectedValue + "'");
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
            if (!string.IsNullOrEmpty(txtCommission.Text) && !string.IsNullOrEmpty(txtSurcharge.Text) && ddlPackage.SelectedIndex> 0 && dllType.SelectedIndex > 0)
            {
                #region [Insert]
                Int32 Val = objconnection.update_data("AddEditNSDLPanSurcharge '" + hdnRoleID.Value + "','" + dllType.SelectedValue + "','" + txtSurcharge.Text + "'," + txtCommission.Text + "," + chkflatd.Checked + ",'" + ddlPackage.SelectedValue + "','" + dtMember.Rows[0]["ID"].ToString() + "'");
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
            ErrorShow.AlertMessage(page, "Not Permission For Add Aadhar Surcharge", ConstantsData.CompanyName);
        }
    }
    #endregion

    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillData();
    }


    private void EditData(string ID)
    {
        dt = objconnection.select_data_dt("EXEC ManageNSDLPanSurcharge 'Get','" + ID + "'");
        if (dt.Rows.Count > 0)
        {
            hdnRoleID.Value = ID;
            txtCommission.Text = dt.Rows[0]["Commission"].ToString();
            txtSurcharge.Text = dt.Rows[0]["Surcharge"].ToString();
            ddlPackage.SelectedValue = dt.Rows[0]["PackageID"].ToString();
            dllType.SelectedValue = dt.Rows[0]["Name"].ToString();
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
                ErrorShow.AlertMessage(page, "Not Permission For Edit Pan Surcharge", ConstantsData.CompanyName);
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
                ErrorShow.AlertMessage(page, "Not Permission For Delete Pan Surcharge", ConstantsData.CompanyName);
            }
        }
    }

    private void Delete(Int32 id)
    {
        int val = objconnection.update_data("ManageNSDLPanSurcharge 'IsDelete'," + id + "");
        if (val > 0)
        {
            FillData();
        }

    }
}