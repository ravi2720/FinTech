using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_UPISendReceipt : System.Web.UI.Page
{
    Page page;
    cls_connection cls = new cls_connection();
    DataTable dtMember;
    protected void Page_Load(object sender, EventArgs e)
    {
        

        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                if (Request.QueryString["TransID"] != null)
                {
                    GetData();
                }
            }
        }
        else
        {
            Response.Redirect("default.aspx");
        }

    }
    private void GetData()
    {
        DataTable dtWalletTransaction = cls.select_data_dt("Exec GetDataUPITransfer '" + Request.QueryString["TransID"].ToString() + "'");
        if (dtWalletTransaction.Rows.Count > 0)
        {
            rptTransactionDetails.DataSource = dtWalletTransaction;
            rptTransactionDetails.DataBind();

            lblStatus.Text = (dtWalletTransaction.Rows[0]["sTATUS"].ToString().ToUpper() == "SUCCESS" ? "Success" : dtWalletTransaction.Rows[0]["sTATUS"].ToString());
            lblStatus.ForeColor = (dtWalletTransaction.Rows[0]["sTATUS"].ToString().ToUpper() == "SUCCESS" ? System.Drawing.Color.Green : System.Drawing.Color.Red);

        }
    }

    protected void btnexportPdf_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Panel1;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=500px,width=650px,scrollbars=1');</script>");
    }

}