using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for bankopen
/// </summary>
public class bankopen
{
    public static string Authorization = "";
    public static string URL = "";
    public static string debit_account_number = "";

}

public class BankOpenPostCall
{
    public static string PostCall(string Mstr, NameValueCollection Data)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient(bankopen.URL+ Mstr);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Authorization", bankopen.Authorization);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Accept", "application/json");
        foreach (string key in Data)
        {
            request.AddParameter(key, Data[key]);
        }
        IRestResponse response = client.Execute(request);
        return response.Content;
    }
}