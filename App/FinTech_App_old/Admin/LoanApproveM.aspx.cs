using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_LoanApproveM : System.Web.UI.Page
{
    DataAccess obj = new DataAccess();
    DataTable dtMember;
    Page page;
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        dtMember = Session["dtMember"] as DataTable;
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                Filldata();
                string dtt = DateTime.Now.ToShortDateString();
            }
        }
    }
    public void Filldata()
    {
        try
        {
            dt = obj.GetDataTable("exec DBO.SP_LOAN_GETDATA 'DTAPPROVE','',0,0,'',0,'','','',1,1");
            if (dt.Rows.Count > 0)
            {
                rptdata.DataSource = dt;
                rptdata.DataBind();
                lblrecords.Text = "</br><b>" + dt.Rows.Count.ToString() + "</b>";
            }
        }
        catch (Exception ex)
        {

        }
    }
    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        if (chkall.Checked == true)
        {
            foreach (RepeaterItem rptitem in rptdata.Items)
            {
                CheckBox chkrpt = rptitem.FindControl("chkrpt") as CheckBox;
                chkrpt.Checked = true;
            }
        }
        else
        {
            foreach (RepeaterItem rptitem in rptdata.Items)
            {
                CheckBox chkrpt = rptitem.FindControl("chkrpt") as CheckBox;
                chkrpt.Checked = false;
            }
        }
    }

    protected void rptdata_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int result = 0;
        LinkButton btnemichart = (LinkButton)e.Item.FindControl("btnemichart");
        LinkButton btndoc = (LinkButton)e.Item.FindControl("btndoc");
        Button btnapprove = (Button)e.Item.FindControl("btnapprove");
        CheckBox chkrpt = (CheckBox)e.Item.FindControl("chkrpt");
        Label lbldocstatus = (Label)e.Item.FindControl("lbldocstatus");

        if (btnemichart.Text == "View" && e.CommandName.ToString() == "EMI")
        {
            Response.Redirect("dtemichart.aspx?ApplicationID=" + e.CommandArgument.ToString());
        }
        else if (btndoc.Text == "View Doc" && e.CommandName.ToString() == "Doc")
        {
            Response.Redirect("docverification.aspx?ApplicationID=" + e.CommandArgument.ToString());
        }
        else if (btnapprove.Text == "Approve" && e.CommandName.ToString() == "Approve")
        {
            if (chkrpt.Checked)
            {
                if (lbldocstatus.Text == "APPROVED")
                {
                    result = obj.ExecuteQuery("EXEC DBO.SP_LOAN_GETDATA 'DTLOANAPPROVE','',0,0,''," + e.CommandArgument.ToString());
                    if (result > 0)
                    {
                        ErrorShow.AlertMessage(Page, "Loan Approved successfully..!", "Success");
                    }
                }
                else
                {
                    ErrorShow.AlertMessage(Page, "Documents Not Approved..!", "Warning");
                }
            }
            else
            {
                ErrorShow.AlertMessage(Page, "Kindly Check To Approve..!", "Info");
            }
        }
    }

}