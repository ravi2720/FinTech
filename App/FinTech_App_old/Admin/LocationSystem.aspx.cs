using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_LocationSystem : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
            BindData();
        }
    }

    private void BindData()
    {
        DataTable dt = ObjConnection.select_data_dt("PROC_LocationUser '" + txtfromdate.Text + "','" + txttodate.Text + "'");
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
        HiddenField hdnData = (HiddenField)e.Item.FindControl("hdnData");
        DataTable dt = ObjConnection.select_data_dt("SELECT * FROM OPENJSON('"+ hdnData.Value+"')");
        if (dt.Rows.Count > 0)
        {
            dltList.DataSource = dt;
            dltList.DataBind();
        }
    }
}