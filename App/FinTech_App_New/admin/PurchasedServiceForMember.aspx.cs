using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PurchasedServiceForMember : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt;
    DataTable dtEmployee = new DataTable();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtEmployee"] != null)
        {
            dtEmployee = (DataTable)Session["dtEmployee"];
            page = HttpContext.Current.CurrentHandler as Page;
            if (!IsPostBack)
            {
                FillMember();
              //BindData();
            }
        }
    }

    private void FillMember()
    {
        DataTable dtMember = ObjConnection.select_data_dt("select (LoginID + ' ( ' + Name + ' ) ')as Name,msrno from Member where isactive=1");
        Common.BindDropDown(ddlMember, dtMember, "Name", "msrno");
        ddlMember.Items.Insert(0, new ListItem("-- Select Member --", "0"));
    }

    private void BindData()
    {
        dt = ObjConnection.select_data_dt("PROC_Active_service " + ddlMember.SelectedValue + "");
        if (dt.Rows.Count > 0)
        {
            rptadata.DataSource = dt;
            rptadata.DataBind();
        }
        else
        {
            rptadata.DataSource = null;
            rptadata.DataBind();
        }
    }

    protected void rptadata_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Purchase")
        {
            int memberId = Convert.ToInt32(ddlMember.SelectedValue);
            int serviceId = Convert.ToInt32(e.CommandArgument.ToString());

            // Get service price & name
            double price = ObjConnection.select_data_scalar_double($"SELECT price FROM Service WHERE id = {serviceId}");
            string serviceName = ObjConnection.select_data_scalar_string($"SELECT name FROM Service WHERE id = {serviceId}");

            // Get current wallet balance
            double myBalance = 0;
            DataTable dtWalletBalance = ObjConnection.select_data_dt($"EXEC ProcMLM_ManageEWalletBalance 'GetBalanceByMsrNo', {memberId}");
            if (dtWalletBalance.Rows.Count > 0)
                myBalance = Convert.ToDouble(dtWalletBalance.Rows[0]["Balance"]);

            // Proceed only if balance is sufficient or service is free
            if (myBalance >= price && price > 0)
            {
                string referenceNumber = Guid.NewGuid().ToString().Replace("-", "0");
                string narration = $"Service Active TransID-{referenceNumber}";
                string loginId = ObjConnection.select_data_scalar_string($"SELECT loginid FROM Member WHERE msrno = {memberId}");

                // Debit wallet
                DataTable dtPayment = ObjConnection.select_data_dt($"EXEC PROC_EWALLETTRANSACTION '{loginId}', '{price}', 'DR', '{narration}'");
                if (dtPayment.Rows[0]["msrno"].ToString() == "1")
                {
                    ActivateService(memberId, serviceId, price, serviceName);
                }
            }
            else if (price == 0)
            {
                // Free service activation
                ActivateService(memberId, serviceId, 0, serviceName);
            }
            else
            {
                ErrorShow.AlertMessage(Page, "Low Balance", "Transaction Failed");
            }
        }
    }

    private void ActivateService(int memberId, int serviceId, double price, string serviceName)
    {
        string currentServices = ObjConnection.select_data_scalar_string($"SELECT SERVICE FROM MEMBERSERVICE WHERE msrno = {memberId}");
        string updatedServices = string.IsNullOrEmpty(currentServices) ? serviceId.ToString() : $"{currentServices},{serviceId}";

        int val = ObjConnection.update_data($"PROC_ASSIGN_SERVICE '{updatedServices}', {memberId}");
        if (val > 0)
        {
            ObjConnection.update_data($"AddEditServiceActiveByAdmin {memberId}, '{serviceId}', {price}, {dtEmployee.Rows[0]["ID"]}");

            ErrorShow.AlertMessage(Page, $"{serviceName} Service Activated Successfully", "Success");
            BindData();
        }
        else
        {
            ErrorShow.AlertMessage(Page, "Failed to activate service.", "Error");
        }
    }


    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }
}