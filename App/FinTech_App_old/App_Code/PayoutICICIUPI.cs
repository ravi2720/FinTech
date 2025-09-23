using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

using System.Net;
using System.Collections.Specialized;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography;

using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Collections;
using System.ComponentModel;


/// <summary>
/// Summary description for PayoutICICIUPI
/// </summary>
public class PayoutICICIUPI
{
    public static string URL = "https://apibankingone.icicibank.cdom/api/MerchantAPI/UPI/v0/CollectPay2/406821";// "https://apibankingsandbox.icicibank.com/api/MerchantAPI/UPI/v0/CollectPay2/400969";
    public static string URLCheckStatus = "https://apibankingone.icicibank.codm/api/MerchantAPI/UPI/v0/TransactionStatus3/406821";//"https://apibankingsandbox.icicibank.com/api/MerchantAPI/UPI/v0/TransactionStatus1/400969";
    public static string URLQR = "https://apibankingone.icicibank.com/api/MedrchantAPI/UPI/v0/QR/406821";//"https://apibankingsandbox.icicibank.com/api/MerchantAPI/UPI/v0/QR/400969";
    public static string URLQRCheckStatus = "https://apibankingone.icicibank.dcom/api/MerchantAPI/UPI/v0/CallbackStatus2/406821";// "https://apibankingsandbox.icicibank.com/api/MerchantAPI/UPI/v0/CallbackStatus2/400969";
    public PayoutICICIUPI()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string deCipherRequest(byte[] stringToDecrypt)
    {
        X509Certificate2 certificate = getPrivateKey();
        RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PrivateKey;
        byte[] cryptedData = null;
        try
        {
            cryptedData = rsa.Decrypt(stringToDecrypt, false);
        }
        catch (Exception ex)
        {
            string abc = ex.Message;
        }

        return Encoding.UTF8.GetString(cryptedData, 0, cryptedData.Length);
    }
    public static string cipherRequest(byte[] stringToEncrypt)
    {
        //  string Certificate = "C:\\Users\\Soni Techno\\Downloads\\icicipub.cer";
        //string Certificate = HttpContext.Current.Server.MapPath("~/img/icicipub.cer");
        string Certificate = HttpContext.Current.Server.MapPath("~/img/merchantEncryption.pem");// ("~/img/rsa_apikey.cer");
        X509Certificate2 cert2 = getCertificate("icicipub.cer", Certificate);

        X509Certificate2 certificate = cert2;
        RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key;

        byte[] cryptedData = rsa.Encrypt(stringToEncrypt, false);

        return Convert.ToBase64String(cryptedData);
    }

    public static X509Certificate2 getPrivateKey()
    {
        // string _keyURL = @"C:\Users\OneDrive\Desktop\icici\raycharge.pfx";// file should be in pfx only
        string _keyURL = HttpContext.Current.Server.MapPath("~/img/BizPrivateKey.pfx"); ;// @"C:\Users\OneDrive\Desktop\icici\raycharge.pfx";// file should be in pfx only

        X509Certificate2 cert2 = null;

        try
        {
            cert2 = new X509Certificate2(_keyURL, "", X509KeyStorageFlags.MachineKeySet);
        }
        catch (CryptographicException ex)
        {
            //Console.WriteLine(ex.Message);

        }
        catch (Exception ex)
        {

        }
        return cert2;
    }



    public static X509Certificate2 getCertificate(string certificateName, string path)
    {
        X509Certificate2 certificate1 = new X509Certificate2(path);
        X509Store my = new X509Store(StoreName.My, StoreLocation.LocalMachine);
        my.Open(OpenFlags.ReadOnly);


        var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
        store.Open(OpenFlags.ReadOnly);



        X509Certificate2Collection collection = new X509Certificate2Collection();
        collection.Add(certificate1);

        // store.Add(certificate1);
        ////   store.AddRange(collection);
        store.Close();
        X509Certificate2Collection storecollection = (X509Certificate2Collection)store.Certificates;
        X509Certificate2Collection collection1 = my.Certificates;//.Find(X509FindType.FindBySubjectName, certificateName, false);

        if (collection.Count == 1)
        {
            return collection[0];
        }
        else if (collection.Count > 1)
        {
            throw new Exception(string.Format("More than one certificate with name '{0}' found in store LocalMachine/My.", certificateName));
        }
        else
        {
            throw new Exception(string.Format("Certificate '{0}' not found in store LocalMachine/My.", certificateName));
        }
    }

    public static string invokeRequest(string data, string url, string method, string callName = "")
    {
        cls_connection objConnection = new cls_connection();
        string _output = string.Empty;
        string _guid = string.Empty;
        Guid guid = Guid.NewGuid();

        _guid = _guid + Convert.ToString(guid) + DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");

        byte[] _data = Encoding.UTF8.GetBytes(data);
        var _request = cipherRequest(_data);

        string httpUrl = PayoutICICIUPI.URL;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + data + "')");

        objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + _request + "')");
        //SAVE REQUEST LOG
        // ErrorLog(_guid, httpUrl, data, _request.ToString(), method, "");

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
        httpWebRequest.ContentType = "text/plain";
        httpWebRequest.Method = "POST";

        httpWebRequest.KeepAlive = true;



        objConnection.update_data("insert into tbl_Apeslog(url)values('15b856af8ab8413087493d2d268f8b6e')");

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(_request);
            streamWriter.Flush();
        }

        string _resCode = string.Empty;
        string _decrypt = string.Empty;

        HttpWebResponse httpResponse = null;
        try
        {
            //httpResponse.Headers.Add("accept", "*/*");
            //httpResponse.Headers.Add("accept-encoding", "*");
            //httpResponse.Headers.Add("accept-language", "en-US");
            //httpResponse.Headers.Add("q", "0.8,hi");
            //httpResponse.Headers.Add("cache-control","no-cache");
            httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            _resCode = Convert.ToString(httpResponse.StatusCode);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                _output = streamReader.ReadToEnd();
            }
            objConnection.update_data("insert into tbl_Apeslog(url)values('" + _output + "')");

        }
        catch (WebException ex)
        {
            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    _resCode = Convert.ToString(response.StatusCode);
                    _output = response.StatusDescription.ToString();
                }
                else
                {
                    _resCode = "No Code Available #1";
                    _output = "";
                }
            }
            else
            {
                _resCode = "No Code Available #2";
                _output = "";
            }
            objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + _output + "')");

            // ErrorLog(_guid, httpUrl, _output, _decrypt, method, _resCode);
            return "{\"MESSAGE\":\"" + _output + "\"}";
        }

        // ErrorLog(_guid, httpUrl, _output, _decrypt, method, _resCode);


        //byte[] _dataDecrypt = Convert.FromBase64String(_output);
        //_decrypt = deCipherRequest(_dataDecrypt);        
        //return _decrypt;.
        return _output;
    }
    public static string invokeRequestStatusCheck(string data, string url, string method, string callName = "")
    {
        cls_connection objConnection = new cls_connection();
        string _output = string.Empty;
        string _guid = string.Empty;
        Guid guid = Guid.NewGuid();

        _guid = _guid + Convert.ToString(guid) + DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");

        byte[] _data = Encoding.UTF8.GetBytes(data);
        var _request = cipherRequest(_data);

        string httpUrl = PayoutICICIUPI.URLCheckStatus;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + data + "')");

        objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + _request + "')");
        //SAVE REQUEST LOG
        // ErrorLog(_guid, httpUrl, data, _request.ToString(), method, "");

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
        httpWebRequest.ContentType = "text/plain";
        httpWebRequest.Method = "POST";

        httpWebRequest.KeepAlive = true;



        objConnection.update_data("insert into tbl_Apeslog(url)values('15b856af8ab8413087493d2d268f8b6e')");

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(_request);
            streamWriter.Flush();
        }

        string _resCode = string.Empty;
        string _decrypt = string.Empty;

        HttpWebResponse httpResponse = null;
        try
        {
            //httpResponse.Headers.Add("accept", "*/*");
            //httpResponse.Headers.Add("accept-encoding", "*");
            //httpResponse.Headers.Add("accept-language", "en-US");
            //httpResponse.Headers.Add("q", "0.8,hi");
            //httpResponse.Headers.Add("cache-control","no-cache");
            httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            _resCode = Convert.ToString(httpResponse.StatusCode);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                _output = streamReader.ReadToEnd();
            }
            objConnection.update_data("insert into tbl_Apeslog(url)values('" + _output + "')");

        }
        catch (WebException ex)
        {
            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    _resCode = Convert.ToString(response.StatusCode);
                    _output = response.StatusDescription.ToString();
                }
                else
                {
                    _resCode = "No Code Available #1";
                    _output = "";
                }
            }
            else
            {
                _resCode = "No Code Available #2";
                _output = "";
            }
            objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + _output + "')");

            // ErrorLog(_guid, httpUrl, _output, _decrypt, method, _resCode);
            return "{\"MESSAGE\":\"" + _output + "\"}";
        }

        // ErrorLog(_guid, httpUrl, _output, _decrypt, method, _resCode);


        //byte[] _dataDecrypt = Convert.FromBase64String(_output);
        //_decrypt = deCipherRequest(_dataDecrypt);        
        //return _decrypt;.
        return _output;
    }
    public static string invokeRequestURLQR(string data, string url, string method, string callName = "")
    {
        cls_connection objConnection = new cls_connection();
        string _output = string.Empty;
        string _guid = string.Empty;
        Guid guid = Guid.NewGuid();

        _guid = _guid + Convert.ToString(guid) + DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");

        byte[] _data = Encoding.UTF8.GetBytes(data);
        var _request = cipherRequest(_data);

        string httpUrl = PayoutICICIUPI.URLQR;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + data + "')");

        objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + _request + "')");
        //SAVE REQUEST LOG
        // ErrorLog(_guid, httpUrl, data, _request.ToString(), method, "");

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
        httpWebRequest.ContentType = "text/plain";
        httpWebRequest.Method = "POST";

        httpWebRequest.KeepAlive = true;



        objConnection.update_data("insert into tbl_Apeslog(url)values('15b856af8ab8413087493d2d268f8b6e')");

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(_request);
            streamWriter.Flush();
        }

        string _resCode = string.Empty;
        string _decrypt = string.Empty;

        HttpWebResponse httpResponse = null;
        try
        {
            //httpResponse.Headers.Add("accept", "*/*");
            //httpResponse.Headers.Add("accept-encoding", "*");
            //httpResponse.Headers.Add("accept-language", "en-US");
            //httpResponse.Headers.Add("q", "0.8,hi");
            //httpResponse.Headers.Add("cache-control","no-cache");
            httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            _resCode = Convert.ToString(httpResponse.StatusCode);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                _output = streamReader.ReadToEnd();
            }
            objConnection.update_data("insert into tbl_Apeslog(url)values('" + _output + "')");

        }
        catch (WebException ex)
        {
            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    _resCode = Convert.ToString(response.StatusCode);
                    _output = response.StatusDescription.ToString();
                }
                else
                {
                    _resCode = "No Code Available #1";
                    _output = "";
                }
            }
            else
            {
                _resCode = "No Code Available #2";
                _output = "";
            }
            objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + _output + "')");

            // ErrorLog(_guid, httpUrl, _output, _decrypt, method, _resCode);
            return "{\"MESSAGE\":\"" + _output + "\"}";
        }

        // ErrorLog(_guid, httpUrl, _output, _decrypt, method, _resCode);


        //byte[] _dataDecrypt = Convert.FromBase64String(_output);
        //_decrypt = deCipherRequest(_dataDecrypt);        
        //return _decrypt;.
        return _output;
    }
    public static string invokeRequestQRUPICheckStatus(string data, string url, string method, string callName = "")
    {
        cls_connection objConnection = new cls_connection();
        string _output = string.Empty;
        string _guid = string.Empty;
        Guid guid = Guid.NewGuid();

        _guid = _guid + Convert.ToString(guid) + DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss");

        byte[] _data = Encoding.UTF8.GetBytes(data);
        var _request = cipherRequest(_data);

        string httpUrl = PayoutICICIUPI.URLQRCheckStatus;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + data + "')");

        objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + _request + "')");
        //SAVE REQUEST LOG
        // ErrorLog(_guid, httpUrl, data, _request.ToString(), method, "");

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
        httpWebRequest.ContentType = "text/plain";
        httpWebRequest.Method = "POST";

        httpWebRequest.KeepAlive = true;



        objConnection.update_data("insert into tbl_Apeslog(url)values('15b856af8ab8413087493d2d268f8b6e')");

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            streamWriter.Write(_request);
            streamWriter.Flush();
        }

        string _resCode = string.Empty;
        string _decrypt = string.Empty;

        HttpWebResponse httpResponse = null;
        try
        {
            //httpResponse.Headers.Add("accept", "*/*");
            //httpResponse.Headers.Add("accept-encoding", "*");
            //httpResponse.Headers.Add("accept-language", "en-US");
            //httpResponse.Headers.Add("q", "0.8,hi");
            //httpResponse.Headers.Add("cache-control","no-cache");
            httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            _resCode = Convert.ToString(httpResponse.StatusCode);
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                _output = streamReader.ReadToEnd();
            }
            objConnection.update_data("insert into tbl_Apeslog(url)values('" + _output + "')");

        }
        catch (WebException ex)
        {
            if (ex.Status == WebExceptionStatus.ProtocolError)
            {
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                {
                    _resCode = Convert.ToString(response.StatusCode);
                    _output = response.StatusDescription.ToString();
                }
                else
                {
                    _resCode = "No Code Available #1";
                    _output = "";
                }
            }
            else
            {
                _resCode = "No Code Available #2";
                _output = "";
            }
            objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + _output + "')");

            // ErrorLog(_guid, httpUrl, _output, _decrypt, method, _resCode);
            return "{\"MESSAGE\":\"" + _output + "\"}";
        }

        // ErrorLog(_guid, httpUrl, _output, _decrypt, method, _resCode);


        //byte[] _dataDecrypt = Convert.FromBase64String(_output);
        //_decrypt = deCipherRequest(_dataDecrypt);        
        //return _decrypt;.
        return _output;
    }
}