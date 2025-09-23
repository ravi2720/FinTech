using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_LoanLimit : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            BindDropDown.FillDropDown(dllRole, "ActiveRole", "Name", "ID", "Select Role");

            BindData();
            btnSubmit.Text = "Submit";
        }
    }

    private void BindData()
    {
        DataTable dtMember = cls.select_data_dt("select LoanLimit.*,Role.Name as RoleName from LoanLimit inner join Role on Role.ID = LoanLimit.RoleID");
        rptSurcharge.DataSource = dtMember;
        rptSurcharge.DataBind();
    }

    private void FillData(int id)
    {
        hdnID.Value = Convert.ToString(id);
        DataTable dtdata = cls.select_data_dt("select * from LoanLimit where Roleid=" + id + "");
        txtStartval.Text = dtdata.Rows[0]["startval"].ToString();
        txtEndVal.Text = dtdata.Rows[0]["endval"].ToString();
        dllRole.SelectedValue= dtdata.Rows[0]["RoleID"].ToString();
        btnSubmit.Text = "Update";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtStartval.Text) && !string.IsNullOrEmpty(txtEndVal.Text.Trim()) && dllRole.SelectedIndex>0)
            {
                decimal startval = Convert.ToDecimal(txtStartval.Text.Trim());
                decimal endval = Convert.ToDecimal(txtEndVal.Text.Trim());
                if (startval <= endval)
                {
                    int Val = 0;
                   {
                        Val = cls.update_data("exec PROC_LoanLimit "+ dllRole.SelectedValue + ",'" + startval + "','" + endval + "'");
                        if (Val > 0)
                        {
                            ErrorShow.Error(page1: page, Message: "Successfully set");
                        }
                    }                    
                    
                    BindData();
                }
                else
                {
                    ErrorShow.Error(page1: page, Message: "End Value must be greater then Start Value ..");
                }
            }
        }
        catch (Exception)
        {
            ErrorShow.Error(page1: page, Message: "Something went wrong ..");
        }
    }


    protected void rptSurcharge_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);
        if (e.CommandName == "Edit")
        {
            FillData(id);
        }
    }

}