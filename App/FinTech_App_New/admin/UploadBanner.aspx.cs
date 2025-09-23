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
public partial class Admin_UploadBanner : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember, dtcom;
    int Val = 0;
    Page page;
    Company company;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            company = Company.GetCompanyInfo();
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlImage, "ActiveBannerType", "Name", "ID", "Select BannerType");
                GetBannerImage();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    public void GetBannerImage()
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManageBannerImage 'GetAll',0,'" + company.MemberID + "'");
            if (dt.Rows.Count > 0)
            {
                rptData.DataSource = dt;
                rptData.DataBind();
            }
            else
            {
                rptData.DataSource = null;
                rptData.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (fileUpload.HasFile)
            {
                string imgLarge, logo = "";
                Bitmap bmptImgLarge = null;
                string InputFile = string.Empty;
                InputFile = System.IO.Path.GetExtension(fileUpload.FileName);
                string gd = Guid.NewGuid().ToString() + InputFile;
                if (fileUpload.HasFile)
                {
                    try
                    {
                        fileUpload.PostedFile.SaveAs(MapPath("./images/Banner/") + gd);
                        logo = gd.ToString();

                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error occured: " + ex.Message.ToString());
                    }
                    finally
                    {
                        //imgLarge = string.Empty;
                        //bmptImgLarge.Dispose();
                        //fileUpload = new FileUpload();
                    }
                }

                Val = ObjConnection.update_data("AddEditBannerImage " + hdnImageID.Value + ",'" + logo + "','" + ddlImage.SelectedValue + "'," + chkActive.Checked + ",'" + dtMember.Rows[0]["ID"].ToString() + "','" + company.MemberID + "'");
                if (Val > 0)
                {
                    ErrorShow.AlertMessage(page, "Add Image Successfully", ConstantsData.CompanyName);
                    GetBannerImage(); Clear();
                }
                else
                    ErrorShow.AlertMessage(page, "Duplicate Image", ConstantsData.CompanyName);
            }
            else
            {
                ErrorShow.AlertMessage(page, "Enter All Required Field", ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            FillData(ID: e.CommandArgument.ToString());
        }

        if (e.CommandName == "Active")
        {
            Active(ID: e.CommandArgument.ToString());
        }
        if (e.CommandName == "Delete")
        {
            ObjConnection.update_data("delete from BannerImage where id=" + e.CommandArgument + "");
            GetBannerImage();
        }
    }

    private void FillData(string ID)
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManageBannerImage 'GetByID'," + ID + "");
            if (dt.Rows.Count > 0)
            {
                hdnImageID.Value = ID;
                ddlImage.SelectedIndex = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }
    }

    private void Active(string ID)
    {
        try
        {
            Val = ObjConnection.update_data("ManageBannerImage @Action='IsActive',@ID=" + ID + "");
            if (Val > 0)
            {
                GetBannerImage();
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }
    }

    private void Clear()
    {
        ddlImage.SelectedIndex = 0;

        chkActive.Checked = false;
        hdnImageID.Value = "0";
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
}