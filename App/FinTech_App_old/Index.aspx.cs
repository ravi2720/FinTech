using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public Company company;
    DataTable dt;

    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        BindDashBoardBanner();
    }
    private void BindDashBoardBanner()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("EXEC ManageBannerImage 'GetAllBYBannerType','1',"+company.MemberID+"");
        if (dt.Rows.Count > 0)
        {
            rptDashBoardBanner.DataSource = rptDashBoardBanner.DataSource = dt;
            rptDashBoardBanner.DataBind(); rptDashBoardBanner.DataBind();
        }
        else
        {
            rptDashBoardBanner.DataSource = null;
            rptDashBoardBanner.DataBind();
        }

    }
}