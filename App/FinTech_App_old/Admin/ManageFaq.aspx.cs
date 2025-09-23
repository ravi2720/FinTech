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

        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                FillData();
            }
            else
            {
                Response.Redirect("index.aspx");
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Page page = HttpContext.Current.CurrentHandler as Page;  // Add this line

            int id = hdnid.Value == "0" ? 0 : Convert.ToInt16(hdnid.Value.ToString());
            DataTable dt = cls.select_data_dt("Exec AddEditFaq " + id + ",'" + txtNewsName.Text + "','" + ckNewsDesc.Text + "'");
            if (Convert.ToInt16(dt.Rows[0]["ID"].ToString()) > 0)
            {
                ErrorShow.AlertMessage(page, "Record Inserted Successfully", "Success");
                Clear();
                FillData();
            }
            else
            {
                ErrorShow.Error(page, dt.Rows[0]["Status"].ToString());
            }
        }
        catch (Exception ex)
        {
            Page page = HttpContext.Current.CurrentHandler as Page; // Also here
            ErrorShow.Error(page, "Something went wrong");
        }
    }


    protected void FillData()
    {
        dt = cls.select_data_dt("EXEC ManageFaq 'Get','" + 0 + "'");
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
        if(e.CommandName=="Edit")
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
        dt = cls.select_data_dt("EXEC ManageFaq 'Get','" + id + "'");
        txtNewsName.Text = dt.Rows[0]["FaqSubject"].ToString();
        ckNewsDesc.Text= dt.Rows[0]["Description"].ToString();
    }


    private void Active(int id)
    {
        int val = cls.update_data("ManageFaq @Action='IsActive',@ID=" + id + "'");
        if (val > 0)
        {
            FillData();
        }

    }

    private void Clear()
    {
        txtNewsName.Text = "";
        ckNewsDesc.Text = "";

    }
}