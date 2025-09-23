using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SettelmentTime : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            BindData();
            btnSubmit.Text = "Submit";
        }
    }

    private void BindData()
    {
        DataTable dtMember = cls.select_data_dt("select iif(PayOutFrom=0,'RazaorPay','Instant Pay')PayOutFrom,* from impstime");
        rptTime.DataSource = dtMember;
        rptTime.DataBind();
    }

    private void FillData(int id)
    {
        hdnID.Value = Convert.ToString(id);
        DataTable dtdata = cls.select_data_dt("select * from impstime where id=" + id + "");
        txtStartTime.Text = dtdata.Rows[0]["starttime"].ToString();
        txtEndTime.Text = dtdata.Rows[0]["endtime"].ToString();
        txtReason.Text = dtdata.Rows[0]["Reason"].ToString();
        dllPayoutFrom.SelectedValue = dtdata.Rows[0]["PayoutFrom"].ToString();
        chkOnoFF.Checked = Convert.ToBoolean(dtdata.Rows[0]["OnOFF"].ToString());
        btnSubmit.Text = "Update";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtStartTime.Text) && !string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                decimal startval = Convert.ToDecimal(txtStartTime.Text.Trim());
                decimal endval = Convert.ToDecimal(txtEndTime.Text.Trim());
                if (startval <= endval)
                {
                    DataTable dt = new DataTable();
                    if (btnSubmit.Text == "Submit")
                    {
                        dt = cls.select_data_dt("exec AddEditIPMSTime 0,'" + startval + "','" + endval + "','" + chkOnoFF.Checked + "','" + txtReason.Text + "',"+dllPayoutFrom.SelectedValue+"");
                    }
                    else if (btnSubmit.Text == "Update")
                    {
                        dt = cls.select_data_dt("exec AddEditIPMSTime " + Convert.ToInt32(hdnID.Value) + ",'" + startval + "','" + endval + "','" + chkOnoFF.Checked + "','" + txtReason.Text + "'," + dllPayoutFrom.SelectedValue + "");
                    }
                    if (Convert.ToInt32(dt.Rows[0]["id"]) > 0)
                    {
                        ErrorShow.Success(page: page, Message: dt.Rows[0]["status"].ToString());
                    }
                    else
                    {
                        ErrorShow.Error(page1: page, Message: dt.Rows[0]["status"].ToString());
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





    protected void rptTime_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Edit")
        {
            FillData(id);
        }
    }
}