using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ProjectProccess : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                FillData();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int id = hdnid.Value == "0" ? 0 : Convert.ToInt32(hdnid.Value.ToString());
            string InputFile = string.Empty;
            string logo = "";
            InputFile = System.IO.Path.GetExtension(fileupload.FileName);
            string gd = Guid.NewGuid().ToString() + InputFile;
            if (fileupload.HasFile)
            {
                fileupload.PostedFile.SaveAs(MapPath("./images/") + gd);
            }
            logo = gd.ToString();
            DataTable dt = cls.select_data_dt("Exec AddEditProjectProcess " + id + ",'" + txtName.Text + "','" + txtPageURL.Text + "','" + logo + "','" + ckNewsDesc.Text + "','" + chkActive.Checked + "'");
            if (Convert.ToInt16(dt.Rows[0]["ID"].ToString()) > 0)
            {
                ErrorShow.Success(page: page, Message: "'" + dt.Rows[0]["Status"].ToString() + "'");
                Clear();
                FillData();
            }
            else
            {
                ErrorShow.Error(page1: page, Message: "'" + dt.Rows[0]["Status"].ToString() + "'");
            }
        }
        catch (Exception ex)
        {
            ErrorShow.Error(page1: page, Message: "Something went wrong");

        }

    }

    protected void FillData()
    {
        dt = cls.select_data_dt("EXEC ManageProjectProcess 'Get','" + 0 + "' ");
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
            Active(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }
    }

    private void FillNews(int id)
    {
        dt = cls.select_data_dt("EXEC ManageProjectProcess 'Get','" + id + "' ");
        txtName.Text = dt.Rows[0]["Name"].ToString();
        txtPageURL.Text = dt.Rows[0]["PageURL"].ToString();
        ckNewsDesc.Text = dt.Rows[0]["Description"].ToString();
    }


    private void Active(int id)
    {
        int val = cls.update_data("ManageProjectProcess @Action='IsActive',@ID=" + id + "");
        if (val > 0)
        {
            FillData();
        }

    }

    private void Clear()
    {
        txtPageURL.Text = "";
        txtName.Text = "";
        ckNewsDesc.Text = "";

    }
}