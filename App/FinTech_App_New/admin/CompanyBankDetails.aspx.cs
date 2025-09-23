using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CompanyBankDetails : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember;
    int Val = 0;
    Page page;
    Company company;

    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            company = Company.GetCompanyInfo();
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlBankList, "ActiveBankList", "Name", "ID", "Select Bank");
                GetCompanyBankDetails();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }



    public void GetCompanyBankDetails()
    {

        dt = ObjConnection.select_data_dt("exec ManageCompanyBankDetails 'GetAllBYMemberID','"+company.MemberID+"'");
        if (dt.Rows.Count > 0)
        {
            rptData.DataSource = dt;
            rptData.DataBind();
        }
        else
        {
            rptData.DataSource = null;
            rptData.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBankList.SelectedIndex > 0)
            {

                string gdFront = Guid.NewGuid().ToString() + ".png";
                string gdFront2 = Guid.NewGuid().ToString() + ".jpeg";
                if (flieUpload.HasFile)
                {
                    flieUpload.PostedFile.SaveAs(MapPath("./images/") + gdFront);
                }

                if (FileUploadBanklogo.HasFile)
                {
                    FileUploadBanklogo.PostedFile.SaveAs(MapPath("./images/") + gdFront2);
                }

                Val = ObjConnection.update_data("AddEditCompanyBankDetails " + Convert.ToInt32(hdnID.Value) + ",'" + Convert.ToInt32(dtMember.Rows[0]["ID"].ToString()) + "'," + company.MemberID + ",'" + ddlBankList.SelectedValue + "','" + ddlBankList.SelectedItem.Text + "','" + txtbranch.Text + "','" + txtAccountHolder.Text + "','" + txtAccountNumber.Text + "','" + txtIFSCCode.Text + "','" + txtBillinginfo.Text + "','" + txtCashDepositeCharge.Text + "','" + gdFront + "','" + gdFront2 + "'");
                if (Val > 0)
                {
                    GetCompanyBankDetails();
                    ConstantsData.Clear(page.Controls);
                    ErrorShow.AlertMessage(page, "Add Successfully.", ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Fill Required Data.", ConstantsData.CompanyName);
            }

        }
        catch (Exception ex)
        {
            ErrorShow.Error(Page, ex.InnerException.Message);
        }
    }

    protected void txtIfscCode_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtIFSCCode.Text))
        {
            string strBankResult = GetBankDetailsUsingIFSC.GetBankDetails(txtIFSCCode.Text);
            if (strBankResult != "Not Found")
            {
                BankDetailsUsingIFSC bankDetailsUsingIFSC = JsonConvert.DeserializeObject<BankDetailsUsingIFSC>(strBankResult);
                txtbranch.Text = lblBranch.InnerHtml = bankDetailsUsingIFSC.BRANCH;
                lblADDRESS.InnerHtml = bankDetailsUsingIFSC.ADDRESS;
                lblBANK.InnerHtml = bankDetailsUsingIFSC.BANK;
               
                ddlBankList.SelectedItem.Text = bankDetailsUsingIFSC.BANK;
            }
        }
    }

    private Bitmap Resize_Image(Stream streamImage, int maxWidth, int maxHeight)
    {
        Bitmap originalImage = new Bitmap(streamImage);
        int newWidth = originalImage.Width;
        int newHeight = originalImage.Height;
        double aspectRatio = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height);

        newWidth = maxWidth;
        newHeight = maxHeight;
        return new Bitmap(originalImage, newWidth, newHeight);
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
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
        dt = ObjConnection.select_data_dt("ManageCompanyBankDetails @Action='GetAll',@ID=" + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnID.Value = ID;
            ddlBankList.SelectedValue = dt.Rows[0]["BankID"].ToString();
            txtbranch.Text = dt.Rows[0]["BranchName"].ToString();
            txtAccountHolder.Text = dt.Rows[0]["AccountHolderName"].ToString();
            txtAccountNumber.Text = dt.Rows[0]["AccountNumber"].ToString();
            txtIFSCCode.Text = dt.Rows[0]["IFSCCode"].ToString();
            txtBillinginfo.Text = dt.Rows[0]["Billinginfo"].ToString();
            txtCashDepositeCharge.Text = dt.Rows[0]["Cashdepositecharge"].ToString();


        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageCompanyBankDetails @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            GetCompanyBankDetails();
        }
    }

    private void Clear()
    {
        txtAccountHolder.Text = txtAccountNumber.Text = txtbranch.Text = txtCashDepositeCharge.Text = txtAccountHolder.Text = txtIFSCCode.Text = txtBillinginfo.Text = "";
        ddlBankList.SelectedIndex = 0;
    }
}