using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddAPI : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                FillData(Convert.ToInt32(Request.QueryString["id"]));
                fillOperator(Convert.ToInt32(Request.QueryString["id"]));
               // fillCircle(Convert.ToInt32(Request.QueryString["id"]));
                //lblAddEdit.Text = "Update API";
            }
            else
            {
                fillOperator(0);
               // fillCircle(0);
               // lblAddEdit.Text = "Add API";
            }                     
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["id"] == null)
            {
                Int32 intresult = 0;
                intresult = ObjData.update_data("ProcRecharge_AddEditAPI 0,'" + txtAPIName.Text + "','" + txtURL.Text + "','" + txtSplitter.Text + "','" + txtprm1.Text.Trim() + "', '" + txtprm1val.Text.Trim() + "', '" + txtprm2.Text.Trim() + "','" + txtprm2val.Text.Trim() + "','" + txtprm3.Text + "', '" + txtprm4.Text + "','" + txtprm5.Text + "','" + txtprm6.Text + "','" + txtprm7.Text + "','" + txtprm8.Text + "','" + txtprm9.Text + "','" + txtprm9val.Text + "','" + txtprm10.Text + "','" + txtprm10val.Text + "','" + txtTxIDPosition.Text + "','" + txtStatusPosition.Text + "','" + txtSuccess.Text + "','" + txtFailed.Text + "','" + txtPending.Text + "','" + txtOperatorRefPosition.Text + "','" + txtErrorCodePosition.Text + "','" + txtBalanceURL.Text + "','" + txtB_prm1.Text + "','" + txtB_prm1val.Text + "','" + txtB_prm2.Text + "','" + txtB_prm2val.Text + "','" + txtB_prm3.Text + "','" + txtB_prm3val.Text + "','" + txtB_prm4.Text + "','" + txtB_prm4val.Text + "','" + txtB_BalancePosition.Text + "','" + txtStatusURL.Text + "','" + txtS_prm1.Text + "','" + txtS_prm1val.Text + "','" + txtS_prm2.Text + "','" + txtS_prm2val.Text + "','" + txtS_prm3.Text + "','" + txtS_prm4.Text + "','" + txtS_StatusPosition.Text + "','" + txtS_OpratrRefPosition.Text.Trim() + "', '" + txtS_ApiTransPosition.Text.Trim() + "'");
                if (intresult > 0)
                {
                    foreach (RepeaterItem item in gvOperator.Items)
                    {
                        HiddenField hdnOperatorID = (HiddenField)item.FindControl("hdnOperatorID") ;
                        Int32 OperatorID = hdnOperatorID.Value.ToInt32();
                        TextBox txtOperatorCode = (TextBox)item.FindControl("txtOperatorCode");
                        TextBox txtCommission = (TextBox)item.FindControl("txtCommission");
                        CheckBox chkCommissionIsFlat = (CheckBox)item.FindControl("chkCommissionIsFlat");
                        TextBox txtSurcharge = (TextBox)item.FindControl("txtSurcharge");
                        CheckBox chkSurchargeIsFlat = (CheckBox)item.FindControl("chkSurchargeIsFlat");
                        int ii = ObjData.update_data("ProcRecharge_AddEditOperatorCode 0,'" + OperatorID + "','" + intresult + "','" + txtOperatorCode.Text + "','" + Convert.ToDecimal(txtCommission.Text) + "','" + chkCommissionIsFlat.Checked + "','" + Convert.ToDecimal(txtSurcharge.Text) + "', '" + chkSurchargeIsFlat.Checked + "'");

                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Record inserted successfully !');location.replace('Recharge_listapicyindia.aspx');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Warning|API Already Exists !');", true);
                }
            }
            else
            {
                Int32 intresult = 0;

                intresult = ObjData.update_data("ProcRecharge_AddEditAPI " + Convert.ToInt32(Request.QueryString["id"]) + ",'" + txtAPIName.Text + "','" + txtURL.Text + "','" + txtSplitter.Text + "','" + txtprm1.Text.Trim() + "', '" + txtprm1val.Text.Trim() + "', '" + txtprm2.Text.Trim() + "','" + txtprm2val.Text.Trim() + "','" + txtprm3.Text + "', '" + txtprm4.Text + "','" + txtprm5.Text + "','" + txtprm6.Text + "','" + txtprm7.Text + "','" + txtprm8.Text + "','" + txtprm9.Text + "','" + txtprm9val.Text + "','" + txtprm10.Text + "','" + txtprm10val.Text + "','" + txtTxIDPosition.Text + "','" + txtStatusPosition.Text + "','" + txtSuccess.Text + "','" + txtFailed.Text + "','" + txtPending.Text + "','" + txtOperatorRefPosition.Text + "','" + txtErrorCodePosition.Text + "','" + txtBalanceURL.Text + "','" + txtB_prm1.Text + "','" + txtB_prm1val.Text + "','" + txtB_prm2.Text + "','" + txtB_prm2val.Text + "','" + txtB_prm3.Text + "','" + txtB_prm3val.Text + "','" + txtB_prm4.Text + "','" + txtB_prm4val.Text + "','" + txtB_BalancePosition.Text + "','" + txtStatusURL.Text + "','" + txtS_prm1.Text + "','" + txtS_prm1val.Text + "','" + txtS_prm2.Text + "','" + txtS_prm2val.Text + "','" + txtS_prm3.Text + "','" + txtS_prm4.Text + "','" + txtS_StatusPosition.Text + "','" + txtS_OpratrRefPosition.Text.Trim() + "', '" + txtS_ApiTransPosition.Text.Trim() + "'");
                if (intresult > 0)
                {
                    int i = ObjData.delete_data("delete from tblRecharge_OperatorCode where APIID=" + Request.QueryString["id"]);
                    foreach (RepeaterItem item in gvOperator.Items)
                    {
                        // int OperatorID = Convert.ToInt32(row.Cells[0].Text);
                        TextBox txtOperatorCode = (TextBox)item.FindControl("txtOperatorCode");
                        HiddenField hdnOperatorID = (HiddenField)item.FindControl("hdnOperatorID");
                        TextBox txtCommission = (TextBox)item.FindControl("txtCommission");
                        CheckBox chkCommissionIsFlat = (CheckBox)item.FindControl("chkCommissionIsFlat");
                        TextBox txtSurcharge = (TextBox)item.FindControl("txtSurcharge");
                        CheckBox chkSurchargeIsFlat = (CheckBox)item.FindControl("chkSurchargeIsFlat");
                        int ii = ObjData.update_data("ProcRecharge_AddEditOperatorCode 0,'" + hdnOperatorID.Value + "','" + Convert.ToInt32(Request.QueryString["id"]) + "','" + txtOperatorCode.Text + "','" + Convert.ToDecimal(txtCommission.Text) + "','" + chkCommissionIsFlat.Checked + "','" + Convert.ToDecimal(txtSurcharge.Text) + "', '" + chkSurchargeIsFlat.Checked + "'");

                    }

                    //int j = objconnection.delete_data("delete from tblRecharge_CircleCode where APIID=" + intresult);
                    //foreach (GridViewRow row in gvCircle.Rows)
                    //{
                    //    int CircleID = Convert.ToInt32(row.Cells[0].Text);
                    //    TextBox txtOperatorCode = (TextBox)row.Cells[2].FindControl("txtOperatorCode");
                    //    int jj = objCircleCode.AddEditCircleCode(0, CircleID, intresult, txtOperatorCode.Text);
                    //}

                    //clear();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('Email details updated successfully!');location.replace('EmailSettings.aspx');", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Record updated successfully !');location.replace('ListAPI.aspx');", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Record updated successfully !');", true);
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|Sorry for the inconvenience caused, Please enter valid values !');", true);
        }
    }
    public void fillOperator(int APIID)
    {
        dt = ObjData.select_data_dt("ProcRecharge_ManageOperatorCode 'GetByAPIID', " + APIID + "");
        gvOperator.DataSource = dt;
        gvOperator.DataBind();
    }
    private void FillData(int id)
    {
        DataTable dt = new DataTable();
        dt = ObjData.select_data_dt("ProcRecharge_ManageAPI 'GetAll',  " + Convert.ToInt32(id) + ""); 
        if (dt.Rows.Count > 0)
        {
            txtAPIName.Text = Convert.ToString(dt.Rows[0]["APIName"]);
            txtAPIName.Text = Convert.ToString(dt.Rows[0]["APIName"]);
            txtURL.Text = Convert.ToString(dt.Rows[0]["URL"]);
            txtSplitter.Text = Convert.ToString(dt.Rows[0]["Splitter"]);
            txtprm1.Text = Convert.ToString(dt.Rows[0]["prm1"]);
            txtprm1val.Text = Convert.ToString(dt.Rows[0]["prm1val"]);
            txtprm2.Text = Convert.ToString(dt.Rows[0]["prm2"]);
            txtprm2val.Text = Convert.ToString(dt.Rows[0]["prm2val"]);
            txtprm3.Text = Convert.ToString(dt.Rows[0]["prm3"]);
            txtprm4.Text = Convert.ToString(dt.Rows[0]["prm4"]);
            txtprm5.Text = Convert.ToString(dt.Rows[0]["prm5"]);
            txtprm6.Text = Convert.ToString(dt.Rows[0]["prm6"]);
            txtprm7.Text = Convert.ToString(dt.Rows[0]["prm7"]);
            txtprm8.Text = Convert.ToString(dt.Rows[0]["prm8"]);
            txtprm9.Text = Convert.ToString(dt.Rows[0]["prm9"]);
            txtprm9val.Text = Convert.ToString(dt.Rows[0]["prm9val"]);
            txtprm10.Text = Convert.ToString(dt.Rows[0]["prm10"]);
            txtprm10val.Text = Convert.ToString(dt.Rows[0]["prm10val"]);
            txtTxIDPosition.Text = Convert.ToString(dt.Rows[0]["TxIDPosition"]);
            txtStatusPosition.Text = Convert.ToString(dt.Rows[0]["StatusPosition"]);
            txtSuccess.Text = Convert.ToString(dt.Rows[0]["Success"]);
            txtFailed.Text = Convert.ToString(dt.Rows[0]["Failed"]);
            txtPending.Text = Convert.ToString(dt.Rows[0]["Pending"]);
            txtOperatorRefPosition.Text = Convert.ToString(dt.Rows[0]["OperatorRefPosition"]);
            txtErrorCodePosition.Text = Convert.ToString(dt.Rows[0]["ErrorCodePosition"]);
            txtBalanceURL.Text = Convert.ToString(dt.Rows[0]["BalanceURL"]);
            txtB_prm1.Text = Convert.ToString(dt.Rows[0]["B_prm1"]);
            txtB_prm1val.Text = Convert.ToString(dt.Rows[0]["B_prm1val"]);
            txtB_prm2.Text = Convert.ToString(dt.Rows[0]["B_prm2"]);
            txtB_prm2val.Text = Convert.ToString(dt.Rows[0]["B_prm2val"]);
            txtB_prm3.Text = Convert.ToString(dt.Rows[0]["B_prm3"]);
            txtB_prm3val.Text = Convert.ToString(dt.Rows[0]["B_prm3val"]);
            txtB_prm4.Text = Convert.ToString(dt.Rows[0]["B_prm4"]);
            txtB_prm4val.Text = Convert.ToString(dt.Rows[0]["B_prm4val"]);
            txtB_BalancePosition.Text = Convert.ToString(dt.Rows[0]["B_BalancePosition"]);
            txtStatusURL.Text = Convert.ToString(dt.Rows[0]["StatusURL"]);
            txtS_prm1.Text = Convert.ToString(dt.Rows[0]["S_prm1"]);
            txtS_prm1val.Text = Convert.ToString(dt.Rows[0]["S_prm1val"]);
            txtS_prm2.Text = Convert.ToString(dt.Rows[0]["S_prm2"]);
            txtS_prm2val.Text = Convert.ToString(dt.Rows[0]["S_prm2val"]);
            txtS_prm3.Text = Convert.ToString(dt.Rows[0]["S_prm3"]);
            txtS_prm4.Text = Convert.ToString(dt.Rows[0]["S_prm4"]);
            txtS_StatusPosition.Text = Convert.ToString(dt.Rows[0]["S_StatusPosition"]);
            txtS_OpratrRefPosition.Text = Convert.ToString(dt.Rows[0]["S_OpraterRefPosition"]);
            txtS_ApiTransPosition.Text = Convert.ToString(dt.Rows[0]["S_ApiTransPosition"]);
        }
    }

    private void clear()
    {
        txtAPIName.Text = "";
        txtAPIName.Text = "";
        txtURL.Text = "";
        txtSplitter.Text = "";
        txtprm1.Text = "uid";
        txtprm1val.Text = "";
        txtprm2.Text = "pin";
        txtprm2val.Text = "";
        txtprm3.Text = "number";
        txtprm4.Text = "operator";
        txtprm5.Text = "circle";
        txtprm6.Text = "amount";
        txtprm7.Text = "account";
        txtprm8.Text = "usertx";
        txtprm9.Text = "format";
        txtprm9val.Text = "";
        txtprm10.Text = "version";
        txtprm10val.Text = "";
        txtTxIDPosition.Text = "";
        txtStatusPosition.Text = "";
        txtSuccess.Text = "";
        txtFailed.Text = "";
        txtPending.Text = "";
        txtOperatorRefPosition.Text = "";
        txtErrorCodePosition.Text = "";
        txtBalanceURL.Text = "";
        txtB_prm1.Text = "uid";
        txtB_prm1val.Text = "";
        txtB_prm2.Text = "pin";
        txtB_prm2val.Text = "";
        txtB_prm3.Text = "";
        txtB_prm3val.Text = "";
        txtB_prm4.Text = "";
        txtB_prm4val.Text = "";
        txtB_BalancePosition.Text = "";
        txtStatusURL.Text = "";
        txtS_prm1.Text = "uid";
        txtS_prm1val.Text = "";
        txtS_prm2.Text = "pin";
        txtS_prm2val.Text = "";
        txtS_prm3.Text = "";
        txtS_prm4.Text = "";
        txtS_StatusPosition.Text = "";
        txtS_ApiTransPosition.Text = txtS_OpratrRefPosition.Text = "";
    }
}