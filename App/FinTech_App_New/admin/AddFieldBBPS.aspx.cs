using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddFieldBBPS : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    Operator operator1 = new Operator();

    int Val = 0;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(dllService, "BBPSDistinctName", "ServiceType", "ServiceType", "Select Service");
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }
    public void GetOperator()
    {
        dt = ObjConnection.select_data_dt("ManageBBPSDatadown 'GetListByName','"+dllOpeator.SelectedValue+"'");
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
        if (!string.IsNullOrEmpty(txtLabel.Text))
        {
            Val = ObjConnection.update_data("AddEditBBPSDatadown '" + txtOperatorCode.Text + "','" + txtIndex.Text + "','" + txtLabel.Text + "'," + txtminLength.Text + ",'"+txtmaxLength.Text+"'");
            if (Val > 0)
            {
                GetOperator();
                ErrorShow.AlertMessage(Page, "Update Successfully", ConstantsData.CompanyName);
                ConstantsData.Clear(page.Controls);
            }
        }
        else
        {
            ErrorShow.Error(Page, "Fill all required details");
        }
    }

    protected void dllService_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDropDown.FillDropDown(dllOpeator, "BBPSDistinctNameList", "Operator", "spkey", "Select Opeator",dllService.SelectedValue);
    }

    protected void dllOpeator_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtOperatorCode.Text = dllOpeator.SelectedValue;
        GetOperator();
    }
}