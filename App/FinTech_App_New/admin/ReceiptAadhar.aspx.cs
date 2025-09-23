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
public partial class Admin_ReceiptAadhar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {

                cls_connection cls = new cls_connection();
                DataTable dtWalletTransaction = cls.select_data_dt("Exec PROC_AadharPayTransaction '" + Request.QueryString["ID"].ToString() + "'");
                if (dtWalletTransaction.Rows.Count > 0)
                {

                    {
                        repSummarry.DataSource = dtWalletTransaction;
                        repSummarry.DataBind();
                        if (dtWalletTransaction.Rows[0]["status"].ToString().ToUpper() == "SUCCESS")
                        {
                            imgSet.Src = "../img/RightMark.png";

                            Message.InnerHtml = "Cash Withdraw Success";


                        }
                        else
                        {
                            imgSet.Src = "../img/Cross.png";

                            Message.InnerHtml = "Cash Withdraw failed";


                        }
                        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>$('#ErrorMessage').modal('show');</script>");

                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "$('#kycComplete').modal('hide');", true);
            }
        }
    }

    protected void btnexportPdf_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Panel1;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=500px,width=650px,scrollbars=1');</script>");
    }


}