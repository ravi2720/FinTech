using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Plan : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    public Company company;

    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                FillData();
                BindDropDown.FillDropDown(ddlPackage, "ActivePackage", "Name", "ID", "Select Package", company.MemberID);
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        Int32 id = hdnid.Value == "0" ? 0 : Convert.ToInt32(hdnid.Value.ToString());
        if (ddlPackage.SelectedIndex > 0 && !string.IsNullOrEmpty(txtAmount.Text) && !string.IsNullOrEmpty(txtNewsName.Text))
        {
            Int32 Val = cls.update_data("Exec AddEditCreatePlan " + id + ",'" + txtNewsName.Text + "','" + ckNewsDesc.Text + "'," + chkActive.Checked + "," + ddlPackage.SelectedValue + ",'" + txtAmount.Text + "','" + company.MemberID + "'");
            if (Val > 0)
            {
                ErrorShow.AlertMessage(page, "Plan Add Successfully", ConstantsData.CompanyName);
                ConstantsData.Clear(page.Controls);
                FillData();
            }
            else
            {
                ErrorShow.AlertMessage(page, "Already Exists", ConstantsData.CompanyName);
            }
        }

    }

    protected void FillData()
    {
        dt = cls.select_data_dt("EXEC ManageCreatePlan 'Get','" + 0 + "','" + company.MemberID + "'");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();
        }
        else
        {
            repeater1.DataSource = null;
            repeater1.DataBind();

        }
    }

    protected void repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            hdnid.Value = e.CommandArgument.ToString();
            FillNews(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "Active")
        {
            Active(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }
    }

    private void FillNews(int id)
    {
        dt = cls.select_data_dt("EXEC ManageCreatePlan 'Get','" + id + "','" + company.MemberID + "'");
        txtNewsName.Text = dt.Rows[0]["Name"].ToString();
        txtAmount.Text = dt.Rows[0]["Amount"].ToString();
        chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
        ddlPackage.SelectedValue = dt.Rows[0]["PackageID"].ToString();
        ckNewsDesc.Text = dt.Rows[0]["Description"].ToString();
    }


    private void Active(int id)
    {
        int val = cls.update_data("ManageCreatePlan @Action='IsActive',@ID=" + id + ",'" + company.MemberID + "'");
        if (val > 0)
        {
            FillData();
        }

    }

    private void Clear()
    {
        txtNewsName.Text = "";
        ckNewsDesc.Text = "";

    }
}