using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for PaysPrint
/// </summary>
public class PaysPrint
{

    public static string JWTencryptionKey = "";
    public static string AuthKey = "=";
    public static string AESKEY = "";
    public static string AESIV = "";
    public static string PARTNERID = "";
    public static string Prefix = "";
    public static string Authorisedsign = "=";
    public static string URL = "https://paysprint.in/service-api/api/v1/service/";
    public static string OnBoradCall = "http://mysaralpay.com/API/MerchantOnboard.aspx";

    public PaysPrint()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}


public class PaysPrintPostCall
{
    public static string PostCall(string url, string Token)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        var client = new RestClient(PaysPrint.URL + url);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Token", Token);
        request.AddHeader("Authorisedkey", PaysPrint.AuthKey);

        IRestResponse response = client.Execute(request);

        return response.Content;
    }

    public static string PostCallBodyEncrption(string url, string Token, string Body)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        var client = new RestClient(PaysPrint.URL + url);
        cls_connection objConnection = new cls_connection();
       
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Token", Token);
        request.AddHeader("Authorisedkey", PaysPrint.AuthKey);
        request.AddParameter("application/json", "{\"body\":\"" + Body + "\"}", ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        objConnection.update_data("insert into tbl_Apeslog(url)values('" + response.Content.ToString() + "')");

        return response.Content.ToString();
    }
    public static string PostCallWithBody(string url, string Token, string body)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
        var client = new RestClient(PaysPrint.URL + url);
        client.Timeout = -1;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Accept", "application/json");
        request.AddHeader("Token", Token);
       request.AddHeader("Authorisedkey", PaysPrint.AuthKey);
        request.AddParameter("application/json", body, ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);

        return response.Content;
    }
}

public class PaysPrintDMTCheckCustomer : IValidatableObject
{
    [Required(ErrorMessage = "Mobile is required")]
    [MinLength(10, ErrorMessage = "Please Enter Valid Mobile No")]
    [MaxLength(10, ErrorMessage = "Please Enter Valid Mobile No")]
    public string mobile { get; set; }
    [Required]
    public string bank3_flag { get; set; }

    public PaysPrintDMTCheckCustomer()
    {
        bank3_flag = "no";
    }

    public string GetJson()
    {
        PaysPrintDMTCheckCustomer ObjData = new PaysPrintDMTCheckCustomer();
        ObjData.mobile = this.mobile;
        ObjData.bank3_flag = this.bank3_flag;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(mobile))
        {
            yield return new ValidationResult("Can't Null");
        }
    }
}


public class PaysPrintDMTCustomerRegister : IValidatableObject
{
    [Required(ErrorMessage = "Mobile is required")]
    [MinLength(10, ErrorMessage = "Please Enter Valid Mobile No")]
    [MaxLength(10, ErrorMessage = "Please Enter Valid Mobile No")]
    public string mobile { get; set; }
    [Required(ErrorMessage = "firstname is required")]
    [MinLength(3, ErrorMessage = "Please Enter Valid Firstname")]
    public string firstname { get; set; }
    [Required(ErrorMessage = "lastname is required")]
    [MinLength(3, ErrorMessage = "Please Enter Valid lastname")]
    public string lastname { get; set; }
    [Required(ErrorMessage = "address is required")]
    public string address { get; set; }
    [Required(ErrorMessage = "otp is required")]
    public string otp { get; set; }
    [Required(ErrorMessage = "pincode is required")]
    [MinLength(6, ErrorMessage = "Please Enter Valid pincode")]
    [MaxLength(6, ErrorMessage = "Please Enter Valid pincode")]
    public string pincode { get; set; }
    [Required(ErrorMessage = "pincode is required")]
    public string stateresp { get; set; }
    [Required(ErrorMessage = "pincode is required")]
    public string bank3_flag { get; set; }
    [Required(ErrorMessage = "pincode is required")]
    public string dob { get; set; }
    [Required(ErrorMessage = "pincode is required")]
    public string gst_state { get; set; }
    public string GetJson()
    {
        PaysPrintDMTCustomerRegister ObjData = new PaysPrintDMTCustomerRegister();
        ObjData.mobile = this.mobile;
        ObjData.firstname = this.firstname;
        ObjData.lastname = this.lastname;
        ObjData.address = this.address;
        ObjData.otp = this.otp;
        ObjData.pincode = this.pincode;
        ObjData.stateresp = this.stateresp;
        ObjData.bank3_flag = this.bank3_flag;
        ObjData.dob = this.dob;
        ObjData.gst_state = this.gst_state;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(mobile))
        {
            yield return new ValidationResult("Can't Null");
        }
    }
}

public class DeleteBeniPaysP
{
    public string mobile { get; set; }
    public string bene_id { get; set; }

    public string GetJson()
    {
        DeleteBeniPaysP ObjData = new DeleteBeniPaysP();
        ObjData.mobile = this.mobile;
        ObjData.bene_id = this.bene_id;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }
}


public class PaysPrintDMTBeniRegister : IValidatableObject
{
    [Required(ErrorMessage = "Mobile is required")]
    [MinLength(10, ErrorMessage = "Please Enter Valid Mobile No")]
    [MaxLength(10, ErrorMessage = "Please Enter Valid Mobile No")]
    public string mobile { get; set; }
    [Required(ErrorMessage = "Benename is required")]
    public string benename { get; set; }
    [Required(ErrorMessage = "BankID is required")]
    public string bankid { get; set; }
    [Required(ErrorMessage = "Accno is required")]
    public string accno { get; set; }
    [Required(ErrorMessage = "IFSCCODE is required")]
    public string ifsccode { get; set; }
    public string verified { get; set; }
    public string gst_state { get; set; }
    public string dob { get; set; }
    public string address { get; set; }
    public string pincode { get; set; }

    public string GetJson()
    {
        PaysPrintDMTBeniRegister ObjData = new PaysPrintDMTBeniRegister();
        ObjData.mobile = this.mobile;
        ObjData.benename = this.benename;
        ObjData.bankid = this.bankid;
        ObjData.accno = this.accno;
        ObjData.ifsccode = this.ifsccode;
        ObjData.verified = this.verified;
        ObjData.gst_state = this.gst_state;
        ObjData.dob = this.dob;
        ObjData.address = this.address;
        ObjData.pincode = this.pincode;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(mobile))
        {
            yield return new ValidationResult("Can't Null");
        }
    }
}




public class PaysPrintDMTBeniList : IValidatableObject
{
    [Required(ErrorMessage = "Mobile is required")]
    [MinLength(10, ErrorMessage = "Please Enter Valid Mobile No")]
    [MaxLength(10, ErrorMessage = "Please Enter Valid Mobile No")]
    public string mobile { get; set; }
    

    public string GetJson()
    {
        PaysPrintDMTBeniList ObjData = new PaysPrintDMTBeniList();
        ObjData.mobile = this.mobile;
        
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(mobile))
        {
            yield return new ValidationResult("Can't Null");
        }
    }
}


// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class PaysPrintDMTTransfer : IValidatableObject
{
    public string mobile { get; set; }
    public string referenceid { get; set; }
    public string pipe { get; set; }
    public string pincode { get; set; }
    public string address { get; set; }
    public string dob { get; set; }
    public string gst_state { get; set; }
    public int bene_id { get; set; }
    public string txntype { get; set; }
    public decimal amount { get; set; }

    public string GetJson()
    {
        PaysPrintDMTTransfer ObjData = new PaysPrintDMTTransfer();
        ObjData.mobile = this.mobile;
        ObjData.referenceid = this.referenceid;
        ObjData.pipe = this.pipe;
        ObjData.pincode = this.pincode;
        ObjData.address = this.address;
        ObjData.dob = this.dob;
        ObjData.gst_state = this.gst_state;
        ObjData.bene_id = this.bene_id;
        ObjData.txntype = this.txntype;
        ObjData.amount = this.amount;

        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }


    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        throw new NotImplementedException();
    }
}

public class PaysAEPSOnBoard
{
    public string merchantcode { get; set; }
    public string mobile { get; set; }
    public string is_new { get; set; }
    public string email { get; set; }
    public string firm { get; set; }
    public string callback { get; set; }
    public string GetJson()
    {
        PaysAEPSOnBoard ObjData = new PaysAEPSOnBoard();
        ObjData.merchantcode = this.merchantcode;
        ObjData.mobile = this.mobile;
        ObjData.is_new = this.is_new;
        ObjData.email = this.email;
        ObjData.firm = this.firm;
        ObjData.callback = this.callback;

        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }

}

public class PaysBeniFetch
{
    public string mobile { get; set; }
    public int beneid { get; set; }

    public string GetJson()
    {
        PaysBeniFetch ObjData = new PaysBeniFetch();
        ObjData.mobile = this.mobile;
        ObjData.beneid = this.beneid;        

        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }


}

