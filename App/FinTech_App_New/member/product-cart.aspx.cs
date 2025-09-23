using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_product_cart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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