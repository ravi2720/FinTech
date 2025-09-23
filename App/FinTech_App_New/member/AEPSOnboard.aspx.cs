using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_AEPSOnboard : System.Web.UI.Page
{
    cls_connection Objconnection = new cls_connection();
    DataTable dtMember;
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;

        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                txtFirmName.Text = dtMember.Rows[0]["ShopName"].ToString();
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    public void Onboard()
    {
        {
            JWTBankSendRequestNewIntegration jWTBankSendRequest = new JWTBankSendRequestNewIntegration();
            cls_connection objConnection = new cls_connection();
            PaysAEPSOnBoardST paysAEPSOnBoard = new PaysAEPSOnBoardST();
            paysAEPSOnBoard.merchantcode = SONITechnoCredentrial.Prefix + dtMember.Rows[0]["loginID"].ToString();
            paysAEPSOnBoard.mobile = dtMember.Rows[0]["Mobile"].ToString();
            paysAEPSOnBoard.is_new = "0";
            paysAEPSOnBoard.email = dtMember.Rows[0]["Email"].ToString();
            paysAEPSOnBoard.firm = txtFirmName.Text;
            string Result = ApiPostCall.PostCall(paysAEPSOnBoard.GetJson(), SONITechnoCredentrial.AEPSCore);
            JObject Data = JObject.Parse(Result);
            if (Data["code"].ToString().ToUpper() == "TXN")
            {
                if (Convert.ToBoolean(Data["data"]["status"].ToString()))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyFun1", "LoadData('" + Data["data"]["redirecturl"].ToString() + "');", true);
                }
                else
                {
                    ErrorShow.Error(page1: page, Message: Data["data"]["message"].ToString());

                }
            }
            else
            {
                ErrorShow.AlertMessage(page, Data["mess"].ToString(),ConstantsData.CompanyName);

            }
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Onboard();
    }
}