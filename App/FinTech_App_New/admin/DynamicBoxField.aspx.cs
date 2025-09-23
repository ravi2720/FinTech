using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Admin_DynamicBoxField : System.Web.UI.Page
{
    cls_connection Objconnection = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //MakeForm();
            if (Request.QueryString["ID"] != null)
            {
                MakeForm();
            }
        }
    }

    private void MakeForm()
    {
        try
        {
            string RequestJSON = Objconnection.select_data_scalar_string("select RequestJSON from BoxList where ID=" + Request.QueryString["ID"].ToString() + "");
            JArray ObjData = JArray.Parse(RequestJSON);

            hdnReform.Value = RequestJSON;
            //for (int i = 0; i < ObjData.Count; i++)
            //{
            //    dllType.SelectedValue = ObjData[i]["Type"].ToString();
            //    txtName.Text = ObjData[i]["Name"].ToString();
            //    txtMaxLength.Text = ObjData[i]["Max"].ToString();
            //    txtMinLength.Text = ObjData[i]["Min"].ToString();
            //    chkMandatory.Checked = Convert.ToBoolean(ObjData[i]["Mandatory"].ToString());
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ReFormControl('" + RequestJSON + "');", true);
            ClientScript.RegisterStartupScript(Page.GetType(), "OnLoad", "ReFormControl('" + RequestJSON + "');", true);
            //}
        }
        catch (Exception ex)
        {
        }
    }

    protected void dllType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        HtmlGenericControl newdiv = new HtmlGenericControl("DIV");
        HtmlGenericControl newstrong = new HtmlGenericControl("strong");
        newdiv.Attributes.Add("class", "form-group");
        newstrong.InnerHtml = txtName.Text;

        TextBox txt = new TextBox();
        txt.Attributes.Add("class", "form-control");
        newdiv.Controls.Add(newstrong);
        newdiv.Controls.Add(txt);

    }
    protected void btnSaveRequestionJSON_Click(object sender, EventArgs e)
    {
        Int32 Val = Objconnection.update_data("PROC_AddEdit_BoxList " + Request.QueryString["ID"].ToString() + ",1,'','" + hdnData.Value + "'");
        if (Val > 0)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('Form Created successfully');window.location='boxtype.aspx';", true);

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        MakeForm();
    }
}