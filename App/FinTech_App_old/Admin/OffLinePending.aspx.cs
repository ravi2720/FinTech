using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_OffLinePending : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();

    DataTable dt = new DataTable();
    int Val = 0;
    DataTable dtMember;
    Page page;
    public JObject jj;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
             
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", "1");
                BindDropDown.FillDropDown(ddlService, "ActiveServiceBySection", "Name", "ID", "Select Service", ConstantsData.BBPSServiceCategory.ToString());
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void FillTransactions()
    {

        dt = ObjData.select_data_dt("PROC_MANAGE_RECHARGE_HISTORY 'adminoffine'," + ddlService.SelectedValue + ",'" + txtSeach.Text + "','" + txtFromDate.Text + "','" + txtToDate.Text + "','" + ddlStatus.SelectedValue + "'," + 1 + "," + ddlMember.SelectedValue + "");
        if (dt.Rows.Count > 0)
        {
            rptDataRecharge.DataSource = dt;
            rptDataRecharge.DataBind();

            lblSCount.InnerText = dt.Compute("Count(Amount)", "Status='Success'").ToString();
            lblPCount.InnerText = dt.Compute("Count(Amount)", "Status='Pending'").ToString();
            lblFCount.InnerText = dt.Compute("Count(Amount)", "Status='Failed'").ToString();
            lblSsum.InnerText = dt.Compute("SUM(Amount)", "Status='Success'").ToString();
            lblPSum.InnerText = dt.Compute("SUM(Amount)", "Status='Pending'").ToString();
            lblFSum.InnerText = dt.Compute("SUM(Amount)", "Status='Failed'").ToString();
        }
        else
        {
            rptDataRecharge.DataSource = null;
            rptDataRecharge.DataBind();
        }
    }

    protected void btnForceSuccess_Click(object sender, EventArgs e)
    {
        cls_connection cls = new cls_connection();
        foreach (RepeaterItem grow in rptDataRecharge.Items)
        {
            CheckBox chkRow = (CheckBox)grow.FindControl("chkRow");
            HiddenField hdnID = (HiddenField)grow.FindControl("hdnID");
            TextBox txtOpID = (TextBox)grow.FindControl("txtOpID");
            //HiddenField hddTransID = (HiddenField)grow.FindControl("hddTransID");
            if (chkRow.Checked)
            {
                DataTable dtHistory = cls.select_data_dt("Select a.*,b.loginid from tblrecharge_history as a,member as b where a.Memberid=b.msrno and a.ID='" + hdnID.Value + "' and Status in ('success')");
                if (dtHistory.Rows.Count > 0)
                {
                    cls.update_data("Update tblRecharge_History set Status='Success',IsOffSolved=1,APIMessage='" + txtOpID.Text + "' where ID='" + hdnID.Value + "' AND  Status in ('success')");
                }
            }
        }
        FillTransactions();
    }

    protected void btnDispute_Click(object sender, EventArgs e)
    {
        cls_connection cls = new cls_connection();
        foreach (RepeaterItem grow in rptDataRecharge.Items)
        {
            CheckBox chkRow = (CheckBox)grow.FindControl("chkRow");
            HiddenField hdnID = (HiddenField)grow.FindControl("hdnID");
            HiddenField hdnLOGINID = (HiddenField)grow.FindControl("hdnLOGINID");
            HiddenField hdnTransID = (HiddenField)grow.FindControl("hdnTransID");
            if (chkRow.Checked)
            {
                DataTable dth = cls.select_data_dt("select * from tblRecharge_History where TransID='" + hdnTransID.Value + "' and status='Success'");
                if (dth.Rows.Count > 0)
                {
                    ObjData.update_data("update tblRecharge_History set status='Failed' where id='" + dth.Rows[0]["ID"].ToString() + "'");
                    DataTable dtWallet = ObjData.select_data_dt("select * from TBL_EWALLETTRANSACTION where narration like '%" + dth.Rows[0]["TransID"].ToString() + "%'");
                    if (dtWallet.Rows.Count > 0)
                    {
                        cls.update_data("update tblRecharge_History set Status='failed', APIMessage='Failed',IsOffSolved=1 where  ID='" + hdnID.Value + "' AND Status in ('Success')");

                        for (Int32 i = 0; i < dtWallet.Rows.Count; i++)
                        {
                            DataTable dtmem = ObjData.select_data_dt("select * from Member where msrno=" + dtWallet.Rows[i]["Msrno"].ToString() + "");
                            if (dtmem.Rows.Count > 0)
                            {
                                if (dtWallet.Rows[i]["Factor"].ToString().ToUpper() == "CR")
                                {
                                    ObjData.update_data("PROC_EWALLETTRANSACTION '" + dtmem.Rows[0]["LoginID"].ToString() + "'," + Convert.ToDecimal(dtWallet.Rows[i]["Amount"]) + ",'DR','Recharge Failed - " + dtWallet.Rows[i]["Narration"].ToString() + "','Deduct Commission'," + dtWallet.Rows[0]["ServiceID"].ToString() + ",'" + dth.Rows[0]["TransID"].ToString() + "'");
                                }
                                if (dtWallet.Rows[i]["Factor"].ToString().ToUpper() == "DR")
                                {
                                    ObjData.update_data("Exec [PROC_EWALLETTRANSACTION] '" + dtmem.Rows[0]["loginid"].ToString() + "'," + Convert.ToDecimal(dtWallet.Rows[i]["Amount"]) + ", 'Cr','Recharge Credit - " + dtWallet.Rows[i]["Narration"].ToString() + "','Refund- "+ dtWallet.Rows[i]["Description"].ToString() + "'," + dtWallet.Rows[0]["ServiceID"].ToString() + ",'" + dth.Rows[0]["TransID"].ToString() + "'");
                                }

                            }
                        }
                    }
                }
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillTransactions();
    }
}