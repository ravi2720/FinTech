using System;
using Google.Authenticator;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PermissionSetting : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtMember;
    public Company company;

    private void FillCompanyInfo()
    {
            company = Company.GetCompanyInfo();

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtEmployee"] != null)
        {
            dtMember = (DataTable)Session["dtEmployee"];
            FillCompanyInfo();
            if (!IsPostBack)
            {
                AuthenticationTitle = dtMember.Rows[0]["Name"].ToString();
                GenerateTwoFactorAuthentication();
                imgQrCode.ImageUrl = AuthenticationBarCodeImage;
                lblManualSetupCode.Text = AuthenticationManualCode;
                lblAccountName.Text = AuthenticationTitle;
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }
    String AuthenticationCode
    {
        get
        {
            if (ViewState["AuthenticationCode"] != null)
                return ViewState["AuthenticationCode"].ToString().Trim();
            return String.Empty;
        }
        set
        {
            ViewState["AuthenticationCode"] = value.Trim();
        }
    }
    String AuthenticationTitle
    {
        get;
        set;
    }


    String AuthenticationBarCodeImage
    {
        get;
        set;
    }



    String AuthenticationManualCode
    {
        get;
        set;
    }

    public Boolean GenerateTwoFactorAuthentication()
    {
        Guid guid = Guid.NewGuid();
        String uniqueUserKey = "";
        if (string.IsNullOrEmpty(dtMember.Rows[0]["SecretKey"].ToString()))
        {
            uniqueUserKey = Convert.ToString(guid).Replace("-", "").Substring(0, 10);
            Int32 Val = cls.update_data("ManageEMPLOYEE 'updateSecretKey'," + dtMember.Rows[0]["ID"] + ",'" + uniqueUserKey + "'");


        }
        else
        {
            uniqueUserKey = dtMember.Rows[0]["SecretKey"].ToString();
        }
        AuthenticationCode = uniqueUserKey;

        Dictionary<String, String> result = new Dictionary<String, String>();
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        var setupInfo = tfa.GenerateSetupCode(company.Name, AuthenticationTitle, AuthenticationCode, 300, 300);
        if (setupInfo != null)
        {
            AuthenticationBarCodeImage = setupInfo.QrCodeSetupImageUrl;
            AuthenticationManualCode = setupInfo.ManualEntryKey;
            return true;
        }
        return false;
    }
    public Boolean ValidateTwoFactorPIN(String pin)
    {
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        return tfa.ValidateTwoFactorPIN(AuthenticationCode, pin);
    }

    [WebMethod]
    public static string UpdatePermission(string MethodName,string ColN)
    {
        cls_connection Objconnection = new cls_connection();
        Int32 Val = Objconnection.update_data("ManageAdminAccountSetting 'ColUpdate','" + ColN + "'");
        
        return Val.ToString();
    }

    [WebMethod]
    public static string GetData(string MethodName)
    {
        cls_connection Objconnection = new cls_connection();
        DataTable dt = Objconnection.select_data_dt("ManageAdminAccountSetting 'selectdata'");

        return SerializeToJSon(dt);
    }

    public static string SerializeToJSon(DataTable dt)
    {
        JavaScriptSerializer ser = new JavaScriptSerializer();
        List<Dictionary<string, object>> dataRows = new List<Dictionary<string, object>>();
        dt.Rows.Cast<DataRow>().ToList().ForEach(dataRow =>
        {
            var row = new Dictionary<string, object>();
            dt.Columns.Cast<DataColumn>().ToList().ForEach(column =>
            {
                row.Add(column.ColumnName, dataRow[column]);
            });
            dataRows.Add(row);
        });
        return ser.Serialize(dataRows);
    }
}