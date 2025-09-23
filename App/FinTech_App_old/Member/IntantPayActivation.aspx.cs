using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Xml;

public partial class Member_IntantPayActivation : System.Web.UI.Page
{
    DataTable dt, dtMember;
    cls_connection ObjConnection = new cls_connection();
    Page page;
    string ipAddress = "";
    public Company company;

    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        page = HttpContext.Current.CurrentHandler as Page;
        ipAddress = ConstantsData.getipAddress();
        dt = (DataTable)Session["dtMember"]; dtMember = (DataTable)Session["dtMember"];

        string serviceType = Session["serviceType"]?.ToString();

        if (Session["dtMember"] != null)
        {
            if (!IsPostBack)
            {
                if (serviceType != null && serviceType.Equals("CashDeposit", StringComparison.OrdinalIgnoreCase))
                    return;

                // 1. Get KYC details for the current member
                string msrNo = dtMember.Rows[0]["Msrno"].ToString();
                DataTable dtValc = ObjConnection.select_data_dt(
                    "select * from aepskyc where Msrno='" + msrNo + "'");

                if (dtValc.Rows.Count == 0)
                {
                    // No KYC record
                    ErrorShow.AlertMessageWithRedirect(
                        page,
                        "Your KYC Not Complete, Upload Your KYC",
                        "InstantKYC.aspx",
                        ConstantsData.CompanyName
                    );
                    return;
                }

                bool isLoggedIn = CheckLoginStatus(dtValc.Rows[0]["OutLetID"].ToString());
                if (!isLoggedIn)
                {
                    return;
                }

                // Redirect to balance enquiry page if authenticated today
                Response.Redirect("aeps-balance-enquiry.aspx");
                return;
            }
        }
    }

    public DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }

    protected void btnOutletLogin_Click(object sender, EventArgs e)
    {
        string serviceType = Session["serviceType"]?.ToString();

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(HttpUtility.HtmlDecode(hdnCapture.Value));
        string jsonText = JsonConvert.SerializeXmlNode(doc);
        dynamic bsObj;

        if (hdnDevice.Value.ToUpper() == "MORPHO")
            bsObj = JsonConvert.DeserializeObject<RooteeeMorpho>(jsonText);
        else
            bsObj = JsonConvert.DeserializeObject<Mantra>(jsonText);

        string RefID = NWSPay.GenerateExternalRef();
        ipAddress = ConstantsData.getipAddress();

        DataTable dtValc = ObjConnection.select_data_dt(
            "select * from aepskyc where Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

        if (dtValc.Rows.Count > 0)
        {
            bool isAuthenticated = Convert.ToBoolean(dtValc.Rows[0]["Authentication"]);

            ActionLoginRoot loginRequest = new ActionLoginRoot();

            string iPAddress = cls_myMember.GetIPAddress();
            Location location = new Location();
            location = location.GetLocationDataCurrent(iPAddress);

            loginRequest.latitude = location.Latitude;
            loginRequest.longitude = location.Longitude;
            loginRequest.externalRef = RefID;
            loginRequest.type = "DAILY_LOGIN";
            if (serviceType != null && serviceType.Equals("CashDeposit", StringComparison.OrdinalIgnoreCase))
                loginRequest.serviceType = "CashDeposit";
            loginRequest.captureType = "FINGER";

            Session["serviceType"] = string.Empty;

            string ts = string.Empty;
            string srno = string.Empty;
            foreach (var param in bsObj.PidData.DeviceInfo.additional_info.Param)
            {
                if (param.Name.ToLower() == "ts")
                {
                    ts = param.Value.ToString("yyyy-MM-ddTHH:mm:ss");
                }
                if (param.Name.ToLower() == "srno")
                {
                    srno = param.Value;
                }
            }

            BiometricData thumbRequest = new BiometricData();
            thumbRequest.encryptedAadhaar = NWSPay.GetEncryptAadhar(dtMember.Rows[0]["Aadhar"].ToString());
            thumbRequest.dc = bsObj.PidData.DeviceInfo.Dc;
            thumbRequest.ci = bsObj.PidData.Skey.Ci;
            thumbRequest.hmac = bsObj.PidData.Hmac;
            thumbRequest.dpId = bsObj.PidData.DeviceInfo.DpId;
            thumbRequest.mc = bsObj.PidData.DeviceInfo.Mc;
            thumbRequest.pidDataType = bsObj.PidData.Data.Type;
            thumbRequest.sessionKey = bsObj.PidData.Skey.Text;
            thumbRequest.mi = bsObj.PidData.DeviceInfo.Mi;
            thumbRequest.rdsId = bsObj.PidData.DeviceInfo.RdsId;
            thumbRequest.errCode = bsObj.PidData.Resp.ErrCode;
            thumbRequest.errInfo = bsObj.PidData.Resp.ErrInfo;
            thumbRequest.fCount = bsObj.PidData.Resp.FCount;
            thumbRequest.fType = bsObj.PidData.Resp.FType;
            thumbRequest.iCount = 0;
            thumbRequest.iType = "0";
            thumbRequest.pCount = 0;
            thumbRequest.pType = bsObj.PidData.DeviceInfo.RdsId;
            thumbRequest.srno = srno;
            thumbRequest.sysid = "";
            thumbRequest.ts = ts;
            thumbRequest.pidData = bsObj.PidData.Data.Text;
            thumbRequest.qScore = bsObj.PidData.Resp.QScore;
            thumbRequest.nmPoints = bsObj.PidData.Resp.NmPoints;
            thumbRequest.rdsVer = bsObj.PidData.DeviceInfo.RdsVer;
            loginRequest.biometricData = thumbRequest;

            ObjConnection.update_data("insert into logdata(data)values('" + loginRequest.GetJson() + "')");

            string Result = NWSPay.OutletLogin(loginRequest.GetJson(), dtValc.Rows[0]["OutLetID"].ToString());

            ObjConnection.update_data("insert into logdata(data)values('" + Result + "')");

            JObject Data = JObject.Parse(Result);

            if (Data["statusCode"]?.ToString() == "TXN")
            {
                ObjConnection.update_data("update aepskyc set Authentication=1,AuthDate=getdate() where msrno='" + dtMember.Rows[0]["msrno"].ToString() + "'");
                ErrorShow.AlertMessageWithRedirect(page, Data["status"].ToString(), "aeps-balance-enquiry.aspx", ConstantsData.CompanyName);
            }
            else
            {
                ErrorShow.AlertMessage(page, Data["status"].ToString(), ConstantsData.CompanyName);
            }
        }
        else
        {
            ErrorShow.AlertMessageWithRedirect(
                page,
                "Your KYC Not Complete, Upload Your KYC",
                "InstantKYC.aspx",
                ConstantsData.CompanyName
            );
            Session["serviceType"] = string.Empty;
            return;
        }
    }

    private bool CheckLoginStatus(string outLetId)
    {
        try
        {
            var jsonString = JsonConvert.SerializeObject(new { });

            var response = NWSPay.CheckLoginStatus(jsonString,
                outLetId
            );

            JObject Data = JObject.Parse(response);

            if (Data["statusCode"]?.ToString() == "TXN" && Data["actCode"]?.ToString() == "LOGGEDIN")
                return true;
            else return false;
        }
        catch (Exception)
        {
            throw;
        }
    }
}