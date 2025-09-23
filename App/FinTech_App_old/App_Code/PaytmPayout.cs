using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Paytm;
using Newtonsoft.Json;
using System.Text;
using System.IO;
/// <summary>
/// Summary description for PaytmPayout
/// </summary>
public  class PaytmPayout
{
    public static string mid = "ff";
    public static string MerchantKey = "5hAOO!lByVmGkffdW#";
    public static string SalesWalletGuid = "017685f8-23fff3b-4799-aa7b-c68e80253771";


}

public class PaytmPayoutRequest
{
    public string subwalletGuid { get; set; }
    public string orderId { get; set; }
    public string beneficiaryPhoneNo { get; set; }
    public string amount { get; set; }
    public string callbackUrl { get; set; }

    public PaytmPayoutRequest()
    {
        subwalletGuid = PaytmPayout.SalesWalletGuid;
        callbackUrl = "https://idspay.in/PaytmPayoutCallBack.ashx";
    }

    public string GetJson()
    {
        PaytmPayoutRequest ObjData = new PaytmPayoutRequest();
        ObjData.orderId = this.orderId;
        ObjData.subwalletGuid = this.subwalletGuid;
        ObjData.amount = this.amount;
        ObjData.beneficiaryPhoneNo = this.beneficiaryPhoneNo;
        ObjData.beneficiaryPhoneNo = this.beneficiaryPhoneNo;

        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }
}


public class PayoutPaytmStatusCheckRequest
{
    public string orderId { get; set; }
    public string GetJson()
    {
        PayoutPaytmStatusCheckRequest ObjData = new PayoutPaytmStatusCheckRequest();
        ObjData.orderId = this.orderId;
       
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }
}

public class PayoutPaytmCheckStatusResult
{
    public string mid { get; set; }
    public string orderId { get; set; }
    public string paytmOrderId { get; set; }
    public string amount { get; set; }
    public string commissionAmount { get; set; }
    public string tax { get; set; }
    public object rrn { get; set; }
}

public class PayoutPaytmResultCheckStatusRoot
{
    public string status { get; set; }
    public string statusCode { get; set; }
    public string statusMessage { get; set; }
    public PayoutPaytmCheckStatusResult result { get; set; }
}

public class ApiPostCallPaytmPayout
{
    
    public static string PostCall(string Json, string url)
    {
        var baseAddress = url;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        string checksum = Paytm.Checksum.generateSignature(Json,PaytmPayout.MerchantKey);        
        var client = new RestClient(url);
        var request = new RestRequest();

        request.Method = Method.POST;
        request.AddHeader("Accept", "application/json");
        request.AddHeader("x-mid", PaytmPayout.mid);
        request.AddHeader("x-checksum", checksum);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);

        var response = client.Execute(request);
        return response.Content.ToString();



    }

    public static string API_Call_POST(string url, string inputParameter)
    {
        try
        {
            var baseAddress = url;
            string checksum = Paytm.Checksum.generateSignature(inputParameter, PaytmPayout.MerchantKey);
            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
            http.Headers.Add("x-mid", "IDSHAA15850228307048");
            http.Headers.Add("x-checksum", checksum);
            string myJsonString1 = "checksum_" + checksum;
          
            string parsedContent = inputParameter;
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] bytes = encoding.GetBytes(parsedContent);

            Stream newStream = http.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();

            var response = http.GetResponse();

            var stream = response.GetResponseStream();
            var sr = new StreamReader(stream);
            var content = sr.ReadToEnd();


            return content;
        }
        catch (Exception ex)
        {
            return "";
        }

    }

}