using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_UploadDownLineKYC : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtSession;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        dtSession = (DataTable)Session["dtEmployee"];
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {


            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void FillData(int msrno)
    {
        rptData.DataSource = cls.select_data_dt("select * from MEMBERKYCDOCUMENTS where msrno = " + msrno + " ");
        rptData.DataBind();
    }

    private void FillDocument()
    {
        DataTable dtMember = cls.select_data_dt("select * from Member where  loginid='" + txtMemberID.Text.Trim() + "'");
        List<ListItem> deletedItems = new List<ListItem>();
        DataTable dt = cls.select_data_dt("select DocID from MEMBERKYCDOCUMENTS where Msrno = '" + Convert.ToInt16(dtMember.Rows[0]["Msrno"]) + "' and status <> 'Rejected'");
        ddlDocument.DataSource = cls.select_data_dt("exec PROC_MANAGEKYCDOCUMENTS 0,'GetAll'");
        ddlDocument.DataTextField = "Name";
        ddlDocument.DataValueField = "ID";
        ddlDocument.DataBind();
        ddlDocument.Items.Insert(0, new ListItem("-- Select Document --", "0"));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ddlDocument.Items.Remove(ddlDocument.Items.FindByValue(dt.Rows[i]["DocID"].ToString()));
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        DataTable dtMember = cls.select_data_dt("select * from Member where loginid='" + txtMemberID.Text.Trim() + "'");

        string imgFront, Front = "";
        Bitmap bmFront = null;
        string gdFront = Guid.NewGuid().ToString() + ".jpeg";

        string imgBack, Back = "";
        Bitmap bmBack = null;
        string gdBack = Guid.NewGuid().ToString() + ".jpeg";
        if (fuDoc.HasFile && Convert.ToInt16(hdnSide.Value) == 2 ? fuDocBack.HasFile : !fuDocBack.HasFile)
        {
            try
            {
                bmFront = Resize_Image(fuDoc.PostedFile.InputStream, 1200, 1200);
                imgFront = Server.MapPath("./images/KYC/") + gdFront;
                Front = gdFront.ToString();
                bmFront.Save(imgFront, ImageFormat.Jpeg);
                if (Convert.ToInt16(hdnSide.Value) == 2)
                {
                    bmBack = Resize_Image(fuDocBack.PostedFile.InputStream, 1200, 1200);
                    imgBack = Server.MapPath("./images/KYC/") + gdBack;
                    Back = gdBack.ToString();
                    bmBack.Save(imgBack, ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error occured: " + ex.Message.ToString());
            }
            finally
            {
                imgFront = string.Empty;
                // bmFront.Dispose();
                fuDoc = new FileUpload();
                if (Convert.ToInt16(hdnSide.Value) == 2)
                {
                    imgBack = string.Empty;
                    // bmBack.Dispose();
                    fuDocBack = new FileUpload();
                }
            }
            try
            {
                DataTable dtSave = cls.select_data_dt("Exec Proc_AddEditMemberKYCDocument 0,'" + Convert.ToInt16(dtMember.Rows[0]["Msrno"]) + "','" + Convert.ToInt16(ddlDocument.SelectedValue) + "','" + txtDocNumber.Text.Trim() + "','" + Front + "','" + Back + "'");
                if (Convert.ToInt16(dtSave.Rows[0]["ID"]) > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Inserted Successfully ..!!');", true);
                    //ErrorShow.Success(page, "");
                    FillDocument();
                    //FillData(msrno: Convert.ToInt16(dtMember.Rows[0]["Msrno"]));
                    Clear();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error occured: " + ex.Message.ToString());
            }
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
        txtDocNumber.Text = string.Empty;
    }

    protected void ddlDocument_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtMember = cls.select_data_dt("select * from Member where  loginid='" + txtMemberID.Text.Trim() + "'");
        if (Convert.ToInt16(ddlDocument.SelectedValue) > 0)
        {
            DataTable dtcheck = cls.select_data_dt("select * from MEMBERKYCDOCUMENTS where Docid=" + Convert.ToInt16(ddlDocument.SelectedValue) + " and Msrno = " + Convert.ToInt16(dtMember.Rows[0]["Msrno"]) + " and Status <> 'Rejected'");
            if (dtcheck.Rows.Count == 0)
            {
                int side = cls.select_data_scalar_int("select side from KYCDocumentsMaster where id = " + Convert.ToInt16(ddlDocument.SelectedValue) + "");
                if (side > 0)
                {
                    hdnSide.Value = side.ToString();
                    if (side > 1)
                    {
                        fuDocBack.Visible = true;
                        RequiredFieldValidator3.Visible = true;
                        fuDocBack.Enabled = true;
                        RequiredFieldValidator3.Enabled = true;
                    }
                    else
                    {
                        fuDocBack.Visible = false;
                        RequiredFieldValidator3.Visible = false;
                        fuDocBack.Enabled = false;
                        RequiredFieldValidator3.Enabled = false;
                    }
                    fuDoc.Enabled = true;
                    txtDocNumber.Enabled = true;
                    btnSave.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something Went Wrong, try Again ..!!');window.location.replace('dashboard.aspx')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ddlDocument.SelectedItem.Text.ToString() + "' Already Uploaded ..!!');", true);
            }

        }
        else
        {
            fuDoc.Enabled = false;
            txtDocNumber.Enabled = false;
            btnSave.Visible = false;
            fuDocBack.Visible = false;
            RequiredFieldValidator3.Visible = false;
            fuDocBack.Enabled = false;
            RequiredFieldValidator3.Enabled = false;
        }
    }

    protected void btnSearchMember_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
      
        dt = cls.select_data_dt("select * from Member where  loginid='" + txtMemberID.Text.Trim() + "'");
        if (dt.Rows.Count > 0)
        {
            manBody.Visible = true;
            FillDocument();
            FillData(msrno: Convert.ToInt16(dt.Rows[0]["Msrno"]));
        }
        else
        {
            manBody.Visible = false;
            ErrorShow.AlertMessage(page, "You Can Upload Downline KYC", "Information");
        }

    }
}