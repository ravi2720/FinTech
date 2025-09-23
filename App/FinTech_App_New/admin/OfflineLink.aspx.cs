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
public partial class Admin_OfflineLink : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dtMember,dt;
    Service service = new Service();
    int Val = 0;
    Page page;

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
        dt = ObjConnection.select_data_dt("ManageOffLineServiceLink 'GetByMsrno','" + dtMember.Rows[0]["ID"].ToString() +"'");
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
            string imgFront, Front = "";
            Bitmap bmFront = null;
            string gdFront = Guid.NewGuid().ToString() + ".jpeg";
            if (flieUpload.HasFile)
            {
                flieUpload.PostedFile.SaveAs(MapPath("./images/Service/") + gdFront);
                Front = gdFront.ToString();
            }


            if (!string.IsNullOrEmpty(txtServiceName.Text))
            {
                Val = ObjConnection.update_data("AddEditOffLineServiceLink " + hdnServiceID.Value + ",'" + txtServiceName.Text + "','" + txtURL.Text + "','" + Front + "'," + chkActive.Checked + ",'"+dtMember.Rows[0]["ID"].ToString()+"'");
                if (Val > 0) { GetService(); Clear(); }
                ErrorShow.Success(page, "Add Successfully.");
            }
            else
            {
                ErrorShow.Error(Page, "Fill all required details");
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
            FillData(ID: e.CommandArgument.ToString());
        }

        if (e.CommandName == "Active")
        {
            Active(ID: e.CommandArgument.ToString());
        }
    }

    private void FillData(string ID)
    {
        dt = ObjConnection.select_data_dt("ManageOffLineServiceLink @Action='GetAll',@ID=" + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnServiceID.Value = ID;
            txtServiceName.Text = dt.Rows[0]["Name"].ToString();
            txtURL.Text = dt.Rows[0]["Link"].ToString();
            chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageOffLineServiceLink @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            GetService();
        }
    }

    private void Clear()
    {
        txtURL.Text = txtServiceName.Text = "";
        chkActive.Checked = false;
        hdnServiceID.Value = "0";

    }
}