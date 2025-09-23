using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reatiler_Default : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    Registration Registration = new Registration();
    DataTable dt, dtMember;
    Page page;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddltitle, "ActiveTitle", "Name", "ID", "Select Title");
                BindDropDown.FillDropDown(rdGender, "ActiveGender", "Name", "ID", "Select Gender");
                BindDropDown.FillDropDown(ddlRole, "RoleBasePermission", "Name", "ID", "Select Role", "1");
                if (Request.QueryString["ID"] != null)
                {
                    FillMemberDetails(Convert.ToInt32(Request.QueryString["ID"].ToString()));
                }

            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Registration.ID = Convert.ToInt32(Request.QueryString["ID"].ToString());
            Registration.Name = txtName.Text.Trim();
            Registration.ShopName = txtShopName.Text.Trim();
            Registration.Mobile = txtMobile.Text.Trim();
            Registration.LoginPin = Convert.ToInt32(txtLoginPin.Text.Trim());
            Registration.IsActive = chkActive.Checked;
            Registration.GenderID = Convert.ToInt16(rdGender.SelectedValue);
            Registration.Email = txtEmail.Text.Trim();
            Registration.CityID = Convert.ToInt32(0);
            Registration.Address = txtAddress.Text.Trim();
            Registration.PinCode = txtpincode.Text.Trim();
            Registration.DOB = Convert.ToDateTime(txtDOB.Text.Trim()).ToString("MM-dd-yyyy");
            Registration.PackageID = Convert.ToInt32(ddlPackage.SelectedValue);
            Registration.ParentID = Convert.ToInt32(1);
            Registration.Password = txtPassword.Text.Trim();
            Registration.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
            Registration.TitleID = Convert.ToInt32(ddltitle.SelectedValue);
            Registration.AlterNativeMobile = txtAlterMobile.Text;
            Registration.Aadhar = txtAadhar.Text;
            Registration.Pan = txtPanNumber.Text;
            if (Registration.IsAnyNullOrEmpty(Registration))
            {
                int Val = cls.update_data("AddEditMember " + Registration.ID + ",'" + Registration.RoleID + "','" + Registration.TitleID + "','" + Registration.Name + "','" + Registration.Email + "','" + Registration.Mobile + "','" + Registration.GenderID + "','" + Registration.LoginPin + "','" + Registration.Password + "','" + Registration.Address + "','" + Registration.CityID + "','" + Registration.PackageID + "'," + Registration.ParentID + ",'" + txtAlterMobile.Text + "','" + txtAadhar.Text + "','" + txtPanNumber.Text + "','" + Registration.PinCode + "','" + Registration.DOB + "','" + Registration.ShopName + "','',0");
                if (Val > 0)
                {
                    ErrorShow.AlertMessageWithRedirect(page,"Record Updated Successfully","MemberList.aspx",ConstantsData.CompanyName);
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

    private void FillMemberDetails(int id)
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where ID=" + id + "");
        txtAddress.Text = dt.Rows[0]["Address"].ToString();
        txtEmail.Text = dt.Rows[0]["Email"].ToString();
        txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
        txtName.Text = dt.Rows[0]["Name"].ToString();
        txtShopName.Text = dt.Rows[0]["ShopName"].ToString();

        txtLoginPin.Text = dt.Rows[0]["LoginPin"].ToString();
        txtPassword.Text = dt.Rows[0]["Password"].ToString();
        txtpincode.Text = dt.Rows[0]["PinCode"].ToString();
        try
        {
            txtDOB.Text = Convert.ToDateTime(dt.Rows[0]["DOB"].ToString()).ToString("dd-MM-yyyy");
        }
        catch (Exception ex)
        {
        }

        ddlRole.SelectedValue = dt.Rows[0]["RoleID"].ToString();

        BindDropDown.FillDropDown(ddlPackage, "RoleWisePackage", "Name", "ID", "Select Package",ddlRole.SelectedValue.ToString());

        ddlPackage.SelectedValue = dt.Rows[0]["PackageID"].ToString();
        
       
        lblPackageName.Text = dt.Rows[0]["RoleName"].ToString();
        txtAlterMobile.Text = dt.Rows[0]["AlterNativeMobileNumber"].ToString();
        txtAadhar.Text = dt.Rows[0]["Aadhar"].ToString();
        txtPanNumber.Text = dt.Rows[0]["Pan"].ToString();
        ddltitle.SelectedValue = dt.Rows[0]["TitleID"].ToString();
        rdGender.SelectedValue = dt.Rows[0]["GenderID"].ToString();
    }
    private void Clear()
    {
        txtAddress.Text = txtEmail.Text = txtLoginPin.Text = txtMobile.Text = txtName.Text = txtPassword.Text = string.Empty;
        ddlPackage.SelectedIndex = ddlRole.SelectedIndex = ddltitle.SelectedIndex = 0;
        rdGender.SelectedIndex = 0;
    }
}