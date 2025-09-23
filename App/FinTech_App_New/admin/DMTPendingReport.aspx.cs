using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;

public partial class Admin_DMTPendingReport : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtMember = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                dtMember = (DataTable)Session["dtEmployee"];
                fillTransactionDetails();
            }           
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void fillTransactionDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Exec [ProcDMTPaytm_ManageDMT] 'GetDMTHistoryByMsrNo', 0,'','','','Pending'");
        string strFilter = "MsrNo<>0 ";
        string fromDate = "";
        string toDate = "";
        if (txtfromdate.Text != "" && txttodate.Text != "")
        {
            fromDate = changedatetommddyy(txtfromdate.Text);
            toDate = changedatetommddyy(txttodate.Text);
            strFilter = strFilter + "and AddDate>='" + fromDate + "' and AddDate<='" + toDate + "'";
        }
        if (txtMemberID.Text != "")
        {
            strFilter = strFilter + "and MemberID='" + txtMemberID.Text.Trim() + "'";
        }
        if (txtMemberName.Text != "")
        {
            strFilter = strFilter + "and (FirstName Like '%" + txtMemberName.Text.Trim() + "%' OR LastName Like '%" + txtMemberName.Text.Trim() + "%')";
        }
        if (txtBeneName.Text != "")
        {
            strFilter = strFilter + "and BeneficiaryName like '%" + txtBeneName.Text.Trim() + "%'";
        }
        if (txtBeneAccount.Text != "")
        {
            strFilter = strFilter + "and BeneficiaryAccountNo='" + txtBeneAccount.Text.Trim() + "'";
        }

        dt.DefaultView.RowFilter = strFilter;
        DataTable dtNew = new DataTable();
        dtNew = dt.DefaultView.ToTable();
        gvTransactionHistory.DataSource = dtNew;
        gvTransactionHistory.DataBind();
    }

    public string changedatetommddyy(string ddmmyy)
    {
        string mmddyy = "";
        mmddyy = ddmmyy.Substring(6, 4) + ddmmyy.Substring(3, 2) + ddmmyy.Substring(0, 2);
        return mmddyy;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }
}