using RestSharp;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for Credopay
/// </summary>
public class Credopay
{
    public static string ApiKey = "b49d98a5-a444-4a0f-a2ba-312077210ec9";
    public static string AepsKey = "f6a8d038-bba7-425b-a6c5-fcbdb128a8a2";
    public static string AepsSecretkey = "f6b2025d-d6c5-4861-b760-ed17b23a33cd";
    public static string URL = "https://api.thecenterfirst.com/transactions/v3.0/";




    public static string PostCallWithoutEn(string url, string body)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        var client = new RestClient(Credopay.URL + url);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        request.AddHeader("authorization", Credopay.AepsKey);
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);

        return response.Content;
    }

    public static string PostCallWithBody(string url, string body)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        var client = new RestClient(Credopay.URL + url);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        request.AddHeader("authorization", Credopay.AepsKey);
        request.AddParameter("application/json", "{\"encrypted\":\"" + body + "\"}", ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);

        return response.Content;
    }
    public Credopay()
    {
        //
        // TODO: Add constructor logic here
        //
    }




    public static string EncryptJavaEqualent(string keyValue, string text)
    {
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
        AesManaged tdes = new AesManaged();
        tdes.Key = UTF8.GetBytes(keyValue);
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform crypt = tdes.CreateEncryptor();
        byte[] plain = Encoding.UTF8.GetBytes(text);
        byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
        String encryptedText = Convert.ToBase64String(cipher);
        return encryptedText;
    }

    public static string ComputeMD5(string s)
    {
        StringBuilder sb = new StringBuilder();
        // Initialize a MD5 hash object
        using (MD5 md5 = MD5.Create())
        {
            // Compute the hash of the given string
            byte[] hashValue = md5.ComputeHash(Encoding.UTF8.GetBytes(s));
            // Convert the byte array to string format
            foreach (byte b in hashValue)
            {
                sb.Append($"{b:X2}");
            }
        }
        return sb.ToString();
    }


}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class AuthenticationCredopay
{
    public string app_version { get; set; }
    public string hashed_aadhar { get; set; }
    public string aadhar { get; set; }
    public string pid_data { get; set; }
    public string masked_aadhar { get; set; }
    public string cpId { get; set; }
    public string transaction_group { get; set; }
    public string transaction_type { get; set; }
    public string payment_method { get; set; }
    public string virtual_Id { get; set; }
    public string biometric_srno { get; set; }
    public string biometric_rdsVer { get; set; }
    public string biometric_rdsId { get; set; }
    public string biometric_dpId { get; set; }
    public string biometric_mi { get; set; }
    public string operating_system { get; set; }
    public string transaction_origin_ip { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string customer_mobile { get; set; }
    public string virtual_ID { get; set; }


    public AuthenticationCredopay()
    {
        app_version = "1.0.6";
        virtual_ID = "";
    }
}





// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class CWMSBECredopay
{
    public string bank_name { get; set; }
    public string amount { get; set; }
    public string app_version { get; set; }
    public string CRN_U { get; set; }
    public string custom_field1 { get; set; }
    public string hashed_aadhar { get; set; }
    public string aadhar { get; set; }
    public string pid_data { get; set; }
    public string masked_aadhar { get; set; }
    public string cpId { get; set; }
    public string transaction_group { get; set; }
    public string transaction_type { get; set; }
    public string payment_method { get; set; }
    public string auth_reference_no { get; set; }
    public string biometric_srno { get; set; }
    public string biometric_rdsVer { get; set; }
    public string biometric_rdsId { get; set; }
    public string biometric_dpId { get; set; }
    public string biometric_mi { get; set; }
    public string operating_system { get; set; }
    public string transaction_origin_ip { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string customer_mobile { get; set; }
    public string virtual_ID { get; set; }
    public CWMSBECredopay()
    {
        app_version = "1.0.6";
        virtual_ID = "";
    }
}


