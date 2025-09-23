using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AEPSBalanceReport : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public static string value = "";
    public static int i = 0;
    DataTable dtPanAPIDetail = new DataTable();
    public static string PANURL = "https://cyrusrecharge.in/api/API_PAN.aspx";
    public static string MerchantID = "";
    public static string MerchantKey = "";
    public static string PSAID = "";
    protected void Page_Load(object sender, EventArgs e)
    {       
        if (!IsPostBack)
        {
            BindRepeaterValues();
        }

    }    
    public void BindRepeaterValues()
    {
        try
        {

            DataTable dtPANDetails = new DataTable();
            dtPANDetails = cls.select_data_dt("proc_aeps_balance_report");
            if (dtPANDetails.Rows.Count > 0)
            {
                gvtData.DataSource = dtPANDetails;
                gvtData.DataBind();
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alertify.error('" + ex.Message.ToString() + "');", true);
        }
    }



}
