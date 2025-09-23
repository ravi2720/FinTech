using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ProfitReport : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    public ServiceTotal ServiceTotal;
    public ServiceTotalSelf ServiceTotalSelf;
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    cls_connection Objconnection = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                txtfromdate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                fillTransactionDetails();
                FillServiceData();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void FillServiceData()
    {
        DataTable dt = new DataTable();
        dt = Objconnection.select_data_dt("ServiceTotalMemberWise '" + txtfromdate.Text + "'");
        ServiceTotalSelf = ServiceTotalSelf.GetServiceTotalInfo(dt);
    }

    private void fillTransactionDetails()
    {
        STProfitReportRequest sTProfitReportRequest = new STProfitReportRequest();
        sTProfitReportRequest.MethodName = "report";
        sTProfitReportRequest.ToDayDate = txtfromdate.Text;
        string Result = ApiPostCall.PostCall(sTProfitReportRequest.GetJson(), SONITechnoCredentrial.ProfitURL);
        txtresult.Text = Result;
         JObject Data = JObject.Parse(Result);
        if (Data["code"].ToString().ToUpper() != "ERR")
        {
            ServiceTotal = JsonConvert.DeserializeObject<ServiceTotal>(Data["data"].ToString());
        }
        else
        {
            ServiceTotal = ServiceTotal.GetObject();
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
        FillServiceData();
    }
}