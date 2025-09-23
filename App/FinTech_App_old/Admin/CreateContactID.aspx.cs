using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_CreateContactID : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillMember();
        }
    }

    protected void dllMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dllMember.SelectedIndex > 0)
        {
            DataTable dt = ObjConnection.select_data_dt("select * from ContactsRazor where msrno=" + dllMember.SelectedValue + " and self=1");
            if (dt.Rows.Count > 0)
            {
                btnCOntact.Visible = false;
                lblMessage.Text = "Contact ID Already Created";

            }
            else
            {
                lblMessage.Text = "Contact ID create";
                btnCOntact.Visible = true;
            }
        }
    }

    private void FillMember()
    {
        DataTable dtMember = ObjConnection.select_data_dt("select (LoginID + ' ( ' + Name + ' ) ')as Name,msrno from Member where isactive=1");
        Common.BindDropDown(dllMember, dtMember, "Name", "msrno");
        dllMember.Items.Insert(0, new ListItem("-- Select Member --", "0"));
    }

    protected void btnCOntact_Click(object sender, EventArgs e)
    {
        if (dllMember.SelectedIndex > 0)
        {
            DataTable dtMember = ObjConnection.select_data_dt("select * from member where msrno='" + dllMember.SelectedValue + "'");
            if (dtMember.Rows.Count > 0)
            {
                CreateContact createContact = new CreateContact();
                createContact.contact = dtMember.Rows[0]["Mobile"].ToString();
                createContact.name = dtMember.Rows[0]["Name"].ToString();
                createContact.email = dtMember.Rows[0]["email"].ToString();
                createContact.reference_id = System.Guid.NewGuid().ToString().Replace("-", "0");
                createContact.type = "employee";

                CreateContactNotes createContactNotes = new CreateContactNotes();
                createContactNotes.note_key = "Create Contact";
                createContact.notes = createContactNotes;

                string result = ApiPostCallRazorpayx.PostCall(createContact.GetJson(), "contacts");
                JObject Data = JObject.Parse(result);

                if (Data["error"] == null)
                {
                    CreateContactResultRoot contactResultRoot = JsonConvert.DeserializeObject<CreateContactResultRoot>(result);
                    int Val = 0;
                    Val = ObjConnection.update_data("Proc_ContactsRazor '" + contactResultRoot.id + "','" + contactResultRoot.name + "','" + contactResultRoot.contact + "','" + contactResultRoot.email + "'," + dtMember.Rows[0]["msrno"].ToString() + ",'" + contactResultRoot.reference_id + "','" + contactResultRoot.active + "',0,'" + result + "',1");
                    if (Val > 0)
                    {
                        ErrorShow.AlertMessage(Page, "Contact Created successfully", "Success");
                    }
                }
                else
                {
                    ErrorShow.AlertMessage(Page, Data["error"]["description"].ToString(), "Error");
                }
            }
        }
    }

}