using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CustomerList : System.Web.UI.Page
{
    DataTable dtMember;
    cls_connection cls = new cls_connection();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
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
        dt = cls.select_data_dt("ManageCustomerDMT ''," + dtMember.Rows[0]["ID"].ToString() + ",'" + txtfromdate.Text + "','" + txttodate.Text + "','0','" + ddlMember.SelectedValue + "'");

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



    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "SetLimit")
        {
            TextBox txtLimit = (TextBox)e.Item.FindControl("txtLimit");
            if (txtLimit.Text.ToInt32() > 0)
            {
                Int32 Val = cls.update_data("update CustomerDMT set Limit='" + txtLimit.Text + "' where id=" + e.CommandArgument.ToString() + "");
                if (Val > 0)
                {
                    ErrorShow.AlertMessage(page, "Limit Allow Successfully", ConstantsData.CompanyName);
                    fillTransactionDetails();
                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }



    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}