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

public partial class Member_Default : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    Registration registration = new Registration();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    public Permission permission;
    Page page;
    public Company company;
    protected void Page_Init(object sender, EventArgs e)
    {
        permission = Permission.GetPermission();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            company = Company.GetCompanyInfo();
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddltitle, "ActiveTitle", "Name", "ID", "Select Title");
                BindDropDown.FillDropDown(rdGender, "ActiveGender", "Name", "ID", "Select Gender");
                BindDropDown.FillDropDown(dllState, "ActiveState", "Name", "ID", "Select State");
                BindDropDown.FillDropDown(rptRoleList, "RoleBasePermission", dtMember.Rows[0]["RoleID"].ToString());
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    public void FillDropDown(DropDownList ddl, string ActionName, string Name, string Value, string DefaultMessage, int RoleID, int CompanyID)
    {
        cls_connection objData = new cls_connection();
        DataTable dt = new DataTable();
        dt = objData.select_data_dt("select * from package where RoleID=" + RoleID + " and CompanyID=" + CompanyID + "");
        ddl.DataSource = dt;
        ddl.DataTextField = Name;
        ddl.DataValueField = Value;
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem(DefaultMessage, "0"));

    }

    protected void rptRoleList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Role")
        {
            hdnRoleID.Value = e.CommandArgument.ToString();
            ViewState["RoleID"] = e.CommandArgument.ToString();
            multiview.ActiveViewIndex = 1;
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
                    registration.GenderID = Convert.ToInt32(rdGender.SelectedValue);
                    registration.Email = txtEmail.Text.Trim();
                    registration.CityID = Convert.ToInt32(0);
                    registration.PinCode = txtpincode.Text.Trim();
                    registration.DOB = Convert.ToDateTime(txtDOB.Text.Trim()).ToString("MM-dd-yyyy");
                    registration.Address = txtAddress.Text.Trim();
                    registration.PackageID = cls.select_data_scalar_int("select ID from Package where RoleID=" + ViewState["RoleID"].ToString() + "");
                    registration.ParentID = Convert.ToInt32(dtMember.Rows[0]["Msrno"].ToString());  // Pass sessionid
                    registration.Password = registration.Mobile;
                    registration.RoleID = ViewState["RoleID"].ToString().ToInt32();
                    registration.TitleID = Convert.ToInt32(ddltitle.SelectedValue);
                    registration.AlterNativeMobile = txtAlterMobile.Text;
                    registration.Aadhar = txtAadhar.Text;
                    registration.Pan = txtPanNumber.Text;
                    registration.StateID = Convert.ToInt32(dllState.SelectedValue);
                    registration.CityName = txtCityName.Text.Trim();
                    if (registration.PackageID > 0)
                    {
                        if (registration.IsAnyNullOrEmpty(registration))
                        {
                            DataTable dtM = new DataTable();
                            dtM = cls.select_data_dt("AddEditMember " + registration.ID + ",'" + registration.RoleID + "','" + registration.TitleID + "','" + registration.Name + "','" + registration.Email + "','" + registration.Mobile + "','" + registration.GenderID + "','" + registration.LoginPin + "','" + registration.Password + "','" + registration.Address + "','" + registration.CityID + "','" + registration.PackageID + "'," + registration.ParentID + ",'" + txtAlterMobile.Text + "','" + txtAadhar.Text + "','" + txtPanNumber.Text + "','" + registration.PinCode + "','" + registration.DOB + "','" + registration.ShopName + "','" + txtCityName.Text + "'," + dllState.SelectedValue + ",'" + company.MemberID + "'");
                            if (dtM.Rows.Count > 0)
                            {
                                if (dtM.Rows[0]["Status"].ToString() == "1")
                                {
                                    string[] ValueArray = new string[4];
                                    ValueArray[0] = registration.Name;
                                    ValueArray[1] = dtM.Rows[0]["Result"].ToString();
                                    ValueArray[2] = registration.Password;
                                    ValueArray[3] = registration.LoginPin.ToString();
                                    SMS.SendWithV(registration.Mobile, ConstantsData.Registration_SMS, ValueArray, company.MemberID);
                                    string strf = cls.select_data_scalar_string("GetFreeService");

                                    string Msrnod = cls.select_data_scalar_string("select msrno from Member where loginid='" + dtM.Rows[0]["Result"].ToString() + "'");
                                    Int32 Val = cls.update_data("PROC_ASSIGN_SERVICE '" + strf + "'," + Msrnod + "");
                                    try
                                    {
                                        string URL = SONITechnoCredentrial.UPI;

                                        string iPAddress = cls_myMember.GetIPAddress();
                                        Location location = new Location();
                                        location = location.GetLocationDataCurrent(iPAddress);

                                        UPI ObjUpi = new UPI();
                                        ObjUpi.category = "Retail Stores";
                                        ObjUpi.address = registration.Address;
                                        ObjUpi.lat = location.Latitude;
                                        ObjUpi.lon = location.Longitude;
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
                                    ErrorShow.AlertMessage(page, "Your Login ID -" + dtM.Rows[0]["Result"].ToString() + "", ConstantsData.CompanyName);
                                    ConstantsData.Clear(page.Controls);
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
                    }
                    else
                    {
                        ErrorShow.AlertMessage(page, "Select Package", ConstantsData.CompanyName);
                    }
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Please enter Valid OTP", ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.Error(page1: page, Message: "Something Went Wrong ..");
        }
    }

    protected void btnProcess_Click(object sender, EventArgs e)
    {
        Int32 Count = cls.select_data_scalar_int("select count(*) from Member where Mobile='" + txtMobile.Text + "'");
        if (Count == 0)
        {
            MAinBox.Visible = true;
            btnProcess.Visible = false;
            txtOTP.Attributes.Add("style", "pointer-events:none");
            txtOTP.BorderColor = System.Drawing.Color.LightGray;
            showOTPBox.Visible = false;
        }
        else
        {
            ErrorShow.AlertMessage(page, "Mobile Number Already Register", ConstantsData.CompanyName);
        }
    }


    private void Clear()
    {
        txtAddress.Text = txtEmail.Text = txtMobile.Text = txtName.Text = string.Empty;
        ddltitle.SelectedIndex = 0;
        rdGender.SelectedIndex = 0;
    }

    protected void txtpincode_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtpincode.Text))
        {
            string Result = ConstantsData.GetBranchCode(txtpincode.Text);
            JArray jData = JArray.Parse(Result);
            if (jData[0]["Status"].ToString().ToUpper() == "SUCCESS")
            {
                DataTable dt = ConstantsData.GetJSONToDataTableUsingNewtonSoftDll(jData[0]["PostOffice"].ToString());
                dllPostOffice.DataSource = dt;
                dllPostOffice.DataTextField = "Name";
                dllPostOffice.DataValueField = "State";
                dllPostOffice.DataBind();

                txtCityName.Text = dt.Rows[0]["Division"].ToString();

            }
            else
            {
                txtpincode.Text = "";
                ErrorShow.AlertMessage(page, jData[0]["Message"].ToString(), ConstantsData.CompanyName);
            }
        }
        else
        {

        }
    }
}