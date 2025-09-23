using System;
using Google.Authenticator;
using System.Data;
using System.Net;
using System.Web;
using System.Web.UI;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;

public partial class Admin_Default : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public Company company;
    Page page;
    public Permission permission;
    string AuthenticationCode = "";
    protected void Page_Init(object sender, EventArgs e)
    {
        permission = Permission.GetPermission();
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        company = Company.GetCompanyInfo();
        if (!IsPostBack)
        {
        }
    }

    public string GetIPAddress()
    {
        IPHostEntry Host = default(IPHostEntry);
        string Hostname = null, IPAddress = "";
        Hostname = System.Environment.MachineName;
        Host = Dns.GetHostEntry(Hostname);
        foreach (IPAddress IP in Host.AddressList)
        {
            if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                IPAddress = Convert.ToString(IP);
            }
        }
        return IPAddress;
    }
    private string getipAddress()
    {

        string IpAddress = string.Empty;
        try
        {
            IpAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (IpAddress == " " || IpAddress == null)
            {
                IpAddress = Request.ServerVariables["REMOTE_ADDR"];
                return IpAddress;
            }
            else
            {
                return IpAddress;
            }
        }
        catch (ArgumentNullException Ex)
        {
            return IpAddress;
        }
    }


    protected void btnLogin_Click(object sender, EventArgs e)
    {

        if (string.IsNullOrEmpty(hdnLatitude.Value) || string.IsNullOrEmpty(hdnLongitude.Value))
        {
            ErrorShow.AlertMessage(page, "Allow your location", ConstantsData.CompanyName);
            return;
        }
        if (!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(passwordfield.Value))
        {
            DataTable dt = new DataTable();
            dt = cls.select_data_dt("EXEC PROC_LOGIN_EMP 'after',1,'" + username.Text.Trim() + "','" + passwordfield.Value + "','" + getipAddress() + "',0");
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["ID"]) > 0)
                {
                    cls.update_data("update Employee Set Latitude='" + hdnLatitude.Value + "',Longitude='" + hdnLongitude.Value + "' where id='" + dt.Rows[0]["ID"].ToString() + "'");
                    {
                        if (1==1)
                        {
                            string LastLoginIP = cls.select_data_scalar_string("select top 1 IP from AdminLoginHistory where loginid='" + username.Text + "' order by ID desc");
                            string CurrentIP = getipAddress();
                            Int32 CountData = cls.select_data_scalar_int("select count(*) from LoginSecurityForEMP where IP='" + CurrentIP + "' and EMPID=" + dt.Rows[0]["ID"].ToString() + "");
                            //   if (dt.Rows[0]["RoleID"].ToString() == "2")
                            {
                                CountData = 1;
                            }
                            if (CountData > 0)
                            {
                                if (LastLoginIP != CurrentIP)
                                {
                                    if (permission != null)
                                    {
                                        if (permission.TwoWay)
                                        {
                                            if (permission.OTP)
                                            {
                                                Random generator = new Random();
                                                int r = generator.Next(100000, 999999);
                                                string OTP = r.ToString();
                                                string Message = "Login OTP is : " + OTP;
                                                Session["OTP"] = OTP;
                                                ViewState["Password"] = passwordfield.Value;
                                                string[] ValueArray = new string[4];
                                                ValueArray[0] = "admin";
                                                txtTpin.MaxLength = 6;
                                                ValueArray[1] = OTP;
                                                string str = SMS.SendWithV(dt.Rows[0]["Mobile"].ToString(), ConstantsData.Admin_Login_OTP_SMS, ValueArray, company.MemberID);
                                                txtTpin.Attributes.Add("placeholder", "Please Enter OTP");
                                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "Op();", true);
                                            }
                                            else if (permission.TPIN)
                                            {
                                                ViewState["Password"] = passwordfield.Value;
                                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "Op();", true);
                                            }
                                            else if (permission.GoogleAuth)
                                            {
                                                txtTpin.MaxLength = 6;
                                                ViewState["Password"] = passwordfield.Value; 

                                                ViewState["AuthenticationCode"] = dt.Rows[0]["SecretKey"].ToString();

                                                txtTpin.Attributes.Add("placeholder", "Please Enter Your google Authenticator Code");
                                                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "Op();", true);
                                            }
                                        }
                                        else
                                        {
                                            DataTable dtEmployee = cls.select_data_dt("select * from View_EmployeeDetails where ID = " + Convert.ToInt32(dt.Rows[0]["ID"]) + "");
                                            Session.Add("dtEmployee", dtEmployee);
                                            if (dtEmployee.Rows[0]["RoleID"].ToString() == "1")
                                                Response.Redirect("Dashboard.aspx");
                                            else
                                                Response.Redirect("EmployeeDashboard.aspx");

                                        }
                                    }
                                    else
                                    {
                                        DataTable dtEmployee = cls.select_data_dt("select * from View_EmployeeDetails where ID = " + Convert.ToInt32(dt.Rows[0]["ID"]) + "");
                                        Session.Add("dtEmployee", dtEmployee);
                                        if (dtEmployee.Rows[0]["RoleID"].ToString() == "1")
                                            Response.Redirect("Dashboard.aspx");
                                        else
                                            Response.Redirect("EmployeeDashboard.aspx");
                                    }
                                }
                                else
                                {
                                    DataTable dtEmployee = cls.select_data_dt("select * from View_EmployeeDetails where ID = " + Convert.ToInt32(dt.Rows[0]["ID"]) + "");
                                    Session.Add("dtEmployee", dtEmployee);
                                    if (dtEmployee.Rows[0]["RoleID"].ToString() == "1")
                                        Response.Redirect("Dashboard.aspx");
                                    else
                                        Response.Redirect("EmployeeDashboard.aspx");
                                }
                            }
                            else
                            {
                                ErrorShow.AlertMessage(page, "Contact to admin. You have permission to login inside office", ConstantsData.CompanyName);
                            }
                        }
                        else
                        {
                            ErrorShow.AlertMessage(page, "You Are not Valid Person For This Account", ConstantsData.CompanyName);
                        }
                    }
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Invalid Username or Password ..!!", ConstantsData.CompanyName);
                    return;
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Please Try Again ..!!", ConstantsData.CompanyName);
                return;
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Enter Username and Password ..!", ConstantsData.CompanyName);
            return;
        }

    }

    public string Getlog()
    {
        var client = new RestClient("https://api.bigdatacloud.net/data/reverse-geocode-client?latitude=" + hdnLatitude.Value + "&longitude=" + hdnLongitude.Value + "&localityLanguage=en");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json ");
        request.AddParameter("application/json ", "", ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        JObject dd = JObject.Parse(response.Content);
        return dd["principalSubdivision"].ToString();
    }


    protected void btnForgetPassword_Click(object sender, EventArgs e)
    {
        if (btnForgetPassword.Text == "Verify")
        {
            DataTable dt = cls.select_data_dt("select * from View_EmployeeDetails where Mobile='" + txtMobile.Text + "'");
            if (dt.Rows.Count > 0)
            {
                Random generator = new Random();
                int r = generator.Next(100000, 999999);
                string OTP = r.ToString();
                string Message = "Login OTP is : " + OTP;
                Session["OTP"] = OTP;
                string[] ValueArray = new string[4];
                ValueArray[0] = "admin";
                ValueArray[1] = OTP;
                string str = SMS.SendWithV(dt.Rows[0]["Mobile"].ToString(), ConstantsData.Forgot_Password_SMS, ValueArray, company.MemberID);
                btnForgetPassword.Text = "Set Password";
                txtNewPassword.Visible = true;
                txtOTPVerify.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('OTP Send On " + txtMobile.Text + " Mobile Number.');", true);
            }
            else
            {
                txtNewPassword.Visible = false;
                txtOTPVerify.Visible = false;
                btnForgetPassword.Text = "Verify";

                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + txtMobile.Text + " Mobile Number not register');", true);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(Session["OTP"].ToString()) && !string.IsNullOrEmpty(txtOTPVerify.Text))
            {
                if (txtOTPVerify.Text == Session["OTP"].ToString())
                {
                    if (IsStrongPassword(txtNewPassword.Text))
                    {
                        if (txtNewPassword.Text.Trim().Length <= 15)
                        {
                            if (!string.IsNullOrEmpty(txtNewPassword.Text))
                            {

                                DataTable dt = cls.select_data_dt("select * from View_EmployeeDetails where (mobile='" + txtMobile.Text + "')  and isactive=1 ");
                                if (dt.Rows.Count > 0)
                                {
                                    if (!string.IsNullOrEmpty(txtNewPassword.Text))
                                    {
                                        Int32 Val = cls.update_data("PROC_MANAGE_AdminPassword '" + dt.Rows[0]["ID"].ToString() + "','CHANGEPWD','" + txtNewPassword.Text + "'");
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
                                    ErrorShow.AlertMessage(page, "Admin ID Not Found", ConstantsData.CompanyName);
                                }

                            }
                            else
                            {
                                txtNewPassword.BorderColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                        {
                            txtNewPassword.Text = "";
                            ErrorShow.AlertMessage(page, "Max Password length 15.", ConstantsData.CompanyName);
                        }
                    }
                    else
                    {
                        txtNewPassword.Text = "";
                        ErrorShow.AlertMessage(page, "Password Should be Strong", ConstantsData.CompanyName);
                    }

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




    protected void btnOTPSubmit_Click(object sender, EventArgs e)
    {
        //if (txtTpin.Text == Session["OTP"].ToString())
        //{
        //    // dt = cls.select_data_dt("EXEC PROC_LOGIN 1,'" + username.Text.Trim() + "','" + password.Text.Trim() + "','" + getipAddress() + "',0");
        //    DataTable dt = new DataTable();
        //    dt = cls.select_data_dt("EXEC PROC_LOGIN 1,'" + username.Text.Trim() + "','" + ViewState["Password"].ToString() + "','" + getipAddress() + "',1");
        //    if (Convert.ToInt32(dt.Rows[0]["msrno"]) > 0)
        //    {

        //        DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno = " + Convert.ToInt32(dt.Rows[0]["msrno"]) + "");
        //        Session.Add("dtMember", dtMember);
        //        Session["MemberMsrNo"] = dtMember.Rows[0]["msrno"].ToString();
        //        cls.update_data("update member set loginOnOff=1 where loginid='" + username.Text.Trim() + "' and password='" + password.Text.Trim() + "'");
        //        Session["MemberPassword"] = password.Text.Trim();
        //        Session["dtCompany"] = cls.select_data_dt("select * from company where id=1");
        //        Response.Redirect("Dashboard.aspx");
        //    }
        //}
        //else
        //{
        //    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter right OTP');Op();", true);
        //}
    }
    public Boolean ValidateTwoFactorPIN(String pin)
    {
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        return tfa.ValidateTwoFactorPIN(ViewState["AuthenticationCode"].ToString(), pin);
    }
    protected void btnttpin_Click(object sender, EventArgs e)
    {
        if (permission.OTP)
        {
            if (txtTpin.Text == Session["OTP"].ToString())
            {
                DataTable dt = cls.select_data_dt("EXEC PROC_LOGIN_EMP 'after', 1,'" + username.Text.Trim() + "','" + ViewState["Password"].ToString().Trim() + "','" + getipAddress() + "',0");
                if (dt.Rows.Count > 0)
                {
                    DataTable dtEmployee = cls.select_data_dt("select * from View_EmployeeDetails where ID = " + Convert.ToInt32(dt.Rows[0]["ID"]) + "");
                    Session.Add("dtEmployee", dtEmployee);
                    if (dtEmployee.Rows[0]["RoleID"].ToString() == "1")
                        Response.Redirect("Dashboard.aspx");
                    else
                        Response.Redirect("EmployeeDashboard.aspx");
                }
            }
        }
        else if (permission.GoogleAuth)
        {
            String pin = txtTpin.Text;
            Boolean status = ValidateTwoFactorPIN(pin);
            if (status)
            {
                DataTable dt = cls.select_data_dt("EXEC PROC_LOGIN_EMP 'after', 1,'" + username.Text.Trim() + "','" + ViewState["Password"].ToString().Trim() + "','" + getipAddress() + "',0");
                if (dt.Rows.Count > 0)
                {
                    DataTable dtEmployee = cls.select_data_dt("select * from View_EmployeeDetails where ID = " + Convert.ToInt32(dt.Rows[0]["ID"]) + "");
                    Session.Add("dtEmployee", dtEmployee);
                    if (dtEmployee.Rows[0]["RoleID"].ToString() == "1")
                        Response.Redirect("Dashboard.aspx");
                    else
                        Response.Redirect("EmployeeDashboard.aspx");
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Auth Code Invalid", ConstantsData.CompanyName);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(username.Text.Trim()))
            {
                string tpin = cls.select_data_scalar_string("select TransactionPassword from Employee where loginid='" + username.Text.Trim() + "'");
                if (txtTpin.Text == tpin)
                {
                    DataTable dt = cls.select_data_dt("EXEC PROC_LOGIN_EMP 'after', 1,'" + username.Text.Trim() + "','" + ViewState["Password"].ToString().Trim() + "','" + getipAddress() + "',0");
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtEmployee = cls.select_data_dt("select * from View_EmployeeDetails where ID = " + Convert.ToInt32(dt.Rows[0]["ID"]) + "");
                        Session.Add("dtEmployee", dtEmployee);
                        if (dtEmployee.Rows[0]["RoleID"].ToString() == "1")
                            Response.Redirect("Dashboard.aspx");
                        else
                            Response.Redirect("EmployeeDashboard.aspx");
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Enter right Login Pin');Op();", true);
                }
            }
        }
    }


    protected void btnProcess_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(hdnLatitude.Value) || string.IsNullOrEmpty(hdnLongitude.Value))
        {
            ErrorShow.AlertMessage(page, "Allow your location", ConstantsData.CompanyName);
            return;
        }

        DataTable dt = new DataTable();
        dt = cls.select_data_dt("EXEC PROC_LOGIN_EMP 'FirstLook',1,'" + username.Text.Trim() + "','" + passwordfield.Value + "','" + getipAddress() + "',0");
        if (dt.Rows.Count > 0)
        {

            if (!Convert.ToBoolean(dt.Rows[0]["FirstTime"].ToString()))
            {
                Random generator = new Random();
                int r = generator.Next(100000, 999999);
                string OTP = r.ToString();
                string Message = "Login OTP is : " + OTP;
                Session["OTPVV"] = OTP;
                string[] ValueArray = new string[4];
                ValueArray[0] = "admin";
                txtTpin.MaxLength = 6;
                ValueArray[1] = OTP;
                string str = SMS.SendWithV(dt.Rows[0]["Mobile"].ToString(), ConstantsData.Admin_Login_OTP_SMS, ValueArray, company.MemberID);
                txtvOTP.Attributes.Add("placeholder", "Please Enter OTP");
                ErrorShow.AlertMessage(page, "OTP Send On Your Register MobileNo", ConstantsData.CompanyName);
                mul.ActiveViewIndex = 1;
            }
            else
            {
                mul.ActiveViewIndex = 2;
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Enter Username and Password ..!", ConstantsData.CompanyName);
            return;
        }
    }

    protected void btnOtpVerify_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("EXEC PROC_LOGIN_EMP 'FirstLook',1,'" + username.Text.Trim() + "','" + passwordfield.Value + "','" + getipAddress() + "',0");
        if (dt.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(txtvOTP.Text) && !string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(txtSetPass.Text))
            {
                Regex validateGuidRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
                if (validateGuidRegex.IsMatch(txtSetPass.Text))
                {
                    if (Session["OTPVV"].ToString() == txtvOTP.Text)
                    {
                        cls.update_data("update Employee Set FirstTime=1,password=HASHBYTES('SHA2_512', '" + txtSetPass.Text + "'+CAST(salt AS NVARCHAR(36))) where loginid='" + username.Text.Trim() + "'");
                        Session["OTPVV"] = null;
                        mul.ActiveViewIndex = 2;
                    }
                    else
                    {
                        ErrorShow.AlertMessage(page, "Enter Valid OTP ..!", ConstantsData.CompanyName);
                        return;
                    }
                }
                else
                {
                    txtSetPass.Text = "";
                    ErrorShow.AlertMessage(page, "Password Not Strong ..!", ConstantsData.CompanyName);
                    return;
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Enter Valid Details ..!", ConstantsData.CompanyName);
                return;
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "User Not Found!", ConstantsData.CompanyName);
            return;
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