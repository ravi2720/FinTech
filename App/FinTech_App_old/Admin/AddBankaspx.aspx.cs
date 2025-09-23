using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AddBankaspx : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    Bank bank = new Bank();
    int Val = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetBank();
        }
    }

    public void GetBank()
    {
        dt = ObjConnection.select_data_dt("ManageBank @Action='GetAll'");
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bank.Name = txtBankName.Text;
        bank.ID = Convert.ToInt32(hdnBankID.Value);
        bank.IFSC = txtIFSC.Text;
        bank.IsActive = chkActive.Checked;
        if (bank.IsAnyNullOrEmpty(bank))
        {
            Val = ObjConnection.update_data("AddEditBank " + bank.ID + ",'" + bank.Name + "','" + bank.IFSC + "','"+bank.Class+"'," + bank.IsDelete + "," + bank.IsActive + "");
            if (Val > 0) { GetBank(); Clear(); }
        }
        else
        {

        }
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            FillData(ID: e.CommandArgument.ToString());
        }

        if (e.CommandName == "Active")
        {
            Active(ID: e.CommandArgument.ToString());
        }
    }

    private void FillData(string ID)
    {
        dt = ObjConnection.select_data_dt("ManageBank @Action='GetAll',@ID=" + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnBankID.Value = ID;
            txtBankName.Text = dt.Rows[0]["Name"].ToString();
            txtIFSC.Text = dt.Rows[0]["IFSC"].ToString();
            chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageBank @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            GetBank();
        }
    }

    private void Clear()
    {
        txtIFSC.Text = txtBankName.Text = "";
        chkActive.Checked = false;
        hdnBankID.Value = "0";
    }
}