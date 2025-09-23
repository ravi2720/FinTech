using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class API_GetIP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.ContentType = "application/json";
            Response.Write("{\"IP\":\""+ GetIPAddress() + "\"}");
        }
    }

    public string GetIPAddress()
    {
        string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipAddress;
    }
}