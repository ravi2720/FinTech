using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
/// <summary>
/// Summary description for DMTPaytm
/// </summary>
public class DMTPaytm
{
    public static string SaltKey = "";
    public static string Secret_Key = "";

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

    public string Registration(NameValueCollection Values)
    {
        string result = API_Call_POST("http://uat.dhansewa.com/Common/GetState", JSONSerialize(Values));
        return SuccessMessage(result);
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



    public string BankData(NameValueCollection Values)
    {
        var responseString = "";
        using (var client = new WebClient())
        {
            var response = client.UploadValues("https://cyrusrecharge.in/api/CyrusDMRAPI.aspx", Values);
            responseString = Encoding.Default.GetString(response);

        }
        return responseString.ToString();
    }
    public DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }
    public string OTPVerification(NameValueCollection Values)
    {
        var responseString = "";
        using (var client = new WebClient())
        {
            var response = client.UploadValues("https://cyrusrecharge.in/api/CyrusDMRAPI.aspx", Values);
            responseString = Encoding.Default.GetString(response);

        }
        return responseString.ToString();

    }
    public string GetIFSC(NameValueCollection Values)
    {
        var responseString = "";
        using (var client = new WebClient())
        {
            var response = client.UploadValues("https://cyrusrecharge.in/api/CyrusDMRAPI.aspx", Values);
            responseString = Encoding.Default.GetString(response);

        }
        return responseString.ToString();

    }
    public string CustomerRegistration(string Values)
    {
        string SendData = "{\"requestToken\":\"" + Values + "\"}";
        string result = ApiPostCallPayWordDMT.PostCall(SendData, "https://pwmr-api.payworldindia.com/associate/senderRegistration.json");
        return result;
    }

    public string BeneficiaryAccount_Verification(NameValueCollection Values)
    {
        var responseString = "";
        using (var client = new WebClient())
        {
            var response = client.UploadValues("https://cyrusrecharge.in/api/CyrusDMRAPI.aspx", Values);
            responseString = Encoding.Default.GetString(response);

        }
        return responseString.ToString();
    }

    public string RsendOTP(string Values)
    {
        string SendData = "{\"requestToken\":\"" + Values + "\"}";
        string result = ApiPostCallPayWordDMT.PostCall(SendData, "https://pwmr-api.payworldindia.com/associate/generateOtp.json");
        return result;
    }
    public string Getcustomerdetails(string Values)
    {
        string SendData = "{\"requestToken\":\"" + Values + "\"}";
        string result = ApiPostCallPayWordDMT.PostCall(SendData, "https://pwmr-api.payworldindia.com/associate/getSenderDetails.json");
        return result;
    }
    public string AddBeneficiary(string Values)
    {
        string SendData = "{\"requestToken\":\"" + Values + "\"}";
        string result = ApiPostCallPayWordDMT.PostCall(SendData, "https://pwmr-api.payworldindia.com/associate/addBeneficiary.json");
        return result;
    }
    public string DeletBeneficiaryAccount(NameValueCollection Values)
    {
        var responseString = "";
        using (var client = new WebClient())
        {
            var response = client.UploadValues("https://cyrusrecharge.in/api/CyrusDMRAPI.aspx", Values);
            responseString = Encoding.Default.GetString(response);

        }
        return responseString.ToString();
    }

    public string GetBeneficiaryDetails(NameValueCollection Values)
    {
        var responseString = "";
        using (var client = new WebClient())
        {
            var response = client.UploadValues("https://cyrusrecharge.in/api/CyrusDMRAPI.aspx", Values);
            responseString = Encoding.Default.GetString(response);

        }
        return responseString.ToString();
    }
    public string VerifyBeneficiary(NameValueCollection Values)
    {
        string result = API_Call_POST("http://uat.dhansewa.com/AIRTEL/VerifybeneApi", copyFrom(Values));
        return result;
    }
    public string CheckStatus(string Values)
    {
        string SendData = "{\"requestToken\":\"" + Values + "\"}";
        string result = ApiPostCallPayWordDMT.PostCall(SendData, "https://pwmr-api.payworldindia.com/associate/reQuery.json");
        return result;
    }
    public string CheckAndUpdateStatus(NameValueCollection Values)
    {
        string result = API_Call_POST("http://uat.dhansewa.com/Airtel/Common/CheckAndUpdateStatus", copyFrom(Values));

        return result;
    }
    public string VerifyOTP(String Values)
    {
        string SendData = "{\"requestToken\":\"" + Values + "\"}";
        string result = ApiPostCallPayWordDMT.PostCall(SendData, "https://pwmr-api.payworldindia.com/associate/verifyOtp.json");
        return result;
    }
    public string SendAmount(String Values)
    {
        string SendData = "{\"requestToken\":\"" + Values + "\"}";
        string result = ApiPostCallPayWordDMT.PostCall(SendData, "https://pwmr-api.payworldindia.com/associate/moneytransfer.json");
        return result;
    }

    public string GetBalance(String Values)
    {
        string SendData = "{\"requestToken\":\"" + Values + "\"}";
        string result = ApiPostCallPayWordDMT.PostCall(SendData, "https://pwmr-api.payworldindia.com/associate/getBalance.json");
        return result;
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