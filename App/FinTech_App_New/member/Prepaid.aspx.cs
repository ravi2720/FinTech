using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;         // for GroupBy, Select, ToDictionary

public partial class Member_Prepaid : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();
    cls_myMember clsm = new cls_myMember();

    DataTable dtMemberMaster = new DataTable();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Service service = new Service();
    int Val = 0;
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            page = HttpContext.Current.CurrentHandler as Page;
            if (!IsPostBack)
            {
                txtMobileNo.Attributes.Add("@keyup", "GetPrepaidVal");
                txtAmount.Attributes.Add("@keyup", "GetCommission");
                ddlOperator.Attributes.Add("@change", "GetPlan");
                BindDropDown.FillDropDown(ddlOperator, "ActiveOperatorByService", "Name", "ID", "Select Operator", "1");
                Common.BindServiceList(rptServiceData, dtMember.Rows[0]["Msrno"].ToString(), ObjData);
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void btnRecharge_Click(object sender, EventArgs e)
    {
        try
        {
            dtMemberMaster = (DataTable)Session["dtMember"];
            DataTable dtWalletBalance = new DataTable();
            decimal MyBalance = 0;
            dtWalletBalance = ObjData.select_data_dt("select * from VIEW_MEMBERBALANCE_WITH_HOLD where Msrno=" + dtMemberMaster.Rows[0]["Msrno"] + "");

            if (dtWalletBalance.Rows.Count > 0)
            {
                MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["UsableBalance"]);
            }
            if (MyBalance >= Convert.ToDecimal(txtAmount.Text.Trim()))
            {
                string number = txtMobileNo.Value.Trim();
                string operatorId = ddlOperator.SelectedValue;
                string circleCode = hfCircleID.Value.Trim();

                decimal amount;
                if (!decimal.TryParse(txtAmount.Text.Trim(), out amount))
                {
                    ShowMessage("Invalid Amount.");
                    return;
                }

                if (string.IsNullOrEmpty(number) || string.IsNullOrEmpty(operatorId) || string.IsNullOrEmpty(circleCode))
                {
                    ShowMessage("Please enter all details.");
                    return;
                }

                if (amount < 10)
                {
                    ShowMessage("Minimum Recharge Amount is ₹10.");
                    return;
                }

                if (amount > 10000)
                {
                    ShowMessage("Maximum Recharge Amount is ₹10000.");
                    return;
                }

                if (number.Length != 10)
                {
                    ShowMessage("Please enter a valid 10-digit mobile number.");
                    return;
                }

                string externalRef = DateTime.Now.Ticks.ToString().Substring(0, 10);
                string macAddress = cls_myMember.GetMACAddress();
                string iPAddress = cls_myMember.GetIPAddress();
                string customerName = "";

                Location location = new Location();
                location = location.GetLocationDataCurrent(iPAddress);
                string latitude = location.Latitude;
                string longitude = location.Longitude;
                string subService = "1";

                string returnStr = "";
                var msr = dtMember.Rows[0]["Msrno"].ToString();
                DataTable dto = ObjData.select_data_dt($"SELECT OperatorCode FROM Operator WHERE ID='{operatorId}'");
                string operatorCode = dto.Rows.Count > 0 ? Convert.ToString(dto.Rows[0][0]) : "";

                DataTable DtUrl = ObjData.select_data_dt(
                    $"EXEC [ProcMLM_AddRecharge] 0, {msr}, '{number}', '', {amount}, {operatorId}, {circleCode}, '{externalRef}', '', '', 'Queued', '{iPAddress}', '{macAddress}', '{customerName}', '{number}', '{number}', 'Web', '', 0, 0, ''"
                );

                if (DtUrl.Rows.Count == 0)
                {
                    ShowMessage("❌ Error: No response from recharge procedure.");
                    return;
                }

                if (DtUrl.Rows[0]["Yes"].ToString() == "Yes")
                {
                    int historyId = Convert.ToInt32(DtUrl.Rows[0]["Historyid"]);
                    DataTable dtValc = ObjData.select_data_dt($"SELECT * FROM aepskyc WHERE Msrno='{msr}'");

                    var request = new { number, amount = txtAmount.Text, operatorCode, circleCode, externalRef, latitude, longitude, subService };
                    string regJson = JsonConvert.SerializeObject(request);
                    JObject data = JObject.Parse(NWSPay.Recharge(regJson, dtValc.Rows[0]["OutLetID"].ToString()));
                    string status = data["statusCode"]?.ToString();

                    if (data["statusCode"]?.ToString() == "TXN")
                    {
                        DataTable i = ObjData.select_data_dt($"ProcRecharge_UpdateHistory 'UpdateStatus',{historyId},{msr},0,0,0,0,'','','Success','{externalRef}','','{externalRef}'");
                        returnStr = "Recharge Successful !!," + historyId.ToString() + "," + "" + "," + externalRef + "," + externalRef + ",";
                        lblMessage.Text = returnStr;
                        string dbTransId = DtUrl.Rows[0]["Transid"].ToString(); // this is the one from tblRecharge_History
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Recharge Successful !!'); location.replace('BillPayReceipt.aspx?TransID=" + dbTransId + "')", true);
                    }
                    else if (data["statusCode"]?.ToString() == "ERR")
                    {
                        DataTable i = ObjData.select_data_dt($"ProcRecharge_UpdateHistory 'UpdateStatus',{historyId},0,0,0,0,0,'','', 'Failed', '{externalRef}','','{externalRef}'");

                        string memberID = ObjData.select_data_scalar_string($"SELECT loginid FROM member WHERE msrno={msr}");
                        string narration = ObjData.select_data_scalar_string($"SELECT Narration FROM TBL_EWALLETTRANSACTION WHERE TransactionID='{externalRef}'");
                        ObjData.update_data($"PROC_EWALLETTRANSACTION '{memberID}',{amount},'CR','Refund {narration}','','{ConstantsData.SRecharge}','{externalRef}'");

                        returnStr = "Recharge Failed !!," + historyId.ToString() + "," + "" + "," + externalRef + "," + externalRef + ",";
                        lblMessage.Text = returnStr;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Recharge Failed !');responsiveVoice.speak('आपका " + txtAmount.Text + " रुपये का रिचार्ज लो बैलेंस की वीजे से फेल हुआ', 'Hindi Female');", true);
                    }
                    else
                    {
                        DataTable i = ObjData.select_data_dt($"ProcRecharge_UpdateHistory 'UpdateStatus',{historyId},0,0,0,0,0,'','', 'Pending','{externalRef}','','{externalRef}'");

                        returnStr = "Request Submited !!," + historyId.ToString() + "," + "" + "," + externalRef + "," + externalRef + ",";
                        lblMessage.Text = returnStr;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Recharge Pending !');responsiveVoice.speak('आपका " + txtAmount.Text + " रुपये का रिचार्ज प्रतीक्षा में है', 'Hindi Female');", true);

                    }
                }
                else
                {
                    string errorMsg = DtUrl.Rows[0]["No"].ToString();
                    ShowMessage($"❌ Error: {errorMsg}");
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
        catch (Exception ex)
        {
            ShowMessage("❌ Error: " + ex.Message);
        }
    }

    private void ShowMessage(string message, bool success = false, string externalRef = "")
    {
        string script = success
            ? $"alert('{message}'); location.replace('BillPayReceipt.aspx?TransID={externalRef}');"
            : $"alert('{message}');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), script, true);
    }

    protected void btnRoffer_Click(object sender, EventArgs e)
    {
        string opt = ObjData.select_data_scalar_string("select OperatorCode from tblRecharge_OperatorCode where OperatorID=" + ddlOperator.SelectedValue + " and APIID=3");
        string Result = CallPlanAPI("https://sonitechno.in/api/PlanAPI.aspx?MethodName=Roffer&opt=" + opt + "&Number=" + txtMobileNo.Value + "");
        JObject Data = JObject.Parse(Result);
        if (Data["status"].ToString() == "1")
        {
            dt = (DataTable)JsonConvert.DeserializeObject(Data["records"].ToString(), (typeof(DataTable)));
        }
        else
        {
            ErrorShow.AlertMessage(page, Data["records"]["msg"].ToString(), ConstantsData.CompanyName);
        }
    }

    [WebMethod]
    public static string GetCommission(string Amt, string Op)
    {
        cls_connection cls = new cls_connection();
        DataTable dtm = (DataTable)HttpContext.Current.Session["dtMember"];
        string com = cls.select_data_scalar_string("select dbo.GetRechargeCommission(" + Amt + "," + dtm.Rows[0]["MSrno"].ToString() + "," + Op + ")");
        return string.IsNullOrEmpty(com) ? "0" : com;
    }

    [WebMethod]
    public static string GetPrepaid(string searchTerm)
    {
        try
        {
            // Create new connection because static method cannot use instance field
            cls_connection ObjData = new cls_connection();

            // Get current member from Session
            DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];

            if (dtMember == null || dtMember.Rows.Count == 0)
            {
                return JsonConvert.SerializeObject(new
                {
                    status = "ERR",
                    message = "Session expired. Please login again."
                });
            }

            // Fetch AEPS KYC for member
            var msr = dtMember.Rows[0]["Msrno"].ToString();
            DataTable dtValc = ObjData.select_data_dt(
                "SELECT * FROM aepskyc WHERE Msrno='" + dtMember.Rows[0]["Msrno"].ToString() + "'");

            if (dtValc.Rows.Count == 0)
            {
                return JsonConvert.SerializeObject(new
                {
                    status = "ERR",
                    message = "Outlet data not found for AEPS KYC."
                });
            }

            // Call API
            string result = NWSPay.GetMobileInfo(dtValc.Rows[0]["OutLetID"].ToString(), searchTerm);

            JObject Data = JObject.Parse(result);

            if (Data["statusCode"]?.ToString() == "TXN")
            {
                DataTable dto = ObjData.select_data_dt("select ID from Operator where OperatorCode='" + Data["data"]["operatorName"]?.ToString() + "'");

                string circleId = Data["data"]["circle"]?.ToString();
                string operatorId = Data["data"]["operatorName"]?.ToString();

                if (dto.Rows.Count > 0)
                {
                    operatorId = Convert.ToString(dto.Rows[0][0]);
                }

                return operatorId + "," + circleId;
            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    status = "FAIL",
                    message = Data["statusMessage"]?.ToString() ?? "Unable to fetch operator info."
                });
            }
        }
        catch (Exception ex)
        {
            return JsonConvert.SerializeObject(new
            {
                status = "ERR",
                message = ex.Message
            });
        }
    }

    [WebMethod]
    public static string GetPrePaidPlan(string operatorId, string circle)
    {
        try
        {
            cls_connection ObjData = new cls_connection();
            DataTable dtMember = (DataTable)HttpContext.Current.Session["dtMember"];

            if (dtMember == null || dtMember.Rows.Count == 0)
            {
                return JsonConvert.SerializeObject(new
                {
                    status = "ERR",
                    message = "Session expired. Please login again."
                });
            }

            var msr = dtMember.Rows[0]["Msrno"].ToString();
            DataTable dtValc = ObjData.select_data_dt("SELECT * FROM aepskyc WHERE Msrno='" + msr + "'");

            if (dtValc.Rows.Count == 0)
            {
                return JsonConvert.SerializeObject(new
                {
                    status = "ERR",
                    message = "Outlet data not found for AEPS KYC."
                });
            }

            DataTable dto = ObjData.select_data_dt("SELECT OperatorCode FROM Operator WHERE ID='" + operatorId + "'");
            string operatorCode = Convert.ToString(dto.Rows[0][0]);

            string result = NWSPay.GetRPlan(dtValc.Rows[0]["OutLetID"].ToString(), operatorCode, circle);
            JObject Data = JObject.Parse(result);

            if (Data["statusCode"]?.ToString() == "TXN")
            {
                var planList = Data["data"].ToObject<List<JObject>>();

                var groupedRecords = planList
                .GroupBy(p => p["type"]?.ToString() ?? "General")
                .Select(g => new
                {
                    planType = g.Key,
                    plans = g.Select(x => new
                    {
                        validity = x["validity"]?.ToString(),
                        desc = x["details"]?.ToString(),
                        rs = x["amount"]?.ToString()
                    }).ToList()
                }).ToList();

                return JsonConvert.SerializeObject(new
                {
                    status = "TXN",
                    records = groupedRecords
                });

            }
            else
            {
                return JsonConvert.SerializeObject(new
                {
                    status = "FAIL",
                    message = Data["Message"]?.ToString() ?? "Unable to fetch operator info."
                });
            }
        }
        catch (Exception ex)
        {
            return JsonConvert.SerializeObject(new
            {
                status = "ERR",
                message = ex.Message
            });
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