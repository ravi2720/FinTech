using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_TotalRecharge : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    cls_connection ObjConnection = new cls_connection();
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
        DataTable dt = new DataTable();
        dt = ObjConnection.select_data_dt("Exec RechargeAllOperatorHistory'Admin',0");
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