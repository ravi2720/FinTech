using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reatiler_Default : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Page page;
    ActionButtonPermission actionButtonPermission;
    Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            company = Company.GetCompanyInfo();
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.FundRequestPage, dtMember.Rows[0]["RoleID"].ToString());
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", "1");
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                FillData();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    protected void FillData()
    {
        dt = cls.select_data_dt("EXEC ManageFundRequest 'FUND_REQUEST_REPORT','','','"+txtfromdate.Text+"','"+txttodate.Text+"'," + company.MemberID + "");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();
        }
        else
        {
            repeater1.DataSource = null;
            repeater1.DataBind();

        }
    }


    protected void repeater1_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {
        HiddenField hdnID = (HiddenField)e.Item.FindControl("hdnID");

        DataTable dtHis = cls.select_data_dt("select * from FUNDREQUEST where ID=" + hdnID.Value + " and IsApprove=0 and IsDelete=0");
        if (e.CommandName == "IsApprove")
        {
            if (dtHis.Rows.Count > 0)
            {
                if (actionButtonPermission.ForceSuccess)
                {
                    cls.update_data("EXEC PROC_FUND_REQUEST_ACTION 'FUND_REQUEST_APPROVE'," + hdnID.Value);
                    ErrorShow.AlertMessage(page, "Fund Request Approved Successfully", ConstantsData.CompanyName);
                    Activity.AddActivity(dtMember.Rows[0]["ID"].ToString(), ConstantsData.SFundAdd.ToString(), dtHis.Rows[0]["TransactionID"].ToString(), $"{ dtHis.Rows[0]["Amount"].ToString() } Amount Fund Request Approved By { dtMember.Rows[0]["Name"].ToString()}", dtHis.Rows[0]["Msrno"].ToString(), "Fund Request Approved", cls);
                    FillData();
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Not Permission For Approve Fund Request", ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Already Updated", ConstantsData.CompanyName);
            }
        }


        else if (e.CommandName == "IsDelete")
        {
            if (dtHis.Rows.Count > 0)
            {
                if (actionButtonPermission.ForceSuccess)
                {
                    cls.update_data("EXEC PROC_FUND_REQUEST_ACTION 'FUND_REQUEST_DELETE'," + hdnID.Value);
                    ErrorShow.AlertMessage(page, "Fund Request Rejected Successfully", ConstantsData.CompanyName);
                    Activity.AddActivity(dtMember.Rows[0]["ID"].ToString(), ConstantsData.SFundAdd.ToString(), dtHis.Rows[0]["TransactionID"].ToString(), $"{ dtHis.Rows[0]["Amount"].ToString() } Amount Fund Request Rejected By { dtMember.Rows[0]["Name"].ToString()}", dtHis.Rows[0]["Msrno"].ToString(), "Fund Request Rejected", cls);
                    FillData();
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Not Permission For Reject Fund Request", ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Already Updated", ConstantsData.CompanyName);
            }
        }
        else if (e.CommandName == "ViewBankDetails")
        {
            dt = cls.select_data_dt("ManageCompanyBankDetails 'GetAll','" + e.CommandArgument.ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                rptBankData.DataSource = dt;
                rptBankData.DataBind();
            }
        }


    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }
}