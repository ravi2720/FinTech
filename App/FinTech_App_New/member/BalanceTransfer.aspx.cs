using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Member_BalanceTransfer : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtMember;
    protected void Page_Load(object sender, EventArgs e)
    {
        dtMember = (DataTable)Session["dtMember"];
        if (!IsPostBack)
        {
            FillRole();

            multiview1.ActiveViewIndex = 0;
        }
    }

    private void FillRole()
    {
        DataTable dtRole = cls.select_data_dt("Exec ManageRole @Action='RoleBasePermission',@ID=" + dtMember.Rows[0]["RoleID"].ToString() + "");
        Common.BindDropDown(ddlMember, dtRole, "Name", "ID");
        ddlMember.Items.Insert(0, new ListItem("--select Role--", "0"));
    }
    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        multiview1.ActiveViewIndex = 1;
        DataTable dtMember1 = cls.select_data_dt("select * from View_MemberBalance where roleid='" + Convert.ToInt16(ddlMember.SelectedValue) + "' and ParentID="+ dtMember.Rows[0]["msrno"].ToString()+"");
        if (dtMember1.Rows.Count > 0)
        {
            rptDataRecharge.DataSource = dtMember1;
            rptDataRecharge.DataBind();
        }
    }
}