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
            txtFromDate.Text = txtToDate.Text = System.DateTime.Now.ToString("MM-dd-yyy");
            Category();
           // FillData();
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
            dt = ObjData.select_data_dt("PROC_MANAGE_PRODUCT 'admin'," + ddlCategory.SelectedValue+",'"+txtFromDate.Text+"','"+txtToDate.Text+ "',0");
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
        //HiddenField hdnid = (HiddenField)e.Item.FindControl("hdnid");
        //if (e.CommandName == "Edit")
        //{

           
        //    //hdnid.Value = e.CommandArgument.ToString();
        //    Response.Redirect("Product.aspx?PID=" + hdnid.Value + "");
        //    //FillNews(id: Convert.ToInt16(e.CommandArgument.ToString()));
        //}
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
}