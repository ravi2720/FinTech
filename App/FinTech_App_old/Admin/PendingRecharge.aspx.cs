using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_PendingRecharge : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();
    cls_myMember clsm = new cls_myMember();
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
        try
        {
            dt = ObjData.select_data_dt("PROC_MANAGE_RECHARGE_HISTORY 'admin'," + ddlService.SelectedValue + ",'" + txtSeach.Text + "','" + txtFromDate.Text + "','" + txtToDate.Text + "','"+ ddlStatus.SelectedValue + "',0");
            rptDataRecharge.DataSource = dt;
            rptDataRecharge.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillTransactions();
    }

    protected void btnCheckStatus_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (RepeaterItem grow in rptDataRecharge.Items)
            {
                CheckBox chk = (CheckBox)grow.FindControl("chk");
                HiddenField hdId = (HiddenField)grow.FindControl("hdnID");
                if (chk.Checked)
                {
                    if (ObjData.select_data_scalar_int("Select Count(*) from tblrecharge_history where id='" + Convert.ToInt32(hdId.Value) + "' and Status='Pending'") > 0)
                    {
                        clsm.RechargePendingProcess(Convert.ToInt32(hdId.Value), "WEB");
                    }
                }
            }
            FillTransactions();
        }
        catch (Exception ex)
        {

        }
    }
}