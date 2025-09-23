using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AddBannerType : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    int Val = 0;
    Page page;

    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            if (!IsPostBack)
            {
                GetBannerType();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    public void GetBannerType()
    {
        dt = ObjConnection.select_data_dt("ManageBannerType @Action='GetAll'");
        if (dt.Rows.Count > 0)
        {
            rptData.DataSource = dt;
            rptData.DataBind();
        }
        else
        {
            rptData.DataSource = null;
            rptData.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtBannerTypeName.Text))
        {
            Val = ObjConnection.update_data("AddEditBannerType " + hdnBannerTypeID.Value + ",'" + txtBannerTypeName.Text + "','"+ chkActive.Checked + "'");
            if (Val > 0)
            {
                GetBannerType(); Clear();
                ErrorShow.AlertMessage(page, "Successfully Enter",ConstantsData.CompanyName);
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Enter Banner Type Value", ConstantsData.CompanyName);
        }
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            FillData(ID: e.CommandArgument.ToString());

        }

    }

    private void FillData(string ID)
    {
        dt = ObjConnection.select_data_dt("ManageBannerType @Action='GetAll',@ID=" + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnBannerTypeID.Value = ID;
            txtBannerTypeName.Text = dt.Rows[0]["Name"].ToString();
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageBannerType @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            GetBannerType();
        }
    }

    private void Clear()
    {
        txtBannerTypeName.Text = "";
        hdnBannerTypeID.Value = "0";
    }
}