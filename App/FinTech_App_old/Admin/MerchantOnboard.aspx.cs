using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class API_MerchantOnboard : System.Web.UI.Page
{
    cls_connection objaccess = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        string sRequest = string.Empty;

        using (var reader = new StreamReader(Request.InputStream))
        {
            sRequest = reader.ReadToEnd();
        }
       

        if (Request.QueryString["data"] != null)
        {
        }


        try
        {
            JObject jData = JObject.Parse(sRequest);
            if (jData["event"].ToString().ToUpper() == "MERCHANT_STATUS_ONBOARD")
            {
                if (jData["param"]["status"].ToString() == "1")
                {
                    objaccess.update_data("update Member set ActivedForRnfi=1 where mobile='" + jData["param"]["mobile"].ToString() + "'");
                }
            }
            else if (jData["event"].ToString().ToUpper() == "MERCHANT_ONBOARDING")
            {
                string Result = jData["param_enc"].ToString();// JsonWebToken.Decode(jData["param_enc"].ToString(), PaysPrint.JWTencryptionKey);

                JObject JOnB = JObject.Parse(Result);
                DataTable dtMember = objaccess.select_data_dt("select * from Member where loginid='" + JOnB["merchant_id"].ToString().Replace(SONITechnoCredentrial.Prefix, "") + "'");
                if (dtMember.Rows.Count > 0)
                {
                    Decimal Balance = objaccess.select_data_scalar_decimal("select balance from TBL_EWALLETBALANCE where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "'");
                    if (Balance >= 10)
                    {
                        string narration = "AEPS OnBoard Charge-" + JOnB["merchant_id"].ToString();
                        Int32 CountA = objaccess.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration='" + narration + "'");
                        if (CountA == 0)
                        {
                            DataTable dtval = objaccess.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"].ToString() + "','10','DR','" + narration + "','',"+ConstantsData.SAEPSOnBoard+",'" + JOnB["merchant_id"].ToString() + "'");
                            if (dtval.Rows[0]["msrno"].ToString() == "1")
                            {
                                Response.ContentType = "application/json";
                                Response.Write("{\"status\":200,\"message\":\"Transaction completed successfully\"}");
                            }
                        }
                        else
                        {
                            Response.Write("{\"status\":200,\"message\":\"Transaction completed successfully\"}");
                        }
                    }
                    else
                    {

                    }
                }
            }
        }
        catch (Exception ex)
        {

        }

    }
}