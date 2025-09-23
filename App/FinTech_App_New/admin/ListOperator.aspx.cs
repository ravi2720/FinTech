using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ListOperator : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();    
    int Val = 0;
    Page page;

    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown.FillDropDown(ddlService,"ActiveService","Name","ID","Select Service");
            GetOperator();
        }
    }

    public void GetOperator()
    {
        dt = ObjConnection.select_data_dt("ManageOperator 'GetAllServiceWise'," + ddlService.SelectedValue+ "");
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

   

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
       

        if (e.CommandName == "Active")
        {
            Active(ID: e.CommandArgument.ToString());
        }
    }

   

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageService @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            GetOperator();
        }
    }



    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetOperator();
    }
}