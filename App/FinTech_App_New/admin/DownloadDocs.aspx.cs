using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DownloadDocs : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    Page page;
    int Val = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        if (!IsPostBack)
        {
            GetdownloadDoc();
        }


    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        {
            string InputFile = string.Empty, logo = "";
            if (fileUpload.HasFile)
            {
                string InputFilebankSTatement = System.IO.Path.GetExtension(fileUpload.FileName);
                {
                    string gdbankstatement = Guid.NewGuid().ToString() + InputFilebankSTatement;

                    fileUpload.PostedFile.SaveAs(MapPath("./images/Notification/") + gdbankstatement);
                    logo = gdbankstatement.ToString();
                }
            }

            ObjConnection.update_data("AddEditDocumentDownLoad '" + hdnid.Value + "','" + txtNewsName.Text + "','http://dnft.co.in/images/Notification/" + logo + "','" + txtLinkName.Text + "'");
            ErrorShow.AlertMessage(page, "Update Successfully", "Success");

        }
    }

    public void GetdownloadDoc()
    {
        try
        {
            dt = ObjConnection.select_data_dt("exec ManageDocumentDownLoad 'GetActive',0");
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
            ErrorShow.AlertMessage(page, ex.Message, "Error");
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
            ObjConnection.update_data("ManageDocumentDownLoad @Action='IsDelete',@ID=" + e.CommandArgument.ToString() + "");
            GetdownloadDoc();
        }
    }

    private void FillData(string ID)
    {
        dt = ObjConnection.select_data_dt("ManageDocumentDownLoad @Action='GetAll',@ID=" + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnid.Value = ID;
            txtNewsName.Text = dt.Rows[0]["Header"].ToString();
            txtLinkName.Text = dt.Rows[0]["Link"].ToString();
            

        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("DocumentDownLoad @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            GetdownloadDoc();
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
}