using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Member_VirtualSurcharge : System.Web.UI.Page
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
                FillData();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }


    private void FillData()
    {
        try
        {
            dt = objconnection.select_data_dt("EXEC ManageVirtualSurcharge 'GetByPackageID','" + 0 + "' ,'" + dtMember.Rows[0]["PackageID"] + "'");
            rptSurcharge.DataSource = dt;
            rptSurcharge.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

}