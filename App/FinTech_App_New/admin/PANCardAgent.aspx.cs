using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PANCardAgent : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    public static string value = "";
    public static int i = 0;
    DataTable dtPanAPIDetail = new DataTable();
    public static string PANURL = "https://cyrusrecharge.in/api/API_PAN.aspx";
    public static string MerchantID = "";
    public static string MerchantKey = "";
    public static string PSAID = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        dtPanAPIDetail = cls.select_data_dt("Select MemberId,MerchantKey from tbl_PAN_APIDetails");
        if (dtPanAPIDetail.Rows.Count > 0)
        {
            MerchantID = dtPanAPIDetail.Rows[0]["MemberId"].ToString();
            MerchantKey = dtPanAPIDetail.Rows[0]["MerchantKey"].ToString();
        }
        if (!IsPostBack)
        {
            BindRepeaterValues(i);
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            value = txtSearch.Text.Trim();
            i++;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alertify.error('" + ex.Message.ToString() + "');", true);
        }

    }
    public void BindRepeaterValues(int var)
    {
        try
        {
            if (var == 0)
            {
                DataTable dtPANDetails = new DataTable();
                dtPANDetails = cls.select_data_dt("select MsrNo,loginid MemberID,Name, Email,Mobile,PsaIdPan,panstatus,RequestId from Member where PsaIdPan!='' or PsaIdPan!=null ");
                if (dtPANDetails.Rows.Count > 0)
                {
                    gvPANReport.DataSource = dtPANDetails;
                    gvPANReport.DataBind();
                }
            }
            if (var == 1)
            {
                DataTable dtPANDetails = new DataTable();
                dtPANDetails = cls.select_data_dt("select MsrNo,loginid MemberID,Name, Email,Mobile,PsaIdPan,panstatus,RequestId from Member where PsaIdPan='" + value + "' or RequestId='" + value + "' or MemberID='" + value + "'");
                if (dtPANDetails.Rows.Count > 0)
                {
                    gvPANReport.DataSource = dtPANDetails;
                    gvPANReport.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alertify.error('" + ex.Message.ToString() + "');", true);
        }
    }



    protected void gvPANReport_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            clsMahagram ObjMahagram = new clsMahagram();
            if (e.CommandName == "CheckStatus")
            {
                string requestid = e.CommandArgument.ToString();
                using (var client = new WebClient())
                {
                    var Values = new NameValueCollection();
                    //Values["securityKey"] = "MH82#E@IN038#F6230UQ3";
                    //Values["createdby"] = dtMemberMaster.Rows[0]["LoginID"].ToString();
                    Values["APIID"] = SONITechnoCredentrial.APIID;
                    Values["Token"] = SONITechnoCredentrial.Token;
                    Values["MethodName"] = "checkstatuspsa";
                    Values["requestid"] = requestid;
                    var responseString = ObjMahagram.UTIAgentStatus(Values);
                    JObject Data = JObject.Parse(responseString);
                    if (Data["code"].ToString().ToLower() != "err")
                    {
                        JArray ObjData = JArray.Parse(Data["data"].ToString());
                        int UpdatVal = cls.update_data("update Member set panstatus='" + ObjData[0]["status"].ToString() + "',RequestId='" + ObjData[0]["RequestId"].ToString() + "' where PsaIdPan='" + ObjData[0]["psaid"].ToString() + "'");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('Agent Status Updated Successfully');", true);
                        BindRepeaterValues(i);
                    }

                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('Some Error Occurred!!');", true);
                        BindRepeaterValues(i);

                    }
                }

            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "alert('Some Error Occurred" + ex.Message.ToString() + "');", true);
        }
    }
}
