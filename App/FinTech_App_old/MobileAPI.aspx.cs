using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Configuration;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using RestSharp;
using System.ComponentModel.DataAnnotations;

public partial class API_MobileAPI : System.Web.UI.Page
{
    cls_connection objConnection = new cls_connection();
    DataTable dt = new DataTable();
    cls_myMember clsm = new cls_myMember();
    string CompanyName = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                NameValueCollection nvc = Request.Form;
                if (nvc["Request"] != null)
                {
                    JObject jObject = JObject.Parse(nvc["Request"]);
                    string DeviceID = Request.Headers["DeviceID"].ToString().Trim();
                    if (Request.Headers["CompanyName"] != null)
                        CompanyName = Request.Headers["CompanyName"].ToString().Trim();

                    string strUser = "";
                    string strPassword = "";
                    string methodName = Convert.ToString(jObject["MethodName"]).Trim().ToLower();
                    if (methodName == "getsupport" || methodName == "getpermission" || methodName == "sendotp" || methodName == "getcompanydetails" || methodName == "getstate" || methodName == "forgetpassword" || methodName == "getrolelist" || methodName == "offerlist" || methodName == "securitytips" || methodName == "signup" || methodName == "setdeviceid")
                    {
                        strPassword = strUser = "";
                    }
                    else
                    {
                        strUser = Convert.ToString(jObject["UserID"]).Trim();// Request.Headers["UserID"].ToString().Trim();
                        strPassword = Convert.ToString(jObject["Password"]).Trim();// Request.Headers["Password"].ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(DeviceID))
                    {
                        #region [ 35.Forget Password ]

                        if (methodName == "forgetpassword")
                        {
                            if (jObject["UserID"] != null)
                            {
                                string userID = Convert.ToString(jObject["UserID"]).Trim();

                                DataTable dt = new DataTable();
                                DataTable dtMember;
                                dtMember = objConnection.select_data_dt("select * from member where mobile='" + userID + "'");
                                if (dtMember.Rows.Count > 0)
                                {
                                    string[] ValueArray = new string[4];
                                    ValueArray[0] = dtMember.Rows[0]["Name"].ToString();
                                    ValueArray[1] = dtMember.Rows[0]["LoginID"].ToString();
                                    ValueArray[2] = dtMember.Rows[0]["Password"].ToString();
                                    ValueArray[3] = dtMember.Rows[0]["LoginPin"].ToString();
                                    // string str = SMS.SendWithV(dtMember.Rows[0]["Mobile"].ToString(), 9, ValueArray);
                                    SuccessMessage("Login Details Send on Your Register Number");
                                    return;
                                }
                                else
                                {
                                    ErrorMessage("User not persent");
                                }
                            }
                            else
                            {
                                ErrorMessage("Insufficient Parameter");
                            }
                        }
                        if (methodName == "getsupport")
                        {
                            if (jObject["UserID"] != null && jObject["Password"] != null)
                            {

                                string userID = Convert.ToString(jObject["UserID"]).Trim();
                                string password = Convert.ToString(jObject["Password"]).Trim();
                                string Token = Convert.ToString(jObject["Token"]).Trim();
                                string Msrno = "1";
                                if (CompanyName != "")
                                {
                                    Msrno = objConnection.select_data_scalar_string("select MemberID from company where isactive=1 and WebsiteURL like '%" + CompanyName + "%'");
                                }
                                DataTable dt = objConnection.select_data_dt("EXEC ManageSupportSystem 'GetActive',0," + Msrno + "");
                                if (dt.Rows.Count > 0)
                                {
                                    Response.Write(ConvertDataTabletoString(dt)); return;
                                }
                            }
                            else
                            {
                                ErrorMessage("Insufficient Parameter");
                            }
                        }
                        if (methodName == "getpermission")
                        {
                            string userID = Convert.ToString(jObject["UserID"]).Trim();
                            string password = Convert.ToString(jObject["Password"]).Trim();
                            dt = objConnection.select_data_dt("SELECT *  FROM AdminAccountSetting");
                            if (dt.Rows.Count > 0)
                            {
                                Response.Write(ConvertDataTabletoString(dt));
                            }
                            else
                            {
                                ErrorMessage("Password Changed Un-Successfully ..!!");
                            }
                        }
                        if (methodName == "setdeviceid")
                        {
                            if (jObject["UserID"] != null)
                            {
                                string userID = Convert.ToString(jObject["UserID"]).Trim();
                                Int32 val = 0;
                                val = objConnection.update_data("update Member set DeviceID='" + DeviceID + "' where mobile='" + userID + "'");
                                if (val > 0)
                                {
                                    SuccessMessage("success");
                                    return;
                                }
                                else
                                {
                                    ErrorMessage("failed");
                                    return;
                                }
                            }
                        }
                        if (methodName == "sendotp")
                        {
                            if (jObject["MobileNo"] != null)
                            {
                                string MobileNo = Convert.ToString(jObject["MobileNo"]).Trim();

                                Random generator = new Random();
                                int r = generator.Next(1000, 9999);
                                string OTP = r.ToString();
                                string Message = "Login OTP is : " + OTP;
                                Session["OTP"] = OTP;

                                string[] ValueArray = new string[4];
                                ValueArray[0] = "User";
                                ValueArray[1] = OTP;
                                // string str = SMS.SendWithV(MobileNo, 37, ValueArray);
                                string Result = "{\"OTP\":\"" + OTP + "\",\"Mobile\":\"" + MobileNo + "\"}";
                                Response.Write(SuccessArray(Result));
                                return;
                            }
                            else
                            {
                                ErrorMessage("Enter Mobile No");
                                return;
                            }
                        }
                        if (methodName == "getrolelist")
                        {
                            GetRoleList();
                            return;

                        }
                        if (methodName == "checknumber")
                        {
                            if (jObject["Mobile"] != null)
                            {
                                string Mobile = Convert.ToString(jObject["Mobile"]).Trim();

                                Int32 val = 0;
                                val = objConnection.select_data_scalar_int("select count(*) from Member where Mobile='" + Mobile + "'");
                                if (val == 0)
                                {
                                    SuccessMessage("success");
                                    return;
                                }
                                else
                                {
                                    ErrorMessage("Mobile No Alreay exists");
                                    return;
                                }
                            }
                        }
                        if (methodName == "getstate")
                        {
                            Getstate();
                            return;

                        }
                        if (methodName == "getpackagelist")
                        {
                            if (jObject["RoleID"] != null)
                            {
                                string RoleID = Convert.ToString(jObject["RoleID"]).Trim();

                                GetPackageList(RoleID);
                                return;
                            }
                        }
                        if (methodName == "signup")
                        {
                            if (jObject["StateID"] != null && jObject["CityName"] != null && jObject["RoleID"] != null && jObject["PackageID"] != null && jObject["TitleID"] != null && jObject["Name"] != null && jObject["ShopName"] != null && jObject["Email"] != null && jObject["Mobile"] != null && jObject["Aadhar"] != null && jObject["Pan"] != null && jObject["Gender"] != null && jObject["Pin"] != null && jObject["Address"] != null && jObject["DOB"] != null)
                            {
                                string RoleID = Convert.ToString(jObject["RoleID"]).Trim();
                                string PackageID = Convert.ToString(jObject["PackageID"]).Trim();
                                string TitleID = Convert.ToString(jObject["TitleID"]).Trim();
                                string Name = Convert.ToString(jObject["Name"]).Trim();
                                string ShopName = Convert.ToString(jObject["ShopName"]).Trim();
                                string Email = Convert.ToString(jObject["Email"]).Trim();
                                string Mobile = Convert.ToString(jObject["Mobile"]).Trim();
                                string Aadhar = Convert.ToString(jObject["Aadhar"]).Trim();
                                string Pan = Convert.ToString(jObject["Pan"]).Trim();
                                string Gender = Convert.ToString(jObject["Gender"]).Trim();
                                string Pin = Convert.ToString(jObject["Pin"]).Trim();
                                string Address = Convert.ToString(jObject["Address"]).Trim();
                                string StateID = Convert.ToString(jObject["StateID"]).Trim();
                                string CityName = Convert.ToString(jObject["CityName"]).Trim();
                                string DOB = Convert.ToString(jObject["DOB"]).Trim();
                                Int32 ParentMsrno = 1;
                                if (jObject["msrno"] != null)
                                {
                                    Int32 mmsrno = objConnection.select_data_scalar_int("select msrno from member where loginid='" + jObject["msrno"].ToString() + "'");
                                    ParentMsrno = mmsrno;
                                }
                                signup(RoleID, PackageID, TitleID, Name, ShopName, Email, Mobile, Aadhar, Pan, Gender, Pin, Address, DOB, CityName, StateID, ParentMsrno);
                                return;

                            }
                        }
                        if (methodName == "offerlist")
                        {

                            DataTable dt = new DataTable();
                            dt = objConnection.select_data_dt("EXEC ManageBannerImage 'GetAllBYBannerType','3'");
                            if (dt.Rows.Count > 0)
                            {
                                Response.Write(ConvertDataTabletoString(dt));
                                return;

                            }
                            else
                                ErrorMessage("offerlist not available");


                        }
                        if (methodName == "securitytips")
                        {


                            DataTable dt = new DataTable();
                            dt = objConnection.select_data_dt("select Name,Description from SecurityTip");
                            if (dt.Rows.Count > 0)
                            {
                                Response.Write(ConvertDataTabletoString(dt));
                                return;

                            }
                            else
                                ErrorMessage("Tips not available");


                        }
                        #endregion

                        if (CheckDeviceID(strUser, strPassword, DeviceID, methodName))
                        {


                            if (CheckUser(strUser, strPassword, DeviceID, methodName))
                            {

                                #region [ 1.GetCompanyDetails]
                                if (methodName == "verifyuserinotherphone")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["LoginPin"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string LoginPin = Convert.ToString(jObject["LoginPin"]).Trim();
                                        string deviceID = DeviceID;
                                        VerifyUserInOtherPhone(userID, password, deviceID, LoginPin);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 1.GetCompanyDetails]
                                else if (methodName == "getcompanydetails")
                                {
                                    string CID = "1";
                                    if (jObject["URL"] != null)
                                    {
                                        CID = objConnection.select_data_scalar_string("select ID from company where isactive=1 and WebsiteURL like '%" + jObject["URL"] + "%'");
                                    }
                                    if (jObject["URL"] == null)
                                    {
                                        GetCompanyDetails(CID);
                                    }
                                    else
                                    {
                                        GetCompanyDetails(CID);
                                    }
                                }
                                #endregion
                                #region [ 2.GetLoginDetails ]

                                else if (methodName == "getlogindetails")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["AppType"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string deviceID = DeviceID;
                                        string AppType = Convert.ToString(jObject["AppType"]).Trim();
                                        string IP = "";
                                        if (jObject["ip"] != null)
                                        {
                                            IP = jObject["ip"].ToString();
                                        }
                                        GetLoginDetails(userID, password, deviceID, AppType, IP);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion
                                #region [ 3.GETCUSTOMERDETAILS ]

                                else if (methodName == "getcustomerdetails")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["MobileNo"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string MobileNo = Convert.ToString(jObject["MobileNo"]).Trim();
                                        string deviceID = DeviceID;
                                        string AppType = Convert.ToString(jObject["AppType"]).Trim();
                                        GetLoginDetails(userID, password, deviceID, AppType, "");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion
                                #region [ 4.GetOperators ]

                                else if (methodName == "getoperators")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["ServiceTypeID"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string deviceID = DeviceID;
                                        string param1 = Convert.ToString(jObject["ServiceTypeID"]).Trim();
                                        GetOperators(userID, password, deviceID, param1);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion
                                #region [ 5.GetCircles ]

                                else if (methodName == "getcircles")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetCircles(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion
                                #region [ 6.RechargeRequest ]

                                else if (methodName == "rechargerequest")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["Number"] != null
                                        && jObject["Amount"] != null && jObject["Operator"] != null
                                        && jObject["Circle"] != null && jObject["CANumber"] != null
                                        && jObject["Cycle"] != null && jObject["DueDate"] != null
                                        && jObject["Account"] != null && jObject["IPAddress"] != null
                                        && jObject["IMEINumber"] != null && jObject["CustomerName"] != null
                                        && jObject["CustomerMobile"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string Number = Convert.ToString(jObject["Number"]).Trim();
                                        string Amount = Convert.ToString(jObject["Amount"]).Trim();
                                        string Operator = Convert.ToString(jObject["Operator"]).Trim();
                                        string Circle = Convert.ToString(jObject["Circle"]).Trim();
                                        string caNumber = Convert.ToString(jObject["CANumber"]).Trim();
                                        string Cycle = Convert.ToString(jObject["Cycle"]).Trim();
                                        string DueDate = Convert.ToString(jObject["DueDate"]).Trim();
                                        string Account = Convert.ToString(jObject["Account"]).Trim();
                                        string IPAddress = Convert.ToString(jObject["IPAddress"]).Trim();
                                        string IMEINumber = Convert.ToString(jObject["IMEINumber"]).Trim();
                                        string CustomerName = Convert.ToString(jObject["CustomerName"]).Trim();
                                        string CustomerMobile = Convert.ToString(jObject["CustomerMobile"]).Trim();
                                        string CoupanCodeStr = "";
                                        string CoupanAmount = "0";
                                        string PromoCode = "";
                                        string GUID = "";
                                        if (PromoCode.ToLower() == "null")
                                        {
                                            PromoCode = "";
                                        }
                                        RechargeRequest(userID, password, Number, Amount, Operator, Circle, caNumber, Cycle, DueDate, Account, IPAddress, IMEINumber, CustomerName, CustomerMobile, CoupanCodeStr, CoupanAmount, PromoCode, GUID);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 7.PaymentRequest ]

                                else if (methodName == "paymentrequest")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["ReferanceNo"] != null && jObject["PaymentDate"] != null && jObject["PaymentMode"] != null && jObject["Remark"] != null && jObject["Amount"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string ReferanceNo = Convert.ToString(jObject["ReferanceNo"]).Trim();
                                        string PaymentDate = Convert.ToString(jObject["PaymentDate"]).Trim();
                                        string PaymentMode = Convert.ToString(jObject["PaymentMode"]).Trim();
                                        string Remark = Convert.ToString(jObject["Remark"]).Trim();
                                        string Amount = Convert.ToString(jObject["Amount"]).Trim();
                                        PaymentRequest(userID, password, ReferanceNo, PaymentDate, PaymentMode, Remark, Amount);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 8.GetPaymentMode ]

                                else if (methodName == "getpaymentmode")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetPaymentMode(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 9.Changed Pin ]

                                else if (methodName == "changedpin")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["OldPin"] != null && jObject["NewPin"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string OldPin = Convert.ToString(jObject["OldPin"]).Trim();
                                        string NewPin = Convert.ToString(jObject["NewPin"]).Trim();
                                        ChangedPin(userID, password, OldPin, NewPin);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 9.ChangedPassword ]

                                else if (methodName == "changedpassword")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["OldPassword"] != null && jObject["NewPassword"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string OldPassword = Convert.ToString(jObject["OldPassword"]).Trim();
                                        string NewPassword = Convert.ToString(jObject["NewPassword"]).Trim();
                                        ChangedPassword(userID, password, OldPassword, NewPassword);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 10.HistoryReport ]

                                else if (methodName == "historyreport")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["DateFrom"] != null && jObject["DateTo"] != null && jObject["SERVICEID"] != null && jObject["Status"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string param1 = Convert.ToString(jObject["DateFrom"]).Trim();
                                        string param2 = Convert.ToString(jObject["DateTo"]).Trim();
                                        string param3 = Convert.ToString(jObject["SERVICEID"]).Trim();
                                        //  string param3 = Convert.ToString(jObject["PageNumber"]).Trim();
                                        // string param4 = Convert.ToString(jObject["PageSize"]).Trim();  
                                        string param5 = Convert.ToString(jObject["Status"]).Trim();
                                        DataTable dt = objConnection.select_data_dt("Exec [ProcRecharge_AndroidService] 'GetTransactionSummarry', '" + userID + "','" + password + "','', '" + param3 + "', '" + param1 + "', '" + param2 + "',null, '" + param5 + "'");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                        else
                                        {
                                            ErrorMessage("Not Available");
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion
                                #region [ 11.GetService ]

                                else if (methodName == "getservice")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetService(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 12.GetService ]


                                #endregion

                                #region [ 12.GetBalance ]

                                else if (methodName == "getbalance")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetBalance(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 13.GetBcDeatils ]

                                else if (methodName == "getbcdeatils")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetBcDeatils(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 14.AEPS WithdrwalRequest ]

                                else if (methodName == "aepswithdrwalrequest")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["BankID"] != null && jObject["Accountno"] != null && jObject["Remark"] != null && jObject["AepsWithdrwalType"] != null && jObject["Amount"] != null && jObject["TransactionMode"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string Remark = Convert.ToString(jObject["Remark"]).Trim();
                                        string AepsWithdrwalType = Convert.ToString(jObject["AepsWithdrwalType"]).Trim();
                                        string Amount = Convert.ToString(jObject["Amount"]).Trim();
                                        string TransactionMode = Convert.ToString(jObject["TransactionMode"]).Trim();
                                        string BankID = Convert.ToString(jObject["BankID"]).Trim();
                                        string Accountno = Convert.ToString(jObject["Accountno"]).Trim();


                                        AEPSWithdrwalRequest(userID, password, Remark, AepsWithdrwalType, Amount, TransactionMode, BankID, Accountno);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 14.AEPSWithdrwalRequestHistory ]

                                else if (methodName == "aepswithdrwalrequesthistory")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["FromDate"] != null && jObject["ToDate"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string FromDate = Convert.ToString(jObject["FromDate"]).Trim();
                                        string ToDate = Convert.ToString(jObject["ToDate"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        AEPSWithdrwalRequestHistory(userID, password, FromDate, ToDate, Page, Size);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 16.GetBankList ]

                                else if (methodName == "getbanklist")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetBankList(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 17.AddBankDetails ]

                                else if (methodName == "addbankdetails")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["BankID"] != null && jObject["IfscCode"] != null && jObject["AccountNo"] != null && jObject["AccountHolderName"] != null && jObject["BranchName"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string BankID = Convert.ToString(jObject["BankID"]).Trim();
                                        string IfscCode = Convert.ToString(jObject["IfscCode"]).Trim();
                                        string AccountNo = Convert.ToString(jObject["AccountNo"]).Trim();
                                        string AccountHolderName = Convert.ToString(jObject["AccountHolderName"]).Trim();
                                        string BranchName = Convert.ToString(jObject["BranchName"]).Trim();
                                        AddBankDetails(userID, password, BankID, IfscCode, AccountNo, AccountHolderName, BranchName);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 17.EditAddBankDetails ]

                                else if (methodName == "editaddbankdetails")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["ID"] != null && jObject["BankID"] != null && jObject["IfscCode"] != null && jObject["AccountNo"] != null && jObject["AccountHolderName"] != null && jObject["BranchName"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string BankID = Convert.ToString(jObject["BankID"]).Trim();
                                        string ID = Convert.ToString(jObject["BankID"]).Trim();
                                        string EditID = Convert.ToString(jObject["BankID"]).Trim();
                                        string IfscCode = Convert.ToString(jObject["IfscCode"]).Trim();
                                        string AccountNo = Convert.ToString(jObject["AccountNo"]).Trim();
                                        string AccountHolderName = Convert.ToString(jObject["AccountHolderName"]).Trim();
                                        string BranchName = Convert.ToString(jObject["BranchName"]).Trim();
                                        EditAddBankDetails(userID, password, BankID, IfscCode, AccountNo, AccountHolderName, BranchName, EditID);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 18.GetBankDetails ]

                                else if (methodName == "getbankdetails")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetBankDetails(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 19.GetServiceList ]

                                else if (methodName == "getservicelist")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetServiceList(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 20.GetUPIDetails ]

                                else if (methodName == "getupidetails")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetUPIDetails(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 21.CreateUPI ]

                                else if (methodName == "createupi")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["category"] != null && jObject["address"] != null && jObject["name"] != null && jObject["pan"] != null && jObject["upi_id"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string category = Convert.ToString(jObject["category"]).Trim();
                                        string address = Convert.ToString(jObject["address"]).Trim();
                                        string name = Convert.ToString(jObject["name"]).Trim();
                                        string pan = Convert.ToString(jObject["pan"]).Trim();
                                        string upi_id = Convert.ToString(jObject["upi_id"]).Trim();
                                        CreateUPI(userID, password, category, address, name, pan, upi_id);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 22.GetVirtualAccountDetails ]

                                else if (methodName == "getvirtualaccountdetails")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetVirtualAccountDetails(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 23.CreateVirtualAccount ]

                                else if (methodName == "createvirtualaccount")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["reference_number"] != null && jObject["udf1"] != null && jObject["udf2"] != null && jObject["udf3"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string reference_number = Convert.ToString(jObject["reference_number"]).Trim();
                                        string udf1 = Convert.ToString(jObject["udf1"]).Trim();
                                        string udf2 = Convert.ToString(jObject["udf2"]).Trim();
                                        string udf3 = Convert.ToString(jObject["udf3"]).Trim();
                                        CreateVirtualAccount(userID, password, reference_number, udf1, udf2, udf3);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 24.GetUPIID ]

                                else if (methodName == "getupiid")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetUPIID(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 25.GetDocList ]

                                else if (methodName == "getdoclist")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetDocList(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 26.GetKYCList ]

                                else if (methodName == "getkyclist")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        GetKYCList(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 26.UploadKYC ]

                                else if (methodName == "uploadkyc")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["DocID"] != null && jObject["DocNumber"] != null && jObject["Side"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string DocNumber = Convert.ToString(jObject["DocNumber"]).Trim();

                                        string Side = Convert.ToString(jObject["Side"]).Trim();
                                        string DocID = Convert.ToString(jObject["DocID"]).Trim();
                                        var fuDoc = HttpContext.Current.Request.Files["Doc1"];
                                        var fuDocBack = HttpContext.Current.Request.Files["Doc2"];
                                        //if (Convert.ToInt16(Side) == 2)
                                        //{
                                        //    fuDocBack = null;
                                        //}


                                        DataTable dtMember = objConnection.select_data_dt("select msrno from member where mobile='" + userID + "'");
                                        string imgFront, Front = "";
                                        Bitmap bmFront = null;
                                        string gdFront = Guid.NewGuid().ToString() + ".jpeg";

                                        string imgBack, Back = "";
                                        Bitmap bmBack = null;
                                        string gdBack = Guid.NewGuid().ToString() + ".jpeg";
                                        if (!string.IsNullOrEmpty(fuDoc.FileName) && Convert.ToInt16(Side) == 2 ? !string.IsNullOrEmpty(fuDocBack.FileName) : true)
                                        {
                                            try
                                            {
                                                bmFront = Resize_Image(fuDoc.InputStream, 1200, 600);
                                                imgFront = Server.MapPath("../images/KYC/") + gdFront;
                                                Front = gdFront.ToString();
                                                bmFront.Save(imgFront, ImageFormat.Jpeg);
                                                if (Convert.ToInt16(Side) == 2)
                                                {
                                                    bmBack = Resize_Image(fuDocBack.InputStream, 1200, 600);
                                                    imgBack = Server.MapPath("../images/KYC/") + gdBack;
                                                    Back = gdBack.ToString();
                                                    bmBack.Save(imgBack, ImageFormat.Jpeg);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Response.Write("Error occured: " + ex.Message.ToString());
                                            }
                                            finally
                                            {
                                                //imgFront = string.Empty;
                                                //// bmFront.Dispose();
                                                //fuDoc = new FileUpload();
                                                //if (Convert.ToInt16(Side) == 2)
                                                //{
                                                //    imgBack = string.Empty;
                                                //    // bmBack.Dispose();
                                                //    fuDocBack = new FileUpload();
                                                //}
                                            }
                                            try
                                            {
                                                DataTable dtSave = objConnection.select_data_dt("Exec Proc_AddEditMemberKYCDocument 0,'" + Convert.ToInt16(dtMember.Rows[0]["Msrno"]) + "','" + Convert.ToInt16(DocID) + "','" + DocNumber + "','" + Front + "','" + Back + "'");
                                                if (Convert.ToInt16(dtSave.Rows[0]["ID"]) > 0)
                                                {
                                                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Inserted Successfully ..!!');window.location.replace('UploadKYC.aspx')", true);
                                                    //ErrorShow.Success(page, "");
                                                    //FillDocument();
                                                    //FillData(msrno: Convert.ToInt16(dtMember.Rows[0]["Msrno"]));
                                                    SuccessMessage("Save SuccessFully");
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorMessage(ex.Message.ToString());
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                #endregion

                                #region [ 28.AEPS Report ]

                                else if (methodName == "aepsreport")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["DateFrom"] != null && jObject["DateTo"] != null && jObject["Status"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string param1 = Convert.ToString(jObject["DateFrom"]).Trim();
                                        string param2 = Convert.ToString(jObject["DateTo"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        DataTable dt = new DataTable();
                                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

                                        dt = objConnection.select_data_dt("Exec [ProcAEPS_Mahagram_ManageHistory] 'GetByMsrNoMobile', '" + Msrno + "','" + param1 + "', '" + param2 + "',0,''," + Page + ",'" + Size + "'");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                        else
                                        {
                                            ErrorMessage("Not Available");
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion


                                #region [ 29.Loan Type ]

                                else if (methodName == "loantype")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        loantype(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 30.LOANSUBCATEGORY ]

                                else if (methodName == "loansubcategory")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["LoanTypeID"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string LoanTypeID = Convert.ToString(jObject["LoanTypeID"]).Trim();
                                        loansubcategory(userID, password, LoanTypeID);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 31.INSTALLMENTTYPE ]

                                else if (methodName == "installmenttype")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        installmenttype(userID, password);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 31.CALCULATEEMI ]

                                else if (methodName == "calculateemi")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["Password"] != null && jObject["installmenttype"] != null && jObject["Amount"] != null
                                         && jObject["loantenure"] != null && jObject["loansubcat"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string installmenttype = Convert.ToString(jObject["installmenttype"]).Trim();
                                        string Amount = Convert.ToString(jObject["Amount"]).Trim();
                                        string loantenure = Convert.ToString(jObject["loantenure"]).Trim();
                                        string loansubcat = Convert.ToString(jObject["loansubcat"]).Trim();

                                        calculateemi(userID, password, installmenttype, Amount, loantenure, loansubcat);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 32.Loan Report ]

                                else if (methodName == "loanreport")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        DataTable dt = new DataTable();
                                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

                                        dt = objConnection.select_data_dt("Exec DBO.SP_LOAN_DISBURSED_DETAIL '',''," + Msrno + "");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                        else
                                        {
                                            ErrorMessage("Not Available");
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 33. Pay EMI ]

                                else if (methodName == "payemi")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["ApplicationNumber"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string ApplicationNumber = Convert.ToString(jObject["ApplicationNumber"]).Trim();
                                        DataTable dt = new DataTable();
                                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");
                                        DataSet ds = new DataSet();

                                        if (ApplicationNumber != "")
                                        {
                                            string applicationids = objConnection.select_data_scalar_string("Select ApplicationID from loan_disbursement where loanaccountNo='" + ApplicationNumber + "'");
                                            if (applicationids.ToString() != "")
                                            {
                                                ds = objConnection.select_data_ds("EXEC sp_payment_plan " + applicationids.ToString() + ",0");
                                                if (ds.Tables.Count > 0)
                                                {
                                                    Response.Write(ConvertDataTabletoString(ds.Tables[1]));
                                                }
                                                else
                                                {
                                                    ErrorMessage("Loan Account No Does Not Exists..!");
                                                }
                                            }
                                            else
                                            {
                                                ErrorMessage("Loan Account No Does Not Exists..!");
                                            }

                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 34.DownLine Report ]

                                else if (methodName == "downline")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        //&& jObject["RoleID"] != null
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        string RoleID = "0"; // Convert.ToString(jObject["RoleID"]).Trim();

                                        DataTable dt = new DataTable();
                                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

                                        dt = objConnection.select_data_dt("PROC_DownLine '','" + Msrno + "','" + Page + "','" + Size + "','" + RoleID + "'");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                        else
                                        {
                                            ErrorMessage("Not Available");
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 34.DownLine Report ]

                                else if (methodName == "downlinerole")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();


                                        DataTable dt = new DataTable();
                                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

                                        dt = objConnection.select_data_dt("PROC_DownLine_Role '" + Msrno + "'");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                        else
                                        {
                                            ErrorMessage("Not Available");
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion


                                #region [ 35.CashDeposit ]

                                else if (methodName == "cashdeposit")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["FromDate"] != null && jObject["ToDate"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string FromDate = Convert.ToString(jObject["FromDate"]).Trim();
                                        string ToDate = Convert.ToString(jObject["ToDate"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        CashDepositTransactionHIstory(userID, FromDate, ToDate, Page, Size);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 36.Aadharpay ]

                                else if (methodName == "aadharpay")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["FromDate"] != null && jObject["ToDate"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string FromDate = Convert.ToString(jObject["FromDate"]).Trim();
                                        string ToDate = Convert.ToString(jObject["ToDate"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        AadharpayTransactionHIstory(userID, FromDate, ToDate, Page, Size);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion
                                #region [ 36.ServiceList ]

                                else if (methodName == "servicelist")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        DataTable dt = new DataTable();
                                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

                                        dt = objConnection.select_data_dt("PROC_Active_service " + Msrno + "");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                        else
                                        {
                                            ErrorMessage("Not Available");
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 36.Purchase Service ]

                                else if (methodName == "purchaseservice")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["ServiceID"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string ServiceID = Convert.ToString(jObject["ServiceID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        PurchaseService(userID, password, ServiceID);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 37.UPIList ]

                                else if (methodName == "upilist")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        UPIList(userID);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 37.AddUpi ]

                                else if (methodName == "addupi")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["upiid"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string upiid = Convert.ToString(jObject["upiid"]).Trim();
                                        AddUpi(userID, upiid);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 37.SendAmountUsingUPI ]

                                else if (methodName == "sendamtupi")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["upiid"] != null && jObject["Amount"] != null && jObject["TPIN"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string upiid = Convert.ToString(jObject["upiid"]).Trim();
                                        string TPIN = Convert.ToString(jObject["TPIN"]).Trim();
                                        string Amount = Convert.ToString(jObject["Amount"]).Trim();
                                        //SendAmountUsingUPI(userID, upiid, Amount, TPIN);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 35.upisendhistory ]

                                else if (methodName == "upisendhistory")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["FromDate"] != null && jObject["ToDate"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string FromDate = Convert.ToString(jObject["FromDate"]).Trim();
                                        string ToDate = Convert.ToString(jObject["ToDate"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        upisendhistory(userID, FromDate, ToDate, Page, Size);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 35.money transfer history ]

                                else if (methodName == "moneytransferhsitory")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["FromDate"] != null && jObject["ToDate"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string FromDate = Convert.ToString(jObject["FromDate"]).Trim();
                                        string ToDate = Convert.ToString(jObject["ToDate"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        moneytransferhsitory(userID, FromDate, ToDate, Page, Size);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                else if (methodName == "dispute")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["TransID"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string TransID = Convert.ToString(jObject["TransID"]).Trim();
                                        string Password = Convert.ToString(jObject["Password"]).Trim();
                                        DataTable dt = new DataTable();
                                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

                                        DataTable dthis = objConnection.select_data_dt("select * from tblRecharge_History where TransID='" + TransID + "' and Status='Success' and Dispute=0 and ReSolve=0");
                                        if (dthis.Rows.Count > 0)
                                        {
                                            objConnection.update_data("update tblRecharge_History set Dispute=1 where TransID='" + TransID + "' and Status='Success'");
                                            SuccessMessage("Dispute Add Successfully");

                                        }
                                        else
                                        {
                                            ErrorMessage("Dispute Already Exits");
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }
                                #endregion

                                #region [ 35.RNFIMATMHIstory ]

                                else if (methodName == "rnfimatmhistory")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["FromDate"] != null && jObject["ToDate"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string FromDate = Convert.ToString(jObject["FromDate"]).Trim();
                                        string ToDate = Convert.ToString(jObject["ToDate"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        rnfihistory(userID, FromDate, ToDate, Page, Size);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 35.Mahagram MATM History ]

                                else if (methodName == "mahamatmhistory")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["FromDate"] != null && jObject["ToDate"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string FromDate = Convert.ToString(jObject["FromDate"]).Trim();
                                        string ToDate = Convert.ToString(jObject["ToDate"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        mahamatmhistory(userID, FromDate, ToDate, Page, Size);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 35.cashdeposit History ]

                                else if (methodName == "cashdeposithistory")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["FromDate"] != null && jObject["ToDate"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string FromDate = Convert.ToString(jObject["FromDate"]).Trim();
                                        string ToDate = Convert.ToString(jObject["ToDate"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        cashdeposithistory(userID, FromDate, ToDate, Page, Size);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                #region [ 35.ewallethistory ]


                                else if (methodName == "ewallethistory")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["FromDate"] != null && jObject["ToDate"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string FromDate = Convert.ToString(jObject["FromDate"]).Trim();
                                        string ToDate = Convert.ToString(jObject["ToDate"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        ewallethistory(userID, FromDate, ToDate, Page, Size);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                else if (methodName == "getreferenceid")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string refId = RandomString(12);
                                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");
                                        string memberId = objConnection.select_data_scalar_string("select loginid from member where mobile='" + userID + "'");
                                        int i = objConnection.insert_data("insert into tblMicroATM_RefID(MsrNo,RefID,Status,BCID)values(" + Convert.ToInt32(Msrno) + ",'" + refId + "','Pending','" + Convert.ToString(jObject["BCID"]) + "')");
                                        if (i > 0)
                                        {
                                            //MATM_Request mATM_Request = new MATM_Request();
                                            //mATM_Request.referenceNumber = refId;
                                            //mATM_Request.subMerchantId = PaysPrint.Prefix + memberId;
                                            //mATM_Request.merchantCode = PaysPrint.Prefix + memberId;

                                            //Response.Write(SuccessArray(mATM_Request.GetJson()));

                                        }
                                        else
                                        {
                                            ErrorMessage("There is some issue. Please try again !");
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }
                                else if (methodName == "loanmember")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null
                                        && jObject["FirmName"] != null && jObject["Name"] != null
                                        && jObject["gstNo"] != null && jObject["Mobile"] != null
                                        && jObject["AMobile"] != null && jObject["dailysale"] != null
                                        && jObject["Income"] != null && jObject["MinAMount"] != null
                                        && jObject["MaxAmount"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string FirmName = Convert.ToString(jObject["FirmName"]).Trim();
                                        string Name = Convert.ToString(jObject["Name"]).Trim();
                                        string gstNo = Convert.ToString(jObject["gstNo"]).Trim();
                                        string Mobile = Convert.ToString(jObject["Mobile"]).Trim();
                                        string AMobile = Convert.ToString(jObject["AMobile"]).Trim();
                                        string dailysale = Convert.ToString(jObject["dailysale"]).Trim();
                                        string Income = Convert.ToString(jObject["Income"]).Trim();
                                        string MinAMount = Convert.ToString(jObject["MinAMount"]).Trim();
                                        string MaxAmount = Convert.ToString(jObject["MaxAmount"]).Trim();
                                        var fuDoc = HttpContext.Current.Request.Files["Doc1"];
                                        DataTable dtMember = objConnection.select_data_dt("select * from Member where mobile='" + userID + "'");
                                        string BankAccount = "";
                                        string InputFilebankSTatement = System.IO.Path.GetExtension(fuDoc.FileName);
                                        {
                                            string gdbankstatement = Guid.NewGuid().ToString() + InputFilebankSTatement;

                                            fuDoc.SaveAs(MapPath("../images/") + gdbankstatement);
                                            BankAccount = gdbankstatement.ToString();
                                        }
                                        Int32 Val = 0;
                                        Val = objConnection.update_data("AddEditLoanMember " + dtMember.Rows[0]["Msrno"].ToString() + ",'" + FirmName + "','" + Name + "','','" + gstNo + "','" + Mobile + "','" + AMobile + "','" + dailysale + "','" + Income + "','" + MinAMount + "','" + MaxAmount + "',0,'Pending','" + BankAccount + "'");
                                        if (Val > 0)
                                        {
                                            SuccessMessage("Your Form Submited Successfully");
                                        }

                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                else if (methodName == "getreferenceidmh")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["BCID"] != null)
                                    {
                                        if (!string.IsNullOrEmpty(jObject["BCID"].ToString().Trim()))
                                        {
                                            string userID = Convert.ToString(jObject["UserID"]).Trim();
                                            string BCID = Convert.ToString(jObject["BCID"]).Trim();
                                            string refId = System.Guid.NewGuid().ToString();// RandomString(12);
                                            Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");
                                            {
                                                Int32 val = objConnection.insert_data("AddEditMATMRequestID '" + refId + "','" + BCID + "','" + Msrno + "','Pending'");
                                                if (val > 0)
                                                {
                                                    string response = "{\"statuscode\":\"TXN\",\"status\":\"Transaction Successful\",\"data\":\"" + refId + "\"}";
                                                    Response.Write(response);
                                                }
                                                else
                                                {
                                                    ErrorMessage("There is some issue. Please try again !");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ErrorMessage("You have not permission for AMTM");
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }
                                else if (methodName == "checkstatusmh")
                                {
                                    if (jObject["RefId"] != null && jObject["TxnType"] != null)
                                    {
                                        CheckStatusmh(jObject["RefId"].ToString(), jObject["TxnType"].ToString());
                                    }
                                }
                                else if (methodName == "fundtransfer")
                                {
                                    if (jObject["UserID"] != null && jObject["ToUserID"] != null && jObject["Amount"] != null)
                                    {
                                        string UserID = Convert.ToString(jObject["UserID"]).Trim();
                                        string ToUserID = Convert.ToString(jObject["ToUserID"]).Trim();
                                        string Amount = Convert.ToString(jObject["Amount"]).Trim();
                                        decimal MyBalance = 0;
                                        DataTable dtWalletBalance = new DataTable();
                                        DataTable dtMemberP = objConnection.select_data_dt("select * from member where mobile='" + UserID + "'");
                                        DataTable dtMember = objConnection.select_data_dt("select * from member where mobile='" + ToUserID + "' or loginid='" + ToUserID + "'");
                                        if (dtMember.Rows.Count > 0)
                                        {
                                            dtWalletBalance = objConnection.select_data_dt("Exec [ProcMLM_ManageEWalletBalance] 'GetBalanceByMsrNo', " + dtMemberP.Rows[0]["msrno"].ToString());
                                            if (dtWalletBalance.Rows.Count > 0)
                                            {
                                                MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["Balance"]);

                                            }
                                            if (!string.IsNullOrEmpty(Amount) && Convert.ToInt32(Amount) > 0)
                                            {
                                                if (MyBalance > Convert.ToInt32(Amount))
                                                {
                                                    DataTable dtWall = new DataTable(); string nnn = System.Guid.NewGuid().ToString();

                                                    dtWall = objConnection.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMemberP.Rows[0]["LoginID"].ToString() + "','" + Amount + "','dr','Fund Transfer-" + ToUserID + "-" + nnn + "'");
                                                    if (dtWall.Rows[0]["MSRNO"].ToString() == "1")
                                                    {
                                                        int val = objConnection.insert_data("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["LoginID"].ToString() + "','" + Amount + "','CR','" + nnn + "'");
                                                        if (val > 0)
                                                        {
                                                            SuccessMessage("Balance Added Successfully ..!!");
                                                        }
                                                        else
                                                        {
                                                            ErrorMessage("Some Error Occurred , Try Again ..!!");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        ErrorMessage("Low Balance");
                                                    }
                                                }
                                                else
                                                {
                                                    ErrorMessage("Low Balance");
                                                }
                                            }
                                            else
                                            {
                                                ErrorMessage("Amount must be greater then Zero ..");
                                            }
                                        }
                                        else
                                        {
                                            ErrorMessage("User Not Found");

                                        }
                                        return;
                                    }
                                }
                                else if (methodName == "activeprofile")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Amount = objConnection.select_data_scalar_string("select profileamount from company where isactive=1");
                                        DataTable dtMember = objConnection.select_data_dt("select * from Member where mobile='" + userID + "' and ActiveProfile=0");
                                        if (dtMember.Rows.Count > 0)
                                        {
                                            if (Convert.ToDecimal(Amount) > 0)
                                            {
                                                decimal MyBalance = 0;
                                                DataTable dtWalletBalance = new DataTable();
                                                dtWalletBalance = objConnection.select_data_dt("Exec [ProcMLM_ManageEWalletBalance] 'GetBalanceByMsrNo', " + Convert.ToInt32(dtMember.Rows[0]["Msrno"]));
                                                if (dtWalletBalance.Rows.Count > 0)
                                                {
                                                    MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["Balance"]);

                                                }
                                                if (MyBalance >= Convert.ToDecimal(Amount))
                                                {
                                                    string reference_number = System.Guid.NewGuid().ToString().Replace('-', '0');

                                                    string Narration = "Profile Actived TransID-" + reference_number;
                                                    DataTable dtPayment = objConnection.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"].ToString() + "','" + Amount + "','DR','" + Narration + "'");
                                                    if (dtPayment.Rows[0]["msrno"].ToString() == "1")
                                                    {
                                                        objConnection.update_data("update Member set ActiveProfile=1 where mobile='" + userID + "'");
                                                        SuccessMessage("Service Active Successfully");
                                                    }
                                                }
                                                else
                                                {
                                                    {
                                                        ErrorMessage("Low Balance");
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                objConnection.update_data("update Member set ActiveProfile=1 where mobile='" + userID + "'");
                                                SuccessMessage("Service Active Successfully");
                                            }
                                        }
                                        else
                                        {
                                            ErrorMessage("Already Active");
                                        }

                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }
                                }
                                else if (methodName == "addcomplain")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["Message"] != null && jObject["ServiceID"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Message = Convert.ToString(jObject["Message"]).Trim();
                                        string ServiceID = Convert.ToString(jObject["ServiceID"]).Trim();
                                        Addcomplain(userID, Message, ServiceID);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }
                                else if (methodName == "servicelist")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Message = Convert.ToString(jObject["Message"]).Trim();
                                        DataTable dt = new DataTable();
                                        dt = objConnection.select_data_dt("FillDropDown 'ActiveService'");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));

                                        }
                                        else
                                            ErrorMessage("Service not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }
                                else if (methodName == "getofflineformlist")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Message = Convert.ToString(jObject["Message"]).Trim();
                                        DataTable dt = new DataTable();
                                        dt = objConnection.select_data_dt("select * from OffLineservice where IsActive=1");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));

                                        }
                                        else
                                            ErrorMessage("Service not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }
                                }
                                else if (methodName == "getofflineformdata")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["FormID"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Message = Convert.ToString(jObject["Message"]).Trim();
                                        string FormID = Convert.ToString(jObject["FormID"]).Trim();
                                        DataTable dt = new DataTable();
                                        dt = objConnection.select_data_dt("ManageOffLineserviceField 'GetByFormID'," + FormID + "");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));

                                        }
                                        else
                                            ErrorMessage("Service not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }
                                else if (methodName == "downlist")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Message = Convert.ToString(jObject["Message"]).Trim();
                                        DataTable dt = new DataTable();
                                        dt = objConnection.select_data_dt("select * from DocumentDownLoad where IsActive=1");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));

                                        }
                                        else
                                            ErrorMessage("Service not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }
                                else if (methodName == "getdownrole")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Message = Convert.ToString(jObject["Message"]).Trim();
                                        DataTable dtmem = objConnection.select_data_dt("select * from Member where mobile='" + userID + "'");
                                        DataTable dt = objConnection.select_data_dt("select * from role where ID in(select ToRoleID from RoleRegisterPermission where RoleID=" + dtmem.Rows[0]["RoleiD"] + ")");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));

                                        }
                                        else
                                            ErrorMessage("Service not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }

                                else if (methodName == "tokenpurchase")
                                {
                                    //if (jObject["UserID"] != null && jObject["Password"] != null && jObject["TokenType"] != null && jObject["NoToken"] != null)
                                    //{
                                    //    string userID = Convert.ToString(jObject["UserID"]).Trim();
                                    //    string password = Convert.ToString(jObject["Password"]).Trim();
                                    //    Int32 TokenType = Convert.ToInt32(jObject["TokenType"]);
                                    //    string txtnumofCoupons = Convert.ToString(jObject["NoToken"]).Trim();
                                    //    DataTable dtMemberMaster = objConnection.select_data_dt("select * from Member where mobile='" + userID + "'");

                                    //    string hddGUID = System.Guid.NewGuid().ToString();
                                    //    if (TokenType == -1)
                                    //    {
                                    //        ErrorMessage("Please select Coupon Type.");
                                    //        return;
                                    //    }
                                    //    else
                                    //    {

                                    //        int MsrNo = Convert.ToInt32(dtMemberMaster.Rows[0]["MsrNo"]);
                                    //        string MemberId = dtMemberMaster.Rows[0]["Loginid"].ToString();
                                    //        string number = "P" + System.Guid.NewGuid().ToString().Replace("-", "7");
                                    //        string narration = "PAN Card Fees, Transaction Id:" + number;
                                    //        string CommNarration = "PAN Card Commission, Transaction Id:" + number;
                                    //        string RefundNarration = "PAN Card Amount Refunded, Transaction Id:" + number;
                                    //        int DigitalCoupon = 0;
                                    //        decimal MemberBalance = 0;
                                    //        int PhysicalCoupon = 0, PANAmount = 0;
                                    //        Int32 PANDigitalFees = 0;
                                    //        DataTable dtPANComission;
                                    //        Int32 PANPhysicalFees = 0;
                                    //        string reqId = dtMemberMaster.Rows[0]["RequestId"].ToString();
                                    //        decimal refundAmount = 0;
                                    //        int IsFlat = 1;
                                    //        decimal surcharge = 0;
                                    //        decimal my_Surcharge = 0;
                                    //        if (TokenType == 1)
                                    //        {
                                    //            DigitalCoupon = Convert.ToInt32(txtnumofCoupons);
                                    //            PhysicalCoupon = 0;
                                    //            dtPANComission = objConnection.select_data_dt("select PANType,PANFees,IsFlat,surcharge from tblPAN_Surcharge where PackageId=" + dtMemberMaster.Rows[0]["PackageID"].ToString() + " and PANType=" + Convert.ToInt32(TokenType) + "");
                                    //            if (dtPANComission.Rows.Count > 0)
                                    //            {
                                    //                PANDigitalFees = Convert.ToInt32(dtPANComission.Rows[0]["PANFees"]);
                                    //                IsFlat = Convert.ToInt32(dtPANComission.Rows[0]["IsFlat"]);
                                    //                surcharge = Convert.ToDecimal(dtPANComission.Rows[0]["surcharge"]);
                                    //            }
                                    //            else
                                    //            {
                                    //                PANDigitalFees = 100;
                                    //                IsFlat = 1;
                                    //                surcharge = 10;
                                    //            }

                                    //            refundAmount = PANDigitalFees;
                                    //            PANAmount = PANDigitalFees;


                                    //        }
                                    //        else if (TokenType == 0)
                                    //        {
                                    //            PhysicalCoupon = Convert.ToInt32(txtnumofCoupons);
                                    //            DigitalCoupon = 0;
                                    //            dtPANComission = objConnection.select_data_dt("select PANType,PANFees,IsFlat,surcharge from tblPAN_Surcharge where PackageId=" + dtMemberMaster.Rows[0]["PackageID"].ToString() + " and PANType=" + Convert.ToInt32(TokenType) + "");
                                    //            if (dtPANComission.Rows.Count > 0)
                                    //            {
                                    //                PANPhysicalFees = Convert.ToInt32(dtPANComission.Rows[0]["PANFees"]);
                                    //                IsFlat = Convert.ToInt32(dtPANComission.Rows[0]["IsFlat"]);
                                    //                surcharge = Convert.ToDecimal(dtPANComission.Rows[0]["surcharge"]);
                                    //            }
                                    //            else
                                    //            {
                                    //                IsFlat = 1;
                                    //                surcharge = 10;
                                    //            }
                                    //            refundAmount = PANPhysicalFees;
                                    //            PANAmount = PANPhysicalFees;

                                    //        }

                                    //        if (IsFlat == 0)
                                    //        {
                                    //            my_Surcharge = PANAmount * (surcharge / 100);
                                    //        }
                                    //        else
                                    //        {
                                    //            my_Surcharge = surcharge;
                                    //        }

                                    //        decimal TotalAmount = Convert.ToDecimal(txtnumofCoupons) * PANAmount;
                                    //        DataTable dtBal = objConnection.select_data_dt("select Balance from TBL_EWALLETBALANCE  where Msrno=" + MsrNo + "");
                                    //        if (dtBal.Rows.Count > 0)
                                    //        {
                                    //            MemberBalance = Convert.ToDecimal(dtBal.Rows[0]["Balance"]);
                                    //        }
                                    //        decimal retailerBalance = MemberBalance;

                                    //        try
                                    //        {
                                    //            if (retailerBalance > 0)
                                    //            {
                                    //                if (retailerBalance >= (TotalAmount))
                                    //                {

                                    //                    clsMahagram ObjMahagram = new clsMahagram();

                                    //                    DataTable dt = objConnection.select_data_dt("exec [PROC_EWALLETTRANSACTION] '" + Convert.ToString(dtMemberMaster.Rows[0]["LoginID"]) + "','" + (TotalAmount) + "','Dr','" + narration + "','" + number + "'");
                                    //                    if (dt.Rows[0]["MSRNO"].ToString().ToUpper() == "1")
                                    //                    {
                                    //                        int CountDr = objConnection.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where msrno=" + dtMemberMaster.Rows[0]["Msrno"].ToString() + " and narration like '%" + number + "%' and factor='dr'");
                                    //                        if (CountDr == 1)
                                    //                        {
                                    //                            using (var client = new WebClient())
                                    //                            {
                                    //                                var Values = new NameValueCollection();
                                    //                                Values["securityKey"] = clsMahagram.Secret_KeyPan;
                                    //                                Values["createdby"] = clsMahagram.MMID;
                                    //                                Values["psaid"] = dtMemberMaster.Rows[0]["PsaIdPan"].ToString();
                                    //                                Values["transactionid"] = number;
                                    //                                Values["transactiondate"] = DateTime.Today.ToString("yyyy-MM-dd'T'hh:MM:SS");
                                    //                                Values["totalcoupon_digital"] = DigitalCoupon.ToString();
                                    //                                Values["totalcoupon_physical"] = PhysicalCoupon.ToString();
                                    //                                Values["udf1"] = "test1";
                                    //                                Values["udf2"] = "test2";
                                    //                                Values["udf3"] = "test3";
                                    //                                Values["udf4"] = "test4";//2021-04-06T01:57:38.637
                                    //                                Values["udf5"] = "test5";

                                    //                                var responseString = ObjMahagram.CouponRequest(Values);

                                    //                                JArray ObjData = JArray.Parse(responseString);
                                    //                                if (ObjData[0]["StatusCode"].ToString().ToUpper() == "000")
                                    //                                {
                                    //                                    objConnection.select_data_dt("exec [PROC_EWALLETTRANSACTION] '" + Convert.ToString(dtMemberMaster.Rows[0]["LoginID"]) + "','" + (my_Surcharge) + "','Cr','" + CommNarration + "','" + number + "'");
                                    //                                    objConnection.update_data("insert into PAN_Mahagram values (" + MsrNo + ",'" + MemberId + "','" + dtMemberMaster.Rows[0]["PsaIdPan"].ToString() + "','" + ObjData[0]["RequestId"].ToString() + "','" + PhysicalCoupon.ToString() + "','" + DigitalCoupon.ToString() + "','" + ObjData[0]["coupontopupdate"].ToString() + "','" + ObjData[0]["Merchant Id"].ToString() + "','" + ObjData[0]["Reference Id"].ToString() + "','" + ObjData[0]["status"].ToString() + "','" + dtMemberMaster.Rows[0]["PsaIdPan"].ToString() + "','" + responseString + "')");
                                    //                                    SuccessMessage("Coupon Request generated Successfully.Your Request ID is : " + ObjData[0]["RequestId"].ToString() + "");

                                    //                                }
                                    //                                else if (ObjData[0]["StatusCode"].ToString().ToUpper() != "000")
                                    //                                {
                                    //                                    objConnection.select_data_dt("Exec PROC_EWALLETTRANSACTION '" + Convert.ToString(dtMemberMaster.Rows[0]["LoginID"]) + "'," + refundAmount + ", 'Cr', '" + RefundNarration + "','" + number + "'");
                                    //                                    ErrorMessage(ObjData[0]["Message"].ToString());
                                    //                                }

                                    //                            }
                                    //                        }
                                    //                        else
                                    //                        {
                                    //                            ErrorMessage("Low Balance");
                                    //                        }
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        ErrorMessage("Low Balance");

                                    //                    }

                                    //                }
                                    //                else
                                    //                {
                                    //                    ErrorMessage("Low Balance");
                                    //                }
                                    //            }
                                    //            else
                                    //            {
                                    //                ErrorMessage("Low Balance");

                                    //            }
                                    //        }
                                    //        catch (Exception ex)
                                    //        {


                                    //        }
                                    //    }
                                    //}
                                    //else
                                    //{
                                    //    ErrorMessage("Insufficient Parameter");
                                    //}
                                }
                                else if (methodName == "offlineservicelink")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        DataTable dt = new DataTable();
                                        dt = objConnection.select_data_dt("select * from OffLineServiceLink where IsActive=1");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));

                                        }
                                        else
                                            ErrorMessage("Service not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }
                                else if (methodName == "getnews")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Message = Convert.ToString(jObject["Message"]).Trim();
                                        DataTable dt = new DataTable();
                                        string Msrno = "1";
                                        if (CompanyName != "")
                                        {
                                            Msrno = objConnection.select_data_scalar_string("select MemberID from company where isactive=1 and WebsiteURL='" + CompanyName + "'");
                                        }
                                        dt = objConnection.select_data_dt("EXEC ManageNews 'GetActive','" + 0 + "'," + Msrno + "");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));

                                        }
                                        else
                                            ErrorMessage("News not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }
                                else if (methodName == "ticketlist")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Message = Convert.ToString(jObject["Message"]).Trim();
                                        DataTable dt = new DataTable();
                                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

                                        dt = objConnection.select_data_dt("Exec ManageComplainTicket @Action='GetByMsrno',@ID=" + Msrno + "");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));

                                        }
                                        else
                                            ErrorMessage("Tips not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }
                                else if (methodName == "holiday")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Message = Convert.ToString(jObject["Message"]).Trim();
                                        DataTable dt = new DataTable();
                                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

                                        dt = objConnection.select_data_dt("select ID,Name,Convert(nvarchar(30),HolidayDate,106) HolidayDate,Convert(nvarchar(30),AddDate,106)AddDate from Officedays");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));

                                        }
                                        else
                                            ErrorMessage("Tips not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficiant parameters !");
                                    }

                                }

                                else if (methodName == "roffer")
                                {
                                    if (jObject["OperatorID"] != null && jObject["Number"] != null)
                                    {
                                        string OperatorID = Convert.ToString(jObject["OperatorID"]).Trim();
                                        string Number = Convert.ToString(jObject["Number"]).Trim();
                                        string optID = objConnection.select_data_scalar_string("select ID from Operator where OperatorCode='" + OperatorID + "'");

                                        string opt = objConnection.select_data_scalar_string("select OperatorCode from tblRecharge_OperatorCode where OperatorID='" + optID + "' and APIID=3");
                                        string Result = CallPlanAPI("https://sonitechno.in/api/PlanAPI.aspx?MethodName=Roffer&opt=" + opt + "&Number=" + Number + ""); //MPlan.Roffer(opt, Number);
                                        Response.Write(Result);
                                        return;
                                    }
                                }

                                else if (methodName == "dthcustomerinfo")
                                {
                                    if (jObject["OperatorID"] != null && jObject["Number"] != null)
                                    {
                                        string OperatorID = Convert.ToString(jObject["OperatorID"]).Trim();
                                        string Number = Convert.ToString(jObject["Number"]).Trim();
                                        string optID = objConnection.select_data_scalar_string("select ID from Operator where OperatorCode='" + OperatorID + "'");

                                        string opt = objConnection.select_data_scalar_string("select OperatorCode from tblRecharge_OperatorCode where OperatorID=" + optID + " and APIID=3");
                                        if (OperatorID == "TS")
                                        {
                                            opt = "TataSky";
                                        }
                                        else if (OperatorID == "DS")
                                        {
                                            opt = "Dishtv";
                                        }
                                        else if (OperatorID == "AD")
                                        {
                                            opt = "Airteldth";
                                        }
                                        string Result = CallPlanAPI("https://sonitechno.in/api/PlanAPI.aspx?MethodName=dthcustomerinfo&opt=" + opt + "&Number=" + Number + ""); // MPlan.DTHDetails(opt, Number);
                                        Response.Write(Result);
                                        return;
                                    }
                                }

                                else if (methodName == "dthcustomerplan")
                                {
                                    if (jObject["OperatorID"] != null)
                                    {
                                        string OperatorID = Convert.ToString(jObject["OperatorID"]).Trim();
                                        string optID = objConnection.select_data_scalar_string("select ID from Operator where OperatorCode='" + OperatorID + "'");

                                        string opt = objConnection.select_data_scalar_string("select OperatorCode from tblRecharge_OperatorCode where OperatorID=" + optID + " and APIID=3");
                                        string Result = CallPlanAPI("https://sonitechno.in/api/PlanAPI.aspx?MethodName=dthcustomerplan&opt=" + opt + "");// MPlan.DTHPlan(opt);
                                        Response.Write(Result);
                                        return;
                                    }
                                }

                                else if (methodName == "plan")
                                {
                                    if (jObject["OperatorID"] != null)
                                    {
                                        string OperatorID = Convert.ToString(jObject["OperatorID"]).Trim();
                                        string optID = objConnection.select_data_scalar_string("select ID from Operator where OperatorCode='" + OperatorID + "'");

                                        string opt = objConnection.select_data_scalar_string("select OperatorCode from tblRecharge_OperatorCode where OperatorID=" + optID + " and APIID=3");
                                        string Result = CallPlanAPI("https://sonitechno.in/api/PlanAPI.aspx?MethodName=plan&opt=" + opt + "");// MPlan.Plan(opt);
                                        Response.Write(Result);
                                        return;
                                    }
                                }

                                //else if (methodName == "elebill")
                                //{
                                //    if (jObject["OperatorID"] != null && jObject["Number"] != null)
                                //    {
                                //        string OperatorID = Convert.ToString(jObject["OperatorID"]).Trim();
                                //        string Number = Convert.ToString(jObject["Number"]).Trim();
                                //        string optID = objConnection.select_data_scalar_string("select ID from Operator where OperatorCode='" + OperatorID + "'");

                                //        string opt = objConnection.select_data_scalar_string("select OperatorCode from tblRecharge_OperatorCode where OperatorID=" + optID + " and APIID=3");
                                //        string Result = MPlan.Bill(opt, Number);
                                //        Response.Write(Result);
                                //        return;
                                //    }
                                //}
                                else if (methodName == "wlebill")
                                {
                                    if (jObject["OperatorID"] != null && jObject["Number"] != null)
                                    {
                                        string OperatorID = Convert.ToString(jObject["OperatorID"]).Trim();
                                        string Number = Convert.ToString(jObject["Number"]).Trim();
                                        string optID = objConnection.select_data_scalar_string("select ID from Operator where OperatorCode='" + OperatorID + "'");

                                        string opt = objConnection.select_data_scalar_string("select OperatorCode from tblRecharge_OperatorCode where OperatorID=" + optID + " and APIID=3");
                                        string Result = MPlan.WaterBill(opt, Number);
                                        Response.Write(Result);
                                        return;
                                    }
                                }
                                else if (methodName == "gettoken")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["Token"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string Token = Convert.ToString(jObject["Token"]).Trim();
                                        GetToken(userID, password, Token);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }


                                #region [ 35.ewallethistory ]


                                else if (methodName == "aepswallethistory")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["FromDate"] != null && jObject["ToDate"] != null && jObject["Page"] != null && jObject["Size"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string FromDate = Convert.ToString(jObject["FromDate"]).Trim();
                                        string ToDate = Convert.ToString(jObject["ToDate"]).Trim();
                                        string Page = Convert.ToString(jObject["Page"]).Trim();
                                        string Size = Convert.ToString(jObject["Size"]).Trim();
                                        aepswallethistory(userID, FromDate, ToDate, Page, Size);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                #endregion

                                else if (methodName == "getbanner")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        DataTable dt = new DataTable();
                                        string Msrno = "1";
                                        if (CompanyName != "")
                                        {
                                            Msrno = objConnection.select_data_scalar_string("select MemberID from company where isactive=1 and WebsiteURL='" + CompanyName + "'");
                                        }
                                        dt = objConnection.select_data_dt("EXEC ManageBannerImage 'GetAllBYBannerType','1','" + Msrno + "'");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                else if (methodName == "getnotification")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();

                                        dt = objConnection.select_data_dt("select * from Notification order by id desc");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }


                                else if (methodName == "commissionlistservicename")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Message = Convert.ToString(jObject["Message"]).Trim();
                                        DataTable dt = new DataTable();
                                        dt = objConnection.select_data_dt("select Name from CommissionListServiceName");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                        else
                                            ErrorMessage("Service not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                else if (methodName == "commissionaccordingservicename")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["commissionaccordingservicename"] != null)
                                    {
                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string Message = Convert.ToString(jObject["Message"]).Trim();
                                        string commissionaccordingservicename = Convert.ToString(jObject["commissionaccordingservicename"]).Trim();
                                        DataTable dt = new DataTable();
                                        dt = objConnection.select_data_dt("CommissionList '" + userID + "','" + commissionaccordingservicename + "'");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                        else
                                            ErrorMessage("Service not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                else if (methodName == "scan")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["Token"] != null && jObject["ScanValue"] != null)
                                    {
                                        string Token = Convert.ToString(jObject["Token"]).Trim();
                                        string ScanValue = Convert.ToString(jObject["ScanValue"]).Trim();
                                        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where AppToken='" + Token + "'");
                                        objConnection.update_data("update WebTokenLogin set Status='Success',Msrno=" + Msrno + " where Token='" + ScanValue + "'");

                                    }
                                }
                                else if (methodName == "sendpayrequest")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["UPIID"] != null && jObject["Amount"] != null)
                                    {

                                        string userID = Convert.ToString(jObject["UserID"]).Trim();
                                        string password = Convert.ToString(jObject["Password"]).Trim();
                                        string UPIID = Convert.ToString(jObject["UPIID"]).Trim();
                                        string Amount = Convert.ToString(jObject["Amount"]).Trim();
                                        sendpayrequest(userID, UPIID, Amount);
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                else if (methodName == "bankdown")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        string BankResult = BindDropDown.BankDownList();

                                        SuccessMessage(BankResult);

                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                else if (methodName == "elebill")
                                {
                                    if (jObject["OperatorID"] != null)
                                    {

                                        string OperatorID = Convert.ToString(jObject["OperatorID"]).Trim();
                                        string Number = Convert.ToString(jObject["Number"]).Trim();
                                        PostGetCallMSWIFT postGetCallMSWIFT = new PostGetCallMSWIFT();
                                        BillFetchRequest billFetchRequest = new BillFetchRequest();
                                        billFetchRequest.TelecomCircleID = "0";
                                        billFetchRequest.ClientRefId = "0";
                                        billFetchRequest.SPKey = OperatorID;
                                        billFetchRequest.APIID = SONITechnoCredentrial.APIID;
                                        billFetchRequest.Token = SONITechnoCredentrial.Token;
                                        billFetchRequest.MethodName = "billfetch";
                                        billFetchRequest.Number = (jObject["Optional1"] != null ? jObject["Optional1"].ToString() : "");

                                        billFetchRequest.Optional1 = (jObject["Optional2"] != null ? jObject["Optional2"].ToString() : "");
                                        billFetchRequest.Optional2 = (jObject["Optional3"] != null ? jObject["Optional3"].ToString() : "");
                                        billFetchRequest.Optional3 = (jObject["Optional4"] != null ? jObject["Optional4"].ToString() : "");
                                        billFetchRequest.Optional4 = (jObject["Optional5"] != null ? jObject["Optional5"].ToString() : "");
                                        billFetchRequest.Optional5 = (jObject["Optional6"] != null ? jObject["Optional6"].ToString() : "");
                                        billFetchRequest.Optional6 = (jObject["Optional7"] != null ? jObject["Optional7"].ToString() : "");
                                        billFetchRequest.Optional7 = (jObject["Optional8"] != null ? jObject["Optional8"].ToString() : "");
                                        billFetchRequest.Optional8 = (jObject["Optional9"] != null ? jObject["Optional9"].ToString() : "");
                                        billFetchRequest.Optional9 = (jObject["Optional10"] != null ? jObject["Optional10"].ToString() : "");

                                        string Result = postGetCallMSWIFT.PostCall(billFetchRequest.GetJson());
                                        JObject Data = JObject.Parse(Result);
                                        if (Data["code"].ToString().ToUpper() == "TXN")
                                        {
                                            Response.Write(Data["data"].ToString());
                                        }
                                        else
                                        {
                                            ErrorMessage(Data["mess"].ToString());


                                        }
                                        //BillFetch(Number, Number, OperatorID);
                                        //Response.Write(Result);
                                        return;
                                    }
                                }
                                else if (methodName == "paybill")
                                {
                                    if (jObject["OperatorID"] != null)
                                    {

                                        string OperatorID = Convert.ToString(jObject["OperatorID"]).Trim();
                                        string Amount = Convert.ToString(jObject["Amount"]).Trim();
                                        DataTable dtOpHis = objConnection.select_data_dt("select * from tblRecharge_OperatorCode where APIID=4 and OperatorCode='" + OperatorID + "'");
                                        if (dtOpHis.Rows.Count > 0)
                                        {
                                            var date = DateTime.Now;
                                            DataTable dtOp = objConnection.select_data_dt("ManageOperator 'GetDetaisID',0,'" + OperatorID + "'");
                                            if (date.Hour >= Convert.ToInt32(dtOp.Rows[0]["OnTime"].ToString()) && date.Hour <= Convert.ToInt32(dtOp.Rows[0]["OffTime"].ToString()))
                                            {
                                                string userID = Convert.ToString(jObject["UserID"]).Trim();
                                                DataTable dtMember = objConnection.select_data_dt("select * from member where mobile='" + userID + "'");

                                                double Balance = objConnection.select_data_scalar_double("select balance from TBL_EWALLETBALANCE where msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");
                                                if (Balance > (Convert.ToDouble(Amount) + Convert.ToDouble(dtMember.Rows[0]["HoldAmt"].ToString())))
                                                {
                                                    PostGetCallMSWIFT postGetCallMSWIFT = new PostGetCallMSWIFT();
                                                    BillPayRequest billFetchRequest = new BillPayRequest();
                                                    billFetchRequest.TelecomCircleID = "0";
                                                    billFetchRequest.APIID = SONITechnoCredentrial.APIID;
                                                    billFetchRequest.Token = SONITechnoCredentrial.Token;
                                                    billFetchRequest.MethodName = "billpay";
                                                    billFetchRequest.SPKey = OperatorID;

                                                    billFetchRequest.Number = (jObject["Optional1"] != null ? jObject["Optional1"].ToString() : "");

                                                    billFetchRequest.Optional1 = (jObject["Optional2"] != null ? jObject["Optional2"].ToString() : "");
                                                    billFetchRequest.Optional2 = (jObject["Optional3"] != null ? jObject["Optional3"].ToString() : "");
                                                    billFetchRequest.Optional3 = (jObject["Optional4"] != null ? jObject["Optional4"].ToString() : "");
                                                    billFetchRequest.Optional4 = (jObject["Optional5"] != null ? jObject["Optional5"].ToString() : "");
                                                    billFetchRequest.Optional5 = (jObject["Optional6"] != null ? jObject["Optional6"].ToString() : "");
                                                    billFetchRequest.Optional6 = (jObject["Optional7"] != null ? jObject["Optional7"].ToString() : "");
                                                    billFetchRequest.Optional7 = (jObject["Optional8"] != null ? jObject["Optional8"].ToString() : "");
                                                    billFetchRequest.Optional8 = (jObject["Optional9"] != null ? jObject["Optional9"].ToString() : "");
                                                    billFetchRequest.Optional9 = (jObject["Optional10"] != null ? jObject["Optional10"].ToString() : "");
                                                    billFetchRequest.Amount = jObject["Amount"].ToString();
                                                    DataTable DtUrl;
                                                    if (dtOp.Rows[0]["IsPending"].ToString().ToBoolen())
                                                    {
                                                        DtUrl = objConnection.select_data_dt("BBpsRecharge '" + billFetchRequest.Number + "','" + dtMember.Rows[0]["msrno"].ToString() + "','" + Amount + "','" + dtOpHis.Rows[0]["OperatorID"].ToString() + "','" + billFetchRequest.GetJson() + "'," + ConstantsData.OfflineAPIID + ",'" + ConstantsData.getipAddress() + "','App','',1");
                                                    }
                                                    else
                                                    {
                                                        DtUrl = objConnection.select_data_dt("BBpsRecharge '" + billFetchRequest.Number + "','" + dtMember.Rows[0]["msrno"].ToString() + "','" + Amount + "','" + dtOpHis.Rows[0]["OperatorID"].ToString() + "','" + billFetchRequest.GetJson() + "'," + ConstantsData.OfflineAPIID + ",'" + ConstantsData.getipAddress() + "','App','',0");
                                                    }
                                                    if (DtUrl.Rows[0]["Yes"].ToString() == "Yes")
                                                    {
                                                        billFetchRequest.ClientRefId = DtUrl.Rows[0]["TransID"].ToString();// "TransID";
                                                        if (dtOp.Rows[0]["IsPending"].ToString().ToBoolen())
                                                        {
                                                            DataTable i = objConnection.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + "," + dtMember.Rows[0]["msrno"].ToString() + ", 0, 0, 0, 0, '', '', 'Success','" + DtUrl.Rows[0]["TransID"].ToString() + "', '','" + DtUrl.Rows[0]["TransID"].ToString() + "'");
                                                            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                                                            var columns = new Dictionary<string, object>
                                                             {
                                                                 { "Status", 0},
                                                                 { "Message", "Recharge Successful"},
                                                                 { "Number",  billFetchRequest.Number},
                                                                 { "Amount", Amount},
                                                                 { "RechargeStatus", "Success"},
                                                                 { "TransactionID", DtUrl.Rows[0]["TransID"].ToString()},
                                                                 { "DateTime", DateTime.Now.ToString()}
                                                             };
                                                            Response.Write(SuccessArray(Convert.ToString(jsSerializer.Serialize(columns))));
                                                        }
                                                        else
                                                        {
                                                            string Result = postGetCallMSWIFT.PostCallPay(billFetchRequest.GetJson());
                                                            JObject Data = JObject.Parse(Result);
                                                            if (Data["code"].ToString().ToUpper() == "TXN")
                                                            {
                                                                if (Data["data"]["status"].ToString().ToUpper() == "SUCCESS")
                                                                {
                                                                    DataTable i = objConnection.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + "," + dtMember.Rows[0]["msrno"].ToString() + ", 0, 0, 0, 0, '', '', 'Success','" + Data["data"]["TransactionID"].ToString() + "', '','" + Data["data"]["OperatorTransactionId"].ToString() + "'");
                                                                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                                                                    var columns = new Dictionary<string, object>
                                                             {
                                                                 { "Status", 0},
                                                                 { "Message", "Recharge Successful"},
                                                                 { "Number",  billFetchRequest.Number},
                                                                 { "Amount", Amount},
                                                                 { "RechargeStatus", "Success"},
                                                                 { "TransactionID",  Data["data"]["TransactionID"].ToString()},
                                                                 { "DateTime", DateTime.Now.ToString()}
                                                             };
                                                                    Response.Write(SuccessArray(Convert.ToString(jsSerializer.Serialize(columns))));
                                                                }

                                                                else if (Data["data"]["status"].ToString().ToUpper() == "PENDING")
                                                                {
                                                                    DataTable i = objConnection.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + ", 0, 0, 0, 0, 0, '', '','Pending','" + Data["data"]["TransactionID"].ToString() + "','',''");
                                                                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                                                                    var columns = new Dictionary<string, string>
                                                                {
                                                                    { "Status", "0"},
                                                                    { "Message", "Request Submited"},
                                                                    { "Number", billFetchRequest.Number},
                                                                    { "Amount", Amount},
                                                                    { "RechargeStatus", "Pending"},
                                                                    { "TransactionID",  Data["data"]["TransactionID"].ToString()},
                                                                    { "DateTime", DateTime.Now.ToString()}
                                                                };
                                                                    Response.Write(SuccessArray(Convert.ToString(jsSerializer.Serialize(columns))));
                                                                }
                                                                else
                                                                {
                                                                    try
                                                                    {
                                                                        DataTable i = objConnection.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + DtUrl.Rows[0]["Historyid"].ToString() + ", 0, 0, 0, 0, 0, '','', 'Failed', '','" + Data["mess"].ToString() + "',''");
                                                                    }
                                                                    catch (Exception ex) { }
                                                                    DataTable dtNarration = objConnection.select_data_dt("select * from TBL_EWALLETTRANSACTION where TransactionID='" + DtUrl.Rows[0]["TransID"].ToString() + "' and Factor='DR'");
                                                                    if (dtNarration.Rows.Count > 0)
                                                                    {
                                                                        objConnection.update_data("PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["LoginID"].ToString() + "'," + Amount + ",'CR','" + ("Refund " + dtNarration.Rows[0]["Narration"].ToString()) + "','Refund-" + dtNarration.Rows[0]["Description"].ToString() + "'," + dtNarration.Rows[0]["ServiceID"].ToString() + ",'" + DtUrl.Rows[0]["TransID"].ToString() + "'");
                                                                    }
                                                                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                                                                    var columns = new Dictionary<string, string>
                                                             {
                                                                 { "Status", "0"},
                                                                 { "Message", "Recharge Failed"},
                                                                 { "Number", billFetchRequest.Number},
                                                                 { "Amount", Amount},
                                                                 { "RechargeStatus", "Failure"},
                                                                 { "TransactionID", Data["data"]["TransactionID"].ToString() },
                                                                 { "DateTime", DateTime.Now.ToString()}
                                                             };
                                                                    Response.Write(SuccessArray(Convert.ToString(jsSerializer.Serialize(columns))));
                                                                    // ErrorMessage(bsBenBindData.ResponseMessage);
                                                                }
                                                            }
                                                        }

                                                    }
                                                    //BillFetch(Number, Number, OperatorID);
                                                    //Response.Write(Result);
                                                    return;
                                                }
                                                else
                                                {
                                                    ErrorMessage("Low balance");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ErrorMessage("Opeartor not support");
                                        }
                                    }
                                }
                                else if (methodName == "bbpsservice")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null)
                                    {
                                        DataTable dt = new DataTable();
                                        dt = objConnection.select_data_dt("FillDropDown 'NewBBPSServiceMobile',0");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                        else
                                            ErrorMessage("Service not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }
                                else if (methodName == "bbpsoperator")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["ServiceName"] != null)
                                    {
                                        string ServiceName = Convert.ToString(jObject["ServiceName"]).Trim();

                                        DataTable dt = new DataTable();
                                        dt = objConnection.select_data_dt("FillDropDown 'NewBBPS','" + ServiceName + "'");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                        else
                                            ErrorMessage("Service not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                                else if (methodName == "requiredfield")
                                {
                                    if (jObject["UserID"] != null && jObject["Password"] != null && jObject["spkey"] != null)
                                    {
                                        string spkey = Convert.ToString(jObject["spkey"]).Trim();

                                        DataTable dt = new DataTable();
                                        dt = objConnection.select_data_dt("select distinct spkey,[index],Labels,FieldMinLen,FieldMaxLen,IsOptional from bbpsdatadown where spkey='" + spkey + "'");
                                        if (dt.Rows.Count > 0)
                                        {
                                            Response.Write(ConvertDataTabletoString(dt));
                                        }
                                        else
                                            ErrorMessage("Service not available");
                                    }
                                    else
                                    {
                                        ErrorMessage("Insufficient Parameter");
                                    }
                                }

                            }
                            else
                            {
                                ErrorMessage("UnAuthorized Person");
                            }
                        }
                        else
                        {
                            ErrorMessage("UnAuthorized Person");
                        }
                    }
                    else
                    {
                        ErrorMessage("Invalid Request");
                    }
                }
                else
                {
                    ErrorMessage("Invalid Request");
                }

            }
            catch (Exception ex)
            {
                ErrorMessage("Invalid Request");
            }
        }
    }

    #region [ Convertor DataTable to Json ]

    public string ConvertDataTabletoString(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return SuccessArray(serializer.Serialize(rows).ToString());
    }

    #endregion
    private string SuccessArray(string json)
    {
        string response = "{\"statuscode\":\"TXN\",\"status\":\"Transaction Successful\",\"data\": " + json + "}";
        return response;
    }

    public string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string refId = new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
        if (objConnection.select_data_scalar_int("select count(*) from tblMicroATM_RefID where RefID='" + refId + "'") == 1)
        {
            refId = RandomString(12);
        }
        return refId;
    }

    private void ErrorMessage(string ErrorMessage)
    {
        string response = "{\"statuscode\":\"ERR\",\"status\":\"" + ErrorMessage + "\"}";
        Response.Write(response);
        return;

    }

    private string CheckStatusmh(string RefID, string Type)
    {
        string RefId = RefID;// Convert.ToString(Request.Form["RefId"]);
        DataTable dtHistory = objConnection.select_data_dt("select * from MATMRequestID  where RefID='" + RefId + "' and Status='Pending'");
        if (dtHistory.Rows.Count > 0)
        {
            if (dtHistory.Rows[0]["Status"].ToString().ToLower() == "pending")
            {
                MATM_RequestMH objRequest = new MATM_RequestMH();
                objRequest.saltkey = clsMahagram.SaltKey;
                objRequest.secretekey = clsMahagram.Secret_Key;
                objRequest.referenceid = RefId;
                string strJson = JsonConvert.SerializeObject(objRequest);
                string result = clsMahagram.Call_API(strJson);
                if (result != "0")
                {
                    JObject obj = JObject.Parse(result);
                    if (Convert.ToString(obj["code"]).ToUpper() == "TXN")
                    {
                        MATMCheckStatusRoot objRoot = JsonConvert.DeserializeObject<MATMCheckStatusRoot>(result);
                        for (int i = 0; i < objRoot.Data.Count; i++)
                        {
                            string Txntype = "";
                            if (Convert.ToDecimal(objRoot.Data[i].amount) > 0) { Txntype = "CW"; } else { Txntype = "BE"; }

                            string BcId = objRoot.Data[i].bcid;
                            string TerminalId = objRoot.Data[i].pid;
                            string MerchantId = objRoot.Data[i].mid;
                            string Amount = objRoot.Data[i].amount;//0 for BE and amount for CW
                            string TxnStatus = objRoot.Data[i].status;
                            string ReferenceId = objRoot.Data[i].stanno;
                            string Pipe = "1";
                            string EndCustMobile = objRoot.Data[i].udf1;
                            string rrn = objRoot.Data[i].rrn;
                            string Timestamp = objRoot.Data[i].createdate;
                            string clientRefID = objRoot.Data[i].clientrefid;
                            string bankMessage = objRoot.Data[i].bankmessage;
                            String strPathAndQuery = HttpContext.Current.Request.Url.PathAndQuery;
                            string uniqueGUID = "";
                            string CPTransactionID = "";
                            string VendorId = "";
                            string statusCode = "000";
                            string Message = "Success";
                            string bankCode = "";
                            string InvoiceNo = "";
                            string msrno = "0";
                            DataTable dtMemberATM = new DataTable();
                            DataTable dthisRef = new DataTable();
                            dthisRef = objConnection.select_data_dt("select * from MATMRequestID where RefID='" + RefId + "' and Status='Pending' and BCID='"+BcId+"'");
                            dtMemberATM = objConnection.select_data_dt("select * from MATMHistory where RefID='" + RefId + "'");
                            if (dtMemberATM.Rows.Count == 0 && dthisRef.Rows.Count>0)
                            {
                                int valid = objConnection.insert_data("AddEditMATMHistory '"+ dthisRef.Rows[0]["Msrno"].ToString()+ "','"+Txntype+"','"+Timestamp+"','"+BcId+"','"+ ReferenceId + "','"+ TerminalId + "','"+Amount+"',0,0,'"+TxnStatus+"','"+rrn+"','"+ bankMessage + "','"+ objRoot.Data[i].cardno+ "','"+ objRoot.Data[i].clientrefid+ "','"+result+"',1");
                                if (TxnStatus.ToUpper() == "SUCCESS")
                                {
                                    //string MemberID = objConnection.select_data_scalar_string("select loginid from Member where msrno=" + msrno + "");
                                    //string Narration = "MATM TransID-" + RefId;
                                    //objConnection.update_data("Exec ProcMLM__EWalletTransaction_AEPS2 '" + MemberID + "', '" + Amount + "', 'Cr', '" + Narration + "', 'WEB', '" + RefId + "'");
                                    //string GUIDNew = System.Guid.NewGuid().ToString().Replace("-", "5");
                                    //objConnection.update_data("Exec ProcMATM_DistributeCommission " + msrno + ", " + Amount + ", " + Amount + ", '" + RefId + "', '" + GUIDNew + "'");
                                }
                                if (valid > 0)
                                {
                                    objConnection.select_data_dt("update MATMRequestID set Status='" + TxnStatus + "' where RefID='" + RefId + "'");
                                }

                            }
                            else
                            {
                              

                            }
                            SuccessArray(TxnStatus);


                        }
                    }
                    else
                    {
                        if (Convert.ToString(obj["code"]).ToUpper() == "TRANSACTION NOT FOUND")
                        {
                            objConnection.select_data_dt("update tblMicroATM_RefID_Maha set Status='Transaction Not Found' where RefID='" + RefId + "'");
                            string response = "{\"statuscode\":\"ERR\",\"status\":\"Transaction Not Found\",\"RefId\":\"" + RefId + "\"}";
                            ErrorMessage("Transaction Not Found");
                        }
                        else
                        {
                            ErrorMessage(Convert.ToString(obj["mess"]));
                        }
                    }
                }
                else
                {

                }
            }
            else
            {
                ErrorMessage("Transaction allready completed !");
            }
        }
        else
        {
            ErrorMessage("Invalid ReferenceId !");
        }
        return "";
    }

    #region Call API

    public string Call_API(string requestJson)
    {
        string URL = "http://uat.dhansewa.com/MICROATM/GetMATMtxnStatus";
        string response = string.Empty;
        try
        {

            JavaScriptSerializer js = new JavaScriptSerializer();
            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] data = encoder.GetBytes(requestJson);
            string jsrequest = js.Serialize(requestJson);
            HttpWebRequest requestObj = (HttpWebRequest)WebRequest.Create(URL);
            requestObj.Method = "POST";
            requestObj.Accept = "application/json";
            requestObj.ContentType = "application/json";
            requestObj.Timeout = 180000;
            requestObj.GetRequestStream().Write(data, 0, data.Length);
            HttpWebResponse webResponse = (HttpWebResponse)requestObj.GetResponse();
            using (StreamReader sr = new StreamReader(webResponse.GetResponseStream()))
            {
                response = sr.ReadToEnd();
            }
            requestJson = URL + "  " + requestJson;
            response = response.Replace("'", "''");
        }
        catch (Exception ex)
        {
            //requestJson = URL + "  " + requestJson;

            response = "0";
        }
        return response;
    }

    #endregion
    public string copyFrom(NameValueCollection a)
    {
        IDictionary<string, string> dict = new Dictionary<string, string>();
        foreach (var k in a.AllKeys)
        {
            dict.Add(k, a[k]);
        }
        string myJsonString = (new JavaScriptSerializer()).Serialize(dict);

        return myJsonString;

    }
    private static void callAPI(string url)
    {
        try
        {
            WebClient wc = new WebClient();
            wc.DownloadString(url);
        }
        catch { }
    }

    private void SuccessMessage(string ErrorMessage)
    {
        string response = "{\"statuscode\":\"TXN\",\"status\":\"" + ErrorMessage + "\"}";
        Response.Write(response);
        return;

    }

    private void GetRoleList()
    {
        DataTable dtRole = objConnection.select_data_dt("Exec ManageRole @Action='OutSideReg',@ID=1");
        Response.Write(ConvertDataTabletoString(dtRole));

    }
    private void GetPackageList(string RoleID)
    {
        DataTable dtRole = objConnection.select_data_dt("FillDropDown 'RoleWisePackage','" + RoleID + "'");
        Response.Write(ConvertDataTabletoString(dtRole));

    }

    private bool CheckUser(string UserID, string Password, string DeviceID, string MethodName)
    {
        if (MethodName.ToLower() == "getcompanydetails" || MethodName.ToLower() == "verifyuserinotherphone")
        {
            return true;
        }
        else
        {
            DataTable dt = new DataTable();
            dt = objConnection.select_data_dt("ProcRecharge_AndroidService 'CheckUser','" + UserID + "','" + Password + "','" + DeviceID + "'");
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
    private void VerifyUserInOtherPhone(string UserID, string Password, string DeviceID, string LoginPin)
    {
        try
        {
            dt = objConnection.select_data_dt("Exec ProcRecharge_AndroidService 'VerifyUserInOtherPhone', '" + UserID + "', '" + Password + "', '" + DeviceID + "','" + LoginPin + "'");
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["Status"]) == 0)
                    Response.Write(ConvertDataTabletoString(dt));
                else
                    ErrorMessage(Convert.ToString(dt.Rows[0]["Message"]));
            }
            else
            {
                ErrorMessage("No Record Found!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }

    }
    private bool CheckDeviceID(string UserID, string Password, string DeviceID, string MethodName)
    {
        //if (MethodName.ToLower() == "getcompanydetails")
        //{
        //    return true;
        //}
        //else
        //{
        //    DataTable dt = new DataTable();
        //    dt = objConnection.select_data_dt("ProcRecharge_AndroidService 'CheckDeviceID','" + UserID + "','" + Password + "','" + DeviceID + "'");
        //    if (dt.Rows.Count > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        return true;
        //    }
        //}

    }


    #region [ 1.GetCompanyDetails ]
    public void GetCompanyDetails(string id)
    {
        try
        {
            dt = objConnection.select_data_dt("Exec MANAGECOMPANY " + id + ",'GetAll'");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }
    #endregion

    private string GetLocationJson(string IP)
    {
        var client = new RestClient("http://ip-api.com/json/" + IP + "?fields=status,message,country,countryCode,region,regionName,city,zip,lat,lon,timezone,isp,org,as,query");
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json");
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

    #region [ 2.GetLoginDetails ]
    public void GetLoginDetails(string userID, string password, string param1, string param2, string IP)
    {
        try
        {
            dt = objConnection.select_data_dt("Exec ProcRecharge_AndroidService 'GetLoginDetails', '" + userID + "', '" + password + "', '" + param1 + "'");
            if (dt.Rows.Count > 0)
            {
                try
                {
                    if (!string.IsNullOrEmpty(IP))
                    {
                        objConnection.update_data("AddEditLocationUser '','','" + userID + "','" + GetLocationJson(IP) + "'");
                    }
                }
                catch (Exception ex) { }
                if (Convert.ToInt32(dt.Rows[0]["Status"]) == 0)
                    Response.Write(ConvertDataTabletoString(dt));
                else
                    ErrorMessage(Convert.ToString(dt.Rows[0]["Message"]));
            }
            else
            {
                ErrorMessage("Invalid Mobile or Password ..!!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }
    #endregion

    #region [ 4.GetOperators ]

    public void GetOperators(string userID, string password, string deviceID, string param1)
    {
        try
        {
            dt = objConnection.select_data_dt("Exec ProcRecharge_AndroidService 'GetOperators', '" + userID + "', '" + password + "', '" + deviceID + "', '" + param1 + "'");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("Services not assigned to you. Please contact to your Admin.");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 5.GetCircles ]

    public void GetCircles(string userID, string password)
    {
        try
        {
            dt = objConnection.select_data_dt("Exec ProcRecharge_AndroidService 'GetCircles', '" + userID + "', '" + password + "','',''");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 6.RechargeRequest ]

    public void RechargeRequest(string userID, string password, string Number, string Amount, string Operator, string Circle, string caNumber, string Cycle, string DueDate, string Account, string IPAddress, string IMEINumber, string CustomerName, string CustomerMobile, string CoupanCodeStr, string CoupanAmount, string PromoCode, string GUID)
    {
        try
        {
            DataTable dtMemberMaster = objConnection.select_data_dt("select * from Member where Mobile='" + userID + "' and Password='" + password + "' and  IsActive=1");
            if (dtMemberMaster.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(Number) && Number.Length <= 20)
                {

                    if (Convert.ToDecimal(Amount) > 0)
                    {
                        if (!string.IsNullOrEmpty(Operator) && Convert.ToInt32(Operator) > 0)
                        {
                            if (!string.IsNullOrEmpty(Circle) && Convert.ToInt32(Circle) > 0)
                            {
                                RechargeProcess(Convert.ToInt32(dtMemberMaster.Rows[0]["msrno"]), Number, caNumber, Convert.ToDecimal(Amount), Convert.ToInt32(Operator), Convert.ToInt32(Circle), Cycle, DueDate, "Queued", IPAddress, IMEINumber, CustomerName, CustomerMobile, Account, CoupanCodeStr, Convert.ToInt32(CoupanAmount), PromoCode, GUID);
                            }
                            else
                            {
                                ErrorMessage("Invalid Circle");
                            }
                        }
                        else
                        {
                            ErrorMessage("Invalid Operator");
                        }
                    }
                    else
                    {
                        ErrorMessage("Invalid Amount");
                    }
                }
                else
                {
                    ErrorMessage("Invalid Number");
                }
            }
            else
            {
                ErrorMessage("Invalid UserID OR Password");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 6.Payment Request ]

    public void PaymentRequest(string userID, string password, string ReferanceNo, string PaymentDate, string PaymentMode, string Remark, string Amount)
    {
        try
        {
            dt = objConnection.select_data_dt("Exec ProcRecharge_AndroidService 'FundRequest', '" + userID + "', '" + password + "','',''," + Amount + ",'" + ReferanceNo + "','" + PaymentDate + "','" + PaymentMode + "','" + Remark + "'");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["id"].ToString() == "1")
                {
                    SuccessMessage(dt.Rows[0]["Status"].ToString());
                }
                else
                {
                    ErrorMessage(dt.Rows[0]["Status"].ToString());
                }
            }
            else
            {
                ErrorMessage("No Record Found!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 8.GetPaymentMode ]

    public void GetPaymentMode(string userID, string password)
    {
        try
        {
            dt = objConnection.select_data_dt("Exec ProcRecharge_AndroidService 'GetPaymentMode', '" + userID + "', '" + password + "','',''");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 9.ChangedPin ]

    public void ChangedPin(string userID, string password, string OldPin, string NewPin)
    {
        try
        {
            int Va = objConnection.update_data("Exec ProcRecharge_AndroidService 'ChangedPin', '" + userID + "', '" + password + "','','','','',null,'','" + OldPin + "','" + NewPin + "'");
            if (Va > 0)
            {
                SuccessMessage("Pin Changed Successfully ..!!");
            }
            else
            {
                ErrorMessage("Pin Changed Un-Successfully ..!!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 10.ChangedPassword ]

    public void ChangedPassword(string userID, string password, string OldPassword, string NewPassword)
    {
        try
        {
            int Va = objConnection.update_data("Exec ProcRecharge_AndroidService 'CHANGEPassword', '" + userID + "', '" + password + "','','','','',null,'','" + OldPassword + "','" + NewPassword + "'");
            if (Va > 0)
            {
                SuccessMessage("Password Changed Successfully ..!!");
            }
            else
            {
                ErrorMessage("Password Changed Un-Successfully ..!!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion


    private string RechargeProcess(int msrNo, string number, string caNumber, decimal amount, int operatorID, int circleID, string cycle, string dueDate, string status, string ipAddress, string macAddress, string customerName, string customerMobile, string account, string coupanCodeStr, int coupanAmount, string promoCode, string GUID)
    {
        string HistoryID = string.Empty;
        {
            {
                string Recharge_Result = clsm.Cyrus_RechargeProcess_Ravi(0, msrNo, number, caNumber, amount, operatorID, circleID, "", cycle, dueDate, status, ipAddress, macAddress, customerName, customerMobile, account, "APP", coupanCodeStr, coupanAmount, 0, promoCode);
                char Splitter = Convert.ToChar(",");
                string[] split = Recharge_Result.Split(Splitter);
                if (split.Length > 2)
                {
                    dt = objConnection.select_data_dt("PROC_MANAGE_RECHARGE_HISTORY 'BYID', " + Convert.ToInt32(split[1].ToString()) + "");
                    if (dt.Rows.Count > 0)
                    {
                        string mobile = dt.Rows[0]["Mobileno"].ToString();
                        string Amount = dt.Rows[0]["Amount"].ToString();
                        string Msrno = dt.Rows[0]["MemberID"].ToString();
                        string transid = dt.Rows[0]["TransID"].ToString();
                        string apiMessage = dt.Rows[0]["APIMessage"].ToString();
                        if (split[0] == "Recharge Successful !!")
                        {
                            HistoryID = split[1];
                            try
                            {

                                DataTable dtMemberMaster = new DataTable();
                                dtMemberMaster = objConnection.select_data_dt("Select * from Member Where MsrNo=" + msrNo + "");
                                if (dtMemberMaster.Rows[0]["Mobile"].ToString() != mobile)
                                {
                                    string[] valueArray = new string[4];
                                    valueArray[0] = dtMemberMaster.Rows[0]["Name"].ToString();
                                    valueArray[1] = mobile;
                                    valueArray[2] = Amount;
                                    valueArray[3] = transid;
                                    // SMS.SendWithVar(dtMemberMaster.Rows[0]["Mobile"].ToString(), 33, valueArray, msrNo);
                                }
                            }
                            catch { }
                            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                            var columns = new Dictionary<string, object>
                        {
                            { "Status", 0},
                            { "Message", "Recharge Successful"},
                            { "Number", mobile},
                            { "Amount", Amount},
                            { "RechargeStatus", "Success"},
                            { "TransactionID", transid},
                            { "DateTime", DateTime.Now.ToString()}
                        };
                            Response.Write(SuccessArray(Convert.ToString(jsSerializer.Serialize(columns))));
                        }
                        else if (split[0] == "Recharge Failed !!")
                        {
                            DataTable dtHistory = new DataTable();
                            dtHistory = objConnection.select_data_dt("ProcRecharge_ManageHistory 'Get', " + Convert.ToInt32(split[1].ToString()) + "");
                            mobile = dtHistory.Rows[0]["MobileNo"].ToString();
                            Amount = dtHistory.Rows[0]["Amount"].ToString();
                            transid = dtHistory.Rows[0]["TransID"].ToString();
                            HistoryID = split[1];
                            try
                            {

                            }
                            catch { }
                            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                            var columns = new Dictionary<string, string>
                            {
                                { "Status", "0"},
                                { "Message", "Recharge Failed"},
                                { "Number", mobile},
                                { "Amount", Amount},
                                { "RechargeStatus", "Failure"},
                                { "TransactionID", transid},
                                { "DateTime", DateTime.Now.ToString()}
                            };
                            Response.Write(SuccessArray(Convert.ToString(jsSerializer.Serialize(columns))));
                        }
                        else if (split[0] == "Request Submited !!")
                        {
                            //DataTable dtHistory = new DataTable();
                            //dtHistory = objHistory.ManageHistory("GetAll", Convert.ToInt32(split[1]));
                            //string mobile = dtHistory.Rows[0]["Mobileno"].ToString();
                            //string Amount = dtHistory.Rows[0]["RechargeAmount"].ToString();
                            //string transid = dtHistory.Rows[0]["TransID"].ToString();
                            //HistoryID = split[1];
                            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                            var columns = new Dictionary<string, string>
                            {
                                { "Status", "0"},
                                { "Message", "Request Submited"},
                                { "Number", mobile},
                                { "Amount", Amount},
                                { "RechargeStatus", "Pending"},
                                { "TransactionID", transid},
                                { "DateTime", DateTime.Now.ToString()}
                            };
                            Response.Write(SuccessArray(Convert.ToString(jsSerializer.Serialize(columns))));
                        }
                        else
                        {
                            ErrorMessage(split[0].ToString());
                        }
                    }
                    else
                    {
                        ErrorMessage(split[0].ToString());
                    }
                }
                else
                {
                    ErrorMessage(Recharge_Result);
                }
            }


        }
        return HistoryID;
    }

    #region [ 8.GetService ]

    public void GetService(string userID, string password)
    {
        try
        {
            dt = objConnection.select_data_dt("select * from Service where SectionType=1");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 12.GetBalance ]

    public void GetBalance(string userID, string password)
    {
        try
        {
            dt = objConnection.select_data_dt("Exec ProcRecharge_AndroidService 'GetBalance', '" + userID + "', '" + password + "','','','','',null,'','',''");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("Password Changed Un-Successfully ..!!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 13.GetBalance ]

    public void GetBcDeatils(string userID, string password)
    {
        try
        {
            dt = objConnection.select_data_dt("Exec ProcRecharge_AndroidService 'GetBCDetails', '" + userID + "', '" + password + "','','','','',null,'','',''");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("Password Changed Un-Successfully ..!!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion


    #region [ 14.AEPSWithdrwalRequest ]

    public void AEPSWithdrwalRequest(string userID, string password, string Remark, string AepsWithdrwalType, string Amount, string TransactionMode, string BankID, string Accountno)
    {
        try
        {
            DataTable dtMember = objConnection.select_data_dt("select * from member where mobile='" + userID + "'");
            Int32 HoldCount = objConnection.select_data_scalar_int("select count(*) from Member where Msrno=" + dtMember.Rows[0]["msrno"].ToString() + " and OnHold=1");
            if (HoldCount == 0)
            {
                var date = DateTime.Now;
                string PayoutFrom = "";

                string guid = "AP" + System.Guid.NewGuid().ToString().Replace('-', '0');
                Int32 StartTime = objConnection.select_data_scalar_int("select top 1 StartTime from impstime");
                Int32 EndTime = objConnection.select_data_scalar_int("select top 1 endtime from impstime");
                if (date.Hour + 1 >= StartTime && date.Hour < EndTime)
                {
                    if (Convert.ToDecimal(Amount) > 0)
                    {
                        var ActiveData = GetActivePayoutAPI(Amount, dtMember);
                        PayoutFrom = ActiveData.Item1;
                        string MaxSenderAmount = ActiveData.Item2;
                        if (Convert.ToDecimal(Amount) <= Convert.ToDecimal(MaxSenderAmount) && !string.IsNullOrEmpty(PayoutFrom) || (AepsWithdrwalType.ToUpper() == "WALLET"))
                        {
                            try
                            {
                                if (AepsWithdrwalType.ToUpper() == "BANK")
                                {
                                    string AccountSelected = Accountno;

                                    if (!string.IsNullOrEmpty(AccountSelected))
                                    {
                                        DataTable dtBankID = objConnection.select_data_dt("select * from View_MemberBankList where msrno = '" + dtMember.Rows[0]["msrno"] + "' and AccountNumber='" + AccountSelected + "'");
                                        if (dtBankID.Rows.Count > 0)
                                        {
                                            decimal Charge = 0;
                                            string tMode = "";
                                            if (AepsWithdrwalType.ToUpper() == "BANK")
                                            {
                                                //if (TransactionMode.ToUpper() == "IMPS")
                                                //{

                                                Charge = MoneyTransfer_EkoAPI.GetPayoutCharge(Convert.ToDecimal(Amount), TransactionMode);

                                                //}
                                                tMode = TransactionMode;
                                            }
                                            string Balance = objConnection.select_data_scalar_string("SELECT BALANCE FROM TBLMLM_AEPS2EWALLETBALANCE WHERE MSRNO = '" + dtMember.Rows[0]["msrno"] + "'");
                                            Int32 countP = objConnection.select_data_scalar_int("select * from AEPS_WITHDRWALREQUEST where msrno=" + dtMember.Rows[0]["msrno"] + " and RequestStatus='Pending'");

                                            if (countP == 0)
                                            {
                                                if (Convert.ToDecimal(Balance) > (Convert.ToDecimal(Amount) + Charge))
                                                {
                                                    string Narration = "Amound Deducted For Withdraw Request-" + guid;
                                                    DataTable dtWall = new DataTable();
                                                    dtWall = objConnection.select_data_dt("Exec PROC_AEPS2EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"] + "','" + (Convert.ToDecimal(Amount) + Charge) + "','DR','" + Narration + "','Web','" + guid + "'");
                                                    if (dtWall.Rows[0]["id"].ToString() == "1")
                                                    {
                                                        DataTable dtValue;
                                                        {
                                                            dtValue = objConnection.select_data_dt("EXEC PROC_AEPS_WITHDRWALREQUEST '" + dtMember.Rows[0]["loginid"] + "','" + Amount + "','" + dtBankID.Rows[0]["ID"].ToString() + "','" + Remark + "','" + AepsWithdrwalType + "','" + tMode + "'," + Charge + ",'" + guid + "'," + PayoutFrom + "");
                                                            if (Convert.ToInt32(dtValue.Rows[0]["ID"]) > 0)
                                                            {
                                                                if (AepsWithdrwalType.ToUpper() == "BANK")
                                                                {
                                                                    string result = "";
                                                                    if (PayoutFrom == "0")
                                                                    {
                                                                        MoneyTransferRequestST moneyTransferRequestST = new MoneyTransferRequestST();
                                                                        moneyTransferRequestST.Amount = Convert.ToDecimal(Amount);
                                                                        moneyTransferRequestST.ifsc = dtBankID.Rows[0]["IFSCCode"].ToString();
                                                                        moneyTransferRequestST.number = dtBankID.Rows[0]["AccountNumber"].ToString();
                                                                        moneyTransferRequestST.PaymentType = "IMPS";
                                                                        moneyTransferRequestST.OrderID = guid;
                                                                        moneyTransferRequestST.CustomerMobileNo = dtMember.Rows[0]["mobile"].ToString();
                                                                        moneyTransferRequestST.MethodName = "sendamount";
                                                                        var context = new System.ComponentModel.DataAnnotations.ValidationContext(moneyTransferRequestST, serviceProvider: null, items: null);
                                                                        var results = new List<ValidationResult>();
                                                                        var isValid = Validator.TryValidateObject(moneyTransferRequestST, context, results, true);
                                                                        if (!isValid)
                                                                        {
                                                                            foreach (ValidationResult validationResult in results)
                                                                            {
                                                                                ErrorMessage(validationResult.ErrorMessage);
                                                                            }
                                                                        }
                                                                        else
                                                                        {
                                                                            string Result = ApiPostCall.PostCall(moneyTransferRequestST.GetJson(), SONITechnoCredentrial.PayOutURL);
                                                                            JObject TransferData = JObject.Parse(Result);
                                                                            if (TransferData["code"].ToString().ToUpper() != "ERR")
                                                                            {
                                                                                if (TransferData["data"]["Status"].ToString().ToUpper() == "SUCCESS" || TransferData["data"]["Status"].ToString().ToUpper() == "PENDING")
                                                                                {
                                                                                    objConnection.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + Convert.ToInt32(dtValue.Rows[0]["ID"]) + ",'" + TransferData["data"]["STID"].ToString() + "','" + TransferData["data"]["Status"].ToString() + "','" + TransferData["code"].ToString() + "','" + TransferData["mess"].ToString() + "','" + TransferData["data"]["RRN"].ToString() + "','" + Result + "'");

                                                                                    // objConnection.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + Convert.ToInt16(dtValue.Rows[0]["ID"]) + ",'" + TransferData["data"]["RRN"].ToString() + "',''");

                                                                                    if (TransferData["data"]["Status"].ToString().ToUpper() == "PENDING")
                                                                                    {
                                                                                        SuccessMessage("https://multiwayrecharge.in/WithdrawRequestReceipt.aspx?GUID=" + guid + "");
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        SuccessMessage("https://multiwayrecharge.in/WithdrawRequestReceipt.aspx?GUID=" + guid + "");
                                                                                    }

                                                                                    //objConnection.update_data("Update DMTHYPTO set Status='" + TransferData["data"]["Status"].ToString() + "',txn_id='" + TransferData["data"]["RRN"].ToString() + "',Response='" + result + "',RazarID='" + TransferData["data"]["STID"].ToString() + "' where reference_number='" + TransferData["data"]["OrderID"].ToString() + "'");
                                                                                }
                                                                                else if (TransferData["data"]["Status"].ToString().ToUpper() == "FAILED")
                                                                                {
                                                                                    ErrorMessage("Transaction FAILURE Successfully. Your Amount -" + (Convert.ToDecimal(Amount) + Charge) + " refund . aeps wallet.");
                                                                                    objConnection.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE','" + Convert.ToInt32(dtValue.Rows[0]["ID"]) + "','" + TransferData["data"]["STID"].ToString() + "','" + TransferData["data"]["Status"].ToString() + "','" + TransferData["code"].ToString() + "','" + TransferData["mess"].ToString() + "','" + TransferData["data"]["RRN"].ToString() + "','" + Result + "'");

                                                                                    // objConnection.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE','" + Convert.ToInt16(dtValue.Rows[0]["ID"]) + "','','" + TransferData["reason"].ToString() + "'");

                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                ErrorMessage("Transaction FAILURE Successfully. Your Amount -" + (Convert.ToDecimal(Amount) + Charge) + " refund . aeps wallet.");
                                                                                objConnection.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_DELETE','" + Convert.ToInt32(dtValue.Rows[0]["ID"]) + "','','Refunded','','" + TransferData["mess"].ToString() + "'");

                                                                            }
                                                                        }
                                                                    }

                                                                }
                                                                else
                                                                {
                                                                    objConnection.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + Convert.ToInt16(dtValue.Rows[0]["ID"]) + ",'','Success','Wallet','Aeps To Wallet Success','',''");
                                                                    SuccessMessage("Transaction Successfully Trasnfer Aeps to Main Wallet");

                                                                }
                                                            }
                                                            else
                                                            {

                                                            }
                                                        }


                                                    }
                                                    else
                                                    {
                                                        ErrorMessage("AMOUNT MUST BE LESS OR EQUAL TO YOUR WALLET BALANCE");
                                                    }
                                                }
                                                else
                                                {
                                                    ErrorMessage("AMOUNT MUST BE LESS OR EQUAL TO YOUR WALLET BALANCE");

                                                }
                                            }
                                            else
                                            {
                                                ErrorMessage("Your One Request is in pending.So Can not apply next withdrwal rqeuest");

                                            }


                                        }
                                        else
                                        {
                                            ErrorMessage("* You Must Have a Bank Account to Submit a Withdraw Request ..!!*");
                                        }


                                    }
                                    else
                                    {
                                        ErrorMessage("* Select one account details ..!!*");
                                    }
                                }
                                else
                                {
                                    string Balance = objConnection.select_data_scalar_string("SELECT BALANCE FROM TBLMLM_AEPS2EWALLETBALANCE WHERE MSRNO = '" + dtMember.Rows[0]["msrno"] + "'");
                                    if (Convert.ToDecimal(Balance) > Convert.ToDecimal(Amount))
                                    {
                                        string Narration = "Amound Deducted For Withdraw Request-" + guid;
                                        DataTable dtWall = new DataTable();
                                        dtWall = objConnection.select_data_dt("Exec PROC_AEPS2EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"] + "','" + (Convert.ToDecimal(Amount)) + "','DR','" + Narration + "','Web','" + guid + "'");
                                        if (dtWall.Rows[0]["id"].ToString() == "1")
                                        {
                                            DataTable dtValue = objConnection.select_data_dt("EXEC PROC_AEPS_WITHDRWALREQUEST '" + dtMember.Rows[0]["loginid"] + "','" + Amount + "','0','" + Remark + "','" + AepsWithdrwalType + "','',0,'" + guid + "',0");
                                            if (Convert.ToInt16(dtValue.Rows[0]["ID"]) > 0)
                                            {
                                                SuccessMessage("Transaction Successfully Trasnfer Aeps to Main Wallet");
                                                objConnection.update_data("EXEC PROC_AEPS2WITHDRAW_REQUEST_ACTION 'AEPS2WITHDRAW_REQUEST_APPROVE'," + Convert.ToInt16(dtValue.Rows[0]["ID"]) + ",'','Success','Wallet','Aeps To Wallet Success','',''");
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorMessage("* something went wrong ..!!*");
                            }
                        }
                        else
                        {
                            ErrorMessage("* Only " + MaxSenderAmount + " Amount you can send");
                        }
                    }
                    else
                    {
                        ErrorMessage("* Amount should be greater than 0*");
                    }
                }
                else
                {
                    ErrorMessage("* AEPS allow b/w 9 AM to 6 PM*");
                }
            }
            else
            {
                ErrorMessage("Try after Some time");
            }
        }
        catch
        {
            ErrorMessage("Technical Error");
        }
    }

    #endregion

    private Tuple<string, string> GetActivePayoutAPI(string Amount, DataTable dtMem)
    {
        DataTable dtMember = dtMem;

        DataTable dtActivePayoutAPI = objConnection.select_data_dt("Proc_Get_Active_PayoutAPI '" + dtMember.Rows[0]["RoleID"] + "'," + Amount + "");
        if (dtActivePayoutAPI.Rows.Count > 0)
        {
            return Tuple.Create(dtActivePayoutAPI.Rows[0]["payoutFrom"].ToString(), dtActivePayoutAPI.Rows[0]["Amount"].ToString());
        }
        else
        {
            return Tuple.Create("", "0");
        }
    }

    #region [ 15.AEPSWithdrwalRequestHistory ]

    public void AEPSWithdrwalRequestHistory(string userID, string password, string FromDate, string ToDate, string Page, string Size)
    {
        try
        {
            string memberID = objConnection.select_data_scalar_string("select loginid from member where mobile='" + userID + "'");

            dt = objConnection.select_data_dt("EXEC PROC_AEPS_WITHDRAW_REPORT 'WITHDRAW_REPORTMobile','" + memberID + "','','" + FromDate + "','" + ToDate + "'," + Page + "," + Size + "");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 16.GetBankList ]

    public void GetBankList(string userID, string password)
    {
        try
        {
            dt = objConnection.select_data_dt("select * from Bank where IsActive=1");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion


    #region [ 17.AddBankDetails ]

    public void AddBankDetails(string userID, string password, string BankID, string IfscCode, string AccountNo, string AccountHolderName, string BranchName)
    {
        try
        {
            DataTable dtMember = objConnection.select_data_dt("select * from member where mobile='" + userID + "'");
            string CusID = objConnection.select_data_scalar_string("select ContactID  from ContactsRazor where msrno=" + dtMember.Rows[0]["msrno"].ToString() + " and Self=1");

            dt = objConnection.select_data_dt("Exec ProcRecharge_AndroidService 'AddankDetails', '" + userID + "', '" + password + "','" + BankID + "','" + IfscCode + "','" + AccountNo + "','" + AccountHolderName + "',null,'" + BranchName + "','',''");

            ErrorMessage(dt.Rows[0]["Status"].ToString());


        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    public void EditAddBankDetails(string userID, string password, string BankID, string IfscCode, string AccountNo, string AccountHolderName, string BranchName, string ID)
    {
        try
        {
            DataTable dtMember = objConnection.select_data_dt("select * from member where mobile='" + userID + "'");
            dt = objConnection.select_data_dt("exec AddEditMemberBankDetails '" + ID + "','0','" + BankID + "','" + IfscCode + "','" + AccountNo + "','" + AccountHolderName + "','" + BranchName + "'");

            ErrorMessage(dt.Rows[0]["Status"].ToString());

        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #region [ 18.GetBankDetails ]

    public void GetBankDetails(string userID, string password)
    {
        try
        {
            Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");
            dt = objConnection.select_data_dt("select * from VIEW_MEMBERBANKDETAILS where msrno = " + Msrno + "");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found");
            }

        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 19.GetServiceList ]

    public void GetServiceList(string userID, string password)
    {
        try
        {
            Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");
            dt = objConnection.select_data_dt("GetActiveServiceAll " + Msrno + "");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found");
            }

        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 20.GetUPIDetails ]

    public void GetUPIDetails(string userID, string password)
    {
        try
        {
            Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

            Int32 IsUPIAccountExists = objConnection.select_data_scalar_int("select count(1) from MemberUPI where msrno='" + Msrno + "'");
            if (IsUPIAccountExists > 0)
            {
                DataTable dtUPI = objConnection.select_data_dt("select Msrno,ICICIUPI as UPIID,Name,Pan,Category,Address,'ACTIVATED' as  Status,Convert(nvarchar(30),AddDate,106) as adddate from MemberUPI where msrno='" + Msrno + "'");
                if (dtUPI.Rows.Count > 0)
                {
                    Response.Write(ConvertDataTabletoString(dtUPI));
                }
                else
                {
                    ErrorMessage("No Record Found");
                }
            }
            else
            {
                ErrorMessage("Upi Not Found");
            }

        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 21.CreateUPI ]

    public void CreateUPI(string userID, string password, string category, string address, string name, string pan, string upi_id)
    {
        try
        {
            Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");
            string Prefix = "1234";// objConnection.select_data_scalar_string("Select name from UPIPREFIX where status='APPROVED'");
            if (!string.IsNullOrEmpty(Prefix))
            {
                string URL = "https://sonitechno.in/api/UPI.aspx";
                Location ObjLoaction = new Location();
                ObjLoaction = ObjLoaction.GetLocationData();
                UPI ObjUpi = new UPI();
                ObjUpi.category = category;
                ObjUpi.address = address;
                ObjUpi.lat = ObjLoaction.Latitude;
                ObjUpi.lon = ObjLoaction.Longitude;
                ObjUpi.name = name;
                ObjUpi.pan = pan;
                ObjUpi.upi_id = upi_id;
                ObjUpi.MethodName = "createupiid";
                string result = ApiPostCall.PostCall(ObjUpi.GetJson(), URL);
                JObject jData = JObject.Parse(result);
                if (jData["code"].ToString().ToLower() != "err")
                {
                    objConnection.update_data("PROC_MemberUPI '" + Msrno + "','" + jData["data"]["upi"]["upi_id"].ToString() + "','" + ObjUpi.name + "','" + ObjUpi.pan + "','" + ObjUpi.category + "','" + ObjUpi.address + "','" + result + "'");
                    SuccessMessage("Upi ID Create Successfully.");

                }
                else
                {
                    ErrorMessage(jData["mess"].ToString());
                }
            }
            else
            {
                ErrorMessage("Contact to Admin Prefix not approved.");
            }

        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 22.GetVirtualAccountDetails ]

    public void GetVirtualAccountDetails(string userID, string password)
    {
        try
        {
            Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

            Int32 IsUPIAccountExists = objConnection.select_data_scalar_int("select count(1) from Member_VirtualAccount where msrno='" + Msrno + "'");
            if (IsUPIAccountExists > 0)
            {
                DataTable dtUPI = objConnection.select_data_dt("select * from Member_VirtualAccount where msrno='" + Msrno + "'");
                if (dtUPI.Rows.Count > 0)
                {
                    Response.Write(ConvertDataTabletoString(dtUPI));
                }
                else
                {
                    ErrorMessage("No Record Found");
                }
            }
            else
            {
                ErrorMessage("Virtual Account Not Found");
            }

        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion


    #region [ 23.CreateVirtualAccount ]

    public void CreateVirtualAccount(string userID, string password, string reference_number, string udf1, string udf2, string udf3)
    {
        try
        {
            Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

            string URL = "https://partners.hypto.in/api/virtual_accounts";
            VirtualAccount ObjVirtual = new VirtualAccount();
            ObjVirtual.reference_number = reference_number;
            ObjVirtual.udf1 = udf1;
            ObjVirtual.udf2 = udf2;
            ObjVirtual.udf3 = udf3;
            DataTable dtVTID = objConnection.select_data_dt("exec Proc_Member_VirtualAccount '" + Msrno + "','" + ObjVirtual.reference_number + "','" + ObjVirtual.udf1 + "','" + ObjVirtual.udf2 + "','" + ObjVirtual.udf3 + "','',''");
            if (dtVTID.Rows.Count > 0)
            {
                string result = ApiPostCall.PostCall(ObjVirtual.GetJson(), URL);
                VirtualResultRoot bsObj = JsonConvert.DeserializeObject<VirtualResultRoot>(result);
                if (bsObj.success.ToLower() == "true")
                {
                    foreach (var item in bsObj.data.virtual_account.details)
                    {
                        Int32 Detail = objConnection.update_data("exec Proc_Member_VirtualAccount_Details '" + dtVTID.Rows[0]["VTID"].ToString() + "','" + item.account_number + "','" + item.account_ifsc + "','" + string.Join(", ", item.payment_modes.ToArray()) + "','ACTIVE'");
                    }
                    objConnection.select_data_scalar_int("exec Proc_Member_VirtualAccount '" + dtVTID.Rows[0]["VTID"].ToString() + "','" + bsObj.data.virtual_account.reference_number + "','','','','" + bsObj.success + "',''");
                    SuccessMessage("Virtual Account Created Successfully ..!!");

                }

            }
            else
            {
                ErrorMessage("Virtual Account Already Exists on This Reference No ..!!");
            }

        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion


    #region [ 24.GetUPIID ]

    public void GetUPIID(string userID, string password)
    {
        try
        {
            Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

            Int32 IsUPIAccountExists = objConnection.select_data_scalar_int("select count(1) from MemberUPI where msrno='" + Msrno + "'");
            if (IsUPIAccountExists > 0)
            {
                DataTable dtUPI = objConnection.select_data_dt("select ICICIUPI as UPIID,Name from MemberUPI where msrno='" + Msrno + "'");
                if (dtUPI.Rows.Count > 0)
                {
                    Response.Write(ConvertDataTabletoString(dtUPI));
                }
                else
                {
                    ErrorMessage("No Record Found");
                }
            }
            else
            {
                ErrorMessage("UPI ID Not Found");
            }

        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 25.GetDocList ]

    public void GetDocList(string userID, string password)
    {
        try
        {
            Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

            DataTable dt = objConnection.select_data_dt("exec PROC_MANAGEKYCDOCUMENTS  '" + Msrno + "','ForMobile'");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found");
            }


        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 26.GetKYCList ]

    public void GetKYCList(string userID, string password)
    {
        try
        {
            Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + userID + "'");

            DataTable dt = objConnection.select_data_dt("select Msrno,DocID,DocName,DocNumber,DocImage,DocImageBack,IsApproved,Status,IsActive,IsDelete,Convert(nvarchar(30),AddDate,106)AddDate,LastUpdate,Reason from MEMBERKYCDOCUMENTS where msrno = " + Msrno + " ");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found");
            }


        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion



    private Bitmap Resize_Image(Stream streamImage, int maxWidth, int maxHeight)
    {
        Bitmap originalImage = new Bitmap(streamImage);
        int newWidth = originalImage.Width;
        int newHeight = originalImage.Height;
        double aspectRatio = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height);

        newWidth = maxWidth;
        newHeight = maxHeight;
        return new Bitmap(originalImage, newWidth, newHeight);
    }


    #region [ 29.loantype ]

    public void loantype(string userID, string password)
    {
        try
        {
            dt = objConnection.select_data_dt("SELECT  loancatid, loancatname FROM loan_catagory WHERE flag =1");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion


    #region [ 30.loansubcategory ]

    public void loansubcategory(string userID, string password, string LoanTypeID)
    {
        try
        {
            dt = objConnection.select_data_dt("SELECT  loansubcatid, loansubcatname FROM loan_Sub_Category WHERE loancatid=" + LoanTypeID + "");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 31.installmenttype ]

    public void installmenttype(string userID, string password)
    {
        try
        {
            dt = objConnection.select_data_dt("Select Paymentmodeid,paymentmode From LOAN_PAYMENTMODE where isactive=1");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion

    #region [ 32.calculateemi ]

    public void calculateemi(string userID, string password, string installmenttype, string Amount, string loantenure, string loansubcat)
    {
        try
        {
            string lbltotalemi = "";
            if (installmenttype == "5")
            {
                lbltotalemi = Convert.ToString(objConnection.select_data_scalar_string("select (DATEDIFF(day,getdate(),(dateadd(month," + loantenure + ",getdate()))))/7"));
            }
            else if (installmenttype == "9")
            {
                lbltotalemi = Convert.ToString(objConnection.select_data_scalar_string("select (DATEDIFF(day,getdate(),(dateadd(month," + loantenure + ",getdate()))))/15"));
            }

            string lblintresttype = objConnection.select_data_scalar_string("Select interest_type from loan_Sub_Category Where LoanSubCatID=" + loansubcat + "");
            int loandisbursementid = objConnection.select_data_scalar_int("Select ISNULL(MAX(Loan_disbursement_id),0) FROM loan_disbursement");
            string lblintrrate = objConnection.select_data_scalar_string("Select annual_interest_rate from loan_Sub_Category Where LoanSubCatID=" + loansubcat + "");
            dt = objConnection.select_data_dt("SELECT * from [dbo].[calcInstallment] (" + loandisbursementid + ",'" + lblintresttype + "'," + installmenttype + "," + lblintrrate + "," + lbltotalemi + ",'" + Amount + "'," + loantenure + ",'" + DateTime.Now.ToString("MM-dd-yyyy") + "')");
            if (dt.Rows.Count > 0)
            {
                Response.Write(ConvertDataTabletoString(dt));
            }
            else
            {
                ErrorMessage("No Record Found!");
            }
        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    #endregion


    private void CashDepositTransactionHIstory(string UserID, string FromDate, string ToDate, string Page, string Size)
    {
        DataTable dt = new DataTable();
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

        dt = objConnection.select_data_dt("PROC_CashDepositTransaction_Mobile ''," + Msrno + ",'" + FromDate + "','" + ToDate + "'," + Page + "," + Size + "");
        if (dt.Rows.Count > 0)
        {
            Response.Write(ConvertDataTabletoString(dt));
        }
        else
        {
            ErrorMessage("Not Available");
        }
    }

    private void AadharpayTransactionHIstory(string UserID, string FromDate, string ToDate, string Page, string Size)
    {
        DataTable dt = new DataTable();
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

        dt = objConnection.select_data_dt("PROC_AadharTransaction_Mobile ''," + Msrno + ",'" + FromDate + "','" + ToDate + "'," + Page + "," + Size + "");
        if (dt.Rows.Count > 0)
        {
            Response.Write(ConvertDataTabletoString(dt));
        }
        else
        {
            ErrorMessage("Not Available");
        }
    }
    public int GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
    private void signup(string RoleID, string PackageID, string TitleID, string Name, string ShopName, string Email, string Mobile, string Aadhar, string Pan, string Gender, string Pin, string Address, string DOB, string CityName, string StateID, int ParentMsrno)
    {
        Registration registration = new Registration();

        Random generator = new Random();
        string number = GenerateRandomNo().ToString();// generator.Next(1, 10000).ToString("D4");
        registration.Name = Name;
        registration.ShopName = "";

        registration.Mobile = Mobile;
        registration.LoginPin = Convert.ToInt32(number);
        registration.IsActive = true;
        registration.GenderID = Convert.ToInt16(Gender);
        registration.Email = Email;
        registration.CityID = Convert.ToInt16(0);
        registration.PinCode = Pin;
        registration.DOB = DOB;
        registration.Address = Address;
        registration.PackageID = (Convert.ToInt16(RoleID) == 6 ? 2 : (Convert.ToInt16(RoleID) == 5 ? 3 : 4));
        registration.ParentID = ParentMsrno;  // Pass sessionid
        registration.Password = generator.Next(1, 10000).ToString("D10");
        registration.RoleID = Convert.ToInt16(RoleID);
        registration.TitleID = Convert.ToInt16(1);
        registration.AlterNativeMobile = Mobile;
        registration.Aadhar = Aadhar;
        registration.Pan = Pan;
        registration.StateID = Convert.ToInt32(StateID);
        registration.CityName = CityName;
        // TimeSpan sd = (DateTime.Now - Convert.ToDateTime(DOB));
        double Years = 50;

        if (Years > 18)
        {
            if (registration.PackageID > 0)
            {
                if (registration.IsAnyNullOrEmpty(registration))
                {
                    DataTable dtM = new DataTable();
                    dtM = objConnection.select_data_dt("AddEditMember " + registration.ID + ",'" + registration.RoleID + "','" + registration.TitleID + "','" + registration.Name + "','" + registration.Email + "','" + registration.Mobile + "','" + registration.GenderID + "','" + registration.LoginPin + "','" + registration.Password + "','" + registration.Address + "','" + registration.CityID + "','" + registration.PackageID + "'," + registration.ParentID + ",'','" + Aadhar + "','" + Pan + "','" + registration.PinCode + "','" + registration.DOB + "','" + registration.ShopName + "','" + CityName + "'," + StateID + "");
                    if (dtM.Rows.Count > 0)
                    {
                        if (dtM.Rows[0]["Status"].ToString() == "1")
                        {
                            string[] ValueArray = new string[4];
                            ValueArray[0] = registration.Name;
                            ValueArray[1] = dtM.Rows[0]["Result"].ToString();
                            ValueArray[2] = registration.Password;
                            ValueArray[3] = registration.LoginPin.ToString();
                            //  SMS.SendWithV(registration.Mobile, 9, ValueArray);
                            string strf = objConnection.select_data_scalar_string("GetFreeService");
                            string Msrnod = objConnection.select_data_scalar_string("select msrno from Member where loginid='" + dtM.Rows[0]["Result"].ToString() + "'");
                            Int32 Val = objConnection.update_data("PROC_ASSIGN_SERVICE '" + strf + "'," + Msrnod + "");
                            try
                            {
                                string URL = "https://sonitechno.in/api/UPI.aspx";
                                Location ObjLoaction = new Location();
                                ObjLoaction = ObjLoaction.GetLocationData();
                                UPI ObjUpi = new UPI();
                                ObjUpi.category = "Retail Stores";
                                ObjUpi.address = registration.Address;
                                ObjUpi.lat = ObjLoaction.Latitude;
                                ObjUpi.lon = ObjLoaction.Longitude;
                                ObjUpi.name = registration.Name;
                                ObjUpi.pan = registration.Pan;
                                ObjUpi.upi_id = registration.Mobile;
                                ObjUpi.MethodName = "createupiid";
                                string result = ApiPostCall.PostCall(ObjUpi.GetJson(), URL);
                                JObject jData = JObject.Parse(result);
                                if (jData["code"].ToString().ToLower() != "err")
                                {
                                    objConnection.update_data("PROC_MemberUPI '" + Msrnod + "','" + jData["data"]["upi"]["upi_id"].ToString() + "','" + ObjUpi.name + "','" + ObjUpi.pan + "','" + ObjUpi.category + "','" + ObjUpi.address + "','" + result + "'");
                                }


                                try
                                {
                                    //  Emails.SendEmail("sir", registration.Email, registration.Mobile, registration.Password, registration.LoginPin.ToString(), "welcome.htm", dtM.Rows[0]["Result"].ToString());
                                }
                                catch (Exception ex)
                                {

                                }

                            }
                            catch (Exception ex) { }
                            SuccessMessage("Your Login ID -" + dtM.Rows[0]["Result"].ToString() + "");
                        }
                        else
                        {
                            ErrorMessage(dtM.Rows[0]["Result"].ToString());
                        }
                    }
                    else
                    {
                        ErrorMessage("Try After some time");
                    }
                }
                else
                {

                }
            }
            else
            {
                ErrorMessage("Select Package");
            }
        }
        else
        {
            ErrorMessage("Only 18+ yrs old merchant are allowed to do this business.");
        }
    }

    private void CreateContact(string ID)
    {
        DataTable dtMember = objConnection.select_data_dt("select * from Member where loginid='" + ID + "'");
        if (dtMember.Rows.Count > 0)
        {
            CreateContact createContact = new CreateContact();
            createContact.contact = dtMember.Rows[0]["Mobile"].ToString();
            createContact.name = dtMember.Rows[0]["Name"].ToString();
            createContact.email = dtMember.Rows[0]["email"].ToString();
            createContact.reference_id = System.Guid.NewGuid().ToString().Replace("-", "0");
            createContact.type = "employee";
            CreateContactNotes createContactNotes = new CreateContactNotes();
            createContactNotes.note_key = "Create Contact";
            createContact.notes = createContactNotes;
            string result = ApiPostCallRazorpayx.PostCall(createContact.GetJson(), "contacts");
            JObject Data = JObject.Parse(result);
            if (Data["error"] == null)
            {
                CreateContactResultRoot contactResultRoot = JsonConvert.DeserializeObject<CreateContactResultRoot>(result);
                int Val = 0;
                Val = objConnection.update_data("Proc_ContactsRazor '" + contactResultRoot.id + "','" + contactResultRoot.name + "','" + contactResultRoot.contact + "','" + contactResultRoot.email + "'," + dtMember.Rows[0]["msrno"].ToString() + ",'" + contactResultRoot.reference_id + "','" + contactResultRoot.active + "',0,'" + result + "',1");
                if (Val > 0)
                {

                }
            }

        }

    }

    private void RNFIOnboard(string ID, string StateID)
    {
        AEPSBankBodyRequest aEPSBankBodyRequest = new AEPSBankBodyRequest();

        DataTable dtMember = objConnection.select_data_dt("select * from Member where loginid='" + ID + "'");
        if (dtMember.Rows.Count > 0)
        {
            MerchantOnboarding merchantOnboarding = new MerchantOnboarding();
            merchantOnboarding.address = dtMember.Rows[0]["address"].ToString();
            merchantOnboarding.name = dtMember.Rows[0]["Name"].ToString();
            merchantOnboarding.phone = dtMember.Rows[0]["Mobile"].ToString();
            merchantOnboarding.pincode = dtMember.Rows[0]["PinCode"].ToString();
            merchantOnboarding.pannumber = dtMember.Rows[0]["pan"].ToString();
            merchantOnboarding.stateid = Convert.ToInt32(StateID);
            merchantOnboarding.submerchantid = dtMember.Rows[0]["LoginID"].ToString();
            merchantOnboarding.firmname = dtMember.Rows[0]["shopname"].ToString();
            merchantOnboarding.city = dtMember.Rows[0]["cityname"].ToString();
            merchantOnboarding.email = dtMember.Rows[0]["email"].ToString();

            string Result = aEPSBankBodyRequest.MerchantOnboarding(merchantOnboarding);
            JObject jObject = JObject.Parse(Result);
            //{"response":1,"status":true,"message":"Merchant activated successfully.!"}
            //{ "status":false,"message":"city is required."}
            if (jObject["status"] == null)
            {
                if (jObject["response"].ToString() == "1")
                {
                    objConnection.update_data("update Member set ActivedForRnfi=1 where msrno=" + dtMember.Rows[0]["msrno"].ToString() + "");
                }

            }

        }

    }
    private void PurchaseService(string userID, string password, string ServiceID)
    {
        double Price = objConnection.select_data_scalar_double("select price from Service where id=" + ServiceID + "");
        string ServiceName = objConnection.select_data_scalar_string("select name from Service where id=" + ServiceID + "");
        double MyBalance = 0;
        DataTable dtMember = objConnection.select_data_dt("select * from Member where mobile='" + userID + "'");

        DataTable dtWalletBalance = new DataTable();
        dtWalletBalance = objConnection.select_data_dt("Exec [ProcMLM_ManageEWalletBalance] 'GetBalanceByMsrNo', " + Convert.ToInt32(dtMember.Rows[0]["Msrno"]));
        if (dtWalletBalance.Rows.Count > 0)
        {
            MyBalance = Convert.ToDouble(dtWalletBalance.Rows[0]["Balance"]);

        }
        if (MyBalance >= Price && Price > 0)
        {
            string reference_number = System.Guid.NewGuid().ToString().Replace('-', '0');

            string Narration = "Service Active TransID-" + reference_number;
            DataTable dtPayment = objConnection.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"].ToString() + "','" + Price + "','DR','" + Narration + "'");
            if (dtPayment.Rows[0]["msrno"].ToString() == "1")
            {
                string strService = objConnection.select_data_scalar_string("select SERVICE from MEMBERSERVICE where msrno=" + Convert.ToInt32(dtMember.Rows[0]["Msrno"]) + "");
                Int32 Val = objConnection.update_data("PROC_ASSIGN_SERVICE '" + string.Concat(strService, "," + ServiceID) + "'," + dtMember.Rows[0]["msrno"].ToString() + "");
                if (Val > 0)
                {
                    objConnection.update_data("Proc_ServiceDistributeCommission " + dtMember.Rows[0]["Msrno"].ToString() + ",'" + reference_number + "','" + ServiceID + "'," + Price + "");
                    SuccessMessage(" Service Active Successfully");
                }
            }
        }
        else
        {
            if (Price == 0)
            {
                string strService = objConnection.select_data_scalar_string("select SERVICE from MEMBERSERVICE where msrno=" + Convert.ToInt32(dtMember.Rows[0]["Msrno"]) + "");
                Int32 Val = objConnection.update_data("PROC_ASSIGN_SERVICE '" + string.Concat(strService, "," + ServiceID) + "'," + dtMember.Rows[0]["msrno"].ToString() + "");
                if (Val > 0)
                {
                    SuccessMessage(" Service Active Successfully");
                }
            }
            else
            {
                ErrorMessage("Low Balance");
            }
        }
    }



    private void UPIList(string UserID)
    {
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

        string ContactsID = objConnection.select_data_scalar_string("select ContactID from ContactsRazor where msrno=" + Msrno + " and MobileNo='" + UserID + "'");

        DataTable dtUPIDetails = objConnection.select_data_dt("select ID,FundAccount_id,account_number from FundAccountRazar where msrno=" + Msrno + " and contact_id='" + ContactsID + "' and account_type='vpa'");
        if (dtUPIDetails.Rows.Count > 0)
        {
            Response.Write(ConvertDataTabletoString(dtUPIDetails));
        }
        else
        {
            ErrorMessage("Not Available");
        }
    }


    private void AddUpi(string UserID, string UPI)
    {
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

        string ContactsID = objConnection.select_data_scalar_string("select ContactID from ContactsRazor where msrno=" + Msrno + " and  Self=1");

        VpaRequest vpaRequest = new VpaRequest();
        Vpa vpa = new Vpa();
        vpa.address = UPI;
        vpaRequest.vpa = vpa;
        vpaRequest.account_type = "vpa";
        vpaRequest.contact_id = ContactsID;
        string result = ApiPostCallRazorpayx.PostCall(vpaRequest.GetJson(), "fund_accounts");
        JObject Data = JObject.Parse(result);
        if (Data["error"] == null)
        {
            VPAResultRoot fundAccountResultRoot = JsonConvert.DeserializeObject<VPAResultRoot>(result);
            int Val = 0;
            Val = objConnection.update_data("Proc_FundAccountRazar '" + fundAccountResultRoot.id + "','" + fundAccountResultRoot.contact_id + "','" + fundAccountResultRoot.account_type + "'," + Msrno + ",'" + fundAccountResultRoot.active + "','" + fundAccountResultRoot.vpa.handle + "','','','" + fundAccountResultRoot.vpa.address + "','" + result + "'");
            if (Val > 0)
            {
                SuccessMessage("Verify Successfully");
            }
        }
        else
        {
            ErrorMessage(Data["error"]["description"].ToString());
        }
    }

    private void SendAmountUsingUPI(string userID, string upiid, string Amount, string TPIN)
    {
        DataTable dtMember = objConnection.select_data_dt("select * from member where mobile='" + userID + "'");
        Int32 HoldCount = objConnection.select_data_scalar_int("select count(*) from Member where Msrno=" + dtMember.Rows[0]["msrno"].ToString() + " and OnHold=1");
        if (HoldCount == 0)
        {
            string guid = "SU" + System.Guid.NewGuid().ToString().Replace('-', '0');
            DataTable dtWalletBalance = new DataTable();
            decimal MyBalance = 0;


            dtWalletBalance = objConnection.select_data_dt("Exec [ProcMLM_ManageEWalletBalance] 'GetBalanceByMsrNo', " + Convert.ToInt32(dtMember.Rows[0]["Msrno"]));
            if (dtWalletBalance.Rows.Count > 0)
            {
                MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["Balance"]);

            }
            decimal surcharge = MoneyTransfer_EkoAPI.GetUPISurcharge(Convert.ToDecimal(Amount));
            decimal TotalAmount = surcharge + Convert.ToDecimal(Amount);

            if (MyBalance >= Convert.ToDecimal(TotalAmount)) // Verification fee
            {
                string ContactsID = objConnection.select_data_scalar_string("select ContactID from ContactsRazor where msrno=" + dtMember.Rows[0]["msrno"].ToString() + " and Self=1");
                string fund_account_id = objConnection.select_data_scalar_string("select FundAccount_id from FundAccountRazar where msrno=" + dtMember.Rows[0]["msrno"].ToString() + " and contact_id='" + ContactsID + "' and account_number='" + upiid + "'");
                if (!string.IsNullOrEmpty(fund_account_id))
                {
                    string Narration = "Send UPI TransID-" + guid;
                    DataTable dtWall = new DataTable();
                    DataTable dtDRCR = objConnection.select_data_dt("exec [PROC_EWALLETTRANSACTION_CashDeposit]  '" + dtMember.Rows[0]["LoginID"].ToString() + "', " + Convert.ToDecimal(TotalAmount) + ", 'Dr','" + Narration + "','" + System.Guid.NewGuid().ToString() + "'");
                    if (dtDRCR.Rows[0]["msrno"].ToString() == "1")
                    {


                        Int32 ValInsert = objConnection.update_data("EXEC AddEditSendToUPI '" + dtMember.Rows[0]["msrno"] + "','" + Amount + "'," + surcharge + ",'" + fund_account_id + "','UPI','" + guid + "','','','Pending'");
                        if (ValInsert > 0)
                        {

                            CreatePayout createPayout = new CreatePayout();
                            createPayout.account_number = "3434913597520120";// dtBankID.Rows[0]["AccountNumber"].ToString();
                            createPayout.amount = Convert.ToInt32(Amount) * 100;
                            createPayout.currency = "INR";
                            createPayout.fund_account_id = fund_account_id;
                            createPayout.mode = "UPI";
                            createPayout.narration = "";
                            createPayout.purpose = "payout";
                            createPayout.queue_if_low_balance = false;
                            createPayout.reference_id = guid;
                            CreatePayoutNotes createPayoutNotes = new CreatePayoutNotes();
                            createPayout.notes = createPayoutNotes;
                            string result = ApiPostCallRazorpayx.PostCall(createPayout.GetJson(), "payouts");
                            JObject Data = JObject.Parse(result);
                            if (Data["error"] == null)
                            {
                                CreatePayoutResult createPayoutResult = JsonConvert.DeserializeObject<CreatePayoutResult>(result);


                                if (createPayoutResult.status.ToUpper() == "PROCESSED")
                                {
                                    SuccessMessage("Transaction Successfully Done");
                                }
                                else if (createPayoutResult.status.ToUpper() == "PROCESSING" || createPayoutResult.status.ToUpper() == "PENDING")
                                {
                                    SuccessMessage("Transaction in PROCESSING");
                                }
                                else if (createPayoutResult.status.ToUpper() == "PENDING")
                                {
                                    SuccessMessage("Transaction in PENDING");
                                }
                                objConnection.update_data("update SendToUPI set TransID='" + createPayoutResult.id + "',status='" + createPayoutResult.status + "' where reference_id='" + guid + "'");

                            }
                            else
                            {


                                objConnection.update_data("update SendToUPI set status='reversed' where reference_id='" + guid + "'");

                                ErrorMessage("Transaction FAILURE Successfully. Your Amount -" + (Convert.ToDecimal(Amount)) + " refund . Main wallet.");
                                Int32 CountDr = 0, CountCr = 0;
                                CountDr = objConnection.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where factor='dr'  and narration like '%" + guid + "%'");
                                if (CountDr == 1)
                                {
                                    CountCr = objConnection.select_data_scalar_int("select count(*) from TBL_EWALLETTRANSACTION where factor='Cr' and narration like '%" + guid + "%'");
                                    if (CountCr == 0)
                                    {
                                        objConnection.select_data_dt("exec [PROC_EWALLETTRANSACTION_CashDeposit]  '" + dtMember.Rows[0]["LoginID"].ToString() + "', " + TotalAmount + ", 'Cr','Refund-" + Narration + "','" + System.Guid.NewGuid().ToString() + "'");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        ErrorMessage("AMOUNT MUST BE LESS OR EQUAL TO YOUR WALLET BALANCE");
                    }
                }
                else
                {
                    ErrorMessage("UPI not register");

                }
            }
            else
            {
                ErrorMessage("Low Balance");

            }
        }
        else
        {
            ErrorMessage("Try after Some Time");

        }
    }

    private void upisendhistory(string UserID, string FromDate, string ToDate, string Page, string Size)
    {
        DataTable dt = new DataTable();
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

        dt = objConnection.select_data_dt("PROC_UPItoSendTransaction_Mobile ''," + Msrno + ",'" + FromDate + "','" + ToDate + "'," + Page + "," + Size + "");
        if (dt.Rows.Count > 0)
        {
            Response.Write(ConvertDataTabletoString(dt));
        }
        else
        {
            ErrorMessage("Not Available");
        }
    }
    private void moneytransferhsitory(string UserID, string FromDate, string ToDate, string Page, string Size)
    {
        DataTable dt = new DataTable();
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

        dt = objConnection.select_data_dt("PROC_MoneyTransferTransaction_Mobile ''," + Msrno + ",'" + FromDate + "','" + ToDate + "'," + Page + "," + Size + "");
        if (dt.Rows.Count > 0)
        {
            Response.Write(ConvertDataTabletoString(dt));
        }
        else
        {
            ErrorMessage("Not Available");
        }
    }
    private void rnfihistory(string UserID, string FromDate, string ToDate, string Page, string Size)
    {
        DataTable dt = new DataTable();
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

        dt = objConnection.select_data_dt("PROC_RNFIMATMTransaction_Mobile ''," + Msrno + ",'" + FromDate + "','" + ToDate + "'," + Page + "," + Size + "");
        if (dt.Rows.Count > 0)
        {
            Response.Write(ConvertDataTabletoString(dt));
        }
        else
        {
            ErrorMessage("Not Available");
        }
    }

    private void mahamatmhistory(string UserID, string FromDate, string ToDate, string Page, string Size)
    {
        DataTable dt = new DataTable();
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

        dt = objConnection.select_data_dt("PROC_MahagramMATMTransaction_Mobile ''," + Msrno + ",'" + FromDate + "','" + ToDate + "'," + Page + "," + Size + "");
        if (dt.Rows.Count > 0)
        {
            Response.Write(ConvertDataTabletoString(dt));
        }
        else
        {
            ErrorMessage("Not Available");
        }
    }

    private void cashdeposithistory(string UserID, string FromDate, string ToDate, string Page, string Size)
    {
        DataTable dt = new DataTable();
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

        dt = objConnection.select_data_dt("PROC_CashDepositTransaction_Mobile ''," + Msrno + ",'" + FromDate + "','" + ToDate + "'," + Page + "," + Size + "");
        if (dt.Rows.Count > 0)
        {
            Response.Write(ConvertDataTabletoString(dt));
        }
        else
        {
            ErrorMessage("Not Available");
        }
    }

    private void ewallethistory(string UserID, string FromDate, string ToDate, string Page, string Size)
    {
        DataTable dt = new DataTable();
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

        dt = objConnection.select_data_dt("PROC_EwalletTransaction_Mobile ''," + Msrno + ",'" + FromDate + "','" + ToDate + "'," + Page + "," + Size + "");
        if (dt.Rows.Count > 0)
        {
            Response.Write(ConvertDataTabletoString(dt));
        }
        else
        {
            ErrorMessage("Not Available");
        }
    }

    private void aepswallethistory(string UserID, string FromDate, string ToDate, string Page, string Size)
    {
        DataTable dt = new DataTable();
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");

        dt = objConnection.select_data_dt("PROC_AEPSwalletTransaction_Mobile ''," + Msrno + ",'" + FromDate + "','" + ToDate + "'," + Page + "," + Size + "");
        if (dt.Rows.Count > 0)
        {
            Response.Write(ConvertDataTabletoString(dt));
        }
        else
        {
            ErrorMessage("Not Available");
        }
    }

    private void Addcomplain(string UserID, string Message, string ServiceID)
    {
        ComplainTicket complainTicket = new ComplainTicket();

        complainTicket.ID = 0;
        complainTicket.ServiceID = Convert.ToInt32(ServiceID);
        complainTicket.Message = Message;
        Int32 Msrno = objConnection.select_data_scalar_int("select msrno from member where mobile='" + UserID + "'");


        if (complainTicket.IsAnyNullOrEmpty(complainTicket))
        {

            int Val = objConnection.update_data("EXEC Proc_AddEditComplainTicket " + complainTicket.ID + ",'" + complainTicket.ServiceID + "'," + Msrno + ",'" + complainTicket.Message + "'");
            if (Val > 0)
            {
                SuccessMessage("Complain Accept Successfully");

            }
            else
            {
                ErrorMessage("Record Not Inserted, Try Again ..");
            }
        }
        else
        {

        }
    }
    public string ConvertDataTabletoString1(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows).ToString();
    }

    private void Getstate()
    {
        //AEPSBankBodyRequest aEPSBankBodyRequest = new AEPSBankBodyRequest();
        dt = objConnection.select_data_dt("select id,Name as statename,StateCode as stateId from state");
        // string bankList = aEPSBankBodyRequest.GetStatelist();

        Response.Write("{\"status\":true,\"response_code\":0,\"banklist\":" + ConvertDataTabletoString1(dt) + "}");
    }

    public void GetToken(string userID, string password, string Token)
    {
        try
        {
            Int32 Count = objConnection.update_data("update member set AppToken='" + Token + "' where mobile='" + userID + "'");

            if (Count > 0)
            {
                SuccessMessage("Saved Successfully");
            }
            else
            {
                ErrorMessage("Un-Successfully");
            }

        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    public void sendpayrequest(string userID, string UPIID, string Amount)
    {
        try
        {
            DataTable dtMember = objConnection.select_data_dt("select * from Member where mobile='" + userID + "'");
            if (!string.IsNullOrEmpty(UPIID) && !string.IsNullOrEmpty(Amount))
            {
                string RequestID = System.Guid.NewGuid().ToString().Replace("-", "0").Substring(0, 22);
                CollectPayRequest collectPayRequest = new CollectPayRequest();
                collectPayRequest.payerVa = UPIID;
                collectPayRequest.amount = Amount + ".00";
                collectPayRequest.subMerchantId = dtMember.Rows[0]["Mobile"].ToString();
                collectPayRequest.subMerchantName = dtMember.Rows[0]["Name"].ToString();
                collectPayRequest.merchantTranId = RequestID;
                collectPayRequest.billNumber = RequestID;//31/01/2021 10:00 AM
                collectPayRequest.collectByDate = System.DateTime.Now.AddMinutes(2).ToString("dd-MM-yyyy hh:mm tt").Replace("-", "/");
                Int32 Val = 0;
                Val = objConnection.update_data("AddEdit_UPIICICIRequestInitiated " + dtMember.Rows[0]["Msrno"].ToString() + ",'" + collectPayRequest.amount + "',0,'" + collectPayRequest.merchantTranId + "','" + collectPayRequest.billNumber + "','" + collectPayRequest.collectByDate + "','" + collectPayRequest.payerVa + "','','Initiated'");
                if (Val > 0)
                {
                    string Result = AccountStatement.UPI(collectPayRequest);

                    JObject Data = JObject.Parse(Result);
                    if (Data["success"].ToString().ToUpper() == "TRUE")
                    {
                        SuccessMessage("Transaction initiated, Request Send on " + UPIID + "");

                    }
                    else
                    {
                        ErrorMessage(Data["message"].ToString());
                    }
                }
            }
            else
            {
                ErrorMessage("Fill Info");

            }

        }
        catch
        {
            //ErrorMessage("Technical Error");
        }
    }

    private string CallPlanAPI(string URL)
    {
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

        var client = new RestClient(URL);
        client.Timeout = -1;
        var request = new RestRequest(Method.GET);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Cookie", "ASP.NET_SessionId=0foxho2jkc1qoxmakntej1hx");
        IRestResponse response = client.Execute(request);
        return response.Content;
    }

}



public class MATM_Request
{
    public string partnerId { get; set; }
    public string apiKey { get; set; }
    public string referenceNumber { get; set; }
    public string subMerchantId { get; set; }
    public string merchantCode { get; set; }


    public string GetJson()
    {
        MATM_Request ObjData = new MATM_Request();

        //ObjData.partnerId = PaysPrint.PARTNERID;
        //ObjData.apiKey = PaysPrint.JWTencryptionKey;
        //ObjData.referenceNumber = this.referenceNumber;
        //ObjData.subMerchantId = this.subMerchantId;
        //ObjData.merchantCode = this.merchantCode;
        string jsonString;
        jsonString = JsonConvert.SerializeObject(ObjData);
        return jsonString;
    }
}

