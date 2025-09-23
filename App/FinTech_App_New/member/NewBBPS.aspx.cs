using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Service_NewBBPS : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();
    DataTable dtMember = new DataTable();
    int Val = 0;
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            page = HttpContext.Current.CurrentHandler as Page;
            if (!IsPostBack)
            {
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
}