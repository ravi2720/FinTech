using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_product_details : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static string GetCategoryData(string MethodName, Int32 ID)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("ManageProduct 'Get','" + ID + "'");
        return StaticMethod.SerializeToJSon(dt);
    }



    [WebMethod]
    public static string GetRealtedCategoryProductData(string MethodName, Int32 ID)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("ManageProduct 'GetRealtedByCatID','" + ID + "'");
        return StaticMethod.SerializeToJSon(dt);
    }

    [WebMethod]
    public static string AddToCart(AddToCart addToCart)
    {
        Int32 val = 0;
        cls_connection Objconnection = new cls_connection();
        val = Objconnection.update_data("AddEditAddToCart " + addToCart.Msrno + ",'" + addToCart.ID + "','" + addToCart.Quantity + "'");
        return StaticMethod.SuccessResponse("Add To Cart Successfully");
    }

    [WebMethod]
    public static string DeleteAddToCart(AddToCart addToCart)
    {
        Int32 val = 0;
        cls_connection Objconnection = new cls_connection();
        val = Objconnection.update_data("ManageAddToCart 'Delete'," + addToCart.Msrno + ",'" + addToCart.ID + "'");
        return StaticMethod.SuccessResponse("Delete Successfully");
    }
}