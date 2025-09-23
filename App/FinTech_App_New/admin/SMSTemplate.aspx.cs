using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_SMSTemplate : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt, dtMember;
    int Val = 0;
    Page page;
    Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            company = Company.GetCompanyInfo();
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(dllSMSCategory, "ActiveSMSCategory", "Name", "ID", "Select SMSCategory");
                GetSMSTemplate();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }

    }

    public void GetSMSTemplate()
    {
        dt = ObjConnection.select_data_dt("ManageSMSTemplate 'GetByMsrno',"+ company.MemberID+ "");
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
        if (dllSMSCategory.SelectedIndex > 0)
        {
            Val = ObjConnection.update_data("AddEditSMSTemplate " + hdnRoleID.Value + ",'" + dllSMSCategory.SelectedValue + "','" + txtTemplateName.Text + "','" + txtTemplateID.Text + "','" + dtMember.Rows[0]["ID"].ToString() + "','" + chkActive.Checked + "','"+company.MemberID+"'");
            if (Val > 0)
            {
                ErrorShow.AlertMessage(page, "SMS Template Update Successfully", ConstantsData.CompanyName);
                GetSMSTemplate();
                Clear();
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Select Category", ConstantsData.CompanyName);
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
        dt = ObjConnection.select_data_dt("ManageSMSTemplate 'GetAll'," + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnRoleID.Value = ID;
            dllSMSCategory.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
            txtTemplateID.Text = dt.Rows[0]["TemplateID"].ToString();
            txtTemplateName.Text = dt.Rows[0]["Template"].ToString();
            chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageSMSTemplate 'IsActive'," + ID + "");
        if (Val > 0)
        {
            ErrorShow.AlertMessage(page, ConstantsData.SMSCategoryActive, ConstantsData.CompanyName);
            GetSMSTemplate();
        }
    }

    private void Clear()
    {
        txtTemplateName.Text= txtTemplateID.Text = "";
        chkActive.Checked = false;
        hdnRoleID.Value = "0";
    }
}