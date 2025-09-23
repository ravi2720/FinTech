using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PayoutHistory : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Page page;
    ActionButtonPermission actionButtonPermission;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.Payout, dtMember.Rows[0]["RoleID"].ToString());

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

    protected void FillData()
    {
        dt = cls.select_data_dt("EXEC PROC_AEPS_WITHDRAW_REPORT 'WITHDRAW_REPORT_ALL',1,'" + dllStatus.SelectedValue + "','" + txtfromdate.Text + "','" + txtfromdate.Text + "',0,0,'" + ddlMember.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();

            lblSCount.InnerText = dt.Compute("Count(Amount)", "RequestSTatus='Success'").ToString();
            lblPCount.InnerText = dt.Compute("Count(Amount)", "RequestSTatus='Pending'").ToString();
            lblFCount.InnerText = dt.Compute("Count(Amount)", "RequestSTatus='Failed'").ToString();
            lblSsum.InnerText = dt.Compute("SUM(Amount)", "RequestSTatus='Success'").ToString();
            lblPSum.InnerText = dt.Compute("SUM(Amount)", "RequestSTatus='Pending'").ToString();
            lblFSum.InnerText = dt.Compute("SUM(Amount)", "RequestSTatus='Failed'").ToString();
        }
        else
        {
            repeater1.DataSource = null;
            repeater1.DataBind();

        }
    }


    protected void repeater1_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
    {

        HiddenField hdnID = (HiddenField)e.Item.FindControl("hdnID");
        DataTable dthis = cls.select_data_dt("select * from AEPS_WithdrwalRequest where id=" + hdnID.Value + " and RequestStatus='Pending'");
        if (e.CommandName == "IsApprove")
        {
            if (actionButtonPermission.ForceSuccess)
            {
                if (dthis.Rows.Count > 0)
                {
                    cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + hdnID.Value + ",'" + dthis.Rows[0]["TransactionID"].ToString() + "','Success','','Success','','" + dthis.Rows[0]["Response"].ToString() + "'," + ConstantsData.SSettlement + "");
                    ErrorShow.AlertMessage(page, "Force Successfully Done", ConstantsData.CompanyName);
                    Activity.AddActivity(dtMember.Rows[0]["ID"].ToString(), ConstantsData.SSettlement.ToString(), dthis.Rows[0]["RequestID"].ToString(), $"Force Success By {dtMember.Rows[0]["Name"].ToString()}", dthis.Rows[0]["Msrno"].ToString(), "Payout Force Success", cls);
                    FillData();
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Force Success", ConstantsData.CompanyName);
            }
        }
        if (e.CommandName == "IsDelete")
        {
            if (actionButtonPermission.ForceFailed)
            {
                cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE','" + Convert.ToInt32(hdnID.Value) + "','','Refunded','','failed','','" + dthis.Rows[0]["Response"].ToString() + "'," + ConstantsData.SSettlement + "");
                FillData();
                Activity.AddActivity(dtMember.Rows[0]["ID"].ToString(), ConstantsData.SSettlement.ToString(), dthis.Rows[0]["RequestID"].ToString(), $"Force Failed By {dtMember.Rows[0]["Name"].ToString()}", dthis.Rows[0]["Msrno"].ToString(), "Payout Force Failed", cls);
                ErrorShow.AlertMessage(page, "Force Failed Done", ConstantsData.CompanyName);
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Force Failed", ConstantsData.CompanyName);
            }
        }
        if (e.CommandName == "ViewBankDetails")
        {
            FillBankDetails(e.CommandArgument.ToString());
        }
        if (e.CommandName == "CheckStatus")
        {
            if (actionButtonPermission.CheckStatus)
            {
                CheckStatusInstantPay checkStatusInstantPay = new CheckStatusInstantPay();
                checkStatusInstantPay.externalRef = dthis.Rows[0]["RequestID"].ToString();
                checkStatusInstantPay.transactionDate = Convert.ToDateTime(dthis.Rows[0]["RequestDate"].ToString()).ToString("yyyy-MM-dd");
                checkStatusInstantPay.source = "ORDER";
                string Result = NWSPay.CheckStatus(checkStatusInstantPay.GetJson(), Request.Headers["User-Agent"].ToString());
                if (!string.IsNullOrEmpty(Result))
                {
                    JObject jInstant = JObject.Parse(Result);
                    {
                        if (jInstant["data"]["transactionStatusCode"].ToString().ToUpper() == "TXN")
                        {
                            if (jInstant["data"]["transactionStatusCode"].ToString().ToUpper() == "TXN")
                            {
                                cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + hdnID.Value + ",'" + dthis.Rows[0]["TransactionID"].ToString() + "','Success','','" + jInstant["data"]["transactionStatus"].ToString() + "','" + jInstant["data"]["transactionReferenceId"].ToString() + "','" + Result + "'," + ConstantsData.SSettlement + "");
                            }
                            else if (jInstant["data"]["transactionStatusCode"].ToString().ToUpper() == "ERR")
                            {
                                cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + hdnID.Value + ",'" + dthis.Rows[0]["TransactionID"].ToString() + "','failed','','" + jInstant["data"]["transactionStatus"].ToString() + "','" + jInstant["data"]["transactionReferenceId"].ToString() + "','" + Result + "'," + ConstantsData.SSettlement + "");
                            }
                            else
                            {
                                cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + hdnID.Value + ",'" + dthis.Rows[0]["TransactionID"].ToString() + "','failed','','" + jInstant["data"]["transactionStatus"].ToString() + "','" + jInstant["data"]["transactionReferenceId"].ToString() + "','" + Result + "'," + ConstantsData.SSettlement + "");
                            }
                            Activity.AddActivity(dtMember.Rows[0]["ID"].ToString(), ConstantsData.SSettlement.ToString(), dthis.Rows[0]["RequestID"].ToString(), $"Check Status By {dtMember.Rows[0]["Name"].ToString()}", dthis.Rows[0]["Msrno"].ToString(), "PCheck Status", cls);
                        }
                        FillData();
                        ErrorShow.AlertMessage(page, jInstant["status"].ToString(), ConstantsData.CompanyName);
                    }

                }
                else
                {
                    ErrorShow.AlertMessage(page, "Blank Response From Vendor Side Contact To Vendor", ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Check Status", ConstantsData.CompanyName);
            }
        }
    }

    private void FillBankDetails(string msrno)
    {
        try
        {
            dt = cls.select_data_dt("select * from View_MemberBankList where ID = " + msrno + "");
            rptBankData.DataSource = dt;
            rptBankData.DataBind();

        }
        catch (Exception ex)
        {
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }
}