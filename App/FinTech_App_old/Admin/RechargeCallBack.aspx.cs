using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using System.Web.Services;
using System.Configuration;
public partial class API_RechargeCallBack : System.Web.UI.Page
{
    cls_connection objconnection = new cls_connection();

    DataTable dtHistory = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["STATUS"] != null && Request.QueryString["TRANID"] != null && Request.QueryString["AGENTID"] != null && Request.QueryString["AGENTID"] != null)
        {
           
            // Select Transaction details through Client TransID
            dtHistory = objconnection.select_data_dt("Select a.*,b.loginid from tblrecharge_history as a,member as b where a.Memberid=b.msrno and a.transid='" + Request.QueryString["AGENTID"].ToString() + "'");
            if (dtHistory.Rows.Count > 0)
            {
                // Update Operator reference
                if (Request.QueryString["Status"].ToString().Trim().ToLower() == "success" && dtHistory.Rows[0]["status"].ToString().ToUpper() == "SUCCESS")
                {
                    objconnection.update_data("Update tblRecharge_history set APITransID='" + Request.QueryString["AGENTID"].ToString() + "', APImessage='" + Request.QueryString["TRANID"].ToString() + "', APIErrorCode='' where ID='" + dtHistory.Rows[0]["ID"].ToString() + "'");
                }

                if (dtHistory.Rows[0]["status"].ToString().ToLower() == "pending")
                {
                    if (Request.QueryString["STATUS"].ToString().Trim().ToLower() == "success")
                    {
                        DataTable i = objconnection.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + Convert.ToInt32(dtHistory.Rows[0]["id"].ToString()) + "," + Convert.ToInt32(dtHistory.Rows[0]["memberid"].ToString()) + ", 0, 0, 0, 0, '', '', 'Success','" + Request.QueryString["TRANID"].ToString() + "', '','" + Request.QueryString["AGENTID"].ToString() + "'");
                    }
                    if (Request.QueryString["STATUS"].ToString().Trim().ToLower() == "failed" || Request.QueryString["STATUS"].ToString().Trim().ToLower() == "failure")
                    {

                        string narration = "Recharge to " + dtHistory.Rows[0]["mobileno"].ToString() + "(Txn ID : " + dtHistory.Rows[0]["TransID"].ToString() + ")";
                        int CountCr = 0;
                        CountCr = objconnection.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like '%" + dtHistory.Rows[0]["TransID"].ToString() + "%' and factor='cr'");
                        if (CountCr == 0)
                        {
                            objconnection.update_data("PROC_EWALLETTRANSACTION '" + dtHistory.Rows[0]["loginid"].ToString() + "'," + dtHistory.Rows[0]["amount"].ToString() + ",'CR','" + ("Refund " + narration) + "','',"+ConstantsData.SRecharge+",'"+ dtHistory.Rows[0]["TransID"].ToString() + "'");
                        }
                        objconnection.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus','" + Convert.ToInt32(dtHistory.Rows[0]["id"].ToString()) + "', 0, 0, 0, 0, 0, '', '', 'Failed', '" + Request.QueryString["TRANID"].ToString() + "','', '" + Request.QueryString["AGENTID"].ToString() + "'");
                    }
                }
            }
        }
    }
}