
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
/// <summary>
/// Summary description for ClsMahagramPan
/// </summary>
public class ClsMahagramPan
{
    public static string Secret_Key_Pan = "";
    public static string Createdby_Pan = "";

	
    public string copyFrom(NameValueCollection a)
    {
        IDictionary<string, string> dict = new Dictionary<string, string>();
        foreach (var k in a.AllKeys)
        {
            dict.Add(k, a[k]);
        }
        string myJsonString = (new JavaScriptSerializer()).Serialize(dict);
        return myJsonString;

    }
    public string Agentonboard(NameValueCollection Values)
    {
        string result = API_Call_POST("http://uat.dhansewa.com/UTI/UATInsUTIAgent", copyFrom(Values));
        return result;
    }
    public string StatusAgent(NameValueCollection Values)
    {
        string result = API_Call_POST("http://uat.dhansewa.com/UTI/UATUTIAgentRequestStatus", copyFrom(Values));
        return result;
    }
    public string API_Call_GET(string url)
    {
        try
        {
            
            using (var client = new System.Net.WebClient())
            {
                string result = client.DownloadString(url);
                return result;
            }
        }
        catch
        {
            return "0";
        }
    }
    public string StateData()
    {
        string result = API_Call_GET("http://uat.dhansewa.com/Common/GetState");
        return result;
    }
    public static string JSONSerialize(NameValueCollection _nvc)
    {
        //var dict = _nvc.ToDictionary();
        var json = new JavaScriptSerializer().Serialize(_nvc);
        return json;
    }

    public string API_Call_POST(string url, string inputParameter)
    {


        try
        {
            var baseAddress = url;

            var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
            http.Accept = "application/json";
            http.ContentType = "application/json";
            http.Method = "POST";
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