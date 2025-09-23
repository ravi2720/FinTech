using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class API_LapuAPI : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string returnVal = "";
            string reqType = "";
            string baseURL = "";
            if (Request.QueryString["uid"] != null || Request.QueryString["pin"] != null)
            {
                if (Request.QueryString["reqType"] != null)
                {
                    reqType = Request.QueryString["reqType"].ToString();
                }
                if (reqType.ToLower() == "rc")
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["operator"]))
                    {
                        string UserID = Request.QueryString["uid"].ToString();
                        string Token = Request.QueryString["pin"].ToString();
                        string Account = Request.QueryString["number"].ToString();
                        string SPKey = Request.QueryString["Operator"].ToString();
                        string Amount = Request.QueryString["amount"].ToString();
                        string APIRequestID = Request.QueryString["usertx"].ToString();
                        baseURL = "https://mrobotics.in/api/recharge_get?api_token=" + Token + "&company_id=" + SPKey + "&mobile_no=" + Account + "&amount=" + Amount + "&order_id=" + APIRequestID + "&is_stv=false";
                        string result = apicall(baseURL);
                        if (result.Trim() != "")
                        {
                            JObject ResultData = JObject.Parse(result);
                            if (ResultData["error"] == null)
                            {
                                if (Convert.ToString(ResultData["status"]).Trim().ToUpper() == "SUCCESS")
                                {
                                    returnVal = "SUCCESS" + "," + " " + "," + Convert.ToString(ResultData["user_id"]).ToString() + "," + Convert.ToString(ResultData["tnx_id"].ToString()) + ",";
                                }
                                else if (Convert.ToString(ResultData["status"]).Trim().ToUpper() == "PENDING")
                                {
                                    if (ResultData["response"].ToString().ToString().ToUpper() == "TRANSACTION ID IS DUPLICATE.")
                                    {
                                        returnVal = "FAILED" + "," + ResultData["response"].ToString() + "," + Convert.ToString(ResultData["user_id"]).ToString() + "," + Convert.ToString(ResultData["tnx_id"].ToString()) + ",";
                                    }
                                    else
                                    {
                                        returnVal = "PENDING" + "," + " " + "," + Convert.ToString(ResultData["user_id"]).ToString() + "," + Convert.ToString(ResultData["tnx_id"].ToString()) + ",";
                                    }
                                }
                                else if (Convert.ToString(ResultData["status"]).Trim().ToUpper() == "FAILURE")
                                {
                                    returnVal = "FAILED" + "," + " " + "," + Convert.ToString(ResultData["user_id"]).ToString() + "," + Convert.ToString(ResultData["tnx_id"].ToString()) + ",";
                                }
                                else
                                {
                                    returnVal = "PENDING" + "," + " " + "," + Convert.ToString(ResultData["user_id"]).ToString() + "," + Convert.ToString(ResultData["tnx_id"].ToString()) + ",";
                                }
                            }
                            else
                            {
                                returnVal = "Failed" + "," + ResultData["errorMessage"].ToString() + "," + " " + "," + " " + ",";
                            }
                        }
                        else
                        {
                            returnVal = "Failed" + ",Contact To mrobotics," + " " + "," + " " + ",";

                        }
                        Response.Write(returnVal);
                    }
                    else
                    {
                        returnVal = "Failed" + "," + "Operator Not Supported" + "," + " " + "," + " " + ",";
                        Response.Write(returnVal);
                    }
                }
            }

        }
    }

    public string apicall(string url)
    {

        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        try
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter("text/plain", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response.Content;
        }
        catch (Exception ex)
        {
            return "0";
        }
    }
}