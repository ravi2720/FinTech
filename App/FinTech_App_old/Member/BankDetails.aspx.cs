using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_BankDetails : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt;
    DataTable dtMember;
    Page page;
    public Permission permission;
    public Company company;
    protected void Page_Init(object sender, EventArgs e)
    {
        permission = Permission.GetPermission();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        company = Company.GetCompanyInfo();
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            hdnMsrno.Value = dtMember.Rows[0]["Msrno"].ToString();
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlBankList, "ActiveBankList", "Name", "ID", "Select Bank");
                FillBankDetails();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void FillBankDetails()
    {
        DataTable dtData = cls.select_data_dt("select * from VIEW_MEMBERBANKDETAILS where msrno = " + Convert.ToInt32(dtMember.Rows[0]["Msrno"]) + "");
        if (dtData.Rows.Count > 0)
        {
            BindDropDown.FillDropDown(ddlBankList, "ActiveBankList", "Name", "ID", "Select Bank");
            rptBankData.DataSource = dtData;
            rptBankData.DataBind();
        }
    }

    private void EditBank(string ID)
    {
        DataTable dtData = cls.select_data_dt("select * from VIEW_MEMBERBANKDETAILS where ID = " + ID + "");
        if (dtData.Rows.Count > 0)
        {
            BindDropDown.FillDropDown(ddlBankList, "ActiveBankList", "Name", "ID", "Select Bank");
            //  ddlBankList.SelectedValue = dtData.Rows[0]["BankID"].ToString();
            txtAccountHolderName.Text = dtData.Rows[0]["AccountHolderName"].ToString();
            txtAccountNo.Text = dtData.Rows[0]["AccountNumber"].ToString();
            txtBranchName.Text = dtData.Rows[0]["BranchName"].ToString();
            txtIfscCode.Text = dtData.Rows[0]["IfscCode"].ToString();
            btnSubmit.Text = "Update";

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (btnSubmit.Text == "Save")
        {
            if (permission.BankAddOTP)
            {
                Random generator = new Random();
                int r = generator.Next(100000, 999999);
                string OTP = r.ToString();
                string Message = "Verify OTP is : " + OTP;
                Session["OTP"] = OTP;

                string[] ValueArray = new string[4];
                ValueArray[0] = "User";
                ValueArray[1] = OTP;
                string str = SMS.SendWithV(dtMember.Rows[0]["Mobile"].ToString(), ConstantsData.Bank_Add_OTP_SMS, ValueArray, company.MemberID);

                divOTP.Visible = true;
                btnSubmit.Text = "VerifyOTP";
                ErrorShow.AlertMessage(page,"OTP send on " + dtMember.Rows[0]["Mobile"].ToString() + " your mobile no",ConstantsData.CompanyName);
            }
            else if (permission.BankAddTPIN)
            {
                divOTP.Visible = true;
                btnSubmit.Text = "VerifyOTP";
                Session["OTP"] = dtMember.Rows[0]["loginpin"].ToString();
                txtOTP.Attributes.Add("placeholder", "Please enter your TPIN");
                ErrorShow.AlertMessage(page,"Please Enter Your TPIN For Add Bank Details", ConstantsData.CompanyName);
            }
            else
            {
                ErrorShow.Error(page1: page, Message: "Permission Not Allow");
            }
        }
        else if (btnSubmit.Text == "VerifyOTP")
        {
            if (Session["OTP"].ToString() == txtOTP.Text)
            {
                dt = cls.select_data_dt("exec AddEditMemberBankDetails 0,'" + Convert.ToInt32(hdnMsrno.Value) + "','" + Convert.ToInt32(ddlBankList.SelectedValue) + "','" + txtIfscCode.Text.Trim() + "','" + txtAccountNo.Text.Trim() + "','" + txtAccountHolderName.Text.Trim() + "','" + txtBranchName.Text.Trim() + "'");
                if (dt.Rows[0]["ID"].ToString() == "1")
                {
                    ErrorShow.AlertMessage(Page, "Successfully Added", ConstantsData.CompanyName);
                }

                if (dt.Rows.Count > 0)
                {
                    Label1.Text = dt.Rows[0]["Status"].ToString();
                    Label1.ForeColor = System.Drawing.Color.Green;
                    ConstantsData.Clear(page.Controls);
                    FillBankDetails();
                    btnSubmit.Enabled = true;
                }
            }
            else
            {
                ErrorShow.Error(page1: page, Message: "Enter Valid Data");
            }
        }
        
    }

  


    protected void txtIfscCode_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtIfscCode.Text))
        {
            string strBankResult = GetBankDetailsUsingIFSC.GetBankDetails(txtIfscCode.Text);
            if (strBankResult != "Not Found")
            {
                BankDetailsUsingIFSC bankDetailsUsingIFSC = JsonConvert.DeserializeObject<BankDetailsUsingIFSC>(strBankResult);
                txtBranchName.Text = lblBranch.InnerHtml = bankDetailsUsingIFSC.BRANCH;
                lblADDRESS.InnerHtml = bankDetailsUsingIFSC.ADDRESS;
                lblBANK.InnerHtml = bankDetailsUsingIFSC.BANK;
                // ddlBankList.SelectedItem.Text = bankDetailsUsingIFSC.BANK;

            }
        }
    }


    protected void rptBankData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            hdnID.Value = e.CommandArgument.ToString();
            EditBank(e.CommandArgument.ToString());
            txtIfscCode_TextChanged(source, e);

        }
    }
}