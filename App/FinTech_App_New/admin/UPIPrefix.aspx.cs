using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

public partial class Admin_UPIPrefix : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                fillDetails();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void fillDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Select * from UPIPREFIX");
        if (dt.Rows.Count > 0)
        {
            rptPreFixed.DataSource = dt;
            rptPreFixed.DataBind();
        }
        else
        {
            rptPreFixed.DataSource = null;
            rptPreFixed.DataBind();
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string URL = "https://partners.hypto.in/api/upi_prefixes";
        CreatePrefix ojCreatePrefix = new CreatePrefix();
        ojCreatePrefix.prefix = txtPreFix.Text.Trim();

        string result = ApiPostCall.PostCall(ojCreatePrefix.GetJson(), URL);
        JObject jData = JObject.Parse(result);
        if (jData["success"].ToString().ToLower() == "true")
        {
            cls.update_data("insert into UPIPREFIX(Name,Status,Response)values('" + jData["data"]["prefix"].ToString() + "','" + jData["data"]["status"].ToString() + "','" + result + "')");
            ErrorShow.AlertMessage(page, "UPI Prefix Created Successfully.", "Success");
        }
        else
        {
            ErrorShow.AlertMessage(page, jData["message"].ToString(), "Error");
        }
        fillDetails();
    }


    protected void rptPreFixed_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "CheckStatus")
        {
            string URL = "https://partners.hypto.in/api/upi_prefixes/" + e.CommandArgument.ToString().Trim();
            string result = ApiPostCall.GetHytpo(URL);
            JObject jData = JObject.Parse(result);
            if (jData["success"].ToString().ToLower() == "true")
            {
                cls.update_data("update UPIPREFIX set Status='" + jData["data"]["status"].ToString() + "' where NAME='" + jData["data"]["prefix"].ToString() + "'");
                ErrorShow.AlertMessage(page, "UPI Prefix is now in " + jData["data"]["status"].ToString() + " Mode", "Status Update");
            }
            else
            {
                ErrorShow.AlertMessage(page, jData["message"].ToString(), "Error");
            }
            fillDetails();
        }
    }

}