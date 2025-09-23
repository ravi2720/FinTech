using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Member_AEPSEWalletSummary : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dtMember = new DataTable();
    int Val = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            if (!IsPostBack)
            {
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                FillData();
            }
        }
        else
        {
            Response.Redirect("default.aspx");
        }

    }
    private void FillData()
    {

        dtMember = (DataTable)Session["dtMember"];
        DataTable dt = new DataTable();
        dt = ObjConnection.select_data_dt("Exec PROC_WALLET_REPORT 'aepsEwalletTransaction'," + Convert.ToInt32(dtMember.Rows[0]["msrno"].ToString()) + ",'" + txtfromdate.Text + "','" + txttodate.Text + "'");
        rptData.DataSource = dt;
        rptData.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }
}