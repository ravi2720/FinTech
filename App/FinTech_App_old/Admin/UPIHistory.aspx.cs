using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_UPIHistory : System.Web.UI.Page
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
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", dtMember.Rows[0]["ID"].ToString());
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
        dt = cls.select_data_dt("ManageUPITransfer '',1,'" + txtfromdate.Text + "','" + txttodate.Text + "','0','" + ddlMember.SelectedValue + "'");

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
                dt = cls.select_data_dt("select * from UPITransfer where OrderID='" + e.CommandArgument.ToString() + "' and status='Pending'");
                if (dt.Rows.Count > 0)
                {
                    cls.update_data("Update UPITransfer set Status='Success' where OrderID='" + dt.Rows[0]["OrderID"] + "'");
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
                dt = cls.select_data_dt("select * from UPITransfer where OrderID='" + e.CommandArgument.ToString() + "' and status in ('Pending','Success')");
                if (dt.Rows.Count > 0)
                {
                    DataTable dtMemberMaster = cls.select_data_dt("select * from Member where msrno='" + dt.Rows[0]["MSrno"] + "'");
                    cls.update_data("Update UPITransfer set Status='Refund' where OrderID='" + dt.Rows[0]["OrderID"] + "'");
                    DataTable dtHistory = new DataTable();
                    dtHistory = cls.select_data_dt("select * from UPITransfer where OrderID='" + dt.Rows[0]["OrderID"] + "'");

                    Int32 CountCR = 0;
                    CountCR = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like 'DMT TransID-%" + dt.Rows[0]["OrderID"] + "%' and factor='cr'");
                    if (CountCR == 0)
                    {
                        string Narration = "Send UPI-" + dt.Rows[0]["OrderID"];
                        cls.update_data("Exec [PROC_EWALLETTRANSACTION] '" + dtMemberMaster.Rows[0]["loginid"].ToString() + "'," + Convert.ToDecimal(dt.Rows[0]["FinalAmount"]) + ", 'CR','Refund - " + Narration + "',''," + ConstantsData.SUPITransfer + ",'" + dt.Rows[0]["OrderID"].ToString() + "'");
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