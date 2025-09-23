using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_AccountStatement : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("dd-MM-yyyy");

              //  fillTransactionDetails();
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
        ICICIPayoutRequest iCICIPayoutRequest = new ICICIPayoutRequest();
        iCICIPayoutRequest.FROMDATE = txtfromdate.Text;
        iCICIPayoutRequest.TODATE = txttodate.Text;

        string Result = AccountStatement.GetAccountStateMent(iCICIPayoutRequest);
        //{"MESSAGE":"The transactions do not exist for the account with the entered criteria.","RESPONSE":"Failure"}
        cls.update_data("insert into tbl_Apeslog(url)values('" + Result + "')");
        dt = GetJSONToDataTableUsingNewtonSoftDll(JObject.Parse(Result)["Record"].ToString());
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

    public DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }
}