using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_products : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public Company company;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {

    } 

    [WebMethod]
    public static string GetProductData(string MethodName, string Data)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("Select * from PRODUCT where id in (select * from dbo.Function_Split('" + Data + "',','))");
        return StaticMethod.SerializeToJSon(dt);
    }

    [WebMethod]
    public static string GetProductData()
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("FillProductCategory 'ParentCategory'");
        return StaticMethod.SerializeToJSon(dt);
    }




    [WebMethod]
    public static string ProductDetails(string Data)
    {
        cls_connection objDataAccess = new cls_connection();
        DataTable dt = objDataAccess.select_data_dt("select * from Product where ProductName = '" + Data + "'");
        return StaticMethod.SerializeToJSon(dt);
    }



    [WebMethod]
    public static string ProductSearch(string Data)
    {
        cls_connection objDataAccess = new cls_connection();
        DataTable dt = objDataAccess.select_data_dt("select * from PRODUCT where ProductName like '%" + Data + "%'");
        return StaticMethod.SerializeToJSon(dt);
    }




    [WebMethod]
    public static string GetAllCategory()
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("FillProductCategory 'LevelCategoryBind'");
        return StaticMethod.SerializeToJSon(dt);
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
    public static string GetCategoryProductData(string MethodName, Int32 ID)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("ManageProduct 'GetByCatID','" + ID + "'");
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
    public static string GetFeaturesData(string MethodName)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("select * from Product where isactive=1");
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