using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_UploadData : System.Web.UI.Page
{
    public static string path = @"C:\Users\Soni Techno\Desktop\Admin8.xlsx";
    public static string excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //  ConvertExcelToDataTable("file/Admin..xlsx");
        }
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {

        if (!FileUpload1.HasFile)
        {
            try
            {
                //string path = string.Concat(Server.MapPath("~/Image/ColoreCode.xlsx"));//+ FileUpload1.FileName));

                //FileUpload1.SaveAs(path);

                // Connection String to Excel Workbook

                // string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);
                // string excelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";"+"Extended Properties="Excel 12.0;HDR=Yes";

                OleDbConnection connection = new OleDbConnection();

                connection.ConnectionString = excelConnectionString;

                OleDbCommand command = new OleDbCommand("select * from [Sheet1$]", connection);

                connection.Open();

                // Create DbDataReader to Data Worksheet

                DbDataReader dr = command.ExecuteReader();

                // SQL Server Connection String

                string sqlConnectionString = @"server=103.129.98.27;database=SoniRecharge;UId=sonitech_svdsdigitalpay;Pwd=^3Tf71cl";

                // Bulk Copy to SQL Server

                SqlBulkCopy bulkInsert = new SqlBulkCopy(sqlConnectionString);

                bulkInsert.DestinationTableName = "colormaster";

                bulkInsert.WriteToServer(dr);

                //  Label1.Text = "Ho Gaya";

            }

            catch (Exception ex)
            {

                //Label1.Text = ex.Message;

            }

        }
    }
}