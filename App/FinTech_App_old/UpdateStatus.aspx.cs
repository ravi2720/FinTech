using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateStatus : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    clsMahagram ObjMaha = new clsMahagram();
    string IP = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //TransactionId=MH130619120638577812C7B&VenderId=144088494&Status=SUCCESS&BcCode=BC658306479&rrn=916412656820
            if (Request.QueryString["TransactionId"] != null && Request.QueryString["VenderId"] != null && Request.QueryString["Status"] != null
                && Request.QueryString["BcCode"] != null && Request.QueryString["rrn"] != null)
            {
                IP = GetIPAddress();
                int msrno = cls.select_data_scalar_int("select ID from Member where BCRegistrationID='" + Request.QueryString["BcCode"].ToString() + "'");
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                string narration = "";
                DataTable dtHistory1 = cls.select_data_dt("Select * from AEPS_History where TransactionId='" + Request.QueryString["TransactionId"].ToString() + "' and  status='Pending'");
                DataTable dtHistoryAadhar = cls.select_data_dt("Select * from Aadhar_History where TransactionId='" + Request.QueryString["TransactionId"].ToString() + "' and  status='Pending'");
                DataTable dtHistoryCashDeposit = cls.select_data_dt("Select * from CashDepositTransaction with(nolock) where merchanttxnid='" + Request.QueryString["TransactionId"].ToString() + "' and  status='Pending'");

                if (dtHistory1.Rows.Count > 0 && dtHistoryAadhar.Rows.Count == 0 && dtHistoryCashDeposit.Rows.Count == 0)
                {

                    if (dtHistory1.Rows[0]["transcationtype"].ToString().ToUpper() == "CW")
                    {
                        if (Request.QueryString["Status"].ToString().ToUpper() == "SUCCESS")
                        {
                            Int32 Val = cls.update_data("proc_Credit_AEPS_History '" + dtHistory1.Rows[0]["referenceno"].ToString() + "','" + dtHistory1.Rows[0]["msrno"].ToString() + "','CW','Success','',0,'" + Request.QueryString["rrn"].ToString() + "','','','" + IP + "','" + strPathAndQuery + "'");
                            try
                            {
                                string loginid = cls.select_data_scalar_string("select loginid from Member where msrno=" + dtHistory1.Rows[0]["msrno"].ToString() + "");
                                narration = "Aeps TransID-" + dtHistory1.Rows[0]["referenceno"].ToString();
                                cls.update_data("Exec [ProcMLM__EWalletTransaction_AEPS2] '" + loginid + "'," + dtHistory1.Rows[0]["Amount"].ToString() + " , 'Cr', '" + narration + "', 'WEB', '" + System.Guid.NewGuid().ToString().Replace("-", "C") + "' ");
                                cls.update_data("Exec [ProcAEPSMahagram_DistributeCommission] '" + dtHistory1.Rows[0]["msrno"].ToString() + "'," + dtHistory1.Rows[0]["Amount"].ToString() + " ," + dtHistory1.Rows[0]["Amount"].ToString() + ", '" + dtHistory1.Rows[0]["referenceno"].ToString() + "', '" + System.Guid.NewGuid().ToString().Replace("-", "C") + "' ");
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            Int32 Val = cls.update_data("proc_Credit_AEPS_History '" + dtHistory1.Rows[0]["referenceno"].ToString() + "','" + dtHistory1.Rows[0]["msrno"].ToString() + "','CW','" + Request.QueryString["Status"].ToString() + "','',0,'" + Request.QueryString["rrn"].ToString() + "','','','" + IP + "','" + strPathAndQuery + "'");
                        }
                    }
                    else if (dtHistory1.Rows[0]["transcationtype"].ToString().ToUpper() == "BE")
                    {
                        Int32 Val = cls.update_data("proc_Credit_AEPS_History '" + dtHistory1.Rows[0]["referenceno"].ToString() + "','" + dtHistory1.Rows[0]["msrno"].ToString() + "','BE','" + Request.QueryString["Status"].ToString() + "','',0,'" + Request.QueryString["rrn"].ToString() + "','','','" + IP + "','" + strPathAndQuery + "'");
                    }
                    else if (dtHistory1.Rows[0]["transcationtype"].ToString().ToUpper() == "MS")
                    {
                        Int32 Val = cls.update_data("proc_Credit_AEPS_History '" + dtHistory1.Rows[0]["referenceno"].ToString() + "','" + dtHistory1.Rows[0]["msrno"].ToString() + "','BE','" + Request.QueryString["Status"].ToString() + "','',0,'" + Request.QueryString["rrn"].ToString() + "','','','" + IP + "','" + strPathAndQuery + "'");
                    }

                    // DataTable dtHistory = cls.select_data_dt("Exec [ProcAEPS_Mahagram_AddEditHistory] 1, " + Convert.ToInt32(msrno) + ", '','', '', 0, 0,0, '" + Request.QueryString["Status"].ToString() + "', '" + narration + "','','','','', '', '" + Request.QueryString["TransactionId"].ToString() + "','" + Request.QueryString["rrn"].ToString() + "','','','','', '', 0,0,0,0, '','','" + strPathAndQuery + "'");


                }
                else if (dtHistoryAadhar.Rows.Count > 0 && dtHistory1.Rows.Count == 0 && dtHistoryCashDeposit.Rows.Count == 0)
                {
                    if (dtHistoryAadhar.Rows[0]["transcationtype"].ToString().ToUpper() == "CW")
                    {
                        if (Request.QueryString["Status"].ToString().ToUpper() == "SUCCESS")
                        {
                            Int32 Val = cls.update_data("proc_Credit_Aadhar_History '" + dtHistoryAadhar.Rows[0]["referenceno"].ToString() + "','" + dtHistoryAadhar.Rows[0]["msrno"].ToString() + "','CW','Success','',0,'" + Request.QueryString["rrn"].ToString() + "','','','" + IP + "','" + strPathAndQuery + "'");
                            if (Val > 0)
                            {
                                cls.update_data("exec aadharpaycredit '" + dtHistoryAadhar.Rows[0]["referenceno"].ToString() + "','" + dtHistoryAadhar.Rows[0]["msrno"].ToString() + "'");
                            }
                        }
                        else
                        {
                            Int32 Val = cls.update_data("proc_Credit_Aadhar_History '" + dtHistoryAadhar.Rows[0]["referenceno"].ToString() + "','" + dtHistoryAadhar.Rows[0]["msrno"].ToString() + "','CW','" + Request.QueryString["Status"].ToString() + "','',0,'" + Request.QueryString["rrn"].ToString() + "','','','" + IP + "','" + strPathAndQuery + "'");
                        }
                    }
                }
                else if (dtHistoryCashDeposit.Rows.Count > 0 && dtHistoryAadhar.Rows.Count == 0 && dtHistory1.Rows.Count == 0)
                {
                    if (Request.QueryString["Status"].ToString().ToUpper() == "SUCCESS")
                    {
                        Int32 Val = cls.update_data("PorcCashDepositTransaction 'update','" + dtHistoryCashDeposit.Rows[0]["msrno"].ToString() + "','','','0','" + dtHistoryCashDeposit.Rows[0]["referenceno"].ToString() + "','Success','" + dtHistoryCashDeposit.Rows[0]["merchanttxnid"].ToString() + "','','','" + Request.QueryString["rrn"].ToString() + "','','" + strPathAndQuery + "'");

                        // Int32 Val = cls.update_data("proc_Credit_Aadhar_History '" + dtHistoryAadhar.Rows[0]["referenceno"].ToString() + "','" + dtHistoryAadhar.Rows[0]["msrno"].ToString() + "','CW','Success','',0,'" + Request.QueryString["rrn"].ToString() + "','','','" + IP + "','" + strPathAndQuery + "'");
                        if (Val > 0)
                        {
                        }
                    }
                    else
                    {
                        Int32 Val = cls.update_data("PorcCashDepositTransaction 'update','" + dtHistoryCashDeposit.Rows[0]["msrno"].ToString() + "','','','0','" + dtHistoryCashDeposit.Rows[0]["referenceno"].ToString() + "','Refund','" + dtHistoryCashDeposit.Rows[0]["merchanttxnid"].ToString() + "','','','','','" + strPathAndQuery + "'");
                        if (Val > 0)
                        {
                            string MemberID = cls.select_data_scalar_string("select loginid from Member where msrno='" + msrno + "'");
                            string Narration = "Refund Cash Deposit TransID-" + dtHistoryCashDeposit.Rows[0]["referenceno"].ToString();
                            cls.select_data_dt("exec [PROC_AEPS2EWALLETTRANSACTION]  '" + MemberID + "', " + dtHistoryCashDeposit.Rows[0]["Amount"].ToString() + ", 'Cr','" + Narration + "','" + dtHistoryCashDeposit.Rows[0]["referenceno"].ToString() + "'");
                        }
                    }
                }

                var Values = new NameValueCollection();
                string mess = "";
                if (Request.QueryString["Status"].ToString().ToUpper() == "SUCCESS")
                {
                    mess = "update Successfully!!";
                }
                else if (Request.QueryString["Status"].ToString().ToUpper() == "PENDING")
                {
                    mess = "Pending!!";
                }
                else if (Request.QueryString["Status"].ToString().ToUpper() == "PENDING")
                {
                    mess = "FAILED";
                }
                Values["MESSAGE"] = mess;// "BC658306479";// "BC674784069";       
                Values["STATUS"] = Request.QueryString["Status"].ToString();

                Response.Write(ObjMaha.copyFrom(Values));
            }
            else
            {
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                cls.insert_data("insert into tbl_Apeslog(url)values('" + strPathAndQuery + "')");
            }
        }
        catch (Exception ex)
        {
            String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            cls.insert_data("insert into tbl_Apeslog(url)values('" + ex.Message + "')");
        }
    }

    public string GetIPAddress()
    {
        string ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        return ipAddress;
    }
}