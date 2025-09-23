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
           

            if (Request.QueryString["ID"] != null)
            {
                FillData(Convert.ToInt16(Request.QueryString["ID"].ToString()));
            }

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        // Get current page instance safely
        Page page = HttpContext.Current.CurrentHandler as Page;

        try
        {

            //string imgFront, Front = "";
            //Bitmap bmFront = null;
            //string gdFront = Guid.NewGuid().ToString() + ".jpeg";
            //if (flieUpload.HasFile)
            //{
            //    bmFront = Resize_Image(flieUpload.PostedFile.InputStream, 1200, 1200);
            //    imgFront = Server.MapPath("./images/") + gdFront;
            //    Front = gdFront.ToString();
            //    bmFront.Save(imgFront, ImageFormat.Jpeg);

            //}


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

            int id = Convert.ToInt32((Request.QueryString["ID"] == null ? "0" : Request.QueryString["ID"].ToString()));
            DataTable dt = cls.select_data_dt("Exec AddEditProductFunction " + id + ",'" + txtProductFunction.Text + "','" + Front + "'");

            if (dt.Rows[0]["Status"].ToString() == "1")
            {
                ErrorShow.AlertMessage(page, dt.Rows[0]["Result"].ToString(), "Success");
                Clear();
            }
            else
            {
                ErrorShow.AlertMessage(page, dt.Rows[0]["Result"].ToString(), "Error");
            }
        }
        catch (Exception ex)
        {
            ErrorShow.Error(page, "Something went wrong");
        }
    }

    private void FillData(int id)
    {
        dt = cls.select_data_dt("EXEC ManageProductFunction 'Get','" + id + "'");
        txtProductFunction.Text = dt.Rows[0]["Name"].ToString();
        
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
      
       txtProductFunction.Text =   "";

    }
}