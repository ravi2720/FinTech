using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_PAN_FeeSettings : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillData();
        }
    }
    private void fillData()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Exec [ProcPAN_ManageFeeSetting] 'GetByMsrNo', 1");
        if (dt.Rows.Count > 0)
        {
            txtNewPanAmount.Text = Convert.ToString(dt.Rows[0]["NewAmount"]);
            txtCorrectPanAmount.Text = Convert.ToString(dt.Rows[0]["CorrectionAmount"]);
            hddExistingID.Value = Convert.ToString(dt.Rows[0]["ID"]);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dtresult = new DataTable();
        dtresult = cls.select_data_dt("Exec [ProcPAN_AddEditFeeSetting] '" + hddExistingID.Value + "', 1, '" + txtNewPanAmount.Text.Trim() + "', '" + txtCorrectPanAmount.Text.Trim() + "'");
        if (dtresult.Rows.Count > 0)
        {
            if (Convert.ToInt32(dtresult.Rows[0]["ID"]) > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Record updated successfully !'); location.replace('PAN_FeeSettings.aspx')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Failed|Already Exist !');", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Failed|Technical Issue !');", true);
        }
    }
}