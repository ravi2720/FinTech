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

public partial class Member_OffLineForm : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    Page page;
    DataTable dtMember;
    string FormID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (Request.QueryString["FormID"] != null)
            {
                FormID = Request.QueryString["FormID"].ToString();
                DataTable dtForm = ObjConnection.select_data_dt("select * from OffLineservice where ID=" + FormID + "");
                if (dtForm.Rows.Count > 0)
                {
                    divHeading.InnerHtml = dtForm.Rows[0]["Name"].ToString();
                    lblAmount.Text = dtForm.Rows[0]["Amount"].ToString();
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
        dt = ObjConnection.select_data_dt("ManageOffLineserviceField 'GetByFormID'," + FormID + "");
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
                String FieldValue = Convert.ToString(dt.Rows[i]["FieldValue"]);

                Label lbcustomename = new Label();
                lbcustomename.ID = "lb" + FieldName;
                lbcustomename.Text = FieldName;
                td.Controls.Add(lbcustomename);
                tr.Controls.Add(td);

                if (FieldType.ToLower().Trim() == "textbox")
                {
                    TextBox txtcustombox = new TextBox();
                    txtcustombox.ID = "txt" + FieldName;
                    txtcustombox.ClientIDMode = ClientIDMode.Static;
                    txtcustombox.Text = FieldValue;
                    txtcustombox.Attributes.Add("placeholder", "Please Enter Valid " + FieldName);
                    txtcustombox.MaxLength = Convert.ToInt32(dt.Rows[i]["MaxLen"].ToString());
                    if (Convert.ToBoolean(dt.Rows[i]["Mandatory"].ToString()))
                    {
                        txtcustombox.CssClass = "form-control Man";
                        //RequiredFieldValidator requiredFieldValidator = new RequiredFieldValidator();
                        //requiredFieldValidator.ControlToValidate = txtcustombox.ID;
                        //requiredFieldValidator.ErrorMessage = "Please Enter "+ FieldName;
                        //requiredFieldValidator.ValidationGroup = "LoginFrame";
                    }
                    else
                    {
                        txtcustombox.CssClass = "form-control";
                    }


                    td1.Controls.Add(txtcustombox);
                }
                else if (FieldType.ToLower().Trim() == "fileupload")
                {
                    FileUpload txtcustombox = new FileUpload();
                    txtcustombox.ID = "file" + FieldName;
                    txtcustombox.ClientIDMode = ClientIDMode.Static;

                    if (Convert.ToBoolean(dt.Rows[i]["Mandatory"].ToString()))
                    {
                        txtcustombox.CssClass = "form-control Man";
                    }
                    else
                    {
                        txtcustombox.CssClass = "form-control";
                    }
                    td1.Controls.Add(txtcustombox);
                }
                else if (FieldType.ToLower().Trim() == "checkbox")
                {
                    CheckBox chkbox = new CheckBox();
                    chkbox.ID = "chk" + FieldName;
                    if (FieldValue == "1")
                    {
                        chkbox.Checked = true;
                    }
                    else
                    {
                        chkbox.Checked = false;
                    }
                    chkbox.ClientIDMode = ClientIDMode.Static;

                    if (Convert.ToBoolean(dt.Rows[i]["Mandatory"].ToString()))
                    {
                        chkbox.CssClass = "form-control Man";
                    }
                    else
                    {
                        chkbox.CssClass = "form-control";
                    }
                    td1.Controls.Add(chkbox);
                }
                else if (FieldType.ToLower().Trim() == "radiobutton")
                {
                    RadioButtonList rbnlst = new RadioButtonList();
                    rbnlst.ID = "rbnlst" + FieldName;
                    rbnlst.Items.Add(new ListItem("Male", "1"));
                    rbnlst.Items.Add(new ListItem("Female", "2"));
                    rbnlst.ClientIDMode = ClientIDMode.Static;
                    if (FieldValue != String.Empty)
                    {
                        rbnlst.SelectedValue = FieldValue;
                    }
                    else
                    {
                        rbnlst.SelectedValue = "1";
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["Mandatory"].ToString()))
                    {
                        rbnlst.CssClass = "form-control Man";
                    }
                    else
                    {
                        rbnlst.CssClass = "form-control";
                    }
                    rbnlst.RepeatDirection = RepeatDirection.Horizontal;
                    td1.Controls.Add(rbnlst);
                }
                else if (FieldType.ToLower().Trim() == "dropdownlist")
                {
                    DropDownList ddllst = new DropDownList();
                    ddllst.ID = "ddl" + FieldName;
                    ddllst.Items.Add(new ListItem("Select", "0"));
                    ddllst.ClientIDMode = ClientIDMode.Static;
                    if (FieldName.ToLower().Trim() == "state")
                    {
                        ddllst.Items.Add(new ListItem("Alabama", "AL"));
                        ddllst.Items.Add(new ListItem("Alaska", "AK"));
                        ddllst.Items.Add(new ListItem("Arizona", "AZ"));
                        ddllst.Items.Add(new ListItem("California", "CA"));
                        ddllst.Items.Add(new ListItem("New York", "NY"));
                    }
                    else if (FieldName.ToLower().Trim() == "job")
                    {
                        ddllst.Items.Add(new ListItem("Developer", "1"));
                        ddllst.Items.Add(new ListItem("Tester", "2"));
                    }
                    if (FieldValue != String.Empty)
                    {
                        ddllst.SelectedValue = FieldValue;
                    }
                    else
                    {
                        ddllst.SelectedValue = "0";
                    }
                    if (Convert.ToBoolean(dt.Rows[i]["Mandatory"].ToString()))
                    {
                        ddllst.CssClass = "form-control Man";
                    }
                    else
                    {
                        ddllst.CssClass = "form-control";
                    }
                    td1.Controls.Add(ddllst);
                }
                tr.Controls.Add(td1);
                tbldynamic.Rows.Add(tr);


                //Add button  after last record  
                if (i == dt.Rows.Count - 1)
                {

                    td = new TableCell();
                    td1 = new TableCell();
                    tr = new TableRow();
                    Button btnSubmit = new Button();
                    btnSubmit.ID = "btnSubmit";
                    btnSubmit.Text = "Submit"; btnSubmit.CssClass = "btn btn-danger";
                    btnSubmit.Click += new System.EventHandler(btnSubmit_click);
                    btnSubmit.OnClientClick = "return ValidateForm();";
                    td.Controls.Add(btnSubmit);
                    tr.Cells.Add(td1);
                    tr.Cells.Add(td);
                    tbldynamic.Rows.Add(tr);
                    placeholder.Controls.Add(tbldynamic);


                }
            }

        }

    }

    public void Save()
    {
        DataTable dtFormValues = new DataTable();
        dtFormValues.Columns.Add("FieldType", typeof(String));
        dtFormValues.Columns.Add("FieldName", typeof(String));
        dtFormValues.Columns.Add("Value", typeof(String));

        DataTable dt = new DataTable();
        dt = CustomFields();
        if (dt.Rows.Count > 0)
        {
            for (Int32 i = 0; i < dt.Rows.Count; i++)
            {
                String FieldName = Convert.ToString(dt.Rows[i]["FieldName"]);
                String FieldType = Convert.ToString(dt.Rows[i]["FieldType"]);
                dtFormValues.NewRow();

                if (FieldType.ToLower().Trim() == "textbox")
                {
                    TextBox txtbox = (TextBox)placeholder.FindControl("txt" + FieldName);
                    if (txtbox != null)
                    {
                        dtFormValues.Rows.Add(FieldType, FieldName, txtbox.Text);
                    }
                }
                else if (FieldType.ToLower().Trim() == "checkbox")
                {
                    CheckBox checkbox = (CheckBox)placeholder.FindControl("chk" + FieldName);
                    if (checkbox != null)
                    {
                        dtFormValues.Rows.Add(FieldType, FieldName, checkbox.Checked ? "1" : "0");
                    }
                }
                else if (FieldType.ToLower().Trim() == "radiobutton")
                {
                    RadioButtonList radiobuttonlist = (RadioButtonList)placeholder.FindControl("rbnlst" + FieldName);
                    if (radiobuttonlist != null)
                    {
                        dtFormValues.Rows.Add(FieldType, FieldName, radiobuttonlist.SelectedValue);
                    }
                }
                else if (FieldType.ToLower().Trim() == "dropdownlist")
                {
                    DropDownList dropdownlist = (DropDownList)placeholder.FindControl("ddl" + FieldName);
                    if (dropdownlist != null)
                    {
                        dtFormValues.Rows.Add(FieldType, FieldName, dropdownlist.SelectedValue);
                    }
                }

                else if (FieldType.ToLower().Trim() == "fileupload")
                {
                    FileUpload fileUpload = (FileUpload)placeholder.FindControl("file" + FieldName);
                    if (fileUpload != null)
                    {
                        if (fileUpload.HasFile)
                        {
                            string InputFile = string.Empty;
                            InputFile = System.IO.Path.GetExtension(fileUpload.FileName);
                            string gd = Guid.NewGuid().ToString() + InputFile;
                            fileUpload.PostedFile.SaveAs(MapPath("./images/Form/") + gd);
                            string logo = gd.ToString();
                            dtFormValues.Rows.Add(FieldType, FieldName, logo);
                        }
                    }
                }
            }
            decimal MyBalance = 0;
            DataTable dtWalletBalance = new DataTable();
            dtWalletBalance = ObjConnection.select_data_dt("Exec PROC_WALLET_REPORT 'EwalletBalance', " + Convert.ToInt32(dtMember.Rows[0]["Msrno"]));
            if (dtWalletBalance.Rows.Count > 0)
            {
                MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["Balance"]);
            }
            DataTable dtForm = ObjConnection.select_data_dt("select * from OffLineservice where ID=" + FormID + "");
            if (dtForm.Rows.Count > 0)
            {
                if (MyBalance >= Convert.ToDecimal(dtForm.Rows[0]["Amount"].ToString())) // Verification fee
                {
                    string TransID = System.Guid.NewGuid().ToString().Replace("-", "");
                    string narration = "Service TransID-" + TransID;
                    string Description = "" + dtForm.Rows[0]["Name"] + " Service Applied With TransID-" + TransID + "";
                    DataTable dtval = ObjConnection.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"].ToString() + "','" + dtForm.Rows[0]["Amount"].ToString() + "','DR','" + narration + "','" + Description + "','" + ConstantsData.SOffline + "','" + TransID + "'");
                    if (dtval.Rows[0]["msrno"].ToString() == "1")
                    {
                        string PostData = DataTableToJSONWithJSONNet(dtFormValues);
                       
                        Int32 Val = ObjConnection.update_data("AddEditOffLineTransaction '"+ FormID + "',"+ dtMember.Rows[0]["Msrno"].ToString() + ",'" + PostData + "','" + TransID + "',"+ dtForm.Rows[0]["Amount"].ToString() + ",0,'Pending'");
                        if (Val > 0)
                        {
                            ErrorShow.AlertMessage(page, "Form Submit Successfully",ConstantsData.CompanyName);
                        }
                        else
                        {
                            ErrorShow.AlertMessage(page, "Try After SomeTime", ConstantsData.CompanyName);
                        }
                    }
                    else
                    {
                        ErrorShow.AlertMessage(page, "low Balance", ConstantsData.CompanyName);
                    }
                }
                else
                {
                    ErrorShow.AlertMessage(page, "low Balance", ConstantsData.CompanyName);
                }
            }
        }
    }

    public string DataTableToJSONWithJSONNet(DataTable table)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
    }
    protected void btnSubmit_click(object sender, EventArgs e)
    {
        Save();


    }

}