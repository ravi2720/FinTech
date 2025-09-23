using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RechargeCommission : System.Web.UI.Page
{
    cls_connection objconnection = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Page page;
    ActionButtonPermission actionButtonPermission;
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
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.RechargeCommission, dtMember.Rows[0]["RoleID"].ToString());

            if (!IsPostBack)
            {
                Session["mySurcharge"] = null;
                BindDropDown.FillDropDown(ddlPackage, "ActivePackage", "Name", "ID", "Select Package", dtMember.Rows[0]["ID"].ToString());
                BindDropDown.FillDropDown(ddlService, "ActiveService", "Name", "ID", "Select Service");
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (actionButtonPermission.Submit)
        {
            #region [Insert]
            Int32 intresult = 0;
            foreach (RepeaterItem row in gvMemberMaster.Items)
            {
                HiddenField hidOpid = (HiddenField)row.FindControl("hidOpid");
                if (hidOpid.Value != "")
                {
                    int OperatorID = Convert.ToInt32(hidOpid.Value);
                    TextBox txtCommission = (TextBox)row.FindControl("txtCommission");
                    decimal Commission = Convert.ToDecimal(txtCommission.Text);
                    CheckBox chkSurcharge = (CheckBox)row.FindControl("chkSurcharge");
                    CheckBox chkFlat = (CheckBox)row.FindControl("chkFlat");
                    HiddenField hdnActiveAPI = (HiddenField)row.FindControl("hdnActiveAPI");
                    if (txtCommission.Text.Trim() == "")
                    {
                        txtCommission.Text = "0";
                    }
                    if (hdnActiveAPI.Value != "&nbsp;")
                    {
                        int APIID = Convert.ToInt32(hdnActiveAPI.Value);
                        intresult = intresult + objconnection.insert_data("AddEditRechargeCommission " + OperatorID + "," + Commission + "," + Convert.ToInt32(ddlPackage.SelectedValue) + "," + APIID + ",'" + chkFlat.Checked + "','" + chkSurcharge.Checked + "'," + dtMember.Rows[0]["ID"].ToString() + "");
                    }
                    else
                    {
                        intresult = intresult + objconnection.insert_data("AddEditRechargeCommission " + OperatorID + "," + Commission + "," + Convert.ToInt32(ddlPackage.SelectedValue) + "," + 0 + ",'" + chkFlat.Checked + "','" + chkSurcharge.Checked + "'," + dtMember.Rows[0]["ID"].ToString() + "");
                    }
                }
            }
            if (intresult > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alertify.success('Success|Plan save successfully !!');", true);
                ErrorShow.AlertMessage(page, "Success|Plan save successfully !!", ConstantsData.CompanyName);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alertify.error('Error |Kindly check and Update Again !!');", true);
            }
            #endregion
        }
        else
        {
            ErrorShow.AlertMessage(page, "Not Permission For Add Recharge Commission", ConstantsData.CompanyName);
        }
    }

    public void selectChk()
    {
        string OpreratorId = "";
        string serviceType = ",";
        for (int i = 0; i < gvMemberMaster.Items.Count; i++)
        {
            CheckBox chk = (CheckBox)gvMemberMaster.Items[i].FindControl("chk");
            if (chk.Checked)
            {
                HiddenField hid = (HiddenField)gvMemberMaster.Items[i].FindControl("hidOpid");
                OpreratorId = OpreratorId + hid.Value.ToString() + ",";
                DataTable dt = new DataTable();
                dt = objconnection.select_data_dt(@"select s.id as ServiceTypeId  from service as s inner join Operator as op on
                s.id=op.ServiceId where op.operatorid='" + Convert.ToInt32(hid.Value) + "' ");
                string ss = dt.Rows[0]["ServiceTypeId"].ToString();
                if (!serviceType.Contains("," + ss + ","))
                {
                    serviceType = serviceType + ss + ",";
                }
            }
        }
        serviceType = serviceType.TrimStart(',');
    }

    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindList();
    }

    private void BindList()
    {
        if (Convert.ToInt32(ddlPackage.SelectedItem.Value) > 0)
        {
            btnSubmit.Visible = true;
            dt = objconnection.select_data_dt("Exec ManageRechargeCommission '" + ddlPackage.SelectedValue + "',0,'admin','" + ddlService.SelectedValue + "'");
            gvMemberMaster.DataSource = dt;
            gvMemberMaster.DataBind();
            gvMemberMaster.Visible = true;
        }
        else
        {
            btnSubmit.Visible = false;
            gvMemberMaster.DataSource = null;
            gvMemberMaster.DataBind();
            gvMemberMaster.Visible = false;

        }
    }
    protected void gvMemberMaster_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
    }



    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindList();

    }
}