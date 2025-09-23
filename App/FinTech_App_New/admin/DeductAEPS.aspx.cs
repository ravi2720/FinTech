using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_DeductAEPS : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtEmployee;
    Page page;
    public Permission permission;
    ActionButtonPermission actionButtonPermission;
    Company company;

    protected void Page_Init(object sender, EventArgs e)
    {
        permission = Permission.GetPermission();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            company = Company.GetCompanyInfo();
            dtEmployee = (DataTable)SessionManager.CurrentSessionAdmin;
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.AddFund, dtEmployee.Rows[0]["RoleID"].ToString());
            if (!IsPostBack)
            {
                if (Request.QueryString["usr"] != null)
                {
                    BindData(Convert.ToInt32(Request.QueryString["usr"]));
                }
                else
                {
                    BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", dtEmployee.Rows[0]["ID"].ToString());
                }
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }



    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        multiview1.ActiveViewIndex = 1;
        DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERAEPSBALANCE where msrno = '" + Convert.ToInt16(ddlMember.SelectedValue) + "'");
        rptMember.DataSource = dtMember;
        rptMember.DataBind();
    }

    private void BindData(int msrno)
    {
        multiview1.ActiveViewIndex = 1;
        DataTable dtMember = cls.select_data_dt("select * from VIEW_MEMBERAEPSBALANCE where msrno = '" + msrno + "'");
        rptMember.DataSource = dtMember;
        rptMember.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;
        Label lblBalance = (Label)item.FindControl("lblBalance");
        Label lblName = (Label)item.FindControl("lblName");
        Label lblLoginid = (Label)item.FindControl("lblLoginid");
        Label lblAddress = (Label)item.FindControl("lblAddress");
        Label lblMobile = (Label)item.FindControl("lblMobile");
        Label lblEmail = (Label)item.FindControl("lblEmail");
        TextBox txtAmount = (TextBox)item.FindControl("txtAmount");
        TextBox txtRemark = (TextBox)item.FindControl("txtRemark");
        TextBox txtTransactionPIN = (TextBox)item.FindControl("txtTransactionPIN");

        try
        {
            if (!string.IsNullOrEmpty(txtAmount.Text) && Convert.ToDecimal(txtAmount.Text.Trim()) > 0 && !string.IsNullOrEmpty(txtTransactionPIN.Text))
            {
                if (txtTransactionPIN.Text == Session["OTPAddFund"].ToString())
                {
                    string GUID = "AEPS-" + System.Guid.NewGuid().ToString();
                    string Narration = $"{txtRemark.Text} with TransID-{GUID}";
                    DataTable dtval = cls.select_data_dt("exec PROC_AEPSWALLETTRANSACTION '" + lblLoginid.Text + "','" + txtAmount.Text + "','DR','" + Narration + "','" + txtRemark.Text + "'," + ConstantsData.SFundAdd + ",'" + GUID + "'");
                    if (dtval.Rows.Count > 0)
                    {
                        if (dtval.Rows[0]["MSRNO"].ToString() == "1")
                        {
                            Int32 Msrno = cls.select_data_scalar_int("select msrno from member where loginid='" + lblLoginid.Text + "'");
                            Activity.AddActivity(dtEmployee.Rows[0]["ID"].ToString(), ConstantsData.SFundAdd.ToString(), GUID, $"AEPS {txtAmount.Text} Amount Deduct By { dtEmployee.Rows[0]["Name"].ToString()}", Msrno.ToString(), "AEPS Amount Deduct", cls);
                            ErrorShow.AlertMessageWithRedirect(page, "Balance Deduct Successfully ..!!", "DeductAEPS.aspx", ConstantsData.CompanyName);
                        }
                        else
                        {
                            ErrorShow.Error(page1: page, Message: dtval.Rows[0]["STATUS"].ToString());
                        }
                    }
                    else
                    {
                        ErrorShow.Error(page1: page, Message: "Some Error Occurred , Try Again ..!!");
                    }
                }
                else
                {
                    txtTransactionPIN.Text = "";
                    txtTransactionPIN.BorderColor = System.Drawing.Color.Red;
                    ErrorShow.Error(page1: page, Message: "Wrong Transaction PIN");
                }
            }
            else
            {
                ErrorShow.Error(page1: page, Message: "Amount must be greater then Zero ..");
            }
        }
        catch (Exception)
        {
            ErrorShow.Error(page1: page, Message: "Something went wrong ..");
        }
    }


    protected void rptMember_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (actionButtonPermission.Submit)
        {
            Button btnSendSMS = (Button)e.Item.FindControl("btnSendSMS");
            Button btnverify = (Button)e.Item.FindControl("btnverify");
            Button btnSubmit = (Button)e.Item.FindControl("btnSubmit");
            TextBox txtTransactionPIN = (TextBox)e.Item.FindControl("txtTransactionPIN");
            if (e.CommandName == "SendOTP")
            {
                if (btnSendSMS.Text == "Send OTP")
                {
                    if (permission.AddBalanceOTP)
                    {
                        if (dtEmployee.Rows[0]["Mobile"].ToString().Length == 10)
                        {
                            Random generator = new Random();
                            int r = generator.Next(100000, 999999);
                            string OTP = r.ToString();
                            string Message = "Login OTP is : " + OTP;
                            Session["OTPAddFund"] = OTP;
                            btnSendSMS.Visible = false;
                            btnverify.Visible = true;
                            string[] ValueArray = new string[4];
                            ValueArray[0] = "User";
                            ValueArray[1] = OTP;
                            string str = SMS.SendWithV(dtEmployee.Rows[0]["Mobile"].ToString(), ConstantsData.Admin_Fund_Add_OTP_SMS, ValueArray, company.MemberID);
                            ErrorShow.AlertMessage(page, "OTP send on " + dtEmployee.Rows[0]["Mobile"].ToString() + " your mobile no", ConstantsData.CompanyName);
                        }
                        else
                        {
                            ErrorShow.Error(page1: page, Message: "Mobile Number not 10 digit so provide valid number");
                            btnSubmit.Enabled = false;
                        }
                    }
                    else if (permission.AddBalanceTPIN)
                    {
                        btnSendSMS.Visible = false;
                        btnverify.Visible = true;
                        Session["OTPAddFund"] = dtEmployee.Rows[0]["TransactionPassword"].ToString();
                        txtTransactionPIN.Attributes.Add("placeholder", "Please enter your TPIN");
                        ErrorShow.AlertMessage(page, "PLease Enter Admin Trasnaction Pin", ConstantsData.CompanyName);
                    }
                    else
                    {
                        ErrorShow.Error(page1: page, Message: "Permission Not Allow");
                    }
                }
            }
            if (e.CommandName == "Verify")
            {
                if (!string.IsNullOrEmpty(Session["OTPAddFund"].ToString()) && !string.IsNullOrEmpty(txtTransactionPIN.Text))
                {
                    if (Session["OTPAddFund"].ToString() == txtTransactionPIN.Text)
                    {
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        ErrorShow.Error(page1: page, Message: "Wrong Transaction PIN");
                    }
                }
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Contact to Admin. Not Permission For Add Fund", ConstantsData.CompanyName);
        }
    }
}