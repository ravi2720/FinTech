using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_PageList : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember;
    int Val = 0; Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                GetData();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }


    public void GetData()
    {
        dt = ObjConnection.select_data_dt("ManagePageList 'GetAllList',0");
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

        if (!string.IsNullOrEmpty(txtPageName.Text))
        {
            Val = ObjConnection.update_data("AddEditPagelist " + hdnRoleID.Value + ",'" + txtPageName.Text + "'");
            if (Val > 0)
            {
                ObjConnection.update_data("AddEditPermissionPage 0");
                GetData();               
                ErrorShow.AlertMessage(page, "Page Update Successfully", ConstantsData.CompanyName);
                ConstantsData.Clear(page.Controls);
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Fill All Data", ConstantsData.CompanyName);
        }
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {
            FillData(ID: e.CommandArgument.ToString());
        }

        else if (e.CommandName == "Active")
        {
            Active(ID: e.CommandArgument.ToString());
        }
    }

    private void FillData(string ID)
    {
        dt = ObjConnection.select_data_dt("ManagePageList 'GetAllList'," + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnRoleID.Value = ID;
            txtPageName.Text = dt.Rows[0]["Name"].ToString();
           
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManagePageList 'IsActive'," + ID + "");
        if (Val > 0)
        {
            GetData();
            ErrorShow.AlertMessage(page, "Page Active/DeActive Successfully", ConstantsData.CompanyName);
        }
    }

}