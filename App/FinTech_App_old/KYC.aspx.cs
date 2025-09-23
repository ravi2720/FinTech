using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class KYC : System.Web.UI.Page
{
    DataTable dtMember;
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["LoginID"] != null)
            {
                dtMember = cls.select_data_dt("select * from Member where Loginid='" + Request.QueryString["LoginID"].ToString() + "'");
                lblName.Text = dtMember.Rows[0]["Name"].ToString();
                lblPan.Text = dtMember.Rows[0]["Pan"].ToString();
                lblEmail.Text = dtMember.Rows[0]["Email"].ToString();
                if (!IsPostBack)
                {
                    fillTransactionDetails();
                }
            }
        }

    }
    private void fillTransactionDetails()
    {
        dtMember = cls.select_data_dt("select * from Member where Loginid='" + Request.QueryString["LoginID"].ToString() + "'");
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("ManageAEPSKYC ''," + dtMember.Rows[0]["ID"].ToString() + ",'','','0','" + dtMember.Rows[0]["Msrno"].ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            mul.ActiveViewIndex = 2;
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();
        }
        else
        {
            gvTransactionHistory.DataSource = null;
            gvTransactionHistory.DataBind();
        }

    }
    protected void btnProccess_Click(object sender, EventArgs e)
    {
        dtMember = cls.select_data_dt("select * from Member where Loginid='" + Request.QueryString["LoginID"].ToString() + "'");
        if (btnProccess.Text == "Process")
        {
            SignupInitiate signupInitiate = new SignupInitiate();
            signupInitiate.mobile = dtMember.Rows[0]["Mobile"].ToString();
            signupInitiate.pan = dtMember.Rows[0]["Pan"].ToString();
            signupInitiate.email = dtMember.Rows[0]["email"].ToString();
            string data = NWSPay.GetEncryptAadhar(dtMember.Rows[0]["Aadhar"].ToString()); ;
            signupInitiate.aadhaar = data;// "dMu7JPln5FqPkJ3W3XfRHj3BUbmyIi88wphuOHNKmss=";// dtMember.Rows[0]["Aadhar"].ToString();
            Location location = new Location();
            location = location.GetLocationData();
            signupInitiate.latitude = location.Latitude;
            signupInitiate.longitude = location.Longitude;
            signupInitiate.consent = "Y";
            string result = InstantPayout.PostCall(signupInitiate.GetJson(), InstantPayout.Url + "initiate", "");
            JObject Data = JObject.Parse(result);
            if (Data["statuscode"].ToString() == "TXN")
            {
                mul.ActiveViewIndex = 1;
                hdnop.Value = Data["data"]["otpReferenceID"].ToString();
                hdnHash.Value = Data["data"]["hash"].ToString();
                ErrorShow.AlertMessage(Page, Data["status"].ToString(), ConstantsData.CompanyName);
            }
            else
            {
                mul.ActiveViewIndex = 0;

                ErrorShow.AlertMessage(Page, Data["status"].ToString(), ConstantsData.CompanyName);
            }
        }
        else
        {

        }
    }

    protected void btnpross_Click(object sender, EventArgs e)
    {
        dtMember = cls.select_data_dt("select * from Member where Loginid='" + Request.QueryString["LoginID"].ToString() + "'");
        if (!string.IsNullOrEmpty(txtOTP.Text))
        {
            SignupOTPVerify signupOTPVerify = new SignupOTPVerify();
            signupOTPVerify.otpReferenceID = hdnop.Value;
            signupOTPVerify.otp = txtOTP.Text;
            signupOTPVerify.hash = hdnHash.Value;
            string result = InstantPayout.PostCall(signupOTPVerify.GetJson(), InstantPayout.Url + "validate", "");
            JObject Data = JObject.Parse(result);
            if (Data["statuscode"].ToString() == "TXN")
            {
                cls.update_data("AddEditAEPSKYC " + dtMember.Rows[0]["Msrno"].ToString() + ",'" + Data["data"]["outletId"].ToString() + "','" + Data["data"]["name"].ToString() + "','" + Data["data"]["dateOfBirth"].ToString() + "','" + Data["data"]["gender"].ToString() + "','" + Data["data"]["pincode"].ToString() + "','" + Data["data"]["state"].ToString() + "','" + Data["data"]["districtName"].ToString() + "','" + Data["data"]["address"].ToString() + "','" + Data["data"]["profilePic"].ToString() + "','" + result + "'");
                ErrorShow.AlertMessage(Page, Data["status"].ToString(), ConstantsData.CompanyName);
            }
            else
            {
                ErrorShow.AlertMessage(Page, Data["status"].ToString(), ConstantsData.CompanyName);
            }
        }
        else
        {
            ErrorShow.AlertMessage(Page, "Enter OTP", ConstantsData.CompanyName);
        }
    }
}