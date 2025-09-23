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
    DataTable dt = new DataTable();
    Page page;

    //protected void Page_Preint(object sender, EventArgs e)
    //{
    //    page = HttpContext.Current.CurrentHandler as Page;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            FillTitle();
            FillGender();
            FillPackage();
            FillRole();
        }
    }


    private void FillTitle()
    {
        DataTable dtTitle = cls.select_data_dt("select * from Title");
        Common.BindDropDown(ddltitle,dtTitle,"Name","ID");
    }

    private void FillGender()
    {
        DataTable dtGender = cls.select_data_dt("select * from Gender");
        Common.BindRadioButtonList(rdGender, dtGender, "Name", "ID");
        rdGender.SelectedIndex = 0;
    }

    private void FillPackage()
    {
        DataTable dtPackage = cls.select_data_dt("select * from Role");
        Common.BindDropDown(ddlPackage, dtPackage, "Name", "ID");
    }

    private void FillRole()
    {
        DataTable dtRole = cls.select_data_dt("Exec ManageRole @Action='RoleBasePermission',@ID="+2+""); // pass login user RoleID
        Common.BindDropDown(ddlRole, dtRole, "Name", "ID");
        ddlRole.Items.Insert(0, new ListItem("--select Role--", "0"));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            Registration.Name = txtName.Text.Trim();
            Registration.Mobile = txtMobile.Text.Trim();
            Registration.LoginPin = Convert.ToInt16(txtLoginPin.Text.Trim());
            Registration.IsActive = chkActive.Checked;
            Registration.GenderID = Convert.ToInt16(rdGender.SelectedValue);
            Registration.Email = txtEmail.Text.Trim();
            Registration.CityID = Convert.ToInt16(0);
            Registration.Address = txtAddress.Text.Trim();
            Registration.PackageID = Convert.ToInt16(ddlPackage.SelectedValue);
            Registration.ParentID = Convert.ToInt16(1);  
            Registration.Password = txtPassword.Text.Trim();
            Registration.RoleID = Convert.ToInt16(ddlRole.SelectedValue);
            Registration.TitleID = Convert.ToInt16(ddltitle.SelectedValue);
            
            if (Registration.IsAnyNullOrEmpty(Registration))
            {
                int Val = cls.update_data("AddEditMember " + Registration.ID + ",'" + Registration.RoleID + "','"+Registration.TitleID+ "','" + Registration.Name + "','" + Registration.Email + "','" + Registration.Mobile + "','" + Registration.GenderID + "','" + Registration.LoginPin + "','" + Registration.Password + "','" + Registration.Address + "','" + Registration.CityID + "','" + Registration.PackageID + "',"+ Registration.ParentID + "");
                if (Val > 0)
                {
                    ErrorShow.Success(page:page, Message: "Record Inserted Successfully");
                    Clear();
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

    private void Clear()
    {
        txtAddress.Text = txtEmail.Text = txtLoginPin.Text = txtMobile.Text = txtName.Text = txtPassword.Text = string.Empty;
        ddlPackage.SelectedIndex = ddlRole.SelectedIndex = ddltitle.SelectedIndex = 0;
        rdGender.SelectedIndex = 0;
    }
}