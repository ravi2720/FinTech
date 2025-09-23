using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class services : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    public DataTable dtMember = new DataTable();
    public Company company;


    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();

    }
}