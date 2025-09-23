using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class about_us : System.Web.UI.Page
{
    public Company company;
    DataTable dt;
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        if (!IsPostBack)
        {
            BindTermCondition();
        }
    }

    private void BindTermCondition()
    {
        dt = cls.select_data_dt("ManageSecurity 'GetActive',0");
        if (dt.Rows.Count > 0)
        {
            rptDataT.DataSource = dt;
            rptDataT.DataBind();
        }
    }
}