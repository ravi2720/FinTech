using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
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
        
    protected void FillMenu()
    {
        dtMember = (DataTable)Session["dtMember"];
        rptr_main_menu.DataSource = ObjConnection.select_data_dt("EXEC FillMenu @Action='RoleMenu', @RoleID=" + Convert.ToInt16(dtMember.Rows[0]["RoleID"].ToString()) + ",@ID=0,@ParentID=0");
        rptr_main_menu.DataBind();
    }

    protected void FillService()
    {
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
