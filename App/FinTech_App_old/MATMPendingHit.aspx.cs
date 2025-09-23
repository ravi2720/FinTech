using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MATMPendingHit : System.Web.UI.Page
{
    cls_connection objconnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string result = "";
            using (var reader = new StreamReader(Request.InputStream))
            {
                result = reader.ReadToEnd();
            }
            if (!string.IsNullOrEmpty(result))
            {
                JObject Data1 = JObject.Parse(result);
                string TransID = Data1["txnrefrenceNo"].ToString();
                DataTable dthistory = objconnection.select_data_dt("select * from MATMRequestID where RefID='" + TransID + "' and Status='Pending'");
                if (dthistory.Rows.Count > 0)
                {
                    string Result = result;
                    JObject Data = JObject.Parse(Result);
                    Int32 Val = 0;
                    if (Convert.ToBoolean(Data["status"].ToString()))
                    {
                        if (Data["txnstatus"].ToString() == "1")
                        {
                            Val = objconnection.update_data("AddEditMATMHistory '" + dthistory.Rows[0]["Msrno"].ToString() + "','" + Data["transactiontype"].ToString() + "','','" + dthistory.Rows[0]["Msrno"].ToString() + "','" + dthistory.Rows[0]["RefID"].ToString() + "','" + Data["ackno"].ToString() + "','" + Data["amount"].ToString() + "',0,0,'Success','" + Data["bankrrn"].ToString() + "','" + Data["message"].ToString() + "','" + Data["cardnumber"].ToString() + "','" + Data["txnrefrenceNo"].ToString() + "','" + Result + "',1");
                            if (Val > 0)
                            {
                                try
                                {
                                    string MemberID = objconnection.select_data_scalar_string("select loginid from Member where msrno=" + dthistory.Rows[0]["Msrno"].ToString() + "");
                                    string narration = "", Decs = "";
                                    narration = "MATM TransID-" + Data["txnrefrenceNo"].ToString();
                                    Decs = $"MATM Amount - {Data["amount"].ToString()}. Done With Card No {Data["cardnumber"].ToString()}. TransID - {Data["txnrefrenceNo"].ToString()}";
                                    objconnection.update_data("exec PROC_AEPSWALLETTRANSACTION '" + MemberID + "','" + Data["amount"].ToString() + "','CR','" + narration + "','" + Decs + "','" + ConstantsData.SMatm + "','" + dthistory.Rows[0]["RefID"].ToString() + "'");
                                    objconnection.update_data("Exec Proc_MATM_DistributeCommission '" + dthistory.Rows[0]["msrno"].ToString() + "'," + Data["amount"].ToString() + ", '" + dthistory.Rows[0]["RefID"].ToString() + "', '" + ConstantsData._TDS + "','" + ConstantsData.SMATMCommission + "','" + ConstantsData._TDSVal + "'");
                                    objconnection.select_data_dt("update MATMRequestID set Status='Success' where RefID='" + Data["txnrefrenceNo"].ToString() + "'");
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                        else if (Data["txnstatus"].ToString() == "3")
                        {
                            Val = objconnection.update_data("AddEditMATMHistory '" + dthistory.Rows[0]["Msrno"].ToString() + "','" + Data["transactiontype"].ToString() + "','','" + dthistory.Rows[0]["Msrno"].ToString() + "','" + dthistory.Rows[0]["RefID"].ToString() + "','" + Data["ackno"].ToString() + "','" + Data["amount"].ToString() + "',0,0,'failed','','" + Data["message"].ToString() + "','" + Data["cardnumber"].ToString() + "','" + Data["txnrefrenceNo"].ToString() + "','" + Result + "',1");
                            if (Val > 0)
                            {
                                objconnection.select_data_dt("update MATMRequestID set Status='failed' where RefID='" + Data["txnrefrenceNo"].ToString() + "'");
                            }

                        }
                    }
                    else
                    {
                        if (Data["message"].ToString() == "Data not Found.")
                        {
                            objconnection.select_data_dt("update MATMRequestID set Status='Transaction Not Found' where RefID='" + TransID + "'");
                        }
                        else
                        {
                            Val = objconnection.update_data("AddEditMATMHistory '" + dthistory.Rows[0]["Msrno"].ToString() + "','" + Data["transactiontype"].ToString() + "','','" + dthistory.Rows[0]["Msrno"].ToString() + "','" + dthistory.Rows[0]["RefID"].ToString() + "','" + Data["ackno"].ToString() + "','" + Data["amount"].ToString() + "',0,0,'failed','','" + Data["message"].ToString() + "','" + Data["cardnumber"].ToString() + "','" + Data["txnrefrenceNo"].ToString() + "','" + Result + "',1");
                        }

                    }
                }
            }
        }
    }
}