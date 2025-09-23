using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CollectionUser : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetData();
        }
    }

    private void GetData()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("proc_Single_Day_Collection");
        rptData.DataSource = dt;
        rptData.DataBind();
    }

}