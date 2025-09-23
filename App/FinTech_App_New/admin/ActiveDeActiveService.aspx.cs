using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ActiveDeActiveService : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");

        }
    }

    private void BindData()
    {
        DataTable dt = ObjConnection.select_data_dt("PROC_Active_service_For_Admin '" + txtfromdate.Text + "','" + txttodate.Text + "'");
        if (dt.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();
        }
        else
        {
            gvTransactionHistory.DataSource = null;
            gvTransactionHistory.DataBind();
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvTransactionHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DataList dltList = (DataList)e.Item.FindControl("dltList");
        HiddenField hdnMsrno = (HiddenField)e.Item.FindControl("hdnMsrno");
        DataTable dt = ObjConnection.select_data_dt("PROC_Active_service " + hdnMsrno.Value + "");
        if (dt.Rows.Count > 0)
        {
            dltList.DataSource = dt;
            dltList.DataBind();
        }
    }
}