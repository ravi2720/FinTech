using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_OffLineTransactionList : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    Package package = new Package();
    DataTable dtMember = new DataTable();
    int Val = 0;
    Page page;

    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        dtMember = (DataTable)Session["dtEmployee"];
        if (!IsPostBack)
        {
            GetData();
        }
    }

    public void GetData()
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManageOffLineTransaction 'GetAllDate',0,'" + txtfromdate.Text+"','"+txttodate.Text+"'");
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
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message,ConstantsData.CompanyName);
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetData();
    }
}