using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AdminMaster : System.Web.UI.MasterPage
{
    cls_connection objconnection = new cls_connection();
    DataTable dtMember;
    public Company company;
    Page page;
    public Permission permission;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtEmployee"] != null)
        {
            company = new Company();
            company = Company.GetCompanyInfo();
            dtMember = (DataTable)Session["dtEmployee"];
            if (!IsPostBack)
            {
                FillMenu();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }

    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session.Contents.RemoveAll();
        Session.Abandon();
        Session.Clear(); Session.Abandon();
        Response.Redirect("Default.aspx");
    }
   

    protected void FillMenu()
    {
        dtMember = (DataTable)Session["dtEmployee"];
        rptr_main_menu.DataSource = objconnection.select_data_dt("EXEC FillMenu @Action='RoleMenu', @RoleID=" + Convert.ToInt16(dtMember.Rows[0]["RoleID"].ToString()) + ",@ID=0,@ParentID=0");
        rptr_main_menu.DataBind();
    }


    protected void rptr_main_menu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        dtMember = (DataTable)Session["dtEmployee"];
        Repeater rptr_sub_menu = (Repeater)e.Item.FindControl("rptr_sub_menu");
        HiddenField hdn_id = (HiddenField)e.Item.FindControl("hdn_id");
        rptr_sub_menu.DataSource = objconnection.select_data_dt("EXEC FillMenu @Action='RoleMenu', @RoleID=" + Convert.ToInt16(dtMember.Rows[0]["RoleID"].ToString()) + ",@ID=0,@ParentID=" + hdn_id.Value + "");
        rptr_sub_menu.DataBind();
    }
}
