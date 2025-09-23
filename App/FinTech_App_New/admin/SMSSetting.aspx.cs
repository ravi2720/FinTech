using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SMSSetting : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt, dtMember;
    Role role = new Role();
    int Val = 0;
    Company company;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            company = Company.GetCompanyInfo();
            if (!IsPostBack)
            {
                GetRole();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }

    }

    public void GetRole()
    {
        dt = ObjConnection.select_data_dt("ManageSMSSetting 'GetByMsrno','"+ company.MemberID+ "'");
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        {
            Val = ObjConnection.update_data("AddEditSMSSetting " + hdnRoleID.Value + ",'" + txtURL.Text + "','" + txtSender.Text + "','" + txtCountry.Text + "','" + txtroute.Text + "','" + txtParam1Text.Text + "','" + txtParam1Val.Text + "','" + txtParam2Text.Text + "','" + txtParam2Val.Text + "','" + txtParam3Text.Text + "','" + txtParam3Val.Text + "'," + role.IsActive + ",'"+txtRouteText.Text+"','"+txtCountryText.Text+"','"+txtDltText.Text+"','"+txtSenderText.Text+"','"+dtMember.Rows[0]["ID"].ToString()+"','"+company.MemberID+"'");
            if (Val > 0) {
                ErrorShow.AlertMessage(Page, "Save Successfully",ConstantsData.CompanyName);
                GetRole(); Clear();
            }
        }

    }

    protected void repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
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
        dt = ObjConnection.select_data_dt("ManageSMSSetting 'GetAll'," + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnRoleID.Value = ID;
            txtURL.Text = dt.Rows[0]["URL"].ToString();
            txtSender.Text = dt.Rows[0]["Sender"].ToString();
            txtCountry.Text = dt.Rows[0]["Country"].ToString();
            txtroute.Text = dt.Rows[0]["route"].ToString();
            txtParam1Text.Text = dt.Rows[0]["Param1Text"].ToString();
            txtParam1Val.Text = dt.Rows[0]["Param1Val"].ToString();
            txtParam2Text.Text = dt.Rows[0]["Param2Text"].ToString();
            txtParam2Val.Text = dt.Rows[0]["Param2Val"].ToString();
            txtParam3Text.Text = dt.Rows[0]["Param3Text"].ToString();
            txtParam3Val.Text = dt.Rows[0]["Param3Val"].ToString();
            txtSenderText.Text = dt.Rows[0]["SenderText"].ToString();
            txtRouteText.Text = dt.Rows[0]["RouteText"].ToString();
            txtDltText.Text = dt.Rows[0]["DltText"].ToString();
            txtCountryText.Text = dt.Rows[0]["CountryText"].ToString();
            chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageSMSSetting 'IsActive'," + ID + "");
        if (Val > 0)
        {
            GetRole();
        }
    }

    private void Clear()
    {
        txtURL.Text = txtCountry.Text = "";
        txtParam1Text.Text = txtParam1Val.Text = "";
        txtParam2Text.Text = txtParam2Val.Text = "";
        txtParam3Text.Text = txtParam3Val.Text = "";
        chkActive.Checked = false;
        hdnRoleID.Value = "0";
    }
}