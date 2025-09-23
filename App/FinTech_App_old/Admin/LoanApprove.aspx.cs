using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_LoanApprove : System.Web.UI.Page
{
    DataAccess obj = new DataAccess();
    DataTable dt = new DataTable();
    cls_connection cls = new cls_connection();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                Filldata();
                string dtt = DateTime.Now.ToShortDateString();
            }
        }
    }
    public void Filldata()
    {
        try
        {
            dt = obj.GetDataTable("exec DBO.SP_LOAN_GETDATA 'ADAPPROVE'");
            if (dt.Rows.Count > 0)
            {
                rptdata.DataSource = dt;
                rptdata.DataBind();
                lblrecords.Text = "</br><b>" + dt.Rows.Count.ToString() + "</b>";
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {
        if (chkall.Checked == true)
        {
            foreach (RepeaterItem rptitem in rptdata.Items)
            {
                CheckBox chkrpt = rptitem.FindControl("chkrpt") as CheckBox;
                chkrpt.Checked = true;
            }
        }
        else
        {
            foreach (RepeaterItem rptitem in rptdata.Items)
            {
                CheckBox chkrpt = rptitem.FindControl("chkrpt") as CheckBox;
                chkrpt.Checked = false;
            }
        }
    }

    protected void rptdata_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DataTable dtdisburse = obj.GetDataTable("EXEC DBO.SP_LOAN_GETDATA 'GETLOANDATA','',0,0,''," + e.CommandArgument.ToString() + "");
        LinkButton btnemichart = (LinkButton)e.Item.FindControl("btnemichart");
        LinkButton btndoc = (LinkButton)e.Item.FindControl("btndoc");
        Button btnapprove = (Button)e.Item.FindControl("btnapprove");
        CheckBox chkrpt = (CheckBox)e.Item.FindControl("chkrpt");
        if (btnemichart.Text == "View" && e.CommandName.ToString() == "EMI")
        {
            Response.Redirect("dtemichart.aspx?ApplicationID=" + e.CommandArgument.ToString() + "");
            Session["TYPEOFUSER"] = "Admin";
        }
        else if (btndoc.Text == "View Doc" && e.CommandName.ToString() == "Doc")
        {
            Response.Redirect("docverification.aspx?ApplicationID=" + e.CommandArgument.ToString() + "");
        }
        else if (btnapprove.Text == "Approve" && e.CommandName.ToString() == "Approve")
        {
            btnapprove.Enabled = false;
            if (chkrpt.Checked)
            {
                HiddenField hdnMemberID = (HiddenField)e.Item.FindControl("hdnMemberID");
                HiddenField hdnFinalSendAmount = (HiddenField)e.Item.FindControl("hdnFinalSendAmount");

                if (Convert.ToInt32(hdnMemberID.Value) > 0)
                {
                    DataTable dtBankID = cls.select_data_dt("select * from View_MemberBankList where msrno = '" + hdnMemberID.Value + "'");
                    if (dtBankID.Rows.Count > 0)
                    {
                        DataSet ds = new DataSet();
                        int installment_no = 0;
                        if ((obj.ExecuteIntScalar("select installment_type from loan_application where applicationid=" + e.CommandArgument.ToString() + "")) == 5)
                        {
                            installment_no = obj.ExecuteIntScalar("select (DATEDIFF(day,getdate(),(dateadd(month," + obj.ExecuteIntScalar("select Loan_Tenure from loan_application where applicationid = " + e.CommandArgument.ToString() + "") + ",getdate()))))/7");
                        }
                        else if ((obj.ExecuteIntScalar("select installment_type from loan_application where applicationid=" + e.CommandArgument.ToString() + "")) == 9)
                        {
                            installment_no = obj.ExecuteIntScalar("select (DATEDIFF(day,getdate(),(dateadd(month," + obj.ExecuteIntScalar("select Loan_Tenure from loan_application where applicationid = " + e.CommandArgument.ToString() + "") + ",getdate()))))/15");
                        }
                        else
                        {
                            installment_no = obj.ExecuteIntScalar("select Loan_Tenure from loan_application where applicationid = " + e.CommandArgument.ToString() + "");
                        }
                        ds = obj.GetDataSet("EXEC dbo.Sp_loan_OpenAccount 'new-account'," + e.CommandArgument.ToString() + ",'" + dtdisburse.Rows[0]["LoanAMt"].ToString() + "','" + dtdisburse.Rows[0]["Loan_tenure"].ToString() + "','" + dtdisburse.Rows[0]["Installment_type"].ToString() + "'," + installment_no + ",'" + DateTime.Now.ToString("MM-dd-yyyy") + "','" + dtdisburse.Rows[0]["LoanApprovedAMount"].ToString() + "','','','','Loan Disbursement','','',0");
                        if (ds.Tables.Count > 1)
                        {
                            PayoutRequestLoan ObjRequestLoan = new PayoutRequestLoan();
                            ObjRequestLoan.amount = Convert.ToDouble(ds.Tables[0].Rows[0]["Approved_LoanAmt"].ToString());
                            ObjRequestLoan.ifsc = dtBankID.Rows[0]["IFSCCode"].ToString();
                            ObjRequestLoan.note = "IDS Pay Payment Services";
                            ObjRequestLoan.number = dtBankID.Rows[0]["AccountNumber"].ToString();
                            ObjRequestLoan.payment_type = "IMPS";
                            ObjRequestLoan.reference_number = System.Guid.NewGuid().ToString().Replace('-', '0');
                            ObjRequestLoan.udf1 = "";
                            ObjRequestLoan.udf2 = "";
                            ObjRequestLoan.udf3 = "";
                            int ValSend = cls.update_data("PROC_HYPTOLOAN '" + ds.Tables[0].Rows[0]["Approved_LoanAmt"].ToString() + "','Pending','" + ObjRequestLoan.payment_type + "','" + ObjRequestLoan.ifsc + "','" + ObjRequestLoan.number + "','" + ObjRequestLoan.note + "','" + ObjRequestLoan.reference_number + "','" + ObjRequestLoan.udf1 + "','" + ObjRequestLoan.udf2 + "','" + ObjRequestLoan.udf3 + "','" + ds.Tables[0].Rows[0]["LoanAccountNo"].ToString() + "',''");
                            if (ValSend > 0)
                            {
                                string Result = ApiPostCallLoan.PostCall(ObjRequestLoan.GetJson());
                                HyptoBankLoan bsObj = JsonConvert.DeserializeObject<HyptoBankLoan>(Result);
                                if (bsObj.success == true)
                                {
                                    cls.update_data("update HYPTOLOAN set status='" + bsObj.data.status + "',txn_id='" + bsObj.data.txn_id + "',Response='" + Result + "' where reference_number='" + ObjRequestLoan.reference_number + "'");
                                    cls.update_data("update Loan_Disbursement set SendOrNot=1 where LoanAccountNo='" + ds.Tables[0].Rows[0]["LoanAccountNo"].ToString() + "'");
                                }
                                else
                                {
                                    cls.update_data("update HYPTOLOAN set status='Failed',reason='" + JObject.Parse(Result)["reason"].ToString() + "',Response='" + Result + "' where reference_number='" + ObjRequestLoan.reference_number + "'");
                                }
                            }
                            else
                            {

                            }
                            Filldata();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Loan Disbursed Successfully...!Loan Disbursement ID " + ds.Tables[0].Rows[0]["LoanAccountNo"].ToString() + "')", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('" + ds.Tables[0].Rows[0]["column1"].ToString() + "')", true);
                        }
                    }
                    else
                    {
                        ErrorShow.AlertMessage(Page, "Please Add Bank Account Number", "Information");
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Kindly Checked To Approve Loan...!')", true);
            }
        }
    }
}