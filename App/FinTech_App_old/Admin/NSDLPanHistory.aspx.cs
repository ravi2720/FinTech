using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_NSDLPanHistory : System.Web.UI.Page
{
    DataTable dtMember;
    cls_connection cls = new cls_connection();
    ActionButtonPermission actionButtonPermission;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.Pan, dtMember.Rows[0]["RoleID"].ToString());
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", dtMember.Rows[0]["ID"].ToString());
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void fillTransactionDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("ManageNSDLPan ''," + dtMember.Rows[0]["ID"].ToString() + ",'" + txtfromdate.Text + "','" + txttodate.Text + "','0','" + ddlMember.SelectedValue + "'");

        if (dt.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();

            lblSCount.InnerText = dt.Compute("Count(Amount)", "Status='Success'").ToString();
            lblPCount.InnerText = dt.Compute("Count(Amount)", "Status='Pending'").ToString();
            lblFCount.InnerText = dt.Compute("Count(Amount)", "Status='Failed'").ToString();
            lblSsum.InnerText = dt.Compute("SUM(Amount)", "Status='Success'").ToString();
            lblPSum.InnerText = dt.Compute("SUM(Amount)", "Status='Pending'").ToString();
            lblFSum.InnerText = dt.Compute("SUM(Amount)", "Status='Failed'").ToString();
        }
        else
        {
            gvTransactionHistory.DataSource = null;
            gvTransactionHistory.DataBind();
        }

    }



    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }



    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        //
    }

    protected void gvTransactionHistory_ItemCommand1(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "ck")
        {
            DataTable dthis = cls.select_data_dt("select * from nsdlpan where id='" + e.CommandArgument.ToString() + "' and Status='Pending'");
           
        }
        fillTransactionDetails();
    }
}