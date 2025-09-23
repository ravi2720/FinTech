using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_UPIHIstoryBank : System.Web.UI.Page
{
    DataTable dtMemberMaster, dtMember;
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", "1");
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                fillTransactionDetails();
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
        dt = cls.select_data_dt("ManageFundAdd_UPI_Virtaul_Transaction '',1,'" + txtfromdate.Text + "','" + txttodate.Text + "','0','" + ddlMember.SelectedValue + "'");

        if (dt.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();

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

    protected void gvTransactionHistory_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {
        //if (e.CommandName == "CheckStatus")
        //{
        //    DataTable dtHistory = new DataTable();
        //    DataTable dtMember = new DataTable();
        //    dtHistory = cls.select_data_dt("select * from DMTHYPTO where reference_number='" + e.CommandArgument.ToString() + "' and status in('PENDING','PROCESSING')");
        //    if (dtHistory.Rows.Count > 0)
        //    {
        //        dtMember = cls.select_data_dt("select * from member where msrno='" + dtHistory.Rows[0]["msrno"].ToString() + "'");

        //        string result = ApiPostCallRazorpayx.GetCheckStatus(dtHistory.Rows[0]["RazarID"].ToString());
        //        JObject Data = JObject.Parse(result);
        //        if (Data["error"] == null)
        //        {
        //            CreatePayoutResult createPayoutResult = JsonConvert.DeserializeObject<CreatePayoutResult>(result);
        //            cls.update_data("update DMTHYPTO set RazarID='" + createPayoutResult.id + "',status='" + createPayoutResult.status + "',txn_id='" + createPayoutResult.utr + "' where reference_number='" + dtHistory.Rows[0]["reference_number"].ToString() + "'");

        //            if (createPayoutResult.status.ToUpper() == "REVERSED")
        //            {
        //                Int32 CountDr = 0, CountCr = 0;
        //                CountDr = cls.select_data_scalar_int("select * from TBL_EWALLETTRANSACTION where factor='dr'  and narration like '%" + dtHistory.Rows[0]["reference_number"].ToString() + "%'");
        //                if (CountDr == 1)
        //                {
        //                    CountCr = cls.select_data_scalar_int("select * from TBL_EWALLETTRANSACTION where factor='Cr' and narration like '%" + dtHistory.Rows[0]["reference_number"].ToString() + "%'");
        //                    if (CountCr == 0)
        //                    {
        //                        cls.select_data_dt("exec [PROC_EWALLETTRANSACTION_CashDeposit]  '" + dtMember.Rows[0]["LoginID"].ToString() + "', " + Convert.ToDecimal(dtHistory.Rows[0]["FinalAmount"]) + ", 'Cr','Refund - " + dtHistory.Rows[0]["Narration"].ToString() + "','" + System.Guid.NewGuid().ToString() + "'");
        //                    }
        //                }

        //            }


        //        }
        //    }
        //    fillTransactionDetails();
        //}
    }
}