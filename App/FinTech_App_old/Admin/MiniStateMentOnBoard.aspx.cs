using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_MiniStateMentOnBoard : System.Web.UI.Page
{
    //AEPSBankBodyRequest aEPSBankBodyRequest = new AEPSBankBodyRequest();
    cls_connection Objconnection = new cls_connection();
    DataTable dt = new DataTable();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            BindState();
        }

    }
    private void BindData()
    {
        dt = Objconnection.select_data_dt("select * from Member where LoginID='"+ txtMemberID.Text + "'");
        if (dt.Rows.Count > 0)
        {
            txtAddress.Text = dt.Rows[0]["Address"].ToString();
            txtemail.Text = dt.Rows[0]["email"].ToString();
            txtName.Text = dt.Rows[0]["Name"].ToString();
            txtphone.Text = dt.Rows[0]["mobile"].ToString();
            txtpannumber.Text = dt.Rows[0]["Pan"].ToString();
            txtfirmname.Text = dt.Rows[0]["ShopName"].ToString();
            txtpincode.Text = dt.Rows[0]["pincode"].ToString();

            if (dt.Rows[0]["ActivedForRnfi"].ToString() == "True")
            {
                btnSubmit.Text = "Merchant activated successfully.!";
                btnSubmit.Attributes.Add("class", "btn btn-success");
                btnSubmit.Enabled = false;
            }
            else
            {
                btnSubmit.Text = "Submit";
                btnSubmit.Attributes.Add("class", "btn btn-danger");
                btnSubmit.Enabled = true;
            }
        }
    }
    private void BindState()
    {
        //string bankList = aEPSBankBodyRequest.GetStatelist();
        //JObject jData = JObject.Parse(bankList);
        //dt = GetJSONToDataTableUsingNewtonSoftDll(jData["banklist"].ToString());
        //dllState.DataSource = dt;
        //dllState.DataTextField = "statename";
        //dllState.DataValueField = "stateId";
        //dllState.DataBind();
        //dllState.Items.Insert(0, new ListItem("--select State--", "0"));

    }

    public DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (dllState.SelectedIndex > 0)
        {
            dt = Objconnection.select_data_dt("select * from Member where LoginID='" + txtMemberID.Text + "'");
            if (dt.Rows.Count == 0)
            {
                ErrorShow.AlertMessage(Page, "Member not found.","Error");
                return;
            }

            //MerchantOnboarding merchantOnboarding = new MerchantOnboarding
            //{
            //    address = txtAddress.Text.Trim(),
            //    name = txtName.Text.Trim(),
            //    phone = txtphone.Text.Trim(),
            //    pincode = txtpincode.Text.Trim(),
            //    pannumber = txtpannumber.Text.Trim(),
            //    stateid = Convert.ToInt32(dllState.SelectedValue),
            //    submerchantid = dt.Rows[0]["LoginID"].ToString(),
            //    firmname = txtfirmname.Text.Trim(),
            //    city = "Odisa",  // Consider getting this from a dropdown or input if needed
            //    email = txtemail.Text.Trim()
            //};

            //string Result = aEPSBankBodyRequest.MerchantOnboarding(merchantOnboarding);
            //JObject jObject = JObject.Parse(Result);

            //{"response":1,"status":true,"message":"Merchant activated successfully.!"}
            //{"status":false,"message":"city is required."}

            //if (jObject["status"] == null)
            //{
            //    if (jObject["response"]?.ToString() == "1")
            //    {
            //        Objconnection.update_data("update Member set ActivedForRnfi=1 where msrno=" + dt.Rows[0]["msrno"].ToString());
            //        ErrorShow.AlertMessageWithRedirect(Page, jObject["message"].ToString(), "DashBoard.aspx", "Success");
            //    }
            //    else
            //    {
            //        ErrorShow.AlertMessage(Page, jObject["message"].ToString(), "Error");
            //    }
            //}
            //else
            //{
            //    ErrorShow.AlertMessage(Page, jObject["message"].ToString(), "Error");
            //}
        }
        else
        {
            ErrorShow.AlertMessage(Page, "Please select a state.", "Validation");
        }
    }


    protected void txtMemberID_TextChanged(object sender, EventArgs e)
    {
        BindData();
    }
}