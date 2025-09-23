using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddSupportSystem : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt, dtMember;
    Page page;
    public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            page = HttpContext.Current.CurrentHandler as Page;
            company = Company.GetCompanyInfo();
            if (!IsPostBack)
            {
                FillData();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int id = hdnid.Value == "0" ? 0 : Convert.ToInt32(hdnid.Value.ToString());
            DataTable dt = cls.select_data_dt("Exec AddEditSupportSystem " + id + ",'" + txtNewsName.Text + "','" + ckNewsDesc.Text + "',"+ dtMember.Rows[0]["ID"].ToString() + ",'" + company.MemberID + "'");
            if (Convert.ToInt16(dt.Rows[0]["ID"].ToString()) > 0)
            {
                ErrorShow.AlertMessage(page,dt.Rows[0]["Status"].ToString(),ConstantsData.CompanyName);
                ConstantsData.Clear(page.Controls);
                FillData();
            }
            else
            {
                ErrorShow.AlertMessage(page, dt.Rows[0]["Status"].ToString(), ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }

    }

    protected void FillData()
    {
        dt = cls.select_data_dt("EXEC ManageSupportSystem 'Get','" + 0 + "','" + company.MemberID + "'");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();
        }
        else
        {
            repeater1.DataSource = null;
            repeater1.DataBind();

        }
    }

    protected void repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            hdnid.Value = e.CommandArgument.ToString();
            FillNews(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "Active")
        {
            Active(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }
    }

    private void FillNews(int id)
    {
        dt = cls.select_data_dt("EXEC ManageSupportSystem 'Get','" + id + "','" + company.MemberID + "'");
        txtNewsName.Text = dt.Rows[0]["Name"].ToString();
        ckNewsDesc.Text = dt.Rows[0]["Description"].ToString();
    }


    private void Active(int id)
    {
        int val = cls.update_data("ManageSupportSystem @Action='IsActive',@ID=" + id + ",'" + company.MemberID + "'");
        if (val > 0)
        {
            FillData();
        }

    }

}