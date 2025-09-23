using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_view : System.Web.UI.Page
{
    DataAccess obj = new DataAccess();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                ds = obj.GetDataSet("exec sp_payment_plan '" + Request.QueryString["ID"].ToString() + "',0");
                rptdata.DataSource = ds.Tables[1];
                rptdata.DataBind();

            }
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("LoanDisbursedDetail.aspx");
    }
}