using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default2 : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                if (Session["dtEmployee"] == null)
                {
                    Response.Redirect("default.aspx");
                }
                else
                {
                    FillData();
                }

            }
        }
    }

    protected void FillData()
    {
        DataTable dt = new DataTable();
        dt = ObjConnection.select_data_dt("Exec GetRequestStatus " + 0 + "");
        repData.DataSource = dt;
        repData.DataBind();
    }
}