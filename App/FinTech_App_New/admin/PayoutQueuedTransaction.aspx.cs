using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_PayoutQueuedTransaction : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                fillTransactionDetails();
                GetRazorPay();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void GetRazorPay()
    {
        try
        {
            var client = new RestClient("https://api.razorpay.com/v1/transactions?account_number=3434913597520120&count=1");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic cnpwX2xpdmVfdllJaDNPOVVnQ2hpT2c6WnVrZ0trUkZ6SjZHdlJSMWFNejFDUk40");
            IRestResponse response = client.Execute(request);

            string Result = response.Content;
            cls.update_data("insert into tbl_Apeslog(url)values('" + Result + "')");

            JObject Data = JObject.Parse(Result);
            lblRazorPayBalance.Text = "Razor Balance-" + (Convert.ToDecimal(JArray.Parse(Data["items"].ToString())[0]["balance"].ToString()) / 100).ToString();

            ICICIBalanceFetch iCICIBalanceFetch = new ICICIBalanceFetch();

            string Result1 = AccountStatement.GetBalanceInquiry(iCICIBalanceFetch);
            cls.update_data("insert into tbl_Apeslog(url)values('" + Result1 + "')");

            JObject Data1 = JObject.Parse(Result1);
            lblICICI.Text = "ICICI Balance-" + Data1["EFFECTIVEBAL"].ToString();
        }
        catch (Exception ex)
        {
        }

    }



    private void fillTransactionDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("PROC_AEPS_WITHDRWALREQUEST_Queued_History '" + txtSearch.Text + "',0,'" + txtfromdate.Text + "','" + txttodate.Text + "','" + dllStatus.SelectedValue + "'");

        if (dt.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();
        }
        else
        {
            gvTransactionHistory.DataSource = null;
            gvTransactionHistory.DataBind();
        }
        GetRazorPay();
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }

    protected void btnAllProcess_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in gvTransactionHistory.Items)
        {
            HiddenField hdnRequestID = (HiddenField)item.FindControl("hdnRequestID");
            HiddenField hdnMsrno = (HiddenField)item.FindControl("hdnMsrno");
            HiddenField hdnBankID = (HiddenField)item.FindControl("hdnBankID");
            HiddenField hdnID = (HiddenField)item.FindControl("hdnID");
            HiddenField hdnAmount = (HiddenField)item.FindControl("hdnAmount");
            HiddenField hdnTransMode = (HiddenField)item.FindControl("hdnTransMode");
            DataTable dtHistory = cls.select_data_dt("select * from AEPS_WITHDRWALREQUEST where id=" + hdnID.Value + " and RequestStatus='Queued'");
            if (dtHistory.Rows.Count > 0)
            {
                DataTable dtBankID = cls.select_data_dt("select * from View_MemberBankList where msrno = '" + hdnMsrno.Value + "' and ID='" + hdnBankID.Value + "'");
                if (dtBankID.Rows.Count > 0)
                {
                    string ContactsID = cls.select_data_scalar_string("select ContactID from ContactsRazor where msrno=" + hdnMsrno.Value + "  and Self=1");
                    string fund_account_id = cls.select_data_scalar_string("select FundAccount_id from FundAccountRazar where msrno=" + hdnMsrno.Value + " and contact_id='" + ContactsID + "' and account_number='" + dtBankID.Rows[0]["AccountNumber"].ToString() + "'");
                    if (!string.IsNullOrEmpty(ContactsID) && !string.IsNullOrEmpty(fund_account_id))
                    {
                        cls.update_data("update  AEPS_WITHDRWALREQUEST set ManualSystem=ManualSystem+1 where ID=" + hdnID.Value + "");
                        CreatePayout createPayout = new CreatePayout();
                        createPayout.account_number = "3434913597520120";// dtBankID.Rows[0]["AccountNumber"].ToString();
                        createPayout.amount = Convert.ToInt32(hdnAmount.Value.Replace(".00", "").Trim()) * 100;
                        createPayout.currency = "INR";
                        createPayout.fund_account_id = fund_account_id;
                        if (dllType.SelectedIndex > 0)
                        {
                            createPayout.mode = dllType.SelectedValue;
                            cls.update_data("update  AEPS_WITHDRWALREQUEST set AdminTransType='" + dllType.SelectedValue + "' where ID=" + hdnID.Value + "");
                        }
                        else
                        {
                            createPayout.mode = hdnTransMode.Value;
                        }
                        createPayout.narration = "";
                        createPayout.purpose = "payout";
                        createPayout.queue_if_low_balance = false;
                        createPayout.reference_id = hdnRequestID.Value;
                        CreatePayoutNotes createPayoutNotes = new CreatePayoutNotes();
                        createPayout.notes = createPayoutNotes;
                        string result = ApiPostCallRazorpayx.PostCall(createPayout.GetJson(), "payouts");

                        JObject Data = JObject.Parse(result);
                        if (Data["error"] == null)
                        {
                            CreatePayoutResult createPayoutResult = JsonConvert.DeserializeObject<CreatePayoutResult>(result);
                            if (createPayoutResult.status.ToUpper() == "PROCESSED")
                            {
                                //ErrorShow.AlertMessageWithRedirect(page, "Transaction Successfully Done", "WithdrawRequestReceipt.aspx?GUID=" + guid);
                            }
                            else if (createPayoutResult.status.ToUpper() == "PROCESSING" || createPayoutResult.status.ToUpper() == "PENDING")
                            {
                                //ErrorShow.AlertMessageWithRedirect(page, "Transaction in PROCESSING", "WithdrawRequestReceipt.aspx?GUID=" + guid);
                            }
                            else if (createPayoutResult.status.ToUpper() == "PENDING")
                            {
                                //ErrorShow.AlertMessage(page, "Transaction in PENDING");
                            }
                            else if (createPayoutResult.status.ToUpper() == "CANCELLED" || createPayoutResult.status.ToUpper() == "UNAUTHORIZED")
                            {
                                // ErrorShow.AlertMessage(page, "Transaction FAILURE Successfully. Your Amount -" + (Convert.ToDecimal(txtamount.Text) + Charge) + " refund . aeps wallet.");
                                cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE_Queued'," + hdnID.Value + ",'','failed','" + createPayoutResult.failure_reason + "','Failed','','" + result + "'");
                            }

                            {
                                cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + Convert.ToInt16(hdnID.Value) + ",'" + createPayoutResult.id + "','" + createPayoutResult.status + "','DE_001','','rrn','" + result + "'");
                            }

                            // ErrorShow.AlertMessageWithRedirect(page, lblMessage.Text, "WithdrawRequestReceipt.aspx?GUID=" + guid);
                        }
                        else
                        {
                            // ErrorShow.AlertMessage(page, "Transaction FAILURE Successfully. Your Amount -" + (Convert.ToDecimal(txtamount.Text) + Charge) + " refund . aeps wallet.");
                            cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE_Queued'," + hdnID.Value + ",'','failed','" + Data["error"]["code"].ToString() + "','" + Data["error"]["description"].ToString() + "','','" + result + "'");
                        }
                    }
                }
            }
        }
        fillTransactionDetails();
    }

    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Reject")
        {
            cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE'," + e.CommandArgument.ToString() + ",'','Refunded','','Recevier Bank Down','',''");
            fillTransactionDetails();
        }

        if (e.CommandName == "Processed")
        {
            DataTable dtHistory = cls.select_data_dt("select * from AEPS_WITHDRWALREQUEST where id=" + e.CommandArgument.ToString() + " and RequestStatus='Queued'");
            if (dtHistory.Rows.Count > 0)
            {
                DataTable dtBankID = cls.select_data_dt("select * from View_MemberBankList where msrno = '" + dtHistory.Rows[0]["Msrno"].ToString() + "' and ID='" + dtHistory.Rows[0]["BankID"].ToString() + "'");
                if (dtBankID.Rows.Count > 0)
                {
                    string ContactsID = cls.select_data_scalar_string("select ContactID from ContactsRazor where msrno=" + dtHistory.Rows[0]["Msrno"].ToString() + "  and Self=1");
                    string fund_account_id = cls.select_data_scalar_string("select FundAccount_id from FundAccountRazar where msrno=" + dtHistory.Rows[0]["Msrno"].ToString() + " and contact_id='" + ContactsID + "' and account_number='" + dtBankID.Rows[0]["AccountNumber"].ToString() + "'");
                    if (!string.IsNullOrEmpty(ContactsID) && !string.IsNullOrEmpty(fund_account_id))
                    {
                        cls.update_data("update  AEPS_WITHDRWALREQUEST set ManualSystem=ManualSystem+1 where ID=" + dtHistory.Rows[0]["ID"].ToString() + "");
                        CreatePayout createPayout = new CreatePayout();
                        createPayout.account_number = "3434913597520120";// dtBankID.Rows[0]["AccountNumber"].ToString();
                        createPayout.amount = Convert.ToInt32(dtHistory.Rows[0]["Amount"].ToString().Replace(".00", "").Trim()) * 100;
                        createPayout.currency = "INR";
                        createPayout.fund_account_id = fund_account_id;
                        if (dllType.SelectedIndex > 0)
                        {
                            createPayout.mode = dllType.SelectedValue;
                            cls.update_data("update  AEPS_WITHDRWALREQUEST set AdminTransType='" + dllType.SelectedValue + "' where ID=" + dtHistory.Rows[0]["ID"].ToString() + "");
                        }
                        else
                        {
                            createPayout.mode = dtHistory.Rows[0]["TransMode"].ToString();
                        }
                        createPayout.narration = "";
                        createPayout.purpose = "payout";
                        createPayout.queue_if_low_balance = false;
                        createPayout.reference_id = dtHistory.Rows[0]["RequestID"].ToString();
                        CreatePayoutNotes createPayoutNotes = new CreatePayoutNotes();
                        createPayout.notes = createPayoutNotes;
                        string result = ApiPostCallRazorpayx.PostCall(createPayout.GetJson(), "payouts");

                        JObject Data = JObject.Parse(result);
                        if (Data["error"] == null)
                        {
                            CreatePayoutResult createPayoutResult = JsonConvert.DeserializeObject<CreatePayoutResult>(result);
                            if (createPayoutResult.status.ToUpper() == "PROCESSED")
                            {
                                //ErrorShow.AlertMessageWithRedirect(page, "Transaction Successfully Done", "WithdrawRequestReceipt.aspx?GUID=" + guid);
                            }
                            else if (createPayoutResult.status.ToUpper() == "PROCESSING" || createPayoutResult.status.ToUpper() == "PENDING")
                            {
                                //ErrorShow.AlertMessageWithRedirect(page, "Transaction in PROCESSING", "WithdrawRequestReceipt.aspx?GUID=" + guid);
                            }
                            else if (createPayoutResult.status.ToUpper() == "PENDING")
                            {
                                //ErrorShow.AlertMessage(page, "Transaction in PENDING");
                            }
                            else if (createPayoutResult.status.ToUpper() == "CANCELLED" || createPayoutResult.status.ToUpper() == "UNAUTHORIZED")
                            {
                                // ErrorShow.AlertMessage(page, "Transaction FAILURE Successfully. Your Amount -" + (Convert.ToDecimal(txtamount.Text) + Charge) + " refund . aeps wallet.");
                                cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE_Queued'," + dtHistory.Rows[0]["ID"].ToString() + ",'','failed','" + createPayoutResult.failure_reason + "','Failed','','" + result + "'");
                            }

                            {
                                cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + dtHistory.Rows[0]["ID"].ToString() + ",'" + createPayoutResult.id + "','" + createPayoutResult.status + "','DE_001','','rrn','" + result + "'");
                            }

                            // ErrorShow.AlertMessageWithRedirect(page, lblMessage.Text, "WithdrawRequestReceipt.aspx?GUID=" + guid);
                        }
                        else
                        {
                            // ErrorShow.AlertMessage(page, "Transaction FAILURE Successfully. Your Amount -" + (Convert.ToDecimal(txtamount.Text) + Charge) + " refund . aeps wallet.");
                            cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE_Queued'," + dtHistory.Rows[0]["ID"].ToString() + ",'','failed','" + Data["error"]["code"].ToString() + "','" + Data["error"]["description"].ToString() + "','','" + result + "'");
                        }
                    }
                }
            }
            fillTransactionDetails();
        }
        if (e.CommandName == "ReTry")
        {
            DataTable dtHistoryOld = cls.select_data_dt("select * from AEPS_WITHDRWALREQUEST where id=" + e.CommandArgument.ToString() + " and RequestStatus='failed'");
            if (dtHistoryOld.Rows.Count > 0)
            {
                Int32 ValReTry = cls.update_data("insert into ReTryPayoutTransaction(PayoutHistory,PayoutRequestID)values(" + dtHistoryOld.Rows[0]["ID"].ToString() + ",'" + dtHistoryOld.Rows[0]["RequestID"].ToString() + "')");
                if (ValReTry == 1)
                {
                    string NewRequestID = "APN" + System.Guid.NewGuid().ToString().Replace("-", "");
                    Int32 UpdateRequestID = cls.update_data("update AEPS_WITHDRWALREQUEST set RequestID='" + NewRequestID + "' where ID=" + dtHistoryOld.Rows[0]["ID"].ToString() + "");
                    if (UpdateRequestID == 1)
                    {
                        DataTable dtHistory = cls.select_data_dt("select * from AEPS_WITHDRWALREQUEST where id=" + dtHistoryOld.Rows[0]["ID"].ToString() + " and RequestStatus='failed'");
                        if (dtHistory.Rows.Count > 0)
                        {
                            DataTable dtBankID = cls.select_data_dt("select * from View_MemberBankList where msrno = '" + dtHistory.Rows[0]["Msrno"].ToString() + "' and ID='" + dtHistory.Rows[0]["BankID"].ToString() + "'");
                            if (dtBankID.Rows.Count > 0)
                            {
                                string ContactsID = cls.select_data_scalar_string("select ContactID from ContactsRazor where msrno=" + dtHistory.Rows[0]["Msrno"].ToString() + "  and Self=1");
                                string fund_account_id = cls.select_data_scalar_string("select FundAccount_id from FundAccountRazar where msrno=" + dtHistory.Rows[0]["Msrno"].ToString() + " and contact_id='" + ContactsID + "' and account_number='" + dtBankID.Rows[0]["AccountNumber"].ToString() + "'");
                                if (!string.IsNullOrEmpty(ContactsID) && !string.IsNullOrEmpty(fund_account_id))
                                {
                                    cls.update_data("update  AEPS_WITHDRWALREQUEST set ManualSystem=ManualSystem+1 where ID=" + dtHistory.Rows[0]["ID"].ToString() + "");
                                    CreatePayout createPayout = new CreatePayout();
                                    createPayout.account_number = "3434913597520120";// dtBankID.Rows[0]["AccountNumber"].ToString();
                                    createPayout.amount = Convert.ToInt32(dtHistory.Rows[0]["Amount"].ToString().Replace(".00", "").Trim()) * 100;
                                    createPayout.currency = "INR";
                                    createPayout.fund_account_id = fund_account_id;
                                    if (dllType.SelectedIndex > 0)
                                    {
                                        createPayout.mode = dllType.SelectedValue;
                                        cls.update_data("update  AEPS_WITHDRWALREQUEST set AdminTransType='" + dllType.SelectedValue + "' where ID=" + dtHistory.Rows[0]["ID"].ToString() + "");
                                    }
                                    else
                                    {
                                        createPayout.mode = dtHistory.Rows[0]["TransMode"].ToString();
                                    }
                                    createPayout.narration = "";
                                    createPayout.purpose = "payout";
                                    createPayout.queue_if_low_balance = false;
                                    createPayout.reference_id = dtHistory.Rows[0]["RequestID"].ToString();
                                    CreatePayoutNotes createPayoutNotes = new CreatePayoutNotes();
                                    createPayout.notes = createPayoutNotes;
                                    string result = ApiPostCallRazorpayx.PostCall(createPayout.GetJson(), "payouts");

                                    JObject Data = JObject.Parse(result);
                                    if (Data["error"] == null)
                                    {
                                        CreatePayoutResult createPayoutResult = JsonConvert.DeserializeObject<CreatePayoutResult>(result);
                                        if (createPayoutResult.status.ToUpper() == "PROCESSED")
                                        {
                                            //ErrorShow.AlertMessageWithRedirect(page, "Transaction Successfully Done", "WithdrawRequestReceipt.aspx?GUID=" + guid);
                                        }
                                        else if (createPayoutResult.status.ToUpper() == "PROCESSING" || createPayoutResult.status.ToUpper() == "PENDING")
                                        {
                                            //ErrorShow.AlertMessageWithRedirect(page, "Transaction in PROCESSING", "WithdrawRequestReceipt.aspx?GUID=" + guid);
                                        }
                                        else if (createPayoutResult.status.ToUpper() == "PENDING")
                                        {
                                            //ErrorShow.AlertMessage(page, "Transaction in PENDING");
                                        }
                                        else if (createPayoutResult.status.ToUpper() == "CANCELLED" || createPayoutResult.status.ToUpper() == "UNAUTHORIZED")
                                        {
                                            // ErrorShow.AlertMessage(page, "Transaction FAILURE Successfully. Your Amount -" + (Convert.ToDecimal(txtamount.Text) + Charge) + " refund . aeps wallet.");
                                            cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE_Queued'," + dtHistory.Rows[0]["ID"].ToString() + ",'','failed','" + createPayoutResult.failure_reason + "','Failed','','" + result + "'");
                                        }

                                        {
                                            cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + dtHistory.Rows[0]["ID"].ToString() + ",'" + createPayoutResult.id + "','" + createPayoutResult.status + "','DE_001','','rrn','" + result + "'");
                                        }

                                        // ErrorShow.AlertMessageWithRedirect(page, lblMessage.Text, "WithdrawRequestReceipt.aspx?GUID=" + guid);
                                    }
                                    else
                                    {
                                        // ErrorShow.AlertMessage(page, "Transaction FAILURE Successfully. Your Amount -" + (Convert.ToDecimal(txtamount.Text) + Charge) + " refund . aeps wallet.");
                                        cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE_Queued'," + dtHistory.Rows[0]["ID"].ToString() + ",'','failed','" + Data["error"]["code"].ToString() + "','" + Data["error"]["description"].ToString() + "','','" + result + "'");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            fillTransactionDetails();
        }
    }

    protected void btnCheckStatus_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem item in gvTransactionHistory.Items)
        {
            HiddenField hdnID = (HiddenField)item.FindControl("hdnID");
            if (!string.IsNullOrEmpty(hdnID.Value))
            {
                DataTable dtHistory = cls.select_data_dt("select * from AEPS_WITHDRWALREQUEST where id=" + hdnID.Value + " and RequestStatus='processing'");
                if (dtHistory.Rows.Count > 0)
                {
                    try
                    {
                        string result = ApiPostCallRazorpayx.GetCheckStatus(dtHistory.Rows[0]["TransactionID"].ToString());
                        if (!string.IsNullOrEmpty(result))
                        {
                            JObject Data = JObject.Parse(result);
                            if (Data["error"] == null)
                            {
                                CreatePayoutResult createPayoutResult = JsonConvert.DeserializeObject<CreatePayoutResult>(result);
                                if (createPayoutResult.status.ToUpper() == "REVERSED")
                                {
                                    cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE'," + dtHistory.Rows[0]["ID"].ToString() + ",'','" + createPayoutResult.status + "','Failed','Failed','','" + result + "'");
                                }
                                if (createPayoutResult.status.ToUpper() == "PROCESSED")
                                {
                                    cls.update_data("update AEPS_WithdrwalRequest set rrn='" + createPayoutResult.utr + "', RequestStatus='" + createPayoutResult.status + "' where ID='" + dtHistory.Rows[0]["ID"].ToString() + "'");
                                }
                                else
                                {
                                    cls.update_data("update AEPS_WithdrwalRequest set RequestStatus='" + createPayoutResult.status + "' where tID='" + dtHistory.Rows[0]["ID"].ToString() + "'");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

            }


        }
        fillTransactionDetails();
    }
}