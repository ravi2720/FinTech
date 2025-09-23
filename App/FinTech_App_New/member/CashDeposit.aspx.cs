using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Xml;
public partial class Member_CashDeposit : System.Web.UI.Page
{
    DataTable dt, dtMember;
    cls_connection ObjConnection = new cls_connection();
    DataTable dtMemberMaster = new DataTable();
    Page page;
    public Company company;
    string ipAddress = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtMember"] != null)
        {
            company = Company.GetCompanyInfo();
            dtMember = (DataTable)Session["dtMember"];
            page = HttpContext.Current.CurrentHandler as Page;
            ipAddress = ConstantsData.getipAddress();

            if (!IsPostBack)
            {
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
                    Session["serviceType"] = "CashDeposit";
                    ErrorShow.AlertMessageWithRedirect(
                        page,
                        "You are not logged in. Please login to continue.",
                        "IntantPayActivation.aspx",
                        ConstantsData.CompanyName
                    );
                    return;
                }

                string outletId = dtValc.Rows[0]["OutLetID"].ToString();
                string bankListstr = NWSPay.GetBankList(outletId);
                JObject jData = JObject.Parse(bankListstr);

                if (jData["statusCode"]?.ToString() == "TXN")
                {
                    DataTable dt1 = GetJSONToDataTableUsingNewtonSoftDll(jData["data"].ToString());
                    nationalbank.DataSource = dt1;
                    nationalbank.DataTextField = "name";
                    nationalbank.DataValueField = "iin";
                    nationalbank.DataBind();
                }

                // Add JavaScript event to amount textbox
                txtAmount.Attributes.Add("onkeyup", "amtchnage()");
            }
        }
    }

    public DataTable GetJSONToDataTableUsingNewtonSoftDll(string JSONData)
    {
        DataTable dt = (DataTable)JsonConvert.DeserializeObject(JSONData, (typeof(DataTable)));
        return dt;
    }

    protected void btnCD_Click(object sender, EventArgs e)
    {
        dtMemberMaster = (DataTable)Session["dtMember"];
        DataTable dtWalletBalance = new DataTable();
        decimal MyBalance = 0;
        dtWalletBalance = ObjConnection.select_data_dt("select * from VIEW_MEMBERBALANCE_WITH_HOLD where Msrno=" + dtMemberMaster.Rows[0]["Msrno"] + "");

        if (dtWalletBalance.Rows.Count > 0)
        {
            MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["UsableAEPSBalance"]);
        }
        if (MyBalance >= Convert.ToDecimal(txtAmount.Text))
        {
            if (!string.IsNullOrEmpty(formmobile.Text.Trim()) && !string.IsNullOrEmpty(aadhar_number.Text) && !string.IsNullOrEmpty(hdnCapture.Value))
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
                        Int32 Val = ObjConnection.update_data("Exec Proc_Add_edit_AEPS " + dtMember.Rows[0]["msrno"].ToString() + ", '" + RefID + "','" + txtAmount.Text + "', 'CD', " + nationalbank.SelectedValue + ",'" + (("xxxx-xxxx-" + aadhar)) + "', '" + formmobile.Text + "', 'Pending','','','','','App','','',''");
                        if (Val > 0)
                        {
                            InstantPayCWRoot cashDeposit = new InstantPayCWRoot();
                            string iPAddress = cls_myMember.GetIPAddress();
                            Location location = new Location();
                            location = location.GetLocationDataCurrent(iPAddress);

                            cashDeposit.latitude = location.Latitude;
                            cashDeposit.longitude = location.Longitude;
                            cashDeposit.amount = txtAmount.Text;
                            cashDeposit.bankiin = nationalbank.SelectedValue;
                            cashDeposit.mobile = formmobile.Text;
                            cashDeposit.externalRef = RefID;

                            InstantPayBEBody thumbRequest = new InstantPayBEBody();
                            thumbRequest.encryptedAadhaar = NWSPay.GetEncryptAadhar(aadhar_number.Text);// bsObj.PidData.Skey.Ci;
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

                            cashDeposit.biometricData = thumbRequest;

                            string Result = NWSPay.CashDeposit(cashDeposit.GetJson(), dtValc.Rows[0]["OutLetID"].ToString());
                            JObject Data = JObject.Parse(Result);

                            if (Data["statusCode"]?.ToString() == "TXN")
                            {
                                Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','CD','Success','" + Data["status"].ToString() + "',0,'" + Data["data"]["operatorId"].ToString() + "','','" + Result + "'");
                                if (Val > 0)
                                {
                                    try
                                    {
                                        string narration = "", Decs = "";
                                        narration = "Aeps TransID-" + RefID;
                                        Decs = $"AEPS Amount - {cashDeposit.amount}. Done With Aadhar No {("xxxx-xxxx-" + aadhar)}. TransID - {RefID}";
                                        ObjConnection.update_data("exec PROC_AEPSWALLETTRANSACTION '" + dtMember.Rows[0]["LoginID"].ToString() + "','" + cashDeposit.amount + "','DR','" + narration + "','" + Decs + "','" + ConstantsData.SAeps + "','" + RefID + "'");
                                        ObjConnection.update_data("Exec Proc_AEPS_DistributeCommission '" + dtMember.Rows[0]["msrno"].ToString() + "'," + cashDeposit.amount + ", '" + RefID + "', '" + ConstantsData._TDS + "','" + ConstantsData.SAEPSCommission + "','" + ConstantsData._TDSVal + "'");
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                }

                                imgSet.Src = "../img/RightMark.png";
                                Message.InnerHtml = "CashWithdraw  Success";
                                lblBankName.Text = Data["data"]["bankName"].ToString();
                                CusMobile.Text = cashDeposit.mobile;
                                lblBcCode.Text = dtMember.Rows[0]["LoginID"].ToString();
                                BCName.Text = dtMember.Rows[0]["Name"].ToString();
                                lblamountCW.Text = txtAmount.Text;
                                lblTransID.Text = cashDeposit.externalRef;
                                lblMark.Attributes.Add("class", "btn btn-success");
                                lblAmount.Text = Data["data"]["bankAccountBalance"].ToString();
                                lblMark.Text = Data["status"].ToString();
                                lblBankNumber.Text = Data["data"]["operatorId"].ToString();
                                lblAadharNo.Text = ("xxxx-xxxx-" + aadhar);

                                lbldate.Text = System.DateTime.Now.ToString();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "$('#ErrorMessage').modal('show');", true);
                                hdnCapture.Value = "";
                            }
                            else
                            {
                                imgSet.Src = "../img/Cross.png";
                                Message.InnerHtml = "CashWithdraw  failed";
                                lblBankName.Text = nationalbank.SelectedItem.Text;
                                CusMobile.Text = cashDeposit.mobile;
                                lblBcCode.Text = dtMember.Rows[0]["LoginID"].ToString();
                                BCName.Text = dtMember.Rows[0]["Name"].ToString();
                                lblTransID.Text = cashDeposit.externalRef;
                                lblMark.Attributes.Add("class", "btn btn-danger");
                                lblMark.Text = Data["status"].ToString();
                                lblBankNumber.Text = "";
                                lbldate.Text = System.DateTime.Now.ToString();
                                lblAadharNo.Text = ("xxxx-xxxx-" + aadhar);
                                Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dtMember.Rows[0]["msrno"].ToString() + "','CW','Failed','" + Data["status"].ToString() + "',0,'','','" + Result + "','" + dtMember.Rows[0]["PackageID"].ToString() + "'");
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
                ErrorShow.AlertMessage(
                    page,
                    "Fill All Information",
                    ConstantsData.CompanyName
                );
            }
        }
        else
        {
            ErrorShow.AlertMessage(
                page,
                "Capping amount.",
                ConstantsData.CompanyName
            );
            return;
        }
    }

    private bool CheckLoginStatus(string outLetId)
    {
        try
        {
            var jsonString = JsonConvert.SerializeObject(new { type = "CashDeposit" });

            var response = NWSPay.CheckLoginStatus(
                jsonString,
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