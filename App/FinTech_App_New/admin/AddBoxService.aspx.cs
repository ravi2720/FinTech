using CKEditor.NET;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Admin_AddBoxService : System.Web.UI.Page
{
    cls_connection Objconnection = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
            if (Request.QueryString["ID"] != null)
            {
                BindData();
                GetEditData();
            }

        }
    }

    private void GetEditData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Objconnection.select_data_dt("select * from BoxList where ID=" + Request.QueryString["ID"].ToString() + "");
            dllBoxType.SelectedValue = dt.Rows[0]["TypeID"].ToString();
            GetDataProperty();

        }
        catch (Exception ex)
        {
        }
    }


    private void BindData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Objconnection.select_data_dt("select * from TYPEBOX");
            dllBoxType.DataSource = dt;
            dllBoxType.DataTextField = "Name";
            dllBoxType.DataValueField = "ID";
            dllBoxType.DataBind();
            dllBoxType.Items.Insert(0, new ListItem("Select", "0"));


            dt = Objconnection.select_data_dt("select * from SizeBox");
            dllSize.DataSource = dt;
            dllSize.DataTextField = "Name";
            dllSize.DataValueField = "Name";
            dllSize.DataBind();
            dllSize.Items.Insert(0, new ListItem("Select  Size For Box", "0"));



        }
        catch (Exception ex)
        {
        }
    }


    private void GetDataProperty()
    {
        try
        {
            if (Request.QueryString["ID"] == null)
            {
                DataTable dt = new DataTable();
                dt = Objconnection.select_data_dt("select * from TYPEBOX_Property where TYPEBOXID=" + dllBoxType.SelectedValue + "");
                if (dt.Rows.Count > 0)
                {
                    if (dllBoxType.SelectedValue == "3")
                    {
                        HtmlGenericControl newdiv = new HtmlGenericControl("DIV");
                        HtmlGenericControl newdivChild = new HtmlGenericControl("select");
                        newdiv.Attributes.Add("class", "form-group");
                        newdivChild.Attributes.Add("class", "form-control ValData");
                        newdivChild.Attributes.Add("id", "selectElement");
                        newdivChild.Attributes.Add("data-type", "Category");


                        newdiv.Controls.Add(newdivChild);
                        plnData.Controls.Add(newdiv);
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HtmlGenericControl newdiv = new HtmlGenericControl("DIV");
                        HtmlGenericControl newstrong = new HtmlGenericControl("strong");
                        newdiv.Attributes.Add("class", "form-group");
                        newstrong.InnerHtml = dt.Rows[i]["name"].ToString();
                        newstrong.Attributes.Add("style", "color:black");
                        if (dt.Rows[i]["type"].ToString().ToUpper() == "TEXT")
                        {

                            TextBox txt = new TextBox();
                            txt.Attributes.Add("class", "form-control ValData");
                            txt.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());
                            if (dt.Rows[i]["name"].ToString() == "Product-Color")
                            {
                                txt.Attributes.Add("class", "form-control ValData Color");
                            }
                            if (dt.Rows[i]["name"].ToString() == "Product-Size")
                            {

                                txt.Attributes.Add("class", "form-control ValData Size");

                            }
                            txt.Attributes.Add("data-set", "text");
                            txt.Attributes.Add("data-man", dt.Rows[i]["Man"].ToString());
                            if (dt.Rows[i]["name"].ToString().ToUpper() == "SIZE")
                            {

                                txt.Attributes.Add("style", "pointer-events: none");

                                txt.Attributes.Add("ID", "SizeData");

                            }


                            newdiv.Controls.Add(newstrong);
                            newdiv.Controls.Add(txt);
                        }

                        if (dt.Rows[i]["type"].ToString().ToUpper() == "TEXTCK")
                        {

                            CKEditorControl txt = new CKEditorControl();
                            txt.Attributes.Add("class", "form-control ValData");
                            txt.Width = 150;
                            txt.Width = 400;
                            txt.ID = dt.Rows[i]["name"].ToString().Replace(" ", "");
                            txt.BasePath = "~/ckeditor/";
                            txt.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());
                            txt.Attributes.Add("data-CK", "1");
                            txt.Attributes.Add("data-set", "text");
                            txt.ClientIDMode = ClientIDMode.Static;
                            newdiv.Controls.Add(newstrong);
                            newdiv.Controls.Add(txt);


                        }

                        if (dt.Rows[i]["type"].ToString().ToUpper() == "COLOR")
                        {

                            TextBox txt = new TextBox();
                            txt.Attributes.Add("class", "form-control ValData");
                            txt.Attributes.Add("type", "color");
                            txt.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());
                            txt.Attributes.Add("data-set", "text");

                            newdiv.Controls.Add(newstrong);
                            newdiv.Controls.Add(txt);
                        }
                        if (dt.Rows[i]["type"].ToString().ToUpper() == "CHECKBOX")
                        {

                            CheckBox txt = new CheckBox();
                            txt.Attributes.Add("class", "form-control ValData");
                            txt.Attributes.Add("type", "checkbox");
                            txt.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());
                            txt.Attributes.Add("data-set", "checkbox");

                            newdiv.Controls.Add(newstrong);
                            newdiv.Controls.Add(txt);
                        }
                        if (dt.Rows[i]["type"].ToString().ToUpper() == "IMG")
                        {
                            newdiv.Controls.Add(newstrong);


                            FileUpload file = new FileUpload();
                            file.Attributes.Add("ID", "fupload");
                            file.Attributes.Add("onchange", "change()");

                            file.Attributes.Add("class", "form-control ValData");
                            file.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());


                            newdiv.Controls.Add(file);
                        }

                        plnData.Controls.Add(newdiv);
                    }

                }
                //if (dllBoxType.SelectedValue == "3")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "LoadData();", true);
                //}
                if (dllBoxType.SelectedValue == "3")
                {
                    btnVariants.Visible = true;
                    btnVariantsPrice.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "LoadData();", true);
                }
            }
            else
            {
                dllBoxType.Enabled = false;
                DataTable dt = new DataTable();
                DataTable dtEditData = new DataTable();
                dt = Objconnection.select_data_dt("select * from TYPEBOX_Property where TYPEBOXID=" + dllBoxType.SelectedValue + "");
                dtEditData = Objconnection.select_data_dt("select * from BoxList where ID=" + Request.QueryString["ID"].ToString() + "");
                if (dt.Rows.Count > 0)
                {
                    JArray jBoxProperty = JArray.Parse(dtEditData.Rows[0]["BoxProperty"].ToString());
                    if (dllBoxType.SelectedValue == "3")
                    {
                        HtmlGenericControl newdiv = new HtmlGenericControl("DIV");
                        HtmlGenericControl newdivChild = new HtmlGenericControl("select");
                        newdiv.Attributes.Add("class", "form-group");
                        newdivChild.Attributes.Add("class", "form-control ValData");
                        newdivChild.Attributes.Add("id", "selectElement");
                        newdivChild.Attributes.Add("data-type", "Category");


                        newdiv.Controls.Add(newdivChild);
                        plnData.Controls.Add(newdiv);
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        HtmlGenericControl newdiv = new HtmlGenericControl("DIV");
                        HtmlGenericControl newstrong = new HtmlGenericControl("strong");
                        newdiv.Attributes.Add("class", "form-group");
                        newstrong.InnerHtml = dt.Rows[i]["name"].ToString();
                        newstrong.Attributes.Add("style", "color:black");
                        if (dt.Rows[i]["type"].ToString().ToUpper() == "TEXT")
                        {

                            TextBox txt = new TextBox();
                            txt.Attributes.Add("class", "form-control ValData");
                            txt.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());
                            if (dt.Rows[i]["name"].ToString() == "Product-Color")
                            {
                                txt.Attributes.Add("class", "form-control ValData Color");
                            }
                            if (dt.Rows[i]["name"].ToString() == "Product-Size")
                            {
                                txt.Attributes.Add("class", "form-control ValData Size");
                            }

                            txt.Attributes.Add("data-man", dt.Rows[i]["Man"].ToString());

                            txt.Attributes.Add("data-set", "text");
                            if (dt.Rows[i]["name"].ToString().ToUpper() == "SIZE")
                            {
                                txt.Attributes.Add("style", "pointer-events: none");

                                txt.Attributes.Add("id", "SizeData");
                                dllSize.SelectedValue = jBoxProperty[0][dt.Rows[i]["name"].ToString()].ToString();
                            }
                            //txt.Attributes.Add("style", "pointer-events: none");
                            txt.Text = (jBoxProperty[0][dt.Rows[i]["name"]] != null ? jBoxProperty[0][dt.Rows[i]["name"].ToString()].ToString() : "");
                            dllSize.SelectedValue = (jBoxProperty[0][dt.Rows[i]["name"]] != null ? jBoxProperty[0][dt.Rows[i]["name"].ToString()].ToString() : "");
                            newdiv.Controls.Add(newstrong);
                            newdiv.Controls.Add(txt);
                        }

                        if (dt.Rows[i]["type"].ToString().ToUpper() == "TEXTCK")
                        {

                            CKEditorControl txt = new CKEditorControl();
                            txt.Attributes.Add("class", "form-control ValData");
                            txt.Width = 150;
                            txt.Width = 400;
                            txt.ID = dt.Rows[i]["name"].ToString().Replace(" ", "");
                            txt.BasePath = "~/ckeditor/";
                            txt.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());
                            txt.Attributes.Add("data-CK", "1");
                            txt.Attributes.Add("data-man", dt.Rows[i]["Man"].ToString());

                            txt.Attributes.Add("data-set", "text");
                            txt.Text = jBoxProperty[0][dt.Rows[i]["name"].ToString()].ToString();

                            txt.ClientIDMode = ClientIDMode.Static;
                            newdiv.Controls.Add(newstrong);
                            newdiv.Controls.Add(txt);


                        }

                        if (dt.Rows[i]["type"].ToString().ToUpper() == "COLOR")
                        {

                            TextBox txt = new TextBox();
                            txt.Attributes.Add("class", "form-control ValData");
                            txt.Attributes.Add("type", "color");
                            txt.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());
                            txt.Attributes.Add("data-set", "text");
                            txt.Text = jBoxProperty[0][dt.Rows[i]["name"].ToString()].ToString();
                            txt.Attributes.Add("data-man", dt.Rows[i]["Man"].ToString());

                            newdiv.Controls.Add(newstrong);
                            newdiv.Controls.Add(txt);
                        }
                        if (dt.Rows[i]["type"].ToString().ToUpper() == "CHECKBOX")
                        {

                            CheckBox txt = new CheckBox();
                            txt.Attributes.Add("class", "form-control ValData");
                            txt.Attributes.Add("type", "checkbox");
                            txt.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());
                            txt.Attributes.Add("data-set", "checkbox");
                            txt.Checked = (jBoxProperty[0][dt.Rows[i]["name"]] != null ? Convert.ToBoolean(jBoxProperty[0][dt.Rows[i]["name"].ToString()].ToString()) : false);
                            txt.Attributes.Add("data-man", dt.Rows[i]["Man"].ToString());

                            newdiv.Controls.Add(newstrong);
                            newdiv.Controls.Add(txt);
                        }
                        if (dt.Rows[i]["type"].ToString().ToUpper() == "IMG")
                        {
                            newdiv.Controls.Add(newstrong);

                            Image img = new Image();
                            img.ImageUrl = ".././images/" + jBoxProperty[0]["Image"].ToString().Replace("C:\\fakepath\\", "");
                            img.Height = 200;
                            img.Attributes.Add("class", "pull-right");
                            img.Attributes.Add("ID", "imgData");
                            img.Attributes.Add("FileName", jBoxProperty[0]["Image"].ToString().Replace("C:\\fakepath\\", ""));
                            img.ClientIDMode = ClientIDMode.Static;
                            newdiv.Controls.Add(img);

                            FileUpload file = new FileUpload();
                            file.Attributes.Add("ID", "fupload");
                            file.Attributes.Add("onchange", "change()");

                            file.Attributes.Add("class", "form-control ValData");
                            file.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());
                            if (string.IsNullOrEmpty(jBoxProperty[0]["Image"].ToString()))
                            {

                                file.Attributes.Add("data-man", dt.Rows[i]["Man"].ToString());
                            }


                            newdiv.Controls.Add(file);
                        }

                        plnData.Controls.Add(newdiv);
                    }
                    if (dllBoxType.SelectedValue == "3")
                    {
                        btnVariants.Visible = true;
                        btnVariantsPrice.Visible = true;
                        if (jBoxProperty[0]["SizeColorDetails"] != null)
                        {
                            hdnEditData.Value = jBoxProperty[0]["SizeColorDetails"].ToString();
                        }
                        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "SetEditTime('" + hdnEditData.Value + "');", true);
                        //ScriptManager.RegisterStartupScript(GetType(), "Javascript", "javascript:SetEditTime('" + hdnEditData.Value + "'); ", true);
                        if (hdnEditData.Value != "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "SetEditTime('" + hdnEditData.Value + "');", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "LoadData();", true);
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void dllBoxType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDataProperty();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (dllBoxType.SelectedIndex > 0 && dllSize.SelectedIndex > 0 && hdnData.Value != "[{}]")
        {
            Int32 Val = 0;
            if (Request.QueryString["ID"] == null)
            {
                Val = Objconnection.update_data("PROC_AddEdit_BoxList 0," + dllBoxType.SelectedValue + ",'" + hdnData.Value + "','','"+JArray.Parse(hdnData.Value)[0]["Heading"].ToString() +"'");
            }
            else
            {
                Val = Objconnection.update_data("PROC_AddEdit_BoxList " + Request.QueryString["ID"].ToString() + "," + dllBoxType.SelectedValue + ",'" + hdnData.Value + "','','" + JArray.Parse(hdnData.Value)[0]["Heading"].ToString() + "'");
            }

            if (Val > 0)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('Service Created Successfully');window.location='BoxType.aspx';", true);

            }
        }
        else
        {
            dllSize.SelectedIndex = 0;
            dllBoxType.SelectedIndex = 0;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('Select Size and Box Service');", true);

        }
    }


    [WebMethod]
    public static string SubmitSuccessfully()
    {
        cls_connection Objconnection = new cls_connection();
        DataTable dt=Objconnection.select_data_dt("select * from NLavelCategory");
        return ConvertDataTabletoString(dt);
    }

    public static string ConvertDataTabletoString(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows).ToString();
    }
}