using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_BalanceTransfer : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtMember;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                GetBindData();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }


    private void GetBindData()
    {
        DataTable dt = cls.select_data_dt("select * from View_MemberBalance where MSRNO in (select * from dbo.GetDownList(1)) ");
        if (dt.Rows.Count > 0)
        {
            rptDataRecharge.DataSource = dt;
            rptDataRecharge.DataBind();
        }
        else
        {
            rptDataRecharge.DataSource = null;
            rptDataRecharge.DataBind();
        }
    }

    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetBindData();


    }
}