using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AssignTID : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Page page;
    public Company company;



    protected void Page_Load(object sender, EventArgs e)
    {

        page = HttpContext.Current.CurrentHandler as Page;
        dtMember = (DataTable)Session["dtEmployee"];
        company = Company.GetCompanyInfo();
        if (!IsPostBack)
        {
            FillMember();
            FillData();
        }
    }
    private void FillMember()
    {
        DataTable dtMember = ObjConnection.select_data_dt("select (LoginID + ' ( ' + Name + ' ) ')as Name,msrno from Member where isactive=1");
        Common.BindDropDown(ddlMember, dtMember, "Name", "msrno");
        ddlMember.Items.Insert(0, new ListItem("-- Select Member --", "0"));
    }
    protected void FillData()
    {


        dt = ObjConnection.select_data_dt("EXEC ManageAssignTID 'Get',0");

        if (dt.Rows.Count > 0)
        {
            RptData.DataSource = dt;
            RptData.DataBind();
        }
        else
        {
            RptData.DataSource = null;
            RptData.DataBind();

        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {



        DataTable dt = ObjConnection.select_data_dt("Exec AddEditAssignTID " + hdnRoleID.Value + ",'" + ddlMember.SelectedValue + "','" + txtAEPSID.Text + "','" + ddlStatus.SelectedValue + "','" + txtMobile.Text + "'");
        if (Convert.ToInt32(dt.Rows[0]["ID"].ToString()) > 0)
        {
            ErrorShow.AlertMessage(page, dt.Rows[0]["Status"].ToString(), ConstantsData.CompanyName);
            FillData();
        }



    }



    protected void RptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        if (e.CommandName == "Delete")
        {

            Delete(id: Convert.ToInt32(e.CommandArgument.ToString()));

        }
    }

    private void Delete(Int32 id)
    {
        int val = ObjConnection.update_data("ManageAssignTID 'IsDelete'," + id + "");
        if (val > 0)
        {
            FillData();
        }

    }
}