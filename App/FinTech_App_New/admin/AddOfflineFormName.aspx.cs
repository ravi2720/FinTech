using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AddOfflineFormName : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    Package package = new Package();
    DataTable dtMember = new DataTable();
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
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                GetData();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    public void GetData()
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManageOffLineservice 'GetByMSRNO'," + dtMember.Rows[0]["ID"].ToString() + "");
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
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message,ConstantsData.CompanyName);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            package.Name = txtFormName.Text;
            package.ID = Convert.ToInt32(hdnPackageID.Value);
            package.Price = Convert.ToDecimal(txtPrice.Text);
            package.IsActive = chkActive.Checked;
            if (package.IsAnyNullOrEmpty(package))
            {
                Val = ObjConnection.update_data("AddEditOffLineservice " + package.ID + ",'" + package.Name + "','" + package.Price + "'," + package.IsActive + "," + dtMember.Rows[0]["ID"].ToString() + "");
                if (Val > 0)
                {
                    ErrorShow.AlertMessage(page, "Add OfflineService Successfully", ConstantsData.CompanyName);
                    GetData(); Clear();
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Enter All Required Field", ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            FillData(ID: e.CommandArgument.ToString());
        }

        if (e.CommandName == "Active")
        {
            Active(ID: e.CommandArgument.ToString());
        }
    }

    private void FillData(string ID)
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManageOffLineservice @Action='GetAll',@ID=" + ID + "");
            if (dt.Rows.Count > 0)
            {
                hdnPackageID.Value = ID;
                txtFormName.Text = dt.Rows[0]["Name"].ToString();
                txtPrice.Text = dt.Rows[0]["Amount"].ToString();
                chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }
    }

    private void Active(string ID)
    {
        try
        {
            Val = ObjConnection.update_data("ManageOffLineservice @Action='IsActive',@ID=" + ID + "");
            if (Val > 0)
            {
                GetData();
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }
    }

    private void Clear()
    {
        txtFormName.Text = "";
        txtPrice.Text = "0";
        chkActive.Checked = false;
        hdnPackageID.Value = "0";
    }
}