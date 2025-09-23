using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json.Linq;
using System.Web.UI.WebControls;
using System.IO;


public partial class CBAPI_bbpsCallback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string sRequest = string.Empty;
            using (var reader = new StreamReader(Request.InputStream))
            {
                sRequest = reader.ReadToEnd();
            }
            if (!string.IsNullOrEmpty(sRequest))
            {
                JObject Data = JObject.Parse(sRequest);
                if (Data["TransactionStatus"].ToString().ToUpper() == "SUCCESS")
                {
					
       Response.ContentType="application/json";             Response.Write("{\"ResponseCode\":\"000\",\"status\":\"Success\"}");
                }
                else if (Data["TransactionStatus"].ToString().ToUpper() == "FAIL")
                {
					
                    Response.Write("{\"ResponseCode\":\"999”\",\"status\":\"Failed\"}");
                }
                else if (Data["TransactionStatus"].ToString().ToUpper() == "PENDING")
                {
					Response.ContentType="application/json";
                    Response.Write("{\"ResponseCode\":\"000\",\"status\":\"Pending\"}");
                }
            }
        }
    }
}