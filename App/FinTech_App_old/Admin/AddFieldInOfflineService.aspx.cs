using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddFieldInOfflineService : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    Package package = new Package();
    DataTable dtMember = new DataTable();
    int Val = 0;
    Page page;

    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;

            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                GetOffLineServiceList();

            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }
    public void GetFieldName()
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManageOffLineserviceField 'GetByFormID'," + ddlFormName.SelectedValue + "");
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
    public void GetOffLineServiceList()
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManageOffLineservice 'GetAll',0");
            if (dt.Rows.Count > 0)
            {
                ddlFormName.DataSource = dt;
                ddlFormName.DataTextField = "Name";
                ddlFormName.DataValueField = "ID";
                ddlFormName.DataBind();
                ddlFormName.Items.Insert(0, new ListItem("Select Form", "0"));
            }
            else
            {
                ddlFormName.DataSource = null;
                ddlFormName.DataBind();
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

            if (package.IsAnyNullOrEmpty(package))
            {
                Val = ObjConnection.update_data("AddEditOffLineserviceField " + hdnPackageID.Value + ",'" + ddlFormName.SelectedValue + "','" + txtFieldName.Text + "','" + dllFieldType.SelectedValue + "'," + chkActive.Checked + ",'" + txtMaxLength.Text + "','" + txtMinLength.Text + "','" + chkMandatory.Checked + "'");
                if (Val > 0)
                {
                    ErrorShow.AlertMessage(page, "Add OfflineService Successfully", ConstantsData.CompanyName);
                    GetFieldName();
                    Clear();
                }
                else
                    ErrorShow.AlertMessage(page, "Duplicate OfflineService", ConstantsData.CompanyName);
            }
            else
            {
                ErrorShow.AlertMessage(page, "Enter All Required Field", ConstantsData.CompanyName);
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
            FillData(ID: e.CommandArgument.ToString());
        }

        if (e.CommandName == "Active")
        {
            Active(ID: e.CommandArgument.ToString());
        }
    }

    private void FillData(string ID)
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManageOffLineserviceField @Action='GetAll',@ID=" + ID + "");
            if (dt.Rows.Count > 0)
            {
                hdnPackageID.Value = ID;
                txtFieldName.Text = dt.Rows[0]["FieldName"].ToString();
                txtMinLength.Text = dt.Rows[0]["Min"].ToString();
                txtMaxLength.Text = dt.Rows[0]["MaxLen"].ToString();
                dllFieldType.SelectedValue = dt.Rows[0]["FieldType"].ToString();
                ddlFormName.SelectedValue = dt.Rows[0]["FormID"].ToString();
                chkMandatory.Checked = Convert.ToBoolean(dt.Rows[0]["Mandatory"].ToString());
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
        try
        {
            Val = ObjConnection.update_data("ManageOffLineserviceField @Action='IsActive',@ID=" + ID + "");
            if (Val > 0)
            {
                GetFieldName();
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }
    }

    private void Clear()
    {
        txtFieldName.Text = "";
        hdnPackageID.Value = "0";
    }

    protected void ddlFormName_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetFieldName();
    }
}