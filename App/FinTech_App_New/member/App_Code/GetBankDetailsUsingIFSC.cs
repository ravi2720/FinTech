﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for GetBankDetailsUsingIFSC
/// </summary>
public class GetBankDetailsUsingIFSC
{
    public static string GetBankDetails(string IFSC)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient("https://ifsc.razorpay.com/"+ IFSC);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

}

public class BankDetailsUsingIFSC
{
    public string BRANCH { get; set; }
    public string CENTRE { get; set; }
    public string DISTRICT { get; set; }
    public string STATE { get; set; }
    public string ADDRESS { get; set; }
    public string CONTACT { get; set; }
    public string MICR { get; set; }
    public bool UPI { get; set; }
    public bool RTGS { get; set; }
    public string CITY { get; set; }
    public bool NEFT { get; set; }
    public bool IMPS { get; set; }
    public string BANK { get; set; }
    public string BANKCODE { get; set; }
    public string IFSC { get; set; }
}
