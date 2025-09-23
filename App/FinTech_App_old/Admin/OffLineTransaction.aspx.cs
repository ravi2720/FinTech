using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class Admin_OffLineTransaction : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    Page page;
    DataTable dtMember;
    string FormID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtEmployee"] != null)
        {
            dtMember = (DataTable)Session["dtEmployee"];
            if (Request.QueryString["FormID"] != null)
            {
                FormID = Request.QueryString["FormID"].ToString();
                DataTable dtForm = ObjConnection.select_data_dt("ManageOffLineTransaction 'GetAll'," + FormID + "");
                if (dtForm.Rows.Count > 0)
                {
                    divHeading.InnerHtml = dtForm.Rows[0]["ServiceName"].ToString() + "(<span class='btn btn-success'>" + dtForm.Rows[0]["Status"].ToString() + "</span>)";

                }
                CreateDynamicControls();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private DataTable CustomFields()
    {
        DataTable dt = new DataTable();
        dt = ObjConnection.select_data_dt("ManageOffLineTransaction 'GetAll'," + FormID + "");
        dt = JsonConvert.DeserializeObject<DataTable>(dt.Rows[0]["Data"].ToString());
        return dt;
    }

    public void CreateDynamicControls()
    {
        DataTable dt = new DataTable();
        dt = CustomFields();  //calling the function which describe the fieldname and fieldtype
        if (dt.Rows.Count > 0)
        {
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                TableCell td = new TableCell();
                TableCell td1 = new TableCell();
                TableRow tr = new TableRow();

                String FieldName = Convert.ToString(dt.Rows[i]["FieldName"]);
                String FieldType = Convert.ToString(dt.Rows[i]["FieldType"]);
                String FieldValue = Convert.ToString(dt.Rows[i]["Value"]);

                Label lbcustomename = new Label();
                lbcustomename.ID = "lb" + FieldName;
                lbcustomename.Text = FieldName;
                lbcustomename.ForeColor = System.Drawing.Color.Black;
                lbcustomename.Attributes.Add("style", " font-weight:bold;");
                td.Controls.Add(lbcustomename);
                tr.Controls.Add(td);

                if (FieldType.ToLower().Trim() == "textbox")
                {
                    Label txtcustombox = new Label();
                    txtcustombox.ID = "txt" + FieldName;
                    txtcustombox.ClientIDMode = ClientIDMode.Static;
                    txtcustombox.Text = dt.Rows[i]["Value"].ToString();

                    td1.Controls.Add(txtcustombox);
                }
                else if (FieldType.ToLower().Trim() == "fileupload")
                {
                    HyperLink txtcustombox = new HyperLink();
                    txtcustombox.NavigateUrl = "./images/Form/" + dt.Rows[i]["Value"].ToString();
                    txtcustombox.Target = "_blank";
                    txtcustombox.Text = FieldName;
                    td1.Controls.Add(txtcustombox);
                }
                else if (FieldType.ToLower().Trim() == "checkbox")
                {

                    Label txtcustombox = new Label();
                    txtcustombox.ID = "txt" + FieldName;
                    txtcustombox.ClientIDMode = ClientIDMode.Static;
                    txtcustombox.Text = dt.Rows[i]["Value"].ToString();

                    td1.Controls.Add(txtcustombox);
                }


                tr.Controls.Add(td1);
                tbldynamic.Rows.Add(tr);


                //Add button  after last record  

            }

        }

    }






    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (dllStatus.SelectedIndex > 0)
        {
            DataTable dt = new DataTable();
            dt = ObjConnection.select_data_dt("ManageOffLineTransaction 'GetAll'," + FormID + "");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Status"].ToString().ToUpper() == "PENDING")
                {
                    if (dllStatus.SelectedValue == "Success")
                    {
                        Int32 val = 0;
                        val = ObjConnection.update_data("update OffLineTransaction set Status='Success',Remark='" + txtRemark.Text + "' where id=" + dt.Rows[0]["ID"].ToString() + "");
                        if (val > 0)
                        {
                            ErrorShow.AlertMessage(page, "Form Accept Successfully", ConstantsData.CompanyName);
                        }
                    }
                    else if (dllStatus.SelectedValue == "Success")
                    {
                        string narration = "Service TransID-" + dt.Rows[0]["TransID"].ToString();
                        string Description = "" + dt.Rows[0]["ServiceName"] + " Service Applied With TransID-" + dt.Rows[0]["TransID"].ToString() + "";
                        DataTable dtval = ObjConnection.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dt.Rows[0]["loginid"].ToString() + "','" + dt.Rows[0]["Amount"].ToString() + "','Cr','Refund-" + narration + "','" + Description + "','" + ConstantsData.SOffline + "','" + dt.Rows[0]["TransID"].ToString() + "'");
                        if (dtval.Rows[0]["msrno"].ToString() == "1")
                        {
                            Int32 Val = ObjConnection.update_data("update OffLineTransaction set Status='failed',Remark='" + txtRemark.Text + "' where id=" + dt.Rows[0]["ID"].ToString() + "");
                            if (Val > 0)
                            {
                                ErrorShow.AlertMessage(page, "Form Rejected Successfully", ConstantsData.CompanyName);
                            }
                        }
                        
                    }
                }
                else
                {
                    ErrorShow.AlertMessage(page, "Already Clear", ConstantsData.CompanyName);
                }
            }
        }
    }
}