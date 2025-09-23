
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
/// Summary description for clsMahagram
/// </summary>
public class clsMahagram
{
    public static string SaltKey = "06CD7372EC3C7B3C4F574CC5A11EE2011E8E91FA";
    public static string Secret_Key = "D29248FC6A53B5EF9F649F7296251C81";
    public static string ChannelPartnerId = "CP00285947";

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
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

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

    public string StateData()
    {
        string result = API_Call_GET("http://uat.dhansewa.com/Common/GetState");
        return result;
    }

    public string DistrictData(NameValueCollection Values)
    {
        string result = API_Call_POST("http://uat.dhansewa.com/Common/GetDistrictByState", copyFrom(Values));
        return result;
    }

    public string Registration(NameValueCollection Values)
    {
        string result = API_Call_POST("http://uat.dhansewa.com/AEPS/APIBCRegistration", copyFrom(Values));
        return result;
    }

    public string KYCUpload(NameValueCollection Values)
    {
        string result = API_Call_POST("http://45.249.111.172/airtel/aeps/updatebcidkyc", copyFrom(Values));
        return result;
    }

    public string AddressUpload(NameValueCollection Values)
    {
        string result = API_Call_POST("http://45.249.111.172/airtel/aeps/udpatebcaddresskyc", copyFrom(Values));
        return result;
    }
    public string Shop(NameValueCollection Values)
    {
        string result = API_Call_POST("http://45.249.111.172/airtel/aeps/updatebcshopphotokyc", copyFrom(Values));
        return result;
    }

    public string Photo(NameValueCollection Values)
    {
        string result = API_Call_POST("http://45.249.111.172/airtel/aeps/updatebcpassportsizekyc", copyFrom(Values));
        return result;
    }


    public string PanRegistration(NameValueCollection Values)
    {
        string result = API_Call_POST("https://sonitechno.in/api/pan.aspx", copyFrom(Values));
        return result;
    }

    public string UpdateRegistration(NameValueCollection Values)
    {
        string result = API_Call_POST("https://sonitechno.in/api/pan.aspx", copyFrom(Values));
        return result;
    }

    public string CouponRequest(NameValueCollection Values)
    {
        string result = API_Call_POST("https://sonitechno.in/api/pan.aspx", copyFrom(Values));
        return result;
    }
    public string couponrequeststatus(NameValueCollection Values)
    {
        string result = API_Call_POST("https://sonitechno.in/api/pan.aspx", copyFrom(Values));
        return result;
    }
    
    public string UTIAgentStatus(NameValueCollection Values)
    {
        string result = API_Call_POST("https://sonitechno.in/api/pan.aspx", copyFrom(Values));
        return result;
    }

    public string CheckBCID(NameValueCollection Values)
    {
        string result = API_Call_POST("https://sonitechno.in/api/aeps.aspx", copyFrom(Values));
        return result;
    }

    public string BCInitiate(NameValueCollection Values)
    {
        string result = API_Call_POST("http://uat.dhansewa.com/AEPS/BCInitiate", copyFrom(Values));
        return result;
    }

    public string CheckStatus(NameValueCollection Values)
    {
        string result = API_Call_POST("http://uat.dhansewa.com/Common/CheckAePSTxnStatus", copyFrom(Values));
        return result;
    }

    public static string Call_API(string requestJson)
    {
        string URL = "http://uat.dhansewa.com/MICROATM/GetMATMtxnStatus";
        string response = string.Empty;
        try
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] data = encoder.GetBytes(requestJson);
            string jsrequest = js.Serialize(requestJson);
            HttpWebRequest requestObj = (HttpWebRequest)WebRequest.Create(URL);
            requestObj.Method = "POST";
            requestObj.Accept = "application/json";
            requestObj.ContentType = "application/json";
            requestObj.Timeout = 180000;
            requestObj.GetRequestStream().Write(data, 0, data.Length);
            HttpWebResponse webResponse = (HttpWebResponse)requestObj.GetResponse();
            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
            {
                response = sr.ReadToEnd();
            }
            requestJson = URL + "  " + requestJson;
            response = response.Replace("'", "''");
        }
        catch (Exception ex)
        {
            //requestJson = URL + "  " + requestJson;

            response = "0";
        }
        return response;
    }

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
    public string ImageToBase64(string docPath)
    {

        string path = docPath;
        string base64String = "";
        using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
        {
            using (MemoryStream m = new MemoryStream())
            {
                image.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();
                base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }
    #region [ Json Output ]
    public string SuccessMessage(string SuccessMessage)
    {
        string response = "{\"statuscode\":\"TXN\",\"status\":\"Transaction Successful\",\"data\":" + SuccessMessage + "}";
        return response;
    }
    public string FailureMessage(string ErrorMessage)
    {
        string response = "{\"statuscode\":\"ERR\",\"status\":\"" + ErrorMessage + "\",\"data\":\"\"}";
        return response;
    }
    #endregion
}





public class MATM_RequestMH
{
    public string secretekey { get; set; }
    public string saltkey { get; set; }
    public string referenceid { get; set; }
}




public class MData
{
    public string bcid { get; set; }
    public string mid { get; set; }
    public string pid { get; set; }
    public string stanno { get; set; }
    public string amount { get; set; }
    public string createdate { get; set; }
    public string status { get; set; }
    public string bankmessage { get; set; }
    public string rrn { get; set; }
    public string cardno { get; set; }
    public string refunddate { get; set; }
    public string clientrefid { get; set; }
    public string udf1 { get; set; }
    public string udf2 { get; set; }
    public string udf3 { get; set; }
    public string remarks { get; set; }
}

public class MATMCheckStatusRoot
{
    public string message { get; set; }
    public string statuscode { get; set; }
    public IList<MData> Data { get; set; }
}
