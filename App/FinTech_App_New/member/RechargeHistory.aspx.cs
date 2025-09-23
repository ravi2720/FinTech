using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_RechargeHistory : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();
    public Company company;
    DataTable dt = new DataTable();
    int Val = 0;
    DataTable dtMember;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            company = Company.GetCompanyInfo();
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                txtFromDate.Text = txtToDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                BindDropDown.FillDropDown(ddlMember, "MemberList", "LoginID", "ID", "Select Member", dtMember.Rows[0]["ID"].ToString());
                BindDropDown.FillDropDown(ddlService, "ActiveServiceBySection", "Name", "ID", "Select Service", ConstantsData.RechargeServiceCategory.ToString());
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlM);
        }
    }

    protected void btnTicket_Click(object sender, EventArgs e)
    {
        dt = ObjData.select_data_dt("PROC_MANAGE_RECHARGE_HISTORY 'BYID'," + hdnServiceID.Value + "");
        if (dt.Rows.Count > 0)
        {
            int Val = ObjData.update_data("EXEC Proc_AddEditComplainTicket 0," + dt.Rows[0]["ServiceID"].ToString() + ",'" + dtMember.Rows[0]["msrno"] + "','" + txtMessage.Text + "'," + company.MemberID + ",'" + dt.Rows[0]["TransID"].ToString() + "'");
            if (Val > 0)
            {
                ErrorShow.Success(page: page, Message: "Record Inserted Successfully");
                ConstantsData.Clear(page.Controls);
            }
            else
            {
                ErrorShow.Error(page1: page, Message: "Record Not Inserted, Try Again ..");
            }
        }
    }

    protected void rptDataRecharge_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Dispute")
        {
            DataTable dthis = ObjData.select_data_dt("select * from tblRecharge_History where id='" + e.CommandArgument.ToString() + "' and Status='Success' and Dispute=0 and ReSolve=0");
            if (dthis.Rows.Count > 0)
            {
                ObjData.update_data("update tblRecharge_History set Dispute=1 where id='" + e.CommandArgument.ToString() + "' and Status='Success'");
                FillTransactions();
                ErrorShow.SuccessNotify(page, "Dispute Apply Successfully");
            }
            else
            {
                ErrorShow.ErrorNotify(page, "Dispute Alredy Applied");

            }
        }
        else if (e.CommandName == "c")
        {
            dt = ObjData.select_data_dt("PROC_MANAGE_RECHARGE_HISTORY 'BYID'," + e.CommandArgument.ToString() + "");
            if (dt.Rows.Count > 0)
            {
                hdnServiceID.Value = e.CommandArgument.ToString();
                rptDataOneRow.DataSource = dt;
                rptDataOneRow.DataBind();
            }
            else
            {
                rptDataOneRow.DataSource = null;
                rptDataOneRow.DataBind();
            }
        }
    }


    private void FillTransactions()
    {

        dt = ObjData.select_data_dt("PROC_MANAGE_RECHARGE_HISTORY 'admin'," + ddlService.SelectedValue + ",'" + txtSeach.Text + "','" + txtFromDate.Text + "','" + txtToDate.Text + "','" + ddlStatus.SelectedValue + "'," + dtMember.Rows[0]["ID"].ToString() + "," + ddlMember.SelectedValue + "");
        if (dt.Rows.Count > 0)
        {
            rptDataRecharge.DataSource = dt;
            rptDataRecharge.DataBind();

            
        }
        else
        {
            rptDataRecharge.DataSource = null;
            rptDataRecharge.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillTransactions();
    }


   
}