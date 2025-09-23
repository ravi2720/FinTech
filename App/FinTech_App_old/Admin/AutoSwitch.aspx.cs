using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_AutoSwitch : System.Web.UI.Page
{
    cls_connection objconnection = new cls_connection();
    DataTable dtAPI = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtEmployee"] != null)
        {
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlPackage, "ActivePackage", "Name", "ID", "Select Package", "1");
                GetDataAmountSData();
                fillAPI();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }

    }
    private void fillAPI()
    {
        //dtAPI = objconnection.select_data_dt("ProcRecharge_ManageAPI 'GetAll', 0");
        //ddlAPI.DataSource = dtAPI;
        //ddlAPI.DataValueField = "APIID";
        //ddlAPI.DataTextField = "APIName";
        //ddlAPI.DataBind();
        //ddlAPI.Items.Insert(0, new ListItem("Select", "0"));
    }

    public void fillOperator()
    {
        DataTable dt = new DataTable();
        dt = objconnection.select_data_dt("PROC_GET_OP_LIST_AutoSystem " + Convert.ToInt32(ddlPackage.SelectedValue) + "");
        DataView dv = dt.DefaultView;
        dv.Sort = "ServiceType desc";
        DataTable sortedDT = dv.ToTable();
        rptOperator.DataSource = sortedDT;
        rptOperator.DataBind();
    }
    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlPackage.SelectedItem.Value) == 0)
        {
            DataTable dtOperator = new DataTable();
            rptOperator.DataSource = dtOperator;
            rptOperator.DataBind();
        }
        else
        {
            fillOperator();
        }
    }
    private void fillAPI(DropDownList ddlAPI, DropDownList ddlAPI2, DropDownList ddlAPI3, DropDownList ddlAPI4)
    {
        dtAPI = objconnection.select_data_dt("ProcRecharge_ManageAPI 'GetAll', 0");
        ddlAPI2.DataSource = ddlAPI3.DataSource = ddlAPI.DataSource = ddlAPI4.DataSource = dtAPI;
        ddlAPI2.DataValueField = ddlAPI3.DataValueField = ddlAPI.DataValueField = ddlAPI4.DataValueField = "APIID";
        ddlAPI2.DataTextField = ddlAPI3.DataTextField = ddlAPI.DataTextField = ddlAPI4.DataTextField = "APIName";
        ddlAPI2.DataBind(); ddlAPI3.DataBind(); ddlAPI.DataBind(); ddlAPI4.DataBind();
        ddlAPI.Items.Insert(0, new ListItem("Select", "0"));
        ddlAPI2.Items.Insert(0, new ListItem("Select", "0"));
        ddlAPI3.Items.Insert(0, new ListItem("Select", "0"));
        ddlAPI4.Items.Insert(0, new ListItem("Select", "0"));
    }
    protected void rptOperator_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hdnSaveAutoVal = (HiddenField)e.Item.FindControl("hdnSaveAutoVal");
        DropDownList ddlAPI1 = (DropDownList)e.Item.FindControl("ddlAPI1");
        DropDownList ddlAPI2 = (DropDownList)e.Item.FindControl("ddlAPI2");
        DropDownList ddlAPI3 = (DropDownList)e.Item.FindControl("ddlAPI3");
        DropDownList ddlAPI4 = (DropDownList)e.Item.FindControl("ddlAPI4");
        HiddenField hdnOperatorID = (HiddenField)e.Item.FindControl("hdnOperatorID");
        Label lblCommAPI1 = (Label)e.Item.FindControl("lblCommAPI1");
        Label lblCommAPI2 = (Label)e.Item.FindControl("lblCommAPI2");
        Label lblCommAPI3 = (Label)e.Item.FindControl("lblCommAPI3");
        Label lblCommAPI4 = (Label)e.Item.FindControl("lblCommAPI4");
        fillAPI(ddlAPI1, ddlAPI2, ddlAPI3, ddlAPI4);
        if (hdnSaveAutoVal.Value.Split(',').Length > 0)
        {
            string[] Data = hdnSaveAutoVal.Value.Split(',');
            ddlAPI1.SelectedValue = Data[0];
            if (Data[0].ToString() != "")
            {
                lblCommAPI1.Text = "Commission= " + objconnection.select_data_scalar_string("select Commission from tblRecharge_OperatorCode where APIID='" + Data[0] + "' and OperatorID='" + hdnOperatorID.Value + "'");
            }

            if (Data.Length > 1)
            {
                ddlAPI2.SelectedValue = Data[1];
                lblCommAPI2.Text = "Commission= " + objconnection.select_data_scalar_string("select Commission from tblRecharge_OperatorCode where APIID='" + Data[1] + "' and OperatorID='" + hdnOperatorID.Value + "'");

            }
            if (Data.Length > 2)
            {
                ddlAPI3.SelectedValue = Data[2];
                lblCommAPI3.Text = "Commission= " + objconnection.select_data_scalar_string("select Commission from tblRecharge_OperatorCode where APIID='" + Data[2] + "' and OperatorID='" + hdnOperatorID.Value + "'");

            }
            if (Data.Length > 3)
            {
                ddlAPI4.SelectedValue = Data[3];
                lblCommAPI4.Text = "Commission= " + objconnection.select_data_scalar_string("select Commission from tblRecharge_OperatorCode where APIID='" + Data[3] + "' and OperatorID='" + hdnOperatorID.Value + "'");

            }
        }
    }

    protected void rptOperator_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        DropDownList ddlAPI1 = (DropDownList)e.Item.FindControl("ddlAPI1");
        DropDownList ddlAPI2 = (DropDownList)e.Item.FindControl("ddlAPI2");
        DropDownList ddlAPI3 = (DropDownList)e.Item.FindControl("ddlAPI3");
        DropDownList ddlAPI4 = (DropDownList)e.Item.FindControl("ddlAPI4");
        if (e.CommandName.ToString() == "Save")
        {
            string str = "";
            if (ddlAPI1.SelectedIndex > 0)
            {
                str = ddlAPI1.SelectedValue;
            }
            if (ddlAPI2.SelectedIndex > 0)
            {
                str = str + "," + ddlAPI2.SelectedValue;
            }
            if (ddlAPI3.SelectedIndex > 0)
            {
                str = str + "," + ddlAPI3.SelectedValue;
            }
            if (ddlAPI4.SelectedIndex > 0)
            {
                str = str + "," + ddlAPI4.SelectedValue;
            }
            if (e.CommandName.ToString() == "Save")
            {
                int UpdateData = objconnection.update_data("update tblRecharge_Commission set Auto_System_APID='" + str + "' where CommissionID=" + Convert.ToInt32(e.CommandArgument.ToString()) + "");
            }
        }
    }
    [WebMethod]
    public static string GetCommission(Int32 PackageID, Int32 ActiveAPI, Int32 OperatorID)
    {
        string Commission = "0";
        try
        {
            cls_connection objconnection = new cls_connection();
            Commission = objconnection.select_data_scalar_string("select Commission from tblRecharge_OperatorCode where APIID='" + ActiveAPI + "' and OperatorID='" + OperatorID + "'");
        }
        catch (Exception ex)
        {
        }
        return (Commission == "" ? "0" : Commission);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        for (Int32 i = 0; i < rptOperator.Items.Count; i++)
        {
            string str = "";
            DropDownList ddlAPI1 = (DropDownList)rptOperator.Items[i].FindControl("ddlAPI1");
            DropDownList ddlAPI2 = (DropDownList)rptOperator.Items[i].FindControl("ddlAPI2");
            DropDownList ddlAPI3 = (DropDownList)rptOperator.Items[i].FindControl("ddlAPI3");
            DropDownList ddlAPI4 = (DropDownList)rptOperator.Items[i].FindControl("ddlAPI4");
            if (ddlAPI1.SelectedIndex > 0)
            {
                str = ddlAPI1.SelectedValue;
            }
            if (ddlAPI2.SelectedIndex > 0)
            {
                str = str + "," + ddlAPI2.SelectedValue;
            }
            if (ddlAPI3.SelectedIndex > 0)
            {
                str = str + "," + ddlAPI3.SelectedValue;
            }
            if (ddlAPI4.SelectedIndex > 0)
            {
                str = str + "," + ddlAPI4.SelectedValue;
            }
            HiddenField hdnCommisionVal = (HiddenField)rptOperator.Items[i].FindControl("hdnCommisionVal");
            {
                int UpdateData = objconnection.update_data("update tblRecharge_Commission set Auto_System_APID='" + str + "' where CommissionID=" + Convert.ToInt32(hdnCommisionVal.Value.ToString()) + "");
            }
        }
    }
    private void GetDataAmountSData()
    {
       //
    }
    protected void btnSumbitA_Click(object sender, EventArgs e)
    {
       
    }
}
