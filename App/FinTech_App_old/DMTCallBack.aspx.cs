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

public partial class DMTCallBack : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
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
                JObject Data = JObject.Parse(result);

                DataTable dtHistory = cls.select_data_dt("select * from DMTHYPTO where  reference_number='" + Data["data"]["OrderID"].ToString() + "' and Status='Pending'");
                if (dtHistory.Rows.Count > 0)
                {
                    DataTable dtMember = cls.select_data_dt("select * from Member where msrno=" + dtHistory.Rows[0]["Msrno"].ToString() + "");

                    if (Data["code"].ToString().ToUpper() != "ERR")
                    {
                        if (Data["data"]["Status"].ToString().ToUpper() == "SUCCESS" || Data["data"]["Status"].ToString().ToUpper() == "PENDING")
                        {
                            cls.update_data("update DMTHYPTO set status='" + Data["data"]["Status"].ToString() + "',txn_id='" + Data["data"]["RRN"].ToString() + "',Response='" + result + "' where reference_number='" + Data["data"]["OrderID"].ToString() + "'");

                        }
                        else if (Data["data"]["Status"].ToString().ToUpper() == "FAILED")
                        {
                            cls.update_data("update DMTHYPTO set status='FAILED',Response='" + result + "' where reference_number='" + Data["data"]["OrderID"].ToString() + "'");

                            Int32 CountDr = 0, CountCr = 0;
                            CountDr = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION with(nolock) where factor='dr'  and narration like '%" + dtHistory.Rows[0]["reference_number"].ToString() + "%'");
                            if (CountDr == 1)
                            {
                                CountCr = cls.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION with(nolock)  where factor='Cr' and narration like '%" + dtHistory.Rows[0]["reference_number"].ToString() + "%'");
                                if (CountCr == 0)
                                {
                                    cls.select_data_dt("exec [PROC_EWALLETTRANSACTION_CashDeposit]  '" + dtMember.Rows[0]["LoginID"].ToString() + "', " + Convert.ToDecimal(dtHistory.Rows[0]["FinalAmount"]) + ", 'Cr','Refund - " + dtHistory.Rows[0]["Narration"].ToString() + "','" + System.Guid.NewGuid().ToString() + "'");
                                }
                            }

                        }
                    }
                }
            }

        }
    }
}