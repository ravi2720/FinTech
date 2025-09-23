using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_RechargeHistory : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();

    DataTable dt = new DataTable();
    int Val = 0;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtFromDate.Text = txtToDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");

            FillData();
        }
    }




    private void FillData()
    {
        try
        {
            dt = ObjData.select_data_dt("select * from ProductFunction where isactive=1");
            rptDataRecharge.DataSource = dt;
            rptDataRecharge.DataBind();
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        FillData();
    }

    protected void rptDataRecharge_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "FP")
        {
            hdnFpID.Value = e.CommandArgument.ToString();
            GetPRODUCTFunctionDetail();
            divfeature.Visible = true;
        }
    }
    private void FillNews(int id)
    {
        dt = ObjData.select_data_dt("EXEC ManageProductFunction 'Get','" + id + "' ");
    }


    private void Active(int id)
    {
        int val = ObjData.update_data("ManageProductFunction @Action='IsActive',@ID=" + id + "");
        if (val > 0)
        {
            FillData();
        }

    }

    protected void LinkButton1_Command(object sender, CommandEventArgs e)
    {
        if (e.CommandName == "click")
        {
        }
    }


    protected void rptDataRecharge_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtfeature.Text))
        {
            DataTable dtVal = ObjData.select_data_dt(
                "AddEditPRODUCTFunctionDetail "
                + hdnFpEditID.Value + ",'"
                + hdnFpID.Value + "','"
                + txtfeature.Text.Replace("'", "''") + "',1,'"
                + bgcolor.Value.Replace("'", "''") + "','"
                + txtColor.Value.Replace("'", "''") + "'");

            if (dtVal.Rows.Count > 0)
            {
                string status = dtVal.Rows[0]["Status"].ToString();
                ErrorShow.AlertMessageWithRedirect(Page, "Feature Added Successfully", "ProductFunctionDetail.aspx?PFID=" + status + "&Type=1", "Success");
            }
        }
        else
        {
            ErrorShow.Error(Page, "Enter Feature Product Name");
        }
    }


    protected void rptFData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if(e.CommandName== "Edit")
        {
            hdnFpEditID.Value = e.CommandArgument.ToString();
            DataTable dtFPData = ObjData.select_data_dt("ManagePRODUCTFunctionDetail 'GetData','"+e.CommandArgument.ToString()+"'");
            if (dtFPData.Rows.Count > 0)
            {
                txtfeature.Text = dtFPData.Rows[0]["Header"].ToString();
                txtColor.Value = dtFPData.Rows[0]["TextColor"].ToString();
                bgcolor.Value = dtFPData.Rows[0]["BackGroundColor"].ToString();
            }
        }
    }

    private void GetPRODUCTFunctionDetail()
    {
        DataTable dtFPData = ObjData.select_data_dt("ManagePRODUCTFunctionDetail 'GetData',0");
        if (dtFPData.Rows.Count > 0)
        {
            rptFData.DataSource = dtFPData;
            rptFData.DataBind();
        }
    }

}