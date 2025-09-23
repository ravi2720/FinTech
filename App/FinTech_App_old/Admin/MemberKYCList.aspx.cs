using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_BankDetails : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            company = Company.GetCompanyInfo();
            if (!IsPostBack)
            {
                FillData();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void FillData()
    {
        DataTable dtMember = cls.select_data_dt("select * from MemberKYCDocument where status='" + ddlStatus.SelectedValue + "' order by adddate desc");
        if (dtMember.Rows.Count > 0)
        {
            rptData.DataSource = dtMember;
            rptData.DataBind();
        }
        else
        {
            rptData.DataSource = null;
            rptData.DataBind();
        }
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        TextBox txtReason = (TextBox)e.Item.FindControl("txtReason");
        HiddenField hdnmsrno = (HiddenField)e.Item.FindControl("hdnmsrno");
        Label lblBankName = (Label)e.Item.FindControl("lblBankName");
        Label lblDocName = (Label)e.Item.FindControl("lblDocName");


        DataTable dtdoc = cls.select_data_dt("select * from member where msrno=" + hdnmsrno.Value + "");
        if (e.CommandName == "IsApprove")
        {
            DataTable dt = cls.select_data_dt("exec Proc_ManageMemberKYCDocument " + Convert.ToInt32(e.CommandArgument) + ",'IsApprove','" + txtReason.Text + "'");
            if (dtdoc.Rows.Count > 0)
            {
                if (1 == 1)
                {
                    string[] ValueArray = new string[2];
                    ValueArray[0] = lblDocName.Text;
                    ValueArray[1] = lblBankName.Text;

                    SMS.SendWithV(dtdoc.Rows[0]["Mobile"].ToString(), ConstantsData.KYCApprove, ValueArray, company.MemberID);
                }
            }
            ErrorShow.AlertMessage(Page, "Approved Successfully ..!!", ConstantsData.CompanyName);
            FillData();
        }
        if (e.CommandName == "IsReject")
        {
            string[] ValueArray = new string[2];
            ValueArray[0] = lblDocName.Text;
            ValueArray[1] = lblBankName.Text;

            SMS.SendWithV(dtdoc.Rows[0]["Mobile"].ToString(), ConstantsData.KYCReject, ValueArray, company.MemberID);
            DataTable dt = cls.select_data_dt("exec Proc_ManageMemberKYCDocument " + Convert.ToInt32(e.CommandArgument) + ",'IsReject','" + txtReason.Text + "'");
            ErrorShow.AlertMessage(Page, "Rejected Successfully ..!!", ConstantsData.CompanyName);
            FillData();
        }
    }


    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillData();
    }
}