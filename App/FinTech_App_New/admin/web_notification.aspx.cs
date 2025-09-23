using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_web_notification : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        FillNews();
    }

    protected void FillNews()
    {
        dt = ObjConnection.select_data_dt("EXEC ManageNews 'Get','" + 0 + "' ");
        if (dt.Rows.Count > 0)
        {
            repData.DataSource = dt;
            repData.DataBind();
        }
        else
        {
            repData.DataSource = null;
            repData.DataBind();

        }

    }
}