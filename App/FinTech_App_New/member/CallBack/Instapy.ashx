<%@ WebHandler Language="C#" Class="Instapy" %>

using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class Instapy : IHttpHandler
{
    cls_connection cls = new cls_connection();
    public void ProcessRequest(HttpContext context)
    {
        using (var reader = new StreamReader(context.Request.InputStream))
        {
            string values = reader.ReadToEnd();
            cls.update_data("insert into LogData(data)values('" + context.Request.QueryString.ToString() + "')");
            if (context.Request.QueryString["ipay_id"] != null && context.Request.QueryString["agent_id"] != null)
            {
                string STID = context.Request.QueryString["agent_id"].ToString();
                string ipay_id = context.Request.QueryString["ipay_id"].ToString();
                string opr_id = context.Request.QueryString["opr_id"].ToString();
                string status = context.Request.QueryString["status"].ToString();
                string res_msg = context.Request.QueryString["res_msg"].ToString();

                string optional1 = context.Request.QueryString["optional1"].ToString();

                DataTable dthismem = cls.select_data_dt("select * from Member where mobile='" + STID.Replace("PNKI", "") + "'");
                if (dthismem.Rows.Count == 0)
                {
                    DataTable dthisMn = cls.select_data_dt("select * from MoneyTransfer where OrderID='" + STID + "' and Status='Pending'");
                    if (dthisMn.Rows.Count > 0)
                    {
                        string account_no = context.Request.QueryString["account_no"].ToString();
                        DataTable dtValid = cls.select_data_dt("select * from Member where msrno='" + dthisMn.Rows[0]["Msrno"].ToString() + "'");
                        if (dtValid.Rows.Count > 0)
                        {
                            string Narration = "DMT TransID-" + STID;
                            if (status.ToString().ToUpper() == "REFUND")
                            {
                               
                            }
                            else if (status.ToString().ToUpper() == "SUCCESS")
                            {
                                cls.update_data("update MoneyTransfer set Response='" + context.Request.Url + "',Message='" + res_msg + "',status='Success' where OrderID='" + STID + "'");
                                context.Response.ContentType = "application/json";
                                context.Response.Write("{\"ipay_id\": \"" + ipay_id + "\",\"success\": true,\"description\": \"callback called successfully\"}");
                            }
                        }
                    }
                }
                else
                {
                    string amount = context.Request.QueryString["amount"].ToString();
                    string optional2 = context.Request.QueryString["optional2"].ToString();
                    string res_code = context.Request.QueryString["res_code"].ToString();
                    string ssp_key = context.Request.QueryString["ssp_key"].ToString();
                    string sp_key = context.Request.QueryString["sp_key"].ToString();
                    decimal Surcharge = MoneyTransfer_EkoAPI.GetVirtualSurcharge(amount.ToDecimal(), dthismem.Rows[0]["PackageID"].ToString());
                    int Val = cls.update_data("AddEditVanHistory " + dthismem.Rows[0]["msrno"].ToString() + ",'" + amount + "','" + STID + "','" + optional2 + "','" + ipay_id + "','" + optional1 + "','" + System.DateTime.Now.ToShortDateString() + "','" + optional2 + "','','" + res_code + "','" + ssp_key + "','" + sp_key + "','" + opr_id + "','" + status + "','" + values + "'," + Surcharge + "");
                    if (Val > 0)
                    {
                        if (status.ToUpper() == "SUCCESS")
                        {

                            string Dec = "Amount : " + amount + " Added from Account-" + optional2 + " With Surcharge-" + Surcharge;

                            string MemberID = cls.select_data_scalar_string("select loginid from member where msrno=" + dthismem.Rows[0]["Msrno"].ToString() + "");
                            string narration = "Add Fund VAN UTR-" + opr_id;
                            cls.update_data("exec PROC_EWALLETTRANSACTION '" + MemberID + "','" + (amount.ToDecimal() - Surcharge) + "','Cr','" + narration + "','" + Dec + "'," + ConstantsData.SVan + ",'" + opr_id + "'");
                            context.Response.Write("{\"ipay_id\": \"" + ipay_id + "\",\"success\": true,\"description\": \"callback called successfully\"}");
                        }
                    }
                }



            }

        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}