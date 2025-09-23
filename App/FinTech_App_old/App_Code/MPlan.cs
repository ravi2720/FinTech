using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MPlan
/// </summary>
public class MPlan
{
    public static string Roffer(string Opr, string Mobileno)
    {
        string Result = PostGetMPlan.Roffer(Opr, Mobileno);
        return Result;
    }

    public static string DTHDetails(string Opr, string Mobileno)
    {
        string Result = PostGetMPlan.DTHDetails(Opr.Replace(" ",""), Mobileno);
        return Result;
    }

    public static string DTHPlan(string Opr)
    {
        string Result = PostGetMPlan.DTHPlan(Opr);
        return Result;
    }

    public static string Plan(string Opr)
    {
        string Result = PostGetMPlan.Plan(Opr);
        return Result;
    }
    public static string Bill(string Opr, string Mobileno)
    {
        string Result = PostGetMPlan.Bill(Opr.Replace(" ", ""), Mobileno);
        return Result;
    }
    public static string WaterBill(string Opr, string Mobileno)
    {
        string Result = PostGetMPlan.WaterBill(Opr.Replace(" ", ""), Mobileno);
        return Result;
    }
}


    


public class PostGetMPlan
{
    public static string Roffer(string oprerator, string mobile)
    {
        var client = new RestClient("https://www.mplan.in/api/plans.php?apikey=f84439a60e24365e901549829f7b063c&offer=roffer&tel=" + mobile + "&operator=" + oprerator + "");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "Basic cnpwX2xpdmVfdllJaDNPOVVnQ2hpT2c6WnVrZ0trUkZ6SjZHdlJSMWFNejFDUk40");
        request.AddHeader("Cookie", "mplansession=gef167np5k0o8sam6feeo2c5o3");
        IRestResponse response = client.Execute(request);
       return (response.Content);
    }

    public static string DTHDetails(string oprerator, string mobile)
    {
        var client = new RestClient("https://www.mplan.in/api/Dthinfo.php?apikey=f84439a60e24365e901549829f7b063c&offer=roffer&tel=" + mobile + "&operator=" + oprerator + "");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "Basic cnpwX2xpdmVfdllJaDNPOVVnQ2hpT2c6WnVrZ0trUkZ6SjZHdlJSMWFNejFDUk40");
        request.AddHeader("Cookie", "mplansession=gef167np5k0o8sam6feeo2c5o3");
        IRestResponse response = client.Execute(request);
        return (response.Content);
    }

    public static string DTHPlan(string oprerator)
    {
        var client = new RestClient("https://www.mplan.in/api/dthplans.php?apikey=f84439a60e24365e901549829f7b063c&operator=" + oprerator + "");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "Basic cnpwX2xpdmVfdllJaDNPOVVnQ2hpT2c6WnVrZ0trUkZ6SjZHdlJSMWFNejFDUk40");
        request.AddHeader("Cookie", "mplansession=gef167np5k0o8sam6feeo2c5o3");
        IRestResponse response = client.Execute(request);
        return (response.Content);
    }

    public static string Plan(string oprerator)
    {
        var client = new RestClient("https://www.mplan.in/api/plans.php?apikey=f84439a60e24365e901549829f7b063c&cricle=Rajasthan&operator=" + oprerator + "");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "Basic cnpwX2xpdmVfdllJaDNPOVVnQ2hpT2c6WnVrZ0trUkZ6SjZHdlJSMWFNejFDUk40");
        request.AddHeader("Cookie", "mplansession=gef167np5k0o8sam6feeo2c5o3");
        IRestResponse response = client.Execute(request);
        return (response.Content);
    }
    public static string Bill(string oprerator, string mobile)
    {
        var client = new RestClient("https://www.mplan.in/api/electricinfo.php?apikey=f84439a60e24365e901549829f7b063c&offer=roffer&tel=" + mobile + "&operator=" + oprerator + "");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "Basic cnpwX2xpdmVfdllJaDNPOVVnQ2hpT2c6WnVrZ0trUkZ6SjZHdlJSMWFNejFDUk40");
        request.AddHeader("Cookie", "mplansession=gef167np5k0o8sam6feeo2c5o3");
        IRestResponse response = client.Execute(request);
        return (response.Content);
    }

    public static string WaterBill(string oprerator, string mobile)
    {
        var client = new RestClient("https://www.mplan.in/api/Water.php?apikey=f84439a60e24365e901549829f7b063c&offer=roffer&tel=" + mobile + "&operator=" + oprerator + "");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "Basic cnpwX2xpdmVfdllJaDNPOVVnQ2hpT2c6WnVrZ0trUkZ6SjZHdlJSMWFNejFDUk40");
        request.AddHeader("Cookie", "mplansession=gef167np5k0o8sam6feeo2c5o3");
        IRestResponse response = client.Execute(request);
        return (response.Content);
    }
}