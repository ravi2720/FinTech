using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_Registration : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    Registration Registration = new Registration();
    DataTable dt, dtMember;
    Page page;
    public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            company = Company.GetCompanyInfo();
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddltitle, "ActiveTitle", "Name", "ID", "Select Title");
                BindDropDown.FillDropDown(rdGender, "ActiveGender", "Name", "ID", "Select Gender");
                BindDropDown.FillDropDown(dllState, "ActiveState", "Name", "ID", "Select State");
                BindDropDown.FillDropDown(ddlRole, "RoleBasePermission", "Name", "ID", "Select Role", dtMember.Rows[0]["RoleID"].ToString());
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
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
            Random generator = new Random();
            string number = GenerateRandomNo().ToString();
            Registration.Name = txtName.Text.Trim();
            Registration.ShopName = txtShopName.Text.Trim();
            Registration.Mobile = txtMobile.Text.Trim();
            Registration.LoginPin = Convert.ToInt32(number);
            Registration.IsActive = chkActive.Checked;


            if (string.IsNullOrEmpty(rdGender.SelectedValue) || rdGender.SelectedValue == "0")
            {
                ErrorShow.AlertMessage(page, "Please select Gender", ConstantsData.CompanyName);
                return;
            }

            if (string.IsNullOrEmpty(ddlRole.SelectedValue) || ddlRole.SelectedValue == "0")
            {
                ErrorShow.AlertMessage(page, "Please select Role", ConstantsData.CompanyName);
                return;
            }

            if (string.IsNullOrEmpty(ddltitle.SelectedValue) || ddltitle.SelectedValue == "0")
            {
                ErrorShow.AlertMessage(page, "Please select Title", ConstantsData.CompanyName);
                return;
            }

            if (string.IsNullOrEmpty(ddlPackage.SelectedValue) || ddlPackage.SelectedValue == "0")
            {
                ErrorShow.AlertMessage(page, "Please select Package", ConstantsData.CompanyName);
                return;
            }

            if (string.IsNullOrEmpty(dllState.SelectedValue) || dllState.SelectedValue == "0")
            {
                ErrorShow.AlertMessage(page, "Please select State", ConstantsData.CompanyName);
                return;
            }

            Registration.GenderID = Convert.ToInt32(rdGender.SelectedValue);
            Registration.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
            Registration.TitleID = Convert.ToInt32(ddltitle.SelectedValue);
            Registration.PackageID = Convert.ToInt32(ddlPackage.SelectedValue);
            Registration.StateID = Convert.ToInt32(dllState.SelectedValue);
            Registration.Email = txtEmail.Text.Trim();
            Registration.CityID = 0;
            Registration.Address = txtAddress.Text.Trim();
            Registration.PinCode = txtpincode.Text.Trim();
            Registration.ParentID = 1;  // SessionID
            Registration.Password = Registration.Mobile;
            Registration.AlterNativeMobile = txtAlterMobile.Text;
            Registration.Aadhar = txtAadhar.Text;
            Registration.Pan = txtPanNumber.Text;
            Registration.CityName = txtCityName.Text.Trim();

            DateTime dob;
            if (!DateTime.TryParse(txtDOB.Text.Trim(), out dob))
            {
                ErrorShow.AlertMessage(page, "Invalid DOB format", ConstantsData.CompanyName);
                return;
            }

            Registration.DOB = dob.ToString("MM-dd-yyyy");

            TimeSpan sd = DateTime.Now - dob;
            double Years = sd.TotalDays / 365;

            if (Years <= 18)
            {
                ErrorShow.AlertMessage(page, "Only 18+ yrs old merchant are allowed to do this business.", ConstantsData.CompanyName);
                return;
            }

            if (!Registration.IsAnyNullOrEmpty(Registration))
            {
                ErrorShow.AlertMessage(page, "Please fill all required fields.", ConstantsData.CompanyName);
                return;
            }

            DataTable dtM = new DataTable();
            try
            {
                dtM = cls.select_data_dt("AddEditMember " + Registration.ID + ",'" + Registration.RoleID + "','" + Registration.TitleID + "','" + Registration.Name + "','" + Registration.Email + "','" + Registration.Mobile + "','" + Registration.GenderID + "','" + Registration.LoginPin + "','" + Registration.Password + "','" + Registration.Address + "','" + Registration.CityID + "','" + Registration.PackageID + "'," + Registration.ParentID + ",'" + txtAlterMobile.Text + "','" + txtAadhar.Text + "','" + txtPanNumber.Text + "','" + Registration.PinCode + "','" + Registration.DOB + "','" + Registration.ShopName + "','" + txtCityName.Text + "'," + dllState.SelectedValue + ",'" + company.MemberID + "'");
            }
            catch (Exception sqlEx)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('SQL Error: {sqlEx.Message.Replace("'", "\\'")}');", true);
                return;
            }

            if (dtM.Rows.Count > 0)
            {
                if (dtM.Rows[0]["Status"].ToString() == "1")
                {
                    string[] ValueArray = new string[4];
                    ValueArray[0] = Registration.Name;
                    ValueArray[1] = dtM.Rows[0]["Result"].ToString();
                    ValueArray[2] = Registration.Password;
                    ValueArray[3] = Registration.LoginPin.ToString();

                    ErrorShow.AlertMessage(page, "Your Login ID -" + dtM.Rows[0]["Result"].ToString() + "", ConstantsData.CompanyName);
                    string strf = cls.select_data_scalar_string("GetFreeService");
                    string Msrnod = cls.select_data_scalar_string("select msrno from Member where loginid='" + dtM.Rows[0]["Result"].ToString() + "'");
                    Int32 Val = cls.update_data("PROC_ASSIGN_SERVICE '" + strf + "'," + Msrnod + "");

                    try
                    {
                        string URL = SONITechnoCredentrial.UPIURL;
                        string iPAddress = cls_myMember.GetIPAddress();
                        Location objLocation = new Location();
                        objLocation = objLocation.GetLocationDataCurrent(iPAddress);
                        UPI ObjUpi = new UPI
                        {
                            category = "Retail Stores",
                            address = Registration.Address,
                            lat = objLocation.Latitude,
                            lon = objLocation.Longitude,
                            name = Registration.Name,
                            pan = Registration.Pan,
                            upi_id = Registration.Mobile,
                            MethodName = "createupiid"
                        };

                        string result = ApiPostCall.PostCall(ObjUpi.GetJson(), URL);
                        JObject jData = JObject.Parse(result);
                        if (jData["code"].ToString().ToLower() != "err")
                        {
                            cls.update_data("AddEditMemberUPI '" + Msrnod + "','" + jData["data"]["upi"]["upi_id"].ToString() + "','" + ObjUpi.name + "','" + ObjUpi.pan + "','" + ObjUpi.category + "','" + ObjUpi.address + "','" + result + "'");
                        }

                        SMS.SendWithV(Registration.Mobile, ConstantsData.Registration_SMS, ValueArray, company.MemberID);
                        Emails.SendEmail(Registration.Name, Registration.Email, ValueArray, "welcome.htm");
                    }
                    catch (Exception exUpi)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('UPI Error: {exUpi.Message.Replace("'", "\\'")}');", true);
                    }

                    ConstantsData.Clear(Page.Controls);
                }
                else
                {
                    ErrorShow.AlertMessage(page, dtM.Rows[0]["Result"].ToString(), ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Try After some time", ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('ERROR: {ex.Message.Replace("'", "\\'")}');", true);
        }
    }




    public DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }


    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropDown.FillDropDown(ddlPackage, "RoleWisePackage", "Name", "ID", "Select Package", Convert.ToInt32(ddlRole.SelectedValue.ToString()));
    }
}