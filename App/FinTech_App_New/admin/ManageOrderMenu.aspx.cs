using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime;
using System.Web.Services;

public partial class Admin_ManageOrderMenu : System.Web.UI.Page
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
        rptData.DataSource = ObjConnection.select_data_dt("EXEC FillMenu @Action='ParentMenu', @RoleID=0,@ID=0,@ParentID=0");
        rptData.DataBind();
    }


    [WebMethod]
    public static string UpdateMenu(string ID, string Position)
    {
        Int32 Val = 0;
        cls_connection Objconnection = new cls_connection();
        Val = Objconnection.update_data("update menu set ShowPosition= '"+Position+"' where id="+ID+"");
        return Val.ToString();
    }

}