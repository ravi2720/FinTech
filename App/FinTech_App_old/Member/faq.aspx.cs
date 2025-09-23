using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_faq : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    public Company company;

    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        if (!IsPostBack)
        {
            FillData();


        }
    }
    protected void FillData()
    {
        dt = cls.select_data_dt("EXEC ManageFaq 'Get',0,'" + company.ID + "'");
        if (dt.Rows.Count > 0)
        {
            RptFaq.DataSource = dt;
            RptFaq.DataBind();
        }
        else
        {
            RptFaq.DataSource = null;
            RptFaq.DataBind();

        }
    }
}