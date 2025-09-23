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

public partial class Admin_MATMPending : System.Web.UI.Page
{
    //AEPSBankBodyRequestNew aEPSBankBodyRequest = new AEPSBankBodyRequestNew();
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
            DataTable dthistory = objconnection.select_data_dt("select * from tblMicroATM_RefID where RefID='" + e.CommandArgument.ToString().Trim() + "'");
            if (dthistory.Rows.Count > 0)
            {
                //MATMCheckStatus mATMCheckStatus = new MATMCheckStatus();
                //mATMCheckStatus.reference = e.CommandArgument.ToString();
                //string Result = aEPSBankBodyRequest.CheckStatusMATM(mATMCheckStatus);
                //JObject Data = JObject.Parse(Result);
                //Int32 Val = 0;
                //if (Convert.ToBoolean(Data["status"].ToString()))
                //{
                    
                //    Val = objconnection.update_data("AddEditMATM_History "+ dthistory.Rows[0]["msrno"].ToString() + ",'"+ dthistory.Rows[0]["RefID"].ToString() + "','Success','"+Data["message"].ToString() + "','" + Data["ackno"].ToString() + "','" + Data["amount"].ToString() + "','" + Data["bankrrn"].ToString() + "','" + Data["cardnumber"].ToString() + "','" + Data["bankName"].ToString() + "','"+Result+"'");
                //    if (Val > 0)
                //    {
                //        string Memberid = objconnection.select_data_scalar_string("select loginid from member where msrno='" + dthistory.Rows[0]["msrno"].ToString() + "'");
                //        objconnection.update_data("Exec PROC_AEPS2EWALLETTRANSACTION '"+ Memberid + "','"+ Data["amount"].ToString() + "','CR','MATM TransID-"+ dthistory.Rows[0]["RefID"].ToString() + "','Web','"+ Data["bankrrn"].ToString() + "'");
                //        objconnection.update_data("update tblMicroATM_RefID set status='Success' where RefID='" + e.CommandArgument.ToString().Trim() + "'");

                //        objconnection.update_data("ProcMATM_DistributeCommission "+ dthistory.Rows[0]["msrno"].ToString() + ", "+ Data["amount"].ToString() + ", "+ Data["amount"].ToString() + ", '"+ dthistory.Rows[0]["RefID"].ToString() + "', '"+System.Guid.NewGuid().ToString().Replace("-","1")+"'");
                        
                //        ErrorShow.Success(page, Data["message"].ToString());
                //    }
                //}
                //else
                //{
                //    if (Data["message"].ToString() == "Data not Found or Transaction belong to previous date.")
                //    {
                //        Val = objconnection.update_data("AddEditMATM_History " + dthistory.Rows[0]["msrno"].ToString() + ",'" + dthistory.Rows[0]["RefID"].ToString() + "','failed','" + Data["message"].ToString() + "','','0','','','','" + Result + "'");
                //    }
                //    else
                //    {
                //        Val = objconnection.update_data("AddEditMATM_History " + dthistory.Rows[0]["msrno"].ToString() + ",'" + dthistory.Rows[0]["RefID"].ToString() + "','failed','" + Data["message"].ToString() + "','" + Data["ackno"].ToString() + "','" + Data["amount"].ToString() + "','" + Data["bankrrn"].ToString() + "','" + Data["cardnumber"].ToString() + "','" + Data["bankName"].ToString() + "','" + Result + "'");
                //    }
                //    if (Val > 0)
                //    {
                //        objconnection.update_data("update tblMicroATM_RefID set status='failed' where RefID='" + e.CommandArgument.ToString().Trim() + "'");

                //        ErrorShow.Error(page, Data["message"].ToString());
                //    }
                //}
            }


        }
        BindData();
    }
    public string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string refId = new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
        if (objconnection.select_data_scalar_int("select count(*) from tblMicroATM_RefID where RefID='" + refId + "'") == 1)
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
        string URL = "http://uat.dhansewa.com/MICROATM/GetMATMtxnStatus";
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
    public string message { get; set; }
    public string statuscode { get; set; }
    public IList<MData> Data { get; set; }
}

public class MATM_Request
{
    public string secretekey { get; set; }
    public string saltkey { get; set; }
    public string referenceid { get; set; }
}