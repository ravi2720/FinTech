using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EmployeeList : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dtEmployee = new DataTable();
    int Val = 0;
    Page page;

    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            if (!IsPostBack)
            {
                FillMember();
            }
            else
            {
                Response.Redirect(SessionManager.RedirectUrlA);
            }
        }
    }
    private void FillMember()
    {
        DataTable dtMember;
        if (txtSearch.Text.Trim() != "")
        {
            dtMember = ObjConnection.select_data_dt("select * from VIEW_EMPLOYEEDETAILS where mobile='" + txtSearch.Text + "'");
        }
        else
        {
            dtMember = ObjConnection.select_data_dt("select * from VIEW_EMPLOYEEDETAILS");

        }
        rptData.DataSource = dtMember;
        rptData.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
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
        Val = ObjConnection.update_data("ManageEMPLOYEE 'IsActive'," + ID + "");
        if (Val > 0)
        {
            ErrorShow.AlertMessage(page, "EMPLOYEE Active/Deactive successfully",ConstantsData.CompanyName);
            FillMember();
        }
    }


    protected void btnSubmit_Click1(object sender, EventArgs e)
    {
        FillMember();
    }
}