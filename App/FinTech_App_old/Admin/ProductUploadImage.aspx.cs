using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ProductUploadImage : System.Web.UI.Page
{
    cls_connection Objconnection = new cls_connection();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Request.QueryString["PID"] != null)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }
    }


    private void BindData()
    {
        DataTable dt = Objconnection.select_data_dt("select * from ProductImage where PID='" + Request.QueryString["PID"] + "'");
        rptData.DataSource = dt;
        rptData.DataBind();
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "1")
        {
            ddlType.Attributes.Add("accept", ".png,.jpg,.jpeg");
        }
        else
        {
            ddlType.Attributes.Add("accept", "video/mp4,video/x-m4v,video/*");
        }
    }

    protected void btnUplaod_Click(object sender, EventArgs e)
    {
        if (flieUpload.HasFile)
        {
            string imgLarge, Front = "";
            string InputFile = string.Empty;
            string gd = "";

            InputFile = System.IO.Path.GetExtension(flieUpload.FileName);
            gd = Guid.NewGuid().ToString() + InputFile;
            flieUpload.PostedFile.SaveAs(MapPath("./images/") + gd);
            Int32 Val = 0;
            Val = Objconnection.update_data("AddEditProductImage 0,'"+Request.QueryString["PID"].ToString()+"','"+ddlType.SelectedValue+"','"+gd+"','"+ chkActive.Checked + "'");
            if (Val > 0)
            {
                BindData();
               
                ErrorShow.AlertMessage(page, "Upload Successfully", ConstantsData.CompanyName);
            }

        }
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName== "Delete")
        {
            Int32 Val = 0;
            DataTable dt = Objconnection.select_data_dt("select * from ProductImage where id=" + e.CommandArgument.ToString() + "");
            Val = Objconnection.update_data("delete from ProductImage where id='"+e.CommandArgument.ToString()+"'");
            if (Val > 0)
            {
                try
                {
                    string path = Server.MapPath("./images/" + dt.Rows[0]["URL"].ToString());
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not  
                    {
                        file.Delete();
                    }
                }catch(Exception ex)
                {

                }
                BindData();
               
                ErrorShow.AlertMessage(page, "Delete Successfully", ConstantsData.CompanyName);
            }
        }
    }
}