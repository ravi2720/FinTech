using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Data;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public partial class WithdrawRequestReceipt : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["GUID"] != null)
            {
                CheckData();
            }
        }
    }

    public void CheckData()
    {
        DataTable dtWalletTransaction = cls.select_data_dt("Exec [ManageWithdrawRequest] '" + Request.QueryString["GUID"].ToString() + "'");
        if (dtWalletTransaction.Rows.Count > 0)
        {
            rptTransactionDetails.DataSource = dtWalletTransaction;
            rptTransactionDetails.DataBind();

            lblStatus.Text = dtWalletTransaction.Rows[0]["RequestStatus"].ToString();
            lblStatus.ForeColor = System.Drawing.Color.Green;
            //if (dtWalletTransaction.Rows[0]["RequestStatus"].ToString().ToUpper() == "PENDING" || dtWalletTransaction.Rows[0]["RequestStatus"].ToString().ToUpper() == "PROCESSING")
            //{
            //    imgShow.Visible = true;
            //    tic.Enabled = true;
            //}
            //else
            //{
            //    imgShow.Visible = false;
            //    tic.Enabled = false;
            //}
        }
    }

    protected void btnexportPdf_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Panel1;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=500px,width=650px,scrollbars=1');</script>");
    }

    protected void Unnamed_Tick(object sender, EventArgs e)
    {
        //DataTable dtHistory = new DataTable();
        //DataTable dtMember = new DataTable();
        //dtHistory = cls.select_data_dt("select * from AEPS_WithdrwalRequest where RequestID='" + Request.QueryString["GUID"].ToString().Trim() + "' and RequestStatus in('PROCESSING')  and ManualSystem=0");
        //if (dtHistory.Rows.Count > 0)
        //{
        //    dtMember = cls.select_data_dt("select * from member where msrno='" + dtHistory.Rows[0]["msrno"].ToString() + "'");

        //    string result = ApiPostCallRazorpayx.GetCheckStatus(dtHistory.Rows[0]["TransactionID"].ToString());
        //    JObject Data = JObject.Parse(result);
        //    if (Data["error"] == null)
        //    {
        //        CreatePayoutResult createPayoutResult = JsonConvert.DeserializeObject<CreatePayoutResult>(result);
        //        if (createPayoutResult.status.ToUpper() == "REVERSED")
        //        {
        //            cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE'," + dtHistory.Rows[0]["ID"].ToString() + ",'','"+ createPayoutResult.status + "','Failed','Failed','','" + result + "'");
        //        }
        //        if (createPayoutResult.status.ToUpper() == "PROCESSED")
        //        {
        //            cls.update_data("update AEPS_WithdrwalRequest set rrn='" + createPayoutResult.utr + "', RequestStatus='" + createPayoutResult.status + "' where RequestID='" + Request.QueryString["GUID"].ToString().Trim() + "'");
        //        }
        //        else
        //        {
        //            cls.update_data("update AEPS_WithdrwalRequest set RequestStatus='" + createPayoutResult.status + "' where RequestID='" + Request.QueryString["GUID"].ToString().Trim() + "'");
        //        }
        //        CheckData();
        //    }

        //}
        //else
        //{
        //   // tic.Enabled = false;
        //}
    }
}