using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_EmiSummery : System.Web.UI.Page
{
    DataAccess obj = new DataAccess();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getsummery();
        }
    }
    private void getsummery()
    {
        dt = obj.GetDataTable("EXEC SP_EMI_SUMMERY 'SELECT','" + txtfromdate.Text + "','" + txttodate.Text + "'");
        if (dt.Rows.Count > 0)
        {
            rptdata.DataSource = dt;
            rptdata.DataBind();
            lblrecords.Text = dt.Rows.Count.ToString();
        }
        else
        {
            rptdata.DataSource = null;
            rptdata.DataBind();
        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        getsummery();
    }
}