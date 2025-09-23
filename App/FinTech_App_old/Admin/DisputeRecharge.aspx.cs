using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_DisputeRecharge : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();

    DataTable dt = new DataTable();
    int Val = 0;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");

                FillTransactions();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void FillTransactions()
    {
        try
        {
            dt = ObjData.select_data_dt("PROC_MANAGE_RECHARGE_HISTORY 'admindisoute',0,'0','" + txtFromDate.Text + "','" + txtToDate.Text + "','0',0");
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

    protected void rptDataRecharge_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Dispute")
        {
            DataTable dth = ObjData.select_data_dt("select * from tblRecharge_History where id='" + e.CommandArgument.ToString() + "' and Dispute=1 and ReSolve=0");
            if (dth.Rows.Count > 0)
            {
                ObjData.update_data("update tblRecharge_History set status='Failed',ReSolve=1 where id='" + e.CommandArgument.ToString() + "' and Dispute=1 ");
                DataTable dtWallet = ObjData.select_data_dt("select * from TBL_EWALLETTRANSACTION where narration like '%" + dth.Rows[0]["TransID"].ToString() + "%'");
                if (dtWallet.Rows.Count > 0)
                {
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
                                ObjData.update_data("Exec [PROC_EWALLETTRANSACTION] '" + dtmem.Rows[0]["loginid"].ToString() + "'," + Convert.ToDecimal(dtWallet.Rows[i]["Amount"]) + ", 'Cr','Recharge Credit - " + dtWallet.Rows[i]["Narration"].ToString() + "','Refund- " + dtWallet.Rows[i]["Description"].ToString() + "'," + dtWallet.Rows[0]["ServiceID"].ToString() + ",'" + dth.Rows[0]["TransID"].ToString() + "'");
                            }

                        }
                    }
                }
            }
        }
        else if (e.CommandName == "Cancel")
        {
            DataTable dth = ObjData.select_data_dt("select * from tblRecharge_History where id='" + e.CommandArgument.ToString() + "' and Dispute=1 and ReSolve=0");
            if (dth.Rows.Count > 0)
            {
                ObjData.update_data("update tblRecharge_History set RejectDispute=1 where id='" + e.CommandArgument.ToString() + "' and Dispute=1");
            }
        }
        FillTransactions();
    }
}