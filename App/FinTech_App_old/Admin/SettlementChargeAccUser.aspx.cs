using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_SettlementChargeAccUser : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            FillMember();
            BindData();
            btnSubmit.Text = "Submit";
        }
    }

    private void BindData()
    {
        DataTable dtMember;
        if (ddlMember.SelectedIndex > 0)
        {
            dtMember = cls.select_data_dt("select hh.*,mm.loginid from SettlementChargeAccUser hh inner join Member mm on mm.msrno=hh.msrno where hh.Msrno=" + ddlMember.SelectedValue + "");
        }
        else
        {
            dtMember = cls.select_data_dt("select hh.*,mm.loginid from SettlementChargeAccUser hh inner join Member mm on mm.msrno=hh.msrno");

        }
        rptSurcharge.DataSource = dtMember;
        rptSurcharge.DataBind();
    }

    private void FillData(int id)
    {
        hdnID.Value = Convert.ToString(id);
        DataTable dtdata = cls.select_data_dt("select * from SettlementChargeAccUser where id=" + id + "");
        txtStartval.Text = dtdata.Rows[0]["startval"].ToString();
        txtEndVal.Text = dtdata.Rows[0]["endval"].ToString();
        txtSurcharge.Text = dtdata.Rows[0]["surcharge"].ToString();
        ddlMember.SelectedValue = dtdata.Rows[0]["msrno"].ToString();
        chkIsFlat.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsFlat"].ToString());
        btnSubmit.Text = "Update";
    }
    private void FillMember()
    {
        DataTable dtMember = cls.select_data_dt("select (LoginID + ' ( ' + Name + ' ) ')as Name,msrno from Member where isactive=1");
        Common.BindDropDown(ddlMember, dtMember, "Name", "msrno");
        ddlMember.Items.Insert(0, new ListItem("-- Select Member --", "0"));
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtStartval.Text) && !string.IsNullOrEmpty(txtEndVal.Text.Trim()) && !string.IsNullOrEmpty(txtSurcharge.Text))
            {
                decimal startval = Convert.ToDecimal(txtStartval.Text.Trim());
                decimal endval = Convert.ToDecimal(txtEndVal.Text.Trim());
                decimal surcharge = Convert.ToDecimal(txtSurcharge.Text.Trim());
                if (startval <= endval)
                {
                    DataTable dt = new DataTable();
                    if (btnSubmit.Text == "Submit")
                    {
                        dt = cls.select_data_dt("exec AddEditSettlementChargeAccUser 'insert',0,'" + startval + "','" + endval + "','" + surcharge + "','" + chkIsFlat.Checked + "',"+ddlMember.SelectedValue+"");
                    }
                    else if (btnSubmit.Text == "Update")
                    {
                        dt = cls.select_data_dt("exec AddEditSettlementChargeAccUser 'update'," + Convert.ToInt32(hdnID.Value) + ",'" + startval + "','" + endval + "','" + surcharge + "','" + chkIsFlat.Checked + "'," + ddlMember.SelectedValue + "");
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


    protected void rptSurcharge_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int id = Convert.ToInt32(e.CommandArgument);       
        if (e.CommandName == "Edit")
        {
            FillData(id);
        }
    }


    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMember.SelectedIndex > 0)
        {
            BindData();
        }
    }
}