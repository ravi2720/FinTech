using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Configuration;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using RestSharp;
using System.ComponentModel.DataAnnotations;

public partial class API_MLM : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                NameValueCollection nvc = Request.Form;
                if (nvc["Request"] != null)
                {
                    JObject jObject = JObject.Parse(nvc["Request"]);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}