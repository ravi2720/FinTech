using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ComplainChat : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt;
    DataTable dtEmployee;
    protected void Page_Load(object sender, EventArgs e)
    {
        dtEmployee = (DataTable)Session["dtEmployee"];
        if (!IsPostBack)
        {
          

            if (Request.QueryString["ID"] != null)
            {
                ComplainTitle.InnerHtml = ObjConnection.select_data_scalar_string("select Message from ComplainTicket where ID="+ Request.QueryString["ID"].ToString() + "");
                BindData();
            }
        }
    }

    private void BindData()
    {
        dt = ObjConnection.select_data_dt("Proc_Get_Complain_Chat "+ Request.QueryString["ID"].ToString() + "");
        if (dt.Rows.Count > 0)
        {
            rptData.DataSource = dt;
            rptData.DataBind();
        }
    }

    protected void btnAddMessage_Click(object sender, EventArgs e)
    {
        Int32 Val = ObjConnection.update_data("proc_AddEditChat " + Request.QueryString["ID"].ToString() + ",'"+txtMessage.Text+"',"+ dtEmployee.Rows[0]["ID"].ToString() + "");
        if (Val > 0)
        {
            BindData();
        }
    }
}