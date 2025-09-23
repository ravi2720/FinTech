using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_BankDetails : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    Page page;

    //protected void Page_Preint(object sender, EventArgs e)
    //{
    //    page = HttpContext.Current.CurrentHandler as Page;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {

        if (SessionManager.IsSessionAdmin)
        {
            if (!IsPostBack)
            {
                FillData();
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", "1");
                BindBank();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void BindBank()
    {
        DataTable dtBank = cls.select_data_dt("select * from Bank where IsActive=1");
        if (dtBank.Rows.Count > 0)
        {
            ddlBankList.DataSource = dtBank;
            ddlBankList.DataTextField = "Name";
            ddlBankList.DataValueField = "ID";
            ddlBankList.DataBind();
            ddlBankList.Items.Insert(0, new ListItem("-- Select Bank --", "0"));
        }
    }

  
    private void FillData()
    {
        DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERBANKDETAILS");
        rptData.DataSource = dtMember;
        rptData.DataBind();
    }

    private void FillBankDetails(int ID)
    {
        DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERBANKDETAILS where ID = " + ID + "");
        if (dtMember.Rows.Count > 0)
        {
            BindBank();
            ddlBankList.SelectedValue = dtMember.Rows[0]["BankID"].ToString();
            txtAccountHolderName.Text = dtMember.Rows[0]["AccountHolderName"].ToString();
            txtAccountNo.Text = dtMember.Rows[0]["AccountNumber"].ToString();
            txtBranchName.Text = dtMember.Rows[0]["BranchName"].ToString();
            txtIfscCode.Text = dtMember.Rows[0]["IfscCode"].ToString();
            btnSubmit.Text = "Update";
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (btnSubmit.Text == "Save")
        {
            dt = cls.select_data_dt("exec AddEditMemberBankDetails 0,'" + Convert.ToInt32(hdnMsrno.Value) + "','" + Convert.ToInt16(ddlBankList.SelectedValue) + "','" + txtIfscCode.Text.Trim() + "','" + txtAccountNo.Text.Trim() + "','" + txtAccountHolderName.Text.Trim() + "','" + txtBranchName.Text.Trim() + "'");
            FillData();
        }
        else if (btnSubmit.Text == "Update")
        {
            dt = cls.select_data_dt("exec AddEditMemberBankDetails '" + Convert.ToInt32(hdnID.Value) + "','0','" + Convert.ToInt16(ddlBankList.SelectedValue) + "','" + txtIfscCode.Text.Trim() + "','" + txtAccountNo.Text.Trim() + "','" + txtAccountHolderName.Text.Trim() + "','" + txtBranchName.Text.Trim() + "'");
            btnSubmit.Text = "Save";
            btnSubmit.Enabled = false;
            btnDone.Visible = true;
        }
        Label1.Text = dt.Rows[0]["Status"].ToString();
        Clear();
    }

    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = cls.select_data_dt("Select * from MemberBankDetails where msrno = " + Convert.ToInt16(ddlMember.SelectedValue) + "");
        if (dt.Rows.Count > 0)
        {
            lblMessage.Text = "*Member Already Have Registered A Bank Account ..!!*";
            MultiView1.ActiveViewIndex = 0;
        }
        else
        {
            hdnMsrno.Value = ddlMember.SelectedValue;
            BindBank();
            MultiView1.ActiveViewIndex = 1;
        }
    }

    private void Clear()
    {
        txtAccountHolderName.Text = txtBranchName.Text = txtAccountNo.Text = txtIfscCode.Text = string.Empty;
        ddlMember.SelectedIndex = ddlBankList.SelectedIndex = 0;
        //MultiView1.ActiveViewIndex = 0;
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "IsActive")
        {
            DataTable dt = cls.select_data_dt("exec ManageMemberBankDetails " + Convert.ToInt16(e.CommandArgument) + ",'IsActive'");
            Response.Write("<script>alert('" + dt.Rows[0]["Status"].ToString() + "')</script>");
            FillData();
        }
        if (e.CommandName == "Edit")
        {
            hdnID.Value = e.CommandArgument.ToString();
            FillBankDetails(Convert.ToInt16(hdnID.Value));
            MultiView1.ActiveViewIndex = 1;
        }
        if (e.CommandName == "Delete")
        {
            DataTable dt = cls.select_data_dt("exec ManageMemberBankDetails " + Convert.ToInt16(e.CommandArgument) + ",'delete'");

            ErrorShow.AlertMessage(Page, dt.Rows[0]["Status"].ToString(),ConstantsData.CompanyName);
            FillData();
        }


    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        FillData();
    }
}