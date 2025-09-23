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

public partial class DMTRecipt : System.Web.UI.Page
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
        try
        {
            DataTable dtWalletTransaction = cls.select_data_dt("Exec GetDataMoneyTransfer '" + Request.QueryString["GUID"].ToString() + "'");
            if (dtWalletTransaction.Rows.Count > 0)
            {
                rptTransactionDetails.DataSource = dtWalletTransaction;
                rptTransactionDetails.DataBind();

                lblStatus.Text = (dtWalletTransaction.Rows[0]["sTATUS"].ToString().ToUpper() == "SUCCESS" ? "Success" : dtWalletTransaction.Rows[0]["sTATUS"].ToString());
                lblStatus.ForeColor = (dtWalletTransaction.Rows[0]["sTATUS"].ToString().ToUpper() == "SUCCESS" ? System.Drawing.Color.Green : System.Drawing.Color.Red);
                
            }
        }
        catch (Exception ex)
        {

        }
    }

   
   
}