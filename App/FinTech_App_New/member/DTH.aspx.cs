using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_DTH : System.Web.UI.Page
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
                txtNumber.Attributes.Add("onkeyup", "GetPrepaidVal()");
                ddlOperator.Attributes.Add("onchange", "GetPlan()");

                // Operator dropdown (ServiceID = 3 → Prepaid)
                BindDropDown.FillDropDown(
                    ddlOperator,
                    "ActiveOperatorByService",
                    "Name",
                    "ID",
                    "Select Operator",
                    "3"
                );

                // Circle dropdown
                BindCircleDropdown();

                Common.BindServiceList(
                    rptServiceData,
                    dtMember.Rows[0]["Msrno"].ToString(),
                    ObjData
                );
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void BindCircleDropdown()
    {
        var circles = new List<dynamic>
    {
        new { Name = "Andhra Pradesh", Code = "1" },
        new { Name = "Assam", Code = "2" },
        new { Name = "Bihar & Jharkhand", Code = "3" },
        new { Name = "Chennai", Code = "4" },
        new { Name = "Delhi", Code = "5" },
        new { Name = "Goa", Code = "26" },
        new { Name = "Gujarat", Code = "6" },
        new { Name = "Haryana", Code = "7" },
        new { Name = "Himachal Pradesh", Code = "8" },
        new { Name = "Jammu & Kashmir", Code = "9" },
        new { Name = "Karnataka", Code = "10" },
        new { Name = "Kerala", Code = "11" },
        new { Name = "Kolkata", Code = "12" },
        new { Name = "Madhya Pradesh & Chattisgarh", Code = "14" },
        new { Name = "Maharashtra", Code = "13" },
        new { Name = "Mumbai", Code = "15" },
        new { Name = "North East", Code = "16" },
        new { Name = "Orissa", Code = "17" },
        new { Name = "Punjab", Code = "18" },
        new { Name = "Rajasthan", Code = "19" },
        new { Name = "Tamil Nadu", Code = "20" },
        new { Name = "Uttar Pradesh (E)", Code = "21" },
        new { Name = "Uttar Pradesh (W)", Code = "22" },
        new { Name = "West Bengal", Code = "23" }
    };

        ddlCircle.DataSource = circles;
        ddlCircle.DataTextField = "Name";
        ddlCircle.DataValueField = "Code";
        ddlCircle.DataBind();

        ddlCircle.Items.Insert(0, new ListItem("Select Circle", ""));
    }

    protected void btnRecharge_Click(object sender, EventArgs e)
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
            if (string.IsNullOrEmpty(txtNumber.Text) ||
            string.IsNullOrEmpty(txtAmount.Text) ||
            string.IsNullOrEmpty(ddlOperator.SelectedValue) ||
            string.IsNullOrEmpty(ddlCircle.SelectedValue))
            {
                lblMessage.Text = "Please enter all details.";
                return;
            }
            if (Convert.ToDecimal(txtAmount.Text) < 10)
            {
                lblMessage.Text = "Minimum Recharge Amount is ₹10.";
                return;
            }
            if (Convert.ToDecimal(txtAmount.Text) > 10000)
            {
                lblMessage.Text = "Maximum Recharge Amount is ₹10000.";
                return;
            }

            try
            {
                string number = txtNumber.Text.Trim();
                string operatorId = ddlOperator.SelectedValue;
                decimal amount = Convert.ToDecimal(txtAmount.Text.Trim());
                string circleCode = ddlCircle.SelectedValue;
                string externalRef = DateTime.Now.Ticks.ToString().Substring(0, 10);
                string macAddress = cls_myMember.GetMACAddress();

                DataTable dto = ObjData.select_data_dt("SELECT OperatorCode FROM Operator WHERE ID='" + operatorId + "'");
                string operatorCode = Convert.ToString(dto.Rows[0][0]);

                string iPAddress = cls_myMember.GetIPAddress();
                Location location = new Location();
                location = location.GetLocationDataCurrent(iPAddress);
                string latitude = location.Latitude;
                string longitude = location.Longitude;
                string subService = "2";

                string customerName = "";

                var msr = dtMember.Rows[0]["Msrno"].ToString();

                DataTable DtUrl = new DataTable();

                DtUrl = ObjData.select_data_dt("EXEC [ProcMLM_AddRecharge] 0 ," + msr + ",'" + number + "','" + string.Empty + "'," + amount + "," + operatorId + "," + circleCode + ",'" + externalRef + "' ,'" + string.Empty + "','" + string.Empty + "','Queued','" + iPAddress + "','" + macAddress + "','" + customerName + "','" + number + "','" + number + "', '" + "Web" + "','" + string.Empty + "'," + 0 + "," + 0 + ", '" + string.Empty + "'");

                string returnStr = "";
                int historyId = 0;

                if (DtUrl.Rows[0]["Yes"].ToString() == "Yes")
                {
                    historyId = Convert.ToInt32(DtUrl.Rows[0]["Historyid"].ToString());
                    DataTable dtValc = ObjData.select_data_dt("SELECT * FROM aepskyc WHERE Msrno='" + msr + "'");

                    var request = new { number, amount = txtAmount.Text, operatorCode, circleCode, externalRef, latitude, longitude, subService };
                    string regJson = JsonConvert.SerializeObject(request);
                    JObject data = JObject.Parse(NWSPay.Recharge(regJson, dtValc.Rows[0]["OutLetID"].ToString()));

                    if (data["statusCode"]?.ToString() == "TXN")
                    {
                        DataTable i = ObjData.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + historyId + "," + Convert.ToInt32(msr) + ", 0, 0, 0, 0, '', '', 'Success','" + externalRef + "', '','" + externalRef + "'");
                        returnStr = "Recharge Successful !!," + historyId.ToString() + "," + "" + "," + externalRef + "," + externalRef + ",";
                        lblMessage.Text = returnStr;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Recharge Successful !!'); location.replace('BillPayReceipt.aspx?TransID=" + externalRef + "')", true);
                    }
                    else if (data["statusCode"]?.ToString() == "ERR")
                    {
                        try
                        {
                            DataTable i = ObjData.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatus'," + historyId + ", 0, 0, 0, 0, 0, '','', 'Failed', '" + externalRef + "','" + "" + "','" + externalRef + "'");
                        }
                        catch (Exception ex) { }
                        string MemberID = ObjData.select_data_scalar_string("select loginid from member where msrno=" + msr + "");
                        string Narration = ObjData.select_data_scalar_string("select Narration from TBL_EWALLETTRANSACTION where TransactionID='" + externalRef + "'");
                        ObjData.update_data("PROC_EWALLETTRANSACTION '" + MemberID + "'," + amount + ",'CR','" + ("Refund " + Narration) + "',''," + ConstantsData.SRecharge + ",'" + externalRef + "'");
                        returnStr = "Recharge Failed !!," + historyId.ToString() + "," + "" + "," + externalRef + "," + externalRef + ",";
                        lblMessage.Text = returnStr;

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Recharge Failed !');responsiveVoice.speak('आपका " + txtAmount.Text + " रुपये का रिचार्ज लो बैलेंस की वीजे से फेल हुआ', 'Hindi Female');", true);
                    }
                    else
                    {
                        DataTable i = ObjData.select_data_dt("ProcRecharge_UpdateHistory 'UpdateStatu'," + historyId + ", 0, 0, 0, 0, 0, '', '','Pending','" + externalRef + "','" + "" + "','" + externalRef + "'");

                        returnStr = "Request Submited !!," + historyId.ToString() + "," + "" + "," + externalRef + "," + externalRef + ",";
                        lblMessage.Text = returnStr;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "success", "alert('Recharge Pending !');responsiveVoice.speak('आपका " + txtAmount.Text + " रुपये का रिचार्ज प्रतीक्षा में है', 'Hindi Female');", true);
                    }
                }
                else
                {
                    returnStr = "❌ Error: " + DtUrl.Rows[0]["No"].ToString();
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = returnStr;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "❌ Error: " + ex.Message;
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
}