using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_MATMPendingTransaction : System.Web.UI.Page
{
    cls_connection objconnection = new cls_connection();
    Page page;


    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        DataTable dt = new DataTable();
        dt = objconnection.select_data_dt("Proc_MicroATM_Pending");
        if (dt.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();
        }
    }

    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "CheckStatus")
        {
            MATM_RequestST objRequest = new MATM_RequestST();
            objRequest.RefID = e.CommandArgument.ToString();
            objRequest.MethodName = "checkstatus";
            string strJson = JsonConvert.SerializeObject(objRequest);
            string result = Call_API(strJson);
            if (result != "0")
            {
                //objconnection.insert_data("insert into tbl_MATMLog(Response)values('" + result + "')");
                JObject obj = JObject.Parse(result);
                if (Convert.ToString(obj["code"]).ToUpper() == "TXN")
                {                                       
                    {
                        MATM_RootObject objRoot = JsonConvert.DeserializeObject<MATM_RootObject>(result);
                        for (int i = 0; i < objRoot.Data.Count; i++)
                        {
                            //JObject obj = JObject.Parse(result);
                            string Txntype = "";
                            if (Convert.ToDecimal(objRoot.Data[i].amount) > 0) { Txntype = "CW"; } else { Txntype = "BE"; }

                            string BcId = objRoot.Data[i].bcid;
                            string TerminalId = objRoot.Data[i].pid;
                            string MerchantId = objRoot.Data[i].mid;
                            string Amount = objRoot.Data[i].amount;//0 for BE and amount for CW
                            string TxnStatus = objRoot.Data[i].status;
                            string ReferenceId = objRoot.Data[i].stanno;
                            string Pipe = "1";
                            string EndCustMobile = objRoot.Data[i].udf1;
                            string rrn = objRoot.Data[i].rrn;
                            string Timestamp = objRoot.Data[i].createdate;
                            string clientRefID = objRoot.Data[i].clientrefid;
                            string bankMessage = objRoot.Data[i].bankmessage;
                            String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                            string uniqueGUID = "";
                            string CPTransactionID = "";
                            string VendorId = "";
                            string statusCode = "000";
                            string Message = "Success";
                            string bankCode = "";
                            string InvoiceNo = "";
                            string msrno = "0";
                            DataTable dtMemberATM = new DataTable();
                            DataTable dtHistory = new DataTable();
                            dtHistory = objconnection.select_data_dt("select * from tblMicroATM_RefID_Maha where RefID='" + e.CommandArgument.ToString() + "'");
                            dtMemberATM = objconnection.select_data_dt("select * from tbl_MicroATMTransaction where ClientRefId='" + e.CommandArgument.ToString() + "'");
                            if (dtMemberATM.Rows.Count == 0)
                            {
                                msrno = dtHistory.Rows[0]["msrno"].ToString();
                                uniqueGUID = System.Guid.NewGuid().ToString();
                                CPTransactionID = RandomString(8);
                                VendorId = RandomString(8);
                                string number = "CY" + String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);

                                int valid = objconnection.insert_data("insert into tbl_MicroATMTransaction (MsrNo,TransactionType,TimeStamp,BCID,TerminalID,MerchantID,ReferenceID,Amount,TransactionStatus,EndCustomerMobile,Pipe,CPTransactionID,StatusCode,Message,VendorId,CYTransID,ClientRefId,BankRRNNumber,BankMessage,ResponseData) Values (" + msrno + ",'" + Txntype + "','" + Timestamp + "','" + BcId + "','" + TerminalId + "','" + MerchantId + "','" + ReferenceId + "','" + Amount + "','" + TxnStatus + "','" + EndCustMobile + "','" + Pipe + "','" + CPTransactionID + "','" + statusCode + "','" + Message + "','" + VendorId + "','" + number + "','" + e.CommandArgument.ToString() + "','" + rrn + "','" + bankMessage + "','" + result + "')");
                                if (TxnStatus.ToUpper() == "SUCCESS")
                                {
                                    string MemberID = objconnection.select_data_scalar_string("select loginid from Member where msrno=" + msrno + "");
                                    string Narration = "MATM TransID-" + e.CommandArgument.ToString();
                                    objconnection.update_data("Exec ProcMLM__EWalletTransaction_AEPS2 '" + MemberID + "', '" + Amount + "', 'Cr', '" + Narration + "', 'WEB', '" + e.CommandArgument.ToString() + "'");
                                    string GUIDNew = System.Guid.NewGuid().ToString().Replace("-", "5");
                                    objconnection.update_data("Exec ProcMATM_DistributeCommission " + msrno + ", " + Amount + ", " + Amount + ", '" + e.CommandArgument.ToString() + "', '" + GUIDNew + "'");
                                }
                                if (valid > 0)
                                {
                                    objconnection.select_data_dt("update tblMicroATM_RefID_Maha set Status='" + TxnStatus + "' where RefID='" + e.CommandArgument.ToString() + "'");
                                }
                                BindData();
                            }
                            else
                            {
                                DataTable dtData = objconnection.select_data_dt("select * from tbl_MicroATMTransaction where ClientRefId='" + e.CommandArgument.ToString() + "'");
                                if (dtData.Rows.Count > 0)
                                {
                                    int k = objconnection.update_data("update tbl_MicroATMTransaction set MsrNo=" + msrno + ", TransactionStatus='" + TxnStatus + "',BankCode='" + bankCode + "',BankRRNNumber='" + rrn + "',InvoiceNo='" + InvoiceNo + "',Message='" + Message + "',ResponseData='" + result + "' where ClientRefId='" + e.CommandArgument.ToString() + "'");
                                    if (k > 0)
                                    {
                                        objconnection.select_data_dt("update tblMicroATM_RefID_Maha set Status='" + TxnStatus + "' where RefID='" + e.CommandArgument.ToString() + "'");
                                    }

                                }
                                BindData();
                            }
                            ErrorShow.Success(page: page, Message: TxnStatus);


                        }
                    }

                    // Response.Write(response);                    
                }
                else
                {
                    if (Convert.ToString(obj["code"]).ToUpper() == "TRANSACTION NOT FOUND")
                    {
                        objconnection.select_data_dt("update tblMicroATM_RefID_Maha set Status='Transaction Not Found' where RefID='" + e.CommandArgument.ToString() + "'");
                        string response = "{\"statuscode\":\"ERR\",\"status\":\"Transaction Not Found\",\"RefId\":\"" + e.CommandArgument.ToString() + "\"}";
                        BindData();
                        ErrorShow.Success(page: page, Message: "Transaction Not Found");
                    }
                    else
                    {
                        ErrorShow.Success(page: page, Message: Convert.ToString(obj["mess"]));

                    }
                }
                


            }
            else
            {

            }
        }

    }
    public string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string refId = new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
        if (objconnection.select_data_scalar_int("select count(*) from tblMicroATM_RefID_Maha where RefID='" + refId + "'") == 1)
        {
            refId = RandomString(12);
        }
        return refId;
    }

    public string copyFrom(NameValueCollection a)
    {
        IDictionary<string, string> dict = new Dictionary<string, string>();
        foreach (var k in a.AllKeys)
        {
            dict.Add(k, a[k]);
        }
        string myJsonString = (new JavaScriptSerializer()).Serialize(dict);

        return myJsonString;

    }
    public string Call_API(string requestJson)
    {
        string URL = SONITechnoCredentrial.MATMUTL;
        string response = string.Empty;
        try
        {

            JavaScriptSerializer js = new JavaScriptSerializer();
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] data = encoder.GetBytes(requestJson);
            string jsrequest = js.Serialize(requestJson);
            HttpWebRequest requestObj = (HttpWebRequest)WebRequest.Create(URL);
            requestObj.Method = "POST";
            requestObj.Accept = "application/json";
            requestObj.ContentType = "application/json";
            requestObj.Timeout = 180000;
            requestObj.GetRequestStream().Write(data, 0, data.Length);
            HttpWebResponse webResponse = (HttpWebResponse)requestObj.GetResponse();
            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
            {
                response = sr.ReadToEnd();
            }
            requestJson = URL + "  " + requestJson;
            response = response.Replace("'", "''");
        }
        catch (Exception ex)
        {
            //requestJson = URL + "  " + requestJson;

            response = "0";
        }
        return response;
    }
}


public class MData
{
    public string bcid { get; set; }
    public string mid { get; set; }
    public string pid { get; set; }
    public string stanno { get; set; }
    public string amount { get; set; }
    public string createdate { get; set; }
    public string status { get; set; }
    public string bankmessage { get; set; }
    public string rrn { get; set; }
    public string cardno { get; set; }
    public string refunddate { get; set; }
    public string clientrefid { get; set; }
    public string udf1 { get; set; }
    public string udf2 { get; set; }
    public string udf3 { get; set; }
    public string remarks { get; set; }
}

public class MATM_RootObject
{
    public string code { get; set; }
    public string mess { get; set; }
    public string status { get; set; }
    public IList<MData> Data { get; set; }
}

