using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reatiler_ManageNews : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    public Company company;

    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();

        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
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
            int id = hdnid.Value == "0" ? 0 : Convert.ToInt32(hdnid.Value.ToString());
            DataTable dt = cls.select_data_dt("Exec AddEditNews " + id + ",N'" + txtNewsName.Text + "',N'" + ckNewsDesc.Text + "','" + dtMember.Rows[0]["ID"].ToString() + "','" + company.ID + "'");
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
            ErrorShow.AlertMessage(page, ex.InnerException.Message.ToString(), ConstantsData.CompanyName);
        }

    }

    protected void FillData()
    {
        dt = cls.select_data_dt("EXEC ManageNews 'Get',0,'" + company.ID + "'");
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
        dt = cls.select_data_dt("EXEC ManageNews 'Get','" + id + "','" + company.MemberID + "'");
        txtNewsName.Text = dt.Rows[0]["Name"].ToString();
        ckNewsDesc.Text = dt.Rows[0]["Description"].ToString();
    }


    private void Active(int id)
    {
        int val = cls.update_data("ManageNews 'IsActive'," + id + ",'" + company.MemberID + "'");
        if (val > 0)
        {
            FillData();
        }

    }
}