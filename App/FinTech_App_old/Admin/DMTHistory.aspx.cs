using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_DMTHistory : System.Web.UI.Page
{
    DataTable dtMember;
    cls_connection cls = new cls_connection();
    ActionButtonPermission actionButtonPermission;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.DMTHistory, dtMember.Rows[0]["RoleID"].ToString());
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
        dt = cls.select_data_dt("ManageMoneyTransfer '',1,'" + txtfromdate.Text + "','" + txttodate.Text + "','0','" + ddlMember.SelectedValue + "'");

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



    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "CheckStatus")
        {
            if (actionButtonPermission.CheckStatus)
            {
                //DataTable dt = new DataTable();
                //dt = cls.select_data_dt("select * from MoneyTransfer where OrderID='" + e.CommandArgument.ToString() + "' and status='Pending'");
                //if (dt.Rows.Count > 0)
                //{
                //    MoneyTransferCheckStatusST moneyTransferCheckStatusST = new MoneyTransferCheckStatusST();
                //    moneyTransferCheckStatusST.OrderID = hdnRefNumber.Value.Trim();
                //    moneyTransferCheckStatusST.MethodName = "checkstatus";

                //    string result = ApiPostCall.PostCall(moneyTransferCheckStatusST.GetJson(), SONITechnoCredentrial.MoneyURL);

                //    JObject Data = JObject.Parse(result);

                //    if (Data["code"].ToString().ToUpper() != "ERR")
                //    {
                //        if (Data["data"]["Status"].ToString().ToUpper() == "SUCCESS" || Data["data"]["Status"].ToString().ToUpper() == "PENDING")
                //        {
                //            cls.update_data("update DMTHYPTO set status='" + Data["data"]["Status"].ToString() + "',txn_id='" + Data["data"]["RRN"].ToString() + "',Response='" + result + "' where reference_number='" + Data["data"]["OrderID"].ToString() + "'");

                //        }
                //        else if (Data["data"]["Status"].ToString().ToUpper() == "FAILED")
                //        {
                //            cls.update_data("Update DMTHYPTO set Status='Refund',Response='" + result + "' where reference_number='" + hdnRefNumber.Value + "'");
                //            DataTable dtHistory = new DataTable();
                //            dtHistory = cls.select_data_dt("select * from DMTHYPTO where reference_number='" + hdnRefNumber.Value + "'");
                //            string MemberID = cls.select_data_scalar_string("select loginid from member where msrno=" + dtHistory.Rows[0]["msrno"] + "");
                //            Int32 CountCR = 0;
                //            Int32 CountDR = 0;
                //            CountCR = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like '%" + hdnRefNumber.Value + "%' and factor='cr'");
                //            CountDR = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like '%" + hdnRefNumber.Value + "%' and factor='dr'");
                //            if (CountDR == 1)
                //            {
                //                if (CountCR == 0)
                //                {
                //                    cls.update_data("Exec [PROC_EWALLETTRANSACTION] '" + MemberID + "'," + Convert.ToDecimal(dtHistory.Rows[0]["FinalAmount"]) + ", 'CR','Refund - " + dtHistory.Rows[0]["Narration"].ToString() + "','" + dtHistory.Rows[0]["reference_number"].ToString() + "'");
                //                }
                //            }

                //        }
                //    }
                //    else
                //    {
                //        ErrorShow.AlertMessage(page, Data["mess"].ToString(), ConstantsData.CompanyName);
                //    }


                //}

              
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Check Status", ConstantsData.CompanyName);
            }
        }
        else if (e.CommandName == "Success")
        {
            if (actionButtonPermission.ForceSuccess)
            {
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("select * from MoneyTransfer where OrderID='" + e.CommandArgument.ToString() + "' and status='Pending'");
                if (dt.Rows.Count > 0)
                {
                    cls.update_data("Update MoneyTransfer set Status='Success' where OrderID='" + dt.Rows[0]["OrderID"] + "'");
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Fource Success", ConstantsData.CompanyName);
            }
        }
        else if (e.CommandName == "Failed")
        {
            if (actionButtonPermission.ForceFailed)
            {
                DataTable dt = new DataTable();
                dt = cls.select_data_dt("select * from MoneyTransfer where OrderID='" + e.CommandArgument.ToString() + "' and status in ('Pending','Success')");
                if (dt.Rows.Count > 0)
                {
                    DataTable dtMemberMaster = cls.select_data_dt("select * from Member where msrno='" + dt.Rows[0]["MSrno"] + "'");
                    cls.update_data("Update MoneyTransfer set Status='Refund' where OrderID='" + dt.Rows[0]["OrderID"] + "'");
                    DataTable dtHistory = new DataTable();
                    dtHistory = cls.select_data_dt("select * from MoneyTransfer where OrderID='" + dt.Rows[0]["OrderID"] + "'");
                  
                    Int32 CountCR = 0;
                    CountCR = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like 'DMT TransID-%" + dt.Rows[0]["OrderID"] + "%' and factor='cr'");
                    if (CountCR == 0)
                    {
                        string Narration = "DMT TransID-" + dt.Rows[0]["OrderID"];
                        cls.update_data("Exec [PROC_EWALLETTRANSACTION] '" + dtMemberMaster.Rows[0]["loginid"].ToString() + "'," + Convert.ToDecimal(dt.Rows[0]["FinalAmount"]) + ", 'CR','Refund - " + Narration + "',''," + ConstantsData.SMoneyTransfer + ",'" + dt.Rows[0]["OrderID"].ToString() + "'");
                    }
                    ErrorShow.AlertMessage(page, "Force Failed Successfully", ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Fource Success", ConstantsData.CompanyName);
            }
        }

        fillTransactionDetails();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }



    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        //
    }
}