using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PANDetailsUpdate : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        if (!IsPostBack)
        {

        }
    }

    protected void txtMemberID_TextChanged(object sender, EventArgs e)
    {
        DataTable dthistory = ObjConnection.select_data_dt("select * from Member where LoginiD='" + txtMemberID.Text + "'");
        if (dthistory.Rows.Count > 0)
        {
            txtName.Text = dthistory.Rows[0]["Name"].ToString();
            txtPSAName.Text = dthistory.Rows[0]["Name"].ToString();
            txtDOB.Text = dthistory.Rows[0]["DOB"].ToString();
            txtPincode.Text = dthistory.Rows[0]["PinCode"].ToString();
            txtAddress.Text = dthistory.Rows[0]["Address"].ToString();
            txtEmailID.Text = dthistory.Rows[0]["Email"].ToString();
            txtPhone1.Text = dthistory.Rows[0]["Mobile"].ToString();
            txtPhone2.Text = dthistory.Rows[0]["Mobile"].ToString();
            txtadhar.Text = dthistory.Rows[0]["Aadhar"].ToString();
            txtpan.Text = dthistory.Rows[0]["Pan"].ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtPSAAGENTID.Text) && !string.IsNullOrEmpty(txtMemberID.Text))
        {
            DataTable dthistory = ObjConnection.select_data_dt("select * from Member where LoginiD='" + txtMemberID.Text + "'");
            if (dthistory.Rows.Count > 0)
            {
                Int32 Val = 0;
                Val = ObjConnection.update_data("PanRegistration_Admin " + dthistory.Rows[0]["MSrno"].ToString() + ",'" + dthistory.Rows[0]["loginID"].ToString() + "','" + dthistory.Rows[0]["Name"].ToString() + "','" + dthistory.Rows[0]["Name"].ToString() + "','" + dthistory.Rows[0]["DOB"].ToString() + "','','" + dthistory.Rows[0]["pinCOde"].ToString() + "','" + dthistory.Rows[0]["Address"].ToString() + "','" + dthistory.Rows[0]["Email"].ToString() + "','" + dthistory.Rows[0]["mobile"].ToString() + "','" + dthistory.Rows[0]["MObile"].ToString() + "','" + dthistory.Rows[0]["aadhar"].ToString() + "','" + dthistory.Rows[0]["Pan"].ToString() + "','" + txtPSAAGENTID.Text + "'");
                if (Val > 0)
                {
                    ErrorShow.AlertMessage(page, "Update successfully", "Success");
                }
            }
        }
        else
        {
            txtPSAAGENTID.BorderColor = System.Drawing.Color.Red;
        }
    }

}