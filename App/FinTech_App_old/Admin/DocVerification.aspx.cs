using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_DocVerification : System.Web.UI.Page
{
    DataAccess objaccess = new DataAccess();
    DataTable dt = new DataTable();
    Page page;

    //protected void Page_Preint(object sender, EventArgs e)
    //{
    //    page = HttpContext.Current.CurrentHandler as Page;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            if (Request.QueryString["ApplicationID"] != null)
            {
                Session["Applicationid"] = Request.QueryString["ApplicationID"].ToString();
                getmemberdetails();
                getdocument();
            }
        }
    }
    public void getdocument()
    {
        try
        {
            DataTable dtdoc = new DataTable();
            dtdoc = objaccess.GetDataTable("EXEC DBO.SP_LOAN_GETDATA 'GETDOC','',0,0,''," + Session["ApplicationID"] + "");
            if (dtdoc.Rows.Count > 0)
            {
                rptdata.DataSource = dtdoc;
                rptdata.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }
    public void getmemberdetails()
    {
        try
        {
            dt = objaccess.GetDataTable("Exec DBO.SP_LOAN_GETDATA 'MEMBERDETAILS','',0,0,''," + Session["Applicationid"] + "");
            if (dt.Rows.Count > 0)
            {
                lblmemberid.Text = dt.Rows[0]["MemberID"].ToString();
                lbladdress.Text = dt.Rows[0]["PERMANENT_ADDRESS"].ToString();
                lblapplicationid.Text = dt.Rows[0]["ApplicationID"].ToString();
                lbldob.Text = dt.Rows[0]["DATE_OF_BIRTH"].ToString();
                lblemailid.Text = dt.Rows[0]["EmailID"].ToString();
                lblloanamt.Text = dt.Rows[0]["LoanAmt"].ToString();
                lblloanapplydate.Text = dt.Rows[0]["Loan_apply_date"].ToString();
                lblloancategory.Text = dt.Rows[0]["LoanCatName"].ToString();
                lblloanpurpose.Text = dt.Rows[0]["LoanPurpose"].ToString();
                lblloansubcategory.Text = dt.Rows[0]["LoanSubCatnAme"].ToString();
                lblmembername.Text = dt.Rows[0]["Name"].ToString();
                lblmobileno.Text = dt.Rows[0]["Mobile_no"].ToString();
            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void btnapproveall_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtdate.Text))
            {
                // Call stored procedure with parameters
                int val = objaccess.ExecuteQuery(
                    $"EXEC DBO.SP_LOAN_GETDATA 'DOCAPPROVEL','',0,0,'',{Session["Applicationid"]},'','','{txtdate.Text}'"
                );

                if (val > 0)
                {
                    // Show alert with redirect
                    ErrorShow.AlertMessageWithRedirect(Page, "Approved Successfully", "LoanApproveM.aspx", "Approval Status");
                }
                else
                {
                    ErrorShow.AlertMessage(Page, "No records found to approve.", "Approval Status");
                }
            }
            else
            {
                ErrorShow.AlertMessage(Page, "Please select a date.", "Validation Error");
            }
        }
        catch (Exception ex)
        {
            // You can log the error here if needed
            ErrorShow.AlertMessage(Page, "An unexpected error occurred. Please try again.", "Error");
        }
    }

}