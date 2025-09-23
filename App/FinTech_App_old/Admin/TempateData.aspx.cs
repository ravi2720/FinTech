using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Configuration;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using RestSharp;
using System.ComponentModel.DataAnnotations;

public partial class API_TempateData : System.Web.UI.Page
{
    cls_connection objConnection = new cls_connection();
    DataTable dt = new DataTable();
    cls_myMember clsm = new cls_myMember();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string values = "";
                string ipAddress = "";// CheckRequestIP();// Request.ServerVariables["REMOTE_ADDR"];
                using (var reader = new StreamReader(Request.InputStream))
                {
                    values = reader.ReadToEnd();
                }
                JObject Data = JObject.Parse(values);
                if (Data["MethodName"] != null)
                {
                    string methodName = Data["MethodName"].ToString();
                    if (methodName == "getRecharge")
                    {
                        DataTable dthis = objConnection.select_data_dt("select APIMessage,MobileNo,Amount,TransID,Status from tblRecharge_History where transid='" + Data["TransID"].ToString() + "'");
                        Response.Write(ConvertDataTabletoString(dthis));
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
    public string ConvertDataTabletoString(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return SuccessArray(serializer.Serialize(rows).ToString());
    }
    private string SuccessArray(string json)
    {
        string response = "{\"statuscode\":\"TXN\",\"status\":\"Transaction Successful\",\"data\": " + json + "}";
        return response;
    }

    public string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string refId = new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
        if (objConnection.select_data_scalar_int("select count(*) from tblMicroATM_RefID where RefID='" + refId + "'") == 1)
        {
            refId = RandomString(12);
        }
        return refId;
    }

    private void ErrorMessage(string ErrorMessage)
    {
        string response = "{\"statuscode\":\"ERR\",\"status\":\"" + ErrorMessage + "\"}";
        Response.Write(response);
        return;

    }

    private void SuccessMessage(string ErrorMessage)
    {
        string response = "{\"statuscode\":\"TXN\",\"status\":\"" + ErrorMessage + "\"}";
        Response.Write(response);
        return;

    }

}