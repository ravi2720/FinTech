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
/// Summary description for PayoutICICI
/// </summary>
public class PayoutICICI
{
    public static string URL = "https://api.icicibank.com:8443/api/Corporate/CIdB/v1/";
    public PayoutICICI()
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
      //  string Certificate = HttpContext.Current.Server.MapPath("~/img/icicipub.cer");
        string Certificate = HttpContext.Current.Server.MapPath("~/img/ICICI_PUBLIC_CERT_PROD.txt");
        X509Certificate2 cert2 = getCertificate("icicipub.cer", Certificate);

        X509Certificate2 certificate = cert2;
        RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)certificate.PublicKey.Key;

        byte[] cryptedData = rsa.Encrypt(stringToEncrypt, false);

        return Convert.ToBase64String(cryptedData);
    }

    public static X509Certificate2 getPrivateKey()
    {
       // string _keyURL = @"C:\Users\OneDrive\Desktop\icici\raycharge.pfx";// file should be in pfx only
        string _keyURL = HttpContext.Current.Server.MapPath("~/img/PrivateKey.pfx"); ;// @"C:\Users\OneDrive\Desktop\icici\raycharge.pfx";// file should be in pfx only

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

        string httpUrl = PayoutICICI.URL+url;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        objConnection.update_data("insert into tbl_Apeslog(url)values('Request_" + data + "')");

        //SAVE REQUEST LOG
        // ErrorLog(_guid, httpUrl, data, _request.ToString(), method, "");

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
        httpWebRequest.ContentType = "text/plain";
        httpWebRequest.Method = method;
        httpWebRequest.Headers.Add("apikey", "15b856af8ab8413087493d2d268f8b6e");// "34c26727-7874-4a46-8079-1c02eaf059c6");
        httpWebRequest.KeepAlive = false;

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