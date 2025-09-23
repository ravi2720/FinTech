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

public partial class Reatiler_HolidayCategory : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    cls_connection ObjData = new cls_connection();
    int Val = 0;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            Category();
            Country();


            if (Request.QueryString["ID"] != null)
            {
                FillData(Convert.ToInt16(Request.QueryString["ID"].ToString()));
            }

        }
    }



    private void Category()
    {
        DataTable dt = cls.select_data_dt("select * from ProductCategory where IsActive=1");
        if (dt.Rows.Count > 0)
        {
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", "0"));
        }
    }
    private void Country()
    {
        DataTable dt = cls.select_data_dt("select * from country where IsActive=1");
        if (dt.Rows.Count > 0)
        {
            ddlcountry.DataSource = dt;
            ddlcountry.DataTextField = "Name";
            ddlcountry.DataValueField = "ID";
            ddlcountry.DataBind();
            ddlcountry.Items.Insert(0, new ListItem("-- Select country --", "0"));
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
          


            string imgLarge, Front = "";
            string InputFile = string.Empty;
            InputFile = System.IO.Path.GetExtension(flieUpload.FileName);
            string gd = Guid.NewGuid().ToString() + InputFile;

            if (flieUpload.HasFile)
            {
                try
                {
                    flieUpload.PostedFile.SaveAs(MapPath("./images/") + gd);
                    Front = gd.ToString();
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



            //string imgLarge1, Front1 = "";
            //string InputFile1 = string.Empty;
            //InputFile1 = System.IO.Path.GetExtension(FileUpload1.FileName);
            //string gd1 = Guid.NewGuid().ToString() + InputFile1;

            //if (FileUpload1.HasFile)
            //{
            //    try
            //    {
            //        FileUpload1.PostedFile.SaveAs(MapPath("./images/") + gd1);
            //        Front1 = gd1.ToString();
            //    }
            //    catch (Exception ex)
            //    {
            //        Response.Write("Error occured: " + ex.Message.ToString());
            //    }
            //    finally
            //    {
            //        //imgLarge = string.Empty;
            //        //bmptImgLarge.Dispose();
            //        //fileUpload = new FileUpload();
            //    }
            //}




          
            //string base64 = Request.Form["imgCropped"];
            //byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
            //using (FileStream stream = new FileStream(Server.MapPath("~/Images/Cropped.png"), FileMode.Create))
            //{
            //    stream.Write(bytes, 0, bytes.Length);
            //    //stream.Flush();
            //}


            String path = HttpContext.Current.Server.MapPath("~/Images"); //Path

            //Check if directory exist
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
            }

            string imageName = Guid.NewGuid().ToString() + ".jpg";

            //set the image path
            string imgPath = Path.Combine(path, imageName);

            byte[] imageBytes = Convert.FromBase64String(Request.Form["imgCropped"].Split(',')[1]);

            File.WriteAllBytes(imgPath, imageBytes);

          



            if (radio1.Checked == true)
            {


                //int id = hdnid.Value == "0" ? 0 : Convert.ToInt16(hdnid.Value.ToString());
                int id = Convert.ToInt32((Request.QueryString["ID"] == null ? "0" : Request.QueryString["ID"].ToString()));
                DataTable dt = cls.select_data_dt("Exec AddEditPRODUCT " + id + ",'" + ddlCategory.SelectedValue + "','" + txtProductName.Text + "','','" + ckNewsDesc.Text + "','" + ckDescShort.Text + "','" + txtPrice.Text + "','" + txtQuantity.Text + "','" + txtDiscount.Text + "','" + chkFlate.Checked + "','" + txtGST.Text + "','" + txtHSN.Text + "','" + txtsku.Text + "','" + txtBarcode.Text + "','" + ddlcountry.SelectedValue + "','" + txtWeight.Text + "','" + txtdayreturn.Text + "','" + txtLabels.Text + "','" + txtvideos.Text + "',0,1");

                if (dt.Rows[0]["Status"].ToString() == "1")

                {

                    ErrorShow.AlertMessage(page, dt.Rows[0]["Result"].ToString(), ConstantsData.CompanyName);
                    Clear();


                }
                else
                {
                    ErrorShow.AlertMessage(page, dt.Rows[0]["Result"].ToString(), ConstantsData.CompanyName);
                }


            }


            else
            {
                int id = Convert.ToInt32((Request.QueryString["ID"] == null ? "0" : Request.QueryString["ID"].ToString()));
                DataTable dt = cls.select_data_dt("Exec AddEditPRODUCT " + id + ",'" + ddlCategory.SelectedValue + "','" + txtProductName.Text + "','','" + ckNewsDesc.Text + "','" + ckDescShort.Text + "','" + txtPrice.Text + "','" + txtQuantity.Text + "','" + txtDiscount.Text + "','" + chkFlate.Checked + "','" + txtGST.Text + "','" + txtHSN.Text + "','" + txtsku.Text + "','" + txtBarcode.Text + "','" + ddlcountry.SelectedValue + "','" + txtWeight.Text + "','" + txtdayreturn.Text + "','" + txtLabels.Text + "','"+ imageName + "',0,1");

                if (dt.Rows[0]["Status"].ToString() == "1")

                {

                    ErrorShow.AlertMessage(page, dt.Rows[0]["Result"].ToString(),ConstantsData.CompanyName);
                    Clear();


                }
                else
                {
                    ErrorShow.AlertMessage(page, dt.Rows[0]["Result"].ToString(), ConstantsData.CompanyName);
                }


            }





        }
        catch (Exception ex)
        {
            ErrorShow.Error(page1: page, Message: "Something went wrong");

        }
       
    }

    //private Bitmap Resize_Image(Stream streamImage, int maxWidth, int maxHeight)
    //{
    //    Bitmap originalImage = new Bitmap(streamImage);
    //    int newWidth = originalImage.Width;
    //    int newHeight = originalImage.Height;
    //    double aspectRatio = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height);

    //    newWidth = maxWidth;
    //    newHeight = maxHeight;
    //    return new Bitmap(originalImage, newWidth, newHeight);
    //}
    //protected void FillData()
    //{
    //    dt = cls.select_data_dt("EXEC ManageProduct 'Get','" + 0 + "' ");
    //    if (dt.Rows.Count > 0)
    //    {
    //        repeater1.DataSource = dt;
    //        repeater1.DataBind();
    //    }
    //    else
    //    {
    //        repeater1.DataSource = null;
    //        repeater1.DataBind();

    //    }
    //}

    //protected void repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    if (e.CommandName == "Edit")
    //    {
    //        hdnid.Value = e.CommandArgument.ToString();
    //        FillNews(id: Convert.ToInt16(e.CommandArgument.ToString()));
    //    }
    //    if (e.CommandName == "Active")
    //    {
    //        Active(id: Convert.ToInt16(e.CommandArgument.ToString()));
    //    }
    //}

    private void FillData(int id)
    {
        dt = cls.select_data_dt("EXEC ManageProduct 'Get','" + id + "'");
        txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
        ckNewsDesc.Text = dt.Rows[0]["Description"].ToString();
        ckDescShort.Text = dt.Rows[0]["ShortDescription"].ToString();
        txtPrice.Text = dt.Rows[0]["Price"].ToString();
        txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
        txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
        txtGST.Text = dt.Rows[0]["GST"].ToString();
        ddlCategory.SelectedValue = dt.Rows[0]["CategoryID"].ToString();
        txtHSN.Text = dt.Rows[0]["HSNCode"].ToString();
        txtsku.Text = dt.Rows[0]["SKU"].ToString();
        txtBarcode.Text = dt.Rows[0]["Barcode"].ToString();
        txtWeight.Text = dt.Rows[0]["Weight"].ToString();
        txtdayreturn.Text = dt.Rows[0]["DaysReturn"].ToString();
        txtLabels.Text = dt.Rows[0]["Labels"].ToString();
        txtvideos.Text = dt.Rows[0]["ImageType"].ToString();
        ddlcountry.SelectedValue = dt.Rows[0]["CountryID"].ToString();      
        //Image1.Src = "/images/"+ dt.Rows[0]["ImageType"].ToString(); 
    }


    //private void Active(int id)
    //{
    //    int val = cls.update_data("ManageProduct @Action='IsActive',@ID=" + id + "");
    //    if (val > 0)
    //    {
    //        FillData();
    //    }

    //}

    private void Clear()
    {
       txtProductName.Text = "";
       ckNewsDesc.Text = ckDescShort.Text= txtPrice.Text= txtGST.Text= txtDiscount.Text = txtQuantity.Text = txtHSN.Text = txtsku.Text = txtBarcode.Text = txtWeight.Text = txtdayreturn.Text = txtLabels.Text = txtvideos.Text=  "";

    }

    protected void radio1_CheckedChanged(object sender, EventArgs e)
    {
        if (radio1.Checked == true)
        {
            divvideo.Visible = true;
            divImage.Visible = false;
        }
    }

    protected void radio2_CheckedChanged(object sender, EventArgs e)
    {
         if (radio2.Checked == true)
        {
            divImage.Visible = true;
            divvideo.Visible = false;
        }

    }

}