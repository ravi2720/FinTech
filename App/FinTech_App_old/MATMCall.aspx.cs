using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MATMCall : System.Web.UI.Page
{
    cls_connection objConnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["RefId"] != null)
        {
            string RefId = Request.QueryString["RefId"].ToString();
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
                        dthisRef = objConnection.select_data_dt("select * from MATMRequestID where RefID='" + RefId + "' and Status='Pending' and BCID='" + BcId + "'");
                        dtMemberATM = objConnection.select_data_dt("select * from MATMHistory where RefID='" + RefId + "'");
                        if (dtMemberATM.Rows.Count == 0 && dthisRef.Rows.Count > 0)
                        {
                            int valid = objConnection.insert_data("AddEditMATMHistory '" + dthisRef.Rows[0]["Msrno"].ToString() + "','" + Txntype + "','" + Timestamp + "','" + BcId + "','" + ReferenceId + "','" + TerminalId + "','" + Amount + "',0,0,'" + TxnStatus + "','" + rrn + "','" + bankMessage + "','" + objRoot.Data[i].cardno + "','" + objRoot.Data[i].clientrefid + "','" + result + "',1");
                            if (TxnStatus.ToUpper() == "SUCCESS")
                            {
                                try
                                {
                                    string MemberID = objConnection.select_data_scalar_string("select loginid from Member where msrno=" + dthisRef.Rows[0]["Msrno"].ToString() + "");
                                    string narration = "", Decs = "";
                                    narration = "MATM TransID-" + RefId;
                                    Decs = $"MATM Amount - {Amount}. Done With Card No {objRoot.Data[i].cardno}. TransID - {RefId}";
                                    objConnection.update_data("exec PROC_AEPSWALLETTRANSACTION '" + MemberID + "','" + Amount + "','CR','" + narration + "','" + Decs + "','" + ConstantsData.SMatm + "','" + RefId + "'");
                                    objConnection.update_data("Exec Proc_MATM_DistributeCommission '" + dthisRef.Rows[0]["msrno"].ToString() + "'," + Amount + ", '" + RefId + "', '" + ConstantsData._TDS + "','" + ConstantsData.SMATMCommission + "','" + ConstantsData._TDSVal + "'");
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
                else
                {
                    if (Convert.ToString(obj["message"]).ToUpper() == "DATA NOT FOUND")
                    {
                        objConnection.select_data_dt("update MATMRequestID set Status='Transaction Not Found' where RefID='" + RefId + "'");
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
    }
}