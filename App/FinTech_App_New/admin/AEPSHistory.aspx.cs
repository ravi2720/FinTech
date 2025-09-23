using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;

public partial class Admin_AEPSHistory : System.Web.UI.Page
{
    DataTable dtMember;
    cls_connection cls = new cls_connection();
    Page page;
    ActionButtonPermission actionButtonPermission;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.AEPSHistory, dtMember.Rows[0]["RoleID"].ToString());

            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", "1");
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void fillTransactionDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Exec ManageAEPS_History '" + txtSearch.Text + "','1','" + txtfromdate.Text + "', '" + txttodate.Text + "','" + dllStatus.SelectedValue + "'," + ddlMember.SelectedValue + "");
        if (dt.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();

            lblSCount.InnerText = dt.Compute("Count(Amount)", "Status='Success'").ToString();
            lblPCount.InnerText = dt.Compute("Count(Amount)", "Status='Pending'").ToString();
            lblFCount.InnerText = dt.Compute("Count(Amount)", "Status='Failed'").ToString();
            lblSsum.InnerText = dt.Compute("SUM(Amount)", "Status='Success'").ToString();
            lblPSum.InnerText = dt.Compute("SUM(Amount)", "Status='Pending'").ToString();
            lblFSum.InnerText = dt.Compute("SUM(Amount)", "Status='Failed'").ToString();
        }
        else
        {
            gvTransactionHistory.DataSource = null;
            gvTransactionHistory.DataBind();
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }

    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }

    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "CheckStatus")
        {
            if (!actionButtonPermission.CheckStatus)
            {
                ErrorShow.AlertMessage(page, "Not Permission For Check Status", ConstantsData.CompanyName);
                return;
            }

            try
            {
                DataTable dthis = cls.select_data_dt("SELECT * FROM aeps_history WHERE referenceno = '" + e.CommandArgument.ToString() + "' AND Status = 'Pending'");
                if (dthis.Rows.Count == 0)
                {
                    ErrorShow.AlertMessage(page, "No Pending Transaction Found", ConstantsData.CompanyName);
                    return;
                }

                // Prepare API request
                CheckStatusInstantPay checkStatusInstantPay = new CheckStatusInstantPay
                {
                    externalRef = dthis.Rows[0]["referenceno"].ToString(),
                    transactionDate = Convert.ToDateTime(dthis.Rows[0]["Adddate"]).ToString("yyyy-MM-dd"),
                    source = "ORDER"
                };

                string resultJson = ApiPostCallInstantPay.CheckStatus(checkStatusInstantPay.GetJson(), Request.Headers["User-Agent"].ToString());
                if (string.IsNullOrEmpty(resultJson))
                {
                    ErrorShow.AlertMessage(page, "Blank Response From Vendor Side Contact To Vendor", ConstantsData.CompanyName);
                    return;
                }

                JObject jInstant = JObject.Parse(resultJson);

                string statusCode = jInstant["statusCode"]?.ToString();
                string transactionStatus = jInstant["data"]?["transactionStatus"]?.ToString();
                string spKey = jInstant["data"]?["order"]?["spKey"]?.ToString()?.ToUpper();

                // Handle transaction not found
                if (transactionStatus == "TRANSACTION NOT FOUND IN GIVEN TRANSACTION DATE")
                {
                    cls.update_data("UPDATE aeps_history SET Status = 'Failed' WHERE referenceno = '" + e.CommandArgument.ToString() + "'");
                    fillTransactionDetails();
                    ErrorShow.AlertMessage(page, transactionStatus, ConstantsData.CompanyName);
                    return;
                }

                // Handle transaction success
                if (statusCode == "TXN" && transactionStatus == "TRANSACTION SUCCESSFUL" && spKey == "WAP")
                {
                    string referenceId = jInstant["data"]?["order"]?["referenceId"]?.ToString();
                    string transactionAmount = jInstant["data"]?["transactionAmount"]?.ToString();
                    string account = jInstant["data"]?["order"]?["account"]?.ToString();

                    // Update AEPS history and credit wallet
                    int val = cls.update_data("proc_Credit_AEPS_History '" + e.CommandArgument.ToString() + "','" + dthis.Rows[0]["msrno"].ToString() + "','CW','Success','Success',0,'" + referenceId + "','','" + resultJson + "'");

                    // Wallet credit and commission distribution
                    try
                    {
                        string narration = "Aeps TransID-" + e.CommandArgument.ToString();
                        string msrno = dthis.Rows[0]["Msrno"].ToString();
                        string loginId = cls.select_data_scalar_string("SELECT loginid FROM member WHERE msrno = " + msrno);
                        string description = $"AEPS Amount - {transactionAmount}. Done With Aadhar No {account}. TransID - {e.CommandArgument.ToString()}";

                        cls.update_data("exec PROC_AEPSWALLETTRANSACTION '" + loginId + "','" + transactionAmount + "','CR','" + narration + "','" + description + "','" + ConstantsData.SAeps + "','" + e.CommandArgument.ToString() + "'");
                        cls.update_data("Exec Proc_AEPS_DistributeCommission '" + msrno + "'," + transactionAmount + ", '" + e.CommandArgument.ToString() + "', '" + ConstantsData._TDS + "','" + ConstantsData.SAEPSCommission + "','" + ConstantsData._TDSVal + "'");
                    }
                    catch (Exception ex)
                    {
                        ErrorShow.AlertMessage(page, "Error during wallet/commission update: " + ex.Message, ConstantsData.CompanyName);
                    }

                    fillTransactionDetails();
                }
            }
            catch (Exception exOuter)
            {
                ErrorShow.AlertMessage(page, "Unexpected Error: " + exOuter.Message, ConstantsData.CompanyName);
            }
        }
        else if (e.CommandName == "fs")
        {
            if (!actionButtonPermission.ForceSuccess)
            {
                ErrorShow.AlertMessage(page, "Not Permission For Force Success", ConstantsData.CompanyName);
            }

            // Force Success logic can go here if needed
        }
    }
}
