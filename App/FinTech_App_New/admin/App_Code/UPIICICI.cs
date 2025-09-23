using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UPIICICI
/// </summary>
public class UPIICICI
{

    public string merchantId { get; set; }
    public string subMerchantId { get; set; }
    public string terminalId { get; set; }
    public string merchantTranId { get; set; }

    public UPIICICI()
    {
        merchantId = "406821";
        subMerchantId = "R400102";
        terminalId = "2342342";
        merchantTranId = "612413726581";
    }

}

public class CollectPayRequest
{
    public string payerVa { get; set; }
    public string amount { get; set; }
    public string note { get; set; }
    public string collectByDate { get; set; }
    public string merchantId { get; set; }
    public string merchantName { get; set; }
    public string subMerchantId { get; set; }
    public string subMerchantName { get; set; }
    public string terminalId { get; set; }
    public string merchantTranId { get; set; }
    public string billNumber { get; set; }

    public CollectPayRequest()
    {
        //payerVa = "8890977983@ybl";
        //amount = "5.00";
        note = "Collect Pay";
        //collectByDate = "31/01/2021 10:00 AM";
        merchantId = "406821";
        merchantName = "IDHSAAT ECOMMERCE";
        //subMerchantId = "8890977983";
        //subMerchantName = "Vipin soni";
        terminalId = "5411";
        //merchantTranId = "20503b5602318040fc0929";
        //billNumber = "20503b5602318040fc0929";
    }
    public string GetJson()
    {
        CollectPayRequest ObjData = new CollectPayRequest();
        ObjData.payerVa = this.payerVa;
        ObjData.amount = this.amount;
        ObjData.note = this.note;
        ObjData.collectByDate = this.collectByDate;
        ObjData.merchantId = this.merchantId;
        ObjData.merchantName = this.merchantName;
        ObjData.subMerchantId = this.subMerchantId;
        ObjData.subMerchantName = this.subMerchantName;
        ObjData.terminalId = this.terminalId;
        ObjData.merchantTranId = this.merchantTranId;
        ObjData.billNumber = this.billNumber;

        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;

    }
}



public class CollectPayRequestCheckStatus
{
   
    public string merchantId { get; set; }
    public string subMerchantId { get; set; }
    public string terminalId { get; set; }
    public string merchantTranId { get; set; }

    public CollectPayRequestCheckStatus()
    {       
        merchantId = "406821";
        subMerchantId = "8890977983";
        terminalId = "5411";
        merchantTranId = "RCMT20202912041321955";
    }
    public string GetJson()
    {
        CollectPayRequestCheckStatus ObjData = new CollectPayRequestCheckStatus();
      
        ObjData.merchantId = this.merchantId;
        ObjData.subMerchantId = this.subMerchantId;
        ObjData.terminalId = this.terminalId;
        ObjData.merchantTranId = this.merchantTranId;

        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;

    }
}


public class CollectPayUPIQR
{

    public string merchantId { get; set; }
    public string amount { get; set; }
    public string merchantTranId { get; set; }
    public string billNumber { get; set; }
    public string terminalId { get; set; }


    public CollectPayUPIQR()
    {
        merchantId = "400969";
        amount = "1.00";
        terminalId = "5411";
        merchantTranId = "RCMT20202912041321918";
        billNumber = "RCMT20202912041321918";
    }
    public string GetJson()
    {
        CollectPayUPIQR ObjData = new CollectPayUPIQR();

        ObjData.merchantId = this.merchantId;
        ObjData.terminalId = this.terminalId;
        ObjData.merchantTranId = this.merchantTranId;
        ObjData.billNumber = this.billNumber;
        ObjData.amount = this.amount;

        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;

    }
}



public class CollectPayUPIQRCallbackCheckStatus
{

    public string merchantId { get; set; }
    public string subMerchantId { get; set; }
    public string terminalId { get; set; }
    public string transactionType { get; set; }
    public string merchantTranId { get; set; }
    public string transactionDate { get; set; }
    public string BankRRN { get; set; }
    public string refId { get; set; }


    public CollectPayUPIQRCallbackCheckStatus()
    {
        merchantId = "406821";
        subMerchantId = "406821";
        terminalId = "5411";
        transactionType = "p";
        transactionDate = "23/01/2021";
        merchantTranId = "9113722202";
        BankRRN = "102320352763";
        refId = "MNO9113722202";
    }
    public string GetJson()
    {
        CollectPayUPIQRCallbackCheckStatus ObjData = new CollectPayUPIQRCallbackCheckStatus();

        ObjData.merchantId = this.merchantId;
        ObjData.terminalId = this.terminalId;
        ObjData.transactionType = this.transactionType;
        ObjData.merchantTranId = this.merchantTranId;
        ObjData.transactionDate = this.transactionDate;
        ObjData.merchantTranId = this.merchantTranId;
        ObjData.BankRRN = this.BankRRN;
        ObjData.subMerchantId = this.subMerchantId;

        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;

    }
}