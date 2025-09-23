﻿<%@ WebHandler Language="C#" Class="FileUploadHandler" %>

using System;
using System.Web;

public class FileUploadHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context)  
    {    
        if (context.Request.Files.Count > 0)    
        {    
            HttpFileCollection files = context.Request.Files;    
            for (int i = 0; i < files.Count; i++)    
            {    
                HttpPostedFile file = files[i];    
                string fname = context.Server.MapPath("~/images/" + file.FileName);    
                file.SaveAs(fname);    
            }    
               
            context.Response.Write("File Uploaded Successfully!");    
        }    
     
    }    
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}