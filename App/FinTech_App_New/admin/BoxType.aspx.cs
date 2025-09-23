using CKEditor.NET;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin_BoxType : System.Web.UI.Page
{
    cls_connection Objconnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetData();
            BindData();
            GetDataProperty();
        }
    }
    private void GetData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Objconnection.select_data_dt("select * from BoxList");
            rptData.DataSource = dt;
            rptData.DataBind();
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
        }
        catch (Exception ex)
        {
        }
    }


    public string MakeBOX(string pro, string Type)
    {
        JArray ObjData = JArray.Parse(pro);

        string str1 = "";
        if (ObjData[0]["Image"] != null)
        {
            str1 = ObjData[0]["Image"].ToString().Replace("C:\\fakepath\\", "");
        }
        string str = "";
        if (Type == "1")
        {
            str = "<div class='widget_container full all-slides' onclick=OpenLink('" + Convert.ToString(ObjData[0]["Link"].ToString()) + "')  data-num='0' id='columns'><div class='widget " + Convert.ToString(ObjData[0]["Size"].ToString()) + " widget_orange animation unloaded slide' ><div class='widget_content'><div class='main' style='background-image: url(.././images/" + str1 + ");background-color:" + Convert.ToString(ObjData[0]["Background Color"]) + "'><span>" + Convert.ToString(ObjData[0]["Heading"]) + "</span></div></div></div></div>";
        }
        else if (Type == "2")
        {
            str = "<div class='widget_container full all-slides'  data-num='0' id='columns'><div class='widget " + Convert.ToString(ObjData[0]["Size"]) + " widget_orange animation unloaded slide' ><div class='widget_content'><div class='main' style='background-image: url(.././images/" + str1 + ");background-color:" + Convert.ToString(ObjData[0]["Background Color"]) + "'><span>" + Convert.ToString(ObjData[0]["Heading"]) + "</span></div></div></div></div>";
        }
        else if (Type == "3")
        {
            string AmountADiscount = "0";
            if (ObjData[0]["DisCount"] != null)
            {
                if (Convert.ToDecimal(ObjData[0]["DisCount"].ToString()) > 0)
                {
                    if (ObjData[0]["IsFlat"] != null)
                    {
                        if (!Convert.ToBoolean(ObjData[0]["IsFlat"].ToString()))
                        {
                            AmountADiscount = (Convert.ToDecimal(ObjData[0]["Amount"].ToString()) - (Convert.ToDecimal(ObjData[0]["Amount"].ToString()) * Convert.ToDecimal(ObjData[0]["DisCount"].ToString()) / 100)).ToString();
                        }
                        else
                        {
                            AmountADiscount = (Convert.ToDecimal(ObjData[0]["Amount"].ToString()) - Convert.ToDecimal(ObjData[0]["DisCount"].ToString())).ToString();
                        }
                    }
                    else
                    {
                        AmountADiscount = (Convert.ToDecimal(ObjData[0]["Amount"].ToString()) - Convert.ToDecimal(ObjData[0]["DisCount"].ToString())).ToString();

                    }
                }
            }
            str = " <div class='col-md-12'><div class='new-page'><div class='images'><img src='.././images/" + str1 + "' class='img-responsive' /></div><div class='new-page-1'><div class='images-content'><h5>" + ObjData[0]["Heading"].ToString() + "</h5><ul><li><div class='popover__wrapper'><div class='rating'><span class=''>3.8 <i class='fa fa-star'></i></span><div class='push popover__content m_content'><p class='popover__message'></p><div class='row'><div class='col-md-4 margin'><div class='review_rating'><span>3.8 <i class='fa fa-star'></i></span><p>2,818 Ratings &475 Reviews</p></div></div><div class='col-md-8 margin'><div class='rating_m_div'><div class='progress-inline'><span>5 <i class='fa fa-star'></i></span></div><div class='progress skill-bar progress-1'><div class='progress-bar progress-bar-1 progress-bar-success' role='progressbar' aria-valuenow='100' aria-valuemin='100' aria-valuemax='100'></div></div><div class='rating_counting'><span>1,383</span></div></div><div class='clearfix'></div><div class='rating_m_div'><div class='progress-inline'><span>4 <i class='fa fa-star'></i></span></div><div class='progress skill-bar progress-1'><div class='progress-bar progress-bar-1 progress-bar-success' role='progressbar' aria-valuenow='80' aria-valuemin='80' aria-valuemax='80'></div></div><div class='rating_counting'><span>1,268</span></div></div><div class='clearfix'></div><div class='rating_m_div'><div class='progress-inline'><span>3 <i class='fa fa-star'></i></span></div><div class='progress skill-bar progress-1'><div class='progress-bar progress-bar-1 progress-bar-warning' role='progressbar' aria-valuenow='70' aria-valuemin='70' aria-valuemax='70'></div></div><div class='rating_counting'><span>1,025</span></div></div><div class='clearfix'></div><div class='rating_m_div'><div class='progress-inline'><span>2 <i class='fa fa-star'></i></span></div><div class='progress skill-bar progress-1'><div class='progress-bar progress-bar-1 progress-bar-danger' role='progressbar' aria-valuenow='50' aria-valuemin='50' aria-valuemax='50'></div></div><div class='rating_counting'><span>596</span></div></div><div class='clearfix'></div><div class='rating_m_div'><div class='progress-inline'><span>1 <i class='fa fa-star'></i></span></div><div class='progress skill-bar progress-1'><div class='progress-bar progress-bar-1 progress-bar-info' role='progressbar' aria-valuenow='30' aria-valuemin='30' aria-valuemax='30'></div></div><div class='rating_counting'><span>265</span></div></div><div class='clearfix'></div></div></div></div></div></div></li><li>(184)</li></ul></div><div class='images-content'><ul><li><div class='out-rate'><span><i class='fa fa-inr'></i>" + AmountADiscount + " </span></div></li><li><del><i class='fa fa-inr'></i>(" + ObjData[0]["Amount"].ToString() + ") </del></li><li><div class='discount'><span>" + ObjData[0]["DisCount"].ToString() + "% off</span></div></li></ul></div><div class='offer-line'><ul><li><strong>Offers</strong></li><li>Special Price & 1 More</li></ul></div><div class='shirt-size-outer'><span class='pull-left'>Size: " + (ObjData[0]["Product-Size"] == null ? "" : ObjData[0]["Product-Size"].ToString()) + "</span></div></div></div></div>";
        }
        return str;
    }


    private void GetDataProperty()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Objconnection.select_data_dt("select * from TYPEBOX_Property where TYPEBOXID=" + dllBoxType.SelectedValue + "");
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HtmlGenericControl newdiv = new HtmlGenericControl("DIV");
                    HtmlGenericControl newstrong = new HtmlGenericControl("strong");
                    newdiv.Attributes.Add("class", "form-group");
                    newstrong.InnerHtml = dt.Rows[i]["name"].ToString();
                    newstrong.Attributes.Add("style", "color:white");
                    if (dt.Rows[i]["type"].ToString().ToUpper() == "TEXT")
                    {

                        TextBox txt = new TextBox();
                        txt.Attributes.Add("class", "form-control ValData");
                        txt.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());
                        txt.Attributes.Add("data-set", "text");

                        newdiv.Controls.Add(newstrong);
                        newdiv.Controls.Add(txt);
                    }

                    if (dt.Rows[i]["type"].ToString().ToUpper() == "TEXTCK")
                    {

                        CKEditorControl txt = new CKEditorControl();
                        txt.Attributes.Add("class", "form-control ValData");
                        txt.Width = 150;
                        txt.Width = 400;

                        txt.BasePath = "~/Root/CKEditor/";
                        txt.Attributes.Add("data-type", dt.Rows[i]["name"].ToString());
                        txt.Attributes.Add("data-CK", "1");
                        txt.Attributes.Add("data-set", "text");

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
        Int32 Val = Objconnection.update_data("PROC_AddEdit_BoxList 0," + dllBoxType.SelectedValue + ",'" + hdnData.Value + "',''");
        GetData();
    }
    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            Int32 Trans = Objconnection.update_data("select count(*) from BoxFeaturesTransaction where BoxID='" + e.CommandArgument.ToString() + "'");
            if (Trans == 0)
            {
                Int32 var = Objconnection.update_data("delete from boxlist where id='" + e.CommandArgument.ToString() + "'");
                if (var > 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "alert('Remove successfully');", true);
                    GetData();
                    BindData();
                    GetDataProperty();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "displayalertmessage", "alert('can not Remove');", true);
            }
        }
        if (e.CommandName == "Active")
        {
            Int32 var = Objconnection.update_data("update boxlist set isactive=iif(IsActive=1,0,1) where   id='" + e.CommandArgument.ToString() + "'");
            if (var > 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "alert('Update successfully');", true);
                GetData();
                BindData();
                GetDataProperty();
            }
        }
    }
}
