using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_profile : System.Web.UI.Page
{
    public Company company;
    public DataTable dtMember = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {

            }
        }
        else
        {
            Response.Redirect("default.aspx");

        }
    }

    [WebMethod(EnableSession = true)]
    public static string CheckAuthentication(string Pattern)
    {
        cls_connection cls = new cls_connection();
        Int32 val = cls.select_data_scalar_int("select count(*) from Member where Pattern='" + Pattern + "' and id=2");
        return (val > 0 ? APIResponse.SuccessMessage("Match Successfully") : APIResponse.ErrorMessage("Pattern Not Match"));
    }

    [WebMethod(EnableSession = true)]
    public static string CheckPass(string MethodName, string Data)
    {
        cls_connection cls = new cls_connection();
        if (HttpContext.Current.Session["dtMember"] != null)
        {
            DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
            Int32 val = cls.select_data_scalar_int("select count(*) from Member where Password=HASHBYTES('SHA2_512', " + Data + "+CAST(Salt AS NVARCHAR(36))) and msrno=" + dtMember.Rows[0]["Msrno"].ToString() + "");
            return (val > 0 ? APIResponse.SuccessMessage("Password Match") : APIResponse.ErrorMessage("Password Not Match"));
        }
        else
        {
            return APIResponse.ErrorMessage("Member Not Login");
        }
    }

    [WebMethod(EnableSession = true)]
    public static string ChangePass(string MethodName, string OLD, string New)
    {
        cls_connection cls = new cls_connection();
        if (HttpContext.Current.Session["dtMember"] != null)
        {
            DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
            HttpContext.Current.Session["dtMember"] = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno = " + Convert.ToInt32(dtMember.Rows[0]["msrno"]) + "");

            int val = cls.update_data("exec PROC_MANAGE_PASSWROD " + Convert.ToInt64(dtMember.Rows[0]["Msrno"]) + ",'CHANGEPWD','" + New + "'");
            return (val > 0 ? APIResponse.SuccessMessage("Password Changed Successfully. Please Relogin Your Panel") : APIResponse.ErrorMessage("Password Not Changes"));
        }
        else
        {
            return APIResponse.ErrorMessage("Member Not Login");
        }
    }

    [WebMethod(EnableSession = true)]
    public static string CheckPin(string MethodName, string Data)
    {
        cls_connection cls = new cls_connection();
        if (HttpContext.Current.Session["dtMember"] != null)
        {
            if (!string.IsNullOrEmpty(Data))
            {
                DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
                Int32 val = cls.select_data_scalar_int("select count(*) from Member where LoginPin='" + Data + "' and msrno=" + dtMember.Rows[0]["Msrno"].ToString() + "");
                return (val > 0 ? APIResponse.SuccessMessage("PIN Match") : APIResponse.ErrorMessage("PIN Not Match"));
            }
            else
            {
                return APIResponse.ErrorMessage("Please Enter Valid Pin Number");
            }
        }
        else
        {
            return APIResponse.ErrorMessage("Member Not Login");
        }
    }

    [WebMethod(EnableSession = true)]
    public static string ChangePin(string MethodName, string OLD, string New)
    {
        cls_connection cls = new cls_connection();
        if (HttpContext.Current.Session["dtMember"] != null)
        {
            if (!string.IsNullOrEmpty(New))
            {
                DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
                int val = cls.update_data("exec PROC_MANAGE_PASSWROD " + Convert.ToInt64(dtMember.Rows[0]["Msrno"]) + ",'CHANGEPIN','" + New + "'");
                return (val > 0 ? APIResponse.SuccessMessage("PIN Changed Successfully") : APIResponse.ErrorMessage("PIN Not Changes"));
            }
            else
            {
                return APIResponse.ErrorMessage("Please Enter Valid Pin Number");
            }
        }
        else
        {
            return APIResponse.ErrorMessage("Member Not Login");
        }
    }

    [WebMethod(EnableSession = true)]
    public static string GetServiceList()
    {
        cls_connection cls = new cls_connection();
        if (HttpContext.Current.Session["dtMember"] != null)
        {

            DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
            DataTable dt = cls.select_data_dt("PROC_ActiveService " + dtMember.Rows[0]["Msrno"].ToString() + "");
            return JsonConvert.SerializeObject(dt);
        }
        else
        {
            return APIResponse.ErrorMessage("Member Not Login");
        }
    }

    [WebMethod(EnableSession = true)]
    public static string GetBankAccountList()
    {
        cls_connection cls = new cls_connection();
        if (HttpContext.Current.Session["dtMember"] != null)
        {

            DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
            DataTable dt = cls.select_data_dt("select * from VIEW_MEMBERBANKDETAILS where msrno = " + Convert.ToInt32(dtMember.Rows[0]["Msrno"]) + "");
            return JsonConvert.SerializeObject(dt);
        }
        else
        {
            return APIResponse.ErrorMessage("Member Not Login");
        }
    }

    [WebMethod(EnableSession = true)]
    public static string GetKYCList()
    {
        cls_connection cls = new cls_connection();
        if (HttpContext.Current.Session["dtMember"] != null)
        {

            DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];
            DataTable dt = cls.select_data_dt("select * from MemberKYCDocument where msrno = " + Convert.ToInt32(dtMember.Rows[0]["Msrno"]) + "");
            return JsonConvert.SerializeObject(dt);
        }
        else
        {
            return APIResponse.ErrorMessage("Member Not Login");
        }
    }
}