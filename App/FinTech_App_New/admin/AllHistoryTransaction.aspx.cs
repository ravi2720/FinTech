using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AllHistoryTransaction : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TotalHistoryShowByMember();
        }
    }
    protected void TotalHistoryShowByMember()
    {
        DataTable dtAEPSReq = cls.select_data_dt("EXEC PROC_ADMIN_TRANSACTION_HISTORY_All '" + txtFromDateA.Text + "','" + txtToDateA.Text + "'");
        if (dtAEPSReq.Rows.Count > 0)
        {
            rptTotalHistoryShowByMember.DataSource = dtAEPSReq;
            rptTotalHistoryShowByMember.DataBind();
        }
        else
        {
            rptTotalHistoryShowByMember.DataSource = null;
            rptTotalHistoryShowByMember.DataBind();
        }

        lblMessage.Text = txtFromDateA.Text + " Report Data";
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        TotalHistoryShowByMember();
    }
}