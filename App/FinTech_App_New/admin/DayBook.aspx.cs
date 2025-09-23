using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_DayBook : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtEmployee"] != null)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                fillTransactionDetails();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void fillTransactionDetails()
    {
        if (Request.QueryString["Type"] != null)
        {
            dtExport = cls.select_data_dt("ManageDayBook '"+ Request.QueryString["Type"] + "','" + txtFromDate.Text + "'");
            if (dtExport.Rows.Count > 0)
            {
                gvTransactionHistory.DataSource = dtExport;
                gvTransactionHistory.DataBind();
                tSuccessSum.InnerText = dtExport.Compute("sum(SuccessSum)", "Hit>0").ToString();
                tcommission.InnerText = dtExport.Compute("sum(DirectCommission)", "Hit>0").ToString();
            }
            else
            {
                gvTransactionHistory.DataSource = null;
                gvTransactionHistory.DataBind();
            }
        }

    }




    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }

}