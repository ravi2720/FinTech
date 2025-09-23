using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Member_ActivityLogs : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtMember;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                BinDData();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    public void BinDData()
    {
        DataTable dtH = cls.select_data_dt("ManageMemberLoginHistory "+ dtMember.Rows[0]["Msrno"].ToString()+ "");
        if (dtH.Rows.Count > 0)
        {
            rptData.DataSource = dtH;
            rptData.DataBind();
        }
        else
        {
            rptData.DataSource = null;
            rptData.DataBind();
        }
    }
}