using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

/// <summary>
/// Summary description for InstantPayout
/// </summary>
public class InstantPayout
{
    public readonly static string token = "";
    public readonly static string debit_account = "";
    public readonly static string X_Ipay_Auth_Code = "";
    public readonly static string X_Ipay_Client_Id = "";
    public readonly static string X_Ipay_Client_Secret = "";
    public readonly static string X_Ipay_Endpoint_Ip = "";
    public readonly static string bankId = "";
    public readonly static string EncryptionKey = "";
    public readonly static string bankProfileId = "";
    public readonly static string Url = "";
    public readonly static string AccountVerifyUrl = "";
    public readonly static string CheckStatus = "";
    public readonly static string NumberChange = "";

    public readonly static string[] arrTemplate = new string[]
    {
        "TXN",
        "TUP",
        "RPI",
        "UAD",
        "IAC",
        "IAT",
        "AAB",
        "IAB",
        "ISP",
        "DID",
        "DTX",
        "IAN",
        "IRA",
        "DTB",
        "RBT",
        "SPE",
        "SPD",
        "UED",
        "IEC",
        "IRT",
        "ITI",
        "TSU",
        "IPE",
        "ISE",
        "TRP",
        "OUI",
        "ODI",
        "TDE",
        "DLS",
        "RNF",
        "RAR",
        "IVC",
        "IUA",
        "SNA",
        "ERR",
        "FAB",
        "TRP",
        "ERR",
        "UFC",
        "OLR",
        "OTP",
        "EOP",
        "ONV",
        "RAB",

    };

    public static string PostCall(string Json, string url, string UerAgent)
    {
        var baseAddress = InstantPayout.Url + url;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient(baseAddress);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("X-Ipay-Auth-Code", InstantPayout.X_Ipay_Auth_Code);
        request.AddHeader("X-Ipay-Client-Id", InstantPayout.X_Ipay_Client_Id);
        request.AddHeader("X-Ipay-Client-Secret", InstantPayout.X_Ipay_Client_Secret);
        request.AddHeader("X-Ipay-Endpoint-Ip", InstantPayout.X_Ipay_Endpoint_Ip);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string AccountVerifyPostCall(string Json, string url, string UerAgent)
    {
        var baseAddress = InstantPayout.AccountVerifyUrl;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient(baseAddress);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("X-Ipay-Auth-Code", InstantPayout.X_Ipay_Auth_Code);
        request.AddHeader("X-Ipay-Client-Id", InstantPayout.X_Ipay_Client_Id);
        request.AddHeader("X-Ipay-Client-Secret", InstantPayout.X_Ipay_Client_Secret);
        request.AddHeader("X-Ipay-Endpoint-Ip", InstantPayout.X_Ipay_Endpoint_Ip);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    public static string AccountVerify(string Json, string OutletID)
    {
        var baseAddress = InstantPayout.AccountVerifyUrl;
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient(baseAddress);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        client.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/97.0.4692.71 Safari/537.36";

        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Auth-Code", InstantPayout.X_Ipay_Auth_Code);
        request.AddHeader("Client-Id", InstantPayout.X_Ipay_Client_Id);
        request.AddHeader("Client-Secret", InstantPayout.X_Ipay_Client_Secret);
        request.AddHeader("Endpoint-Ip", InstantPayout.X_Ipay_Endpoint_Ip);
        request.AddHeader("Outlet-Id", OutletID);
        request.AddParameter("application/json", Json, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        return response.Content;
    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Payer
{
    public string bankId { get; set; }
    public string bankProfileId { get; set; }
    public string accountNumber { get; set; }

    public Payer()
    {
        bankId = InstantPayout.bankId;
        bankProfileId = InstantPayout.bankProfileId;
        accountNumber = InstantPayout.debit_account;
    }
}

public class Payee
{
    public string name { get; set; }
    public string accountNumber { get; set; }
    public string bankIfsc { get; set; }
}

public class RootInstantPayout
{
    public Payer payer { get; set; }
    public Payee payee { get; set; }
    public string transferMode { get; set; }
    public string transferAmount { get; set; }
    public string externalRef { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string remarks { get; set; }
    public string alertEmail { get; set; }
    public string purpose { get; set; }

    public string GetJson()
    {
        RootInstantPayout ObjData = new RootInstantPayout();
        ObjData.payer = this.payer;
        ObjData.payee = this.payee;
        ObjData.transferMode = this.transferMode;
        ObjData.transferAmount = this.transferAmount;
        ObjData.externalRef = this.externalRef;
        ObjData.latitude = this.latitude;
        ObjData.longitude = this.longitude;
        ObjData.remarks = this.remarks;
        ObjData.alertEmail = "bobo.ksh@gmail.com";
        ObjData.purpose = "OTHERS";
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }
}

public class AccountVerifyInstantPay
{
    public Payee payee { get; set; }
    public string externalRef { get; set; }
    public string consent { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string isCached { get; set; }

    public AccountVerifyInstantPay()
    {
        isCached = "1";
        consent = "Y";
    }

    public string GetJson()
    {
        AccountVerifyInstantPay ObjData = new AccountVerifyInstantPay();
        ObjData.payee = this.payee;
        ObjData.externalRef = this.externalRef;
        ObjData.consent = this.consent;
        ObjData.externalRef = this.externalRef;
        ObjData.latitude = this.latitude;
        ObjData.longitude = this.longitude;
        ObjData.isCached = this.isCached;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }
}

public class UPIVerifyInstantPay
{
    public Payee payee { get; set; }
    public string externalRef { get; set; }
    public string consent { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string isCached { get; set; }

    public UPIVerifyInstantPay()
    {
        isCached = "1";
        consent = "Y";
    }

    public string GetJson()
    {
        UPIVerifyInstantPay ObjData = new UPIVerifyInstantPay();
        ObjData.payee = this.payee;
        ObjData.externalRef = this.externalRef;
        ObjData.consent = this.consent;
        ObjData.externalRef = this.externalRef;
        ObjData.latitude = this.latitude;
        ObjData.longitude = this.longitude;
        ObjData.isCached = this.isCached;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }
}

public class CheckStatusInstantPay
{
    public string transactionDate { get; set; }
    public string externalRef { get; set; }
    public string source { get; set; }

    public string GetJson()
    {
        CheckStatusInstantPay ObjData = new CheckStatusInstantPay();
        ObjData.transactionDate = this.transactionDate;
        ObjData.externalRef = this.externalRef;
        ObjData.source = this.source;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }
}

public class SignupInitiate
{
    public string mobile { get; set; }
    public string pan { get; set; }
    public string email { get; set; }
    public string aadhaar { get; set; }
    public string bankAccountNo { get; set; }
    public string bankIfsc { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string consent { get; set; } = "Y";

    public string GetJson()
    {
        SignupInitiate ObjData = new SignupInitiate();
        ObjData.mobile = this.mobile;
        ObjData.pan = this.pan;
        ObjData.email = this.email;
        ObjData.aadhaar = this.aadhaar;
        ObjData.bankAccountNo = this.bankAccountNo;
        ObjData.bankIfsc = this.bankIfsc;
        ObjData.latitude = this.latitude;
        ObjData.longitude = this.longitude;
        ObjData.consent = this.consent;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }
}

public class SignupOTPVerify
{
    public string otpReferenceID { get; set; }
    public string otp { get; set; }
    public string hash { get; set; }

    public string GetJson()
    {
        SignupOTPVerify signupOTPVerify = new SignupOTPVerify();
        signupOTPVerify.otpReferenceID = this.otpReferenceID;
        signupOTPVerify.otp = this.otp;
        signupOTPVerify.hash = this.hash;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(signupOTPVerify);
        return jsonString;
    }
}

public class InstantPayBEBody
{
    public string encryptedAadhaar { get; set; }
    public string dc { get; set; }
    public string ci { get; set; }
    public string hmac { get; set; }
    public string dpId { get; set; }
    public string mc { get; set; }
    public string pidDataType { get; set; }
    public string sessionKey { get; set; }
    public string mi { get; set; }
    public string rdsId { get; set; }
    public string errCode { get; set; }
    public string errInfo { get; set; }
    public string fCount { get; set; }
    public string fType { get; set; }
    public int iCount { get; set; }
    public string iType { get; set; }
    public int pCount { get; set; }
    public string pType { get; set; }
    public string srno { get; set; }
    public string sysid { get; set; }
    public string ts { get; set; }
    public string pidData { get; set; }
    public string qScore { get; set; }
    public string nmPoints { get; set; }
    public string rdsVer { get; set; }
}

public class InstantPayBERoot
{
    public string bankiin { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string mobile { get; set; }
    public string externalRef { get; set; }
    public InstantPayBEBody biometricData { get; set; }

    public string GetJson()
    {
        InstantPayBERoot signupOTPVerify = new InstantPayBERoot();
        signupOTPVerify.bankiin = this.bankiin;
        signupOTPVerify.latitude = this.latitude;
        signupOTPVerify.longitude = this.longitude;
        signupOTPVerify.mobile = this.mobile;
        signupOTPVerify.externalRef = this.externalRef;
        signupOTPVerify.biometricData = this.biometricData;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(signupOTPVerify);
        return jsonString;
    }
}

public class InstantPayCWRoot
{
    public string bankiin { get; set; }
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string mobile { get; set; }
    public string amount { get; set; }
    public string externalRef { get; set; }
    public string captureType { get; set; }
    public InstantPayBEBody biometricData { get; set; }

    public string GetJson()
    {
        InstantPayCWRoot signupOTPVerify = new InstantPayCWRoot();
        signupOTPVerify.bankiin = this.bankiin;
        signupOTPVerify.latitude = this.latitude;
        signupOTPVerify.longitude = this.longitude;
        signupOTPVerify.mobile = this.mobile;
        signupOTPVerify.amount = this.amount;
        signupOTPVerify.externalRef = this.externalRef;
        signupOTPVerify.captureType = this.captureType;
        signupOTPVerify.biometricData = this.biometricData;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(signupOTPVerify);
        return jsonString;
    }
}

//mantra
public class Xml
{
    [JsonProperty("@version")]
    public string Version { get; set; }
}

public class Resp
{
    [JsonProperty("@errCode")]
    public string ErrCode { get; set; }

    [JsonProperty("@errInfo")]
    public string ErrInfo { get; set; }

    [JsonProperty("@fCount")]
    public string FCount { get; set; }

    [JsonProperty("@fType")]
    public string FType { get; set; }

    [JsonProperty("@nmPoints")]
    public string NmPoints { get; set; }

    [JsonProperty("@qScore")]
    public string QScore { get; set; }
}

public class Param
{
    [JsonProperty("@name")]
    public string Name { get; set; }

    [JsonProperty("@value")]
    public object Value { get; set; }
}

public class AdditionalInfo
{
    public List<Param> Param { get; set; }
}

public class DeviceInfo
{
    [JsonProperty("@dpId")]
    public string DpId { get; set; }

    [JsonProperty("@rdsId")]
    public string RdsId { get; set; }

    [JsonProperty("@rdsVer")]
    public string RdsVer { get; set; }

    [JsonProperty("@mi")]
    public string Mi { get; set; }

    [JsonProperty("@mc")]
    public string Mc { get; set; }

    [JsonProperty("@dc")]
    public string Dc { get; set; }
    public AdditionalInfo additional_info { get; set; }
}

public class Skey
{
    [JsonProperty("@ci")]
    public string Ci { get; set; }

    [JsonProperty("#text")]
    public string Text { get; set; }
}

public class DataMantra
{
    [JsonProperty("@type")]
    public string Type { get; set; }

    [JsonProperty("#text")]
    public string Text { get; set; }
}

public class PidData
{
    public Resp Resp { get; set; }
    public DeviceInfo DeviceInfo { get; set; }
    public Skey Skey { get; set; }
    public string Hmac { get; set; }
    public DataMantra Data { get; set; }
}

public class Mantra
{
    [JsonProperty("?xml")]
    public Xml Xml { get; set; }
    public PidData PidData { get; set; }
}

public class XmlMorpho
{
    [JsonProperty("@version")]
    public string Version { get; set; }
}

public class RespMorpho
{
    [JsonProperty("@errCode")]
    public string ErrCode { get; set; }

    [JsonProperty("@errInfo")]
    public string ErrInfo { get; set; }

    [JsonProperty("@fCount")]
    public string FCount { get; set; }

    [JsonProperty("@fType")]
    public string FType { get; set; }

    [JsonProperty("@nmPoints")]
    public string NmPoints { get; set; }

    [JsonProperty("@qScore")]
    public string QScore { get; set; }
}

public class ParamMorpho
{
    [JsonProperty("@name")]
    public string Name { get; set; }

    [JsonProperty("@value")]
    public object Value { get; set; }
}

public class AdditionalInfoMorpho
{
    public ParamMorpho Param { get; set; }
}

public class DeviceInfoMorpho
{
    [JsonProperty("@dpId")]
    public string DpId { get; set; }

    [JsonProperty("@rdsId")]
    public string RdsId { get; set; }

    [JsonProperty("@rdsVer")]
    public string RdsVer { get; set; }

    [JsonProperty("@mi")]
    public string Mi { get; set; }

    [JsonProperty("@mc")]
    public string Mc { get; set; }

    [JsonProperty("@dc")]
    public string Dc { get; set; }
    public AdditionalInfoMorpho additional_info { get; set; }
}

public class SkeyMorpho
{
    [JsonProperty("@ci")]
    public string Ci { get; set; }

    [JsonProperty("#text")]
    public string Text { get; set; }
}

public class DataMorpho
{
    [JsonProperty("@type")]
    public string Type { get; set; }

    [JsonProperty("#text")]
    public string Text { get; set; }
}

public class PidDataMorpho
{
    public RespMorpho Resp { get; set; }
    public DeviceInfoMorpho DeviceInfo { get; set; }
    public SkeyMorpho Skey { get; set; }
    public string Hmac { get; set; }
    public DataMorpho Data { get; set; }
}

public class RooteeeMorpho
{
    [JsonProperty("?xml")]
    public XmlMorpho Xml { get; set; }
    public PidDataMorpho PidData { get; set; }
}

public class InstantMobileChange
{
    public string existingMobileNumber { get; set; }
    public string newMobileNumber { get; set; }

    public string GetJson()
    {
        InstantMobileChange signupOTPVerify = new InstantMobileChange();
        signupOTPVerify.existingMobileNumber = this.existingMobileNumber;
        signupOTPVerify.newMobileNumber = this.newMobileNumber;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(signupOTPVerify);
        return jsonString;
    }
}

public class InstantPayOtp
{
    public string existingMobileNumber { get; set; }
    public string newMobileNumber { get; set; }
}

public class InstantPayOTPRoot
{
    public string existingMobileNumber { get; set; }
    public string newMobileNumber { get; set; }
    public InstantPayOtp otp { get; set; }
    public string GetJson()
    {
        InstantPayOTPRoot signupOTPVerify = new InstantPayOTPRoot();
        signupOTPVerify.existingMobileNumber = this.existingMobileNumber;
        signupOTPVerify.newMobileNumber = this.newMobileNumber;
        signupOTPVerify.otp = this.otp;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(signupOTPVerify);
        return jsonString;
    }

}

//Mobile BE Response
public class BEMobileResponse
{
    public bool status { get; set; }
    public string message { get; set; }
    public string ackno { get; set; }
    public string amount { get; set; }
    public string balanceamount { get; set; }
    public string bankrrn { get; set; }
    public string bankiin { get; set; }
    public string response_code { get; set; }
    public string errorcode { get; set; }
    public string clientrefno { get; set; }
    public string last_aadhar { get; set; }
    public string name { get; set; }
    public string OrderID { get; set; }
    public string STID { get; set; }
    public string Commission { get; set; }
    public string BCCode { get; set; }
    public string BCName { get; set; }
    public string Aadhar { get; set; }
    public string TransDate { get; set; }
    public string TransactionID { get; set; }

    public string GetJson()
    {
        BEMobileResponse signupOTPVerify = new BEMobileResponse();
        signupOTPVerify.status = this.status;
        signupOTPVerify.message = this.message;
        signupOTPVerify.ackno = this.ackno;
        signupOTPVerify.amount = this.amount;
        signupOTPVerify.balanceamount = this.balanceamount;
        signupOTPVerify.bankrrn = this.bankrrn;
        signupOTPVerify.bankiin = this.bankiin;
        signupOTPVerify.response_code = this.response_code;
        signupOTPVerify.errorcode = this.errorcode;
        signupOTPVerify.clientrefno = this.clientrefno;
        signupOTPVerify.last_aadhar = this.last_aadhar;
        signupOTPVerify.name = this.name;
        signupOTPVerify.OrderID = this.OrderID;
        signupOTPVerify.STID = this.STID;
        signupOTPVerify.Commission = this.Commission;
        signupOTPVerify.BCCode = this.BCCode;
        signupOTPVerify.BCName = this.BCName;
        signupOTPVerify.Aadhar = this.Aadhar;
        signupOTPVerify.TransDate = this.TransDate;
        signupOTPVerify.TransactionID = this.TransactionID;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(signupOTPVerify);
        return jsonString;
    }
}

public class MinistatementBackResponsemobile
{
    public string date { get; set; }
    public string txnType { get; set; }
    public string amount { get; set; }
    public string narration { get; set; }
}

public class MinistatementBackResponsemobileRoot
{
    public bool status { get; set; }
    public string ackno { get; set; }
    public string datetime { get; set; }
    public string balanceamount { get; set; }
    public string bankrrn { get; set; }
    public string bankiin { get; set; }
    public string message { get; set; }
    public string errorcode { get; set; }
    public List<MinistatementBackResponsemobile> ministatement { get; set; }
    public List<object> ministatementlist { get; set; }
    public string response_code { get; set; }
    public string last_aadhar { get; set; }
    public string name { get; set; }
    public string clientrefno { get; set; }
    public string OrderID { get; set; }
    public string STID { get; set; }
    public string Commission { get; set; }

    public string BCCode { get; set; }
    public string BCName { get; set; }
    public string Aadhar { get; set; }
    public string TransDate { get; set; }
    public string TransactionID { get; set; }


    public string GetJson()
    {
        MinistatementBackResponsemobileRoot signupOTPVerify = new MinistatementBackResponsemobileRoot();
        signupOTPVerify.status = this.status;
        signupOTPVerify.message = this.message;
        signupOTPVerify.ackno = this.ackno;
        signupOTPVerify.balanceamount = this.balanceamount;
        signupOTPVerify.ministatement = this.ministatement;
        signupOTPVerify.bankrrn = this.bankrrn;
        signupOTPVerify.bankiin = this.bankiin;
        signupOTPVerify.response_code = this.response_code;
        signupOTPVerify.errorcode = this.errorcode;
        signupOTPVerify.clientrefno = this.clientrefno;
        signupOTPVerify.last_aadhar = this.last_aadhar;
        signupOTPVerify.name = this.name;
        signupOTPVerify.OrderID = this.OrderID;
        signupOTPVerify.STID = this.STID;
        signupOTPVerify.Commission = this.Commission;
        signupOTPVerify.BCCode = this.BCCode;
        signupOTPVerify.BCName = this.BCName;
        signupOTPVerify.Aadhar = this.Aadhar;
        signupOTPVerify.TransDate = this.TransDate;
        signupOTPVerify.TransactionID = this.TransactionID;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(signupOTPVerify);
        return jsonString;
    }
}

public class ActionKYVRoot
{
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string type { get; set; }

    public BiometricData biometricData { get; set; }

    public string GetJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class ActionLoginRoot
{
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string externalRef { get; set; }
    public string type { get; set; }
    public string captureType { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string serviceType { get; set; } = null;
    public BiometricData biometricData { get; set; }

    public string GetJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class ActionLoginRootRemitterKYC
{
    public string latitude { get; set; }
    public string longitude { get; set; }
    public string externalRef { get; set; }
    public string referenceKey { get; set; }
    public string mobileNumber { get; set; }
    public string captureType { get; set; }
    public string consentTaken { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string serviceType { get; set; } = null;
    public BiometricDataRemitterKYC biometricData { get; set; }

    public string GetJson()
    {
        return JsonConvert.SerializeObject(this);
    }
}

public class BiometricData
{
    public string encryptedAadhaar { get; set; }
    public string dc { get; set; }
    public string ci { get; set; }
    public string hmac { get; set; }
    public string dpId { get; set; }
    public string mc { get; set; }
    public string pidDataType { get; set; }
    public string sessionKey { get; set; }
    public string mi { get; set; }
    public string rdsId { get; set; }
    public string errCode { get; set; }
    public string errInfo { get; set; }
    public string fCount { get; set; }
    public string fType { get; set; }
    public int iCount { get; set; }
    public string iType { get; set; }
    public int pCount { get; set; }
    public string pType { get; set; }
    public string srno { get; set; }
    public string sysid { get; set; }
    public string ts { get; set; }
    public string pidData { get; set; }
    public string qScore { get; set; }
    public string nmPoints { get; set; }
    public string rdsVer { get; set; }
}

public class BiometricDataRemitterKYC
{
    public string ci { get; set; }
    public string hmac { get; set; }
    public string pidData { get; set; }
    public string ts { get; set; }
    public string dc { get; set; }
    public string mi { get; set; }
    public string dpId { get; set; }
    public string mc { get; set; }
    public string rdsId { get; set; }
    public string rdsVer { get; set; }
    public string srno { get; set; }
    public string Skey { get; set; }
    public string pidOptionWadh { get; set; }
}