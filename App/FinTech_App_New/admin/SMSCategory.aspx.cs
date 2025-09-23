using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SMSCategory : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt, dtMember;
    int Val = 0;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                GetSMSCategory();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }

    }

    public void GetSMSCategory()
    {
        dt = ObjConnection.select_data_dt("ManageSMSCategory 'GetAll'");
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
        Val = ObjConnection.update_data("AddEditSMSCategory " + hdnRoleID.Value + ",'" + txtName.Text + "','" + chkActive.Checked + "'");
        if (Val > 0)
        {
            ErrorShow.AlertMessage(page, ConstantsData.SMSCategory, ConstantsData.CompanyName);
            GetSMSCategory();
            Clear();
        }
    }

    protected void repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
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
        dt = ObjConnection.select_data_dt("ManageSMSCategory 'GetAll'," + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnRoleID.Value = ID;
            txtName.Text = dt.Rows[0]["Name"].ToString();
            chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageSMSCategory 'IsActive'," + ID + "");
        if (Val > 0)
        {
            ErrorShow.AlertMessage(page, ConstantsData.SMSCategoryActive, ConstantsData.CompanyName);
            GetSMSCategory();
        }
    }

    private void Clear()
    {
        txtName.Text = "";
        chkActive.Checked = false;
        hdnRoleID.Value = "0";
    }
}