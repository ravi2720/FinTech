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
using System.Xml;

public partial class NewRNFI : System.Web.UI.Page
{
    AEPSBankBodyRequest aEPSBankBodyRequest = new AEPSBankBodyRequest();

    protected void Page_Load(object sender, EventArgs e)
    {
        string bankList = GetBankList();
        Response.Write(bankList);
        JObject jData = JObject.Parse(bankList);

        DataTable dt = new DataTable();
        dt = GetJSONToDataTableUsingNewtonSoftDll(jData["banklist"]["data"].ToString());
        nationalbank.DataSource = dt;
        nationalbank.DataTextField = "bankName";
        nationalbank.DataValueField = "iinno";
        nationalbank.DataBind();
    }
    public DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }

    public string GetBankList()
    {
        JWTBankSendRequestNewIntegration jWTBankSendRequest = new JWTBankSendRequestNewIntegration();
        cls_connection objConnection = new cls_connection();


        string Result = GetCall("balance/balance/banklist", jWTBankSendRequest.GetToken());
        return Result;
    }
    public string GetCall(string url, string Token)
    {
        Response.Write(Token);

        var client = new RestClient("https://paysprint.in/service-api/api/v1/service/aeps/banklist/index");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Token", Token);
        //request.AddHeader("PARTNERID", "PS0033");

        IRestResponse response = client.Execute(request);

        return response.Content;
    }
}


