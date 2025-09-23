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

public partial class Admin_BillPayReceipt : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["TransID"] != null)
            {
                cls_connection cls = new cls_connection();
                DataTable dtWalletTransaction = cls.select_data_dt("Exec PROC_GET_BILLPAY_DETAILS '" + Request.QueryString["TransID"].ToString() + "'");
                if (dtWalletTransaction.Rows.Count > 0)
                {
                    rptTransactionDetails.DataSource = dtWalletTransaction;
                    rptTransactionDetails.DataBind();
                    //if (dtStatus.Rows[0]["TotalSuccess"].ToString()) > 0)
                    //{
                    lblStatus.Text = dtWalletTransaction.Rows[0]["status"].ToString();
                    lblStatus.ForeColor = (lblStatus.Text.ToUpper()=="SUCCESS"?System.Drawing.Color.Green:System.Drawing.Color.Red);
                    //}
                    //else
                    //{
                    //    lblStatus.Text = "FAILURE!";
                    //    lblStatus.ForeColor = System.Drawing.Color.Red;
                    //    return;
                    //}
                }
            }
        }
    }

    protected void btnexportPdf_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Panel1;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=500px,width=650px,scrollbars=1');</script>");
    }
}