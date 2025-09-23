using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Member_DeviceInfo : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtMember;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
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
        DataTable dtH = cls.select_data_dt("ManageDeviceData " + dtMember.Rows[0]["Msrno"].ToString() + "");
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

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "active")
        {
            Int32 Val = 0;
            Val = cls.update_data("update DeviceData set isactive=iif(isactive=1,0,1) where id='" + e.CommandArgument.ToString() + "'");
            if (Val > 0)
            {
                ErrorShow.SuccessNotify(page, "Mobile Device Block/UnBlock Successfully");
                BinDData();
            }
        }
    }
}