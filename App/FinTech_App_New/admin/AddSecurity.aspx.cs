using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddSecurity : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt, dtMember;
    Page page;
    public Company company;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            company = Company.GetCompanyInfo();
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;

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
            Int32 id = hdnid.Value == "0" ? 0 : Convert.ToInt32(hdnid.Value.ToString());
            DataTable dt = cls.select_data_dt("Exec AddEditSecurityTip " + id + ",'" + txtNewsName.Text + "','" + ckNewsDesc.Text + "','"+dtMember.Rows[0]["ID"].ToString()+"','"+ company.MemberID + "'");
            if (Convert.ToInt32(dt.Rows[0]["ID"].ToString()) > 0)
            {
                ErrorShow.AlertMessage(page,dt.Rows[0]["Status"].ToString(),ConstantsData.CompanyName);
                ConstantsData.Clear(page.Controls);
                FillData();
            }
            else
            {
                ErrorShow.AlertMessage(page,dt.Rows[0]["Status"].ToString(), ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, "Something went wrong", ConstantsData.CompanyName);
        }

    }

    protected void FillData()
    {
        dt = cls.select_data_dt("EXEC ManageSecurity 'GetByMsrno','" + company.MemberID + "' ");
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
            FillNews(id: Convert.ToInt32(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "Active")
        {
            Active(id: Convert.ToInt32(e.CommandArgument.ToString()));
        }
    }

    private void FillNews(int id)
    {
        dt = cls.select_data_dt("EXEC ManageSecurity 'Get','" + id + "' ");
        txtNewsName.Text = dt.Rows[0]["Name"].ToString();
        ckNewsDesc.Text = dt.Rows[0]["Description"].ToString();
    }


    private void Active(int id)
    {
        int val = cls.update_data("ManageSecurity @Action='IsActive',@ID=" + id + "");
        if (val > 0)
        {
            FillData();
        }

    }

   
}