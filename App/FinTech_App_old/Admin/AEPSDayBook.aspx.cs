using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AEPSDayBook : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                txtFromDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                // fillTransactionDetails();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void fillTransactionDetails()
    {
        dtExport = cls.select_data_dt("AEPSUserDaybook 'OpWise','" + txtFromDate.Text + "'");
        if (dtExport.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dtExport;
            gvTransactionHistory.DataBind();
        }
        else
        {
            gvTransactionHistory.DataSource = null;
            gvTransactionHistory.DataBind();
        }

    }




    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }

}