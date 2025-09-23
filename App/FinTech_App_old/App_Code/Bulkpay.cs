using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for Bulkpay
/// </summary>
public class Bulkpay
{
    public static string Authorization = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjYyZDdkYjgyN2I2ZjNjYmEzZmJmYWU0YiIsInR5cGUiOiJBUEkiLCJpYXQiOjE2NTg0NzI2ODh9.afIm3fKugoD-l6plbDZGzsK3Jo3QL30qHWNqxHihF4I";
    public static string URL = "https://api.bulkpe.in/client/";
    public Bulkpay()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}

public class BulkpayPayout
{
    public double amount { get; set; }
    public string account_number { get; set; }
    public string payment_mode { get; set; }
    public string reference_id { get; set; }
    public string transcation_note { get; set; }
    public string beneficiaryName { get; set; }
    public string ifsc { get; set; }

    public string GetJson()
    {
        BulkpayPayout ObjData = new BulkpayPayout();
        ObjData.amount = this.amount;
        ObjData.account_number = this.account_number;
        ObjData.payment_mode = this.payment_mode;
        ObjData.reference_id = this.reference_id;
        ObjData.transcation_note = this.transcation_note;
        ObjData.beneficiaryName = this.beneficiaryName;
        ObjData.ifsc = this.ifsc;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }

}


public class BulkApiPostCall
{

    public static string PostCall(string Json, string url)
    {
        var baseAddress = Bulkpay.URL + url;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        var client = new RestClient(baseAddress);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", Bulkpay.Authorization);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content.ToString();
    }
}
