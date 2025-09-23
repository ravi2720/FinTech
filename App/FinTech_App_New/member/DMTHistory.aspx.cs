using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;

public partial class Member_DMTHistory : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtMember = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                fillTransactionDetails();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void fillTransactionDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("ManageMoneyTransfer ''," + dtMember.Rows[0]["ID"].ToString() + ",'" + txtfromdate.Text + "','" + txttodate.Text + "','0','" + ddlMember.SelectedValue + "'");

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
        fillTransactionDetails();
    }
}