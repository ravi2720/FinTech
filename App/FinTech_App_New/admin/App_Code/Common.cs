using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Common
/// </summary>
public static class Common
{
    public static void BindDropDown(DropDownList ddl, DataTable dt, string Text, string Value)
    {
        ddl.DataSource = dt;
        ddl.DataTextField = Text;
        ddl.DataValueField = Value;
        ddl.DataBind();
    }

    public static void BindRadioButtonList(RadioButtonList rdl, DataTable dt, string Text, string Value)
    {
        rdl.DataSource = dt;
        rdl.DataTextField = Text;
        rdl.DataValueField = Value;
        rdl.DataBind();
    }
    public static void BindServiceList(Repeater rpt, string Msrno, cls_connection cls)
    {
        DataTable dt = cls.select_data_dt("FillDropDown 'ActiveServiceMobile'," + Msrno + "");
        if (dt.Rows.Count > 0)
        {
            rpt.DataSource = dt;
            rpt.DataBind();
        }
        else
        {
            rpt.DataSource = null;
            rpt.DataBind();
        }

    }

    public static Tuple<bool, string> GetServiceStatus(string ID, cls_connection cls)
    {
        DataTable dtActivePayoutAPI = cls.select_data_dt("select * from Service where ID=" + ID + " and IsActive=1");
        if (dtActivePayoutAPI.Rows.Count > 0)
        {
            if (Convert.ToBoolean(dtActivePayoutAPI.Rows[0]["Onoff"]))
            {
                return Tuple.Create(true, "");
            }
            else
            {
                return Tuple.Create(false, dtActivePayoutAPI.Rows[0]["Reason"].ToString());
            }
        }
        else
        {
            return Tuple.Create(false, "Service Not Active So Can not Use");
        }

    }

    public static Tuple<bool, string, string> CheckAuthForTransaction(MemberPermission mem, TextBox txtOTP, Label label, string Mobile, string CompanyID,Page page)
    {
        if (mem != null)
        {
            if (mem.TwoWay)
            {
                if (mem.IsTpin)
                {
                    label.Text = "Your TPIN";
                    txtOTP.MaxLength = 4;
                    txtOTP.Text = "Enter Your Transaction PIN";
                    ErrorShow.SuccessNotify(page, "Enter Your Transaction PIN");
                    return Tuple.Create(true, "", "");
                }
                else if (mem.IsOTP)
                {
                    Random generator = new Random();
                    int r = generator.Next(1000, 9999);
                    string OTP = r.ToString();
                    string[] ValueArray = new string[2];
                    ValueArray[0] = "User";
                    txtOTP.MaxLength = 4;
                    ValueArray[1] = OTP;
                    HttpContext.Current.Session["AddFundMemberOTP"] = OTP;
                    string str = SMS.SendWithV(Mobile, ConstantsData.Admin_Fund_Add_OTP_SMS, ValueArray, CompanyID);
                    ErrorShow.SuccessNotify(page, "OTP send on Your Register Mobile Number");
                    label.Text = "Your OTP";
                    txtOTP.Text = "Enter OTP send on Your Number";
                    return Tuple.Create(true, "", "");
                }
                else if (mem.IsGoogleAuth)
                {
                    label.Text = "Authenticator Code";
                    txtOTP.MaxLength = 6;
                    txtOTP.Text = "Please Enter Your google Authenticator Code";
                    ErrorShow.SuccessNotify(page, "Your google Authenticator Code Check in Your Mobile");
                    return Tuple.Create(true, "", "");
                }
                else
                {
                    return Tuple.Create(true, "", "");
                }
            }
            else
            {
                return Tuple.Create(false, "", "");
            }
        }
        else
        {
            return Tuple.Create(false, "", "");
        }
    }

    public static bool CheckVAerifyAuth(MemberPermission permission, string OTP, string TY,Page page)
    {
        if (permission.IsTpin)
        {
            if (OTP == TY)
            {
                return true;
            }
            else
            {
                ErrorShow.ErrorNotify(page, "Transaction Pin not Match");
                return false;
            }
        }
        else if (permission.IsOTP)
        {
            if (OTP == HttpContext.Current.Session["AddFundMemberOTP"].ToString())
            {
                HttpContext.Current.Session["AddFundMemberOTP"] = null;
                return true;
            }
            else
            {
                ErrorShow.ErrorNotify(page, "OTP Not Match");
                return false;
            }
        }
        else if (permission.IsGoogleAuth)
        {
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            if (tfa.ValidateTwoFactorPIN(permission.AuthKey, OTP))
            {
                return true;
            }
            else
            {
                ErrorShow.ErrorNotify(page, "Google Authenticator Not Match");
                return false;
            }
        }
        else
        {
            return true;
        }
    }
}