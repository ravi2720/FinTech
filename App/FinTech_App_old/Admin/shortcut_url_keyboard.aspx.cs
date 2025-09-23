using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_shortcut_url_keyboard : System.Web.UI.Page
{
    cls_connection objDataAccess = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetShortCutKeyData();
        }
    }
    public void GetShortCutKeyData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objDataAccess.select_data_dt("SELECT * FROM TBL_SHORT_CUT_KEYWORD_PAGEURL");
            ViewState["dt"] = dt;
            rptShortkData.DataSource = dt;
            rptShortkData.DataBind();
            DataTable dt1 = new DataTable();
            dt1 = objDataAccess.select_data_dt("SELECT * FROM tbl_keyboard_shortcode");
            ViewState["dt1"] = dt1;
            rptPDFReadyData.DataSource = dt1;
            rptPDFReadyData.DataBind();
        }
        catch (Exception ex)
        {
        }
    }
    private void AddShortCut()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["dt"];
            DataRow dr = dt.NewRow();
            dr[0] = GetLastID();
            dr[1] = "Ctrl + Shift + ";
            dr[2] = 0;
            dr[3] = DateTime.Now;
            dr[4] = "";
            dr[5] = "16,17,";

            dt.Rows.Add(dr);
            rptShortkData.DataSource = dt;
            rptShortkData.DataBind();
        }
        catch (Exception ex)
        {
        }
    }
    protected void Unnamed_Click(object sender, EventArgs e)
    {
        AddShortCut();
    }
    protected void rptShortkData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        int ID = Convert.ToInt16(e.CommandArgument.ToString());
        TextBox TextBox2 = (TextBox)e.Item.FindControl("TextBox2");
        TextBox TextBox1 = (TextBox)e.Item.FindControl("TextBox1");
        TextBox TextBox3 = (TextBox)e.Item.FindControl("TextBox3");
        if (e.CommandName.ToString() == "Save")
        {


            DataTable DT = new DataTable();

            if (TextBox2.Text.Split('+').Length == 3)
            {
                if (TextBox2.Text.Split('+')[2] == " " || TextBox2.Text.Split('+')[2] == "")
                {
                    TextBox2.BorderColor = System.Drawing.Color.Red;
                    TextBox2.BorderWidth = 2;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Please add third Key');</script>", false);
                    return;
                }
            }
            if (TextBox2.Text.Split('+').Length == 2)
            {
                if (TextBox2.Text.Split('+')[1] == " " || TextBox2.Text.Split('+')[1] == "")
                {
                    TextBox2.BorderColor = System.Drawing.Color.Red;
                    TextBox2.BorderWidth = 2;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Please add third Key');</script>", false);
                    return;
                }
            }
            if (TextBox3.Text.Split(',').Length == 3)
            {
                if (TextBox2.Text.Split('+').Length != 3)
                {
                    TextBox3.BorderColor = System.Drawing.Color.Red;
                    TextBox3.BorderWidth = 2;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Shortcut and SHORTCUT KEY VALUE must be same count in digit');</script>", false);
                    return;
                }
                if (TextBox3.Text.Split(',')[2] == "")
                {
                    TextBox3.BorderColor = System.Drawing.Color.Red;
                    TextBox3.BorderWidth = 2;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Please add third numeric code');</script>", false);
                    return;
                }
            }
            if (TextBox3.Text.Split(',').Length == 2)
            {
                if (TextBox2.Text.Split('+').Length != 2)
                {
                    TextBox3.BorderColor = System.Drawing.Color.Red;
                    TextBox3.BorderWidth = 2;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Shortcut and SHORTCUT KEY VALUE must be same count in digit');</script>", false);
                    return;
                }
                if (TextBox3.Text.Split(',')[1] == "")
                {
                    TextBox3.BorderColor = System.Drawing.Color.Red;
                    TextBox3.BorderWidth = 2;
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('add third numeric code');</script>", false);
                    return;
                }
            }

            if (TextBox1.Text == "")
            {
                TextBox1.BorderColor = System.Drawing.Color.Red;
                TextBox1.BorderWidth = 2;

                return;
            }

            int val = objDataAccess.update_data("EXEC PROC_ADD_EDIT_SHORT_CUT_KEYWORD_PAGEURL " + ID + ",'" + TextBox2.Text + "','" + TextBox1.Text + "','" + TextBox3.Text + "'");
            if (val > 0)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Add Successfully');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Something Wrong Check Again');</script>", false);
            }
        }
        if (e.CommandName.ToString() == "Remove")
        {
            int INDEXU = 0;
            INDEXU = objDataAccess.insert_data("delete TBL_SHORT_CUT_KEYWORD_PAGEURL where ID='" + ID + "'");
            if (INDEXU == 1)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.success('Remove Successfully');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Something Wrong Check Again');</script>", false);
            }
        }
        if (e.CommandName.ToString() == "Update")
        {
            int INDEXU = 0;
            INDEXU = objDataAccess.insert_data("update  TBL_SHORT_CUT_KEYWORD_PAGEURL set SHORTCUTNAME='" + TextBox2.Text + "',PAGEURL='" + TextBox1.Text + "',SHORTCUTKETVAL='" + TextBox3.Text + "' where ID='" + ID + "'");
            if (INDEXU == 1)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.success('Updated Successfully');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Something Wrong Check Again');</script>", false);
            }
        }
        if (e.CommandName.ToString() == "ADbtn")
        {
            int INDEXU = 0;
            INDEXU = objDataAccess.insert_data("update  TBL_SHORT_CUT_KEYWORD_PAGEURL set isactive=(case when isactive=0 then 1 else 0 end) where ID='" + ID + "'");
            if (INDEXU == 1)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.success('Successfully');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Something Wrong Check Again');</script>", false);
            }
        }
        GetShortCutKeyData();
    }
    private int GetLastID()
    {
        Int32 Val = 0;
        try
        {
            Val = objDataAccess.select_data_scalar_int("select max(id)+1 from TBL_SHORT_CUT_KEYWORD_PAGEURL");
        }
        catch (Exception ex)
        {
        }
        return Val;
    }
    protected void txtKeyCode_TextChanged(object sender, EventArgs e)
    {

        lblKeyCode.Text = objDataAccess.select_data_scalar_string("SELECT Codew FROM tbl_keyboard_shortcode where Keyw='" + txtKeyCode.Text + "'");
    }
}