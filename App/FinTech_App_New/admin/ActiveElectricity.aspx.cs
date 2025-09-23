using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ActiveElectricity : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt;
    Page page;

    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        dt = ObjConnection.select_data_dt("proc_manage_ACTIVE_ELECTRICITY_Service '" + txtSearch.Text + "'");
        if (dt.Rows.Count > 0)
        {
            rptData.DataSource = dt;
            rptData.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnAssignService_Click(object sender, EventArgs e)
    {
        DataTable dtMemberMaster = ObjConnection.select_data_dt("select * from Member where loginid='" + txtSearch.Text + "'");
        if (dtMemberMaster.Rows.Count > 0)
        {
            int val = ObjConnection.update_data("PROC_ACTIVE_ELECTRICITY_Service " + dtMemberMaster.Rows[0]["MSRNO"] + "");
            if (val > 0)
            {
                ErrorShow.AlertMessage(page, "Your Service Activated", "Success");

            }
        }
    }
}