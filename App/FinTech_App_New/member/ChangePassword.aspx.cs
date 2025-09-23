using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_ChangePassword : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (dtMember.Rows.Count > 0)
            {
                //  txtOldPassword.Text = dtMember.Rows[0]["password"].ToString();
            }
            else
            {
                Response.Redirect("memberlist.aspx");
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
            if (txtOldPassword.Text == dtMember.Rows[0]["password"].ToString())
            {
                if (txtNewPassword.Text.Trim() == txtCnfPassword.Text.Trim())
                {
                    int a = cls.update_data("exec PROC_MANAGE_PASSWROD " + Convert.ToInt16(dtMember.Rows[0]["Msrno"]) + ",'CHANGEPWD','" + txtCnfPassword.Text.Trim() + "'");
                    if (a > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Password Changed Successfully ..!!');", true);
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