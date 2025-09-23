using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class register : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    Registration registration = new Registration();
    DataTable dt = new DataTable();
    Page page;
    public Company company;
    public Permission permission;
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
            hdnRoleID.Value = "0";
            Session["OTP"] = "";
            BindDropDown.FillDropDown(ddltitle, "ActiveTitle", "Name", "ID", "Select Title");
            BindDropDown.FillDropDown(rdGender, "ActiveGender", "Name", "ID", "Select Title");
            BindDropDown.FillDropDown(dllState, "ActiveState", "Name", "ID", "Select State");
            BindOutSideRole();


        }
       
    }

    private void BindOutSideRole()
    {
        dt = cls.select_data_dt("ManageRole 'OutSideReg',0,'"+company.MemberID+"'");
        if (dt.Rows.Count > 0)
        {
            rptRoleData.DataSource = dt;
            rptRoleData.DataBind();
        }
    }

   

    public int GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (permission.RegistratioWIthOTPVerification)
            {
                if (permission.OTPSendRegistrationOTP)
                {
                    if (!string.IsNullOrEmpty(txtOTP.Text.Trim()) && !string.IsNullOrEmpty(Session["OTP"].ToString()))
                    {
                        if (Session["OTP"].ToString() != txtOTP.Text)
                        {
                            return;
                        }
                    }
                    else
                    {
                        txtOTP.BorderColor = System.Drawing.Color.Red;
                        return;
                    }
                }
            }
            if (txtMobile.Text.Length == 10)
            {
                {
                    Random generator = new Random();
                    string number = GenerateRandomNo().ToString(); //generator.Next(1, 10000).ToString("D4");
                    registration.Name = txtName.Text.Trim();
                    registration.ShopName = txtShopName.Text.Trim();

                    registration.Mobile = txtMobile.Text.Trim();
                    registration.LoginPin = Convert.ToInt32(number);
                    registration.IsActive = true;
                    registration.GenderID = Convert.ToInt16(rdGender.SelectedValue);
                    registration.Email = txtEmail.Text.Trim();
                    registration.CityID = Convert.ToInt32(0);
                    registration.PinCode = txtpincode.Text.Trim();
                    registration.DOB = Convert.ToDateTime(txtDOB.Text.Trim()).ToString("MM-dd-yyyy");
                    registration.Address = txtAddress.Text.Trim();
                    registration.PackageID = cls.select_data_scalar_int("select ID from package where RoleID=" + hdnRoleID.Value + " and CompanyMemberID=" + company.MemberID + "");// Convert.ToInt32((hdnRoleID.Value.Split(',')[0] == "6" ? "6" : "7"));
                    registration.ParentID = Convert.ToInt32(company.MemberID);  // Pass sessionid
                    registration.Password = generator.Next(1, 10000).ToString("D10");
                    registration.RoleID = Convert.ToInt32(hdnRoleID.Value.Split(',')[0]);
                    registration.TitleID = Convert.ToInt32(ddltitle.SelectedValue);
                    registration.AlterNativeMobile = txtMobile.Text;
                    registration.Aadhar = txtAadhar.Text;
                    registration.Pan = txtPanNumber.Text;
                    registration.StateID = Convert.ToInt32(dllState.SelectedValue);
                    registration.CityName = txtCityName.Text.Trim();
                    ValidationContext valContext = new ValidationContext(registration, null, null);
                    var validationsResults = new List<ValidationResult>();
                    bool correct = Validator.TryValidateObject(registration, valContext, validationsResults, true);
                    if (correct)
                    {
                        if (registration.PackageID > 0)
                        {
                            if (registration.IsAnyNullOrEmpty(registration))
                            {
                                DataTable dtM = new DataTable();
                                dtM = cls.select_data_dt("AddEditMember " + registration.ID + ",'" + registration.RoleID + "','" + registration.TitleID + "','" + registration.Name + "','" + registration.Email + "','" + registration.Mobile + "','" + registration.GenderID + "','" + registration.LoginPin + "','" + registration.Password + "','" + registration.Address + "','" + registration.CityID + "','" + registration.PackageID + "'," + registration.ParentID + ",'" + txtMobile.Text + "','" + txtAadhar.Text + "','" + txtPanNumber.Text + "','" + registration.PinCode + "','" + registration.DOB + "','" + registration.ShopName + "','" + txtCityName.Text + "',22");
                                if (dtM.Rows.Count > 0)
                                {
                                    if (dtM.Rows[0]["Status"].ToString() == "1")
                                    {
                                        if (permission.OTPSendRegistration)
                                        {
                                            string[] ValueArray = new string[4];
                                            ValueArray[0] = registration.Name;
                                            ValueArray[1] = dtM.Rows[0]["Result"].ToString();
                                            ValueArray[2] = registration.Password;
                                            ValueArray[3] = registration.LoginPin.ToString();
                                            SMS.SendWithV(registration.Mobile, ConstantsData.Registration_SMS, ValueArray,company.MemberID);
                                        }
                                        string strf = cls.select_data_scalar_string("GetFreeService");
                                        string Msrnod = cls.select_data_scalar_string("select msrno from Member where loginid='" + dtM.Rows[0]["Result"].ToString() + "'");
                                        Int32 Val = cls.update_data("PROC_ASSIGN_SERVICE '" + strf + "'," + Msrnod + "");

                                        try
                                        {
                                            string URL = SONITechnoCredentrial.UPI;
                                            Location ObjLoaction = new Location();
                                            ObjLoaction = ObjLoaction.GetLocationData();
                                            UPI ObjUpi = new UPI();
                                            ObjUpi.category = "Retail Stores";
                                            ObjUpi.address = registration.Address;
                                            ObjUpi.lat = ObjLoaction.Latitude;
                                            ObjUpi.lon = ObjLoaction.Longitude;
                                            ObjUpi.name = registration.Name;
                                            ObjUpi.pan = registration.Pan;
                                            ObjUpi.upi_id = registration.Mobile;
                                            ObjUpi.MethodName = "createupiid";
                                            string result = ApiPostCall.PostCall(ObjUpi.GetJson(), URL);
                                            JObject jData = JObject.Parse(result);
                                            if (jData["code"].ToString().ToLower() != "err")
                                            {
                                                cls.update_data("AddEditMemberUPI '" + Msrnod + "','" + jData["data"]["upi"]["upi_id"].ToString() + "','" + ObjUpi.name + "','" + ObjUpi.pan + "','" + ObjUpi.category + "','" + ObjUpi.address + "','" + result + "'");
                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                        
                                        ErrorShow.AlertMessageWithRedirect(page,"Your Login ID -" + dtM.Rows[0]["Result"].ToString() + "", "../Member/default.aspx",ConstantsData.CompanyName);
                                        Clear();
                                    }
                                    else
                                    {
                                        ErrorShow.AlertMessage( page, dtM.Rows[0]["Result"].ToString(), ConstantsData.CompanyName);
                                    }
                                }
                                else
                                {
                                    ErrorShow.AlertMessage(page,"Try After some time", ConstantsData.CompanyName);
                                }
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                            ErrorShow.AlertMessage(page,"Select Package", ConstantsData.CompanyName);
                        }
                    }
                    else
                    {
                        foreach (ValidationResult validationResult in validationsResults)
                        {
                            ErrorShow.AlertMessage(page, validationResult.ErrorMessage, ConstantsData.CompanyName);
                        }
                    }
                }


            }
        }
        catch (Exception ex)
        {
            ErrorShow.Error(page1: page, Message: "Something Went Wrong ..");
        }
    }
  

    public DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }

    private void Clear()
    {
        txtAddress.Text = txtEmail.Text = txtMobile.Text = txtName.Text = string.Empty;
        ddltitle.SelectedIndex = 0;
        rdGender.SelectedIndex = 0;
    }


    protected void btnProcess_Click(object sender, EventArgs e)
    {
        if (permission.RegistratioWIthOTPVerification)
        {
            if (permission.RegistratioWIthOTPVerification)
            {
                if (btnProcess.Text != "Verify OTP")
                {
                    if (!string.IsNullOrEmpty(txtMobile.Text))
                    {
                        if (txtMobile.Text.Length == 10)
                        {
                            Int32 Count = cls.select_data_scalar_int("select count(*) from Member where Mobile='" + txtMobile.Text + "'");
                            if (Count == 0)
                            {
                                txtOTP.Enabled = true;
                                Random generator = new Random();
                                int r = generator.Next(100000, 999999);
                                string OTP = r.ToString();
                                string Message = "Login OTP is : " + OTP;
                                Session["OTP"] = OTP;
                                txtMobile.Attributes.Add("style", "pointer-events:none");
                                showOTPBox.Visible = true;
                                string[] ValueArray = new string[4];
                                ValueArray[0] = "User";
                                ValueArray[1] = OTP;
                                string str = SMS.SendWithV(txtMobile.Text, ConstantsData.Sign_Up_OTP_SMS, ValueArray, company.MemberID);
                                ErrorShow.AlertMessage(page, "OTP send on " + txtMobile.Text + " your mobile no", ConstantsData.CompanyName);
                                btnProcess.Text = "Verify OTP";
                            }
                            else
                            {
                                ErrorShow.AlertMessage(page, "Mobile Number Already Register", ConstantsData.CompanyName);

                            }
                        }
                        else
                        {
                            txtMobile.BorderColor = System.Drawing.Color.Red;

                            showOTPBox.Visible = false;
                            txtOTP.Enabled = false;
                        }
                    }
                    else
                    {
                        showOTPBox.Visible = false;
                        txtMobile.BorderColor = System.Drawing.Color.Red;
                        ErrorShow.Error(page1: page, Message: "Enter Mobile No.");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtOTP.Text))
                    {
                        if (Session["OTP"].ToString() == txtOTP.Text.Trim())
                        {
                            MAinBox.Visible = true;
                            btnProcess.Visible = false;
                            txtOTP.Attributes.Add("style", "pointer-events:none");
                            txtOTP.BorderColor = System.Drawing.Color.LightGray;

                        }
                        else
                        {
                            txtOTP.BorderColor = System.Drawing.Color.Red;
                            ErrorShow.Error(page1: page, Message: "Enter Current OTP");

                            MAinBox.Visible = false;
                        }
                    }
                    else
                    {
                        txtOTP.BorderColor = System.Drawing.Color.Red;

                    }
                }
            }
            else
            {
                MAinBox.Visible = true;
                btnProcess.Visible = false;
                txtOTP.Attributes.Add("style", "pointer-events:none");
                txtOTP.BorderColor = System.Drawing.Color.LightGray;
                showOTPBox.Visible = false;
            }
        }
        else
        {
            MAinBox.Visible = true;
            btnProcess.Visible = false;
            txtOTP.Attributes.Add("style", "pointer-events:none");
            txtOTP.BorderColor = System.Drawing.Color.LightGray;
            showOTPBox.Visible = false;
        }

    }

    protected void rptRoleData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName== "Res")
        {
            hdnRoleID.Value = e.CommandArgument.ToString();
            multiview.ActiveViewIndex = 1;
        }
    }
}