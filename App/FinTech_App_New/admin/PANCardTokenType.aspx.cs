using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PANCardTokenType : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();

    #region [PageLoad]

    #region [PagePreInit]

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["dtEmployee"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {

        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int cnt = cls.select_data_scalar_int("Select count(*) from tbl_PANTokenType");
            FillData();
        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int cnt = 0; //cls.select_data_scalar_int("Select count(*) from tbl_PANTokenType");
        if (cnt == 0)
        {
            #region [Insert]
            Int32 intresult = 0;
            intresult = cls.insert_data("update tbl_PANTokenType set PANFees=" + Convert.ToDecimal(txtFees.Text.Trim())+ " where PANCardID="+ddlPANType.SelectedValue+"");
            if (intresult > 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Key", String.Format("Dialog_Alert('{0}');", "Success|Record inserted successfully !"), true);
                txtFees.Text = "";
                FillData();
                ddlPANType.SelectedIndex = -1;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Key", String.Format("Dialog_Alert('{0}');", "Warning|Setting Already Exists !"), true);
            }
            #endregion
        }


    }
    #endregion

    #region [All Functions]

    private void FillData()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Select * from tbl_PANTokenType");

        if (dt.Rows.Count > 0)
        {
            rptData.DataSource = dt;
            rptData.DataBind();
        }
    }
    #endregion
    protected void ddlPANType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPANType.SelectedItem.Value == "2")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Key", String.Format("Dialog_Alert('{0}');", "Warning|Kindly Select Coupon Type !"), true);
            }


        }
        catch (Exception ex)
        {

        }
    }
}