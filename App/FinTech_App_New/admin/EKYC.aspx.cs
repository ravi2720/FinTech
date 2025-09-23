using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EKYC : System.Web.UI.Page
{
    clsMahagram ObjMahagram = new clsMahagram();
    cls_connection Objconnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void txtMember_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtMember.Text))
        {
            DataTable dthistory = Objconnection.select_data_dt("select * from member where loginid='" + txtMember.Text + "'"); ;
            if (dthistory.Rows.Count > 0)
            {
                tblName.Text = dthistory.Rows[0]["Name"].ToString();
                lblBCID.Text = dthistory.Rows[0]["BCRegistrationID"].ToString();
                lblMobile.Text = dthistory.Rows[0]["Mobile"].ToString();
            }
        }
    }

    protected void btnkycUpload_Click(object sender, EventArgs e)
    {
        var Values = new NameValueCollection();
        Values["saltkey"] = "MH82#E@IN038#F6230UQ3";
        Values["secretkey"] = "MH82#E@IN038#F6230UQ3";
        Values["bc_id"] = lblBCID.Text;
        Values["phone1"] = lblMobile.Text;
        Values["kyc1"] = ObjMahagram.ImageToBase64(GetPath(fileKYC, "ID", "IdentityProof"));
        var responseString = ObjMahagram.KYCUpload(Values);
        //[{"Message":"Success","StatusCode":"001"}]
        JArray ObjData = JArray.Parse(responseString);
        if (ObjData[0]["StatusCode"] != null)
        {
            if (ObjData[0]["StatusCode"].ToString().ToUpper() == "001")
            {
                btnkycUpload.Visible = false;
                lblKYCStatus.Text = "Upload Success";
                lblKYCStatus.CssClass = "btn btn-success";
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Upload Successfully');", true);
            }
            else
            {
                btnkycUpload.Visible = true;
                lblKYCStatus.Text = ObjData[0]["Message"].ToString();
                lblKYCStatus.CssClass = "btn btn-danger";
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('"+ ObjData[0]["Message"].ToString() + "');", true);

            }
        }
    }
    private string GetPath(FileUpload fileObj, string tag, string loc)
    {
        string FileName = "";
        string opath = "";
        try
        {
            opath = Server.MapPath("~/images/");
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
    protected void btnAddressKYC_Click(object sender, EventArgs e)
    {
        var Values = new NameValueCollection();
        Values["saltkey"] = "MH82#E@IN038#F6230UQ3";
        Values["secretkey"] = "MH82#E@IN038#F6230UQ3";
        Values["bc_id"] = lblBCID.Text;
        Values["phone1"] = lblMobile.Text;
        Values["kyc2"] = ObjMahagram.ImageToBase64(GetPath(fileAddressKYC, "ID", "IdentityProof"));
        var responseString = ObjMahagram.AddressUpload(Values);
        //[{"Message":"Success","StatusCode":"001"}]
        JArray ObjData = JArray.Parse(responseString);
        if (ObjData[0]["StatusCode"] != null)
        {
            if (ObjData[0]["StatusCode"].ToString().ToUpper() == "001")
            {
                btnAddressKYC.Visible = false;
                lblAddressKYC.Text = "Upload Success";
                lblAddressKYC.CssClass = "btn btn-success";
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Upload Successfully');", true);
            }
            else
            {
                btnAddressKYC.Visible = true;
                lblAddressKYC.Text = ObjData[0]["Message"].ToString();
                lblAddressKYC.CssClass = "btn btn-danger";
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('" + ObjData[0]["Message"].ToString() + "');", true);

            }
        }
    }

   

    protected void btnPhoto_Click(object sender, EventArgs e)
    {
        var Values = new NameValueCollection();
        Values["saltkey"] = "MH82#E@IN038#F6230UQ3";
        Values["secretkey"] = "MH82#E@IN038#F6230UQ3";
        Values["bc_id"] = lblBCID.Text;
        Values["phone1"] = lblMobile.Text;
        Values["kyc4"] = ObjMahagram.ImageToBase64(GetPath(filePhoto, "ID", "IdentityProof"));
        var responseString = ObjMahagram.Photo(Values);
        //[{"Message":"Success","StatusCode":"001"}]
        JArray ObjData = JArray.Parse(responseString);
        if (ObjData[0]["StatusCode"] != null)
        {
            if (ObjData[0]["StatusCode"].ToString().ToUpper() == "001")
            {
                btnPhoto.Visible = false;
                lblPhoto.Text = "Upload Success";
                lblPhoto.CssClass = "btn btn-success";
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Upload Successfully');", true);
            }
            else
            {
                btnPhoto.Visible = true;
                lblPhoto.Text = ObjData[0]["Message"].ToString();
                lblPhoto.CssClass = "btn btn-danger";
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('" + ObjData[0]["Message"].ToString() + "');", true);

            }
        }
    }

    protected void btnShop_Click(object sender, EventArgs e)
    {
        var Values = new NameValueCollection();
        Values["saltkey"] = "MH82#E@IN038#F6230UQ3";
        Values["secretkey"] = "MH82#E@IN038#F6230UQ3";
        Values["bc_id"] = lblBCID.Text;
        Values["phone1"] = lblMobile.Text;
        Values["kyc3"] = ObjMahagram.ImageToBase64(GetPath(fileShop, "ID", "IdentityProof"));
        var responseString = ObjMahagram.Shop(Values);
        //[{"Message":"Success","StatusCode":"001"}]
        JArray ObjData = JArray.Parse(responseString);
        if (ObjData[0]["StatusCode"] != null)
        {
            if (ObjData[0]["StatusCode"].ToString().ToUpper() == "001")
            {
                btnShop.Visible = false;
                lblEShop.Text = "Upload Success";
                lblEShop.CssClass = "btn btn-success";
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('Upload Successfully');", true);
            }
            else
            {
                btnShop.Visible = true;
                lblEShop.Text = ObjData[0]["Message"].ToString();
                lblEShop.CssClass = "btn btn-danger";
                Page.ClientScript.RegisterStartupScript(GetType(), "msgbox", "alert('" + ObjData[0]["Message"].ToString() + "');", true);

            }
        }
    }
}