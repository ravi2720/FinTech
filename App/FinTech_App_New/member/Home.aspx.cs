using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_Home : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();
    DataTable dt, dtMember;
    public ServiceTotalSelf ServiceTotal=new ServiceTotalSelf();
    public Company company;

    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            company = Company.GetCompanyInfo();
            dtMember = (DataTable)Session["dtMember"];
            page = HttpContext.Current.CurrentHandler as Page;
            if (!IsPostBack)
            {
                hdnAuth.Value = dtMember.Rows[0]["msrno"].ToString();
               // news.InnerHtml = ObjData.select_data_scalar_string("select top 1 Description from Important where IsActive=1 and msrno="+company.MemberID+"");
                //FillServiceData();
               // FillData();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
       
    }
    private void FillServiceData()
    {
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        DataTable dt = new DataTable();
        dt = ObjData.select_data_dt("ServiceTotalMemberWiseBYMsrno '" + dtMember.Rows[0]["Msrno"].ToString() + "'");
        ServiceTotal = ServiceTotalSelf.GetServiceTotalInfo(dt);
    }


    protected void FillData()
    {
        dt = ObjData.select_data_dt("EXEC ManageNews 'GetActive','" + 0 + "','" + company.MemberID + "'");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();
        }
        else
        {
            repeater1.DataSource = null;
            repeater1.DataBind();

        }
    }



    [WebMethod]
    public static string GetData(string MethodName)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("FillDropDown 'NewBBPSService',0");
        return ConstantsData.SerializeToJSon(dt);
    }

    [WebMethod]
    public static string GetOffLine(string MethodName)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("select * from OffLineServiceLink where isactive=1");
        return ConstantsData.SerializeToJSon(dt);
    }

    [WebMethod]
    public static string ServiceData(string MethodName,string Auth)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("FillDropDown 'ActiveServiceDashBoad'," + Auth + "");
        return ConstantsData.SerializeToJSon(dt);
    }

    [WebMethod]
    public static string ServiceDataAll(string MethodName, string Auth)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("GetActiveServiceAll " + Auth + "");
        return ConstantsData.SerializeToJSon(dt);
    }

    

    [WebMethod]
    public static string CommingSoonService(string MethodName, string Auth)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("FillDropDown 'CommingSoonService'," + Auth + "");
        return ConstantsData.SerializeToJSon(dt);
    }
   

}