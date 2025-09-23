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

public partial class PayoutCallBack : System.Web.UI.Page
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

            cls.update_data("insert into tbl_Apeslog(url)values('" + values + "')");

            if (!string.IsNullOrEmpty(values))
            {
                JObject Data = JObject.Parse(values);
                if (Data["code"].ToString().ToUpper() != "ERR")
                {
                    DataTable dthistory = cls.select_data_dt("select * from AEPS_WithdrwalRequest where RequestID='" + Data["data"]["OrderID"].ToString() + "' and RequestStatus='Pending'");
                    if (dthistory.Rows.Count > 0)
                    {
                        if (Data["data"]["Status"].ToString().ToUpper() == "SUCCESS" || Data["data"]["Status"].ToString().ToUpper() == "PENDING")
                        {
                            cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + dthistory.Rows[0]["ID"].ToString() + ",'" + Data["data"]["STID"].ToString() + "','Success','DE_001','','" + Data["data"]["RRN"].ToString() + "','" + values + "'");
                        }
                        else if (Data["data"]["Status"].ToString().ToUpper() == "FAILED")
                        {
                            cls.update_data("update AEPS_WithdrwalRequest set Response='" + values + "' where id=" + dthistory.Rows[0]["ID"].ToString() + "");

                            cls.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE'," + dthistory.Rows[0]["ID"].ToString() + ",'','Refunded','','" + Data["mess"].ToString() + "','','" + values + "'");
                        }
                    }
                }
            }

        }
    }
}