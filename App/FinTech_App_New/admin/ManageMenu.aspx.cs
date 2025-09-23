using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime;
public partial class Admin_ManageMenu : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    int Val = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            if (!IsPostBack)
            {
                FillMenu();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void FillMenu()
    {
        rptData.DataSource = ObjConnection.select_data_dt("EXEC FillMenu @Action='All', @RoleID=0,@ID=0,@ParentID=0");
        rptData.DataBind();
        ddlMainMenu.Items.Insert(0, new ListItem("-Select Main Menu-", "0"));
        ddlSubMenu.Items.Insert(0, new ListItem("-Select Sub Menu-", "0"));

    }

    private void fillMainMenu()
    {
        DataTable dtMainMenu = ObjConnection.select_data_dt("EXEC FillMenu @Action='ParentMenu', @RoleID=0,@ID=0,@ParentID=0");
        Common.BindDropDown(ddlMainMenu, dtMainMenu, "Name", "ID");
        ddlMainMenu.Items.Insert(0, new ListItem("-Select Main Menu-", "0"));
    }
    private void fillSubMenu(int id)
    {
        DataTable dtSubMenu = ObjConnection.select_data_dt("EXEC FillMenu @Action='ParentMenu', @RoleID=0,@ID=0,@ParentID=" + id + "");
        Common.BindDropDown(ddlSubMenu, dtSubMenu, "Name", "ID");
        ddlSubMenu.Items.Insert(0, new ListItem("-Select Sub Menu-", "0"));
    }
    private void fill_update_menu(int id)
    {
        ddlMainMenu.Items.Clear();
        ddlSubMenu.Items.Clear();
        DataTable dt = new DataTable();
        hdnMenuId.Value = id.ToString();
        dt = ObjConnection.select_data_dt("EXEC FillMenu @Action='selectedMenu', @RoleID=0,@ID=" + id + ",@ParentID=0");
        txtClass.Text = dt.Rows[0]["CLASS"].ToString();
        txtMenuIcon.Text = dt.Rows[0]["ICON"].ToString();
        txtMenuLink.Text = dt.Rows[0]["LINK"].ToString();
        txtMenuPosition.Text = dt.Rows[0]["POSITION"].ToString();
        txtMenuName.Text = dt.Rows[0]["Name"].ToString();
        ddlMenuLevel.SelectedValue = dt.Rows[0]["LEVEL"].ToString();

        string[] tokens = dt.Rows[0]["PARENTSTR"].ToString().Split(',');
        string[] sss = tokens.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        int cnt = sss.Count();
        if (cnt == 3)
        {
            fillMainMenu();
            ddlMainMenu.SelectedValue = sss[0].ToString();
            fillSubMenu(Convert.ToInt32(ddlMainMenu.SelectedValue));
            ddlSubMenu.SelectedValue = sss[1].ToString();
        }
        else if (cnt == 2)
        {
            fillMainMenu();
            ddlMainMenu.SelectedValue = sss[0].ToString();

        }
        else
        {

        }

        btnSubmit.Text = "Update";
    }

    private void Active(string ID)
    {
        Val = ObjConnection.update_data("ManageMenu @Action='IsActive',@ID=" + ID + "");
        if (Val > 0)
        {
            FillMenu();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int parentmenu = 0;

        if (ddlMenuLevel.SelectedValue == "2")
        {
            parentmenu = Convert.ToInt32(ddlMainMenu.SelectedValue);
        }
        else if (ddlMenuLevel.SelectedValue == "3")
        {
            parentmenu = Convert.ToInt32(ddlSubMenu.SelectedValue);
        }
        else
        {
            parentmenu = 0;
        }
        int id = hdnMenuId.Value == "0" ? 0 : Convert.ToInt16(hdnMenuId.Value.ToString());
        DataTable dt = new DataTable();
        dt = ObjConnection.select_data_dt("EXEC AddEditMenu " + id + ",'" + ddlMenuType.SelectedValue + "','" + txtMenuName.Text.Trim() + "','" + txtMenuLink.Text.Trim() + "','" + txtMenuIcon.Text.Trim() + "'," + Convert.ToInt32(ddlMenuLevel.SelectedValue) + "," + parentmenu + "," + Convert.ToInt32(txtMenuPosition.Text.Trim()) + ",'" + txtClass.Text.Trim() + "'");
        if (Convert.ToInt32(dt.Rows[0]["Result"].ToString()) > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>alert('" + dt.Rows[0]["status"] + "'); window.location.href = 'ManageMenu.aspx';</script>", false);
            FillMenu();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>alert('" + dt.Rows[0]["status"] + "');</script>", false);
        }
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

    protected void ddlMenuLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillMainMenu();
    }

    protected void ddlMainMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillSubMenu(Convert.ToInt32(ddlMainMenu.SelectedValue));

    }
}