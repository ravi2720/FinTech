using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JWTBankSendRequestNewIntegration
/// </summary>
public class JWTBankSendRequestNewIntegration
{
    public double timestamp { get; set; }
    public string partnerId { get; set; }
    public string reqid { get; set; }
    public JWTBankSendRequestNewIntegration()
    {
        //timestamp =  (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;// Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssffff"));
        //partnerId = PaysPrint.PARTNERID;
        //reqid = ApiPostCallInstantPay.GenerateExternalRef();
    }

    public void SetLiveParameter()
    {

        //timestamp = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddHHmmssffff"));
        //partnerId = PaysPrint.PARTNERID; 
        //reqid = ApiPostCallInstantPay.GenerateExternalRef();
    }

    public string GetToken()
    {
        JWTBankSendRequestNewIntegration jWTBankSendRequest = new JWTBankSendRequestNewIntegration();
        string Token = "";// JsonWebToken.Encode(jWTBankSendRequest, PaysPrint.JWTencryptionKey, JwtHashAlgorithm.HS256);
        return Token;
    }
    public string GetLiveToken()
    {
        JWTBankSendRequestNewIntegration jWTBankSendRequest = new JWTBankSendRequestNewIntegration();
        jWTBankSendRequest.SetLiveParameter();
        string Token = "";// JsonWebToken.Encode(jWTBankSendRequest, PaysPrint.JWTencryptionKey, JwtHashAlgorithm.HS256);
        return Token;
    }
}