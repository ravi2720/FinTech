using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MonthlyReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
   
    [WebMethod]
    public static string GetData(string MethodName, string Data)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("MonthlyReport  '" + (Data == "" ? "September, 2022" : Data) + "'");        
        return JsonConvert.SerializeObject(dt);
    }
}