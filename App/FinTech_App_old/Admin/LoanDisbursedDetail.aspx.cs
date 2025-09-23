using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_LoanDisbursedDetail : System.Web.UI.Page
{
    DataAccess objaccess = new DataAccess();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getdata();
        }
    }
    private void getdata()
    {
        try
        {
            dt = objaccess.GetDataTable("Exec DBO.SP_LOAN_DISBURSED_DETAIL");
            if (dt.Rows.Count > 0)
            {
                lblrecords.Text = "<b>" + dt.Rows.Count.ToString() + "</b>";
                rptdata.DataSource = dt;
                rptdata.DataBind();
                lblgivenprincipal.Text = dt.Compute("SUM(LOANAMT)", "").ToString();
                lblinterestgenerate.Text = dt.Compute("SUM(INTEREST_AMOUNT)", "").ToString();
                lblreceivedintr.Text = dt.Compute("SUM(RECEIVEDINTEREST)", "").ToString();
                lblreceivedprinc.Text = dt.Compute("SUM(RECEIVEDPRINCIPAL)", "").ToString();

            }
            else
            {
                rptdata.DataSource = null;
                rptdata.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void rptdata_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "View")
        {
            Response.Redirect("View.aspx?ID=" + e.CommandArgument.ToString() + "");
        }
    }
}