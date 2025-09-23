using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AssignAgentID : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

    }
    private void fillTransactionDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Select MsrNo,loginid as MemberId,Name as 'Name',Email,BCRegistrationId,Mobile from member where Mobile like '%" + txtSearch.Text + "%' or Email like '%" + txtSearch.Text + "%' or loginid like '%" + txtSearch.Text + "%' ");

        DataTable dtNew = new DataTable();
        dtNew = dt.DefaultView.ToTable();
        if (dtNew.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dtNew;
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

    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Update")
        {
            TextBox txtAgentID = (TextBox)e.Item.FindControl("txtAgentID");
            HiddenField hdnMSrno = (HiddenField)e.Item.FindControl("hdnMSrno");

            if (!string.IsNullOrEmpty(txtAgentID.Text))
            {
                Int32 Val = cls.update_data("update member set BCRegistrationId='" + txtAgentID.Text + "' where msrno=" + hdnMSrno.Value);
                if (Val > 0)
                {
                    ErrorShow.AlertMessage(page, "Update Successfully", "Success");
                }
            }
            else
            {
                txtAgentID.BorderColor = System.Drawing.Color.Red;
            }
        }
    }

}