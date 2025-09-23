using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class inner : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        CityFrom();
        CityTo();
    }
    private void CityFrom()
    {
        DataTable dt = cls.select_data_dt("select * from state where IsActive=1");
        if (dt.Rows.Count > 0)
        {
            ddlcityF.DataSource = dt;
            ddlcityF.DataTextField = "Name";
            ddlcityF.DataValueField = "ID";
            ddlcityF.DataBind();
            ddlcityF.Items.Insert(0, new ListItem("-- Select state --", "0"));
        }
    }
    private void CityTo()
    {
        DataTable dt = cls.select_data_dt("select * from state where IsActive=1");
        if (dt.Rows.Count > 0)
        {
            ddlcityT.DataSource = dt;
            ddlcityT.DataTextField = "Name";
            ddlcityT.DataValueField = "ID";
            ddlcityT.DataBind();
            ddlcityT.Items.Insert(0, new ListItem("-- Select state --", "0"));
        }
    }
}