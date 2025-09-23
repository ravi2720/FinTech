using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_NewLook : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    cls_connection ObjConnection = new cls_connection();
    Page page;
    string ipAddress = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        ipAddress = ConstantsData.getipAddress();
        dt = (DataTable)Session["dtMember"];
        if (Session["dtMember"] != null)
        {
            if (!IsPostBack)
            {
                Int32 Valc = ObjConnection.select_data_scalar_int("select count(*) from Member where loginid='" + dt.Rows[0]["loginid"].ToString() + "' and ActivedForRnfi=0");
                if (Valc == 0)
                {
                    BankList bankList = new BankList();
                    string bankListstr = ApiPostCall.PostCall(bankList.GetJson(), SONITechnoCredentrial.AEPSCore);
                    JObject jData = JObject.Parse(bankListstr);
                    if (jData["code"].ToString().ToUpper() == "TXN")
                    {
                        DataTable dt1 = new DataTable();
                        dt1 = GetJSONToDataTableUsingNewtonSoftDll(jData["data"].ToString());
                        nationalbank.DataSource = dt1;
                        nationalbank.DataTextField = "bankName";
                        nationalbank.DataValueField = "iinno";
                        nationalbank.DataBind();
                    }
                }
                else
                {
                    ErrorShow.AlertMessageWithRedirect(page, "Your KYC Not Complete, Upload Your KYC", "AEPSOnboard.aspx", ConstantsData.CompanyName);
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
        if (btnBalance.Text == "Balance Enquiry")
        {
            if (!string.IsNullOrEmpty(formmobile.Text.Trim()) && !string.IsNullOrEmpty(aadhar_number.Text))
            {
                {
                    string RefID = NWSPay.GenerateExternalRef();
                    ipAddress = ConstantsData.getipAddress();
                    string aadhar = aadhar_number.Text.Substring(8, 4);
                    Int32 Val = ObjConnection.update_data("Exec Proc_Add_edit_AEPS " + dt.Rows[0]["msrno"].ToString() + ", '" + RefID + "','" + txtAmount.Text + "', '" + ddlType.SelectedValue + "', " + nationalbank.SelectedValue + ",'" + (("xxxx-xxxx-" + aadhar)) + "', '" + formmobile.Text + "', 'Pending','','" + hdnDevice.Value + "','','','Site','','','" + ipAddress + "'");
                    if (Val > 0)
                    {
                        BE bALANCEENQUIRY = new BE();
                        bALANCEENQUIRY.mobilenumber = formmobile.Text.Trim();
                        bALANCEENQUIRY.adhaarnumber = aadhar_number.Text.Replace("-", "").Trim();
                        bALANCEENQUIRY.BankID = nationalbank.SelectedValue;
                        bALANCEENQUIRY.data = HttpUtility.HtmlDecode(hdnCapture.Value);
                        bALANCEENQUIRY.OrderID = RefID;
                        bALANCEENQUIRY.submerchantid = SONITechnoCredentrial.Prefix + dt.Rows[0]["loginid"].ToString();
                        bALANCEENQUIRY.accessmodetype = "SITE";

                        string result = ApiPostCall.PostCall(bALANCEENQUIRY.GetJson(), SONITechnoCredentrial.AEPSCore);
                        JObject Data = JObject.Parse(result);
                        if (Data["code"].ToString().ToUpper() == "TXN")
                        {
                            if (Data["data"]["status"].ToString().ToUpper() == "TRUE")
                            {
                                imgSet.Src = "../img/RightMark.png";
                                Message.InnerHtml = "Balance Inquiry Success";
                                lblBankName.Text = nationalbank.SelectedItem.Text;
                                CusMobile.Text = bALANCEENQUIRY.mobilenumber;
                                lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                                lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                                BCName.Text = dt.Rows[0]["Name"].ToString();
                                lblTransID.Text = bALANCEENQUIRY.OrderID;
                                lblMark.Attributes.Add("class", "btn btn-success");
                                lblAmount.Text = Data["data"]["balanceamount"].ToString();
                                lblMark.Text = Data["data"]["message"].ToString();
                                lblBankNumber.Text = Data["data"]["bankrrn"].ToString();
                                lblAadharNo.Text = ("xxxx-xxxx-" + aadhar);
                                Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dt.Rows[0]["msrno"].ToString() + "','BE','Success','" + Data["data"]["message"].ToString() + "',0,'" + Data["data"]["bankrrn"].ToString() + "','','" + result + "'");
                                lblDate.Text = System.DateTime.Now.ToString();
                                hdnCapture.Value = "";
                                mul.ActiveViewIndex = 1;
                            }
                            else
                            {
                                imgSet.Src = "../img/Cross.png";
                                Message.InnerHtml = "Balance Inquiry failed";
                                lblBankName.Text = nationalbank.SelectedItem.Text;
                                CusMobile.Text = bALANCEENQUIRY.mobilenumber;
                                lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                                lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                                BCName.Text = dt.Rows[0]["Name"].ToString();
                                lblTransID.Text = bALANCEENQUIRY.OrderID;
                                lblMark.Attributes.Add("class", "btn btn-danger");
                                lblMark.Text = Data["data"]["message"].ToString();
                                lblBankNumber.Text = "";
                                lblDate.Text = System.DateTime.Now.ToString();
                                lblAadharNo.Text = ("xxxx-xxxx-" + aadhar);
                                Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dt.Rows[0]["msrno"].ToString() + "','BE','Failed','" + Data["data"]["message"].ToString() + "',0,'','','" + result + "'");
                                mul.ActiveViewIndex = 1;
                                hdnCapture.Value = "";
                            }
                        }
                    }
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Fill All Information", ConstantsData.CompanyName);
            }
        }
        else if (btnBalance.Text == "Mini StateMent")
        {
            if (!string.IsNullOrEmpty(formmobile.Text.Trim()) && !string.IsNullOrEmpty(aadhar_number.Text))
            {
                string aadhar = aadhar_number.Text.Substring(8, 4);
                string RefID = NWSPay.GenerateExternalRef();
                Int32 Val = ObjConnection.update_data("Exec Proc_Add_edit_AEPS " + dt.Rows[0]["msrno"].ToString() + ", '" + RefID + "','0', 'MS', " + nationalbank.SelectedValue + ",'" + (("xxxx-xxxx-" + aadhar)) + "', '" + formmobile.Text + "', 'Pending','','" + hdnDevice.Value + "','','','Site','','','" + ipAddress + "'");
                if (Val > 0)
                {
                    MSST miniStatement = new MSST();
                    miniStatement.mobilenumber = formmobile.Text.Trim();
                    miniStatement.adhaarnumber = aadhar_number.Text.Trim();
                    miniStatement.BankID = nationalbank.SelectedValue;
                    miniStatement.data = HttpUtility.HtmlDecode(hdnCapture.Value);
                    miniStatement.OrderID = RefID;
                    miniStatement.accessmodetype = "SITE";
                    miniStatement.submerchantid = SONITechnoCredentrial.Prefix + dt.Rows[0]["loginid"].ToString();
                    string result = ApiPostCall.PostCall(miniStatement.GetJson(), SONITechnoCredentrial.AEPSCore);
                    JObject Data = JObject.Parse(result);
                    if (Data["code"].ToString().ToUpper() == "TXN")
                    {
                        if (Data["data"]["status"].ToString().ToUpper() == "FALSE")
                        {
                            imgSet.Src = "../img/Cross.png";
                            Message.InnerHtml = "MiniStatement failed";
                            lblBankName.Text = nationalbank.SelectedItem.Text;
                            CusMobile.Text = miniStatement.mobilenumber;
                            lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                            lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                            BCName.Text = dt.Rows[0]["Name"].ToString();
                            lblTransID.Text = miniStatement.OrderID;
                            lblMark.Attributes.Add("class", "btn btn-danger");
                            lblMark.Text = Data["data"]["message"].ToString();
                            lblBankNumber.Text = "";
                            lblDate.Text = System.DateTime.Now.ToString();
                            lblAadharNo.Text = ("xxxx-xxxx-" + aadhar);
                            mul.ActiveViewIndex = 1;
                            Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dt.Rows[0]["msrno"].ToString() + "','MS','Failed','" + Data["data"]["message"].ToString() + "',0,'','','" + result + "'");

                            hdnCapture.Value = "";
                            rptMiniState.DataSource = null;
                            rptMiniState.DataBind();
                        }
                        else
                        {
                            imgSet.Src = "../img/RightMark.png";
                            Message.InnerHtml = "MiniStatement Success";
                            lblBankName.Text = nationalbank.SelectedItem.Text;
                            CusMobile.Text = miniStatement.mobilenumber;
                            lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                            lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                            BCName.Text = dt.Rows[0]["Name"].ToString();
                            lblTransID.Text = miniStatement.OrderID;
                            lblMark.Attributes.Add("class", "btn btn-success");
                            lblAmount.Text = Data["data"]["balanceamount"].ToString();
                            lblMark.Text = Data["data"]["message"].ToString();
                            lblBankNumber.Text = Data["data"]["bankrrn"].ToString();
                            lblAadharNo.Text = ("xxxx-xxxx-" + aadhar);
                            DataTable dt1 = new DataTable();
                            dt1 = GetJSONToDataTableUsingNewtonSoftDll(Data["data"]["ministatement"].ToString());
                            if (dt1.Rows.Count > 0)
                            {
                                rptMiniState.DataSource = dt1;
                                rptMiniState.DataBind();
                            }
                            else
                            {
                                rptMiniState.DataSource = null;
                                rptMiniState.DataBind();
                            }
                            mul.ActiveViewIndex = 1;
                            Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dt.Rows[0]["msrno"].ToString() + "','MS','Success','" + Data["data"]["message"].ToString() + "',0,'" + Data["data"]["bankrrn"].ToString() + "','','" + result + "'");

                            hdnCapture.Value = "";
                        }
                    }
                }
            }
        }
        else if (btnBalance.Text == "Cash Withdrwal")
        {
            if (!string.IsNullOrEmpty(formmobile.Text.Trim()) && !string.IsNullOrEmpty(aadhar_number.Text) && !string.IsNullOrEmpty(hdnCapture.Value))
            {
                {
                    string aadhar = aadhar_number.Text.Substring(8, 4);
                    ipAddress = ConstantsData.getipAddress();
                    string RefID = NWSPay.GenerateExternalRef();
                    Int32 Val = ObjConnection.update_data("Exec Proc_Add_edit_AEPS " + dt.Rows[0]["msrno"].ToString() + ", '" + RefID + "','" + txtAmount.Text + "', 'CW', " + nationalbank.SelectedValue + ",'" + (("xxxx-xxxx-" + aadhar)) + "', '" + formmobile.Text + "', 'Pending','','" + hdnDevice.Value + "','','','Site','','','" + ipAddress + "'");
                    if (Val > 0)
                    {
                        CW cashWithdraw = new CW();
                        cashWithdraw.mobilenumber = formmobile.Text.Trim();
                        cashWithdraw.adhaarnumber = aadhar_number.Text.Replace("-", "").Trim();
                        cashWithdraw.BankID = nationalbank.SelectedValue;
                        cashWithdraw.data = HttpUtility.HtmlDecode(hdnCapture.Value);
                        cashWithdraw.OrderID = RefID;
                        cashWithdraw.Amount = txtAmount.Text;
                        cashWithdraw.accessmodetype = "SITE";
                        cashWithdraw.submerchantid = SONITechnoCredentrial.Prefix + dt.Rows[0]["LoginID"].ToString();

                        string result = ApiPostCall.PostCall(cashWithdraw.GetJson(), SONITechnoCredentrial.AEPSCore);
                        JObject Data = JObject.Parse(result);
                        if (Data["code"].ToString().ToUpper() == "TXN")
                        {
                            if (Data["data"]["status"].ToString().ToUpper() == "TRUE")
                            {
                                imgSet.Src = "../img/RightMark.png";
                                Message.InnerHtml = "Cash Withdrawal Success";
                                lblBankName.Text = nationalbank.SelectedItem.Text;
                                CusMobile.Text = cashWithdraw.mobilenumber;
                                lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                                lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                                BCName.Text = dt.Rows[0]["Name"].ToString();
                                lblTransID.Text = cashWithdraw.OrderID;
                                lblMark.Attributes.Add("class", "btn btn-success");
                                lblAmount.Text = Data["data"]["balanceamount"].ToString();
                                lblMark.Text = Data["data"]["message"].ToString();
                                lblBankNumber.Text = Data["data"]["bankrrn"].ToString();
                                lblAadharNo.Text = ("xxxx-xxxx-" + aadhar);
                                lblAmountCW.Text = txtAmount.Text;
                                Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dt.Rows[0]["msrno"].ToString() + "','CW','Success','" + Data["data"]["message"].ToString() + "'," + txtAmount.Text + ",'" + Data["data"]["bankrrn"].ToString() + "','" + Data["data"]["ackno"].ToString() + "','" + result + "'");
                                try
                                {
                                    string narration = "", Decs = "";
                                    narration = "Aeps TransID-" + RefID;
                                    Decs = $"AEPS Amount - {cashWithdraw.Amount}. Done With Aadhar No {("xxxx-xxxx-" + aadhar)}. TransID - {RefID}";
                                    ObjConnection.update_data("exec PROC_AEPSWALLETTRANSACTION '" + dt.Rows[0]["LoginID"].ToString() + "','" + cashWithdraw.Amount + "','CR','" + narration + "','" + Decs + "','" + ConstantsData.SAeps + "','" + RefID + "'");
                                    ObjConnection.update_data("Exec Proc_AEPS_DistributeCommission '" + dt.Rows[0]["msrno"].ToString() + "'," + cashWithdraw.Amount + " , '" + RefID + "', '" + ConstantsData._TDS + "','" + ConstantsData.SAEPSCommission + "','" + ConstantsData._TDSVal + "'");
                                }
                                catch (Exception ex)
                                {

                                }
                                mul.ActiveViewIndex = 1;
                                hdnCapture.Value = "";
                            }
                            else
                            {
                                imgSet.Src = "../img/Cross.png";
                                Message.InnerHtml = "Cash Withdrawal failed";
                                lblBankName.Text = nationalbank.SelectedItem.Text;
                                CusMobile.Text = cashWithdraw.mobilenumber;
                                lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                                lblBcCode.Text = dt.Rows[0]["LoginID"].ToString();
                                BCName.Text = dt.Rows[0]["Name"].ToString();
                                lblTransID.Text = cashWithdraw.OrderID;
                                lblMark.Attributes.Add("class", "btn btn-danger");
                                lblMark.Text = Data["data"]["message"].ToString();
                                lblBankNumber.Text = "";
                                lblAadharNo.Text = ("xxxx-xxxx-" + aadhar);
                                Val = ObjConnection.update_data("proc_Credit_AEPS_History '" + RefID + "','" + dt.Rows[0]["msrno"].ToString() + "','CW','Failed','" + Data["data"]["message"].ToString() + "',0,'','','" + result + "'");
                                mul.ActiveViewIndex = 1;
                                hdnCapture.Value = "";
                            }
                        }
                    }
                }

            }
            else
            {
                ErrorShow.AlertMessage(page, "Fill All Information", ConstantsData.CompanyName);
            }
        }
    }
    protected void btnexportPdf_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('Print.aspx','PrintMe','height=500px,width=650px,scrollbars=1');</script>");
    }

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlType.SelectedValue == "CW")
        {
            btnBalance.Text = "Cash Withdrwal";
            divamount.Visible = true;
        }
        else if (ddlType.SelectedValue == "MS")
        {
            btnBalance.Text = "Mini StateMent";
            divamount.Visible = false;
        }
        else if (ddlType.SelectedValue == "BE")
        {
            btnBalance.Text = "Balance Enquiry";
            divamount.Visible = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "$('#datascan').removeAttr('disabled');", true);
    }

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        mul.ActiveViewIndex = 0;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "$('#datascan').removeAttr('disabled');", true);


    }
}