using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_MHRegisterMemberList : System.Web.UI.Page
{
    clsMahagram ObjMahagram = new clsMahagram();
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                fillTransactionDetails();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void fillTransactionDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("PROC_MahagramRegister_Report '',0,'" + txtfromdate.Text + "','" + txttodate.Text + "'");

        if (dt.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();
        }
        else
        {
            gvTransactionHistory.DataSource = null;
            gvTransactionHistory.DataBind();
        }

    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }
    public static string CheckRequestIP()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }
        return context.Request.ServerVariables["REMOTE_ADDR"];
    }
    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string requestIP = CheckRequestIP();
        using (var client = new WebClient())
        {
            DataTable dtBCRegistrationId = cls.select_data_dt("select * from member where BCRegistrationID='" + e.CommandArgument.ToString() + "'");
            var Values = new NameValueCollection();
            Values["bc_id"] = e.CommandArgument.ToString();
            Values["phone1"] = dtBCRegistrationId.Rows[0]["Mobile"].ToString();
            Values["ip"] = requestIP;
            Values["userid"] = dtBCRegistrationId.Rows[0]["BCRegistrationId"].ToString();
            Values["saltkey"] = "MH82#E@IN038#F6230UQ3";
            Values["secretkey"] = "MH82#E@IN038#F6230UQ3";


            var responseString = ObjMahagram.BCInitiate(Values);
            JArray ObjData = JArray.Parse(responseString);


            if (ObjData[0]["Message"].ToString().ToLower() == "success")
            {
                string str = "https://icici.bankmitra.org/Location.aspx?text=" + ObjData[0]["Result"].ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "window.open('" + str + "');", true);
                //Response.Redirect(str);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('" + ObjData[0]["Message"].ToString() + "');", true);
            }

        }
    }
}