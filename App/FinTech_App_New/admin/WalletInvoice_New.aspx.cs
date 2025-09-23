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


public partial class Admin_WalletInvoice_New : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["GUID"] != null)
            {
                cls_connection cls = new cls_connection();
                DataTable dtWalletTransaction = cls.select_data_dt("Exec [ProcDMTPaytm_ManageDMT] 'GetReciptByGUID',0,'" + Request.QueryString["GUID"].ToString() + "'");
                if (dtWalletTransaction.Rows.Count > 0)
                {
                    rptTransactionDetails.DataSource = dtWalletTransaction;
                    rptTransactionDetails.DataBind();
                    DataTable dtStatus = cls.select_data_dt("Exec [ProcDMTPaytm_ManageDMT] 'GetReciptStatusByGUID',0,'" + Request.QueryString["GUID"].ToString() + "'");
                    //if (dtStatus.Rows[0]["TotalSuccess"].ToString()) > 0)
                    //{
                    lblStatus.Text = dtStatus.Rows[0]["status"].ToString();
                    lblStatus.ForeColor = System.Drawing.Color.Green;
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
