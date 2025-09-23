using Newtonsoft.Json.Linq;
using System;
using System.Data;

public partial class Member_InstantKYC : System.Web.UI.Page
{
    DataTable dtMember;
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            lblName.Text = dtMember.Rows[0]["Name"].ToString();
            lblPan.Text = dtMember.Rows[0]["Pan"].ToString();
            lblEmail.Text = dtMember.Rows[0]["Email"].ToString();
            if (!IsPostBack)
            {
                fillTransactionDetails();
            }
        }
        else
        {
            Response.Write("Default.aspx");
        }
    }
    private void fillTransactionDetails()
    {
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
        if (btnProccess.Text == "Process")
        {
            SignupInitiate signupInitiate = new SignupInitiate();
            signupInitiate.mobile = dtMember.Rows[0]["Mobile"].ToString();
            signupInitiate.pan = dtMember.Rows[0]["Pan"].ToString();
            signupInitiate.email = dtMember.Rows[0]["email"].ToString();

            //Encrypt Aadhar
            signupInitiate.aadhaar = NWSPay.GetEncryptAadhar(dtMember.Rows[0]["Aadhar"].ToString());

            string iPAddress = cls_myMember.GetIPAddress();
            Location location = new Location();
            location = location.GetLocationDataCurrent(iPAddress);

            signupInitiate.latitude = location.Latitude;
            signupInitiate.longitude = location.Longitude;
            signupInitiate.consent = "Y";
            signupInitiate.bankAccountNo = txtAccountNumber.Text.Trim();
            signupInitiate.bankIfsc = txtIFSC.Text.Trim();

            string result = NWSPay.OutletKYCInitiate(signupInitiate.GetJson());
            JObject Data = JObject.Parse(result);
            if (Data["statusCode"]?.ToString() == "TXN")
            {
                mul.ActiveViewIndex = 1;

                var dataObj = Data["data"] as JObject;

                if (dataObj != null)
                {
                    hdnop.Value = dataObj["otpReferenceId"]?.ToString();
                    hdnHash.Value = dataObj["hash"]?.ToString();
                }

                ErrorShow.AlertMessage(Page, Data["status"]?.ToString(), ConstantsData.CompanyName);
            }
            else
            {
                mul.ActiveViewIndex = 0;
                ErrorShow.AlertMessage(Page, Data["status"]?.ToString(), ConstantsData.CompanyName);
            }

        }
        else
        {

        }
    }

    protected void btnpross_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtOTP.Text))
        {
            SignupOTPVerify signupOTPVerify = new SignupOTPVerify
            {
                otpReferenceID = hdnop.Value,
                otp = txtOTP.Text,
                hash = hdnHash.Value
            };

            string result = NWSPay.OutletKYCInitiateValidate(signupOTPVerify.GetJson());
            JObject Data = JObject.Parse(result);

            if (Data["statusCode"]?.ToString() == "TXN")
            {
                var dataObj = Data["data"] as JObject;

                if (dataObj != null)
                {
                    string outletId = dataObj["outletId"]?.ToString() ?? "";
                    string name = dataObj["name"]?.ToString() ?? "";
                    string dob = dataObj["dateOfBirth"]?.ToString() ?? "";
                    string gender = dataObj["gender"]?.ToString() ?? "";
                    string pincode = dataObj["pincode"]?.ToString() ?? "";
                    string state = dataObj["state"]?.ToString() ?? "";
                    string district = dataObj["districtName"]?.ToString() ?? "";
                    string address = dataObj["address"]?.ToString() ?? "";
                    string profilePic = dataObj["profilePic"]?.ToString() ?? "";

                    cls.update_data($"AddEditAEPSKYC {dtMember.Rows[0]["Msrno"]},'{outletId}','{name}','{dob}','{gender}','{pincode}','{state}','{district}','{address}','{profilePic}','{result}'");
                }

                ErrorShow.AlertMessage(Page, Data["status"]?.ToString(), ConstantsData.CompanyName);
            }
            else
            {
                ErrorShow.AlertMessage(Page, Data["status"]?.ToString(), ConstantsData.CompanyName);
            }
        }
        else
        {
            ErrorShow.AlertMessage(Page, "Enter OTP", ConstantsData.CompanyName);
        }
    }
}