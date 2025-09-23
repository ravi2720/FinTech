using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Member_AddFund : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    Page page;
    DataTable dtMemberP;
    public MemberPermission permission;
    public Company company;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            dtMemberP = (DataTable)Session["dtMember"];
            company = Company.GetCompanyInfo();
            page = HttpContext.Current.CurrentHandler as Page;
            permission = MemberPermission.GetPermission(dtMemberP.Rows[0]["Msrno"].ToString());
            if (!IsPostBack)
            {
                if (Request.QueryString["usr"] != null)
                {
                    BindData(Convert.ToInt32(Request.QueryString["usr"]));
                }
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void BindData(int msrno)
    {
        multiview1.ActiveViewIndex = 0;
        DataTable dtMember = cls.select_data_dt("select * from View_MemberBalance where msrno = '" + msrno + "' and parentID=" + dtMemberP.Rows[0]["msrno"].ToString() + "");
        rptMember.DataSource = dtMember;
        rptMember.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        RepeaterItem item = (sender as Button).NamingContainer as RepeaterItem;

        Button clbtn = (Button)item.FindControl("btnSubmit");
        Label lblBalance = (Label)item.FindControl("lblBalance");
        Label lblName = (Label)item.FindControl("lblName");
        Label lblLoginid = (Label)item.FindControl("lblLoginid");
        Label lblAddress = (Label)item.FindControl("lblAddress");
        Label lblMobile = (Label)item.FindControl("lblMobile");
        Label lblEmail = (Label)item.FindControl("lblEmail");
        TextBox txtAmount = (TextBox)item.FindControl("txtAmount");
        TextBox txtRemark = (TextBox)item.FindControl("txtRemark");
        HtmlTableRow rowhide = item.FindControl("rowhide") as HtmlTableRow;
        TextBox txtOTP = (TextBox)item.FindControl("txtOTP");
        Label lblAuth = (Label)item.FindControl("lblAuth");

        if (clbtn.Text == "Submit")
        {
            
            var iy = Common.CheckAuthForTransaction(permission, txtOTP, lblAuth, dtMemberP.Rows[0]["Mobile"].ToString(), company.MemberID.ToString(), Page);
            if (iy.Item1 == true)
            {
                clbtn.Text = "Verify";
                rowhide.Visible = true;
                return;
            }
            

        }
        else
        {
            if (!Common.CheckVAerifyAuth(permission, txtOTP.Text, dtMemberP.Rows[0]["LoginPin"].ToString(), Page))
            {
                return;
            }
        }

        try
        {
            decimal MyBalance = 0;

            DataTable dtWalletBalance = new DataTable();
            dtWalletBalance = cls.select_data_dt("select * from VIEW_MEMBERBALANCE where Msrno=" + dtMemberP.Rows[0]["Msrno"] + "");
            if (dtWalletBalance.Rows.Count > 0)
            {
                MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["Balance"]);
            }

            if (!string.IsNullOrEmpty(txtAmount.Text) && Convert.ToInt32(txtAmount.Text.Trim()) > 0)
            {
                if (MyBalance > Convert.ToInt32(txtAmount.Text.Trim()))
                {
                    DataTable dtWall = new DataTable();
                    string nnn = System.Guid.NewGuid().ToString();

                    dtWall = cls.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMemberP.Rows[0]["LoginID"].ToString() + "','" + txtAmount.Text + "','Dr','Fund Transfer TransID-" + nnn + "','Fund Transfer To " + lblLoginid.Text + " (" + txtRemark.Text + ")','" + ConstantsData.SFundAdd + "','" + nnn + "'");
                    if (dtWall.Rows[0]["MSRNO"].ToString() == "1")
                    {
                        dtWall = cls.select_data_dt("exec PROC_EWALLETTRANSACTION '" + lblLoginid.Text + "','" + txtAmount.Text + "','Cr','Fund Recevied TransID-" + nnn + "','Fund Recevied From " + dtMemberP.Rows[0]["LoginID"].ToString() + "','" + ConstantsData.SFundAdd + "','" + nnn + "'");
                        if (dtWall.Rows[0]["MSRNO"].ToString() == "1")
                        {
                            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('Balance Added Successfully ..!!');window.location.replace('BalanceTransfer.aspx')", true);
                            //ErrorShow.Success(page: page, Message: "Balance Added Successfully ..!!");
                            //txtAmount.Text = txtRemark.Text = "";
                        }
                        else
                        {
                            ErrorShow.Error(page1: page, Message: "Some Error Occurred , Try Again ..!!");
                        }
                    }
                    else
                    {
                        ErrorShow.Error(page1: page, Message: "Low Balance");
                    }
                }
                else
                {
                    ErrorShow.Error(page1: page, Message: "Low Balance");
                }
            }
            else
            {
                ErrorShow.Error(page1: page, Message: "Amount must be greater then Zero ..");
            }
        }
        catch (Exception ex)
        {
            ErrorShow.Error(page1: page, Message: "Something went wrong ..");
        }
    }

}