using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Member_RechargeCommission : System.Web.UI.Page
{
    cls_connection objconnection = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            if (!IsPostBack)
            {
                dtMember = (DataTable)Session["dtMember"];
                BindDataCommission();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }


    private void BindDataCommission()
    {
        try
        {
            dt = objconnection.select_data_dt("Exec ManageRechargeCommission '" + dtMember.Rows[0]["PackageID"].ToString() + "',0,'admin',0");
            rptData.DataSource = dt;
            rptData.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

}