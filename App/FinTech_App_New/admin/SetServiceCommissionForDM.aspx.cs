using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SetServiceCommissionForDM : System.Web.UI.Page
{
    cls_connection objConnection = new cls_connection();
    Page page;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            FillRole();
            FillService();
            BindData();
        }
    }

    public void BindData()
    {
        dt = objConnection.select_data_dt("Manage_ServiceCommission");
        if (dt.Rows.Count > 0)
        {
            rptData.DataSource = dt;
            rptData.DataBind();
        }
    }

    private void FillRole()
    {
        DataTable dtTitle = objConnection.select_data_dt("select * from Role where ID in(3,4,5)");
        Common.BindDropDown(dllRole, dtTitle, "Name", "ID");
    }
    private void FillService()
    {
        DataTable dtTitle = objConnection.select_data_dt("select * from Service where IsActive=1 and url<>'' and Name<>''");
        Common.BindDropDown(dllService, dtTitle, "Name", "ID");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Page page = HttpContext.Current.CurrentHandler as Page;

        if (!string.IsNullOrEmpty(txtPrice.Text))
        {
            Int32 Val = 0;
            Val = objConnection.update_data("AddEdit_ServiceCommission " + dllRole.SelectedValue + "," + dllService.SelectedValue + ",'" + txtPrice.Text + "','" + chkData.Checked + "'");
            if (Val > 0)
            {
                BindData();
                ErrorShow.AlertMessage(page, "Submit Successfully", "Success");
            }
        }
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            Int32 Val = 0;
            Val = objConnection.update_data("delete from ServiceCommission where ID=" + e.CommandArgument.ToString());
            if (Val > 0)
            {
                BindData();
                ErrorShow.AlertMessage(page, "Delete Successfully", "Success");
            }
        }
    }

}