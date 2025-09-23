using RestSharp;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for NWSPay
/// </summary>
public class NWSPay
{
    public readonly static string Auth_Code = "1";
    public readonly static string Client_Id = "gqfvDbrK6Dl3WOpk";
    public readonly static string Client_Secret = "3zdMApNgm8iIFzHIf0JNaCEP8gmKE5G8";
    public readonly static string EncryptionKey = "ADLBYft62tW4gBrdxaa7DMJY9GxeL8mP";
    public readonly static string baseUrl = "https://api.nwsglobal.in";

    public static string GetEncryptAadhar(string data)
    {
        string EncryptionKey = NWSPay.EncryptionKey;
        string iniVector;
        byte[] IV = ASCIIEncoding.ASCII.GetBytes("91543c0ce2ff7bf4");
        byte[] clearBytes = Encoding.Unicode.GetBytes(data);
        AesCryptoServiceProvider crypt_provider;
        crypt_provider = new AesCryptoServiceProvider();
        crypt_provider.KeySize = 256;
        crypt_provider.Key = ASCIIEncoding.ASCII.GetBytes(EncryptionKey);
        crypt_provider.IV = IV;
        crypt_provider.Mode = CipherMode.CBC;
        crypt_provider.Padding = PaddingMode.PKCS7;
        ICryptoTransform transform = crypt_provider.CreateEncryptor();
        byte[] encrypted_bytes = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(data), 0, data.Length);
        byte[] encryptedData = new byte[encrypted_bytes.Length + IV.Length];
        IV.CopyTo(encryptedData, 0);
        encrypted_bytes.CopyTo(encryptedData, IV.Length);
        data = Convert.ToBase64String(encryptedData);
        return data;
    }

    public static string OutletKYCInitiate(string Json)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        ////Bypass SSL certificate validation (only for dev / testing)
        //   ServicePointManager.ServerCertificateValidationCallback =
        //       (sender, cert, chain, sslPolicyErrors) => true;

        var client = new RestClient($"{baseUrl}/Outlet/signup/initiate");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string OutletKYCInitiateValidate(string Json)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        ////Bypass SSL certificate validation (only for dev / testing)
        //   ServicePointManager.ServerCertificateValidationCallback =
        //       (sender, cert, chain, sslPolicyErrors) => true;

        var client = new RestClient($"{baseUrl}/Outlet/signup/validate");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string OutletLogin(string Json, string OutletID)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/fi/aeps/outletLogin");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutletID);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string MiniStatement(string Json, string OutletID)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/fi/aeps/miniStatement");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutletID);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string CashWithdrawal(string Json, string OutletID)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/fi/aeps/cashWithdrawal");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutletID);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string CashDeposit(string Json, string OutletID)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/fi/aeps/cashDeposit");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutletID);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string BalanceInquery(string Json, string OutletID)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/fi/aeps/balanceInquiry");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutletID);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string AccountVerify(string Json, string OutletID)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Identity/verifyBankAccount");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutletID);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string CheckStatus(string Json, string UerAgent)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/reports/txnStatus");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string GetRemitBankList(string OutLet)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Fi/remit/out/domestic/v2/banks");
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutLet);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string GetRemitProfile(string Json, string outletId)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Fi/remit/out/domestic/v2/remitterProfile");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", outletId);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string RemitterRegistration(string Json, string outletId)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Fi/remit/out/domestic/v2/remitterRegistration");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", outletId);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string RemitterRegistrationVerify(string Json, string outletId)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Fi/remit/out/domestic/v2/remitterRegistrationVerify");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", outletId);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string RemitterKYC(string Json, string outletId)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Fi/remit/out/domestic/v2/remitterKyc");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", outletId);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string BeneficiaryRegistration(string Json, string outletId)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Fi/remit/out/domestic/v2/beneficiaryRegistration");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", outletId);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string BeneficiaryRegistrationVerify(string Json, string outletId)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Fi/remit/out/domestic/v2/beneficiaryRegistrationVerify");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", outletId);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string BeneficiaryDelete(string json, string outletId)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Fi/remit/out/domestic/v2/beneficiaryDelete");
        client.Timeout = -1;

        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", outletId);

        request.AddParameter("application/json", json, ParameterType.RequestBody);

        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string BeneficiaryDeleteVerify(string Json, string outletId)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Fi/remit/out/domestic/v2/beneficiaryDeleteVerify");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", outletId);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string GenerateTransactionOtp(string Json, string outletId)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Fi/remit/out/domestic/v2/generateTransactionOtp");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", outletId);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string Transaction(string Json, string outletId)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Fi/remit/out/domestic/v2/transaction");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", outletId);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string GetBankList(string OutLet)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/fi/aeps/banks");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutLet);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string CheckLoginStatus(string json, string OutLet)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/fi/aeps/outletLoginStatus");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddParameter("application/json", json, ParameterType.RequestBody);
        request.AddHeader("Outlet-Id", OutLet);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string GenerateExternalRef()
    {
        // Base: Guid without dashes
        string baseRef = Guid.NewGuid().ToString("N"); // 32 alphanumeric characters

        // Insert a few allowed special characters to ensure compatibility
        string externalRef = $"{baseRef.Substring(0, 8)}_{baseRef.Substring(8, 4)}-{baseRef.Substring(12, 4)}.{baseRef.Substring(16, 4)}";

        // Ensure it is max 36 characters
        return externalRef.Length > 36 ? externalRef.Substring(0, 36) : externalRef;
    }

    // Recharge
    public static string GetMobileInfo(string OutLet, string mobile)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        ////Bypass SSL certificate validation (only for dev / testing)
           ServicePointManager.ServerCertificateValidationCallback =
               (sender, cert, chain, sslPolicyErrors) => true;

        var client = new RestClient($"https://localhost:7212/Recharge/mobile-info?mobile={Uri.EscapeDataString(mobile)}");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutLet);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string GetDthInfo(string OutLet, string operatorCode, string number)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Recharge/dth-info?operatorCode={operatorCode}&number={number}");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutLet);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string GetRPlan(string OutLet, string operatorCode, string circle)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Recharge/plan?operatorCode={operatorCode}&circle={circle}");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutLet);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string GetROffer(string OutLet, string operatorCode, string number)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Recharge/offer?operatorCode={operatorCode}&number={number}");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", OutLet);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string Recharge(string json, string outletId)
    {
        string endpointIp = cls_myMember.GetIPAddress();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient($"{baseUrl}/Recharge");
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", NWSPay.Auth_Code);
        request.AddHeader("Client-Id", NWSPay.Client_Id);
        request.AddHeader("Client-Secret", NWSPay.Client_Secret);
        request.AddHeader("Endpoint-Ip", endpointIp);
        request.AddHeader("Outlet-Id", outletId);
        request.AddParameter("application/json", json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }
}
