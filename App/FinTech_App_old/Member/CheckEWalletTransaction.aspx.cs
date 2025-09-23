using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_ChangePassword : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] == null)
        {
            Response.Redirect("default.aspx");
        }
        if (!IsPostBack)
        {
           
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtTransactionID.Text))
        {
            DataTable dtMember = (DataTable)Session["dtMember"];
            dt = cls.select_data_dt("select * from VIEW_EWALLETTRANSACTION where TRANSACTIONID = '" + txtTransactionID.Text.Trim() + "' and msrno=" + Convert.ToInt32(dtMember.Rows[0]["Msrno"]) + "");
            if (dt.Rows.Count > 0)
            {
                rptData.DataSource = dt;
                rptData.DataBind();
            }
            else
            {
                rptData.DataSource = null;
                rptData.DataBind();
            }
        }
    }
}