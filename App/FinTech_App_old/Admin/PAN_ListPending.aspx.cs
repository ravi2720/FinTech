using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Admin_PAN_ListPending : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    cls_myMember clsm = new cls_myMember();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid(0);
        }
    }
    
   
    private void BindGrid(int id)
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Exec [ProcPAN_ManageHistory] 'GetAllPending', 1 ,'fromdate','todate','" + txtsearch.Text + "'");
        if (dt.Rows.Count > 0)
        {
            dgmember.DataSource = dt;
            dgmember.DataBind();

            ViewState["dtExport"] = dt;
        }
        else
        {
            dgmember.DataSource = null;
            dgmember.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid(0);
    }

    protected void dgmember_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        // Empty for now – implement later if needed
    }

    protected void btnForceFailed_Click(object sender, EventArgs e)
    {
        // Empty for now – implement later if needed
    }

}