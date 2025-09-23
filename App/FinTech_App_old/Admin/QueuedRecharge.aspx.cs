using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_QueuedRecharge : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();

    DataTable dt = new DataTable();
    int Val = 0;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDropDown.FillDropDown(ddlService, "ActiveService", "Name", "ID", "Select Service");
            FillTransactions();
        }
    }

    private void FillTransactions()
    {

        dt = ObjData.select_data_dt("PROC_MANAGE_RECHARGE_HISTORY 'admin'," + ddlService.SelectedValue + ",'" + txtSeach.Text + "','" + txtFromDate.Text + "','" + txtToDate.Text + "','" + ddlStatus.SelectedValue + "',1");
        rptDataRecharge.DataSource = dt;
        rptDataRecharge.DataBind();
    }

    protected void btnForceSuccess_Click(object sender, EventArgs e)
    {
        cls_connection cls = new cls_connection();
        foreach (RepeaterItem grow in rptDataRecharge.Items)
        {
            CheckBox chkRow = (CheckBox)grow.FindControl("chkRow");
            HiddenField hdnID = (HiddenField)grow.FindControl("hdnID");
            //HiddenField hddTransID = (HiddenField)grow.FindControl("hddTransID");
            if (chkRow.Checked)
            {
                cls.update_data("Update tblRecharge_History set Status='Success',APIMessage='Force Success' where ID='" + hdnID.Value + "' AND Status in ('Queued','Pending')");
            }
        }
        FillTransactions();
    }
    protected void btnForceFail_Click(object sender, EventArgs e)
    {
        cls_connection cls = new cls_connection();
        foreach (RepeaterItem grow in rptDataRecharge.Items)
        {
            CheckBox chkRow = (CheckBox)grow.FindControl("chkRow");
            HiddenField hdnID = (HiddenField)grow.FindControl("hdnID");
            HiddenField hdnLOGINID = (HiddenField)grow.FindControl("hdnLOGINID");
            HiddenField hdnTransID = (HiddenField)grow.FindControl("hdnTransID");
            //  HiddenField hddTransID = (HiddenField)grow.FindControl("hddTransID");
            DataTable dth = cls.select_data_dt("select * from tblRecharge_History where TransID='" + hdnTransID.Value + "'");
            if (dth.Rows.Count > 0)
            {
                if (chkRow.Checked)
                {
                    string Narration = cls.select_data_scalar_string("select Narration from TBL_EWALLETTRANSACTION where TransactionID='" + hdnTransID.Value + "'");
                    cls.update_data("update tblRecharge_History set Status='Refunded', APIMessage='Force Failed' where  ID='" + hdnID.Value + "' AND Status in ('Queued','Pending')");
                    int count = 0;
                    count = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like '%" + hdnTransID.Value + "%' and factor='dr'");
                    if (count == 1)
                    {
                        count = 0;
                        count = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like '%" + hdnTransID.Value + "%' and factor='cr'");
                        if (count == 0)
                        {
                            ObjData.update_data("PROC_EWALLETTRANSACTION '" + hdnLOGINID.Value + "'," + dth.Rows[0]["Amount"].ToString() + ",'Cr','Refund - " + Narration + "',''," + ConstantsData.SRecharge + ",'" + hdnTransID.Value + "'");
                        }
                    }
                }
            }
        }
        FillTransactions();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillTransactions();
    }
}