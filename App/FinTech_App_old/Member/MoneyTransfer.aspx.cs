using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


public partial class Member_MoneyTransfer : System.Web.UI.Page
{
    DataTable dtMember, dtCUS;
    cls_connection objConnection = new cls_connection();
    DataTable dtMemberMaster = new DataTable();
    Page page;
    Company company;

    public Permission permission;
    string AuthID = "";
    JArray bankList = new JArray();

    protected void Page_Load(object sender, EventArgs e)
    {
        var var = Common.GetServiceStatus("16", objConnection);
        if (var.Item1 == true)
        {

            // Only set if WADH exists
            if (Session["PidOptionWadh"] != null)
            {
                txtWadh.Value = Session["PidOptionWadh"].ToString();
            }

            // Initial state
            SetOtpStage(false);

            permission = Permission.GetPermission();
            page = HttpContext.Current.CurrentHandler as Page;
            company = Company.GetCompanyInfo();
            if (Session["dtMember"] != null && Session["CustomerMobile"] != null)
            {
                dtMember = (DataTable)Session["dtMember"];
                dtCUS = objConnection.select_data_dt("select * from CustomerDMT where mobile='" + Session["CustomerMobile"] + "' and IsActive=1");
            }
            else
            {
                if (Session["CustomerMobile"] == null)
                {
                    MultiView1.ActiveViewIndex = 0;
                }
            }
            dtMember = (DataTable)Session["dtMember"];

            if (dtMember != null && (bankList == null || bankList.Count == 0))
            {
                DataTable dtValc = objConnection.select_data_dt(
                    "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

                if (dtValc.Rows.Count == 0)
                {
                    ErrorShow.AlertMessageWithRedirect(
                        page,
                        "You are not logged in. Please login to continue.",
                        "InstantKYC.aspx",
                        ConstantsData.CompanyName
                    );
                    return;
                }

                bankList = GetBanks(dtValc.Rows[0]["OutLetID"].ToString());
            }
        }
        else
        {
            btnSubmit.Text = var.Item2;
            btnSubmit.Enabled = false;
            ErrorShow.ErrorNotify(page, var.Item2);
        }

    }

    protected void ddlBankList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBankList.SelectedIndex > 0)
        {
            string selectedBankId = ddlBankList.SelectedValue;

            DataTable dtBanks = ViewState["BankList"] as DataTable;
            if (dtBanks != null)
            {
                DataRow[] bankRows = dtBanks.Select("BankId = '" + selectedBankId + "'");
                if (bankRows.Length > 0)
                {
                    // Set IFSC
                    string ifsc = bankRows[0]["IFSC"].ToString();
                    txtAddBIFSC.Text = ifsc;

                    // Get NEFT/IMPS flags
                    string neft = bankRows[0]["NEFT"].ToString();
                    string imps = bankRows[0]["IMPS"].ToString();

                    // Populate and show Transfer Type dropdown
                    transferTypeWrapper.Style["display"] = "block";
                    transferTypeList.Items.Clear();

                    if (neft == "1")
                        transferTypeList.Items.Add(new ListItem("NEFT", "NEFT"));
                    if (imps == "1")
                        transferTypeList.Items.Add(new ListItem("IMPS", "IMPS"));

                    if (transferTypeList.Items.Count == 0)
                    {
                        transferTypeList.Items.Add(new ListItem("No Transfer Mode Available", ""));
                    }
                    else
                    {
                        transferTypeList.SelectedIndex = 0; // Select first available
                    }
                }
            }
        }
        else
        {
            txtAddBIFSC.Text = "";
            transferTypeList.Items.Clear();
            transferTypeList.Items.Add(new ListItem("-- Select Transfer Type --", ""));
        }
    }

    protected void Unnamed_Click(object sender, EventArgs e)
    {
        try
        {
            // Validate AEPS KYC outlet config
            DataTable dtValc = objConnection.select_data_dt(
                "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");
            if (dtValc.Rows.Count == 0)
            {
                ErrorShow.AlertMessage(this.Page, "Outlet data not found for AEPS KYC.", ConstantsData.CompanyName);
                return;
            }
            var regPayload = new
            {
                beneficiaryMobileNumber = txtAddBMobileNo.Text,
                remitterMobileNumber = Session["CustomerMobile"].ToString(),
                accountNumber = txtAddBAccountNo.Text.Trim(),
                ifsc = txtAddBIFSC.Text,
                bankId = txtAddBIFSC.Text,
                name = txtAddBName.Text
            };

            string regJson = JsonConvert.SerializeObject(regPayload);
            string regResponseStr = NWSPay.BeneficiaryRegistration(regJson, dtValc.Rows[0]["OutLetID"].ToString());
            JObject Data = JObject.Parse(regResponseStr);

            if (Data["statusCode"]?.ToString() == "OTP")
            {
            }
            else
            {
                ErrorShow.AlertMessage(page, Data["mess"].ToString(), ConstantsData.CompanyName);
            }

        }
        catch (Exception ex)
        {

        }
    }

    private Boolean CheckSendLimitInMonth(double Sendamt)
    {
        Int32 LIMITAMT = 1;

        return (LIMITAMT == 1 ? true : false);
    }

    protected void lnkButtonView_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSendMoney.Text))
        {
            ErrorShow.AlertMessage(this.Page, "Please enter amount to send.", ConstantsData.CompanyName);
            return;
        }
        rptDataView.DataSource = null;
        rptDataView.DataBind();
        DataTable mytable = new DataTable();
        dtMemberMaster = (DataTable)Session["dtMember"];
        mytable.Columns.Add("Amt", typeof(double));
        mytable.Columns.Add("Surcharge", typeof(double));

        int factorAmount = Convert.ToInt32(txtSendMoney.Text);
        int totAmount = Convert.ToInt32(txtSendMoney.Text);
        int remainingAmount = totAmount % factorAmount;
        int loop = totAmount / factorAmount;
        for (int i = 0; i < loop; i++)
        {
            decimal mySurcharge = MoneyTransfer_EkoAPI.GetSurchargeDMT(dtMemberMaster.Rows[0]["PackageID"].ToString(), Convert.ToDecimal(factorAmount));
            DataRow dr1 = mytable.NewRow();
            dr1 = mytable.NewRow();
            dr1["Amt"] = factorAmount;
            dr1["Surcharge"] = mySurcharge;
            mytable.Rows.Add(dr1);
        }
        if (remainingAmount > 0)
        {
            decimal mySurcharge = MoneyTransfer_EkoAPI.GetSurchargeDMT(dtMemberMaster.Rows[0]["PackageID"].ToString(), Convert.ToDecimal(remainingAmount));
            DataRow dr1 = mytable.NewRow();
            dr1 = mytable.NewRow();
            dr1["Amt"] = remainingAmount;
            dr1["Surcharge"] = mySurcharge;
            mytable.Rows.Add(dr1);
        }

        rptDataView.DataSource = mytable;
        rptDataView.DataBind();
    }

    protected void Unnamed_Click1(object sender, EventArgs e)
    {
        Session["DMT_PayworldResponse"] = null;
        Session["DMT_Mobile"] = null;
        Session["CustomerMobile"] = null;
        MultiView1.ActiveViewIndex = 0;
    }

    // Remitter KYC
    // Add 
    protected void btnValidate_Click(object sender, EventArgs e)
    {
        try
        {
            string mobile = txtMobile.Text.Trim();
            string aadhar = txtAdharNumber.Text.Trim();

            if (string.IsNullOrWhiteSpace(mobile) || string.IsNullOrWhiteSpace(aadhar))
            {
                ErrorShow.AlertMessage(this.Page, "Mobile and Aadhar are required.", ConstantsData.CompanyName);
                return;
            }

            // Validate AEPS KYC outlet config
            DataTable dtValc = objConnection.select_data_dt(
                "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");
            if (dtValc.Rows.Count == 0)
            {
                ErrorShow.AlertMessage(this.Page, "Outlet data not found for AEPS KYC.", ConstantsData.CompanyName);
                return;
            }

            // Step 1: Check Remitter Profile
            var customerDetails = new CustomerDetails
            {
                Mobile = mobile,
                MethodName = "customerdetails"
            };
            var validationContext = new ValidationContext(customerDetails);
            var validationResults = new List<ValidationResult>();
            bool isDetailsValid = Validator.TryValidateObject(customerDetails, validationContext, validationResults, true);

            if (!isDetailsValid)
            {
                foreach (ValidationResult result in validationResults)
                {
                    ErrorShow.AlertMessage(this.Page, result.ErrorMessage, ConstantsData.CompanyName);
                    return;
                }
            }

            // Request remitter profile
            var remitRequest = new { mobileNumber = mobile };
            string remitJson = JsonConvert.SerializeObject(remitRequest);
            string remitResult = NWSPay.GetRemitProfile(remitJson, dtValc.Rows[0]["OutLetID"].ToString());

            JObject remitResponse = JObject.Parse(remitResult);

            if (remitResponse["statusCode"]?.ToString() == "TXN")
            {
                Session["CustomerMobile"] = mobile;

                JObject data = remitResponse["data"] as JObject;
                if (data != null)
                {
                    BindCustomerDetails(data);
                    BindBeneficiaryList(data["beneficiaries"] as JArray);
                    ;
                    BindBank(bankList);
                }

                MultiView1.ActiveViewIndex = 2;
                return;
            }
            else if (remitResponse["statusCode"]?.ToString() == "RNF")
            {
                // Customer not found → start registration/OTP process
                Session["CustomerMobile"] = mobile;
                JObject jSTDetails = remitResponse["data"] as JObject;
                Session["PidOptionWadh"] = jSTDetails?["pidOptionWadh"]?.ToString();
                Session["ReferenceKey"] = jSTDetails?["referenceKey"]?.ToString();
            }
            else
            {
                string status = remitResponse["status"]?.ToString() ?? "Unknown Error";
                ErrorShow.AlertMessage(this.Page, "Customer check failed: " + status, ConstantsData.CompanyName);
                return;
            }

            // Step 2: Begin Registration (send OTP)
            var regPayload = new
            {
                mobileNumber = mobile,
                encryptedAadhaar = NWSPay.GetEncryptAadhar(aadhar),
                referenceKey = Session["ReferenceKey"]?.ToString()
            };

            string regJson = JsonConvert.SerializeObject(regPayload);
            string regResponseStr = NWSPay.RemitterRegistration(regJson, dtValc.Rows[0]["OutLetID"].ToString());
            JObject regResponse = JObject.Parse(regResponseStr);

            if (regResponse["statusCode"]?.ToString() == "OTP")
            {
                Session["ReferenceKey"] = regResponse["data"]?["referenceKey"]?.ToString();
                divOTP.Visible = true;
                btnSubmit.Enabled = true;
                btnValidate.Enabled = false;

                string maskedMobile = string.IsNullOrEmpty(mobile) || mobile.Length < 4
                            ? "**********"
                            : new string('*', mobile.Length - 4) + mobile.Substring(mobile.Length - 4);

                ErrorShow.AlertMessage(this.Page, $"OTP sent to {maskedMobile}", ConstantsData.CompanyName);
            }
            else
            {
                ErrorShow.AlertMessage(this.Page, "OTP sending failed. Please retry.", ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(this.Page, "Unexpected error occurred during validation.", ConstantsData.CompanyName);
        }
    }

    private void RefreshCustomerAndBeneficiaries(string mobile, JArray bankList)
    {
        DataTable dtValc = objConnection.select_data_dt(
            "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

        if (dtValc.Rows.Count == 0)
        {
            ErrorShow.AlertMessage(this.Page, "Outlet data not found for AEPS KYC.", ConstantsData.CompanyName);
            return;
        }

        var remitRequest = new { mobileNumber = mobile };
        string remitJson = JsonConvert.SerializeObject(remitRequest);
        string remitResult = NWSPay.GetRemitProfile(remitJson, dtValc.Rows[0]["OutLetID"].ToString());

        JObject remitResponse = JObject.Parse(remitResult);

        if (remitResponse["statusCode"]?.ToString() == "TXN")
        {
            JObject data = remitResponse["data"] as JObject;

            Session["ReferenceKey"] = data?["referenceKey"]?.ToString();
            if (data != null)
            {
                BindBank(bankList);
                BindCustomerDetails(data);
                BindBeneficiaryList(data["beneficiaries"] as JArray);
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string otp = txtOTP.Text.Trim();
            string mobile = txtMobile.Text.Trim();
            string referenceKey = Session["ReferenceKey"]?.ToString();

            // Validate OTP presence
            if (string.IsNullOrEmpty(otp))
            {
                ErrorShow.AlertMessage(this.Page, "Please enter OTP.", ConstantsData.CompanyName);
                return;
            }

            // Validate session key
            if (string.IsNullOrEmpty(referenceKey))
            {
                ErrorShow.AlertMessage(this.Page, "Session expired. Please validate again.", ConstantsData.CompanyName);
                btnValidate.Enabled = true;
                return;
            }

            // Fetch AEPS outlet info
            DataTable dtValc = objConnection.select_data_dt(
                "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

            if (dtValc.Rows.Count == 0)
            {
                ErrorShow.AlertMessage(this.Page, "Outlet data not found. Please complete KYC.", ConstantsData.CompanyName);
                return;
            }

            // Prepare API payload
            var requestData = new
            {
                mobileNumber = mobile,
                otp = otp,
                referenceKey = referenceKey
            };

            string requestJson = JsonConvert.SerializeObject(requestData);

            // Send OTP verification request
            string responseStr = NWSPay.RemitterRegistrationVerify(
                requestJson,
                dtValc.Rows[0]["OutLetID"].ToString()
            );

            JObject response = JObject.Parse(responseStr);

            if (response["statusCode"]?.ToString().ToUpper() == "KYC")
            {
                referenceKey = response["data"]?["referenceKey"]?.ToString();
                // Save updated referenceKey and move to biometric view
                Session["ReferenceKey"] = referenceKey;
                MultiView1.ActiveViewIndex = 1;
            }
            else
            {
                // OTP mismatch or error from API
                string status = response["status"]?.ToString() ?? "OTP verification failed.";
                btnValidate.Enabled = true;
                ErrorShow.AlertMessage(this.Page, status, ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            // Log if needed: ex.ToString()
            ErrorShow.AlertMessage(this.Page, "An unexpected error occurred. Please try again.", ConstantsData.CompanyName);
        }
    }

    protected void btnRegistration_Click(object sender, EventArgs e)
    {
        try
        {
            string referenceKey = Session["ReferenceKey"]?.ToString();
            // Validate session key
            if (string.IsNullOrEmpty(referenceKey))
            {
                ErrorShow.AlertMessage(this.Page, "Session expired. Please validate again.", ConstantsData.CompanyName);
                btnValidate.Enabled = true;
                return;
            }

            // 1. Decode and parse biometric XML response
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(HttpUtility.HtmlDecode(hdnCapture.Value));
            string jsonText = JsonConvert.SerializeXmlNode(doc);

            dynamic bsObj;
            string device = hdnDevice.Value.ToUpper();
            if (device == "MORPHO")
                bsObj = JsonConvert.DeserializeObject<RooteeeMorpho>(jsonText);
            else
                bsObj = JsonConvert.DeserializeObject<Mantra>(jsonText);

            string refID = NWSPay.GenerateExternalRef();

            // 2. Get outlet details from database
            DataTable dtValc = objConnection.select_data_dt(
                "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

            if (dtValc.Rows.Count == 0)
            {
                ErrorShow.AlertMessage(page, "AEPS KYC not found. Please complete KYC first.", ConstantsData.CompanyName);
                return;
            }

            // 3. Construct registration request payload
            string iPAddress = cls_myMember.GetIPAddress();
            Location location = new Location();
            location = location.GetLocationDataCurrent(iPAddress);

            var loginRequest = new ActionLoginRootRemitterKYC
            {
                mobileNumber = Session["CustomerMobile"].ToString(),
                latitude = location.Latitude,
                longitude = location.Longitude,
                externalRef = refID,
                referenceKey = referenceKey,
                captureType = "FINGER",
                consentTaken = "Y",
            };

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
            var thumbRequest = new BiometricDataRemitterKYC
            {
                ci = bsObj.PidData.Skey.Ci,
                hmac = bsObj.PidData.Hmac,
                pidData = bsObj.PidData.Data.Text,
                ts = ts,
                dc = bsObj.PidData.DeviceInfo.Dc,
                mi = bsObj.PidData.DeviceInfo.Mi,
                dpId = bsObj.PidData.DeviceInfo.DpId,
                mc = bsObj.PidData.DeviceInfo.Mc,
                rdsId = bsObj.PidData.DeviceInfo.RdsId,
                rdsVer = bsObj.PidData.DeviceInfo.RdsVer,
                srno = srno,
                Skey = bsObj.PidData.Skey.Text,
                pidOptionWadh = Session["PidOptionWadh"]?.ToString()
            };

            loginRequest.biometricData = thumbRequest;

            // 4. Make API call to remitterKYC endpoint
            string result = NWSPay.RemitterKYC(
                loginRequest.GetJson(),
                dtValc.Rows[0]["OutLetID"].ToString()
            );

            JObject data = JObject.Parse(result);

            if (data["statusCode"]?.ToString() == "TXN")
            {
                Session.Remove("ReferenceKey");
                Session.Remove("PidOptionWadh");
                Session.Remove("serviceType");

                // 5. Insert customer into local DB using values from dtValc (AEPS KYC table)
                string fullName = dtValc.Rows[0]["Name"].ToString();
                string mobile = dtValc.Rows[0]["Mobile"].ToString();
                string address = dtValc.Rows[0]["Address"].ToString();
                string pan = dtValc.Rows[0]["Pan"].ToString();
                string aadhar = dtValc.Rows[0]["Aadhar"].ToString();

                DataTable dt = objConnection.select_data_dt(
                    $"AddEditCustomerDMT '{fullName}','{mobile}','{address}','{pan}','{aadhar}',{dtMember.Rows[0]["Msrno"]}");

                // ✅ Refresh beneficiary list
                if (!string.IsNullOrEmpty(mobile))
                {
                    RefreshCustomerAndBeneficiaries(mobile, bankList);
                }

                ErrorShow.AlertMessage(page, "Registration Successful", ConstantsData.CompanyName);
                MultiView1.ActiveViewIndex = 2;
            }
            else
            {
                string message = data["status"]?.ToString() ?? "Registration failed.";

                MultiView1.ActiveViewIndex = 0;
                Session.Remove("ReferenceKey");
                Session.Remove("PidOptionWadh");
                Session.Remove("CustomerMobile");
                Session.Remove("serviceType");

                ErrorShow.AlertMessage(page, message, ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(page, "Biometric registration failed: " + ex.Message, ConstantsData.CompanyName);
        }
    }

    protected void btnBack1_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    // Beneficiary
    // Add
    protected void btnBeneficiarySendOTP_Click(object sender, EventArgs e)
    {
        try
        {
            // Step 1: Validate AEPS KYC outlet config
            DataTable dtValc = objConnection.select_data_dt(
                "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

            if (dtValc.Rows.Count == 0)
            {
                ErrorShow.AlertMessage(this.Page, "Outlet data not found for AEPS KYC.", ConstantsData.CompanyName);
                return;
            }

            // Step 2: Read input values
            string bankId = ddlBankList.SelectedValue;
            string accountNo = txtAddBAccountNo.Text.Trim();
            string ifsc = txtAddBIFSC.Text.Trim().ToUpper();
            string beneficiaryMobileNumber = txtAddBMobileNo.Text.Trim();
            string remitterMobileNumber = Session["CustomerMobile"]?.ToString();
            string name = txtAddBName.Text.Trim();

            // Step 3: Validate inputs
            if (string.IsNullOrWhiteSpace(remitterMobileNumber))
            {
                ErrorShow.AlertMessage(this.Page, "Session expired. Please reload and try again.", ConstantsData.CompanyName);
                return;
            }

            if (string.IsNullOrWhiteSpace(bankId) || bankId == "0")
            {
                ErrorShow.AlertMessage(this.Page, "Please select a valid bank.", ConstantsData.CompanyName);
                return;
            }

            if (string.IsNullOrWhiteSpace(accountNo) || accountNo.Length < 9)
            {
                ErrorShow.AlertMessage(this.Page, "Please enter a valid Account Number.", ConstantsData.CompanyName);
                return;
            }

            if (string.IsNullOrWhiteSpace(ifsc) || ifsc.Length != 11)
            {
                ErrorShow.AlertMessage(this.Page, "Please enter a valid 11-digit IFSC code.", ConstantsData.CompanyName);
                return;
            }

            if (string.IsNullOrWhiteSpace(beneficiaryMobileNumber) || !System.Text.RegularExpressions.Regex.IsMatch(beneficiaryMobileNumber, @"^\d{10}$"))
            {
                ErrorShow.AlertMessage(this.Page, "Please enter a valid 10-digit Mobile Number.", ConstantsData.CompanyName);
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                ErrorShow.AlertMessage(this.Page, "Please enter Beneficiary Name.", ConstantsData.CompanyName);
                return;
            }
            // Step 4: Prepare payload and call API
            var regPayload = new
            {
                beneficiaryMobileNumber = beneficiaryMobileNumber,
                remitterMobileNumber = remitterMobileNumber,
                accountNumber = accountNo,
                ifsc = ifsc,
                bankId = bankId,
                name = name
            };

            string regJson = JsonConvert.SerializeObject(regPayload);
            string regResponseStr = NWSPay.BeneficiaryRegistration(
                regJson, dtValc.Rows[0]["OutLetID"].ToString());

            JObject Data = JObject.Parse(regResponseStr);

            // Step 5: Handle response and show OTP panel
            if (Data["statusCode"]?.ToString() == "OTP")
            {
                // Save reference key in session
                Session["ReferenceKey"] = Data["data"]?["referenceKey"]?.ToString();
                Session["BeneficiaryId"] = Data["data"]?["beneficiaryId"]?.ToString();

                // Masked mobile for message
                string maskedMobile = string.IsNullOrEmpty(remitterMobileNumber) || remitterMobileNumber.Length < 4
                    ? "**********"
                    : new string('*', remitterMobileNumber.Length - 4) + remitterMobileNumber.Substring(remitterMobileNumber.Length - 4);

                // ✅ Show OTP input and Submit button
                pnlOTP.Visible = true;
                divSendOTP.Visible = false;
                divSubmitBeneficiary.Visible = true;

                // Alert user
                ErrorShow.AlertMessage(this.Page, $"OTP sent to {maskedMobile}", ConstantsData.CompanyName);
            }
            else
            {
                pnlOTP.Visible = false;
                divSubmitBeneficiary.Visible = false;
                divSendOTP.Visible = true;

                ErrorShow.AlertMessage(this.Page, Data["mess"]?.ToString() ?? "Something went wrong.", ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(this.Page, "Unexpected error occurred: " + ex.Message, ConstantsData.CompanyName);
        }
    }

    protected void btnSubmitBeneficiary_Click(object sender, EventArgs e)
    {
        try
        {
            // Step 1: Validate AEPS KYC outlet config
            DataTable dtValc = objConnection.select_data_dt(
                "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

            if (dtValc.Rows.Count == 0)
            {
                ErrorShow.AlertMessage(this.Page, "Outlet data not found for AEPS KYC.", ConstantsData.CompanyName);
                return;
            }

            string enteredOtp = txtOtpB.Text.Trim();
            if (string.IsNullOrWhiteSpace(enteredOtp))
            {
                ErrorShow.AlertMessage(this.Page, "Please enter OTP.", ConstantsData.CompanyName);
                return;
            }

            // Step 2: Read input values
            string remitterMobileNumber = Session["CustomerMobile"]?.ToString();
            string beneficiaryId = Session["BeneficiaryId"]?.ToString();
            string referenceKey = Session["ReferenceKey"]?.ToString();

            // Step 3: Validate inputs
            if (string.IsNullOrWhiteSpace(remitterMobileNumber) || string.IsNullOrWhiteSpace(beneficiaryId) || string.IsNullOrWhiteSpace(referenceKey))
            {
                ErrorShow.AlertMessage(this.Page, "Session expired. Please reload and try again.", ConstantsData.CompanyName);
                return;
            }

            // Step 4: Prepare payload and call API
            var regPayload = new
            {
                beneficiaryId = beneficiaryId,
                remitterMobileNumber = remitterMobileNumber,
                otp = enteredOtp,
                referenceKey = referenceKey,
            };

            string regJson = JsonConvert.SerializeObject(regPayload);
            string regResponseStr = NWSPay.BeneficiaryRegistrationVerify(
                regJson, dtValc.Rows[0]["OutLetID"].ToString());

            JObject Data = JObject.Parse(regResponseStr);

            // Step 5: Handle response and show OTP panel
            if (Data["statusCode"]?.ToString() == "TXN")
            {
                Session.Remove("ReferenceKey");
                Session.Remove("BeneficiaryId");

                // ✅ Refresh beneficiary list
                string mobile = Session["CustomerMobile"]?.ToString();
                if (!string.IsNullOrEmpty(mobile))
                {
                    RefreshCustomerAndBeneficiaries(mobile, bankList);
                }

                // ✅ Show success message
                ErrorShow.AlertMessage(this.Page, "Beneficiary added successfully.", ConstantsData.CompanyName);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMyModal",
                "var myModal = bootstrap.Modal.getInstance(document.getElementById('myModal')); " +
                "if(myModal){ myModal.hide(); }", true);

            }
            else
            {
                // ❌ Show error from API response
                string message = Data["status"]?.ToString() ?? "Beneficiary addition failed. Please try again.";
                ErrorShow.AlertMessage(this.Page, message, ConstantsData.CompanyName);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMyModal",
                "var myModal = bootstrap.Modal.getInstance(document.getElementById('myModal')); " +
                "if(myModal){ myModal.hide(); }", true);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(this.Page, "Unexpected error occurred: " + ex.Message, ConstantsData.CompanyName);
        }
    }

    protected void btnVerifyBankAccount_Click(object sender, EventArgs e)
    {
        try
        {
            dtMemberMaster = (DataTable)Session["dtMember"];
            decimal MyBalance = 0;
            string refID = NWSPay.GenerateExternalRef();

            DataTable dtWalletBalance = new DataTable();
            dtWalletBalance = objConnection.select_data_dt("select * from VIEW_MEMBERBALANCE where Msrno=" + dtMemberMaster.Rows[0]["Msrno"] + "");
            if (dtWalletBalance.Rows.Count > 0)
            {
                MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["Balance"]);
            }
            if (MyBalance >= Convert.ToDecimal(MoneyTransfer_EkoAPI.Beneficiary_VerificationFee)) // Verification fee
            {

                if (MoneyTransfer_EkoAPI.CheckMinimumBalance(Convert.ToInt32(dtMemberMaster.Rows[0]["Msrno"]), Convert.ToDecimal(MoneyTransfer_EkoAPI.Beneficiary_VerificationFee), MyBalance))
                {

                    string Desc = "" + txtAddBAccountNo.Text + " Account Verification With TransID-" + refID;
                    string narration = "Account Verification TransID-" + refID;
                    decimal deductAmount = (Convert.ToDecimal(MoneyTransfer_EkoAPI.Beneficiary_VerificationFee));

                    DataTable dtValc = objConnection.select_data_dt(
                        "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

                    if (dtValc.Rows.Count == 0)
                    {
                        ErrorShow.AlertMessage(this.Page, "Outlet data not found for AEPS KYC.", ConstantsData.CompanyName);
                        return;
                    }

                    // Step 2: Read input values
                    string accountNo = txtAddBAccountNo.Text.Trim();
                    string ifsc = txtAddBIFSC.Text.Trim().ToUpper();
                    string name = txtAddBName.Text.Trim();

                    // Step 3: Validate inputs
                    if (string.IsNullOrWhiteSpace(Session["CustomerMobile"]?.ToString()))
                    {
                        ErrorShow.AlertMessage(this.Page, "Session expired. Please reload and try again.", ConstantsData.CompanyName);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(accountNo) || accountNo.Length < 9)
                    {
                        ErrorShow.AlertMessage(this.Page, "Please enter a valid Account Number.", ConstantsData.CompanyName);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(ifsc) || ifsc.Length != 11)
                    {
                        ErrorShow.AlertMessage(this.Page, "Please enter a valid 11-digit IFSC code.", ConstantsData.CompanyName);
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        ErrorShow.AlertMessage(this.Page, "Please enter Beneficiary Name.", ConstantsData.CompanyName);
                        return;
                    }

                    string iPAddress = cls_myMember.GetIPAddress();
                    Location location = new Location();
                    location = location.GetLocationDataCurrent(iPAddress);

                    // Step 4: Prepare payload and call API
                    var payload = new
                    {
                        payee = new
                        {
                            name = name,
                            accountNumber = accountNo,
                            bankIfsc = ifsc
                        },
                        externalRef = refID,
                        consent = "Y",
                        pennyDrop = "YES",
                        latitude = location.Latitude,
                        longitude = location.Longitude
                    };


                    string regJson = JsonConvert.SerializeObject(payload);
                    string regResponseStr = NWSPay.AccountVerify(
                        regJson, dtValc.Rows[0]["OutLetID"].ToString());

                    JObject Data = JObject.Parse(regResponseStr);

                    // Step 5: Handle response and show OTP panel
                    if (Data["statusCode"]?.ToString() == "TXN")
                    {
                        string verifiedName = Data["data"]?["payee"]?["name"]?.ToString();
                        int Val = objConnection.update_data("AddEditMoneyTransfer " + dtMemberMaster.Rows[0]["Msrno"].ToString() + "," + MoneyTransfer_EkoAPI.Beneficiary_VerificationFee + ",0,0,'Success','IMPS','','" + refID + "','','" + Data["data"]["txnReferenceId"].ToString() + "','" + regResponseStr + "'");
                        if (Val > 0)
                        {
                            objConnection.select_data_dt("exec [PROC_EWALLETTRANSACTION]  '" + dtMemberMaster.Rows[0]["LoginID"].ToString() + "', " + deductAmount + ", 'Dr','" + narration + "','" + Desc + "'," + ConstantsData.SAccountVerfication + ",'" + refID + "'");
                            txtAddBName.Text = verifiedName;
                        }

                        Session["ReferenceKey"] = Data["data"]?["referenceKey"]?.ToString();

                        // ✅ Show success message
                        ErrorShow.AlertMessage(this.Page, $"Account verified successfully. Beneficiary Name: {verifiedName}", ConstantsData.CompanyName);

                        // ✅ Show and enable Send OTP
                        divSendOTP.Visible = true;
                        btnSendOTP.Enabled = true;

                        // 🔒 Lock fields after verification
                        txtAddBAccountNo.Enabled = false;
                        txtAddBIFSC.Enabled = false;
                        txtAddBName.Enabled = false;
                        ddlBankList.Enabled = false;

                        // 🔒 Hide or disable Verify button
                        btnVerifyBAccount.Visible = false;

                        ErrorShow.AlertMessage(this.Page, Data["status"]?.ToString(), ConstantsData.CompanyName);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowMessage(2,'" + Data["status"].ToString() + "');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowMessage(2,'Low Balance! Minimum balance limit exceed.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowMessage(2,'You have Insufficient Balance.');", true);
            }

        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(this.Page, "Unexpected error occurred: " + ex.Message, ConstantsData.CompanyName);
        }
    }

    private void BindCustomerDetails(JObject data)
    {
        lblDataCName.Text = $"{data["firstName"]} {data["lastName"]}";
        lblDataCMobile.Text = data["mobileNumber"]?.ToString();
        lblDataCTotalLimit.Text = data["limitTotal"]?.ToString();
        lblConsumedLimit.Text = data["limitConsumed"]?.ToString();
        txtAddressCus.Text = $"{data["city"]}, {data["pincode"]}";
    }

    private void BindBeneficiaryList(JArray beneficiaries)
    {
        if (beneficiaries == null || beneficiaries.Count == 0)
        {
            rptDataBeneficiary.DataSource = null;
            rptDataBeneficiary.DataBind();
            return;
        }

        DataTable dtBeni = new DataTable();
        dtBeni.Columns.Add("id");
        dtBeni.Columns.Add("name");
        dtBeni.Columns.Add("accountNumber");
        dtBeni.Columns.Add("ifsc");
        dtBeni.Columns.Add("bank");

        foreach (var beni in beneficiaries)
        {
            var row = dtBeni.NewRow();
            row["id"] = beni["id"]?.ToString();
            row["name"] = beni["name"]?.ToString();
            row["accountNumber"] = beni["account"]?.ToString();
            row["ifsc"] = beni["ifsc"]?.ToString();
            row["bank"] = beni["bank"]?.ToString();
            dtBeni.Rows.Add(row);
        }

        rptDataBeneficiary.DataSource = dtBeni;
        rptDataBeneficiary.DataBind();
    }

    private JArray GetBanks(string outLetID)
    {
        JArray banks = new JArray();

        string result = NWSPay.GetRemitBankList(
            outLetID);

        JObject json = JObject.Parse(result);

        if (json["statusCode"]?.ToString() == "TXN")
        {
            banks = (JArray)json["data"];
        }

        return banks;
    }

    private string GetBankId(JArray banks, string ifsc)
    {
        if (string.IsNullOrWhiteSpace(ifsc) || banks == null || banks.Count == 0)
            return "0";

        var match = banks
            .FirstOrDefault(b => string.Equals(
                b["ifscGlobal"]?.ToString()?.Trim(),
                ifsc.Trim(),
                StringComparison.OrdinalIgnoreCase));

        return match?["bankId"]?.ToString() ?? "0";
    }

    private void BindBank(JArray banks)
    {
        if (banks != null && banks.Count > 0)
        {
            DataTable dtBanks = new DataTable();
            dtBanks.Columns.Add("BankId");
            dtBanks.Columns.Add("BankName");
            dtBanks.Columns.Add("IFSC");
            dtBanks.Columns.Add("NEFT");
            dtBanks.Columns.Add("IMPS");

            foreach (var bank in banks)
            {
                string bankId = bank["bankId"]?.ToString();
                string bankName = bank["bankName"]?.ToString();
                string ifsc = bank["ifscGlobal"]?.ToString();
                string neft = bank["neftEnabled"]?.ToString();
                string imps = bank["impsEnabled"]?.ToString();

                if (!string.IsNullOrEmpty(bankId))
                {
                    dtBanks.Rows.Add(
                        bankId,
                        string.IsNullOrEmpty(bankName) ? "Bank ID " + bankId : bankName,
                        ifsc ?? "",
                        neft ?? "0",
                        imps ?? "0"
                    );
                }
            }

            if (dtBanks.Rows.Count > 0)
            {
                ddlBankList.DataSource = dtBanks;
                ddlBankList.DataTextField = "BankName";
                ddlBankList.DataValueField = "BankId";
                ddlBankList.DataBind();
                ddlBankList.Items.Insert(0, new ListItem("-- Select Bank --", "0"));

                // Optionally store dtBanks in ViewState for later lookup during dropdown change
                ViewState["BankList"] = dtBanks;
            }
            else
            {
                ddlBankList.Items.Clear();
                ddlBankList.Items.Insert(0, new ListItem("-- No Banks Found --", "0"));
                ErrorShow.AlertMessage(this.Page, "No active banks available.", ConstantsData.CompanyName);
            }
        }
        else
        {
            ErrorShow.AlertMessage(this.Page, "Bank data is empty from the API.", ConstantsData.CompanyName);
        }
    }

    protected void rdoAuthentication_SelectedIndexChanged(object sender, EventArgs e)
    {
        divOTP.Visible = true;
        txtOTP.Enabled = true;
        Random generator = new Random();
        int r = generator.Next(100000, 999999);
        string OTP = r.ToString();
        string Message = "Login OTP is : " + OTP;
        Session["OTPVerify"] = OTP;
        string[] ValueArray = new string[4];
        ValueArray[0] = "User";
        ValueArray[1] = OTP;
        MultiView1.ActiveViewIndex = 1;

        string str = SMS.SendWithV(Session["CustomerMobile"].ToString(), ConstantsData.Login_OTP_SMS, ValueArray, company.MemberID);
        ErrorShow.AlertMessage(page, "OTP send on " + Session["CustomerMobile"].ToString() + " your mobile no", ConstantsData.CompanyName);

    }

    // Delete
    protected void rptDataBeneficiary_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            // Step 1: Validate AEPS KYC outlet config
            DataTable dtValc = objConnection.select_data_dt(
                "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

            if (dtValc.Rows.Count == 0)
            {
                ErrorShow.AlertMessage(this.Page, "Outlet data not found for AEPS KYC.", ConstantsData.CompanyName);
                return;
            }

            if (e.CommandName == "DeleteBeni")
            {
                // Step 2: Read input values
                string remitterMobileNumber = Session["CustomerMobile"]?.ToString();
                string beneficiaryId = e.CommandArgument.ToString();


                // Step 3: Validate inputs
                if (string.IsNullOrWhiteSpace(remitterMobileNumber))
                {
                    ErrorShow.AlertMessage(this.Page, "Session expired. Please reload and try again.", ConstantsData.CompanyName);
                    return;
                }

                if (string.IsNullOrWhiteSpace(beneficiaryId))
                {
                    ErrorShow.AlertMessage(this.Page, "Beneficiary ID is required.", ConstantsData.CompanyName);
                    return;
                }

                // Step 4: Prepare payload and call API
                var regPayload = new
                {
                    remitterMobileNumber = remitterMobileNumber,
                    beneficiaryId = beneficiaryId
                };

                string json = JsonConvert.SerializeObject(regPayload);

                string regResponseStr = NWSPay.BeneficiaryDelete(
                    json, dtValc.Rows[0]["OutLetID"].ToString());

                JObject Data = JObject.Parse(regResponseStr);

                // Step 5: Handle response and show OTP panel
                if (Data["statusCode"]?.ToString() == "OTP")
                {
                    Session["ReferenceKey"] = Data["data"]?["referenceKey"]?.ToString();
                    hddBeneficiaryId.Value = beneficiaryId;
                    pnlOTP.Visible = true;
                    divSubmitBeneficiary.Visible = true;
                    divSendOTP.Visible = false;
                    txtOtpB.Text = "";

                    string maskedMobile = string.IsNullOrEmpty(remitterMobileNumber) || remitterMobileNumber.Length < 4
                        ? "**********"
                        : new string('*', remitterMobileNumber.Length - 4) + remitterMobileNumber.Substring(remitterMobileNumber.Length - 4);

                    string script = $"alert('OTP sent to {maskedMobile}'); $('#otpModal').modal('show');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowOtpModal", script, true);
                }
                else
                {
                    pnlOTP.Visible = false;
                    divSubmitBeneficiary.Visible = false;
                    divSendOTP.Visible = true;

                    ErrorShow.AlertMessage(this.Page, Data["mess"]?.ToString() ?? "Something went wrong.", ConstantsData.CompanyName);
                }
            }
            else if (e.CommandName == "SendMoney")
            {
                txtSendMoney.Text = "";
                txtComment.Text = "";
                DisableInputFields(false);
                SetOtpStage(false);
                RefreshCustomerAndBeneficiaries(Session["CustomerMobile"]?.ToString(), bankList);

                // Get controls from the Repeater row
                Label lblName = (Label)e.Item.FindControl("lblBName");
                Label lblAccount = (Label)e.Item.FindControl("lblBAccountno");
                Label lblIFSC = (Label)e.Item.FindControl("lblBifsc");

                // Set values in modal fields
                lblSendMoneyName.Text = lblName.Text;
                lblSendMoneyAccountNo.Text = lblAccount.Text;
                lblSendMoneyIFSC.Text = lblIFSC.Text;
                lblbeneficiaryId.Text = e.CommandArgument.ToString();

                // ✅ Set BankId in modal's HiddenField
                hdnSendMoneyBankId.Value = GetBankId(bankList, lblSendMoneyIFSC.Text);

                // Trigger modal display
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#SendMonay').modal('show');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(this.Page, "Unexpected error occurred: " + ex.Message, ConstantsData.CompanyName);
        }
    }

    protected void btnSubmitOtp_Click(object sender, EventArgs e)
    {
        try
        {
            // Step 1: Validate AEPS KYC outlet config
            DataTable dtValc = objConnection.select_data_dt(
                "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

            if (dtValc.Rows.Count == 0)
            {
                ErrorShow.AlertMessage(this.Page, "Outlet data not found for AEPS KYC.", ConstantsData.CompanyName);
                return;
            }

            string enteredOtp = otpDeleteBeni.Text.Trim();
            if (string.IsNullOrWhiteSpace(enteredOtp))
            {
                ErrorShow.AlertMessage(this.Page, "Please enter OTP.", ConstantsData.CompanyName);
                return;
            }

            // Step 2: Read input values
            string remitterMobileNumber = Session["CustomerMobile"]?.ToString();
            string beneficiaryId = hddBeneficiaryId.Value;
            string referenceKey = Session["ReferenceKey"]?.ToString();

            // Step 3: Validate inputs
            if (string.IsNullOrWhiteSpace(remitterMobileNumber) || string.IsNullOrWhiteSpace(beneficiaryId) || string.IsNullOrWhiteSpace(referenceKey))
            {
                ErrorShow.AlertMessage(this.Page, "Session expired. Please reload and try again.", ConstantsData.CompanyName);
                return;
            }

            // Step 4: Prepare payload and call API
            var regPayload = new
            {
                beneficiaryId = beneficiaryId,
                remitterMobileNumber = remitterMobileNumber,
                otp = enteredOtp,
                referenceKey = referenceKey,
            };

            string regJson = JsonConvert.SerializeObject(regPayload);
            string regResponseStr = NWSPay.BeneficiaryDeleteVerify(
                regJson, dtValc.Rows[0]["OutLetID"].ToString());

            JObject Data = JObject.Parse(regResponseStr);

            // Step 5: Handle response and show OTP panel
            if (Data["statusCode"]?.ToString() == "TXN")
            {
                Session.Remove("ReferenceKey");
                Session.Remove("BeneficiaryId");

                string mobile = Session["CustomerMobile"]?.ToString();
                if (!string.IsNullOrEmpty(mobile))
                {
                    RefreshCustomerAndBeneficiaries(mobile, bankList);
                }

                // ✅ Show success message
                ErrorShow.AlertMessage(this.Page, "Beneficiary deleted successfully.", ConstantsData.CompanyName);
            }
            else
            {
                // ❌ Show error from API response
                string message = Data["status"]?.ToString() ?? "Beneficiary addition failed. Please try again.";
                ErrorShow.AlertMessage(this.Page, message, ConstantsData.CompanyName);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(this.Page, "Unexpected error occurred: " + ex.Message, ConstantsData.CompanyName);
        }
    }

    // Send Money
    protected void btnDMTSendOtp_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSendMoney.Text != "" && Convert.ToDouble(txtSendMoney.Text) > ConstantsData.DMTSetLimitLow)
            {
                double remianinSendLimit = ConstantsData.DMTSetLimitHigh;
                if (remianinSendLimit >= Convert.ToDouble(txtSendMoney.Text))
                {
                    if (CheckSendLimitInMonth(Convert.ToDouble(txtSendMoney.Text)))
                    {
                        if (Convert.ToDouble(txtSendMoney.Text) <= ConstantsData.DMTSetLimitHigh)
                        {
                            hddGroupTransID.Value = MoneyTransfer_EkoAPI.GetUniqueSessionID();

                            dtMemberMaster = (DataTable)Session["dtMember"];
                            if (dtMemberMaster.Rows.Count > 0)
                            {
                                // Step 1: Validate AEPS KYC outlet config
                                DataTable dtValc = objConnection.select_data_dt(
                                    "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

                                if (dtValc.Rows.Count == 0)
                                {
                                    ErrorShow.AlertMessage(this.Page, "Outlet data not found for AEPS KYC.", ConstantsData.CompanyName);
                                    return;
                                }

                                // Perform basic validations if needed
                                string amount = txtSendMoney.Text.Trim();
                                string comment = txtComment.Text.Trim();

                                string remitterMobileNumber = Session["CustomerMobile"]?.ToString();
                                string referenceKey = Session["ReferenceKey"]?.ToString();

                                // Step 3: Validate inputs
                                if (string.IsNullOrWhiteSpace(remitterMobileNumber) || string.IsNullOrWhiteSpace(referenceKey))
                                {
                                    ErrorShow.AlertMessage(this.Page, "Session expired. Please reload and try again.", ConstantsData.CompanyName);
                                    return;
                                }

                                if (string.IsNullOrWhiteSpace(amount) || amount == "0")
                                {
                                    ErrorShow.AlertMessage(this.Page, "Please select a valid amount.", ConstantsData.CompanyName);
                                    return;
                                }

                                // Step 4: Prepare payload and call API
                                var regPayload = new
                                {
                                    remitterMobileNumber = remitterMobileNumber,
                                    amount = amount,
                                    referenceKey = referenceKey,
                                };

                                string regJson = JsonConvert.SerializeObject(regPayload);
                                string regResponseStr = NWSPay.GenerateTransactionOtp(
                                    regJson, dtValc.Rows[0]["OutLetID"].ToString());

                                JObject Data = JObject.Parse(regResponseStr);

                                // Step 5: Handle response and show OTP panel
                                if (Data["statusCode"]?.ToString() == "OTP")
                                {
                                    // Save reference key in session
                                    Session["ReferenceKey"] = Data["data"]?["referenceKey"]?.ToString();

                                    // Masked mobile for message
                                    string maskedMobile = string.IsNullOrEmpty(remitterMobileNumber) || remitterMobileNumber.Length < 4
                                        ? "**********"
                                        : new string('*', remitterMobileNumber.Length - 4) + remitterMobileNumber.Substring(remitterMobileNumber.Length - 4);

                                    // Disable fields and switch view
                                    DisableInputFields(true);
                                    SetOtpStage(true);

                                    ErrorShow.AlertMessage(page, "OTP send on " + maskedMobile + " your mobile no", ConstantsData.CompanyName);
                                }
                                else
                                {
                                    ErrorShow.AlertMessage(this.Page, Data["mess"]?.ToString() ?? "Something went wrong.", ConstantsData.CompanyName);
                                }
                            }
                            else
                            {
                                objConnection.update_data("INSERT INTO RECHARGEURL(URL,TRANSID)VALUES('6','') ");
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowAlertMessage('You Can only send 25000 in one time');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowAlertMessage('Your monthly limit of 1 Lakh Rs is exhausted, kindly try next month');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowAlertMessage('Monthly Limit Completed');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowAlertMessage('Please enter right Amount');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(this.Page, "Unexpected error occurred: " + ex.Message, ConstantsData.CompanyName);
        }
    }

    protected void btnSendMonay_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSendMoney.Text != "" && Convert.ToDouble(txtSendMoney.Text) > ConstantsData.DMTSetLimitLow)
            {
                double remianinSendLimit = ConstantsData.DMTSetLimitHigh;
                if (remianinSendLimit >= Convert.ToDouble(txtSendMoney.Text))
                {
                    if (CheckSendLimitInMonth(Convert.ToDouble(txtSendMoney.Text)))
                    {
                        if (Convert.ToDouble(txtSendMoney.Text) <= ConstantsData.DMTSetLimitHigh)
                        {
                            hddGroupTransID.Value = MoneyTransfer_EkoAPI.GetUniqueSessionID();

                            dtMemberMaster = (DataTable)Session["dtMember"];
                            if (dtMemberMaster.Rows.Count > 0)
                            {
                                decimal mySurcharge = 0;
                                int factorAmount = Convert.ToInt32(txtSendMoney.Text);
                                int totAmount = Convert.ToInt32(txtSendMoney.Text);
                                int remainingAmount = totAmount % factorAmount;
                                int loop = totAmount / factorAmount;
                                for (int i = 0; i < loop; i++)
                                {
                                    mySurcharge = mySurcharge + MoneyTransfer_EkoAPI.GetSurchargeDMT(dtMemberMaster.Rows[0]["PackageID"].ToString(), Convert.ToDecimal(factorAmount));
                                }
                                if (remainingAmount > 0)
                                {
                                    mySurcharge = mySurcharge + MoneyTransfer_EkoAPI.GetSurchargeDMT(dtMemberMaster.Rows[0]["PackageID"].ToString(), Convert.ToDecimal(remainingAmount));
                                }

                                dtMemberMaster = (DataTable)Session["dtMember"];
                                DataTable dtWalletBalance = new DataTable();
                                decimal MyBalance = 0;
                                dtWalletBalance = objConnection.select_data_dt("select * from VIEW_MEMBERBALANCE_WITH_HOLD where Msrno=" + dtMemberMaster.Rows[0]["Msrno"] + "");

                                if (dtWalletBalance.Rows.Count > 0)
                                {
                                    MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["UsableBalance"]);
                                }
                                if (MyBalance >= Convert.ToDecimal(txtSendMoney.Text.Trim()) + mySurcharge)
                                {
                                    string agentID = System.Guid.NewGuid().ToString().Replace('-', 'D');
                                    decimal deductAmount = Convert.ToDecimal(txtSendMoney.Text) + mySurcharge;
                                    string Dec = "DMT Topup with Charges Rs." + Convert.ToDecimal(txtSendMoney.Text).ToString() + " + " + mySurcharge + " + Txn:-" + agentID;//"DMT2 Fund Transfer(Account: " + lblSendMoneyAccountNo.Text + ")";                                       
                                    string narration = "DMT TransID-" + agentID;
                                    DataTable dtval = objConnection.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMemberMaster.Rows[0]["loginid"].ToString() + "','" + deductAmount + "','DR','" + narration + "','" + Dec + "'," + ConstantsData.SMoneyTransfer + ",'" + agentID + "'");

                                    if (dtval.Rows[0]["msrno"].ToString() == "1")
                                    {
                                        bool response = FundTransfer(Convert.ToDecimal(txtSendMoney.Text), mySurcharge, dtMemberMaster, agentID);
                                        if (response)
                                        {
                                            objConnection.update_data("DMT_DistributeCommission " + dtMemberMaster.Rows[0]["Msrno"].ToString() + "," + Convert.ToDecimal(txtSendMoney.Text) + ",'" + agentID + "'");
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "alert('Your Transaction Successful');location.replace('ReceiptDMTNew.aspx?GUID=" + agentID + "')", true);

                                            // Reset form
                                            DisableInputFields(false);
                                            SetOtpStage(false);
                                            textDmtOTP.Text = "";
                                            txtSendMoney.Text = "";
                                            txtComment.Text = "";
                                        }
                                        else
                                        {
                                            ErrorShow.AlertMessage(page, "Your Transaction Failed", ConstantsData.CompanyName);
                                        }
                                    }
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowMessage(2,'Insufficient Balance');", true);

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
                            else
                            {
                                objConnection.update_data("INSERT INTO RECHARGEURL(URL,TRANSID)VALUES('6','') ");

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowAlertMessage('You Can only send 25000 in one time');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowAlertMessage('Your monthly limit of 1 Lakh Rs is exhausted, kindly try next month');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowAlertMessage('Monthly Limit Completed');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Script", "ShowAlertMessage('Please enter right Amount');", true);
            }
        }
        catch (Exception ex)
        {
            ErrorShow.AlertMessage(this.Page, "Unexpected error occurred: " + ex.Message, ConstantsData.CompanyName);
        }
    }

    private bool FundTransfer(decimal transferAmount, decimal SurchargeAmount, DataTable dtMemberMaster, string agentID)
    {
        bool response = false;
        try
        {
            string bankId = hdnSendMoneyBankId.Value;
            int Val = objConnection.update_data("AddEditMoneyTransfer " + dtMemberMaster.Rows[0]["Msrno"].ToString() + "," + transferAmount + "," + SurchargeAmount + "," + bankId + ",'Pending','IMPS','','" + agentID + "','',''");
            if (Val > 0)
            {

                // Step 1: Validate AEPS KYC outlet config
                DataTable dtValc = objConnection.select_data_dt(
                    "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

                if (dtValc.Rows.Count == 0)
                {
                    ErrorShow.AlertMessage(this.Page, "Outlet data not found for AEPS KYC.", ConstantsData.CompanyName);
                    return response;
                }

                // Step 2: Read input values
                string enteredOtp = textDmtOTP.Text.Trim();

                // Proceed with money transfer
                string accountNumber = lblSendMoneyAccountNo.Text.Trim();
                string ifsc = lblSendMoneyIFSC.Text.Trim();
                string transferMode = "IMPS";
                string comment = txtComment.Text.Trim();
                string remitterMobileNumber = Session["CustomerMobile"]?.ToString();
                string referenceKey = Session["ReferenceKey"]?.ToString();
                string refID = NWSPay.GenerateExternalRef();

                // 3. Construct registration request payload
                string iPAddress = cls_myMember.GetIPAddress();
                Location location = new Location();
                location = location.GetLocationDataCurrent(iPAddress);

                // Step 3: Validate inputs
                if (string.IsNullOrWhiteSpace(remitterMobileNumber) || string.IsNullOrWhiteSpace(referenceKey))
                {
                    ErrorShow.AlertMessage(this.Page, "Session expired. Please reload and try again.", ConstantsData.CompanyName);
                    return response;
                }

                if (string.IsNullOrWhiteSpace(transferMode) || transferMode == "0")
                {
                    ErrorShow.AlertMessage(this.Page, "Please select a valid amount.", ConstantsData.CompanyName);
                    return response;
                }

                // Step 4: Prepare payload and call API
                var regPayload = new
                {
                    remitterMobileNumber = remitterMobileNumber,
                    accountNumber = accountNumber,
                    ifsc = ifsc,
                    transferMode = transferMode,
                    transferAmount = transferAmount.ToString(),
                    latitude = location.Latitude,
                    longitude = location.Longitude,
                    referenceKey = referenceKey,
                    otp = enteredOtp,
                    externalRef = refID,
                };

                string regJson = JsonConvert.SerializeObject(regPayload);
                string result = NWSPay.Transaction(
                    regJson, dtValc.Rows[0]["OutLetID"].ToString());

                JObject Data = JObject.Parse(result);

                // Step 5: Handle response and show OTP panel
                if (Data["statusCode"]?.ToString() == "TXN")
                {
                    objConnection.update_data("Update MoneyTransfer set Status='" + "Success" + "',VendorID='" + Data["data"]["txnReferenceId"].ToString() + "',Response='" + result + "',Message='" + Data["status"].ToString() + "',RRN='" + Data["data"]["externalRef"].ToString() + "' where OrderID='" + Data["orderId"].ToString() + "'");

                    response = true;
                }
                else if (Data["statusCode"]?.ToString() == "TUP")
                {
                    objConnection.update_data("Update MoneyTransfer set Status='" + "Pending" + "',VendorID='" + Data["data"]["txnReferenceId"].ToString() + "',Response='" + result + "',Message='" + Data["status"].ToString() + "',RRN='" + Data["data"]["externalRef"].ToString() + "' where OrderID='" + Data["orderid"].ToString() + "'");

                    response = true;
                }
                else
                {
                    objConnection.update_data("Update MoneyTransfer set Status='Refund',Response='" + result + "' where OrderID='" + agentID + "'");
                    DataTable dtHistory = new DataTable();
                    dtHistory = objConnection.select_data_dt("select * from MoneyTransfer where OrderID='" + agentID + "'");

                    Int32 CountCR = 0;
                    CountCR = objConnection.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where narration like '%" + agentID + "%' and factor='cr'");
                    if (CountCR == 0)
                    {
                        string Narration = "DMT TransID-" + agentID;
                        objConnection.update_data("Exec [PROC_EWALLETTRANSACTION] '" + dtMemberMaster.Rows[0]["loginid"].ToString() + "'," + Convert.ToDecimal(dtHistory.Rows[0]["FinalAmount"]) + ", 'CR','Refund - " + Narration + "',''," + ConstantsData.SMoneyTransfer + ",'" + dtHistory.Rows[0]["OrderID"].ToString() + "'");
                    }
                    lblStatus.Text = (Data["status"].ToString() == "Balance Insufficient" ? "Service down please try after few minute" : Data["status"].ToString());
                    lblStatus.ForeColor = System.Drawing.Color.Red;
                    ErrorShow.AlertMessage(this.Page, Data["status"]?.ToString() ?? "Something went wrong.", ConstantsData.CompanyName);
                }

                return response;
            }
            else
            {
                return response;
            }
        }
        catch
        {
            return response;
        }
    }

    private void DisableInputFields(bool disable)
    {
        txtSendMoney.Enabled = !disable;
        txtComment.Enabled = !disable;
        dllSendAccountType.Enabled = !disable;
    }

    private void SetOtpStage(bool isOtpStage)
    {
        rowOtp.Visible = isOtpStage;
        rowSendMoney.Visible = isOtpStage;
        btnDMTSendOtp.Visible = !isOtpStage;
    }
}