using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Xml;

public partial class Member_Ministatement : System.Web.UI.Page
{
    DataTable dtMember = new DataTable();
    cls_connection ObjConnection = new cls_connection();
    Page page;
    string ipAddress = ""; public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        page = HttpContext.Current.CurrentHandler as Page;
        ipAddress = ConstantsData.getipAddress();
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];

            if (!IsPostBack)
            {
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
                    ErrorShow.AlertMessageWithRedirect(
                        page,
                        "You are not logged in. Please login to continue.",
                        "IntantPayActivation.aspx",
                        ConstantsData.CompanyName
                    );
                    return;
                }

                string ipAddress = cls_myMember.GetIPAddress();
                string bankListstr = NWSPay.GetBankList(dtValc.Rows[0]["OutLetID"].ToString());
                JObject jData = JObject.Parse(bankListstr);

                if (jData["statusCode"]?.ToString() == "TXN")
                {
                    DataTable dt1 = GetJSONToDataTableUsingNewtonSoftDll(jData["data"].ToString());
                    nationalbank.DataSource = dt1;
                    nationalbank.DataTextField = "name";
                    nationalbank.DataValueField = "iin";
                    nationalbank.DataBind();
                }
            }
        }
    }

    public DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }


    protected void btnMS_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(formmobile.Text.Trim()) && !string.IsNullOrEmpty(aadhar_number.Text))
        {
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
            string aadhar = aadhar_number.Text.Substring(8, 4);

            DataTable dtValc = ObjConnection.select_data_dt(
                "select * from aepskyc where Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

            if (dtValc.Rows.Count > 0)
            {
                bool isLoggedIn = CheckLoginStatus(dtValc.Rows[0]["OutLetID"].ToString());
                if (!isLoggedIn)
                {
                    ErrorShow.AlertMessageWithRedirect(
                        page,
                        "You are not logged in. Please login to continue.",
                        "IntantPayActivation.aspx",
                        ConstantsData.CompanyName
                    );
                    return;
                }

                if (isLoggedIn)
                {
                    Int32 Val = ObjConnection.update_data("Exec Proc_Add_edit_AEPS " + dtMember.Rows[0]["msrno"].ToString() + ", '" + RefID + "','0', 'MS', " + nationalbank.SelectedValue + ",'" + (("xxxx-xxxx-" + aadhar)) + "', '" + formmobile.Text + "', 'Pending','','" + hdnDevice.Value + "','','','Site','','','" + ipAddress + "'");
                    if (Val > 0)
                    {
                        InstantPayBERoot miniStatement = new InstantPayBERoot();

                        string iPAddress = cls_myMember.GetIPAddress();
                        Location location = new Location();
                        location = location.GetLocationDataCurrent(iPAddress);

                        miniStatement.latitude = location.Latitude;
                        miniStatement.longitude = location.Longitude;

                        miniStatement.bankiin = nationalbank.SelectedValue;
                        miniStatement.mobile = formmobile.Text;
                        miniStatement.externalRef = RefID;

                        InstantPayBEBody thumbRequest = new InstantPayBEBody();
                        thumbRequest.encryptedAadhaar = NWSPay.GetEncryptAadhar(aadhar_number.Text);
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
                        thumbRequest.srno = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.sysid = "";
                        thumbRequest.ts = bsObj.PidData.DeviceInfo.RdsId;
                        thumbRequest.pidData = bsObj.PidData.Data.Text;
                        thumbRequest.qScore = bsObj.PidData.Resp.QScore;
                        thumbRequest.nmPoints = bsObj.PidData.Resp.NmPoints;
                        thumbRequest.rdsVer = bsObj.PidData.DeviceInfo.RdsVer;

                        miniStatement.biometricData = thumbRequest;
                        string Result = NWSPay.MiniStatement(miniStatement.GetJson(), dtValc.Rows[0]["OutLetID"].ToString());
                        JObject Data = JObject.Parse(Result);

                        if (Data["statusCode"]?.ToString() == "TXN")
                        {
                            successMessage.Visible = true;
                            lblBankNumber.Text = Data["data"]["operatorId"].ToString();
                            lblSuccessMessage.Text = Data["status"].ToString();
                            lblStatus.Text = "Success";
                            lblAmount.Text = "0";
                            lblBankNumber.Text = Data["data"]["operatorId"].ToString();
                            lblRemainigBalance.Text = Data["data"]["bankAccountBalance"].ToString();
                            DataTable dt1 = new DataTable();
                            dt1 = GetJSONToDataTableUsingNewtonSoftDll(Data["data"]["miniStatement"].ToString());
                            if (dt1 != null && dt1.Rows.Count > 0 && dt1.Columns.Count > 0)
                            {
                                rptMiniState.DataSource = dt1;
                                rptMiniState.DataBind();
                            }
                            else
                            {
                                rptMiniState.DataSource = null;
                                rptMiniState.DataBind();
                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "$('#ErrorMessage').modal('show');", true);
                            Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','MS','Success','" + Data["status"].ToString() + "',0,'" + Data["data"]["operatorId"].ToString() + "','','" + Result + "'");

                            hdnCapture.Value = "";
                        }
                        else
                        {
                            errorMessage.Visible = true;
                            lblBankNumber.Text = "";
                            lblErrorMessage.Text = Data["status"].ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "$('#ErrorMessage').modal('show');", true);
                            Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','MS','Failed','" + Data["status"].ToString() + "',0,'','','" + Result + "'");

                            hdnCapture.Value = "";
                            rptMiniState.DataSource = null;
                            rptMiniState.DataBind();

                        }
                    }
                }
                else
                {
                    ErrorShow.AlertMessageWithRedirect(
                        page,
                        "You are not logged in. Please login to continue.",
                        "IntantPayActivation.aspx",
                        ConstantsData.CompanyName
                    );
                    return;
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
                return;
            }
        }
    }

    protected void btnexportPdf_Click(object sender, EventArgs e)
    {
        Session["ctrl"] = Panel1;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=500px,width=650px,scrollbars=1');</script>");
    }

    private bool CheckLoginStatus(string outLetId)
    {
        try
        {
            string ipAddress = cls_myMember.GetIPAddress();
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