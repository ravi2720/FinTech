using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddAPIOperatorCode : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {               
                fillOperator(Convert.ToInt32(Request.QueryString["id"]));
                hdnAPIID.Value = Request.QueryString["id"].ToString();


            }
            else
            {
                fillOperator(0);
               
            }
        }
    }

    public void fillOperator(int APIID)
    {
        dt = ObjData.select_data_dt("ProcRecharge_ManageOperatorCode 'GetByAPIID', "+APIID+"");
        gvOperator.DataSource = dt;
        gvOperator.DataBind();
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvOperator.Rows)
            {
                int OperatorID = Convert.ToInt32(row.Cells[0].Text);
                TextBox txtOperatorCode = (TextBox)row.Cells[3].FindControl("txtOperatorCode");
                TextBox txtCommission = (TextBox)row.Cells[4].FindControl("txtCommission");
                CheckBox chkCommissionIsFlat = (CheckBox)row.Cells[4].FindControl("chkCommissionIsFlat");
                TextBox txtSurcharge = (TextBox)row.Cells[5].FindControl("txtSurcharge");
                CheckBox chkSurchargeIsFlat = (CheckBox)row.Cells[5].FindControl("chkSurchargeIsFlat");
                int ii = ObjData.update_data("ProcRecharge_AddEditOperatorCode 0,'" + OperatorID + "','" + hdnAPIID.Value + "','" + txtOperatorCode.Text + "','" + Convert.ToDecimal(txtCommission.Text) + "','" + chkCommissionIsFlat.Checked + "','" + Convert.ToDecimal(txtSurcharge.Text) + "', '" + chkSurchargeIsFlat.Checked + "'");
            }
            hdnAPIID.Value = "0";
        }
        catch (Exception ex)
        {
            ErrorShow.Error(Page, ex.InnerException.Message);
        }
    }
}