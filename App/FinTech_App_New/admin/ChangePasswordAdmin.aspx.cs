using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ChangePasswordAdmin : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtemp;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["dtEmployee"] != null)
        {
           dt= dtemp =(DataTable) Session["dtEmployee"];
            if (!IsPostBack)
            {
                dt = cls.select_data_dt("select * from Employee where ID = " + dtemp.Rows[0]["ID"].ToString() + "");
                txtOldPassword.Text = dt.Rows[0]["password"].ToString();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtNewPassword.Text) && !string.IsNullOrEmpty(txtCnfPassword.Text) && !string.IsNullOrEmpty(txtOldPassword.Text))
        {
            if (dt.Rows[0]["password"].ToString() == txtOldPassword.Text)
            {
                if (txtNewPassword.Text.Trim() == txtCnfPassword.Text.Trim())
                {
                    int a = cls.update_data("exec PROC_MANAGE_PASSWROD_Admin " + Convert.ToInt16(dt.Rows[0]["ID"]) + ",'CHANGEPWD','" + txtCnfPassword.Text.Trim() + "'");
                    if (a > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password Changed Successfully ..!!');window.location.replace('DashBoard.aspx');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Confirm password not match');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Old Password not match');", true);

            }
        }
    }


}