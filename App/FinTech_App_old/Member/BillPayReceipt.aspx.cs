using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Data;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Services;

public partial class Member_BillPayReceipt : System.Web.UI.Page
{
    public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        if (!IsPostBack)
        {
            if (Request.QueryString["TransID"] != null)
            {

            }
        }
    }


    [WebMethod]
    public static string BillData(string MethodName, string TransID)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("Exec PROC_GET_BILLPAY_DETAILS '" + TransID + "'");
        return ConstantsData.SerializeToJSon(dt);
    }

    [WebMethod]
    public static string UPITransferData(string MethodName, string TransID)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("Exec GetDataUPITransfer '" + TransID + "'");
        return ConstantsData.SerializeToJSon(dt);
    }

    [WebMethod]
    public static string AEPSData(string MethodName, string TransID)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("Exec ManageAEPS_History '" + TransID + "'");
        return ConstantsData.SerializeToJSon(dt);
    }
}