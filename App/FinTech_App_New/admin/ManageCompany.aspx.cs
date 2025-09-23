using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageMenu : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dtMember;
    int Val = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                txtCompanyName.Focus();
                FillMember();
                FillCompany();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void FillMember()
    {
        DataTable dtMember = ObjConnection.select_data_dt("select (LoginID + ' ( ' + Name + ' ) ')as Name,msrno from Member where isactive=1 and  roleID in (1,2)");
        Common.BindDropDown(ddlMember, dtMember, "Name", "msrno");
        ddlMember.Items.Insert(0, new ListItem("-- Select Member --", "0"));
    }
    private void FillCompany()
    {
        rptData.DataSource = ObjConnection.select_data_dt("select * from View_CompanyDetails");
        rptData.DataBind();
    }

    private void fill_update_menu(int id)
    {
        DataTable dt = new DataTable();
        hdnID.Value = id.ToString();
        dt = ObjConnection.select_data_dt("select * from View_CompanyDetails where ID = " + id + "");
        if (dt.Rows.Count > 0)
        {
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            txtAlternateEmail.Text = dt.Rows[0]["AlternateEmail"].ToString();
            txtAlternateMobile.Text = dt.Rows[0]["AlternateMobile"].ToString();
            txtAndroidURL.Text = dt.Rows[0]["AndroidURL"].ToString();
            txtCompanyName.Text = dt.Rows[0]["Name"].ToString();
            txtCopyright.Text = dt.Rows[0]["Copyright"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
            txtOwnerName.Text = dt.Rows[0]["OwnerName"].ToString();
            txtWebsiteURL.Text = dt.Rows[0]["WebsiteURL"].ToString();
            txtFacebook.Text = dt.Rows[0]["Facebook"].ToString();
            txtinstagram.Text = dt.Rows[0]["Instagram"].ToString();
            txtwhatsapp.Text = dt.Rows[0]["WhastApp"].ToString();
            txttwitter.Text = dt.Rows[0]["Twiter"].ToString();
            txtYoutube.Text = dt.Rows[0]["Youtube"].ToString();

            txtBankName.Text = dt.Rows[0]["BankName"].ToString();
            txtACName.Text = dt.Rows[0]["ACName"].ToString();
            txtAcType.Text = dt.Rows[0]["ACType"].ToString();
            txtACNumber.Text = dt.Rows[0]["ACNumber"].ToString();
            txtIFSCCode.Text = dt.Rows[0]["IFSC"].ToString();
            txtMicrCode.Text = dt.Rows[0]["MICRCode"].ToString();
            BodycolorPicker.Value = dt.Rows[0]["BodyColor"].ToString();
            HeadercolorPicker.Value = dt.Rows[0]["HeaderColor"].ToString();
            LeftcolorPicker.Value = dt.Rows[0]["LeftColor"].ToString();
            txtProfileAmount.Text = dt.Rows[0]["ProfileAmount"].ToString();
            ddlMember.SelectedValue = dt.Rows[0]["MemberID"].ToString();
            txtCompanyName.Focus();
            btnSubmit.Text = "Update";
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageCompany @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            FillCompany();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string imgLarge, logo = "";
        Bitmap bmptImgLarge = null;
        string gd = Guid.NewGuid().ToString() + ".png";
        string gdIcon = Guid.NewGuid().ToString() + ".png";
        string SgdIcon = Guid.NewGuid().ToString() + ".png";
        if (fuLogo.HasFile)
        {

            try
            {
                fuLogo.PostedFile.SaveAs(MapPath("./images/Company/") + gd);
                logo = gd.ToString();
            }
            catch (Exception ex)
            {
                Response.Write("Error occured: " + ex.Message.ToString());
            }
            finally
            {
                imgLarge = string.Empty;
                fuLogo = new FileUpload();
            }
        }
        if (fuicon.HasFile)
        {

            try
            {
                {
                    fuicon.PostedFile.SaveAs(MapPath("./images/Company/") + gdIcon);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error occured: " + ex.Message.ToString());
            }
            finally
            {
                fuLogo = new FileUpload();
            }
        }
        if (filesignature.HasFile)
        {

            try
            {
                {
                    filesignature.PostedFile.SaveAs(MapPath("./images/Company/") + SgdIcon);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error occured: " + ex.Message.ToString());
            }
            finally
            {
                filesignature = new FileUpload();
            }
        }
        DataTable dt = new DataTable();
        if (btnSubmit.Text == "Submit")
        {
            dt = ObjConnection.select_data_dt("EXEC ADDEDITCOMPANY 0,'" + txtCompanyName.Text.Trim() + "','" + txtOwnerName.Text.Trim() + "','" + txtEmail.Text.Trim() + "','" + txtAlternateEmail.Text.Trim() + "','" + txtMobile.Text.Trim() + "','" + txtAlternateMobile.Text.Trim() + "','" + txtWebsiteURL.Text.Trim() + "','" + txtAndroidURL.Text.Trim() + "','" + logo + "','" + txtAddress.Text.Trim() + "','" + txtCopyright.Text.Trim() + "','" + txtFacebook.Text.Trim() + "','" + txtwhatsapp.Text.Trim() + "','" + txtinstagram.Text.Trim() + "','" + txttwitter.Text.Trim() + "','" + txtYoutube.Text.Trim() + "','" + txtBankName.Text + "','" + txtACName.Text + "','" + txtAcType.Text + "','" + txtACNumber.Text + "','" + txtIFSCCode.Text + "','" + txtMicrCode.Text + "','" + txtProfileAmount.Text + "','" + gdIcon + "','" + SgdIcon + "','" + ddlMember.SelectedValue + "','" + BodycolorPicker.Value + "','" + LeftcolorPicker.Value + "','" + HeadercolorPicker.Value + "'");
        }
        else if (btnSubmit.Text == "Update")
        {
            dt = ObjConnection.select_data_dt("EXEC ADDEDITCOMPANY " + Convert.ToInt16(hdnID.Value) + ",'" + txtCompanyName.Text.Trim() + "','" + txtOwnerName.Text.Trim() + "','" + txtEmail.Text.Trim() + "','" + txtAlternateEmail.Text.Trim() + "','" + txtMobile.Text.Trim() + "','" + txtAlternateMobile.Text.Trim() + "','" + txtWebsiteURL.Text.Trim() + "','" + txtAndroidURL.Text.Trim() + "','" + logo + "','" + txtAddress.Text.Trim() + "','" + txtCopyright.Text.Trim() + "','" + txtFacebook.Text.Trim() + "','" + txtwhatsapp.Text.Trim() + "','" + txtinstagram.Text.Trim() + "','" + txttwitter.Text.Trim() + "','" + txtYoutube.Text.Trim() + "','" + txtBankName.Text + "','" + txtACName.Text + "','" + txtAcType.Text + "','" + txtACNumber.Text + "','" + txtIFSCCode.Text + "','" + txtMicrCode.Text + "','" + txtProfileAmount.Text + "','" + gdIcon + "','" + SgdIcon + "','" + ddlMember.SelectedValue + "','" + BodycolorPicker.Value + "','" + LeftcolorPicker.Value + "','" + HeadercolorPicker.Value + "'");
            btnSubmit.Text = "Save";
            btnSubmit.Enabled = false;
            btnDone.Visible = true;
            btnDone.Focus();
        }
        lblMessage.Text = dt.Rows[0]["Status"].ToString();
        Clear();
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            fill_update_menu(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }

        if (e.CommandName == "Active")
        {
            Active(ID: e.CommandArgument.ToString());
        }
    }

    private Bitmap Resize_Image(Stream streamImage, int maxWidth, int maxHeight)
    {
        Bitmap originalImage = new Bitmap(streamImage);
        int newWidth = originalImage.Width;
        int newHeight = originalImage.Height;
        double aspectRatio = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height);

        newWidth = maxWidth;
        newHeight = maxHeight;
        return new Bitmap(originalImage, newWidth, newHeight);
    }

    private void Clear()
    {
        txtAddress.Text = txtAlternateEmail.Text = txtAlternateMobile.Text = txtAndroidURL.Text = txtCompanyName.Text = txtCopyright.Text = string.Empty;
        txtEmail.Text = txtMobile.Text = txtOwnerName.Text = txtWebsiteURL.Text = string.Empty;
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        Response.Redirect("managecompany.aspx");
    }
}