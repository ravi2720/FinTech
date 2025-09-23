using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Data;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public partial class Admin_ReceiptCashDeposit : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["GUID"] != null)
            {
                CheckData();
            }
        }
    }

    public void CheckData()
    {
        DataTable dtWalletTransaction = cls.select_data_dt("PROC_CashDepositTransaction '"+ Request.QueryString["GUID"].ToString() + "',0,'',''");
        if (dtWalletTransaction.Rows.Count > 0)
        {
            rptTransactionDetails.DataSource = dtWalletTransaction;
            rptTransactionDetails.DataBind();

            lblStatus.Text = dtWalletTransaction.Rows[0]["Status"].ToString();
            lblStatus.ForeColor = System.Drawing.Color.Green;
           
        }
    }

    protected void btnexportPdf_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Panel1;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=500px,width=650px,scrollbars=1');</script>");
    }
    
}