using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_MATMHistory : System.Web.UI.Page
{
    DataTable dtMember;
    cls_connection cls = new cls_connection();
    Page page;
    ActionButtonPermission actionButtonPermission;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.MATM, dtMember.Rows[0]["RoleID"].ToString());

            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", "1");
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
        dt = cls.select_data_dt("Exec ManageMATMHistory '" + txtSearch.Text + "','1','" + txtfromdate.Text + "', '" + txttodate.Text + "','" + dllStatus.SelectedValue + "'," + ddlMember.SelectedValue + "");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }

    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }

    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "CheckStatus")
        {
            if (actionButtonPermission.CheckStatus)
            {
                DataTable dthis = cls.select_data_dt("select * from matmhistory where Refid='" + e.CommandArgument.ToString() + "' and Status='Pending'");
                if (dthis.Rows.Count > 0)
                {
                    string RefId = e.CommandArgument.ToString();
                    MATM_RequestMH objRequest = new MATM_RequestMH();
                    objRequest.saltkey = clsMahagram.SaltKey;
                    objRequest.secretekey = clsMahagram.Secret_Key;
                    objRequest.referenceid = RefId;
                    string strJson = JsonConvert.SerializeObject(objRequest);
                    string result = clsMahagram.Call_API(strJson);
                    if (result != "0")
                    {
                        JObject obj = JObject.Parse(result);
                        if (Convert.ToString(obj["statuscode"]).ToUpper() == "000")
                        {
                            MATMCheckStatusRoot objRoot = JsonConvert.DeserializeObject<MATMCheckStatusRoot>(result);
                            for (int i = 0; i < objRoot.Data.Count; i++)
                            {
                                string Txntype = "";
                                if (Convert.ToDecimal(objRoot.Data[i].amount) > 0) { Txntype = "CW"; } else { Txntype = "BE"; }

                                string BcId = objRoot.Data[i].bcid;
                                string TerminalId = objRoot.Data[i].pid;
                                string MerchantId = objRoot.Data[i].mid;
                                string Amount = objRoot.Data[i].amount;//0 for BE and amount for CW
                                string TxnStatus = objRoot.Data[i].status;
                                string ReferenceId = objRoot.Data[i].stanno;
                                string Pipe = "1";
                                string EndCustMobile = objRoot.Data[i].udf1;
                                string rrn = objRoot.Data[i].rrn;
                                string Timestamp = objRoot.Data[i].createdate;
                                string clientRefID = objRoot.Data[i].clientrefid;
                                string bankMessage = objRoot.Data[i].bankmessage;
                                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;

                                string Message = "Success";
                                string bankCode = "";
                                string InvoiceNo = "";
                                string msrno = "0";
                                DataTable dtMemberATM = new DataTable();
                                DataTable dthisRef = new DataTable();
                                dthisRef = cls.select_data_dt("select * from MATMRequestID where RefID='" + RefId + "' and Status='Pending' and BCID='" + BcId + "'");

                                if (dthis.Rows.Count == 1 && dthisRef.Rows.Count > 0)
                                {
                                    int valid = cls.insert_data("AddEditMATMHistory '" + dthisRef.Rows[0]["Msrno"].ToString() + "','" + Txntype + "','" + Timestamp + "','" + BcId + "','" + ReferenceId + "','" + TerminalId + "','" + Amount + "',0,0,'" + TxnStatus + "','" + rrn + "','" + bankMessage + "','" + objRoot.Data[i].cardno + "','" + objRoot.Data[i].clientrefid + "','" + result + "',1");
                                    if (TxnStatus.ToUpper() == "SUCCESS")
                                    {
                                        try
                                        {
                                            string MemberID = cls.select_data_scalar_string("select loginid from Member where msrno=" + dthisRef.Rows[0]["Msrno"].ToString() + "");
                                            string narration = "", Decs = "";
                                            narration = "MATM TransID-" + RefId;
                                            Decs = $"MATM Amount - {Amount}. Done With Card No {objRoot.Data[i].cardno}. TransID - {RefId}";
                                            cls.update_data("exec PROC_AEPSWALLETTRANSACTION '" + MemberID + "','" + Amount + "','CR','" + narration + "','" + Decs + "','" + ConstantsData.SMatm + "','" + RefId + "'");
                                            cls.update_data("Exec Proc_MATM_DistributeCommission '" + dthisRef.Rows[0]["msrno"].ToString() + "'," + Amount + ", '" + RefId + "', '" + ConstantsData._TDS + "','" + ConstantsData.SMATMCommission + "','" + ConstantsData._TDSVal + "'");
                                        }
                                        catch (Exception ex)
                                        {

                                        }

                                        ErrorShow.AlertMessage(page, "Transaction Success", ConstantsData.CompanyName);
                                    }
                                    else
                                    {
                                        ErrorShow.AlertMessage(page, "Transaction "+TxnStatus+" due to "+ bankMessage + "", ConstantsData.CompanyName);

                                    }
                                    if (valid > 0)
                                    {
                                        cls.select_data_dt("update MATMRequestID set Status='" + TxnStatus + "' where RefID='" + RefId + "'");
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (Convert.ToString(obj["message"]).ToUpper() == "DATA NOT FOUND")
                            {
                                cls.select_data_dt("update MATMRequestID set Status='Transaction Not Found' where RefID='" + RefId + "'");
                                string response = "{\"statuscode\":\"ERR\",\"status\":\"Transaction Not Found\",\"RefId\":\"" + RefId + "\"}";
                            }
                            else
                            {

                            }
                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Transaction Already Clear", ConstantsData.CompanyName);
                }

            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Check Status", ConstantsData.CompanyName);
            }
        }
        fillTransactionDetails();
    }


}
