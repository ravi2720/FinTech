using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_EmiChart : System.Web.UI.Page
{
    DataAccess objaccess = new DataAccess();
    DataTable dt = new DataTable();
    int applicationid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ApplicationID"] != "")
            {
                Session["applicationid"] = Request.QueryString["Applicationid"];
                EMI();
            }
        }
    }
    public void EMI()
    {
        int no_installment = 0;
        int loandisbursementid = objaccess.ExecuteIntScalar("Select ISNULL(MAX(Loan_disbursement_id),0) FROM loan_disbursement");
        dt = objaccess.GetDataTable("Select * from loan_application where applicationid=" + Session["applicationid"] + "");
        string lblintresttype = objaccess.ExecuteStringScalar("select interest_type from loan_sub_category where loansubcatid=" + dt.Rows[0]["loansubcatid"].ToString() + "");
        decimal intrrate = objaccess.ExecuteIntScalar("select annual_interest_rate from loan_sub_category where loansubcatid=" + dt.Rows[0]["loansubcatid"].ToString() + "");
        if (dt.Rows[0]["INSTALLMENT_TYPE"].ToString() == "5")
        {
            no_installment = objaccess.ExecuteIntScalar("select (DATEDIFF(day,getdate(),(dateadd(month," + dt.Rows[0]["Loan_Tenure"].ToString() + ",getdate()))))/7");
        }
        else if (dt.Rows[0]["INSTALLMENT_TYPE"].ToString() == "9")
        {
            no_installment = objaccess.ExecuteIntScalar("select (DATEDIFF(day,getdate(),(dateadd(month," + dt.Rows[0]["Loan_Tenure"].ToString() + ",getdate()))))/15");
        }
        else
        {
            no_installment = Convert.ToInt32(dt.Rows[0]["Loan_Tenure"]);
        }
        DataTable dtinstallment = objaccess.GetDataTable("SELECT * from [dbo].[calcInstallment] (" + loandisbursementid + ",'" + lblintresttype.ToString() + "'," + dt.Rows[0]["Installment_type"].ToString() + "," + intrrate.ToString() + "," + no_installment + ",'" + dt.Rows[0]["LoanAmt"].ToString() + "'," + dt.Rows[0]["Loan_Tenure"].ToString() + ",'" + DateTime.Now.ToString("MM-dd-yyyy") + "')");
        rptinstallment.DataSource = dtinstallment;
        rptinstallment.DataBind();
        lblnoinstallment.Text = dtinstallment.Rows.Count.ToString();
        lblintramt.Text = dtinstallment.Compute("SUM(interestamount)", "").ToString();
        lbltotalinstallment.Text = dtinstallment.Compute("SUM(installmentamount)", "").ToString();
        lblinstamt.Text = dtinstallment.Rows[0]["installmentamount"].ToString();
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        {
            Response.Redirect("dtloanapprove.aspx");
        }
    }
}