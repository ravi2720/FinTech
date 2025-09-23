using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_Settlement : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Page page;
    string PayoutFrom = "";
    dynamic date;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            var var = Common.GetServiceStatus("23", cls);
            if (var.Item1 == true)
            {
                ErrorShow.SuccessNotify(page, "Service Working Fine");
                dtMember = (DataTable)Session["dtMember"];
                page = HttpContext.Current.CurrentHandler as Page;
                DataTable dtIMPS = cls.select_data_dt("select top 1 * from Service where ID='" + ConstantsData.SSettlement + "'");
                if (dtIMPS.Rows.Count > 0)
                {
                    if (!IsPostBack)
                    {
                        txtRemark.Text = " Move To bank";
                        if (Session["dtMember"] != null)
                        {
                            if (dtIMPS.Rows[0]["Onoff"].ToString() == "False")
                            {
                                btnSubmit.Visible = false;
                                lblMessage.Text = "AEPS Withdraw Request Down due to " + dtIMPS.Rows[0]["Reason"].ToString();
                            }
                            FillBalance(Convert.ToInt32(dtMember.Rows[0]["msrno"]));
                            GetBankDetails();
                            spanstartTime.InnerHtml = dtIMPS.Rows[0]["OnTime"].ToString();
                            spnendTime.InnerHtml = dtIMPS.Rows[0]["OffTime"].ToString();

                        }
                        else
                        {
                            Response.Redirect("default.aspx");
                        }
                    }
                }
            }
            else
            {
                ErrorShow.ErrorNotify(page, var.Item2);
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void GetBankDetails()
    {
        try
        {
            DataTable dtBank = cls.select_data_dt("select * from VIEW_MEMBERBANKDETAILS where msrno = " + dtMember.Rows[0]["MSRNO"] + "");
            rptBankData.DataSource = dtBank;
            rptBankData.DataBind();
        }
        catch (Exception ex)
        {

        }
    }
    public void FillBalance(int msrno)
    {
        DataTable dtBalance = new DataTable();
        dtBalance = cls.select_data_dt("select Balance from VIEW_MEMBER_AEPS_BALANCE where msrno = " + msrno + "");
        lblBalance.Text = dtBalance.Rows[0]["Balance"].ToString();
    }


    protected void clear()
    {
        txtRemark.Text = txtamount.Text = string.Empty;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        dtMember = (DataTable)Session["dtMember"];
        Int32 HoldCount = cls.select_data_scalar_int("select count(1) from Member where Msrno=" + dtMember.Rows[0]["msrno"].ToString() + " and OnHold=1");
        if (HoldCount == 0)
        {
            var date = DateTime.Now;
            string guid = "AP" + System.Guid.NewGuid().ToString().Replace('-', '0');
            if (Convert.ToDecimal(txtamount.Text) > 0)
            {
                var ActiveData = GetActivePayoutAPI(txtamount.Text);
                PayoutFrom = ActiveData.Item1;
                string MaxSenderAmount = ActiveData.Item2;
                if (Convert.ToDecimal(txtamount.Text) <= Convert.ToDecimal(MaxSenderAmount) && !string.IsNullOrEmpty(PayoutFrom) || (rdtype.SelectedValue.ToUpper() == "WALLET"))
                {
                    lblMessage.Text = "";
                    if (Session["dtMember"] != null)
                    {
                        if (dtMember.Rows.Count > 0)
                        {
                            try
                            {
                                if (rdtype.SelectedValue.ToUpper() == "BANK")
                                {
                                    string AccountSelected = "";
                                    foreach (RepeaterItem item in rptBankData.Items)
                                    {
                                        CheckBox chk = (CheckBox)item.FindControl("chk");
                                        HiddenField hdnAccount = (HiddenField)item.FindControl("hdnAccount");
                                        if (chk.Checked)
                                        {
                                            AccountSelected = hdnAccount.Value;
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(AccountSelected))
                                    {
                                        {
                                            DataTable dtBankID = cls.select_data_dt("select * from View_MemberBankList where msrno = '" + dtMember.Rows[0]["msrno"] + "' and AccountNumber='" + AccountSelected + "'");
                                            if (dtBankID.Rows.Count > 0)
                                            {
                                                decimal Charge = 0;
                                                string tMode = "";
                                                if (rdtype.SelectedValue.ToUpper() == "BANK")
                                                {
                                                    Charge = MoneyTransfer_EkoAPI.GetPayoutCharge(Convert.ToDecimal(txtamount.Text), rdtTransactionMode.SelectedValue);
                                                    tMode = rdtTransactionMode.SelectedValue;
                                                }
                                                string Balance = cls.select_data_scalar_string("SELECT BALANCE FROM TBLMLM_AEPS2EWALLETBALANCE WHERE MSRNO = '" + dtMember.Rows[0]["msrno"] + "'");
                                                Int32 countP = cls.select_data_scalar_int("select * from AEPS_WITHDRWALREQUEST where msrno=" + dtMember.Rows[0]["msrno"] + " and RequestStatus='Pending'");
                                                if (countP >= 0)
                                                {
                                                    if (Convert.ToDecimal(Balance) > (Convert.ToDecimal(txtamount.Text) + Charge))
                                                    {
                                                        string Narration = "Amound Deducted For Withdraw Request-" + guid;
                                                        string Description = $"{txtamount.Text} Rs Settlemnt in Acc. {AccountSelected} in { rdtTransactionMode.SelectedValue } Mode With Charge-{Charge}";
                                                        DataTable dtWall = new DataTable();
                                                        dtWall = cls.select_data_dt("Exec PROC_AEPSWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"] + "','" + (Convert.ToDecimal(txtamount.Text) + Charge) + "','DR','" + Narration + "','" + Description + "','" + ConstantsData.SSettlement + "','" + guid + "'");
                                                        if (dtWall.Rows[0]["Msrno"].ToString() == "1")
                                                        {
                                                            DataTable dtValue;
                                                            {
                                                                dtValue = cls.select_data_dt("EXEC PROC_AEPS_WITHDRWALREQUEST '" + dtMember.Rows[0]["loginid"] + "','" + txtamount.Text + "','" + dtBankID.Rows[0]["ID"].ToString() + "','" + txtRemark.Text.Trim() + "','" + rdtype.SelectedValue + "','" + tMode + "'," + Charge + ",'" + guid + "'," + PayoutFrom + "");
                                                                if (Convert.ToInt32(dtValue.Rows[0]["ID"]) > 0)
                                                                {
                                                                    if (rdtype.SelectedValue.ToUpper() == "BANK")
                                                                    {
                                                                        string result = "";
                                                                        if (PayoutFrom == "1")
                                                                        {
                                                                            RootInstantPayout rootInstantPayout = new RootInstantPayout();
                                                                            Payee payee = new Payee();
                                                                            payee.name = dtBankID.Rows[0]["Name"].ToString().ToUpper();
                                                                            payee.bankIfsc = dtBankID.Rows[0]["IFSCCode"].ToString().ToUpper();
                                                                            payee.accountNumber = dtBankID.Rows[0]["AccountNumber"].ToString();
                                                                            rootInstantPayout.payee = payee;
                                                                            Payer payer = new Payer();
                                                                            rootInstantPayout.payer = payer;
                                                                            Location objLocation = new Location();
                                                                            objLocation = objLocation.GetLocationDataCurrent("103.73.190.82");
                                                                            rootInstantPayout.externalRef = guid;
                                                                            rootInstantPayout.latitude = objLocation.Latitude;
                                                                            rootInstantPayout.longitude = objLocation.Longitude;
                                                                            rootInstantPayout.transferMode = "IMPS";
                                                                            rootInstantPayout.transferAmount = txtamount.Text;
                                                                            rootInstantPayout.remarks = "AePS Payout";
                                                                            rootInstantPayout.alertEmail = dtMember.Rows[0]["Email"].ToString();

                                                                            //string Result = ApiPostCallInstantPay.PostCall(rootInstantPayout.GetJson(), "payout", Request.Headers["User-Agent"].ToString());
                                                                            string Result = "";
                                                                            {
                                                                                JObject TransferData = JObject.Parse(Result);
                                                                                if (InstantPayout.arrTemplate.Contains(TransferData["statuscode"].ToString()))
                                                                                {
                                                                                    if (TransferData["statuscode"].ToString().ToUpper() == "TXN" || TransferData["statuscode"].ToString().ToUpper() == "TUP")
                                                                                    {
                                                                                        if (TransferData["statuscode"].ToString().ToUpper() == "TUP")
                                                                                        {
                                                                                            cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + Convert.ToInt32(dtValue.Rows[0]["ID"]) + ",'" + TransferData["data"]["poolReferenceId"].ToString() + "','Pending','" + TransferData["statuscode"].ToString() + "','" + TransferData["status"].ToString() + "','','" + Result + "'," + ConstantsData.SSettlement + "");

                                                                                            ErrorShow.AlertMessageWithRedirect(page, "Transaction in PROCESSING", "WithdrawRequestReceipt.aspx?GUID=" + guid, ConstantsData.CompanyName);
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + Convert.ToInt32(dtValue.Rows[0]["ID"]) + ",'" + TransferData["data"]["poolReferenceId"].ToString() + "','Success','" + TransferData["statuscode"].ToString() + "','" + TransferData["status"].ToString() + "','','" + Result + "'," + ConstantsData.SSettlement + "");
                                                                                            ErrorShow.AlertMessageWithRedirect(page, "Transaction Success", "WithdrawRequestReceipt.aspx?GUID=" + guid, ConstantsData.CompanyName);
                                                                                        }
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        ErrorShow.AlertMessage(page, "Transaction Failed. Your Amount -" + (Convert.ToDecimal(txtamount.Text) + Charge) + " refund . aeps wallet.", ConstantsData.CompanyName);
                                                                                        cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE','" + Convert.ToInt32(dtValue.Rows[0]["ID"]) + "','','failed','" + TransferData["statuscode"].ToString() + "','" + TransferData["status"].ToString() + "','','" + Result + "'," + ConstantsData.SSettlement + "");

                                                                                    }
                                                                                }
                                                                                else
                                                                                {
                                                                                    ErrorShow.AlertMessage(page, "Transaction Failed. Your Amount -" + (Convert.ToDecimal(txtamount.Text) + Charge) + " refund . aeps wallet.", ConstantsData.CompanyName);
                                                                                    cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE','" + Convert.ToInt32(dtValue.Rows[0]["ID"]) + "','','Refunded','','" + TransferData["status"].ToString() + "','','" + Result + "'," + ConstantsData.SSettlement + "");

                                                                                }

                                                                                //  string Result = ApiPostCall.PostCall(moneyTransferRequestST.GetJson(), SONITechnoCredentrial.PayOutURL);For Techno
                                                                                //JObject TransferData = JObject.Parse(Result);
                                                                                //if (TransferData["code"].ToString().ToUpper() != "ERR")
                                                                                //{
                                                                                //    if (TransferData["data"]["Status"].ToString().ToUpper() == "SUCCESS" || TransferData["data"]["Status"].ToString().ToUpper() == "PENDING")
                                                                                //    {
                                                                                //        cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + Convert.ToInt32(dtValue.Rows[0]["ID"]) + ",'" + TransferData["data"]["STID"].ToString() + "','" + TransferData["data"]["Status"].ToString() + "','" + TransferData["code"].ToString() + "','" + TransferData["mess"].ToString() + "','" + TransferData["data"]["RRN"].ToString() + "','" + Result + "'," + ConstantsData.SSettlement + "");
                                                                                //        if (TransferData["data"]["Status"].ToString().ToUpper() == "PENDING")
                                                                                //        {
                                                                                //            ErrorShow.AlertMessageWithRedirect(page, "Transaction in PROCESSING", "WithdrawRequestReceipt.aspx?GUID=" + guid, ConstantsData.CompanyName);
                                                                                //        }
                                                                                //        else
                                                                                //        {
                                                                                //            ErrorShow.AlertMessageWithRedirect(page, "Transaction Success", "WithdrawRequestReceipt.aspx?GUID=" + guid, ConstantsData.CompanyName);

                                                                                //        }
                                                                                //    }
                                                                                //    else if (TransferData["data"]["Status"].ToString().ToUpper() == "FAILED")
                                                                                //    {
                                                                                //        ErrorShow.AlertMessage(page, "Transaction Failed. Your Amount -" + (Convert.ToDecimal(txtamount.Text) + Charge) + " refund . aeps wallet.", ConstantsData.CompanyName);
                                                                                //        cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE','" + Convert.ToInt32(dtValue.Rows[0]["ID"]) + "','" + TransferData["data"]["STID"].ToString() + "','" + TransferData["data"]["Status"].ToString() + "','" + TransferData["code"].ToString() + "','" + TransferData["mess"].ToString() + "','" + TransferData["data"]["RRN"].ToString() + "','" + Result + "'," + ConstantsData.SSettlement + "");

                                                                                //    }
                                                                                //}
                                                                                //else
                                                                                //{
                                                                                //    ErrorShow.AlertMessage(page, "Transaction Failed. Your Amount -" + (Convert.ToDecimal(txtamount.Text) + Charge) + " refund . aeps wallet.", ConstantsData.CompanyName);
                                                                                //    cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE','" + Convert.ToInt32(dtValue.Rows[0]["ID"]) + "','','Refunded','','" + TransferData["mess"].ToString() + "','','" + Result + "'," + ConstantsData.SSettlement + "");

                                                                                //}

                                                                            }
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        ErrorShow.AlertMessageWithRedirect(page, "Transaction Successfully Trasnfer Aeps to Main Wallet", "settlement.aspx", ConstantsData.CompanyName);
                                                                        cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + Convert.ToInt32(dtValue.Rows[0]["ID"]) + ",'','Success','Wallet','Aeps To Wallet Success','','',''," + ConstantsData.SSettlement + "");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            lblMessage.Text = "AMOUNT MUST BE LESS OR EQUAL TO YOUR WALLET BALANCE";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        lblMessage.Text = "AMOUNT MUST BE LESS OR EQUAL TO YOUR WALLET BALANCE";

                                                    }
                                                }
                                                else
                                                {
                                                    lblMessage.Text = "Your One Request is in pending.So Can not apply next withdrwal rqeuest";

                                                }

                                            }
                                            else
                                            {
                                                lblMessage.Text = "* You Must Have a Bank Account to Submit a Withdraw Request ..!!*";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblMessage.Text = "* Select one account details ..!!*";
                                    }
                                }
                                else
                                {
                                    string Balance = cls.select_data_scalar_string("SELECT BALANCE FROM TBLMLM_AEPS2EWALLETBALANCE WHERE MSRNO = '" + dtMember.Rows[0]["msrno"] + "'");
                                    if (Convert.ToDecimal(Balance) > Convert.ToDecimal(txtamount.Text))
                                    {
                                        string Narration = "Amound Deducted For Withdraw Request-" + guid;
                                        DataTable dtWall = new DataTable();
                                        dtWall = cls.select_data_dt("Exec PROC_AEPSWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"] + "','" + (Convert.ToDecimal(txtamount.Text)) + "','DR','" + Narration + "',''," + ConstantsData.SSettlement + ",'" + guid + "'");
                                        if (dtWall.Rows[0]["MSRNO"].ToString() == "1")
                                        {
                                            DataTable dtValue = cls.select_data_dt("EXEC PROC_AEPS_WITHDRWALREQUEST '" + dtMember.Rows[0]["loginid"] + "','" + txtamount.Text + "','0','" + txtRemark.Text.Trim() + "','" + rdtype.SelectedValue + "','',0,'" + guid + "',0");
                                            if (Convert.ToInt32(dtValue.Rows[0]["ID"]) > 0)
                                            {
                                                ErrorShow.AlertMessageWithRedirect(page, "Transaction Successfully Trasnfer Aeps to Main Wallet", "WithdrawRequestReceipt.aspx", ConstantsData.CompanyName);
                                                cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + Convert.ToInt32(dtValue.Rows[0]["ID"]) + ",'','Success','Wallet','Aeps To Wallet Success','',''," + ConstantsData.SSettlement + "");
                                            }
                                        }
                                        else
                                        {
                                            ErrorShow.AlertMessage(page, dtWall.Rows[0]["STATUS"].ToString(), ConstantsData.CompanyName);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                lblMessage.Text = "* something went wrong ..!!*";
                                lblMessage.Focus();
                            }
                        }
                        else
                        {
                            Response.Redirect("default.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("default.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "* Only " + MaxSenderAmount + " Amount you can send";
                }
            }
            else
            {
                lblMessage.Text = "* Amount should be greater than 0*";
            }
        }
        else
        {
            lblMessage.Text = "Please try after Some Time";
        }
    }

    private Tuple<string, string> GetActivePayoutAPI(string Amount)
    {
        //DataTable dtActivePayoutAPI = cls.select_data_dt("Proc_Get_Active_PayoutAPI '" + dtMember.Rows[0]["RoleID"] + "'," + Amount + "");
        //if (dtActivePayoutAPI.Rows.Count > 0)
        //{
        //    return Tuple.Create(dtActivePayoutAPI.Rows[0]["payoutFrom"].ToString(), dtActivePayoutAPI.Rows[0]["Amount"].ToString());
        //}
        //else
        //{
        //    return Tuple.Create("", "0");
        //}

        return Tuple.Create("1", "1000000");
    }

    protected void rdtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdtype.SelectedValue.ToUpper() == "BANK")
        {
            divTransactionType.Visible = true;
            divTransactionCharge.Visible = true;
            txtRemark.Text = " Settlement To Bank";
        }
        else
        {
            divTransactionCharge.Visible = false;
            divTransactionType.Visible = false;
            txtRemark.Text = "Transfer To Wallet";

        }
    }

    protected void rdtTransactionMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdtTransactionMode.SelectedValue.ToUpper() == "IMPS")
        {
            divTransactionCharge.Visible = true;
        }
        else
        {
            //   divTransactionCharge.Visible = false;
        }
    }


}
