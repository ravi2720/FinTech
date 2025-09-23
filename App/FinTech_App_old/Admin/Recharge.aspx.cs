
















using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class API_Recharge : System.Web.UI.Page
{
    cls_connection Objconnection = new cls_connection();
    cls_myMember clsm = new cls_myMember();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RechargeResponse rechargeRespons = new RechargeResponse();
            if (Request.QueryString["APIID"] != null && Request.QueryString["Token"] != null && Request.QueryString["number"] != null && Request.QueryString["operator"] != null && Request.QueryString["circle"] != null && Request.QueryString["amount"] != null && Request.QueryString["ClientID"] != null && Request.QueryString["Format"] != null)
            {
                string APIID = Request.QueryString["APIID"].Trim();
                string Token = Request.QueryString["Token"].Trim();
                string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                string number = Request.QueryString["number"].Trim();
                string @operator = Request.QueryString["operator"].Trim();
                string circle = Request.QueryString["circle"].Trim();
                string amount = Request.QueryString["amount"].Trim();
                string ClientID = Request.QueryString["ClientID"].Trim();
                string Format = Request.QueryString["Format"].Trim();
                DataTable dtValid = Objconnection.select_data_dt("select * from Member where Loginid='" + APIID + "'");// Authenticate.VelidateData(APIID, Token, ipAddress, Constant.RechargeID);
                if (Request.QueryString["Type"] == null)
                {
                    if (dtValid.Rows.Count > 0)
                    {
                        if (dtValid.Rows.Count > 0)
                        {
                            DataTable dtService = Objconnection.select_data_dt("select * from Operator where OperatorCode='" + @operator + "' and IsActive=1");
                            if (dtService.Rows.Count > 0)
                            {
                                if (dtService.Rows[0]["ServiceID"].ToString() == "1")
                                {
                                    if (number.Length != 10)
                                    {
                                        rechargeRespons.STID = "";
                                        rechargeRespons.Status = "FAILED";
                                        rechargeRespons.OperatorRef = "";
                                        rechargeRespons.Message = "Please Enter Valid Number";
                                        rechargeRespons.ClientID = ClientID;
                                        Response.Write(rechargeRespons.GetResult);
                                        return;
                                    }
                                }
                                Int32 CountClinetID = Objconnection.select_data_scalar_int("select count(*) from tblRecharge_History where ClientID='" + ClientID + "'");
                                if (CountClinetID == 0)
                                {
                                    decimal Balance = Convert.ToDecimal(Objconnection.select_data_scalar_string("select Balance from TBL_EWALLETBALANCE where msrno=" + dtValid.Rows[0]["msrno"].ToString() + ""));
                                    if (Balance > Convert.ToDecimal(amount))
                                    {
                                        string TransID = Cyrus_GetTransactionID_New();
                                        string Result = clsm.unikRecharge(0, Convert.ToInt32(dtValid.Rows[0]["Msrno"].ToString()), number, "", Convert.ToDecimal(amount), Convert.ToInt32(dtService.Rows[0]["ID"].ToString()), Convert.ToInt32(19), TransID, "", "", "Queued", ipAddress, ipAddress, "", "", "", "WEB", "", 0, 0, "", ClientID);
                                        Response.Write(Result);
                                    }
                                    else
                                    {
                                        rechargeRespons.STID = "";
                                        rechargeRespons.Status = "FAILED";
                                        rechargeRespons.OperatorRef = "";
                                        rechargeRespons.Message = "Low Balance";
                                        rechargeRespons.ClientID = ClientID;
                                        Response.Write(rechargeRespons.GetResult);
                                        return;
                                    }
                                }
                                else
                                {
                                    rechargeRespons.STID = "";
                                    rechargeRespons.Status = "FAILED";
                                    rechargeRespons.OperatorRef = "";
                                    rechargeRespons.Message = "Pass Uniue ClientID";
                                    rechargeRespons.ClientID = ClientID;
                                    Response.Write(rechargeRespons.GetResult);
                                    return;
                                }
                            }
                            else
                            {
                                rechargeRespons.STID = "";
                                rechargeRespons.Status = "FAILED";
                                rechargeRespons.OperatorRef = "";
                                rechargeRespons.Message = "Operator not found";
                                rechargeRespons.ClientID = ClientID;
                                Response.Write(rechargeRespons.GetResult);
                                return;
                            }
                        }
                        else
                        {
                            rechargeRespons.STID = "";
                            rechargeRespons.Status = "FAILED";
                            rechargeRespons.OperatorRef = "";
                            rechargeRespons.Message = dtValid.Rows[0]["Mess"].ToString();
                            rechargeRespons.ClientID = ClientID;
                            Response.Write(rechargeRespons.GetResult);
                            return;
                        }
                    }
                    else
                    {
                        rechargeRespons.STID = "";
                        rechargeRespons.Status = "FAILED";
                        rechargeRespons.OperatorRef = "";
                        rechargeRespons.Message = dtValid.Rows[0]["ID"].ToString();
                        rechargeRespons.ClientID = dtValid.Rows[0]["ClientID"].ToString();
                        Response.Write(rechargeRespons.GetResult);
                        return;
                    }

                }
                else
                {
                    string Balance = Objconnection.select_data_scalar_string("select balance from TBL_EWALLETBALANCE where msrno=" + dtValid.Rows[0]["Msrno"].ToString() + "");
                    Response.Write(Balance + ",");
                    return;
                }
            }
            else if (Request.QueryString["Type"] == "Bal" && Request.QueryString["APIID"] != null && Request.QueryString["Token"] != null)
            {
                string APIID = Request.QueryString["APIID"].Trim();
                string Token = Request.QueryString["Token"].Trim();
                string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                DataTable dtValid = Objconnection.select_data_dt("select * from Member where Loginid='" + APIID + "'");//Authenticate.VelidateData(APIID, Token, ipAddress, Constant.RechargeID);

                if (dtValid.Rows.Count > 0)
                {
                    //if (dtValid.Rows[0]["ID"].ToString() == "1")
                    {
                        string Balance = Objconnection.select_data_scalar_string("select balance from TBL_EWALLETBALANCE where msrno=" + dtValid.Rows[0]["Msrno"].ToString() + "");
                        Response.Write(Balance + ",");
                        return;
                    }
                    //else
                    //{
                    //    Response.Write(dtValid.Rows[0]["mess"].ToString());
                    //    return;
                    //}
                }


            }
            else if (Request.QueryString["Type"] == "checkstatus" && Request.QueryString["APIID"] != null && Request.QueryString["Token"] != null && Request.QueryString["ClientID"] != null)
            {
                string APIID = Request.QueryString["APIID"].Trim();
                string Token = Request.QueryString["Token"].Trim();
                string ClientID = Request.QueryString["ClientID"].Trim();
                string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                DataTable dtValid = Objconnection.select_data_dt("select * from Member where Loginid='" + APIID + "'");//Authenticate.VelidateData(APIID, Token, ipAddress, Constant.RechargeID);

                if (dtValid.Rows.Count > 0)
                {
                    if (dtValid.Rows.Count > 0)
                    {
                        DataTable dtStat = Objconnection.select_data_dt("select * from tblRecharge_History with(nolock) where ClientID='" + ClientID + "'");
                        if (dtStat.Rows.Count > 0)
                        {
                            rechargeRespons.STID = dtStat.Rows[0]["TransID"].ToString();
                            rechargeRespons.Status = dtStat.Rows[0]["Status"].ToString();
                            rechargeRespons.OperatorRef = dtStat.Rows[0]["APIMessage"].ToString();
                            rechargeRespons.Message = dtStat.Rows[0]["APIErrorCode"].ToString();
                            rechargeRespons.ClientID = dtStat.Rows[0]["ClientID"].ToString();
                            Response.Write(rechargeRespons.GetResult);
                            return;
                        }
                        else
                        {
                            rechargeRespons.STID = "";
                            rechargeRespons.Status = "FAILED";
                            rechargeRespons.OperatorRef = "";
                            rechargeRespons.Message = "Not Found";
                            rechargeRespons.ClientID = ClientID;
                            Response.Write(rechargeRespons.GetResult);
                            return;
                        }
                       
                    }
                    else
                    {
                        Response.Write(dtValid.Rows[0]["mess"].ToString());
                        return;
                    }
                }


            }
            else if (Request.QueryString["Type"] == "oplist" && Request.QueryString["APIID"] != null && Request.QueryString["Token"] != null && Request.QueryString["Category"] != null)
            {
                string Category = Request.QueryString["Category"].ToString();
                DataTable dt = Objconnection.select_data_dt("select op.Name,OperatorCode,sr.Name as Category from Operator op inner join Service sr on sr.id = Op.ServiceID where sr.Name=iif('" + Category + "'='',sr.Name,'" + Category + "')");
                Response.Write(ConvertDataTabletoString(dt)); ;
            }
            else
            {
                rechargeRespons.STID = "";
                rechargeRespons.Status = "FAILED";
                rechargeRespons.OperatorRef = "";
                rechargeRespons.Message = "Please Pass All Data";
                rechargeRespons.ClientID = "";
                Response.Write(rechargeRespons.GetResult);
                return;
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
        string response = "{\"code\":\"TXN\",\"status\":\"success\",\"mess\":\"success\",\"data\": " + json + "}";
        Response.ContentType = "application/json";
        return response;
    }
    public string Cyrus_GetTransactionID_New()
    {
        Random rnd = new Random();
        Int64 month = rnd.Next(10000, 99999);
        month = Convert.ToInt64(month.ToString() + Convert.ToString(rnd.Next(10000, 99999)));
        int n = Objconnection.select_data_scalar_int("Select Count(*) from tblRecharge_History where TransID='" + month.ToString() + "'");
        if (n > 0)
        {
            Cyrus_GetTransactionID_New();
        }
        return month.ToString();
    }
}

