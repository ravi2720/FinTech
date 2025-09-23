using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_LoginSecurityForEmp : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;

            if (!IsPostBack)
            {
                FillMember();
                BindData();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }
    private void FillMember()
    {
        DataTable dtMember = ObjConnection.select_data_dt("select (LoginID + ' ( ' + Name + ' ) ')as Name,ID from Employee where isactive=1");
        Common.BindDropDown(dllEMP, dtMember, "Name", "ID");
        dllEMP.Items.Insert(0, new ListItem("-- Select Employee --", "0"));
    }

    private void BindData()
    {
        DataTable dt = ObjConnection.select_data_dt("ManageLoginSecurityForEMP " + dllEMP.SelectedValue + "");
        if (dt.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();
        }
        else
        {
            gvTransactionHistory.DataSource = null;
            gvTransactionHistory.DataBind();
        }
    }
    protected void dllEMP_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Remove")
        {
            Int32 Val = ObjConnection.update_data("Delete from LoginSecurityForEMP where ID=" + e.CommandArgument.ToString() + "");
            if (Val > 0)
            {
                BindData();
                ErrorShow.AlertMessage(page, "Remove Successfully",ConstantsData.CompanyName);
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Int32 Val = ObjConnection.update_data("AddEditLoginSecurityForEMP '" + txtIP.Text.Trim() + "'," + dllEMP.SelectedValue + "");
        if (Val > 0)
        {
            BindData();
            ErrorShow.AlertMessage(page, "Add Successfully", ConstantsData.CompanyName);
        }

    }
}