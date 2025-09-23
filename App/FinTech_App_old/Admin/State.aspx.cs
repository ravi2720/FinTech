using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_State : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    State state = new State();
    int Val = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetState();
        }
    }

    public void GetState()
    {
        dt = ObjConnection.select_data_dt("ManageState @Action='GetAll'");
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
        state.Name = txtStateName.Text;
        state.ID = Convert.ToInt32(hdnStateID.Value);
        state.CircleCode = txtStateCode.Text;
        state.IsActive = chkActive.Checked;
        if (state.IsAnyNullOrEmpty(state))
        {
            Val = ObjConnection.update_data("AddEditState " + state.ID + ",'" + state.Name + "','" + state.CircleCode + "',"+ state.CountryID+ "," + state.IsDelete + "," + state.IsActive + "");
            if (Val > 0) { GetState(); Clear(); }
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
        dt = ObjConnection.select_data_dt("ManageState @Action='GetAll',@ID=" + ID + "");
        if (dt.Rows.Count > 0)
        {
            hdnStateID.Value = ID;
            txtStateName.Text = dt.Rows[0]["Name"].ToString();
            txtStateCode.Text = dt.Rows[0]["StateCode"].ToString();
            chkActive.Checked = Convert.ToBoolean(dt.Rows[0]["IsActive"].ToString());
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageState @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            GetState();
        }
    }

    private void Clear()
    {
        txtStateCode.Text = txtStateName.Text = "";
        chkActive.Checked = false;
        hdnStateID.Value = "0";
    }
}