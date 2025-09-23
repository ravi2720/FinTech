using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for mswiftRequest
/// </summary>
public class mswiftRequest
{
    public mswiftRequest()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}

public class BillFetchRequest
{
    public string APIID { get; set; }

    public string Token { get; set; }
    public string MethodName { get; set; }
    public string ClientRefId { get; set; }
    public string Number { get; set; }
    public string SPKey { get; set; }
    public string TelecomCircleID { get; set; }
    public string Optional1 { get; set; }
    public string Optional2 { get; set; }
    public string Optional3 { get; set; }
    public string Optional4 { get; set; }
    public string Optional5 { get; set; }
    public string Optional6 { get; set; }
    public string Optional7 { get; set; }
    public string Optional8 { get; set; }
    public string Optional9 { get; set; }

    public string GetJson()
    {
        BillFetchRequest ObjData = new BillFetchRequest();
        ObjData.ClientRefId = this.ClientRefId;
        ObjData.Number = this.Number;
        ObjData.SPKey = this.SPKey;
        ObjData.TelecomCircleID = this.TelecomCircleID;
        ObjData.Optional1 = this.Optional1;
        ObjData.Optional2 = this.Optional2;
        ObjData.Optional3 = this.Optional3;
        ObjData.Optional4 = this.Optional4;
        ObjData.Optional5 = this.Optional5;
        ObjData.Optional6 = this.Optional6;
        ObjData.Optional7 = this.Optional7;
        ObjData.Optional8 = this.Optional8;
        ObjData.Optional9 = this.Optional9;
        ObjData.APIID = this.APIID;
        ObjData.Token = this.Token;
        ObjData.MethodName = this.MethodName;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }

}


public class BillPayRequest
{
    public string APIID { get; set; }

    public string Token { get; set; }
    public string MethodName { get; set; }
    public string ClientRefId { get; set; }
    public string Number { get; set; }
    public string SPKey { get; set; }
    public string Amount { get; set; }
    public string TelecomCircleID { get; set; }
    public string Optional1 { get; set; }
    public string Optional2 { get; set; }
    public string Optional3 { get; set; }
    public string Optional4 { get; set; }
    public string Optional5 { get; set; }
    public string Optional6 { get; set; }
    public string Optional7 { get; set; }
    public string Optional8 { get; set; }
    public string Optional9 { get; set; }

    public BillPayRequest()
    {
        Optional1 = "";
        Optional2 = "";
        Optional3 = "";
        Optional4 = "";
        Optional5 = "";
        Optional6 = "";
        Optional7 = "";
        Optional8 = "";
        Optional9 = "";
    }

    public string GetJson()
    {
        BillPayRequest ObjData = new BillPayRequest();
        ObjData.ClientRefId = this.ClientRefId;
        ObjData.Number = this.Number;
        ObjData.SPKey = this.SPKey;
        ObjData.Amount = this.Amount;
        ObjData.TelecomCircleID = this.TelecomCircleID;
        ObjData.Optional1 = this.Optional1;
        ObjData.Optional2 = this.Optional2;
        ObjData.Optional3 = this.Optional3;
        ObjData.Optional4 = this.Optional4;
        ObjData.Optional5 = this.Optional5;
        ObjData.Optional6 = this.Optional6;
        ObjData.Optional7 = this.Optional7;
        ObjData.Optional8 = this.Optional8;
        ObjData.Optional9 = this.Optional9;
        ObjData.APIID = this.APIID;
        ObjData.Token = this.Token;
        ObjData.MethodName = this.MethodName;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }

}


public class BillPayResult
{
    public string ResponseCode { get; set; }
    public string ResponseMessage { get; set; }
    public string TransactionId { get; set; }
    public string AvailableBalance { get; set; }
    public string ClientRefId { get; set; }
    public object OperatorTransactionId { get; set; }
}

public class BillFetchResult
{
    public string ResponseCode { get; set; }
    public string ResponseMessage { get; set; }
    public double dueamount { get; set; }
    public string duedate { get; set; }
    public string customername { get; set; }
    public string billnumber { get; set; }
    public string billdate { get; set; }
    public string acceptPartPay { get; set; }
    public string BBPSCharges { get; set; }
    public string BillUpdate { get; set; }
    public string RequestID { get; set; }
    public string ClientRefId { get; set; }
}


public class PostGetCallMSWIFT
{
    public string PostCall(string Data)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient(SONITechnoCredentrial.BBPS);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("JWTKey", "65547E7A5D6471042978830DC59C1DC25A261A65D721CEF9FCDE40303890C574");
        request.AddHeader("APIID", "API70001");
        //request.AddHeader("ClientID", "1000000031");
        //request.AddHeader("SecretKey", "ERTERTG!@#$242");
        //request.AddHeader("TokenID", "IDSH#124%342");
        request.AddParameter("application/json", Data, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }
    public string PostCallPay(string Data)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        //var client = new RestClient("https://mswift.quicksekure.com/api/BBPS/BBPSPayment");
        //client.Timeout = -1;
        //var request = new RestRequest(Method.POST);
        //request.AddHeader("Content-Type", "application/json");
        //request.AddHeader("JWTKey", "65547E7A5D6471042978830DC59C1DC25A261A65D721CEF9FCDE40303890C574");
        //request.AddHeader("APIID", "API70001");
        //request.AddHeader("ClientID", "1000000031");
        //request.AddHeader("SecretKey", "ERTERTG!@#$242");
        //request.AddHeader("TokenID", "IDSH#124%342");
        //request.AddParameter("application/json", Data, ParameterType.RequestBody);
        //IRestResponse response = client.Execute(request);
        //return response.Content;
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        var client = new RestClient(SONITechnoCredentrial.BBPS);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("JWTKey", "65547E7A5D6471042978830DC59C1DC25A261A65D721CEF9FCDE40303890C574");
        request.AddHeader("APIID", "API70001");
        //request.AddHeader("ClientID", "1000000031");
        //request.AddHeader("SecretKey", "ERTERTG!@#$242");
        //request.AddHeader("TokenID", "IDSH#124%342");
        request.AddParameter("application/json",Data, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }
}