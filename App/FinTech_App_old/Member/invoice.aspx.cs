using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_invoice : System.Web.UI.Page
{

    cls_connection cls = new cls_connection();
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

            GetBindData();
            GetBindGST();

        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlM);
        }
    }


    private void GetBindGST()
    {
        DataTable dtData = cls.select_data_dt("select * from View_GSTInvoiceSetting");
        if (dtMember.Rows.Count > 0)
        {
          
            rptdata.DataSource = dtData;
            rptdata.DataBind();
        }
    }



    private void GetBindData()
    {
        DataTable dtData = cls.select_data_dt("GetBillByMonth '" + dtMember.Rows[0]["Msrno"].ToString() + "','2022-12'");
        if (dtMember.Rows.Count > 0)
        {
            //mmbri.InnerHtml = dtData.Rows[0]["LoginiD"].ToString();
            rptDataRecharge.DataSource = dtData;
            rptDataRecharge.DataBind();
        }
    }

    [WebMethod]
    public static string GetInvoiceNumber(string Auth, string InvoiceMonth)
    {
        cls_connection cls = new cls_connection();
        DataTable dtData = cls.select_data_dt("AddEditInvoiceListNumber '" + Auth + "','" + InvoiceMonth + "',''");
        if (dtData.Rows.Count > 0)
        {
            return dtData.Rows[0]["InvoiceNo"].ToString();
        }
        else
        {
            return "";
        }
    }


}