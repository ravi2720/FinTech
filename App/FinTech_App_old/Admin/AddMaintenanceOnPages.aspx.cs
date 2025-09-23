using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_AddMaintenanceOnPages : System.Web.UI.Page
{
    cls_connection objDataAccess = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetMainTenanceData();
        }
    }
    public void GetMainTenanceData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = objDataAccess.select_data_dt("SELECT * FROM TBL_MAINTENANCE_PAGEURL_WISE");
            ViewState["dt"] = dt;
            rptShortkData.DataSource = dt;
            rptShortkData.DataBind();
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
            dr[1] = "";
            dr[2] = 0;
            dr[3] = DateTime.Now;
            dr[4] = 0;
            dr[5] = "";

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
        TextBox TextBox1 = (TextBox)e.Item.FindControl("TextBox1");
        TextBox TextBox2 = (TextBox)e.Item.FindControl("TextBox2");

        if (e.CommandName.ToString() == "Save")
        {


            DataTable DT = new DataTable();

            if (TextBox1.Text == "")
            {
                TextBox1.BorderColor = System.Drawing.Color.Red;
                TextBox1.BorderWidth = 2;
                return;
            }

            DT = objDataAccess.select_data_dt("EXEC PROC_ADD_EDIT_MAINTENANCE_PAGEURL_WISE " + ID + ",'" + TextBox1.Text.ToUpper() + "','" + TextBox2.Text + "'");
            if (DT.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('" + DT.Rows[0]["ERR"].ToString() + "');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Something Wrong Check Again');</script>", false);
            }
        }
        if (e.CommandName.ToString() == "Remove")
        {
            int INDEXU = 0;
            INDEXU = objDataAccess.insert_data("delete TBL_MAINTENANCE_PAGEURL_WISE where ID='" + ID + "'");
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
            INDEXU = objDataAccess.insert_data("update  TBL_MAINTENANCE_PAGEURL_WISE set PAGEURL='" + TextBox1.Text + "',pagename='" + TextBox2.Text + "' where ID='" + ID + "'");
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
            INDEXU = objDataAccess.insert_data("update  TBL_MAINTENANCE_PAGEURL_WISE set isactive=(case when isactive=0 then 1 else 0 end) where ID='" + ID + "'");
            if (INDEXU == 1)
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.success('Successfully');</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Message", "<script> alertify.error('Something Wrong Check Again');</script>", false);
            }
        }
        GetMainTenanceData();
    }
    private int GetLastID()
    {
        Int32 Val = 0;
        try
        {
            Val = objDataAccess.select_data_scalar_int("select max(id)+1 from TBL_MAINTENANCE_PAGEURL_WISE");
        }
        catch (Exception ex)
        {
        }
        return Val;
    }

    protected void rptShortkData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        TextBox TextBox1 = (TextBox)e.Item.FindControl("TextBox1");
        if (TextBox1.Text.Contains("APP_"))
        {
            TextBox1.BackColor = System.Drawing.Color.DarkTurquoise;
        }
        if (TextBox1.Text.Contains("M_"))
        {
            TextBox1.BackColor = System.Drawing.Color.Pink;
        }

    }
}