using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_ContentMasterFLoan : System.Web.UI.Page
{
    DataAccess objaccess = new DataAccess();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getdate();
        }
    }
    private void getdate()
    {
        dt = objaccess.GetDataTable("Select * from TBL_CONTENT_MASTER");
        txttitle.Text = dt.Rows[0]["Title"].ToString();
        txtcontent.Text = dt.Rows[0]["Content"].ToString();
    }
    private void insertdata()
    {
        try
        {
            string filename = "";
            if (flupld.HasFile)
            {
                filename = Path.Combine(Server.MapPath("./images/Loan"), flupld.FileName);
                flupld.SaveAs(filename);
                filename = flupld.PostedFile.FileName;
            }
            int result = objaccess.ExecuteQuery("EXEC DBO.SP_CONTENT_MASTER '" + txttitle.Text + "','" + txtcontent.Text + "','" + filename.ToString() + "'");
            if (result > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Inserted Successfully..!')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('Something Went Wrong..!')", true);
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnsearch_Click(object sender, EventArgs e)
    {
        insertdata();
    }
}