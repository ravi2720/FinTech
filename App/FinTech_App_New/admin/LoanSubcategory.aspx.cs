using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_LoanSubcategory : System.Web.UI.Page
{
    DataAccess obj = new DataAccess();
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
            obj.Bindloan(ddlloancategory);
            FillData();
        }
    }
    public void FillData()
    {
        dt = obj.GetDataTable("EXEC SP_LOAN_SUBCATEGORY 'SELECT'");
        if (dt.Rows.Count > 0)
        {
            rptdata.DataSource = dt;
            rptdata.DataBind();
        }
        else
        {
            rptdata.DataSource = null;
            rptdata.DataBind();
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int a = obj.ExecuteQuery("exec DBO.SP_LOAN_SUBCATEGORY 'INSERT',0," + ddlloancategory.SelectedValue + ",'" + txtloanscheme.Text + "','" + ddlinteresttype.SelectedItem.Text + "','" + txtintrrate.Text.Trim() + "','" + txtfileprocessing.Text + "','" + txtotherservice.Text + "','" + txtlatepayment.Text + "','" + txtbounce.Text + "','" + RadioButtonList1.SelectedValue + "','" + RadioButtonList2.SelectedValue + "','" + RadioButtonList3.SelectedValue + "','" + RadioButtonList4.SelectedValue + "'");
            if (a > 0)
            {
                Page currentPage = HttpContext.Current.CurrentHandler as Page;
                ErrorShow.AlertMessage(Page, "Record Inserted Successfully..!", "Success");

                FillData();
            }
            else
            {
                Response.Write("<script>alert('Something went Wrong..!')</script>");
            }
        }
        catch (Exception ex)
        {
            // Optionally log ex.Message here
        }
    }


    protected void rptdata_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            Int32 Val = 0;
            Val = obj.ExecuteIntScalar("delete from loan_Sub_Category where loansubcatid=" + e.CommandArgument.ToString() + "");

            ErrorShow.AlertMessage(Page, "Record Deleted Successfully..!", "Success");
            FillData();

        }
    }
}