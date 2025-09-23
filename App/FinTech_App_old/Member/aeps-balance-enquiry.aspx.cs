using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Xml;


public partial class Member_aeps_balance_enquiry : System.Web.UI.Page
{
    DataTable dt, dtMember;
    cls_connection ObjConnection = new cls_connection();
    Page page;
    string ipAddress = ""; public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        company = Company.GetCompanyInfo();
        page = HttpContext.Current.CurrentHandler as Page;
        ipAddress = ConstantsData.getipAddress();
        dt = (DataTable)Session["dtMember"]; dtMember = (DataTable)Session["dtMember"];

        if (Session["dtMember"] != null)
        {
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

                string iPAddress = cls_myMember.GetIPAddress();
                // Authenticated today – proceed with API call
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

    protected void btnBalance_Click(object sender, EventArgs e)
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

                if (isLoggedIn)
                {
                    Int32 Val = ObjConnection.update_data("Exec Proc_Add_edit_AEPS " + dt.Rows[0]["msrno"].ToString() + ", '" + RefID + "','0', 'BE', " + nationalbank.SelectedValue + ",'" + (("xxxx-xxxx-" + aadhar)) + "', '" + formmobile.Text + "', 'Pending','','" + hdnDevice.Value + "','','','Site','','','" + ipAddress + "'");
                    if (Val > 0)
                    {
                        InstantPayBERoot balanceInquery = new InstantPayBERoot();


                        string iPAddress = cls_myMember.GetIPAddress();
                        Location location = new Location();
                        location = location.GetLocationDataCurrent(iPAddress);

                        balanceInquery.latitude = location.Latitude;
                        balanceInquery.longitude = location.Longitude;
                        balanceInquery.bankiin = nationalbank.SelectedValue;
                        balanceInquery.mobile = formmobile.Text;
                        balanceInquery.externalRef = RefID;

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

                        balanceInquery.biometricData = thumbRequest;

                        string Result = NWSPay.BalanceInquery(balanceInquery.GetJson(), dtValc.Rows[0]["OutLetID"].ToString());
                        JObject Data = JObject.Parse(Result);

                        if (Data["statusCode"]?.ToString() == "TXN")
                        {
                            imgSet.Src = "../img/RightMark.png";
                            Message.InnerHtml = "Balance Inquiry Success";
                            lblBankName.Text = Data["data"]["bankName"].ToString();
                            CusMobile.Text = balanceInquery.mobile;
                            lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                            BCName.Text = dt.Rows[0]["Name"].ToString();
                            lblTransID.Text = balanceInquery.externalRef;
                            lblMark.Attributes.Add("class", "btn btn-success");
                            lblAmount.Text = Data["data"]["bankAccountBalance"].ToString();
                            lblMark.Text = Data["status"].ToString();
                            lblBankNumber.Text = Data["data"]["operatorId"].ToString();
                            lblAadharNo.Text = ("xxxx-xxxx-" + aadhar);
                            Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dt.Rows[0]["msrno"].ToString() + "','BE','Success','" + Data["status"].ToString() + "',0,'" + Data["data"]["operatorId"].ToString() + "','','" + Result + "'");
                            lblDate.Text = System.DateTime.Now.ToString();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "$('#ErrorMessage').modal('show');", true);
                            hdnCapture.Value = "";
                        }
                        else
                        {
                            imgSet.Src = "../img/Cross.png";
                            Message.InnerHtml = "Balance Inquiry failed";
                            lblBankName.Text = nationalbank.SelectedItem.Text;
                            CusMobile.Text = balanceInquery.mobile;
                            lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                            BCName.Text = dt.Rows[0]["Name"].ToString();
                            lblTransID.Text = balanceInquery.externalRef;
                            lblMark.Attributes.Add("class", "btn btn-danger");
                            lblMark.Text = Data["status"].ToString();
                            lblBankNumber.Text = "";
                            lblDate.Text = System.DateTime.Now.ToString();
                            lblAadharNo.Text = ("xxxx-xxxx-" + aadhar);
                            Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dt.Rows[0]["msrno"].ToString() + "','BE','Failed','" + Data["status"].ToString() + "',0,'','','" + Result + "'");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "$('#ErrorMessage').modal('show');", true);
                            hdnCapture.Value = "";
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
        else
        {
            ErrorShow.AlertMessage(page, "Fill All Information", ConstantsData.CompanyName);
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