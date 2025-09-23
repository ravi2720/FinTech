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

public partial class Admin_AddService : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dtMember, dt;
    Service service = new Service();
    int Val = 0;
    Page page;
    ActionButtonPermission actionButtonPermission;
    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.Service, dtMember.Rows[0]["RoleID"].ToString());
            if (!IsPostBack)
            {
                GetService();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    public void GetService()
    {
        dt = ObjConnection.select_data_dt("ManageService @Action='GetAll'");
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (actionButtonPermission != null)
            {
                if (actionButtonPermission.Submit)
                {
                    string imgFront, Front = "";
                    Bitmap bmFront = null;
                    string gdFront = Guid.NewGuid().ToString() + ".png";
                    if (flieUpload.HasFile)
                    {
                        flieUpload.PostedFile.SaveAs(MapPath("./images/icon/") + gdFront);
                        Front = gdFront.ToString();
                    }
                    service.Name = txtServiceName.Text;
                    service.URL = txtURL.Text;
                    service.Price = txtPrice.Text;
                    service.ImageURL = Front;
                    service.ID = Convert.ToInt32(hdnServiceID.Value);
                    service.SectionTypeID = Convert.ToInt32(ddlSectionType.SelectedValue);
                    service.IsActive = chkActive.Checked;
                    if (service.IsAnyNullOrEmpty(service))
                    {
                        Val = ObjConnection.update_data("AddEditService " + service.ID + ",'" + service.Name + "','" + service.URL + "','" + service.SectionTypeID + "'," + service.IsActive + ",'" + txtOnTime.Text + "','" + txtOffTime.Text + "','" + dtMember.Rows[0]["ID"].ToString() + "'," + service.Price + ",'" + Front + "','" + chkIsNew.Checked + "','" + chkSoon.Checked + "','" + chkOnOff.Checked + "','" + txtReason.Text + "'");
                        if (Val > 0) { GetService(); Clear(); }
                        ErrorShow.Success(page, "Add Successfully.");
                    }
                    else
                    {
                        ErrorShow.Error(Page, "Fill all required details");
                    }
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Not Permission For Add Service", ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Add Service", ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.Error(Page, ex.InnerException.Message);
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

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            if (actionButtonPermission.Edit)
            {
                FillData(ID: e.CommandArgument.ToString());
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Edit Service", ConstantsData.CompanyName);
            }
        }

        else if (e.CommandName == "Active")
        {
            if (actionButtonPermission.Active)
            {
                Active(ID: e.CommandArgument.ToString());
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Active Service", ConstantsData.CompanyName);
            }
        }

        if (e.CommandName == "onoff")
        {
            OnOFF(ID: e.CommandArgument.ToString());
        }
    }

    private void FillData(string ID)
    {
        dt = ObjConnection.select_data_dt("ManageService @Action='GetAll',@ID=" + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnServiceID.Value = ID;
            txtServiceName.Text = dt.Rows[0]["Name"].ToString();
            txtPrice.Text = dt.Rows[0]["Price"].ToString();
            txtURL.Text = dt.Rows[0]["URL"].ToString();
            ddlSectionType.SelectedValue = dt.Rows[0]["SectionType"].ToString();
            chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
            chkIsNew.Checked = Convert.ToBoolean(dt.Rows[0]["IsNew"].ToString());
            chkSoon.Checked = Convert.ToBoolean(dt.Rows[0]["IsComming"].ToString());
            chkOnOff.Checked = Convert.ToBoolean(dt.Rows[0]["OnOff"].ToString());
            txtReason.Text = dt.Rows[0]["Reason"].ToString();
            txtOffTime.Text = dt.Rows[0]["OffTime"].ToString();
            txtOnTime.Text = dt.Rows[0]["OnTime"].ToString();
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageService @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            GetService();
        }
    }

    private void OnOFF(string ID)
    {
        Val = ObjConnection.update_data("ManageService @Action='OnOFF',@ID=" + ID + "");
        if (Val > 0)
        {
            GetService();
        }
    }

    private void Clear()
    {
        txtURL.Text = txtServiceName.Text = "";
        txtOffTime.Text = txtOnTime.Text = "0";
        chkActive.Checked = false;
        chkIsNew.Checked = false;
        chkSoon.Checked = false;
        txtPrice.Text = hdnServiceID.Value = "0";
        ddlSectionType.SelectedIndex = 0;
    }
}