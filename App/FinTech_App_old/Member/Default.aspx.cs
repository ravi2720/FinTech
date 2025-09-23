using Google.Authenticator;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_Default : System.Web.UI.Page
{
    public Company company;
    cls_connection cls = new cls_connection();
    DataTable dt;
    public DataTable dtCustomer;
    HttpBrowserCapabilities objBrwInfo;
    string AuthenticationCode = "";
    Page page;

    public MemberPermission permission;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        var date = DateTime.Now;
        objBrwInfo = Request.Browser;
        company = Company.GetCompanyInfo();
        if (!IsPostBack)
        {

            BindDashBoardBanner();
            News();
            TermsCondition();
            BindNews();

        }

    }

    private void BindNews()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select * from news where IsActive=1");
        if (dt.Rows.Count > 0)
        {
            RptData.DataSource = RptData.DataSource = dt;
            RptData.DataBind(); RptData.DataBind();
        }
        else
        {
            RptData.DataSource = null;
            RptData.DataBind();
        }

    }

    public void TermsCondition()
    {
        dt = cls.select_data_dt("EXEC ManageTermsCondition 'Get','" + 0 + "' ,'" + company.MemberID + "'");
        if (dt.Rows.Count > 0)
        {
            rptDataTermsCondition.DataSource = dt;
            rptDataTermsCondition.DataBind();
        }
    }
    private string GetLocationJson()
    {
        return "";
    }


    private string getipAddress()
    {

        string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipAddress;
    }

    public static string getOperatinSystemDetails(string browserDetails)
    {
        try
        {
            switch (browserDetails.Substring(browserDetails.LastIndexOf("Windows NT") + 11, 3).Trim())
            {
                case "6.2":
                    return "Windows 8";
                case "6.1":
                    return "Windows 7";
                case "6.0":
                    return "Windows Vista";
                case "5.2":
                    return "Windows XP 64-Bit Edition";
                case "5.1":
                    return "Windows XP";
                case "5.0":
                    return "Windows 2000";
                default:
                    return browserDetails.Substring(browserDetails.LastIndexOf("Windows NT"), 14);
            }
        }
        catch
        {
            if (browserDetails.Length > 149)
                return browserDetails.Substring(0, 149);
            else
                return browserDetails;
        }
    }


    private void BindDashBoardBanner()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("EXEC ManageBannerImage 'GetAllBYBannerType','2','" + company.MemberID + "'");
        if (dt.Rows.Count > 0)
        {
            rptDashBoardBanner.DataSource = rptDashBoardBanner.DataSource = dt;
            rptDashBoardBanner.DataBind(); rptDashBoardBanner.DataBind();
        }
        else
        {
            rptDashBoardBanner.DataSource = null;
            rptDashBoardBanner.DataBind();
        }

    }

    protected void News()
    {
        NewsData.DataSource = cls.select_data_dt("EXEC ManageNews 'GetActive',0,1");
        NewsData.DataBind();
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {

        if (DateTime.Now.Hour >= 0 /*&& DateTime.Now.Hour < 23*/)
        {
            Login product = new Login
            {
                LoginID = email.Value,
                Password = passwordfield.Value,
                Latitude = hdnLatitude.Value,
                Longitude = hdnLongitude.Value
            };

            ValidationContext context = new ValidationContext(product, null, null);
            var results = new System.Collections.Generic.List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(product, context, results, true);
            if (isValid)
            {
                if (chkterms.Checked)
                {
                    var item = product.Validate(product.LoginID, product.Password, cls);
                    if (item.Item1)
                    {
                        if (string.IsNullOrEmpty(hdnLatitude.Value) || string.IsNullOrEmpty(hdnLongitude.Value))
                        {
                            lblMessage.Text = "Allow your location";
                            return;
                        }
                        if (!string.IsNullOrEmpty(email.Value) && !string.IsNullOrEmpty(passwordfield.Value))
                        {
                            DataTable dt = new DataTable();
                            dt = cls.select_data_dt("EXEC PROC_LOGIN 3,'" + email.Value + "','" + passwordfield.Value + "','" + getipAddress() + "',1,'" + getOperatinSystemDetails(Request.UserAgent) + "','" + objBrwInfo.Browser + "','" + company.MemberID + "','" + hdnLatitude.Value + "','" + hdnLongitude.Value + "'");
                            if (dt.Rows.Count > 0)
                            {
                                if (Convert.ToInt32(dt.Rows[0]["msrno"]) > 0)
                                {
                                    // cls.update_data("update member Set Latitude='" + hdnLatitude.Value + "',Longitude='" + hdnLongitude.Value + "' where msrno='" + dt.Rows[0]["msrno"].ToString() + "'");
                                    if (1 == 1)
                                    {
                                        bool check = false;
                                        permission = MemberPermission.GetPermission(dt.Rows[0]["msrno"].ToString());
                                        if (permission != null)
                                        {
                                            if (permission.TwoWay)
                                            {
                                                string LastLoginIP = cls.select_data_scalar_string("select  macaddress IP from Member where LoginID='" + email.Value + "' or mobile='" + email.Value + "' order by ID desc");
                                                if (permission.IsTpin)
                                                {
                                                    if (LastLoginIP != Session.SessionID)
                                                    {
                                                        ViewState["Password"] = passwordfield.Value;
                                                        lblName.Text = dt.Rows[0]["Name"].ToString();
                                                        multi.ActiveViewIndex = 1;
                                                    }
                                                    else
                                                    {
                                                        check = true;
                                                    }
                                                }
                                                else if (permission.IsOTP)
                                                {
                                                    if (LastLoginIP != Session.SessionID)
                                                    {
                                                        Random generator = new Random();
                                                        int r = generator.Next(1000, 9999);
                                                        string OTP = r.ToString();
                                                        string Message = "Login OTP is : " + OTP;
                                                        Session["OTP"] = OTP;
                                                        ViewState["Password"] = passwordfield.Value;

                                                        string[] ValueArray = new string[2];
                                                        //ValueArray[0] = "admin";
                                                        ValueArray[0] = dt.Rows[0]["Name"].ToString();
                                                        txtTpin.MaxLength = 4;
                                                        ValueArray[1] = OTP;
                                                        //string str = SMS.SendWithV(dt.Rows[0]["Mobile"].ToString(), ConstantsData.Admin_Login_OTP_SMS, ValueArray, company.MemberID);
                                                        Emails.SendEmail(dt.Rows[0]["Name"].ToString(), dt.Rows[0]["Email"].ToString(), ValueArray, "otp_login.htm");
                                                        //ErrorShow.AlertMessage(page, "OTP Send On Your Register Number", ConstantsData.CompanyName);
                                                        ErrorShow.AlertMessage(page, "OTP Send On Your Register Email", ConstantsData.CompanyName);
                                                        txtotp.Attributes.Add("placeholder", "Please Enter OTP");
                                                        lblName.Text = dt.Rows[0]["Name"].ToString();
                                                        multi.ActiveViewIndex = 1;
                                                    }
                                                    else
                                                    {
                                                        check = true;
                                                    }
                                                }
                                                else if (permission.IsGoogleAuth)
                                                {
                                                    if (LastLoginIP != Session.SessionID)
                                                    {
                                                        txtotp.MaxLength = 6;
                                                        ViewState["Password"] = passwordfield.Value;

                                                        ViewState["AuthenticationCode"] = permission.AuthKey;
                                                        lblName.Text = dt.Rows[0]["Name"].ToString();
                                                        multi.ActiveViewIndex = 1;
                                                        txtotp.Attributes.Add("placeholder", "Please Enter Your google Authenticator Code");
                                                    }
                                                    else
                                                    {
                                                        check = true;
                                                    }
                                                }
                                                else
                                                {
                                                    DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno = " + Convert.ToInt32(dt.Rows[0]["msrno"]) + "");
                                                    Session.Add("dtMember", dtMember);
                                                    Session["MemberMsrNo"] = dtMember.Rows[0]["msrno"].ToString();
                                                    cls.update_data("update member set loginOnOff=1 where (loginid='" + email.Value + "' or mobile='" + email.Value + "') and password=HASHBYTES('SHA2_512', '" + passwordfield.Value + "'+CAST(Salt AS NVARCHAR(36)))");
                                                    Session["MemberPassword"] = passwordfield.Value;
                                                    Session["dtCompany"] = cls.select_data_dt("select * from company where id=1");
                                                    Response.Redirect("Dashboard.aspx");
                                                }

                                                if (check == true)
                                                {
                                                    DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno = " + Convert.ToInt32(dt.Rows[0]["msrno"]) + "");
                                                    Session.Add("dtMember", dtMember);
                                                    Session["MemberMsrNo"] = dtMember.Rows[0]["msrno"].ToString();
                                                    cls.update_data("update member set loginOnOff=1 where (loginid='" + email.Value + "' or mobile='" + email.Value + "') and password=HASHBYTES('SHA2_512', '" + passwordfield.Value + "'+CAST(Salt AS NVARCHAR(36)))");
                                                    Session["MemberPassword"] = passwordfield.Value;
                                                    Session["dtCompany"] = cls.select_data_dt("select * from company where id=1");
                                                    Response.Redirect("Dashboard.aspx");
                                                }
                                                else
                                                {
                                                    string[] ValueArray = new string[7];
                                                    ValueArray[0] = dt.Rows[0]["Name"].ToString();
                                                    ValueArray[1] = dt.Rows[0]["Mobile"].ToString();
                                                    ValueArray[2] = System.DateTime.Now.ToShortDateString();
                                                    ValueArray[3] = getOperatinSystemDetails(Request.UserAgent);
                                                    ValueArray[4] = getipAddress();
                                                    ValueArray[5] = getipAddress();
                                                    ValueArray[6] = "link";
                                                    string str = SMS.SendWithV(dt.Rows[0]["Mobile"].ToString(), ConstantsData.LoginOtherDevice, ValueArray, company.MemberID);
                                                }


                                            }
                                            else
                                            {
                                                DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno = " + Convert.ToInt32(dt.Rows[0]["msrno"]) + "");
                                                Session.Add("dtMember", dtMember);
                                                Session["MemberMsrNo"] = dtMember.Rows[0]["msrno"].ToString();
                                                cls.update_data("update member set loginOnOff=1 where (loginid='" + email.Value + "' or mobile='" + email.Value + "') and password=HASHBYTES('SHA2_512', '" + passwordfield.Value + "'+CAST(Salt AS NVARCHAR(36)))");
                                                Session["MemberPassword"] = passwordfield.Value;
                                                Session["dtCompany"] = cls.select_data_dt("select * from company where id=1");
                                                Response.Redirect("Dashboard.aspx");
                                            }
                                        }
                                        else
                                        {
                                            DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno = " + Convert.ToInt32(dt.Rows[0]["msrno"]) + "");
                                            Session.Add("dtMember", dtMember);
                                            Session["MemberMsrNo"] = dtMember.Rows[0]["msrno"].ToString();
                                            cls.update_data("update member set loginOnOff=1 where (loginid='" + email.Value + "' or mobile='" + email.Value + "') and password=HASHBYTES('SHA2_512', '" + passwordfield.Value + "'+CAST(Salt AS NVARCHAR(36)))");
                                            Session["MemberPassword"] = passwordfield.Value;
                                            Session["dtCompany"] = cls.select_data_dt("select * from company where id=1");
                                            Response.Redirect("Dashboard.aspx");
                                        }
                                    }
                                    else
                                    {
                                        ErrorShow.AlertMessage(page, "You Are not Valid Person For This Account", ConstantsData.CompanyName);
                                    }
                                }
                                else
                                {
                                    lblMessage.Text = "Invalid Username or Password ..!!";
                                    return;
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Please Try Again ..!!";
                                //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Please Try Again ..!!');", true);
                                return;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Enter Username and Password ..!!";
                            //ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script>alertify.error('Enter Username and Password ..!!');</script>", false);
                            return;
                        }
                    }
                    else
                    {
                        ErrorShow.AlertMessage(page, $"Validation Error: {item.Item2}", ConstantsData.CompanyName);
                    }
                }
                else
                {
                    lblMessage.Text = "Please Accept the Terms & Conditions First..!!";
                    return;
                }
            }
            else
            {
                foreach (var result in results)
                {
                    ErrorShow.AlertMessage(page, $"Validation Error: {result.ErrorMessage}", ConstantsData.CompanyName);
                }
            }
        }

    }

    protected void btnttpin_Click(object sender, EventArgs e)
    {
        permission = MemberPermission.GetPermission(cls.select_data_scalar_string("select msrno from Member where loginid='" + email.Value + "' or Mobile='" + email.Value.Trim() + "'"));
        if (permission != null)
        {
            if (permission.TwoWay)
            {
                if (permission.IsTpin)
                {
                    DataTable dttpin = cls.select_data_dt("select * from Member where loginid='" + email.Value + "' or Mobile='" + email.Value.Trim() + "'");
                    if (txtotp.Text == dttpin.Rows[0]["LoginPin"].ToString())
                    {
                        DataTable dt = new DataTable();
                        dt = cls.select_data_dt("EXEC PROC_LOGIN 3,'" + email.Value + "','" + ViewState["Password"].ToString() + "','" + getipAddress() + "',1,'','','" + company.MemberID + "'");
                        if (Convert.ToInt32(dt.Rows[0]["msrno"]) > 0)
                        {
                            cls.update_data("update Member set macaddress='" + Session.SessionID + "' where loginid='" + email.Value + "' or Mobile='" + email.Value.Trim() + "'");
                            DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno = " + Convert.ToInt32(dt.Rows[0]["msrno"]) + "");
                            Session.Add("dtMember", dtMember);
                            Session["MemberMsrNo"] = dtMember.Rows[0]["msrno"].ToString();
                            cls.update_data("update member set loginOnOff=1 where (loginid='" + email.Value + "' or Mobile='" + email.Value.Trim() + "') and password=HASHBYTES('SHA2_512', '" + passwordfield.Value + "'+CAST(Salt AS NVARCHAR(36)))");
                            Session["MemberPassword"] = passwordfield.Value;
                            Response.Redirect("DashBoard.aspx");
                        }

                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter right Login Pin');Op();", true);
                    }
                }
                else if (permission.IsOTP)
                {

                    if (txtotp.Text == Session["OTP"].ToString())
                    {
                        DataTable dt = new DataTable();
                        dt = cls.select_data_dt("EXEC PROC_LOGIN 3,'" + email.Value + "','" + ViewState["Password"].ToString() + "','" + getipAddress() + "',1,'','','" + company.MemberID + "'");
                        if (Convert.ToInt32(dt.Rows[0]["msrno"]) > 0)
                        {
                            cls.update_data("update Member set macaddress='" + Session.SessionID + "' where loginid='" + email.Value + "' or Mobile='" + email.Value.Trim() + "'");
                            DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno = " + Convert.ToInt32(dt.Rows[0]["msrno"]) + "");
                            Session.Add("dtMember", dtMember);
                            Session["MemberMsrNo"] = dtMember.Rows[0]["msrno"].ToString();
                            cls.update_data("update member set loginOnOff=1 where (loginid='" + email.Value + "' or Mobile='" + email.Value.Trim() + "') and password=HASHBYTES('SHA2_512', '" + passwordfield.Value + "'+CAST(Salt AS NVARCHAR(36)))");
                            Session["MemberPassword"] = passwordfield.Value;
                            Response.Redirect("Dashboard.aspx");
                        }
                    }
                    else
                    {
                        ErrorShow.AlertMessage(page, "OTP Not Match", ConstantsData.CompanyName);
                    }
                }
                else if (permission.IsGoogleAuth)
                {
                    String pin = txtotp.Text;
                    Boolean status = ValidateTwoFactorPIN(pin);
                    if (status)
                    {
                        DataTable dt = new DataTable();
                        dt = cls.select_data_dt("EXEC PROC_LOGIN 3,'" + email.Value + "','" + ViewState["Password"].ToString() + "','" + getipAddress() + "',1,'','','" + company.MemberID + "'");
                        if (Convert.ToInt32(dt.Rows[0]["msrno"]) > 0)
                        {
                            cls.update_data("update Member set macaddress='" + Session.SessionID + "' where loginid='" + email.Value + "' or Mobile='" + email.Value.Trim() + "'");
                            DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno = " + Convert.ToInt32(dt.Rows[0]["msrno"]) + "");
                            Session.Add("dtMember", dtMember);
                            Session["MemberMsrNo"] = dtMember.Rows[0]["msrno"].ToString();
                            cls.update_data("update member set loginOnOff=1 where (loginid='" + email.Value + "' or Mobile='" + email.Value.Trim() + "') and password=HASHBYTES('SHA2_512', '" + passwordfield.Value + "'+CAST(Salt AS NVARCHAR(36)))");
                            Session["MemberPassword"] = passwordfield.Value;
                            Response.Redirect("Dashboard.aspx");
                        }
                    }
                    else
                    {
                        ErrorShow.AlertMessage(page, "Auth Code Invalid", ConstantsData.CompanyName);
                    }
                }
            }
        }
    }

    public Boolean ValidateTwoFactorPIN(String pin)
    {
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        return tfa.ValidateTwoFactorPIN(ViewState["AuthenticationCode"].ToString(), pin);
    }

    protected void btnForgetPassword_Click(object sender, EventArgs e)
    {
        if (btnForgetPassword.Text == "Verify")
        {
            DataTable dt = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where (LoginID='" + txtMobile.Text + "')  and isactive=1 and EMPID='" + company.MemberID + "'");
            if (dt.Rows.Count > 0)
            {
                Random generator = new Random();
                int r = generator.Next(100000, 999999);
                string OTP = r.ToString();
                string Message = "Login OTP is : " + OTP;
                Session["OTP"] = OTP;

                string[] ValueArray = new string[2];
                ValueArray[0] = dt.Rows[0]["Name"].ToString();
                ValueArray[1] = OTP;
                //string str = SMS.SendWithV(dt.Rows[0]["Mobile"].ToString(), ConstantsData.Forgot_Password_SMS, ValueArray, company.MemberID);
                Emails.SendEmail(dt.Rows[0]["Name"].ToString(), dt.Rows[0]["Email"].ToString(), ValueArray, "forgot_password.htm");
                btnForgetPassword.Text = "Set Password";
                txtNewPassword.Visible = true;
                txtOTPVerify.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('OTP Send On " + dt.Rows[0]["Email"].ToString().Substring(0, 2) + "xxxxxx Email.');", true);
            }
            else
            {
                txtNewPassword.Visible = false;
                txtOTPVerify.Visible = false;
                btnForgetPassword.Text = "Verify";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + txtMobile.Text + " UserID not register');", true);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(Session["OTP"].ToString()) && !string.IsNullOrEmpty(txtOTPVerify.Text))
            {
                if (txtOTPVerify.Text == Session["OTP"].ToString())
                {
                    //if (IsStrongPassword(txtNewPassword.Text))
                    //{
                    if (txtNewPassword.Text.Trim().Length <= 15)
                    {
                        DataTable dt = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where (LoginID='" + txtMobile.Text + "')  and isactive=1 and EMPID='" + company.MemberID + "'");
                        if (dt.Rows.Count > 0)
                        {
                            if (!string.IsNullOrEmpty(txtNewPassword.Text))
                            {
                                Int32 Val = cls.update_data("PROC_MANAGE_PASSWROD '" + dt.Rows[0]["msrno"].ToString() + "','CHANGEPWD','" + txtNewPassword.Text + "'");
                                if (Val > 0)
                                {
                                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Password Changed Successfully');window.location='default.aspx';", true);
                                }
                            }
                            else
                            {
                                txtNewPassword.BorderColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            ErrorShow.AlertMessage(page, "Member Not Fount", ConstantsData.CompanyName);
                        }
                    }
                    else
                    {
                        txtNewPassword.Text = "";
                        ErrorShow.AlertMessage(page, "Max Password length 15.", ConstantsData.CompanyName);
                    }
                    //}
                    //else
                    //{
                    //    txtNewPassword.Text = "";
                    //    ErrorShow.AlertMessage(page, "Password Should be Strong", ConstantsData.CompanyName);
                    //}
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('OTP WRONG');", true);
                }
            }
            else
            {
                txtOTPVerify.BorderColor = System.Drawing.Color.Red;
            }
        }
    }

    static bool IsStrongPassword(string password)
    {
        // Use a regular expression to define the criteria for a strong password
        string pattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";

        // Match the password against the pattern
        Match match = Regex.Match(password, pattern);

        // Return true if the password matches the pattern
        return match.Success;
    }
}