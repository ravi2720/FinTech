using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_InstantKYCList : System.Web.UI.Page
{
    DataTable dtMember;
    cls_connection cls = new cls_connection();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", dtMember.Rows[0]["ID"].ToString());
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void fillTransactionDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("ManageAEPSKYC '',1,'" + txtfromdate.Text + "','" + txttodate.Text + "','0','" + ddlMember.SelectedValue + "'");

        if (dt.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();


        }
        else
        {
            gvTransactionHistory.DataSource = null;
            gvTransactionHistory.DataBind();
        }

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }



    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Send OTP")
        {
            if (!string.IsNullOrEmpty(txtNewMobile.Text) && !string.IsNullOrEmpty(txtoldMobile.Text))
            {
                InstantMobileChange instantMobileChange = new InstantMobileChange();
                instantMobileChange.existingMobileNumber = txtoldMobile.Text;
                instantMobileChange.newMobileNumber = txtNewMobile.Text;
                string result = InstantPayout.PostCall(instantMobileChange.GetJson(), InstantPayout.NumberChange + "mobileUpdate", "");
                JObject Data = JObject.Parse(result);
                if (Data["statuscode"].ToString() == "TXN")
                {
                    btnsubmit.Text = "Verify Mobile";
                    otpdiv.Visible = true;
                    ErrorShow.AlertMessage(Page, Data["data"]["existing"].ToString(), ConstantsData.CompanyName);
                    ErrorShow.AlertMessage(Page, Data["data"]["new"].ToString(), ConstantsData.CompanyName);
                }
                else
                {
                    otpdiv.Visible = false;
                    ErrorShow.AlertMessage(Page, Data["status"].ToString(), ConstantsData.CompanyName);
                }
            }
            else
            {

            }
        }
        else
        {
            InstantPayOTPRoot instantPayOTPRoot = new InstantPayOTPRoot();
            instantPayOTPRoot.newMobileNumber = txtNewMobile.Text;
            instantPayOTPRoot.existingMobileNumber = txtoldMobile.Text;
            InstantPayOtp instantPayOtp = new InstantPayOtp();
            instantPayOtp.existingMobileNumber = txtOldOTP.Text;
            instantPayOtp.newMobileNumber = txtNewOTP.Text;
            instantPayOTPRoot.otp = instantPayOtp;
            string result = InstantPayout.PostCall(instantPayOTPRoot.GetJson(), InstantPayout.NumberChange + "mobileUpdateVerify", "");
            JObject Data = JObject.Parse(result);
            if (Data["statuscode"].ToString() == "TXN")
            {
                btnsubmit.Text = "Send OTP";
                ConstantsData.Clear(page.Controls);
                ErrorShow.AlertMessage(Page, Data["status"].ToString(), ConstantsData.CompanyName);
            }
            else
            {
                ErrorShow.AlertMessage(Page, Data["status"].ToString(), ConstantsData.CompanyName);
            }

        }
    }

    protected void txtoldMobile_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtoldMobile.Text))
        {
            DataTable dtm = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where Mobile='" + txtoldMobile.Text + "'");
            if (dtm.Rows.Count > 0)
            {
                rptDataMemberDtails.DataSource = dtm;
                rptDataMemberDtails.DataBind();
            }
            else
            {
                rptDataMemberDtails.DataSource = null;
                rptDataMemberDtails.DataBind();
            }
        }
    }
}