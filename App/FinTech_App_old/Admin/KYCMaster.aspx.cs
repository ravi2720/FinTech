using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reatiler_ChangePassword : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            FillData();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtDocuments.Text))
        {
            if (btnSubmit.Text == "Submit")
            {
                dt = cls.select_data_dt("exec PROC_ADDEDITKYCDOCUMENTS 0,'" + txtDocuments.Text.Trim() + "','"+ Convert.ToInt16(ddlSide.SelectedValue) +"'");
            }
            else if (btnSubmit.Text == "Update")
            {
                dt = cls.select_data_dt("exec PROC_ADDEDITKYCDOCUMENTS '"+ Convert.ToInt16(hdnID.Value) + "','" + txtDocuments.Text.Trim() + "','" + Convert.ToInt16(ddlSide.SelectedValue) + "'");
                btnSubmit.Text = "Submit";
            }
            if (dt.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('"+ dt.Rows[0]["Status"] +"');", true);
                FillData();
            }
            txtDocuments.Text = "";
        }
    }

    private void FillData()
    {
        DataTable dtMember = cls.select_data_dt("exec PROC_MANAGEKYCDOCUMENTS 0,'GETALL'");
        if (dtMember.Rows.Count > 0)
        {
            rptData.DataSource = dtMember;
        }
        else
        {
            rptData.DataSource = null;
        }
        rptData.DataBind();
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "IsActive")
        {
            int a = cls.update_data("exec PROC_MANAGEKYCDOCUMENTS "+Convert.ToInt16(e.CommandArgument) + ",'ISACTIVE'");
            if (a>0)
            {
                ErrorShow.Success(page,"Status Updated Successfully");
                FillData();
            }
        }

        if (e.CommandName == "Edit")
        {
            dt = cls.select_data_dt("select * from KYCDocumentsMaster where id = " + Convert.ToInt16(e.CommandArgument) +"");
            if (dt.Rows.Count>0)
            {
                hdnID.Value = e.CommandArgument.ToString();
                txtDocuments.Text = dt.Rows[0]["Name"].ToString();
                ddlSide.SelectedValue = dt.Rows[0]["Side"].ToString();
                btnSubmit.Text = "Update";
            }
        }
    }
}