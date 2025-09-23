using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EmployeeDashboard : System.Web.UI.Page
{
    public ServiceTotalSelf ServiceTotal;
    cls_connection Objconnection = new cls_connection();
    DataTable dtMember;
    public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        if (Session["dtEmployee"] != null)
        {
            if (!IsPostBack)
            {
                ServiceTotal = new ServiceTotalSelf();
              
            }
            dtMember = (DataTable)Session["dtEmployee"];
        
           

        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
  



}