using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RechargeAPICommission : System.Web.UI.Page
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
                lblAddEdit.Text = "Update API Commission Structure";
                string APIName = ObjData.select_data_scalar_string("Select apiname from tblrecharge_api where apiid='" + Request.QueryString["id"].ToString() + "'");
                lblAddEdit.Text = lblAddEdit.Text + "----------------------------->" + APIName;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Key", String.Format("Dialog_Alert('{0}');", "Error|Invalid Request !|Dashboard.aspx"), true);
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "Key", String.Format("Dialog_Alert('{0}');", "Warning | Unable to modify !"), true);
            }
            else
            {
                //Int32 intresult = 0;
                //intresult = objAPI.AddEditAPI(Convert.ToInt32(Request.QueryString["id"]), txtAPIName.Text, txtURL.Text, txtSplitter.Text, txtprm1.Text, txtprm1val.Text, txtprm2.Text, txtprm2val.Text, txtprm3.Text, txtprm4.Text, txtprm5.Text, txtprm6.Text, txtprm7.Text, txtprm8.Text, txtprm9.Text, txtprm9val.Text, txtprm10.Text, txtprm10val.Text, txtTxIDPosition.Text, txtStatusPosition.Text, txtSuccess.Text, txtFailed.Text, txtPending.Text, txtOperatorRefPosition.Text, txtErrorCodePosition.Text, txtBalanceURL.Text, txtB_prm1.Text, txtB_prm1val.Text, txtB_prm2.Text, txtB_prm2val.Text, txtB_prm3.Text, txtB_prm3val.Text, txtB_prm4.Text, txtB_prm4val.Text, txtB_BalancePosition.Text, txtStatusURL.Text, txtS_prm1.Text, txtS_prm1val.Text, txtS_prm2.Text, txtS_prm2val.Text, txtS_prm3.Text, txtS_prm4.Text, txtS_StatusPosition.Text);
                //if (intresult > 0)
                //{
                int i = ObjData.delete_data("delete from tblRecharge_OperatorCode where APIID=" + Request.QueryString["id"].ToString());
                foreach (GridViewRow row in gvOperator.Rows)
                {
                    int OperatorID = Convert.ToInt32(row.Cells[0].Text);
                    TextBox txtOperatorCode = (TextBox)row.Cells[3].FindControl("txtOperatorCode");
                    TextBox txtCommission = (TextBox)row.Cells[4].FindControl("txtCommission");
                    CheckBox chkCommissionIsFlat = (CheckBox)row.Cells[4].FindControl("chkIsCommissionFlat");
                    TextBox txtSurcharge = (TextBox)row.Cells[5].FindControl("txtSurcharge");
                    CheckBox chkSurchargeIsFlat = (CheckBox)row.Cells[5].FindControl("chkIsSurchargeFlat");
                    int ii = ObjData.update_data("ProcRecharge_AddEditOperatorCode 0, '" + OperatorID + "','" + Convert.ToInt32(Request.QueryString["id"].ToString()) + "','" + txtOperatorCode.Text + "','" + Convert.ToDecimal(txtCommission.Text) + "', '" + chkCommissionIsFlat.Checked + "', '" + Convert.ToDecimal(txtSurcharge.Text) + "', '" + chkSurchargeIsFlat.Checked + "'");
                }


                ScriptManager.RegisterStartupScript(this, typeof(string), "Key", String.Format("Dialog_Alert('{0}');", "Success|Record updated successfully !|Dashboard.aspx"), true);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, typeof(string), "Key", String.Format("Dialog_Alert('{0}');", "Warning|API Already Exists !"), true);
                //}
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "Key", String.Format("Dialog_Alert('{0}');", "Error|Sorry for the inconvenience caused, Please enter valid values !"), true);
        }
    }

    public void fillOperator(int APIID)
    {
        dt = ObjData.select_data_dt("ProcRecharge_ManageOperatorCode 'GetByAPIID', " + APIID + "");
        gvOperator.DataSource = dt;
        gvOperator.DataBind();
    }
}