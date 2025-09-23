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
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                FillData();
                BindDropDown.FillDropDown(dllRole, "ActiveRole", "Name", "ID", "Select Role");
            }
            else
            {
                Response.Redirect("logout.aspx");
            }
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int id = hdnid.Value == "0" ? 0 : Convert.ToInt16(hdnid.Value.ToString());
            DataTable dt = cls.select_data_dt("Exec PROC_ADD_EDIT_PROCESS_AFTER_RES " + id + ","+ dllRole.SelectedValue + ",'" + txtMessage.Text + "','" + ckNewsDesc.Text + "'");

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
    private void FillData()
    {
        rptData.DataSource = cls.select_data_dt("EXEC ManageProcess @Action='GetAll', @Id=0");
        rptData.DataBind();

    }

   
 
    private void FillProcess(int id)
    {
        dt= cls.select_data_dt("EXEC ManageProcess @Action='GetAll', @Id="+id+"");
        txtMessage.Text = dt.Rows[0]["Name"].ToString();
        ckNewsDesc.Text= dt.Rows[0]["Description"].ToString();
    }


    private void Active(int id)
    {
        int val = cls.update_data("ManageProcess @Action='IsActive',@ID=" + id + "");
        if (val > 0)
        {
            FillData();
        }

    }

    private void Clear()
    {
        dllRole.SelectedValue = "0";
        txtMessage.Text = "";
        ckNewsDesc.Text = "";

    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            hdnid.Value = e.CommandArgument.ToString();
            FillProcess(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "Active")
        {
            Active(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }
    }
}