<%@ WebHandler Language="C#" Class="fileUploader" %>

using System;
using System.Web;
using System.IO;
using System.Data;
     using System.Web.SessionState;
public class fileUploader : IHttpHandler, IRequiresSessionState
{
    cls_connection cls = new cls_connection();
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        try
        {
            string dirFullPath = HttpContext.Current.Server.MapPath("~/images/icon/");
            string[] files;
            int numFiles;
            files = System.IO.Directory.GetFiles(dirFullPath);
            numFiles = files.Length;
            numFiles = numFiles + 1;
            string str_image = "";

            foreach (string s in context.Request.Files)
            {
                HttpPostedFile file = context.Request.Files[s];
                string fileName = file.FileName;
                string fileExtension = file.ContentType;

                if (!string.IsNullOrEmpty(fileName))
                {
                    fileExtension = Path.GetExtension(fileName);
                    str_image = System.Guid.NewGuid().ToString().Replace("-","") + fileExtension;
                    string pathToSave_100 = HttpContext.Current.Server.MapPath("~/images/icon/") + str_image;
                    file.SaveAs(pathToSave_100);
                    DataTable dtMember = (DataTable)context.Session["dtMember"];
                    cls.update_data("update member set Pic='"+str_image+"' where id="+dtMember.Rows[0]["Msrno"].ToString()+"");
                }
            }
            //  database record update logic here  ()

            context.Response.Write(str_image);
        }
        catch (Exception ac)
        {

        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}