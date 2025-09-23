using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AssignPackageRoleWise : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtMember;
    DataTable dt = new DataTable();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            if (!IsPostBack)
            {
                dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
                BindDropDown.FillDropDown(dllRole, "RoleBasePermission", "Name", "ID", "Select Role", dtMember.Rows[0]["RoleID"].ToString());
                BindPackage();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }

    }

    private void BindPackage()
    {
        dt = cls.select_data_dt("ManagePackage 'GetByMSRNO'," + dtMember.Rows[0]["ID"].ToString() + "");
        rptAllPackage.DataSource = dt;
        rptAllPackage.DataBind();
    }

   

    private void FillService(string ID)
    {
        try
        {

            string strOld = cls.select_data_scalar_string("select PackageID from Role where ID=" + ID + "");
            if (!string.IsNullOrEmpty(strOld))
            {
                foreach (RepeaterItem item in rptAllPackage.Items)
                {
                    CheckBox chkData = (CheckBox)item.FindControl("chkData");
                    HiddenField hdnVal = (HiddenField)item.FindControl("hdnVal");

                    for (int i = 0; i < strOld.Split(',').Length; i++)
                    {
                        if (hdnVal.Value == strOld.Split(',')[i])
                        {
                            chkData.Checked = true;
                        }
                        else
                        {
                            chkData.Checked = false;

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
        foreach (RepeaterItem item in rptAllPackage.Items)
        {
            CheckBox chkData = (CheckBox)item.FindControl("chkData");
            HiddenField hdnVal = (HiddenField)item.FindControl("hdnVal");
            if (chkData.Checked)
            {
                str = str + "," + hdnVal.Value;
            }
        }
        dt = cls.select_data_dt("select * from Role where ID='" + dllRole.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            cls.update_data("PROC_ASSIGN_Package '" + str + "'," + dt.Rows[0]["ID"] + "");

            ErrorShow.AlertMessage(page, "Assign Succesfully",ConstantsData.CompanyName);
        }
    }

    protected void dllRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dllRole.SelectedIndex > 0)
        {

            dt = cls.select_data_dt("select * from Role where ID='" + dllRole.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                lblMemberName.Text = Convert.ToString(dt.Rows[0]["Name"]);
                hidMsrNo.Value = Convert.ToString(dt.Rows[0]["ID"]);
                FillService(dt.Rows[0]["ID"].ToString());
            }
            else
            {
                ErrorShow.AlertMessage(page, "Please Enter Valid Role  !", ConstantsData.CompanyName);
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Please Enter Role  !", ConstantsData.CompanyName);
        }
    }
}