using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_ServiceSection : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dt = new DataTable();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            if (!IsPostBack)
            {
                dtEmployee = (DataTable)SessionManager.CurrentSessionAdmin;
                FillService();
                FillData();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }
    private void FillData()
    {
        dt = cls.select_data_dt("ManageServiceSection 'GetAll','" + hdnIDVal.Value + "'");
        rptdata.DataSource = dt;
        rptdata.DataBind();
        if (hdnIDVal.Value.ToInt32() > 0)
        {
            txtName.Text = dt.Rows[0]["Name"].ToString();
        }
    }
    private void FillService()
    {
        try
        {
            dt = cls.select_data_dt("FillDropDown 'ActiveService'");
            rptAllService.DataSource = dt;
            rptAllService.DataBind();

            if (hdnIDVal.Value.ToInt32() > 0)
            {
                FillData();
                string strOld = cls.select_data_scalar_string("ManageServiceSection 'GetSTR'," + hdnIDVal.Value + "");
                if (!string.IsNullOrEmpty(strOld))
                {
                    foreach (RepeaterItem item in rptAllService.Items)
                    {
                        CheckBox chkData = (CheckBox)item.FindControl("chkData");
                        HiddenField hdnVal = (HiddenField)item.FindControl("hdnVal");

                        for (int i = 0; i < strOld.Split(',').Length; i++)
                        {
                            if (hdnVal.Value == strOld.Split(',')[i])
                            {
                                chkData.Checked = true;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }



    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string str = "";
        foreach (RepeaterItem item in rptAllService.Items)
        {
            CheckBox chkData = (CheckBox)item.FindControl("chkData");
            HiddenField hdnVal = (HiddenField)item.FindControl("hdnVal");
            if (chkData.Checked)
            {
                str = str + "," + hdnVal.Value;
            }
        }

        cls.update_data("AddEditServiceSection " + hdnIDVal.Value + ",'" + txtName.Text + "','" + str + "',1");
        hdnIDVal.Value = "0";
        ErrorShow.AlertMessage(Page, "Service Section Successfully.", ConstantsData.CompanyName);
    }


    protected void rptdata_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        hdnIDVal.Value = e.CommandArgument.ToString();
        if (e.CommandName == "Edit")
        {
            FillService();
        }
        else if (e.CommandName == "Active")
        {
            Int32 Val = cls.update_data("ManageServiceSection 'Active','" + hdnIDVal.Value + "'");
            if (Val > 0)
            {
                hdnIDVal.Value = "0";
                FillData();
                ErrorShow.AlertMessage(Page, "Service Section Active/DeActive.", ConstantsData.CompanyName);
            }
        }
    }
}