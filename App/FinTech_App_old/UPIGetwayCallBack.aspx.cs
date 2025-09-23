using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UPIGetwayCallBack : System.Web.UI.Page
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
            cls.update_data("insert into LogData(Data)values('" + values + "')");

            if (values != null) 
            {
                if (Request["status"].ToString() == "success")
                {
                    Int32 msrno = cls.select_data_scalar_int("select msrno from member where mobile='" + Request["customer_mobile"].ToString() + "'");
                    if (msrno > 0)
                    {
                        decimal surcharge = 0.90M;// MoneyTransfer_EkoAPI.GetUPISurcharge(Convert.ToDecimal(ObjUpi.amount));
                        Int32 Val = cls.update_data("PROC_FundAdd_UPI_Virtaul_Transaction " + msrno + ",'" + Request["id"].ToString() + "','" + Request["client_txn_id"].ToString() + "','" + Request["customer_vpa"].ToString() + "','','" + Request["customer_name"].ToString() + "','" + Request["customer_mobile"].ToString() + "','" + Request["amount"].ToString() + "','0','" + Request["amount"].ToString() + "','" + Request["txnAt"].ToString() + "','UPI','" + Request["upi_txn_id"].ToString() + "','" + values + "','" + surcharge + "'");
                        if (Val > 0)
                        {
                            Int32 TransCount = 0;
                            TransCount = cls.select_data_scalar_int("select count(1) from TBL_EWALLETTRANSACTION where transactionid='" + Request["upi_txn_id"].ToString() + "'");
                            if (TransCount == 0)
                            {
                                string MemberID = cls.select_data_scalar_string("select loginid from member where msrno=" + msrno + "");

                                string narration = "Amount : " + Request["amount"].ToString() + " Added from UPI orderID : " + Request["upi_txn_id"].ToString() + " With Surcharge - " + surcharge;
                                string Dec = "Amount : " + Request["amount"].ToString() + " Added from UPIID-" + Request["customer_vpa"].ToString();
                                cls.update_data("exec PROC_EWALLETTRANSACTION '" + MemberID + "','" + (Convert.ToDecimal(Request["amount"].ToString()) - Convert.ToDecimal(surcharge)) + "','Cr','" + narration + "','" + Dec + "'," + ConstantsData.SUPI + ",'" + Request["upi_txn_id"].ToString() + "'");
                            }
                        }
                    }
                }

            }
        }
    }
}
