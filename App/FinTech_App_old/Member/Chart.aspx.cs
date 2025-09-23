using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_Chart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string FindChartData(string Method,string Month, string Date)
    {
        DataTable dt = new DataTable();
        DataTable dtmember = (DataTable)HttpContext.Current.Session["dtMember"];
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("ChartData '" + Method + "',"+ dtmember.Rows[0]["Msrno"].ToString() + ",'"+ Month + "','"+ Date + "'");
        return ConstantsData.SerializeToJSon(dt);
    }
}