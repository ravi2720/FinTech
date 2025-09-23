using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UPICredit : System.Web.UI.Page
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
          
            UPICallBackResponse ObjUpi = JsonConvert.DeserializeObject<UPICallBackResponse>(values);

            
            if (ObjUpi != null)
            {
                Int32 msrno = cls.select_data_scalar_int("select msrno from memberupi where upiid='" + ObjUpi.payee_vpa + "' or ICICIUPI='" + ObjUpi.payee_vpa + "'");
                if (msrno > 0)
                {
                    decimal surcharge = 0;// MoneyTransfer_EkoAPI.GetUPISurcharge(Convert.ToDecimal(ObjUpi.amount));
                    Int32 Val = cls.update_data("PROC_FundAdd_UPI_Virtaul_Transaction " + msrno + ",'" + ObjUpi.txn_id + "','" + ObjUpi.order_id + "','" + ObjUpi.payee_vpa + "','" + ObjUpi.source + "','" + ObjUpi.payer_name + "','" + ObjUpi.payer_mobile + "','" + ObjUpi.amount + "','" + ObjUpi.Surcharge + "','" + ObjUpi.amount + "','" + ObjUpi.txn_time + "','" + ObjUpi.payment_type + "','" + ObjUpi.bank_ref_num + "','" + values + "','" + surcharge + "'");
                    if (Val > 0)
                    {
                        Int32 TransCount = 0;
                        TransCount = cls.select_data_scalar_int("select count(1) from TBL_EWALLETTRANSACTION where transactionid='" + ObjUpi.bank_ref_num + "'");
                        if (TransCount == 0)
                        {
                            string MemberID = cls.select_data_scalar_string("select loginid from member where msrno=" + msrno + "");

                            string narration = "Amount : " + ObjUpi.amount + " Added from UPI orderID : " + ObjUpi.bank_ref_num + " With Surcharge - " + surcharge;
                            string Dec = "Amount : " + ObjUpi.amount + " Added from UPIID-"+ ObjUpi.payee_vpa;
                            cls.update_data("exec PROC_EWALLETTRANSACTION '" + MemberID + "','" + (Convert.ToDecimal(ObjUpi.amount) - Convert.ToDecimal(surcharge)) + "','Cr','" + narration + "','"+ Dec + "',"+ConstantsData.SUPI+",'" + ObjUpi.bank_ref_num + "'");
                        }
                    }
                }

            }

        }
    }
}


public class UPICallBackResponse
{
    public string txn_id { get; set; }
    public string order_id { get; set; }
    public string payee_vpa { get; set; }
    public string source { get; set; }
    public string payer_name { get; set; }
    public string payer_mobile { get; set; }
    public string payer_note { get; set; }
    public string amount { get; set; }
    public string Surcharge { get; set; }
    public string txn_time { get; set; }
    public string created_at { get; set; }
    public string payment_type { get; set; }
    public string bank_ref_num { get; set; }


}