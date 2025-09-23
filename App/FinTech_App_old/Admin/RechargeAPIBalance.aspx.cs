using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
public partial class Admin_RechargeAPIBalance : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();
    DataTable dt = new DataTable();
    Page page;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillAPI();
        }
    }

    private void fillAPI()
    {
        DataTable dt = ObjData.select_data_dt("ProcRecharge_ManageAPI 'Get', 0");
        //dt = cls.select_data_dt("Select * from tblRecharge_API_POSTXMLCSV");
        gvAPI.DataSource = dt;
        gvAPI.DataBind();
        gvAPI.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    public string apicall(string url)
    {
        HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
        httpreq.Timeout = 30000;
        try
        {
            HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
            StreamReader sr = new StreamReader(httpres.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();
            return results;
        }
        catch
        {
            return "0";
        }
    }
    protected void gvAPI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            //DataRowView drview = e.Row.DataItem as DataRowView;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Literal litAPIID = (Literal)e.Row.FindControl("litAPIID");
                Literal litBalanceURL = (Literal)e.Row.FindControl("litBalanceURL");

                DataTable dtAPI = new DataTable();
                dtAPI = ObjData.select_data_dt("ProcRecharge_ManageAPI 'GetAll',  " + Convert.ToInt32(litAPIID.Text) + "");
                //dtAPI = cls.select_data_dt("Select * from tblRecharge_API_POSTXMLCSV where apiid=" + litAPIID.Text + "");
                if (dtAPI.Rows[0]["BalanceURL"].ToString() != "")
                {
                    string strBalanceAPI = "";
                    if (dtAPI.Rows[0]["B_prm1"].ToString() != "" && dtAPI.Rows[0]["B_prm1val"].ToString() != "")
                    {
                        strBalanceAPI = dtAPI.Rows[0]["BalanceURL"].ToString() + dtAPI.Rows[0]["B_prm1"].ToString() + "=" + dtAPI.Rows[0]["B_prm1val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm2"].ToString() != "" && dtAPI.Rows[0]["B_prm2val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm2"].ToString() + "=" + dtAPI.Rows[0]["B_prm2val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm3"].ToString() != "" && dtAPI.Rows[0]["B_prm3val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm3"].ToString() + "=" + dtAPI.Rows[0]["B_prm3val"].ToString() + "&";
                    }
                    if (dtAPI.Rows[0]["B_prm4"].ToString() != "" && dtAPI.Rows[0]["B_prm4val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dtAPI.Rows[0]["B_prm4"].ToString() + "=" + dtAPI.Rows[0]["B_prm4val"].ToString();
                    }
                    if (dtAPI.Rows[0]["AlogorithmName"].ToString() != "" && dtAPI.Rows[0]["AlogorithmName"].ToString() != "" && dtAPI.Rows[0]["AlogorithmName"].ToString() != null)
                    {
                        string strkey = "";

                        string CombineKeyValue = dtAPI.Rows[0]["CombinePARAM"].ToString();
                        string CombineKey = dtAPI.Rows[0]["CombineKey"].ToString();
                        if (dtAPI.Rows[0]["CombineKeyParamB"].ToString() != "")
                        {
                            string[] strKeyBArray = dtAPI.Rows[0]["CombineKeyParamB"].ToString().TrimEnd(',').Split(',');
                            for (Int32 i = 0; i < strKeyBArray.Length; i++)
                            {
                                strkey = strkey + dtAPI.Rows[0][strKeyBArray[i].ToString()].ToString() + CombineKeyValue;
                            }
                            strkey = strkey.TrimEnd('|');
                        }
                        string HaskKey = GenerateSHA512String(strkey).ToLower();
                        strBalanceAPI = strBalanceAPI + CombineKey + "=" + HaskKey;
                    }



                    if (strBalanceAPI.EndsWith("&"))
                        strBalanceAPI = strBalanceAPI.Substring(0, strBalanceAPI.Length - 1);
                    //litBalanceURL.Text = strBalanceAPI;
                    litBalanceURL.Text = "";

                    string result = apicall(strBalanceAPI);
                    //string[] split = result.Split(',');
                    Literal litBalance = (Literal)e.Row.FindControl("litBalance");

                    string Success = Convert.ToString(dtAPI.Rows[0]["Success"]);
                    string Failed = Convert.ToString(dtAPI.Rows[0]["Failed"]);
                    string Pending = Convert.ToString(dtAPI.Rows[0]["Pending"]);
                    string Status = "", ErrorCode = "";
                    string TypeData = ObjData.select_data_scalar_string("select prm9val from tblRecharge_API where APIID='" + dtAPI.Rows[0]["APIID"].ToString() + "'");
                    if (TypeData == "XML")
                    {
                        result = "";// XMLTOJOSN.XmlToJSON(result);
                        JObject ObjDataXMLKey = JObject.Parse(result);
                        string[] xmlKetBala = dtAPI.Rows[0]["XMlKeyBalance"].ToString().Split(',');//Recharge_Credit,Status
                        JObject ObjDataPOST = null;
                        for (int i = 0; i < xmlKetBala.Length; i++)
                        {
                            ObjDataPOST = JObject.Parse(ObjDataXMLKey["" + xmlKetBala[i] + ""].ToString());
                            ObjDataXMLKey = JObject.Parse(ObjDataXMLKey["" + xmlKetBala[i] + ""].ToString());
                        }


                        if (dtAPI.Rows[0]["StatusBalance"].ToString() != "")
                        {
                            Status = ObjDataPOST["" + dtAPI.Rows[0]["StatusBalance"].ToString() + ""].ToString();
                            Status = ObjData.select_data_scalar_string("SELECT CODEMESS FROM TBL_API_STATUS_ERROR_CODE WHERE ERRORCODE='" + Status + "' and APIID='" + dtAPI.Rows[0]["APIID"].ToString() + "'");
                            if (Status.ToString().ToLower() != Failed.ToLower())
                            {
                                litBalance.Text = ObjDataPOST["" + dtAPI.Rows[0]["B_BalancePosition"].ToString() + ""].ToString();
                            }
                            else
                            {
                                litBalance.Text = ObjDataPOST["" + dtAPI.Rows[0]["DescriptionBalance"].ToString() + ""].ToString();
                            }
                        }
                        else
                        {
                            litBalance.Text = ObjDataPOST["" + dtAPI.Rows[0]["B_BalancePosition"].ToString() + ""].ToString();
                        }
                    }
                    else if (TypeData == "JSON")
                    {
                        JObject ObjDataPOST = JObject.Parse(result);

                        if (dtAPI.Rows[0]["StatusBalance"].ToString() != "" && dtAPI.Rows[0]["StatusBalance"] != null)
                        {
                            Status = ObjDataPOST["" + dtAPI.Rows[0]["StatusBalance"].ToString() + ""].ToString();
                            Status = ObjData.select_data_scalar_string("SELECT CODEMESS FROM TBL_API_STATUS_ERROR_CODE WHERE ERRORCODE='" + Status + "' and APIID='" + dtAPI.Rows[0]["APIID"].ToString() + "'");
                            if (Status.ToString().ToLower() != Failed.ToLower())
                            {
                                litBalance.Text = ObjDataPOST["" + dtAPI.Rows[0]["B_BalancePosition"].ToString() + ""].ToString();
                            }
                            else
                            {
                                litBalance.Text = ObjDataPOST["" + dtAPI.Rows[0]["DescriptionBalance"].ToString() + ""].ToString();

                            }
                        }
                        else
                        {
                            litBalance.Text = ObjDataPOST["" + dtAPI.Rows[0]["B_BalancePosition"].ToString() + ""].ToString();
                        }

                    }
                    else if (TypeData == "CSV")
                    {
                        string[] split = result.Split(Convert.ToChar(dtAPI.Rows[0]["Splitter"]));

                        litBalance.Text = split[Convert.ToInt32(dtAPI.Rows[0]["B_BalancePosition"].ToString())];
                    }


                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    public static string GenerateSHA512String(string inputString)
    {
        SHA512 sha512 = SHA512Managed.Create();
        byte[] bytes = Encoding.UTF8.GetBytes(inputString);
        byte[] hash = sha512.ComputeHash(bytes);
        return GetStringFromHash(hash);
    }
    private static string GetStringFromHash(byte[] hash)
    {
        StringBuilder result = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            result.Append(hash[i].ToString("X2"));
        }
        return result.ToString();
    }
}