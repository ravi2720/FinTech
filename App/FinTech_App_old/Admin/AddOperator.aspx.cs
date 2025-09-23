using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddOperator : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    Operator operator1 = new Operator();

    int Val = 0;
    Page page;

    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlService, "ActiveService", "Name", "ID", "Select Service");
                GetOperator();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    public void GetOperator()
    {
        dt = ObjConnection.select_data_dt("ManageOperator @Action='GetAll'");
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
        operator1.Name = txtOperatorName.Text;
        operator1.OperatorCode = txtOperatorCode.Text;
        operator1.ID = Convert.ToInt32(hdnOperatorID.Value);
        operator1.ServiceID = Convert.ToInt32(ddlService.SelectedValue);
        operator1.IsActive = chkActive.Checked;
        if (operator1.IsAnyNullOrEmpty(operator1))
        {
            Val = ObjConnection.update_data("AddEditOperator " + operator1.ID + ",'" + operator1.Name + "','" + operator1.ServiceID + "','" + operator1.OperatorCode + "'," + operator1.IsActive + "");
            if (Val > 0)
            {
                GetOperator();
                ObjConnection.update_data("AddEditBBPSData '" + ddlService.SelectedItem.Text + "','" + operator1.Name + "','" + operator1.OperatorCode + "',1,'','',''");
                ErrorShow.AlertMessage(Page, "Update Successfully", ConstantsData.CompanyName);
                ConstantsData.Clear(page.Controls);
            }
        }
        else
        {
            ErrorShow.Error(Page, "Fill all required details");
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

        if (e.CommandName == "Pending")
        {
            Pending(ID: e.CommandArgument.ToString());
        }
    }

    private void FillData(string ID)
    {
        dt = ObjConnection.select_data_dt("ManageOperator @Action='GetAll',@ID=" + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnOperatorID.Value = ID;
            txtOperatorName.Text = dt.Rows[0]["Name"].ToString();
            txtOperatorCode.Text = dt.Rows[0]["OperatorCode"].ToString();
            ddlService.SelectedValue = dt.Rows[0]["ServiceID"].ToString();
            chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageOperator @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            GetOperator();
        }
    }

    private void Pending(string ID)
    {
        Val = ObjConnection.update_data("ManageOperator @Action='IsOffLine',@ID=" + ID + "");
        if (Val > 0)
        {
            GetOperator();
        }
    }

    private void Clear()
    {
        txtOperatorCode.Text = txtOperatorName.Text = "";
        chkActive.Checked = false;
        hdnOperatorID.Value = "0";
        ddlService.SelectedIndex = 0;
    }
}