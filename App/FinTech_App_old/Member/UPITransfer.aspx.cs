using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;

public partial class Member_UPITransfer : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;


        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (dtMember.Rows.Count > 0)
            {
                if (!IsPostBack)
                {

                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Int32 HoldCount = cls.select_data_scalar_int("select count(*) from Member where Msrno=" + dtMember.Rows[0]["msrno"].ToString() + " and OnHold=1");
        if (HoldCount == 0)
        {
            string guid = "SU" + System.Guid.NewGuid().ToString().Replace('-', '0');
            DataTable dtWalletBalance = new DataTable();
            decimal MyBalance = 0;

            dtWalletBalance = cls.select_data_dt("select * from VIEW_MEMBERBALANCE where Msrno=" + dtMember.Rows[0]["Msrno"] + "");
            if (dtWalletBalance.Rows.Count > 0)
            {
                MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["Balance"]);
            }

            if (Convert.ToDecimal(txtAmount.Text) > 0)
            {
                decimal surcharge = MoneyTransfer_EkoAPI.GetUPITransferSurcharge(dtMember.Rows[0]["packageid"].ToString().ToInt32(), Convert.ToDecimal(txtAmount.Text.ToString()));
                decimal TotalAmount = surcharge + Convert.ToDecimal(txtAmount.Text);
                if (MyBalance >= TotalAmount) // Verification fee
                {
                    string Narration = "Send UPI TransID-" + guid;

                    string Dec = "UPI Topup with Charges Rs." + Convert.ToDecimal(txtAmount.Text).ToString() + " + " + surcharge + " + Txn:-" + guid;//"DMT2 Fund Transfer(Account: " + lblSendMoneyAccountNo.Text + ")";                                       

                    DataTable dtval = cls.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"].ToString() + "','" + TotalAmount + "','DR','" + Narration + "','" + Dec + "'," + ConstantsData.SUPITransfer + ",'" + guid + "'");
                    if (dtval.Rows[0]["msrno"].ToString() == "1")
                    {
                        int Val = cls.update_data("AddEditUPITransfer " + dtMember.Rows[0]["Msrno"].ToString() + "," + txtAmount.Text + "," + surcharge + ",'" + txtUPI.Text + "','" + txtUPIName.Text + "','Pending','IMPS','','" + guid + "','',''");
                        if (Val > 0)
                        {
                            RootInstantPayout rootInstantPayout = new RootInstantPayout();
                            Payee payee = new Payee();
                            payee.name = txtUPIName.Text;
                            payee.bankIfsc = "";
                            payee.accountNumber = txtUPI.Text;
                            rootInstantPayout.payee = payee;
                            Payer payer = new Payer();
                           
                            rootInstantPayout.payer = payer;
                            Location objLocation = new Location();
                            objLocation = objLocation.GetLocationDataCurrent("103.73.190.82");
                            rootInstantPayout.externalRef = guid;
                            rootInstantPayout.latitude = objLocation.Latitude;
                            rootInstantPayout.longitude = objLocation.Longitude;
                            rootInstantPayout.transferMode = "UPI";
                            rootInstantPayout.transferAmount = txtAmount.Text;
                            rootInstantPayout.remarks = "UPI Settlement";
                            rootInstantPayout.alertEmail = dtMember.Rows[0]["Email"].ToString();

                            {
                                string result = InstantPayout.PostCall(rootInstantPayout.GetJson(), "payout", Request.Headers["User-Agent"].ToString());

                                JObject TransferData = JObject.Parse(result);
                                if (InstantPayout.arrTemplate.Contains(TransferData["statuscode"].ToString()))
                                {
                                    if (TransferData["statuscode"].ToString().ToUpper() == "TXN" || TransferData["statuscode"].ToString().ToUpper() == "TUP")
                                    {
                                        if (TransferData["statuscode"].ToString().ToUpper() == "TUP")
                                        {
                                            cls.update_data("Update UPITransfer set Status='Pending',VendorID='" + TransferData["data"]["poolReferenceId"].ToString() + "',Response='" + result + "',Message='" + TransferData["status"].ToString() + "',RRN='" + TransferData["data"]["txnReferenceId"].ToString() + "' where OrderID='" + guid + "'");
                                        }
                                        else
                                        {
                                            cls.update_data("Update UPITransfer set Status='Success',VendorID='" + TransferData["data"]["poolReferenceId"].ToString() + "',Response='" + result + "',Message='" + TransferData["status"].ToString() + "',RRN='" + TransferData["data"]["txnReferenceId"].ToString() + "' where OrderID='" + guid + "'");
                                        }
                                        ErrorShow.AlertMessageWithRedirect(page, "Transaction Successfully Done", "UPISendReceipt.aspx?TransID=" + guid + "", ConstantsData.CompanyName);
                                    }
                                    else
                                    {
                                        cls.update_data("Update UPITransfer set Status='Refund',Message='" + TransferData["status"].ToString() + "',Response='" + result + "' where OrderID='" + guid + "'");
                                        DataTable dtHistory = new DataTable();
                                        dtHistory = cls.select_data_dt("select * from UPITransfer where OrderID='" + guid + "'");

                                        Int32 CountCR = 0;
                                        CountCR = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like '%" + guid + "%' and factor='cr'");
                                        if (CountCR == 0)
                                        {
                                            Narration = "Send UPI TransID-" + guid;
                                            cls.update_data("Exec [PROC_EWALLETTRANSACTION] '" + dtMember.Rows[0]["loginid"].ToString() + "'," + Convert.ToDecimal(dtHistory.Rows[0]["FinalAmount"]) + ", 'CR','Refund - " + Narration + "',''," + ConstantsData.SUPITransfer + ",'" + dtHistory.Rows[0]["OrderID"].ToString() + "'");
                                        }
                                        ErrorShow.AlertMessage(page, "Transaction FAILURE Successfully. Your Amount -" + (Convert.ToDecimal(txtAmount.Text)) + " refund.", ConstantsData.CompanyName);
                                    }
                                }
                                else
                                {
                                    cls.update_data("Update UPITransfer set Status='Refund',Response='" + result + "' where OrderID='" + guid + "'");
                                    DataTable dtHistory = new DataTable();
                                    dtHistory = cls.select_data_dt("select * from UPITransfer where OrderID='" + guid + "'");

                                    Int32 CountCR = 0;
                                    CountCR = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like '%" + guid + "%' and factor='cr'");
                                    if (CountCR == 0)
                                    {
                                        Narration = "Send UPI TransID-" + guid;
                                        cls.update_data("Exec [PROC_EWALLETTRANSACTION] '" + dtMember.Rows[0]["loginid"].ToString() + "'," + Convert.ToDecimal(dtHistory.Rows[0]["FinalAmount"]) + ", 'CR','Refund - " + Narration + "',''," + ConstantsData.SUPITransfer + ",'" + dtHistory.Rows[0]["OrderID"].ToString() + "'");
                                    }
                                    ErrorShow.AlertMessage(page, "Transaction FAILURE Successfully. Your Amount -" + (Convert.ToDecimal(txtAmount.Text)) + " refund.", ConstantsData.CompanyName);
                                }
                            }

                        }

                    }
                    else
                    {
                        ErrorShow.AlertMessage(page, "AMOUNT MUST BE LESS OR EQUAL TO YOUR WALLET BALANCE", ConstantsData.CompanyName);
                    }
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Low Balance", ConstantsData.CompanyName);

                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Enter Right Amount", ConstantsData.CompanyName);

            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "try after some time", ConstantsData.CompanyName);
        }
        //mulSendR.ActiveViewIndex = 2;
    }

    protected void btnVerification_Click(object sender, EventArgs e)
    {
        {
            dtMember = (DataTable)Session["dtMember"];
            decimal MyBalance = 0;
            string agentID = System.Guid.NewGuid().ToString().Replace('-', 'A');
            DataTable dtWalletBalance = new DataTable();
            dtWalletBalance = cls.select_data_dt("select * from VIEW_MEMBERBALANCE where Msrno=" + dtMember.Rows[0]["Msrno"] + "");
            if (dtWalletBalance.Rows.Count > 0)
            {
                MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["Balance"]);

            }
            if (MyBalance >= Convert.ToDecimal(MoneyTransfer_EkoAPI.Beneficiary_VerificationFee)) // Verification fee
            {

                if (MoneyTransfer_EkoAPI.CheckMinimumBalance(Convert.ToInt32(dtMember.Rows[0]["Msrno"]), Convert.ToDecimal(MoneyTransfer_EkoAPI.Beneficiary_VerificationFee), MyBalance))
                {

                    string Desc = "" + txtUPI.Text + " UPI Verification With TransID-" + agentID;
                    string narration = "Account Verification TransID-" + agentID;
                    decimal deductAmount = (Convert.ToDecimal(MoneyTransfer_EkoAPI.Beneficiary_VerificationFee));

                    AccountVerifyInstantPay accountVerifyInstantPay = new AccountVerifyInstantPay();
                    Payee payee = new Payee();
                    payee.accountNumber = txtUPI.Text;
                    payee.bankIfsc = "";
                    accountVerifyInstantPay.payee = payee;
                    accountVerifyInstantPay.externalRef = agentID;
                    Location objLocation = new Location();
                    objLocation = objLocation.GetLocationData();
                    accountVerifyInstantPay.latitude = objLocation.Latitude;
                    accountVerifyInstantPay.longitude = objLocation.Longitude;
                    string result = InstantPayout.AccountVerifyPostCall(accountVerifyInstantPay.GetJson(), "verifyBankAccount", Request.Headers["User-Agent"].ToString());
                    JObject jObject = JObject.Parse(result);

                    if (jObject["statuscode"].ToString().ToUpper() == "TXN")
                    {
                        int Val = cls.update_data("AddEditUPITransfer " + dtMember.Rows[0]["Msrno"].ToString() + "," + MoneyTransfer_EkoAPI.Beneficiary_VerificationFee + ",0,'" + txtUPI.Text + "','" + txtUPIName.Text + "','Success','IMPS','','" + agentID + "','','" + jObject["data"]["txnReferenceId"].ToString() + "','" + result + "'");
                        if (Val > 0)
                        {
                            cls.select_data_dt("exec [PROC_EWALLETTRANSACTION]  '" + dtMember.Rows[0]["LoginID"].ToString() + "', " + deductAmount + ", 'Dr','" + narration + "','" + Desc + "'," + ConstantsData.SAccountVerfication + ",'" + agentID + "'");
                            txtUPIName.Text = jObject["data"]["payee"]["name"].ToString();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowMessage(2,'" + jObject["statusMessage"].ToString() + "');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowMessage(2,'Low Balance! Minimum balance limit exceed.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowMessage(2,'You have Insufficient Balance.');", true); ;
            }
        }
    }
}