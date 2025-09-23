using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class AEPSCoreCallBack : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string values = "";
            using (var reader = new StreamReader(Request.InputStream))
            {
                values = reader.ReadToEnd();
            }

            if (!string.IsNullOrEmpty(values))
            {
                Int32 Val = 0;
                JObject Data = JObject.Parse(values);

                if (Data["code"].ToString().ToUpper() == "TXN")
                {
                    DataTable dthis = cls.select_data_dt("select * from aeps_history where referenceno='" + Data["data"]["OrderID"].ToString().ToUpper() + "' and Status='Pending'");
                    if (dthis.Rows.Count > 0)
                    {
                        if (Data["data"]["status"].ToString().ToUpper() == "FALSE")
                        {
                            Val = cls.update_data("proc_Credit_AEPS_History '" + dthis.Rows[0]["referenceno"].ToString() + "','" + dthis.Rows[0]["msrno"].ToString() + "','CW','Failed','" + Data["data"]["message"].ToString() + "',0,'','','" + values + "'");
                        }
                        else
                        {
                            if (Data["data"]["status"].ToString().ToUpper() == "TRUE" && Data["data"]["txnstatus"].ToString().ToUpper() == "3")
                            {
                                Val = cls.update_data("proc_Credit_AEPS_History '" + dthis.Rows[0]["referenceno"].ToString() + "','" + dthis.Rows[0]["msrno"].ToString() + "','CW','Failed','" + Data["data"]["message"].ToString() + "',0,'','','" + values + "'");
                            }
                            else if (Data["data"]["status"].ToString().ToUpper() == "TRUE" && Data["data"]["txnstatus"].ToString().ToUpper() == "1")
                            {
                                Val = cls.update_data("proc_Credit_AEPS_History '" + dthis.Rows[0]["referenceno"].ToString() + "','" + dthis.Rows[0]["msrno"].ToString() + "','CW','Success','" + Data["data"]["message"].ToString() + "'," + dthis.Rows[0]["Amount"].ToString() + ",'" + Data["data"]["bankrrn"].ToString() + "','" + Data["data"]["ackno"].ToString() + "','" + values + "'");
                                
                                try
                                {
                                    string LoginID = cls.select_data_scalar_string("select loginid from Member where Msrno=" + dthis.Rows[0]["msrno"].ToString() + "");

                                    string narration = "", Decs = "";
                                    narration = "Aeps TransID-" + dthis.Rows[0]["referenceno"].ToString();
                                    Decs = $"AEPS Amount - {dthis.Rows[0]["Amount"].ToString()}. Done With Aadhar No {dthis.Rows[0]["adhaarnumber"].ToString()}. TransID - {dthis.Rows[0]["referenceno"].ToString()}";
                                    cls.update_data("exec PROC_AEPSWALLETTRANSACTION '" + LoginID + "','" + dthis.Rows[0]["Amount"].ToString() + "','CR','" + narration + "','" + Decs + "','" + ConstantsData.SAeps + "','" + dthis.Rows[0]["referenceno"].ToString() + "'");
                                    cls.update_data("Exec Proc_AEPS_DistributeCommission '" + LoginID + "'," + dthis.Rows[0]["Amount"].ToString() + ", '" + dthis.Rows[0]["referenceno"].ToString() + "', '" + ConstantsData._TDS + "','" + ConstantsData.SAEPSCommission + "','" + ConstantsData._TDSVal + "'");
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}