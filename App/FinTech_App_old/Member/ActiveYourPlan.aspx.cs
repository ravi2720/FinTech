using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection;

public partial class Member_ActiveYourPlan : System.Web.UI.Page
{
    DataTable dtMember;
    cls_connection cls = new cls_connection();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                fillTransactionDetails();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlM);
        }
    }

    private void fillTransactionDetails()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Exec ManageCreatePlan 'GetActive',0,"+dtMember.Rows[0]["Msrno"].ToString()+"");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillTransactionDetails();
    }



    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Plan")
        {
            DataTable dthis = cls.select_data_dt("ManageCreatePlan  'GetActive'," + e.CommandArgument.ToString() + ","+dtMember.Rows[0]["Msrno"].ToString()+"");
            if (dthis.Rows.Count > 0)
            {
                if (dthis.Rows[0]["PurchaseOrnot"].ToString() == "0")
                {
                    double MyBalance = 0;
                    DataTable dtWalletBalance = new DataTable();
                    dtWalletBalance = cls.select_data_dt("Exec PROC_WALLET_REPORT 'EwalletBalance', " + Convert.ToInt32(dtMember.Rows[0]["Msrno"]));
                    if (dtWalletBalance.Rows.Count > 0)
                    {
                        MyBalance = Convert.ToDouble(dtWalletBalance.Rows[0]["Balance"]);
                    }
                    if (MyBalance >= Convert.ToDouble(dthis.Rows[0]["Amount"]) && Convert.ToDouble(dthis.Rows[0]["Amount"]) > 0)
                    {
                        string reference_number = System.Guid.NewGuid().ToString().Replace('-', '0');
                        string Narration = "Service Active TransID-" + reference_number;
                        string Description = "" + dthis.Rows[0]["Name"].ToString() + " Plan activated With Service Charge-" + dthis.Rows[0]["Amount"].ToString() + "";
                        Int32 Val = 0;
                        DataTable dtPayment = cls.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"].ToString() + "','" + dthis.Rows[0]["Amount"].ToString() + "','DR','" + Narration + "','" + Description + "','" + ConstantsData.SPlanPurchase + "','" + reference_number + "'");
                        if (dtPayment.Rows.Count > 0)
                        {
                            if (dtPayment.Rows[0]["msrno"].ToString() == "1")
                            {
                                Val = cls.update_data("AddEditUpgradeHistory " + dthis.Rows[0]["ID"] + "," + dthis.Rows[0]["Amount"] + ",'" + reference_number + "'," + dtMember.Rows[0]["Msrno"].ToString() + "");
                                if (Val > 0)
                                {
                                    if (dtPayment.Rows[0]["msrno"].ToString() == "1")
                                    {

                                        Val = cls.update_data("update Member set PackageID='" + dthis.Rows[0]["PackageID"].ToString() + "' where msrno=" + Convert.ToInt32(dtMember.Rows[0]["Msrno"]) + "");
                                        if (Val > 0)
                                        {
                                            ErrorShow.AlertMessage(page, "" + dthis.Rows[0]["Name"].ToString() + " Plan Active Successfully", ConstantsData.CompanyName);
                                        }

                                    }
                                }
                                else
                                {
                                    ErrorShow.SuccessNotify(page, "Plan Alreday Purchase");
                                }
                            }
                            else
                            {
                                ErrorShow.ErrorNotify(page, "Low Balance");
                            }
                        }
                        else
                        {
                            ErrorShow.ErrorNotify(page, "Low Balance");
                        }
                    }
                    else
                    {
                        
                        ErrorShow.ErrorNotify(page, "Low Balance");
                    }
                }
                else
                {
                    ErrorShow.SuccessNotify(page, "Plan Alreday Purchase");
                }
            }
            else
            {

            }
        }
    }
}
