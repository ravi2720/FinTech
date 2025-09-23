using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Member_PayoutHistory : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Page page;
    ActionButtonPermission actionButtonPermission;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];

            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", dtMember.Rows[0]["ID"].ToString());
                txtFromDate.Text = txtToDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlM);
        }
    }

    protected void FillData()
    {
        dt = cls.select_data_dt("EXEC PROC_AEPS_WITHDRAW_REPORT 'WITHDRAW_REPORT_ALL'," + dtMember.Rows[0]["MSrno"].ToString() + ",'" + ddlStatus.SelectedValue + "','" + txtFromDate.Text + "','" + txtFromDate.Text + "',0,0,'" + ddlMember.SelectedValue + "'");
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
        
        if (e.CommandName == "ViewBankDetails")
        {
            FillBankDetails(e.CommandArgument.ToString());
        }
        
    }

    private void FillBankDetails(string msrno)
    {

        dt = cls.select_data_dt("select * from View_MemberBankList where ID = " + msrno + "");
        rptBankData.DataSource = dt;
        rptBankData.DataBind();

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }
}