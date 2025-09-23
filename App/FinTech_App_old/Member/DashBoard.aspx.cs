using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_DashBoard : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public Company company;
    DataTable dtData;
    public DataTable dtMember = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                hdnAuth.Value = dtMember.Rows[0]["Msrno"].ToString();
                FillNews();
                LastTransaction();
                BindDashBoardBanner();

            }
        }
        else
        {
            Response.Redirect("default.aspx");
        }
    }



    private void LastTransaction()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Exec PROC_WALLET_REPORT 'EwalletTransactionTop'," + Convert.ToInt32(dtMember.Rows[0]["msrno"].ToString()) + "");
        if (dt.Rows.Count > 0)
        {
            rptData.DataSource = dt;
            rptData.DataBind();
        }
        else
        {
            rptData.DataSource = null;
            rptData.DataBind();
        }
    }

    private void BindDashBoardBanner()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("EXEC ManageBannerImage 'GetAllBYBannerType','1','" + company.MemberID + "'");
        if (dt.Rows.Count > 0)
        {
            rptDashBoardBanner.DataSource = dt;
            rptDashBoardBanner.DataBind();
        }
        else
        {
            rptDashBoardBanner.DataSource = null;
            rptDashBoardBanner.DataBind();
        }

    }

    protected void FillNews()
    {
        dtData = cls.select_data_dt("EXEC ManageNews 'GetActive','" + 0 + "','" + company.MemberID + "'");
        if (dtData.Rows.Count > 0)
        {
            rptDataNews.DataSource = dtData;
            rptDataNews.DataBind();
        }
        else
        {
            rptDataNews.DataSource = null;
            rptDataNews.DataBind();

        }
    }
    [WebMethod]
    public static List<ServiceTotalSelf> GetData(string MethodName, string Data, string Auth)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("ServiceTotalMemberWise '" + (Data == "" ? DateTime.Now.ToString("MM-dd-yyyy") : Data) + "'," + Auth + "");
        ServiceTotalSelf ServiceTotalSelf = new ServiceTotalSelf();
        List<ServiceTotalSelf> t = new List<ServiceTotalSelf>();
        t.Add(ServiceTotalSelf.GetServiceTotalInfo(dt));
        return t;
    }

    [WebMethod]
    public static string FindTransaction(string TransID)
    {
        DataTable dt = new DataTable();
        cls_connection Objconnection = new cls_connection();
        dt = Objconnection.select_data_dt("select convert(nvarchar(30),ADDDATE,106)ADDDATE,* from VIEW_EWALLETTRANSACTION where TRANSACTIONID = '" + TransID + "'");
        return ConstantsData.SerializeToJSon(dt);
    }
}