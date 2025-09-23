using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ContactHereReport : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();

    DataTable dt = new DataTable();
    int Val = 0;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Text = txtToDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");                   
        }
    }

 

    private void FillData()
    {
        try
        {
            dt = ObjData.select_data_dt("ManageContactHere 'admin','" + txtFromDate.Text+"','"+txtToDate.Text+ "',0");
            rptDataRecharge.DataSource = dt;
            rptDataRecharge.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillData();
    }

    protected void rptDataRecharge_ItemCommand(object source, RepeaterCommandEventArgs e)
    {        
        if (e.CommandName == "Active")
        {
            Active(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }
    }
    private void FillNews(int id)
    {
        dt = ObjData.select_data_dt("EXEC ManageContactHere 'Get','" + id + "' ");        
    }


    private void Active(int id)
    {
        int val = ObjData.update_data("ManageContactHere @Action='IsActive',@ID=" + id + "");
        if (val > 0)
        {
            FillData();
        }

    }
}