using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class matmpnk : System.Web.UI.Page
{
    cls_connection objConnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string result = "";
            using (var reader = new StreamReader(Request.InputStream))
            {
                result = reader.ReadToEnd();
            }
            
            if (result != "")
            {
                objConnection.update_data("insert into LogData(Data)values('"+ result + "')");
                JObject jData = JObject.Parse(result);
                string RefId = jData["id"].ToString();
                DataTable dthis = objConnection.select_data_dt("select * from MATMRequestID where Refid='" + jData["id"].ToString() + "' and status='Pending' ");
                if (dthis.Rows.Count > 0)
                {
                    if (Convert.ToString(jData["apiStatus"]).ToUpper() == "AUTH_SUCCESS")
                    {
                        string Txntype = jData["txnType"].ToString();
                        string BcId = dthis.Rows[0]["BCID"].ToString();
                        string TerminalId = jData["apiTxnId"].ToString();
                        string MerchantId = jData["walletReference"].ToString();
                        string Amount = jData["amount"].ToString();
                        string TxnStatus = (jData["apiStatus"].ToString().ToUpper()== "AUTH_SUCCESS"?"Success":"Failed");
                        string ReferenceId = RefId;
                        string Pipe = "1";
                        string EndCustMobile = "";
                        string rrn = jData["rrn"].ToString();
                        string Timestamp = jData["created_at"].ToString();
                        string clientRefID = RefId;
                        string bankMessage = jData["apiDescription"].ToString();
                        String strPathAndQuery = result;

                        string Message = "Success";
                        string bankCode = "";
                        string InvoiceNo = "";
                        string msrno = "0";
                        DataTable dtMemberATM = new DataTable();
                        DataTable dthisRef = new DataTable();
                        dthisRef = objConnection.select_data_dt("select * from MATMRequestID where RefID='" + RefId + "' and Status='Pending' and BCID='" + BcId + "'");
                        dtMemberATM = objConnection.select_data_dt("select * from MATMHistory where RefID='" + RefId + "'");
                        if (dtMemberATM.Rows.Count == 0 && dthisRef.Rows.Count > 0)
                        {
                            int valid = objConnection.insert_data("AddEditMATMHistory '" + dthisRef.Rows[0]["Msrno"].ToString() + "','" + Txntype + "','" + Timestamp + "','" + BcId + "','" + jData["apiTxnId"].ToString() + "','" + TerminalId + "','" + Amount + "',0,0,'" + TxnStatus + "','" + rrn + "','" + bankMessage + "','" + jData["customerIdentification"].ToString() + "','" + ReferenceId + "','" + result + "',1");
                            if (TxnStatus.ToUpper() == "SUCCESS")
                            {
                                try
                                {
                                    if (Txntype.ToUpper() == "CW")
                                    {
                                        string MemberID = objConnection.select_data_scalar_string("select loginid from Member where msrno=" + dthisRef.Rows[0]["Msrno"].ToString() + "");
                                        string narration = "", Decs = "";
                                        narration = "MATM TransID-" + RefId;
                                        Decs = $"MATM Amount - {Amount}. Done With Card No {jData["customerIdentification"].ToString()}. TransID - {RefId}";
                                        objConnection.update_data("exec PROC_AEPSWALLETTRANSACTION '" + MemberID + "','" + Amount + "','CR','" + narration + "','" + Decs + "','" + ConstantsData.SMatm + "','" + RefId + "'");
                                        objConnection.update_data("Exec Proc_MATM_DistributeCommission '" + dthisRef.Rows[0]["msrno"].ToString() + "'," + Amount + ", '" + RefId + "', '" + ConstantsData._TDS + "','" + ConstantsData.SMATMCommission + "','" + ConstantsData._TDSVal + "'");
                                    }
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            if (valid > 0)
                            {
                                objConnection.select_data_dt("update MATMRequestID set Status='" + TxnStatus + "' where RefID='" + RefId + "'");
                            }

                        }
                        else
                        {
                            DataTable dtData = objConnection.select_data_dt("select * from tbl_MicroATMTransaction where ClientRefId='" + RefId + "'");
                            if (dtData.Rows.Count > 0)
                            {
                                int k = objConnection.update_data("update tbl_MicroATMTransaction set MsrNo=" + msrno + ", TransactionStatus='" + TxnStatus + "',BankCode='" + bankCode + "',BankRRNNumber='" + rrn + "',InvoiceNo='" + InvoiceNo + "',Message='" + Message + "',ResponseData='" + result + "' where ClientRefId='" + RefId + "'");
                                if (k > 0)
                                {
                                    objConnection.select_data_dt("update tblMicroATM_RefID_Maha set Status='" + TxnStatus + "' where RefID='" + RefId + "'");
                                }

                            }

                        }


                    }
                   
                }
            }
            else
            {

            }
        }

    }
}