using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Dashboard : System.Web.UI.Page
{
    public ServiceTotalSelf ServiceTotal;
    cls_connection Objconnection = new cls_connection();
    DataTable dtMember;
    public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        if (Session["dtEmployee"] != null)
        {
            if (!IsPostBack)
            {
                ServiceTotal = new ServiceTotalSelf();
                //  txtFromDateA.Text = DateTime.Now.ToString("MM-dd-yyyy");
            }
            dtMember = (DataTable)Session["dtEmployee"];
            // FillPersonalInfo();
            // FillServiceData();
            // FillRequestNotification();
            FillRoleCount();

        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    private void FillRoleCount()
    {
        DataTable dt = new DataTable();
        dt = Objconnection.select_data_dt("GetMemberRoleCount");
        rptRoleData.DataSource = dt;
        rptRoleData.DataBind();
    }

    private void FillPersonalInfo()
    {
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        DataTable dt = new DataTable();
        // dt = Objconnection.select_data_dt("PersonalDetails " + dtMember.Rows[0]["Msrno"].ToString() + "");
        // personalDetails = PersonalDetails.GetCompanyInfo(dt);

    }
    private void FillRequestNotification()
    {
        DataTable dt = new DataTable();
        dt = Objconnection.select_data_dt("PROC_GET_REQUEST_NOTIFICATION 'REQUEST'");
        rptDataRequest.DataSource = dt;
        rptDataRequest.DataBind();
    }
    private void FillServiceData()
    {
        string url = HttpContext.Current.Request.Url.AbsoluteUri;
        DataTable dt = new DataTable();
        // dt = Objconnection.select_data_dt("ServiceTotalMemberWise '" + txtFromDateA.Text + "'");
        ServiceTotal = ServiceTotalSelf.GetServiceTotalInfo(dt);
    }

    [WebMethod]
    public static string GetDataCount(string MethodName, string Data)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        //dt = Objconnection.select_data_dt("ServiceTotalMemberWiseCount '" + (Data == "" ? DateTime.Now.ToString("yyyy-MM-dd") : Data) + "'");
        //   string JSONString = JsonConvert.SerializeObject(dt);
        return "";
    }
    protected void btnMSendOTP_Click(object sender, EventArgs e)
    {
        //
    }

    protected void btnESendOTP_Click(object sender, EventArgs e)
    {
        //
    }

    protected void txtFromDateA_TextChanged(object sender, EventArgs e)
    {
        FillServiceData();
    }

    [WebMethod]
    public static string GetRedirectUrl(string keycode)
    {
        string url = "";
        try
        {
            cls_connection objDataAccess = new cls_connection();
            url = objDataAccess.select_data_scalar_string("select pageurl from TBL_SHORT_CUT_KEYWORD_PAGEURL where isactive=1 and SHORTCUTKETVAL='" + keycode + "'");
        }
        catch (Exception ex)
        {

        }
        return url;
    }


    [WebMethod]
    public static string MemberDetails(string Data)
    {
        cls_connection objDataAccess = new cls_connection();
        DataTable dtMember = objDataAccess.select_data_dt("select Name,LoginID,MainBalance,AepsBalance,Msrno,AEPSStatus from VIEW_MEMBERDETAILS where LoginID = '" + Data + "'");
        return ConstantsData.SerializeToJSon(dtMember);
    }


    [WebMethod]
    public static string MemberSearch(string Data)
    {
        cls_connection objDataAccess = new cls_connection();
        DataTable dtMember = objDataAccess.select_data_dt("select Name,LoginID,Msrno from VIEW_MEMBERDETAILS where Mobile like '%"+ Data + "%'");
        return ConstantsData.SerializeToJSon(dtMember);
    }



    [WebMethod]
    public static List<ServiceTotalSelf> GetData(string MethodName, string Data)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("ServiceTotalMemberWise '" + (Data == "" ? DateTime.Now.ToString("MM-dd-yyyy") : Data) + "',1");
        ServiceTotalSelf ServiceTotalSelf = new ServiceTotalSelf();
        List<ServiceTotalSelf> t = new List<ServiceTotalSelf>();
        t.Add(ServiceTotalSelf.GetServiceTotalInfo(dt));
        return t;

    }

}