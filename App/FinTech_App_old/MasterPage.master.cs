using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    public Company company;
    DataTable dt;
    cls_connection cls = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        if (!IsPostBack)
        {
            BindNews();
        }
    }

    private void BindNews()
    {
        dt= cls.select_data_dt("EXEC ManageNews 'GetActive',0,'" + Company.GetCompanyInfo().ID + "'");
        rptnewData.DataSource = dt;
        rptnewData.DataBind();
    }
}
