using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_TreeBlance : System.Web.UI.Page
{
   
    public DataTable dtMember;
    cls_connection cls = new cls_connection();
    ActionButtonPermission actionButtonPermission;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {
            page = HttpContext.Current.CurrentHandler as Page;
            dtMember = (DataTable)SessionManager.CurrentSessionAdmin;
            actionButtonPermission = ActionButtonPermission.GetPermission(ConstantsData.DMTHistory, dtMember.Rows[0]["RoleID"].ToString());
            if (!IsPostBack)
            {               
               // BindChild();
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void BindChild()
    {

        int msrno = cls.select_data_scalar_string("select msrno from member  where loginid='" + txtMemberID.Text + "'").ToInt32();
        lblName.InnerText = cls.select_data_scalar_string("select  name from VIEW_MEMBERBALANCE where Loginid='" + txtMemberID.Text + "'");
        lblbalance.InnerText = cls.select_data_scalar_string("select BALANCE from VIEW_MEMBERBALANCE where Loginid='" + txtMemberID.Text + "'");
        DataTable dtChild = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where parentid='" + msrno + "'");

        if (dtChild.Rows.Count > 0)
        {
            rptDateChild.DataSource = dtChild;
            rptDateChild.DataBind();
           
        }
    
        else
        {
            rptDateChild.DataSource = null;
            rptDateChild.DataBind();
        }
       
    }



    protected void rptDateChild_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField rptmsrno = (HiddenField)e.Item.FindControl("rptmsrno");
        Repeater rptinner = (Repeater)e.Item.FindControl("rptinner");
        DataTable dtChild = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where parentid='" + rptmsrno.Value + "'");
        if (dtChild.Rows.Count > 0)
        {
            rptinner.DataSource = dtChild;
            rptinner.DataBind();
        }
        else
        {
            rptinner.DataSource = null;
            rptinner.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindChild();
    }
}