using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_MenuAssignMaster : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable TreeDT = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillRole();
        }
    }

    public void FillRole()
    {
        DataTable dtRole = new DataTable();
        dtRole = ObjConnection.select_data_dt("ManageMasterRole @Action='GetAll',@ID=0");

        ddlRole.DataSource = dtRole;
        ddlRole.DataTextField = "Name";
        ddlRole.DataValueField = "ID";
        ddlRole.DataBind();
        ddlRole.Items.Insert(0, new ListItem("--Select Role--", "0"));

    }
    //private void FillMenu()
    //{
    //    rptData.DataSource = ObjConnection.select_data_dt("EXEC FillMenu @Action='All', @RoleID=0,@ID=0,@ParentID=0");
    //    rptData.DataBind();
    //}


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        //string str = "";
        //foreach (RepeaterItem item in rptData.Items)
        //{

        //    HiddenField id = item.FindControl("hdnid") as HiddenField;

        //    CheckBox cb = (CheckBox)item.FindControl("chk");
        //    if (cb.Checked == true)
        //    {
        //        str = str + id.Value + ",";
        //    }

        //}
        //if (str.EndsWith(","))
        //{
        //    str = str.Substring(0, str.Length - 1);
        //}
        string MenuIDStr = "";
        foreach (TreeNode node in TreeMenu.CheckedNodes)
        {
            MenuIDStr = MenuIDStr + node.Value + ",";
        }
        //if (MenuIDStr.EndsWith(","))
        //{
        //    MenuIDStr = MenuIDStr.Substring(0, MenuIDStr.Length - 1);
        //}
        dt = ObjConnection.select_data_dt("EXEC ManageMenu 'MasteRolePermission', '" + ddlRole.SelectedValue + "' ,'" + MenuIDStr + "'");
        if (Convert.ToInt32(dt.Rows[0]["id"].ToString()) > 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "6", "<script>alert('Update Successfully'); window.location.href = 'MenuAssignMaster.aspx';</script>", false);
            //FillMenu();

        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>alert('something went wrong');</script>", false);
        }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlRole.SelectedValue) > 0)
        {
            BindTree();
        }

    }

    private void BindTree()
    {
        TreeMenu.Nodes.Clear();
        TreeDT = ObjConnection.select_data_dt("EXEC FillMenu @Action='AllMaster', @RoleID=" + ddlRole.SelectedValue + ",@ID=0,@ParentID=0");
        DataRow[] dr = TreeDT.Select("Level=1");
        for (int i = 0; i < dr.Length; i++)
        {
            TreeNode mNode = new TreeNode();
            mNode.Expanded = false;
            mNode.Text = dr[i]["Name"].ToString();
            mNode.Value = dr[i]["ID"].ToString();
            mNode.Checked = Convert.ToBoolean(dr[i]["Checked"]);
            mNode.SelectAction = TreeNodeSelectAction.Expand;
            mNode.PopulateOnDemand = true;
            TreeMenu.Nodes.Add(mNode);
            mNode.ExpandAll();
        }
        TreeMenu.CollapseAll();
    }
    public void ShowData(TreeNode Tnode)
    {
        DataRow[] drnod = TreeDT.Select("ParentID='" + Tnode.Value + "'");
        for (int j = 0; j < drnod.Length; j++)
        {
            TreeNode nod = new TreeNode();
            nod.Value = drnod[j]["ID"].ToString();
            nod.Text = drnod[j]["Name"].ToString();
            nod.Checked = Convert.ToBoolean(drnod[j]["Checked"]);
            nod.PopulateOnDemand = true;
            nod.Expanded = false;
            nod.SelectAction = TreeNodeSelectAction.Expand;
            Tnode.ChildNodes.Add(nod);
        }
    }
    protected void TreeMenu_TreeNodePopulate(object sender, TreeNodeEventArgs e)
    {
        ShowData(e.Node);
    }
}