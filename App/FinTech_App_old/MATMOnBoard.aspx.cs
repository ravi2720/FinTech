using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MATMOnBoard : System.Web.UI.Page
{
    clsMahagram ObjMahagram = new clsMahagram();
    cls_connection ObjAccess = new cls_connection();
    DataTable dtAPESMahaAPIDetail, dtMember;
    public static string AEPS_URL = "http://uat.dhansewa.com/AEPS/APIBCRegistration";
    public static string MerchantID = "";
    public static string MerchantKey = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["LoginID"] != null)
            {
                dtMember = ObjAccess.select_data_dt("select * from Member where Loginid='" + Request.QueryString["LoginID"].ToString() + "'");
                txtMemberID.Text = dtMember.Rows[0]["Loginid"].ToString();                
                BindState();
                txtMemberID_TextChanged(sender,e);
            }
        }
    }
    private void BindState()
    {
        DataTable dt = new DataTable();
        dt = GetJSONToDataTableUsingNewtonSoftDll(ObjMahagram.StateData());
        dllState.DataSource = dt;
        dllState.DataTextField = "statename";
        dllState.DataValueField = "stateid";
        dllState.DataBind();
        dllState.Items.Insert(0, new ListItem("Select State", "0"));

    }
    public static DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }
    protected void dllState_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();

        NameValueCollection values = new NameValueCollection()
       {
              { "stateid", dllState.SelectedValue }
       };
        dt = GetJSONToDataTableUsingNewtonSoftDll(ObjMahagram.DistrictData(values));
        dllDistrict.DataSource = dt;
        dllDistrict.DataTextField = "districtname";
        dllDistrict.DataValueField = "districtid";
        dllDistrict.DataBind();
        dllDistrict.Items.Insert(0, new ListItem("Select District", "0"));
    }

    public void clear()
    {
        txtAdddress.Text = "";
        txtFirstName.Text = "";
        txtMiddleName.Text = "";
        txtLastName.Text = "";
        txtEmialid.Text = "";
        txtPMobileName.Text = "";
        txtAMobileName.Text = "";
        txtMemberID.Text = "";
        txtDOB.Text = "";
        txtLandMark.Text = "";
        txtLocation.Text = "";
        txtMohhalla.Text = "";
        dllState.SelectedIndex = -1;
        dllDistrict.SelectedIndex = -1;
        txtBlock.Text = "";
        txtCity.Text = "";
        txtPanNo.Text = "";
        txtPinCode.Text = "";
        txtShopName.Text = "";
        dllTypeOfShop.SelectedIndex = -1;
        ddlQualification.SelectedIndex = -1;
        ddlAreaPopulation.SelectedIndex = -1;
        ddlTypeOfLocation.SelectedIndex = -1;

    }


    protected void Unnamed_Click(object sender, EventArgs e)
    {

        int Checkvalid = ObjAccess.select_data_scalar_int("Select Count(BCRegistrationId) from Member where loginid = '" + txtMemberID.Text + "'");
        if (Checkvalid == 0)
        {
            using (var client = new WebClient())
            {
                var Values = new NameValueCollection();
                Values["saltkey"] = clsMahagram.SaltKey;
                Values["secretkey"] = clsMahagram.Secret_Key;
                Values["MethodName"] = "bcregistration";
                Values["bc_f_name"] = txtFirstName.Text;
                Values["bc_m_name"] = txtMiddleName.Text;
                Values["bc_l_name"] = txtLastName.Text;
                Values["emailid"] = txtEmialid.Text;
                Values["Phone1"] = txtPMobileName.Text;
                Values["phone2"] = txtPMobileName.Text;
                Values["bc_dob"] = txtDOB.Text;
                Values["bc_state"] = dllState.SelectedItem.Value;
                Values["bc_district"] = dllDistrict.SelectedItem.Value;
                Values["bc_address"] = txtAdddress.Text;
                Values["bc_block"] = txtBlock.Text;
                Values["bc_city"] = txtCity.Text;
                Values["bc_landmark"] = txtLandMark.Text;
                Values["bc_loc"] = txtLocation.Text;
                Values["bc_mohhalla"] = txtMohhalla.Text;
                Values["bc_pan"] = txtPanNo.Text;
                Values["bc_pincode"] = txtPinCode.Text;
                Values["shopname"] = txtShopName.Text;
                Values["kyc1"] = "";//ObjMahagram.ImageToBase64(GetPath(fileIdentityP, "ID", "IdentityProof"));
                Values["kyc2"] = "";// ObjMahagram.ImageToBase64(GetPath(fileAddressP, "AD", "AddressProof"));
                Values["kyc3"] = "";//ObjMahagram.ImageToBase64(GetPath(fileShopPhotoP, "SP", "ShopPhoto"));
                Values["kyc4"] = "";//ObjMahagram.ImageToBase64(GetPath(filePassSizePhotoP, "PS", "PassportSize"));
                Values["shopType"] = (dllTypeOfShop.SelectedItem.Text == "other" ? txtOtherType.Text : dllTypeOfShop.SelectedItem.Text);
                Values["qualification"] = ddlQualification.SelectedItem.Text;
                Values["population"] = ddlAreaPopulation.SelectedItem.Text;
                Values["locationType"] = ddlTypeOfLocation.SelectedItem.Text;

                var responseString = ObjMahagram.Registration(Values);
                if (!string.IsNullOrEmpty(responseString))
                {
                    JArray ObjData = JArray.Parse(responseString);
                    if (ObjData[0]["StatusCode"] == null)
                    {
                        if (ObjData[0]["Statuscode"].ToString().ToUpper() == "000")
                        {

                            ObjAccess.insert_data("MANAGE_MAHAGRAMAEPS_PROCESS '" + ObjData[0]["emailid"].ToString() + "','" + ObjData[0]["bc_id"].ToString() + "','" + txtMemberID.Text + "','" + ObjData[0]["Message"].ToString() + "',''");

                            ErrorShow.AlertMessage(Page, "BC Registration Successfull.Your BC ID is : " + ObjData[0]["bc_id"].ToString() + "",ConstantsData.CompanyName);

                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('BC Registration Successfull.Your BC ID is : " + ObjData["data"][0]["bc_id"].ToString() + "');", true);
                            clear();
                        }
                        else
                        {
                            ErrorShow.AlertMessage(Page, ObjData[0]["Message"].ToString(), ConstantsData.CompanyName);

                        }
                    }
                    else
                    {
                        ErrorShow.AlertMessage(Page, ObjData[0]["Message"].ToString(), ConstantsData.CompanyName);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Something goes wrong. try after sometime.'); ", true);
                }
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Member Already Registered');", true);
        }
    }

    public static string CheckRequestIP()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }
        return context.Request.ServerVariables["REMOTE_ADDR"];
    }
    private string GetPath(FileUpload fileObj, string tag, string loc)
    {
        string FileName = "";
        string opath = "";
        try
        {
            opath = Server.MapPath("~/images/AEPS2/" + loc + "/");
            string Extension = System.IO.Path.GetExtension(fileObj.FileName).ToLower();
            if (Extension == ".jpg" || Extension == ".jpeg" || Extension == ".png")
            {
                FileName = tag + DateTime.Now.Ticks + Extension;
                fileObj.PostedFile.SaveAs(opath + FileName);
            }
        }
        catch (Exception ex)
        {
        }
        return (opath + FileName);
    }

    protected void txtMemberID_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtMemberID.Text != "")
            {

                int Checkvalid = ObjAccess.select_data_scalar_int("Select Count(BcRegistrationId) from member where loginid = '" + txtMemberID.Text + "'");
                if (Checkvalid > 0)
                {
                    DataTable dtBCRegistrationId = ObjAccess.select_data_dt("Select * from Member where loginid = '" + txtMemberID.Text + "'");

                    string requestIP = CheckRequestIP();
                    using (var client = new WebClient())
                    {
                        var Values = new NameValueCollection();
                        Values["bc_id"] = dtBCRegistrationId.Rows[0]["BCRegistrationId"].ToString();
                        Values["phone1"] = dtBCRegistrationId.Rows[0]["Mobile"].ToString();
                        Values["ip"] = requestIP;
                        Values["userid"] = dtBCRegistrationId.Rows[0]["BCRegistrationId"].ToString();
                        Values["saltkey"] = clsMahagram.SaltKey;
                        Values["secretkey"] = clsMahagram.Secret_Key;
                        var responseString = ObjMahagram.BCInitiate(Values);
                        JArray ObjData = JArray.Parse(responseString);

                        if (ObjData[0]["Message"].ToString().ToLower() == "success")
                        {
                            string str = "https://icici.bankmitra.org/Location.aspx?text=" + ObjData[0]["Result"].ToString();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "window.open('" + str + "','_self');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('" + ObjData[0]["Message"].ToString() + "');", true);
                        }
                    }
                }
                else
                {
                    DataTable dtMemberDetails = new DataTable();
                    dtMemberDetails = ObjAccess.select_data_dt("Select Name,Email,Mobile from Member where loginid='" + txtMemberID.Text.Trim() + "'");
                    if (dtMemberDetails.Rows.Count > 0)
                    {
                        txtFirstName.Text = dtMemberDetails.Rows[0]["Name"].ToString();
                        //txtMiddleName.Text = dtMemberDetails.Rows[0]["MiddleName"].ToString();
                        // txtLastName.Text = dtMemberDetails.Rows[0]["LastName"].ToString();
                        txtEmialid.Text = dtMemberDetails.Rows[0]["Email"].ToString();
                        txtPMobileName.Text = dtMemberDetails.Rows[0]["Mobile"].ToString();
                        //txtAMobileName.Text = dtMemberDetails.Rows[0]["Mobile1"].ToString();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('Some Error Occurred!! " + ex.Message.ToString() + "');", true);
        }
    }
}