using System;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AEPSNonActiveMember : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public static string value = "";
    public static int i = 0;
    DataTable dtPanAPIDetail = new DataTable();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindRepeaterValues();
        }

    }
    public void BindRepeaterValues()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = cls.select_data_dt("select * from Member where bcregistrationID is null");
            if (dt.Rows.Count > 0)
            {
                gvtData.DataSource = dt;
                gvtData.DataBind();
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alertify.error('" + ex.Message.ToString() + "');", true);
        }
    }



}
