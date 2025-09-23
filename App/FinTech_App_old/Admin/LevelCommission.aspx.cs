using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_LevelCommission : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    int Val = 0;
    Page page;

    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        dtMember = (DataTable)Session["dtEmployee"];
        if (!IsPostBack)
        {
            GetPackage();
        }
    }

    public void GetPackage()
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManageLevelCommission 'GetALL'");
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


            Val = ObjConnection.update_data("AddEditLevelCommission " + hdnPackageID.Value + ",'" + txtPrice.Text + "'");
            if (Val > 0)
            {
                ErrorShow.AlertMessage(page, "Add Level Successfully", ConstantsData.CompanyName);
                GetPackage(); Clear();
            }
            else
                ErrorShow.AlertMessage(page, "Add Level Successfully", ConstantsData.CompanyName);

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
        }
    }

    private void FillData(string ID)
    {
        try
        {
            dt = ObjConnection.select_data_dt("ManageLevelCommission @Action='GetAll',@ID=" + ID + "");
            if (dt.Rows.Count > 0)
            {
                hdnPackageID.Value = ID;
                txtPrice.Text = dt.Rows[0]["Commission"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, ex.InnerException.Message, ConstantsData.CompanyName);
        }
    }

    

    private void Clear()
    {
        txtPrice.Text = "0";
        hdnPackageID.Value = "0";
    }
}