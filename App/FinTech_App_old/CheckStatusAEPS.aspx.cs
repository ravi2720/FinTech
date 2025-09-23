using System;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckStatusAEPS : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    clsMahagram ObjMaha = new clsMahagram();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["Txntype"] != null && Request.QueryString["Timestamp"] != null && Request.QueryString["BcId"] != null
            && Request.QueryString["TerminalId"] != null && Request.QueryString["TransactionId"] != null && Request.QueryString["Amount"] != null && Request.QueryString["TxnStatus"] != null
            && Request.QueryString["BankIIN"] != null && Request.QueryString["TxnMedium"] != null && Request.QueryString["EndCustMobile"] != null)
            {
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                string uniqueGUID = System.Guid.NewGuid().ToString();
                int msrno = cls.select_data_scalar_int("select ID from Member where BCRegistrationID='" + Request.QueryString["BcId"].ToString() + "'");
                string uniqueTransID = uniqueGUID.Replace('-', '0');
                string narration = "AEPS Transaction TxnID: " + uniqueTransID;

                var Values = new NameValueCollection();
                Values["MESSAGE"] = "SUCCESS";// "BC658306479";// "BC674784069";       
                Values["STATUS"] = "SUCCESS";
                Values["TRANSACTION_ID"] = Request.QueryString["TransactionId"].ToString();// uniqueTransID;
                string number = String.Format("{0:d9}", (DateTime.Now.Ticks / 10) % 1000000000);
                Values["VENDOR_ID"] = number;
                Values["OrderID"] = uniqueGUID;


                if (Request.QueryString["Txntype"].ToString().ToUpper() == "CW")
                {
                    Int32 Val = cls.update_data("Exec Proc_Add_edit_AEPS " + msrno + ", '" + uniqueGUID + "','" + Convert.ToDouble(Request.QueryString["Amount"].ToString()) + "', 'CW', 0,'" + Request.QueryString["TransactionId"].ToString().Split('|')[1] + "', '" + Request.QueryString["EndCustMobile"].ToString() + "', '" + Request.QueryString["TxnStatus"].ToString() + "','','','','','Site','" + strPathAndQuery + "','" + Request.QueryString["TransactionId"].ToString() + "'");
                }
                else if (Request.QueryString["Txntype"].ToString().ToUpper() == "BE")
                {
                    Int32 Val = cls.update_data("Exec Proc_Add_edit_AEPS " + msrno + ", '" + uniqueGUID + "','0', 'BE', 0,'" + Request.QueryString["TransactionId"].ToString().Split('|')[1] + "', '" + Request.QueryString["EndCustMobile"].ToString() + "', '" + Request.QueryString["TxnStatus"].ToString() + "','','','','','Site','" + strPathAndQuery + "','" + Request.QueryString["TransactionId"].ToString() + "'");
                }
                else if (Request.QueryString["Txntype"].ToString().ToUpper() == "MS")
                {
                    Int32 Val = cls.update_data("Exec Proc_Add_edit_AEPS " + msrno + ", '" + uniqueGUID + "','0', 'MS', 0,'" + Request.QueryString["TransactionId"].ToString().Split('|')[1] + "', '" + Request.QueryString["EndCustMobile"].ToString() + "', '" + Request.QueryString["TxnStatus"].ToString() + "','','','','','Site','" + strPathAndQuery + "','" + Request.QueryString["TransactionId"].ToString() + "'");
                }
                else if (Request.QueryString["Txntype"].ToString().ToUpper() == "AP")
                {
                    Int32 Val = cls.update_data("Exec Proc_Add_edit_Aadhar " + msrno + ", '" + uniqueGUID + "','" + Convert.ToDouble(Request.QueryString["Amount"].ToString()) + "', 'CW', 0,'" + Request.QueryString["TransactionId"].ToString().Split('|')[1] + "', '" + Request.QueryString["EndCustMobile"].ToString() + "', '" + Request.QueryString["TxnStatus"].ToString() + "','','','','','Site','" + strPathAndQuery + "','" + Request.QueryString["TransactionId"].ToString() + "'");
                }
                // DataTable dtHistory = cls.select_data_dt("Exec [ProcAEPS_Mahagram_AddEditHistory] 0, " + Convert.ToInt32(msrno) + ", '" + Request.QueryString["EndCustMobile"].ToString() + "','', '', " + Convert.ToDouble(Request.QueryString["Amount"].ToString()) + ", 0, " + Convert.ToDouble(Request.QueryString["Amount"].ToString()) + ", '" + Request.QueryString["TxnStatus"].ToString() + "', '" + narration + "','" + Request.QueryString["TxnMedium"].ToString() + "','" + Request.QueryString["BankIIN"].ToString() + "','" + Request.QueryString["Txntype"].ToString() + "','" + Request.QueryString["Timestamp"].ToString() + "', '" + uniqueTransID + "', '" + Request.QueryString["TransactionId"].ToString() + "','','" + Request.QueryString["TerminalId"].ToString() + "','" + Request.QueryString["TransactionId"].ToString() + "','','', '', 0,0,0,0, '" + uniqueGUID + "','" + strPathAndQuery + "','','','" + number + "'");

                Response.Write(ObjMaha.copyFrom(Values));
            }
            else
            {
                String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                //cls.insert_data("insert into tbl_Apeslog(url)values('" + strPathAndQuery + "')");
            }
        }
        catch (Exception ex)
        {
            String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
            //cls.insert_data("insert into tbl_Apeslog(url)values('" + ex.Message + "')");
        }
    }
    public static string GetUniqueSessionID()
    {
        CultureInfo ci = new CultureInfo("en-US");
        DateTime dt = DateTime.Parse(DateTime.Now.ToString());
        Random rnd = new Random();
        string Session = dt.ToString("ddMMyyyy") + dt.ToString("HHmmss") + rnd.Next(00000, 99999);
        return Session;
    }
}