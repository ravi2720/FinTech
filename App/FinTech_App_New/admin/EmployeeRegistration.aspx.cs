using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeRegistration_Admin : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    Registration Registration = new Registration();
    DataTable dt, dtMember;
    Page page;
    public Company company;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            company = Company.GetCompanyInfo();
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddltitle, "ActiveTitle", "Name", "ID", "Select Title");
                BindDropDown.FillDropDown(rdGender, "ActiveGender", "Name", "ID", "Select Gender");
                BindDropDown.FillDropDown(ddlRole, "RoleBasePermission", "Name", "ID", "Select Role", dtMember.Rows[0]["RoleID"].ToString());
                if (Request.QueryString["ID"] != null)
                {
                    FillDetails();
                }

            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void FillDetails()
    {
        DataTable dt = cls.select_data_dt("select * from Employee where ID=" + Request.QueryString["ID"].ToString() + "");
        if (dt.Rows.Count > 0)
        {
            Registration.ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
            ddlRole.SelectedValue = dt.Rows[0]["RoleID"].ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtAge.Text = dt.Rows[0]["Age"].ToString();
            txtEmail.Text = dt.Rows[0]["Email"].ToString();
            txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
            txtLoginPin.Text = dt.Rows[0]["TransactionPassword"].ToString();
            txtPassword.Text = "";
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            txtPincode.Text = dt.Rows[0]["Pincode"].ToString();
            txtLoginID.Text = dt.Rows[0]["LOGINID"].ToString();
            chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Registration.Name = txtName.Text.Trim();
            Registration.Mobile = txtMobile.Text.Trim();
            Registration.LoginPin = Convert.ToInt32(txtLoginPin.Text.Trim());
            Registration.IsActive = chkActive.Checked;
            //Registration.GenderID = Convert.ToInt16(rdGender.SelectedValue);
            Registration.Email = txtEmail.Text.Trim();
            //Registration.CityID = Convert.ToInt16(0);
            Registration.Address = txtAddress.Text.Trim();
            //Registration.PackageID = Convert.ToInt16(ddlPackage.SelectedValue);
            //Registration.ParentID = Convert.ToInt16(1);
            Registration.Password = txtPassword.Text.Trim();
            Registration.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
            //Registration.TitleID = Convert.ToInt16(ddltitle.SelectedValue);
            if (Request.QueryString["ID"] != null)
            {
                Registration.ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
            }

            if (Registration.IsAnyNullOrEmpty(Registration))
            {
                DataTable Val = cls.select_data_dt("AddEditEmployee " + Registration.ID + ",'" + Registration.RoleID + "','" + Registration.Name + "','" + Registration.Mobile + "','" + Registration.Email + "','" + txtAge.Text + "','1','1','1','" + Registration.Address + "','" + txtPincode.Text.Trim() + "','" + Registration.Password + "','" + Registration.LoginPin + "','"+txtLoginID.Text+"','" + dtMember.Rows[0]["ID"].ToString() + "'");
                if (Convert.ToInt32(Val.Rows[0]["ID"]) > 0)
                {
                    ErrorShow.AlertMessage(page, "Staff Successfully Register",ConstantsData.CompanyName);
                    ConstantsData.Clear(page.Controls);
                }
                else
                {
                    ErrorShow.Error(page1: page, Message: "Record Not Inserted, Try Again ..");
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            ErrorShow.Error(page1: page, Message: "Something Went Wrong ..");
        }
    }

   
}