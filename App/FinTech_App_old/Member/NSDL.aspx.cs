using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_NSDL : System.Web.UI.Page
{
    Page page;
    cls_connection cls = new cls_connection();
    DataTable dtMember;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
        }
        else
        {
            Response.Redirect("default.aspx");
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtEmailID.Text) && !string.IsNullOrEmpty(txtMobile.Text)  && !string.IsNullOrEmpty(txtLastName.Text) && dllTitle.SelectedIndex>0)
        {
            Int32 HoldCount = cls.select_data_scalar_int("select count(1) from Member where Msrno=" + dtMember.Rows[0]["msrno"].ToString() + " and OnHold=1");
            if (HoldCount == 0)
            {
                decimal MyBalance = 0;
                DataTable dtWalletBalance = new DataTable();
                dtWalletBalance = cls.select_data_dt("select * from VIEW_MEMBERBALANCE where Msrno=" + dtMember.Rows[0]["Msrno"] + "");
                if (dtWalletBalance.Rows.Count > 0)
                {
                    MyBalance = Convert.ToDecimal(dtWalletBalance.Rows[0]["Balance"]);
                }
                string agentID = System.Guid.NewGuid().ToString().Replace('-', 'P').Substring(1, 10);

                decimal deductAmount = cls.select_data_scalar_decimal("select isnull(Surcharge,0) from NSDLPanSurcharge where Name='" + rdolist.SelectedValue + "' and PackageID='" + dtMember.Rows[0]["PackageID"].ToString() + "'");
                string Dec = rdolist.SelectedItem.Text + " Pan Card Token Active with Amount-" + deductAmount + " TransID-" + agentID;
                string narration = "Pan TransID-" + agentID;
                if (MyBalance > deductAmount)
                {
                    DataTable dtval = cls.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"].ToString() + "','" + deductAmount + "','DR','" + narration + "','" + Dec + "'," + ConstantsData.SPanCard + ",'" + agentID + "'");
                    if (dtval.Rows.Count > 0)
                    {
                        if (dtval.Rows[0]["msrno"].ToString() == "1")
                        {
                            NSDLURL paysCMSURLMake = new NSDLURL();
                            paysCMSURLMake.title = dllTitle.SelectedValue;
                            paysCMSURLMake.firstname = txtFisrtName.Text;
                            paysCMSURLMake.mobileno = txtMobile.Text;
                            paysCMSURLMake.lastname = txtLastName.Text;
                            paysCMSURLMake.middlename = txtMName.Text;
                            paysCMSURLMake.email = txtEmailID.Text;
                            paysCMSURLMake.PanType = rdolist.SelectedValue;
                            paysCMSURLMake.OrderID = agentID;
                            paysCMSURLMake.MethodName = "urlmake";
                            string request1 = paysCMSURLMake.GetJson();
                            Int32 Val = 0;
                            Val = cls.update_data("exec AddEditNSDLPan '" + dtMember.Rows[0]["Msrno"].ToString() + "','" + rdolist.SelectedValue + "','" + agentID + "','','','" + deductAmount + "','" + deductAmount + "',0,'Pending','" + request1 + "',''");
                            if (Val > 0)
                            {
                                string Result = ApiPostCall.PostCall(paysCMSURLMake.GetJson(), SONITechnoCredentrial.NSDLPan);
                                JObject jData = JObject.Parse(Result);
                                if (jData["code"].ToString().ToUpper() != "ERR")
                                {
                                    if (jData["data"]["status"].ToString().ToUpper() == "TRUE")
                                    {
                                        lblMessage.Text = jData["data"]["message"].ToString();
                                        NSDLVerification nSDLVerification = new NSDLVerification();
                                        nSDLVerification.encdata=jData["data"]["encdata"].ToString();
                                        nSDLVerification.OrderID = agentID;
                                        nSDLVerification.MethodName = "validateurl";
                                        string ResultNew = ApiPostCall.PostCall(nSDLVerification.GetJson(), SONITechnoCredentrial.NSDLPan);
                                        JObject jData1 = JObject.Parse(ResultNew);
                                        if (jData1["code"].ToString().ToUpper() != "ERR")
                                        {
                                            if (jData1["data"]["status"].ToString().ToUpper() == "TRUE")
                                            {
                                                cls.update_data("update NSDLPan set NPID='" + jData1["data"]["nep_id"].ToString() + "', status='Pending',Result='" + ResultNew + "' where Orderid='" + agentID + "'");
                                                ErrorShow.AlertMessageWithRedirect(page, jData1["data"]["message"].ToString(), "NSDL.html?encdata=" + jData1["data"]["encdata"].ToString() + "", ConstantsData.CompanyName);
                                            }
                                            else
                                            {
                                                dtval = cls.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"].ToString() + "','" + deductAmount + "','cr','Refund-" + narration + "','" + Dec + "'," + ConstantsData.SPanCard + ",'" + agentID + "'");
                                                if (dtval.Rows[0]["msrno"].ToString() == "1")
                                                {
                                                    lblMessage.Text = jData1["data"]["message"].ToString();
                                                    cls.update_data("update NSDLPan set status='Failed',Result='" + ResultNew + "' where Orderid='" + agentID + "'");
                                                    ErrorShow.AlertMessage(page, jData1["data"]["message"].ToString(), ConstantsData.CompanyName);
                                                }

                                            }
                                        }
                                        else
                                        {
                                            dtval = cls.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"].ToString() + "','" + deductAmount + "','cr','Refund-" + narration + "','" + Dec + "'," + ConstantsData.SPanCard + ",'" + agentID + "'");
                                            if (dtval.Rows[0]["msrno"].ToString() == "1")
                                            {
                                                cls.update_data("update NSDLPan set status='Failed',Result='" + Result + "' where Orderid='" + agentID + "'");
                                                lblMessage.Text = jData1["mess"].ToString();

                                                ErrorShow.AlertMessage(page, jData1["mess"].ToString(), ConstantsData.CompanyName);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dtval = cls.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"].ToString() + "','" + deductAmount + "','cr','Refund-" + narration + "','" + Dec + "'," + ConstantsData.SPanCard + ",'" + agentID + "'");
                                        if (dtval.Rows[0]["msrno"].ToString() == "1")
                                        {
                                            cls.update_data("update NSDLPan set status='Failed',Result='" + Result + "' where Orderid='" + agentID + "'");
                                            lblMessage.Text = jData["data"]["message"].ToString();

                                            ErrorShow.AlertMessage(page, jData["message"].ToString(), ConstantsData.CompanyName);
                                        }

                                    }
                                }
                                else
                                {
                                    dtval = cls.select_data_dt("exec PROC_EWALLETTRANSACTION '" + dtMember.Rows[0]["loginid"].ToString() + "','" + deductAmount + "','cr','Refund-" + narration + "','" + Dec + "'," + ConstantsData.SPanCard + ",'" + agentID + "'");
                                    if (dtval.Rows[0]["msrno"].ToString() == "1")
                                    {
                                        cls.update_data("update NSDLPan set status='Failed',Result='" + Result + "' where Orderid='" + agentID + "'");
                                        lblMessage.Text = jData["mess"].ToString();

                                        ErrorShow.AlertMessage(page, jData["mess"].ToString(), ConstantsData.CompanyName);
                                    }
                                }
                            }

                        }
                        else
                        {
                            ErrorShow.AlertMessage(page, "Low Balance", ConstantsData.CompanyName);
                        }
                    }
                    else
                    {
                        ErrorShow.AlertMessage(page, "Low Balance", ConstantsData.CompanyName);
                    }

                }
                else
                {
                    ErrorShow.AlertMessage(page, "Low Balance", ConstantsData.CompanyName);
                }
            }
            else
            {
                ErrorShow.AlertMessage(page, "Your Wallet Cross Limit", ConstantsData.CompanyName);
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Fill All Value", ConstantsData.CompanyName);

        }
    }
}