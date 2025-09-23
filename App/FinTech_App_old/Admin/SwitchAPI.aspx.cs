using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_SwitchAPI : System.Web.UI.Page
{
    cls_connection Objconnection = new cls_connection();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            BindDropDown.FillDropDown(ddlpackage, "ActivePackage", "Name", "ID", "Select Package", "1");
        }
    }

    private void BindData()
    {
        DataTable dt = Objconnection.select_data_dt("SwitchAPI " + ddlpackage.SelectedValue + "");
        rptData.DataSource = dt;
        rptData.DataBind();
    }
    protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        DropDownList dllAPILIST = (DropDownList)e.Item.FindControl("dllAPILIST");
        DataTable dt = Objconnection.select_data_dt("ProcRecharge_ManageAPI 'GetAll', 0");
        dllAPILIST.DataSource = dt;
        dllAPILIST.DataTextField = "APIName";
        dllAPILIST.DataValueField = "APIID";
        dllAPILIST.DataBind();
        dllAPILIST.Items.Insert(0, new ListItem("Select API", "0"));
    }

    protected void ddlpackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Change")
        {
            DropDownList dllAPILIST = (DropDownList)e.Item.FindControl("dllAPILIST");
            if (dllAPILIST.SelectedIndex > 0 && ddlpackage.SelectedIndex > 0)
            {
                Int32 Val = Objconnection.update_data("update tblRecharge_Commission set ActiveAPI=" + dllAPILIST.SelectedValue + " where OperatorID='" + e.CommandArgument.ToString() + "' and PackageID=" + ddlpackage.SelectedValue + "");
                if (Val > 0)
                {
                    ErrorShow.AlertMessage(page,"Success",ConstantsData.CompanyName);
                }
            }
        }
    }
}