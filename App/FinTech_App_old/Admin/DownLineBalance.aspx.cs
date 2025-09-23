using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DownLineBalance : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtMember;
    public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            company = Company.GetCompanyInfo();
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                GetBindData();
                BindDropDown.FillDropDown(ddlRole, "RoleBasePermission", "Name", "ID", "Select Role", dtMember.Rows[0]["RoleID"].ToString());
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }


    private void GetBindData()
    {
        DataTable dt = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where RoleID='" + ddlRole.SelectedValue + "'");       
        if (dt.Rows.Count > 0)
        {
            lblsubtotal.Text = Convert.ToString(dt.Compute("SUM(MainBalance)", "MainBalance > 0"));

            rptData.DataSource = dt;
            rptData.DataBind();
        }
        else
        {
            rptData.DataSource = null;
            rptData.DataBind();
            lblsubtotal.Text = "0";
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetBindData();
    }



    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "show")
        {
           
            Filldata(msrno: Convert.ToInt16(e.CommandArgument.ToString()));
        }
    }


    private void Filldata(int msrno)
    {
        DataTable dt = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where parentid='" + msrno + "'");
        if (dt.Rows.Count > 0)
        {
            lblsubtotal.Text = Convert.ToString(dt.Compute("SUM(MainBalance)", "MainBalance > 0"));

            rptData.DataSource = dt;
            rptData.DataBind();
        }
        else
        {
            rptData.DataSource = null;
            rptData.DataBind();
            lblsubtotal.Text = "0";
        }

    }
}