using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AccountStatement
/// </summary>
public class AccountStatement
{
    public static string GetAccountStateMent(ICICIPayoutRequest iCICIPayoutRequest)
    {
        CypherExample cypherExample = new CypherExample();

        string sRequest = PayoutICICI.invokeRequest(iCICIPayoutRequest.GetJson(), "AccountStatement", "POST");
        JObject DataJobect = JObject.Parse(sRequest);
        byte[] IV = CypherExample.HexStringToByte(CypherExample.initialisationVector);
        string Key = CryptographyHelper.Decrypt(DataJobect["encryptedKey"].ToString());
        CypherExample.encryptionKey = Key;
        string DataMain = cypherExample.DecryptDataNew(DataJobect["encryptedData"].ToString(), Key);
        return DataMain;
    }

    public static string GetBalanceInquiry(ICICIBalanceFetch iCICIBalanceFetch)
    {
        cls_connection objConnection = new cls_connection();
        string sRequest = PayoutICICI.invokeRequest(iCICIBalanceFetch.GetJson(), "BalanceInquiry", "POST");
        objConnection.update_data("insert into tbl_Apeslog(url)values('" + sRequest + "')");

        string _decrypt = CryptographyHelper.Decrypt(sRequest);// PayoutICICI.deCipherRequest(_dataDecrypt);
        return _decrypt;
    }
    public static string UPI(CollectPayRequest iCICITransaction)
    {
        cls_connection objConnection = new cls_connection();
        string sRequest = PayoutICICIUPI.invokeRequest(iCICITransaction.GetJson(), "TransactionInquiry", "POST");


        string _decrypt = CryptographyHelper.Decrypt(sRequest);// PayoutICICI.deCipherRequest(_dataDecrypt);
        return _decrypt;
    }
    public static string CheckStatusUPI(CollectPayRequestCheckStatus iCICITransaction)
    {
        cls_connection objConnection = new cls_connection();
        string sRequest = PayoutICICIUPI.invokeRequestStatusCheck(iCICITransaction.GetJson(), "TransactionInquiry", "POST");


        string _decrypt = CryptographyHelper.Decrypt(sRequest);// PayoutICICI.deCipherRequest(_dataDecrypt);
        return _decrypt;
    }

    public static string QRUPI(CollectPayUPIQR iCICITransaction)
    {
        cls_connection objConnection = new cls_connection();
        string sRequest = PayoutICICIUPI.invokeRequestURLQR(iCICITransaction.GetJson(), "TransactionInquiry", "POST");


        string _decrypt = CryptographyHelper.Decrypt(sRequest);// PayoutICICI.deCipherRequest(_dataDecrypt);
        return _decrypt;
    }

    public static string QRUPICheckStatus(CollectPayUPIQRCallbackCheckStatus iCICITransaction)
    {
        cls_connection objConnection = new cls_connection();
        string sRequest = PayoutICICIUPI.invokeRequestQRUPICheckStatus(iCICITransaction.GetJson(), "TransactionInquiry", "POST");


        string _decrypt = CryptographyHelper.Decrypt(sRequest);// PayoutICICI.deCipherRequest(_dataDecrypt);
        return _decrypt;
    }


    
}