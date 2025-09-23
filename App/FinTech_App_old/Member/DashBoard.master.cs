using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_DashBoard : System.Web.UI.MasterPage
{
    cls_connection ObjConnection = new cls_connection();
    public DataTable dtMember = new DataTable();
    public DataTable dtBalance = new DataTable();
    public Company company;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            company = Company.GetCompanyInfo();
           
            if (!IsPostBack)
            {
                if (!Convert.ToBoolean(dtMember.Rows[0]["IsSystemOn"]))
                {
                    ErrorShow.AlertMessage(page, "Your System Down Please Up Your Panel", ConstantsData.CompanyName);
                    ContentPlaceHolder1.Visible = false;
                }
                if (Convert.ToBoolean(dtMember.Rows[0]["FirstLiveLogin"]) == false && Path.GetFileName(Request.Path).ToUpper().Contains("PROFILE.ASPX") == false)
                {
                    ErrorShow.AlertMessageWithRedirect(page, "Your First Time Login So Please Change Your Password and TPIN", "profile.aspx", ConstantsData.CompanyName);
                }
                //  BindUPIAccountDetails();
                Common.BindServiceList(rptServiceData, dtMember.Rows[0]["Msrno"].ToString(), ObjConnection);
                FillMenu();
            }
            dtBalance = ObjConnection.select_data_dt("select * from VIEW_MEMBERBALANCE where msrno=" + dtMember.Rows[0]["Msrno"] + "");

        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    private void BindUPIAccountDetails()
    {
        Int32 IsUPIAccountExists = ObjConnection.select_data_scalar_int("select count(1) from MemberUPI where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "'");
        if (IsUPIAccountExists > 0)
        {
            DataTable dtUPI = ObjConnection.select_data_dt("select * from MemberUPI where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "'");
            if (dtUPI.Rows.Count > 0)
            {
                hdnUPIIDQ.Value = dtUPI.Rows[0]["ICICIUPI"].ToString();
                hdnUPIName.Value = dtUPI.Rows[0]["Name"].ToString();
                hdnUPIALL.Value = "upi://pay?pa=" + dtUPI.Rows[0]["ICICIUPI"].ToString() + "&pn=" + dtUPI.Rows[0]["Name"].ToString();
            }

        }
        else
        {
            string URL = SONITechnoCredentrial.UPIURL;
            Location ObjLoaction = new Location();
            ObjLoaction = ObjLoaction.GetLocationData();
            UPI ObjUpi = new UPI();
            ObjUpi.category = "Retail Stores";
            ObjUpi.address = dtMember.Rows[0]["Address"].ToString();
            ObjUpi.lat = ObjLoaction.Latitude;
            ObjUpi.lon = ObjLoaction.Longitude;
            ObjUpi.name = dtMember.Rows[0]["Name"].ToString();
            ObjUpi.pan = dtMember.Rows[0]["Pan"].ToString();
            ObjUpi.upi_id = dtMember.Rows[0]["Mobile"].ToString();
            ObjUpi.MethodName = "createupiid";
            string result = ApiPostCall.PostCall(ObjUpi.GetJson(), URL);

if(result!=""){
            JObject jData = JObject.Parse(result);
            if (jData["code"].ToString().ToLower() != "err")
            {
                ObjConnection.update_data("AddEditMemberUPI '" + dtMember.Rows[0]["msrno"].ToString() + "','" + jData["data"]["upi"]["upi_id"].ToString() + "','" + ObjUpi.name + "','" + ObjUpi.pan + "','" + ObjUpi.category + "','" + ObjUpi.address + "','" + result + "'");
            }
}

            DataTable dtUPI = ObjConnection.select_data_dt("select * from MemberUPI where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "'");
            if (dtUPI.Rows.Count > 0)
            {
                hdnUPIIDQ.Value = dtUPI.Rows[0]["ICICIUPI"].ToString();
                hdnUPIName.Value = dtUPI.Rows[0]["Name"].ToString();
                hdnUPIALL.Value = "upi://pay?pa=" + dtUPI.Rows[0]["ICICIUPI"].ToString() + "&pn=" + dtUPI.Rows[0]["Name"].ToString();
            }
        }
    }
    protected void FillMenu()
    {
        dtMember = (DataTable)Session["dtMember"];
        rptr_main_menu.DataSource = ObjConnection.select_data_dt("EXEC FillMenu @Action='RoleMenu', @RoleID=" + Convert.ToInt16(dtMember.Rows[0]["RoleID"].ToString()) + ",@ID=0,@ParentID=0");
        rptr_main_menu.DataBind();
    }

    protected void FillService()
    {
        // rptService.DataSource = ObjConnection.select_data_dt("FillDropDown 'ActiveServiceDashBoad','" + dtMember.Rows[0]["Msrno"].ToString() + "'");
        // rptService.DataBind();
    }


    protected void rptr_main_menu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        dtMember = (DataTable)Session["dtMember"];
        Repeater rptr_sub_menu = (Repeater)e.Item.FindControl("rptr_sub_menu");
        HiddenField hdn_id = (HiddenField)e.Item.FindControl("hdn_id");
        rptr_sub_menu.DataSource = ObjConnection.select_data_dt("EXEC FillMenu @Action='RoleMenu', @RoleID=" + Convert.ToInt16(dtMember.Rows[0]["RoleID"].ToString()) + ",@ID=0,@ParentID=" + hdn_id.Value + "");
        rptr_sub_menu.DataBind();
    }

    protected void rptr_sub_menu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        dtMember = (DataTable)Session["dtMember"];
        //Repeater rptr_last_menu = (Repeater)e.Item.FindControl("rptr_last_menu");
        //HiddenField hdn_id = (HiddenField)e.Item.FindControl("hdn_id");
        //rptr_last_menu.DataSource = ObjConnection.select_data_dt("EXEC FillMenu @Action='RoleMenu', @RoleID=" + Convert.ToInt16(dtMember.Rows[0]["RoleID"].ToString()) + ",@ID=0,@ParentID=" + hdn_id.Value + "");
        //rptr_last_menu.DataBind();

    }

    protected void btnSendOTP_Click(object sender, EventArgs e)
    {
        if (btnSendOTP.Text == "Send OTP")
        {
            Random generator = new Random();
            int r = generator.Next(100000, 999999);
            string OTP = r.ToString();
            string Message = "Verify OTP is : " + OTP;
            Session["OTPDown"] = OTP;

            string[] ValueArray = new string[4];
            ValueArray[0] = "User";
            ValueArray[1] = OTP;
            string str = SMS.SendWithV(dtMember.Rows[0]["Mobile"].ToString(), ConstantsData.Sign_Up_OTP_SMS, ValueArray, company.MemberID);
            btnSendOTP.Text = "VerifyOTP";
            ErrorShow.SuccessNotify(page, "OTP send on " + dtMember.Rows[0]["Mobile"].ToString() + " your mobile no");
        }
        else
        {
            if (txtOTP.Text == Session["OTPDown"].ToString())
            {
                Int32 Val = 0;
                bool DownOnNot = Convert.ToBoolean(ObjConnection.select_data_scalar_string("select IsSystemOn from member  where msrno=" + dtMember.Rows[0]["Msrno"].ToString() + ""));
                if (DownOnNot == true)
                {
                    Val = ObjConnection.update_data("update Member set IsSystemOn=0 where msrno=" + dtMember.Rows[0]["Msrno"].ToString() + "");
                    if (Val > 0)
                    {
                        ErrorShow.SuccessNotify(page, "Your Panel Down");
                    }
                }
                else
                {
                    Val = ObjConnection.update_data("update Member set IsSystemOn=1 where msrno=" + dtMember.Rows[0]["Msrno"].ToString() + "");
                    if (Val > 0)
                    {
                        ErrorShow.SuccessNotify(page, "Your Panel Up");
                    }
                }
               Session["dtMember"] = ObjConnection.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno = " + Convert.ToInt32(dtMember.Rows[0]["msrno"]) + "");
            }
            else
            {
                ErrorShow.ErrorNotify(page, "Wrong OTP");
            }
        }
    }

    protected void signout_Click(object sender, ImageClickEventArgs e)
    {
       
        // Session.Contents.RemoveAll();
        Session.Remove("dtMember");
        Session["dtMember"] = null;
        
        //.Clear(); Session.Abandon();
        Response.Redirect("Default.aspx");
    }
}
