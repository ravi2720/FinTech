using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ComplainTicket : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();

    DataTable dtEmployee = new DataTable();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            dtEmployee = (DataTable)SessionManager.CurrentSessionAdmin;
            page = HttpContext.Current.CurrentHandler as Page;
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

    private void FillData()
    {
        DataTable dt = ObjConnection.select_data_dt("Exec ManageComplainTicket 'GetAllCompanyMemberID'," + dtEmployee.Rows[0]["ID"].ToString() + "");
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
        HiddenField hdnID = (HiddenField)e.Item.FindControl("hdnID");
        if (e.CommandName == "IsApprove")
        {
            ObjConnection.update_data("Exec ManageComplainTicket @Action='IsApprove',@ID=" + hdnID.Value + "");
            ErrorShow.AlertMessage(page, "Complain Apporved", ConstantsData.CompanyName);
            FillData();
        }

        if (e.CommandName == "IsReject")
        {
            ObjConnection.update_data("Exec ManageComplainTicket @Action='IsReject',@ID=" + hdnID.Value + "");
            ErrorShow.AlertMessage(page, "Complain Rejected", ConstantsData.CompanyName);

            FillData();
        }

    }
}