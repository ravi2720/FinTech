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


public partial class Admin_AepsRegistration : System.Web.UI.Page
{
    clsMahagram ObjMahagram = new clsMahagram();
    cls_connection ObjAccess = new cls_connection();
    DataTable dtAPESMahaAPIDetail = new DataTable();
    public static string AEPS_URL = "http://uat.dhansewa.com/AEPS/APIBCRegistration";
    public static string MerchantID = "";
    public static string MerchantKey = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        dtAPESMahaAPIDetail = ObjAccess.select_data_dt("Select APIID,PIN from tblAEPS_Setting");
        if (dtAPESMahaAPIDetail.Rows.Count > 0)
        {
            MerchantID = dtAPESMahaAPIDetail.Rows[0]["APIID"].ToString();
            MerchantKey = dtAPESMahaAPIDetail.Rows[0]["PIN"].ToString();
        }

        if (!IsPostBack)
        {
           // if (Session["dtEmployee"] != null)
            {
                DataTable dtMemberMaster = new DataTable();
                dtMemberMaster = (DataTable)Session["dtMember"];
                BindState();
            }
            //else
            //{
            //    Response.Redirect("../Dashboard.aspx");
            //}
        }
    }
    private void BindState()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetJSONToDataTableUsingNewtonSoftDll(ObjMahagram.StateData());
            dllState.DataSource = dt;
            dllState.DataTextField = "statename";
            dllState.DataValueField = "stateid";
            dllState.DataBind();
            dllState.Items.Insert(0, new ListItem("Select State", "0"));
        }
        catch (Exception ex)
        {
        }
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
        try
        {

            //int Checkvalid = ObjAccess.select_data_scalar_int("Select Count(BCRegistrationId) from Member where loginid = '" + txtMemberID.Text + "'");

            //if (Checkvalid == 0)
            {
                using (var client = new WebClient())
                {
                    var Values = new NameValueCollection();
                    // Values["MerchantID"] = MerchantID;
                    // Values["MerchantKey"] = MerchantKey;
                    // Values["MethodName"] = "bcregistration";
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
                    Values["saltkey"] = "MH82#E@IN038#F6230UQ3";
                    Values["secretkey"] = "MH82#E@IN038#F6230UQ3";
                    Values["shopname"] = txtShopName.Text;
                    Values["kyc1"] = "";//ObjMahagram.ImageToBase64(GetPath(fileIdentityP, "ID", "IdentityProof"));
                    Values["kyc2"] = "";// ObjMahagram.ImageToBase64(GetPath(fileAddressP, "AD", "AddressProof"));
                    Values["kyc3"] = "";//ObjMahagram.ImageToBase64(GetPath(fileShopPhotoP, "SP", "ShopPhoto"));
                    Values["kyc4"] = "";// ObjMahagram.ImageToBase64(GetPath(filePassSizePhotoP, "PS", "PassportSize"));

                    Values["shopType"] = (dllTypeOfShop.SelectedItem.Text == "other" ? txtOtherType.Text : dllTypeOfShop.SelectedItem.Text);
                    Values["qualification"] = ddlQualification.SelectedItem.Text;
                    Values["population"] = ddlAreaPopulation.SelectedItem.Text;
                    Values["locationType"] = ddlTypeOfLocation.SelectedItem.Text;




                    var responseString = ObjMahagram.Registration(Values);
                    ObjAccess.update_data("insert into testaepsCustomer(Data)values('" + responseString + "')");
                    if (!string.IsNullOrEmpty(responseString))
                    {
                        JArray ObjData = JArray.Parse(responseString);
                        if (ObjData[0]["Statuscode"].ToString().ToUpper() == "000")
                        {

                            ObjAccess.insert_data("MANAGE_MAHAGRAMAEPS_PROCESS '" + ObjData[0]["emailid"].ToString() + "','" + ObjData[0]["bc_id"].ToString() + "','" + txtMemberID.Text + "','" + ObjData[0]["Message"].ToString() + "',''");

                            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('BC Registration Successfull.Your BC ID is : " + ObjData[0]["bc_id"].ToString() + "');", true);

                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('BC Registration Successfull.Your BC ID is : " + ObjData["data"][0]["bc_id"].ToString() + "');", true);
                            clear();
                        }
                        else
                        {

                            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Some Error Occurred!! " + ObjData[0]["Message"].ToString() + "');", true);

                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Something goes wrong. try after sometime.'); ", true);
                    }
                }
            }
            //else
            //{
            //    Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Member Already Registered');", true);
            //}
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Some Error Occurred!! " + ex.Message.ToString() + "');", true);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('Some Error Occurred!! " + ex.Message.ToString() + "');", true);
            clear();
        }

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

                //int Checkvalid = ObjAccess.select_data_scalar_int("Select Count(BcRegistrationId) from member where loginid = '" + txtMemberID.Text + "'");
                //if (Checkvalid > 0)
                //{
                //    Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Member Already Registered');", true);
                //}
                //else
                {
                    DataTable dtMemberDetails = new DataTable();
                    dtMemberDetails = ObjAccess.select_data_dt("Select Name,Email,Mobile,Pan,Address from Member where loginid='" + txtMemberID.Text.Trim() + "'");
                    if (dtMemberDetails.Rows.Count > 0)
                    {
                        txtFirstName.Text = dtMemberDetails.Rows[0]["Name"].ToString();
                        //txtMiddleName.Text = dtMemberDetails.Rows[0]["MiddleName"].ToString();
                        // txtLastName.Text = dtMemberDetails.Rows[0]["LastName"].ToString();
                        txtEmialid.Text = dtMemberDetails.Rows[0]["Email"].ToString();
                        txtPMobileName.Text = dtMemberDetails.Rows[0]["Mobile"].ToString();
                        txtPanNo.Text = dtMemberDetails.Rows[0]["Pan"].ToString();
                        txtAdddress.Text = dtMemberDetails.Rows[0]["Address"].ToString();
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