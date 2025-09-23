﻿using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Data;
using System.Net;
/// <summary>
/// Summary description for OperatorFatch
/// </summary>
public class OperatorFatch
{
    private string MerchantID = "AP424113";
    private string MerchantKey = "b8bde8b7dff240b";

    public static string GetPrepaid_Detail(string searchTerm)
    {
        DataTable dtResult = new DataTable();
        OperatorFatch objClass = new OperatorFatch();
        string results = "", CircleId = "0", OperatorId = "0";
        try
        {
            WebClient wc = new WebClient();
            results = wc.DownloadString("https://cyrusrecharge.in/API/CyrusOperatorFatchAPI.aspx?APIID=" + objClass.MerchantID.Trim() + "&PASSWORD=" + objClass.MerchantKey.Trim() + "&MOBILENUMBER=" + searchTerm + "&MOBILETYPE=1&FORMAT=JSON");
            JObject jobject = JObject.Parse(results);
            if (Convert.ToString(jobject["Status"]) == "0")
            {
                string optrcd = Convert.ToString(jobject["OperatorCode"]);
                string cclcd = Convert.ToString(jobject["CircleCode"]);
                cls_connection objConnection = new cls_connection();
                DataTable dto = objConnection.select_data_dt("select ID from Operator where OperatorCode='" + optrcd + "'");
                if (dto.Rows.Count > 0)
                {
                    OperatorId = Convert.ToString(dto.Rows[0][0]);
                }
                DataTable dtc = objConnection.select_data_dt("select top 1 CircleId from tblrecharge_CircleCode where APIID=1 and CircleCode='" + cclcd + "'");
                if (dtc.Rows.Count > 0)
                {
                    CircleId = Convert.ToString(dtc.Rows[0][0]);
                }
            }
            return Convert.ToString(OperatorId) + "," + Convert.ToString(CircleId);
        }
        catch
        {
            return Convert.ToString(OperatorId) + "," + Convert.ToString(CircleId);
        }
    }

    public static string PlanNew(string OPID, string Cir)
    {
        string Result = CallPlanAPI("https://sonitechno.in/api/PlanAPI.aspx?MethodName=planNew&opt=" + OPID + "&cir=" + Cir + "");
        return Result;
    }
    public static string Plan(string OPID)
    {
        cls_connection ObjData = new cls_connection();
        string strop = ObjData.select_data_scalar_string("select OperatorCode from tblRecharge_OperatorCode where OperatorID='" + OPID + "' and apiid=3");
        string Result = CallPlanAPI("https://sonitechno.in/api/PlanAPI.aspx?MethodName=plan&opt=" + strop + "");
        return Result;
    }

    public static string RofferPlan(string OPID, string Numer)
    {
        string Result = CallPlanAPI("https://sonitechno.in/api/planapi.aspx?MethodName=Roffer&opt=" + OPID + "&Number=" + Numer + "");
        return Result;
    }

    public static string DTHOpeator(string Number)
    {
        cls_connection ObjData = new cls_connection();
        string Result = CallPlanAPI("https://sonitechno.in/api/PlanAPI.aspx?MethodName=dthoperatorinfo&tel=" + Number + "");
        return Result;
    }

    public static string DTHPlan(string Number)
    {
        cls_connection ObjData = new cls_connection();
        string Result = CallPlanAPI("https://sonitechno.in/api/PlanAPI.aspx?MethodName=dthcustomerplan&opt=" + Number + "");
        return Result;
    }

    public static string DTHCustomerInfo(string Number, string OperatorID)
    {
        cls_connection ObjData = new cls_connection();
        string Result = CallPlanAPI("https://sonitechno.in/api/PlanAPI.aspx?MethodName=dthcustomerinfo&opt=" + OperatorID + "&Number=" + Number + "");
        return Result;
    }

    public static string CallPlanAPI(string URL)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient(URL);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Cookie", "ASP.NET_SessionId=0foxho2jkc1qoxmakntej1hx");
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string GetPostpaid_Detail(string searchTerm)
    {
        DataTable dtResult = new DataTable();
        OperatorFatch objClass = new OperatorFatch();
        string results = "", CircleId = "0", OperatorId = "0";
        try
        {
            WebClient wc = new WebClient();
            results = wc.DownloadString("https://cyrusrecharge.in/API/CyrusOperatorFatchAPI.aspx?APIID=" + objClass.MerchantID.Trim() + "&PASSWORD=" + objClass.MerchantKey.Trim() + "&MOBILENUMBER=" + searchTerm + "&MOBILETYPE=2&FORMAT=JSON");
            JObject jobject = JObject.Parse(results);
            if (Convert.ToString(jobject["Status"]) == "0")
            {
                string optrcd = Convert.ToString(jobject["OperatorCode"]);
                string cclcd = Convert.ToString(jobject["CircleCode"]);
                cls_connection objConnection = new cls_connection();
                DataTable dto = objConnection.select_data_dt("select top 1 operatorid from tblRecharge_OperatorCode where APIID=1 and OperatorCode='" + optrcd + "'");
                if (dto.Rows.Count > 0)
                {
                    OperatorId = Convert.ToString(dto.Rows[0][0]);
                }
                DataTable dtc = objConnection.select_data_dt("select top 1  CircleId from tblrecharge_CircleCode where APIID=1 and CircleCode='" + cclcd + "'");
                if (dtc.Rows.Count > 0)
                {
                    CircleId = Convert.ToString(dtc.Rows[0][0]);
                }
            }
            return Convert.ToString(OperatorId) + "," + Convert.ToString(CircleId);
        }
        catch
        {
            return Convert.ToString(OperatorId) + "," + Convert.ToString(CircleId);
        }
    }
}