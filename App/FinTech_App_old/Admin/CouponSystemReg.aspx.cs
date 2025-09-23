using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_CouponSystemReg : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dt = new DataTable();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        dtEmployee = (DataTable)Session["dtEmployee"];
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                BindData();
                BindDropDown.FillDropDown(dllRole, "RoleBasePermission", "Name", "ID", "Select Role", dtEmployee.Rows[0]["RoleID"].ToString());
                BindDropDown.FillDropDown(ddlMemberList, "DownMemberList", "Name", "ID", "Select Member", "1");
            }
            else
            {
                Response.Redirect("default.aspx");
            }
        }

    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        // Get the current Page instance safely
        Page page = HttpContext.Current.CurrentHandler as Page;

        if (dllRole.SelectedIndex > 0)
        {
            string memberId = ddlMemberList.SelectedValue;
            string amount = txtAmount.Text.Trim();
            string role = dllRole.SelectedValue;
            string count = txtCount.Text.Trim();
            string employeeId = dtEmployee.Rows[0]["ID"].ToString();

            int result = cls.update_data($"AddEditCouponApplySystem '{memberId}', '{amount}', '{role}', '{count}', '{employeeId}', 1");

            if (result > 0)
            {
                ErrorShow.AlertMessage(page, "Assign Successfully", "Success");
            }
            else
            {
                ErrorShow.AlertMessage(page, "Already Assigned", "Warning");
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Please select a role.", "Warning");
        }

        BindData();
    }

    protected void dllRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    public void BindData()
    {
        DataTable dt = cls.select_data_dt("ManageCouponApplySystem");
        if (dt.Rows.Count > 0)
        {
            rptAllPackage.DataSource = dt;
            rptAllPackage.DataBind();
        }
        else
        {
            rptAllPackage.DataSource = null;
            rptAllPackage.DataBind();
        }
    }
}