using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ChangePassword : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["usr"] != null)
        {
            dt = cls.select_data_dt("select * from Member where msrno = " + Convert.ToInt16(Request.QueryString["usr"]) + "");
            txtOldPin.Text = dt.Rows[0]["LoginPin"].ToString();
        }
        else
        {
            Response.Redirect("memberlist.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtNewLoginPin.Text) && !string.IsNullOrEmpty(txtcnfLoginPin.Text))
        {
            int a = cls.update_data("exec PROC_MANAGE_PASSWROD " + Convert.ToInt16(dt.Rows[0]["Msrno"]) + ",'CHANGEPIN','" + txtcnfLoginPin.Text.Trim() + "'");
            if (a > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Pin Changed Successfully ..!!');window.location.replace('memberlist.aspx');", true);
            }
        }
    }
}