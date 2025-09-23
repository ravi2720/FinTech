using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ManageMenu : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dtMember;
    int Val = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
               
                FillCompany();
                State();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }




    private void State()
    {
        DataTable dt = ObjConnection.select_data_dt("select * from State where  countryid=26 and isactive=1");
        if (dt.Rows.Count > 0)
        {
            ddlState.DataSource = dt;
            ddlState.DataTextField = "Name";
            ddlState.DataValueField = "StateCode";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("-- Select State --", "0"));
        }
    }

    private void FillCompany()
    {
        rptData.DataSource = ObjConnection.select_data_dt("select * from GSTSetting");
        rptData.DataBind();
    }

    private void fill_update_menu(int id)
    {
        DataTable dt = new DataTable();
        hdnID.Value = id.ToString();
        dt = ObjConnection.select_data_dt("select * from View_GSTSetting where ID = " + id + "");
        if (dt.Rows.Count > 0)
        {
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            txtOwnerName.Text = dt.Rows[0]["OwnerName"].ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();            
            txtPan.Text = dt.Rows[0]["Pan"].ToString();
            txtGSTNo.Text = dt.Rows[0]["GSTNo"].ToString();
            txtCIN.Text = dt.Rows[0]["CIN"].ToString();
            ddlState.SelectedValue = dt.Rows[0]["StateID"].ToString();
            ddlCompanyType.SelectedValue = dt.Rows[0]["CompanyType"].ToString();

            btnSubmit.Text = "Update";
        }
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageCompany @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            FillCompany();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        DataTable dt = new DataTable();
        if (btnSubmit.Text == "Submit")
        {
            dt = ObjConnection.select_data_dt("EXEC AddEditGSTSetting 0,'" + txtOwnerName.Text.Trim() + "','" + ddlState.SelectedValue+ "','" + txtName.Text.Trim() + "','" + txtAddress.Text.Trim() + "','" + ddlCompanyType.SelectedValue + "','" + txtPan.Text.Trim() + "','" + txtGSTNo.Text.Trim() + "','" + txtCIN.Text.Trim() + "'");
        }
        else if (btnSubmit.Text == "Update")
        {
            dt = ObjConnection.select_data_dt("EXEC AddEditGSTSetting " + Convert.ToInt16(hdnID.Value) + ",'" + txtOwnerName.Text.Trim() + "','" + ddlState.SelectedValue + "','" + txtName.Text.Trim() + "','" + txtAddress.Text.Trim() + "','" + ddlCompanyType.SelectedValue + "','" + txtPan.Text.Trim() + "','" + txtGSTNo.Text.Trim() + "','" + txtCIN.Text.Trim() + "'");
            btnSubmit.Text = "Save";
            btnSubmit.Enabled = false;
            btnDone.Visible = true;
            btnDone.Focus();
        }
        lblMessage.Text = dt.Rows[0]["Status"].ToString();
        Clear();
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            fill_update_menu(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }

        if (e.CommandName == "Active")
        {
            Active(ID: e.CommandArgument.ToString());
        }
    }

    private Bitmap Resize_Image(Stream streamImage, int maxWidth, int maxHeight)
    {
        Bitmap originalImage = new Bitmap(streamImage);
        int newWidth = originalImage.Width;
        int newHeight = originalImage.Height;
        double aspectRatio = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height);

        newWidth = maxWidth;
        newHeight = maxHeight;
        return new Bitmap(originalImage, newWidth, newHeight);
    }

    private void Clear()
    {
        txtAddress.Text = txtName.Text = txtPan.Text = txtGSTNo.Text = txtCIN.Text = txtOwnerName.Text = string.Empty;
        ddlState.SelectedIndex = 0;
    }

    protected void btnDone_Click(object sender, EventArgs e)
    {
        Response.Redirect("managecompany.aspx");
    }
}