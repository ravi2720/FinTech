using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_PagePermissionSetting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (SessionManager.IsSessionAdmin)
        {

        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    [WebMethod]
    public static string GetPageList(string MethodName,string RoleID)
    {
        cls_connection Objconnection = new cls_connection();
        DataTable dt = Objconnection.select_data_dt("ManagePageList 'GellAll',"+ RoleID + "");

        return ConstantsData.SerializeToJSon(dt);
    }

    [WebMethod]
    public static string UpdatePermission(string MethodName, string ColN, string PageID, string RoleID)
    {
        cls_connection Objconnection = new cls_connection();
        Int32 Val = Objconnection.update_data("ManagePermissionPage 'ColUpdate','" + ColN + "','" + RoleID + "','" + PageID + "'");

        return Val.ToString();
    }

    [WebMethod]
    public static string GetRoleData()
    {
        cls_connection Objconnection = new cls_connection();
        DataTable dtmem = (DataTable)SessionManager.CurrentSessionAdmin;
        DataTable dt = Objconnection.select_data_dt("ManageRole 'GetAll',0," + Company.GetCompanyInfo().MemberID + "");

        return ConstantsData.SerializeToJSon(dt);
    }

}