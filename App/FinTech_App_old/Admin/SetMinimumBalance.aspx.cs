using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SetMinimumBalance : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();
    DataTable dt = new DataTable();
    int Val = 0;
    Page page;

    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            FillData();
        }
    }

    private void FillData()
    {
        try
        {
            dt = ObjData.select_data_dt("select * from tblmlm_MinimumBalance");
            if (dt.Rows.Count > 0)
            {
                txtDistributorMinBalance.Text = dt.Rows[0]["DistributorMinimumBAl"].ToString();
                txtMDMinBalance.Text = dt.Rows[0]["MdMinimumBal"].ToString();
                txtRetailerMinBalance.Text = dt.Rows[0]["retailerMinimumBal"].ToString();
                txtPartner.Text = dt.Rows[0]["PartnerMiniBalance"].ToString();
            }
            else
            {
                txtDistributorMinBalance.Text = "0";
                txtMDMinBalance.Text = "0";
                txtRetailerMinBalance.Text = "0";
            }
        }
        catch (Exception ex)
        {
            ErrorShow.Error(page, ex.InnerException.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            Val = ObjData.update_data("AddEditMinBalance " + txtMDMinBalance.Text + ",'" + txtDistributorMinBalance.Text + "','" + txtRetailerMinBalance.Text + "',0,'"+txtPartner.Text+"'");
            if (Val > 0)
            {
                FillData();
                ErrorShow.Success(page, "Balance Updated");

            }
            else
                ErrorShow.Error(page, "SomeThing goes wrong");

        }
        catch (Exception ex)
        {
            ErrorShow.Error(page, ex.InnerException.Message);
        }
    }

}