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
            if (Request.QueryString["PFID"] != null)
            {
                txtFromDate.Text = txtToDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
                Category();
            }
        }
    }

    private void Category()
    {
        DataTable dt = ObjData.select_data_dt("select * from ProductCategory where IsActive=1");
        if (dt.Rows.Count > 0)
        {
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "Name";
            ddlCategory.DataValueField = "ID";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- Select Category --", "0"));
        }
    }



    private void FillData()
    {
        try
        {
            dt = ObjData.select_data_dt("PROC_MANAGE_PRODUCT 'admin'," + ddlCategory.SelectedValue + ",'" + txtFromDate.Text + "','" + txtToDate.Text + "'");
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
        HiddenField hdnid = (HiddenField)e.Item.FindControl("hdnid");
        CheckBox chk = (CheckBox)e.Item.FindControl("chk");
        Button btn = (Button)e.Item.FindControl("btn");
        if (e.CommandName == "save")
        {


            int val = ObjData.update_data("INSERT INTO PRODUCTFunctionDetail(ProductFID, Type)SELECT ID, CategoryID FROM product WHERE ID = '" + chk.Checked + "'");


            if (val > 0)
            {
                Response.Write("<script>alert('Data Save')</script>");
            }


        }



        if (e.CommandName == "Active")
        {
            Active(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }
    }
    private void FillNews(int id)
    {
        dt = ObjData.select_data_dt("EXEC ManageProduct 'Get','" + id + "' ");
        //txtProductName.Text = dt.Rows[0]["ProductName"].ToString();
        //ckNewsDesc.Text = dt.Rows[0]["Description"].ToString();
        //ckDescShort.Text = dt.Rows[0]["ShortDescription"].ToString();
        //txtPrice.Text = dt.Rows[0]["Price"].ToString();
        //txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
        //txtDiscount.Text = dt.Rows[0]["Discount"].ToString();
        //txtGST.Text = dt.Rows[0]["GST"].ToString();
    }


    private void Active(int id)
    {
        int val = ObjData.update_data("ManageProduct @Action='IsActive',@ID=" + id + "");
        if (val > 0)
        {
            FillData();
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdnValues.Value))
        {
            DataTable dtVal = ObjData.select_data_dt("AddEditPRODUCTFunctionDetail '" + Request.QueryString["PFID"].ToString() + "','0','',1,'" + hdnValues.Value + "'");
            if (dtVal.Rows.Count > 0)
            {
                ErrorShow.AlertMessage(page, "Feature Add Successfully", "Success");
            }
        }
        else
        {
            ErrorShow.Error(page, "Select Product");
        }
    }
}