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

public partial class Admin_ReceiptDMTNew : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
		company = Company.GetCompanyInfo();
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

        DataTable dtWalletTransaction = cls.select_data_dt("Exec GetDataMoneyTransfer '" + Request.QueryString["GUID"].ToString() + "'");
        if (dtWalletTransaction.Rows.Count > 0)
        {
            rptTransactionDetails.DataSource = dtWalletTransaction;
            rptTransactionDetails.DataBind();
        }

    }

    protected void btnexportPdf_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Panel1;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=500px,width=650px,scrollbars=1');</script>");
    }

    protected void Unnamed_Tick(object sender, EventArgs e)
    {
        //try
        //{
        //    DataTable dtHistory = new DataTable();
        //    DataTable dtMember = new DataTable();
        //    dtHistory = cls.select_data_dt("select * from DMTHYPTO where reference_number='" + Request.QueryString["GUID"].ToString().Trim() + "' and status in('PENDING','PROCESSING')");
        //    if (dtHistory.Rows.Count > 0)
        //    {
        //        dtMember = cls.select_data_dt("select * from member where msrno='" + dtHistory.Rows[0]["msrno"].ToString() + "'");
        //        MoneyTransferCheckStatusST moneyTransferCheckStatusST = new MoneyTransferCheckStatusST();
        //        moneyTransferCheckStatusST.OrderID=dtHistory.Rows[0]["reference_number"].ToString();
        //        moneyTransferCheckStatusST.MethodName = "checkstatus";

        //        string result = ApiPostCall.PostCall(moneyTransferCheckStatusST.GetJson(), SONITechnoCredentrial.MoneyURL);

        //        JObject Data = JObject.Parse(result);
        //        if (Data["code"].ToString().ToUpper() != "ERR")
        //        {
        //            if (Data["data"]["Status"].ToString().ToUpper() == "SUCCESS" || Data["data"]["Status"].ToString().ToUpper() == "PENDING")
        //            {
        //                cls.update_data("update DMTHYPTO set status='"+ Data["data"]["Status"].ToString() + "',txn_id='" + Data["data"]["RRN"].ToString() + "',Response='" + result + "' where reference_number='" + Data["data"]["OrderID"].ToString() + "'");

        //            }
        //            else if (Data["data"]["Status"].ToString().ToUpper() == "FAILED")
        //            {
        //                cls.update_data("update DMTHYPTO set status='FAILED',Response='" + result + "' where reference_number='" + Data["data"]["OrderID"].ToString() + "'");

        //                Int32 CountDr = 0, CountCr = 0;
        //                CountDr = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION with(nolock) where factor='dr'  and narration like '%" + dtHistory.Rows[0]["reference_number"].ToString() + "%'");
        //                if (CountDr == 1)
        //                {
        //                    CountCr = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION with(nolock)  where factor='Cr' and narration like '%" + dtHistory.Rows[0]["reference_number"].ToString() + "%'");
        //                    if (CountCr == 0)
        //                    {
        //                        cls.select_data_dt("exec [PROC_EWALLETTRANSACTION_CashDeposit]  '" + dtMember.Rows[0]["LoginID"].ToString() + "', " + Convert.ToDecimal(dtHistory.Rows[0]["FinalAmount"]) + ", 'Cr','Refund - " + dtHistory.Rows[0]["Narration"].ToString() + "','" + System.Guid.NewGuid().ToString() + "'");
        //                    }
        //                }

        //            }
        //            CheckData();
        //        }
        //        else
        //        {
        //            tic.Enabled = false;
        //        }

        //    }
        //    else
        //    {
        //        tic.Enabled = false;

        //    }
        //}
        //catch (Exception ex)
        //{
        //    tic.Enabled = false;

        //}
    }
}