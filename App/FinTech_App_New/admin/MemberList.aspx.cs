using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Default2 : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dtMember;
    Page page;
    public Company company;
    ActionButtonPermission actionButtonPermission;
    int Val = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            company = Company.GetCompanyInfo();
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.MemberReport, dtMember.Rows[0]["RoleID"].ToString());

            page = HttpContext.Current.CurrentHandler as Page;
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(dllRole, "RoleBasePermission", "Name", "ID", "Select Role", dtMember.Rows[0]["RoleID"].ToString());
                txtfromdate.Text = txttodate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                FillMember();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }
    private void FillMember()
    {
        DataTable dt;
        if (!string.IsNullOrEmpty(txtCityName.Text))
        {
            dt = ObjConnection.select_data_dt("ManageMemberList '','','" + txtCityName.Text + "',0,0");
        }
        else
        {
            dt = ObjConnection.select_data_dt("ManageMemberList '" + txtfromdate.Text + "','" + txttodate.Text + "','" + txtCityName.Text + "',1," + dllRole.SelectedValue + "");

        }
        rptData.DataSource = dt;
        rptData.DataBind();
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "IsActive")
        {
            if (actionButtonPermission.Active)
            {
                Active(ID: e.CommandArgument.ToString());
                Activity.AddActivity(dtMember.Rows[0]["ID"].ToString(), ConstantsData.MemberReport.ToString(), "", $"ID Active/DecActive by {dtMember.Rows[0]["Name"].ToString()}", e.CommandArgument.ToString(), "Active/DeActive", ObjConnection);
                ErrorShow.Success(page, "Successfully Active/Deactive");
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Active", ConstantsData.CompanyName);
            }
        }
        else if (e.CommandName == "VideoKYC")
        {
            if (actionButtonPermission.Active)
            {
                VideoKYC(ID: e.CommandArgument.ToString());
                Activity.AddActivity(dtMember.Rows[0]["ID"].ToString(), ConstantsData.MemberReport.ToString(), "", $"ID Video KYC by {dtMember.Rows[0]["Name"].ToString()}", e.CommandArgument.ToString(), "Video KYC", ObjConnection);
                ErrorShow.Success(page, "Successfully Video Kyc Active/Deactive");
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Active", ConstantsData.CompanyName);
            }
        }
        else if (e.CommandName == "HoldAmount")
        {
            if (actionButtonPermission.Active)
            {
                TextBox txtAmount = (TextBox)e.Item.FindControl("txtAmount");
                if (Convert.ToDecimal(txtAmount.Text) > 0)
                {
                    ObjConnection.update_data("update Member set HoldAmt='" + txtAmount.Text + "' where id='" + e.CommandArgument.ToString() + "'");
                }
                Activity.AddActivity(dtMember.Rows[0]["ID"].ToString(), ConstantsData.MemberReport.ToString(), "", $"Amount Hold {txtAmount.Text}{" By "} {dtMember.Rows[0]["Name"].ToString()}", e.CommandArgument.ToString(), "Amount Hold", ObjConnection);

                ErrorShow.Success(page, "Amount Hold Successfully");
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Hold Amount", ConstantsData.CompanyName);
            }
        }
        else if (e.CommandName == "IsShowNotificaion")
        {
            Notification(ID: e.CommandArgument.ToString());
        }
        else if (e.CommandName == "OnHold")
        {
            if (actionButtonPermission.Active)
            {
                Hold(ID: e.CommandArgument.ToString());
                ErrorShow.Success(page, "Member Hold/UnHold Successfully");
                Activity.AddActivity(dtMember.Rows[0]["ID"].ToString(), ConstantsData.MemberReport.ToString(), "", $"Member Hold By {dtMember.Rows[0]["Name"].ToString()}", e.CommandArgument.ToString(), "Account Hold", ObjConnection);
            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For Hold", ConstantsData.CompanyName);
            }

        }
        else if (e.CommandName == "Delete")
        {
            //  Delete(ID: e.CommandArgument.ToString());
        }
        else if (e.CommandName == "Login")
        {
            Login(ID: e.CommandArgument.ToString());
        }
        else if (e.CommandName == "ReSend")
        {
            if (actionButtonPermission.Active)
            {
                DataTable dtMem = ObjConnection.select_data_dt("select * from member where msrno='" + e.CommandArgument.ToString() + "'");
                if (dtMem.Rows.Count > 0)
                {
                    string[] ValueArray = new string[4];
                    ValueArray[0] = dtMem.Rows[0]["Name"].ToString();
                    ValueArray[1] = dtMem.Rows[0]["LoginID"].ToString();
                    ValueArray[2] = dtMem.Rows[0]["Password"].ToString();
                    ValueArray[3] = dtMem.Rows[0]["LoginPin"].ToString();
                    SMS.SendWithV(dtMem.Rows[0]["Mobile"].ToString(), ConstantsData.Registration_SMS, ValueArray, company.MemberID);
                    ErrorShow.AlertMessage(page, "Your Login ID Send Successfully", ConstantsData.CompanyName);
                }
                Activity.AddActivity(dtMember.Rows[0]["ID"].ToString(), ConstantsData.MemberReport.ToString(), "", $"SMS Send By {dtMember.Rows[0]["Name"].ToString()}", e.CommandArgument.ToString(), "SMS Send", ObjConnection);

            }
            else
            {
                ErrorShow.AlertMessage(page, "Not Permission For SMS Send", ConstantsData.CompanyName);
            }
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageMember @Action='IsActive',@Msrno=" + ID + "");
        if (Val > 0)
        {
            FillMember();
        }
    }

    private void VideoKYC(string ID)
    {
        Val = ObjConnection.update_data("ManageMember @Action='VideoKYC',@Msrno=" + ID + "");
        if (Val > 0)
        {
            FillMember();
        }
    }
    private void Delete(string ID)
    {
        Val = ObjConnection.update_data("ManageMember @Action='Delete',@Msrno=" + ID + "");
        if (Val > 0)
        {
            FillMember();
        }
    }

    private void Login(string ID)
    {
        DataTable dtMem = ObjConnection.select_data_dt("select * from Member where msrno='" + ID + "'");
        if (dtMem.Rows.Count > 0)
        {
            var key = "b14ca5898a4e4133bbce2ea2315a1916";
            var encryptedStringID = AesOperationLogin.EncryptString(key, dtMem.Rows[0]["LoginID"].ToString());
            var encryptedStringPass = AesOperationLogin.EncryptString(key, dtMem.Rows[0]["Password"].ToString());
          //  ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "window.open('https://agoodlife.co.in/member/default.aspx?AdminLogin=" + encryptedStringID + "&AdminLook=" + encryptedStringPass + "','_blank');", true);


        }
    }

    private void Hold(string ID)
    {
        Val = ObjConnection.update_data("ManageMember @Action='ONHOLD',@Msrno=" + ID + "");
        if (Val > 0)
        {
            FillMember();
        }
    }

    private void Notification(string ID)
    {
        Val = ObjConnection.update_data("ManageProcess @Action='NotificationBlock',@ID=" + ID + "");
        if (Val > 0)
        {
            FillMember();
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        FillMember();
    }
}