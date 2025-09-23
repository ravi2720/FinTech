using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_LoanSentAmountList : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                fillTransactionDetails();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void fillTransactionDetails()
    {        
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("PAID_LOAN_LIST");
       
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



    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "CheckStatus")
        {
            string referenceNumber = e.CommandArgument.ToString();

            DataTable dt = cls.select_data_dt($"SELECT * FROM HYPTOLOAN WHERE reference_number='{referenceNumber}' AND status='Pending'");
            if (dt.Rows.Count > 0)
            {
                string loanAccountNo = dt.Rows[0]["LoanAccountNo"].ToString();
                string loginId = dt.Rows[0]["LoginID"].ToString();
                string narration = dt.Rows[0]["Narration"].ToString();
                string finalAmount = dt.Rows[0]["FinalAmount"].ToString();

                string result = ApiPostCallLoan.GetCall(referenceNumber);
                HyptoBankLoan bsObj = JsonConvert.DeserializeObject<HyptoBankLoan>(result);

                if (bsObj.success == true && bsObj.data.status.ToUpper() == "COMPLETED")
                {
                    cls.update_data($"UPDATE HYPTOLOAN SET status='Success', txn_id='{bsObj.data.txn_id}', Response='{result}' WHERE reference_number='{bsObj.data.reference_number}'");
                    cls.update_data($"UPDATE Loan_Disbursement SET SendOrNot=1 WHERE LoanAccountNo='{loanAccountNo}'");
                }
                else
                {
                    // Refund if success = false or status is not COMPLETED
                    DataTable dtHistory = cls.select_data_dt($"SELECT * FROM DMTHYPTO WHERE reference_number='{referenceNumber}'");
                    if (dtHistory.Rows.Count > 0)
                    {
                        int countCR = cls.select_data_scalar_int($"SELECT COUNT(*) FROM TBL_EWALLETTRANSACTION WHERE narration LIKE '%{referenceNumber}%' AND factor='cr'");
                        if (countCR == 0)
                        {
                            cls.update_data($"EXEC PROC_EWALLETTRANSACTION '{loginId}', {finalAmount}, 'CR', 'Refund - {narration}', '{referenceNumber}'");
                        }

                        cls.update_data($"UPDATE DMTHYPTO SET Status='Refund', Response='{result}' WHERE reference_number='{referenceNumber}'");
                    }
                }

                fillTransactionDetails();
            }
        }
    }

}