using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NSDLPan
/// </summary>
public class NSDLPan
{
    public NSDLPan()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}



public class NSDLURL
{
    public string APIID { get; set; }
    public string Token { get; set; }
    public string MethodName { get; set; }

    [Required]
    public string mobileno { get; set; }
    [Required]
    public string PanType { get; set; }
    [Required]
    public string lastname { get; set; }
    [Required]
    public string firstname { get; set; }
    public string middlename { get; set; }
    [Required]
    public string OrderID { get; set; }
    [Required]
    public string email { get; set; }
    [Required]
    public string title { get; set; }

    public string GetJson()
    {
        NSDLURL ObjData = new NSDLURL();
        ObjData.mobileno = this.mobileno;
        ObjData.lastname = this.lastname;
        ObjData.firstname = this.firstname;
        ObjData.middlename = this.middlename;
        ObjData.APIID = SONITechnoCredentrial.APIID;
        ObjData.OrderID = this.OrderID;
        ObjData.PanType = this.PanType;
        ObjData.MethodName = this.MethodName;
        ObjData.Token = SONITechnoCredentrial.Token;
        ObjData.email = this.email;
        ObjData.title = this.title;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }

}


public class NSDLVerification
{
    public string APIID { get; set; }
    public string Token { get; set; }
    public string MethodName { get; set; }
    

    [Required]
    public string OrderID { get; set; }
    [Required]
    public string encdata { get; set; }
    public string GetJson()
    {
        NSDLVerification ObjData = new NSDLVerification();
        ObjData.APIID = SONITechnoCredentrial.APIID;
        ObjData.Token = SONITechnoCredentrial.Token;
        ObjData.OrderID = this.OrderID;
        ObjData.encdata = this.encdata;
        ObjData.MethodName = this.MethodName;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }

}