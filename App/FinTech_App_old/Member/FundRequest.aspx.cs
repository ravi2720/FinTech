using System;
using System.Data;
using System.Web;
using System.Web.UI;

public partial class Member_Default : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    public Company company;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        if (Session["dtMember"] != null)
        {
            company = Company.GetCompanyInfo();
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                if (dtMember.Rows.Count > 0)
                {
                    ddlPaymentMode.DataSource = ConstantsData.PaymentMode;
                    ddlPaymentMode.DataBind();
                    BindDropDown.FillDropDown(dllBankList, "CompanyList", "BankDetails", "ID", "Select Bank", company.MemberID);
                    FillData(dtMember.Rows[0]["LoginID"].ToString());
                    //  FillBalance(Convert.ToInt16(dtMember.Rows[0]["msrno"]));
					BankList();

                }
                else
                {
                    Response.Redirect("logout.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (dllBankList.SelectedIndex > 0 && txtamount.Text.ToInt32()>0)
        {
            dt = cls.select_data_dt("EXEC AddEditFundRequest '" + dtMember.Rows[0]["Msrno"] + "','" + txtamount.Text + "','" + txtReferanceNo.Text.Trim() + "','" + txtPaymentDate.Text.Trim() + "','" + ddlPaymentMode.SelectedItem.Text + "','" + txtRemark.Text.Trim() + "',"+company.MemberID+","+dllBankList.SelectedValue+"");
            if (dt.Rows.Count > 0)
            {
                lblMessage.Text = "*" + dt.Rows[0]["status"].ToString() + "*";
                lblMessage.Focus();
                FillData(dtMember.Rows[0]["LoginID"].ToString());
                FillBalance(dtMember.Rows[0]["msrno"].ToString().ToInt32());
                string[] ValueArray = new string[5];
                ValueArray[1] = txtamount.Text;
                ValueArray[0] = dtMember.Rows[0]["Name"].ToString();
                ValueArray[2] = dtMember.Rows[0]["Name"].ToString();
                ValueArray[3] = txtReferanceNo.Text;
                ValueArray[4] = dllBankList.SelectedItem.Text;
                string str = SMS.SendWithV("8258888988,9366385112", ConstantsData.FundRequest, ValueArray, company.MemberID);
                ConstantsData.Clear(page.Controls);
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Fill All Valid Details", ConstantsData.CompanyName);
        }
    }
    public void FillBalance(int msrno)
    {
        DataTable dtBalance = new DataTable();
        dtBalance = cls.select_data_dt("select Balance from View_MemberBalance where msrno = " + msrno + "");
        lblBalance.Text = dtBalance.Rows[0]["Balance"].ToString();
    }
    protected void clear()
    {
        //ddlWallet.SelectedIndex = 0;
        txtRemark.Text = txtamount.Text = txtReferanceNo.Text = txtPaymentDate.Text = string.Empty;
        ddlPaymentMode.SelectedIndex = 0;
    }
    protected void FillData(string memberid)
    {
        dt = cls.select_data_dt("EXEC ManageFundRequest 'FUND_REQUEST_REPORT','" + memberid + "' ");
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
	
	protected void BankList()
    {
        DataTable dt = cls.select_data_dt("select * from CompanyBankDetails where isactive=1");
        rptBankDetails.DataSource = dt;
        rptBankDetails.DataBind();
    }
}