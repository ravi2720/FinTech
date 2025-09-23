using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Admin_PAN_Report : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                fillTransactionDetails();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }

   

    private void fillTransactionDetails()
    {
        string strFilter = "MsrNo<>0";

        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select MsrNo,loginid MemberID,Name as [Member Name] ,Email,Mobile,'' PanNumber,PsaIdPan,panstatus from Member where PsaIdPan!='' or PsaIdPan!=null");
       

        if (ddlStatus.SelectedIndex > 0)
        {
            strFilter = strFilter + "and panstatus = '" + ddlStatus.SelectedItem.Text.Trim() + "'";
        }

        dt.DefaultView.RowFilter = strFilter;
        DataTable dtNew = new DataTable();
        dtNew = dt.DefaultView.ToTable();
        if (dtNew.Rows.Count > 0)
        {
            gvOperator.DataSource = dtNew;
            gvOperator.DataBind();
            ViewState["dtExport"] = dtNew;
        }
        else
        {
            gvOperator.DataSource = null;
            gvOperator.DataBind();
        }

    }
}