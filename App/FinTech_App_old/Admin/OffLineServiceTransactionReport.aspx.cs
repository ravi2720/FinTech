using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_OffLineServiceTransactionReport : System.Web.UI.Page
{
    cls_connection Objconnection = new cls_connection();
    StringBuilder htmlTable = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }

    private void BindData()
    {
        DataTable dt = new DataTable();
        dt = Objconnection.select_data_dt("PROC_BoxTractionHistory");
        rptData.DataSource = dt;
        rptData.DataBind();
    }

    public string MakeTable(string Data)
    {
        ConvertJsonToDatatable(Data);
        return "";
    }
    public static DataTable ConvertJsonToDatatable(string jsonString)
    {
        DataTable dt = new DataTable();
        //strip out bad characters
        string[] jsonParts = Regex.Split(jsonString.Replace("[", "").Replace("]", ""), "},{");
        //hold column names
        List<string> dtColumns = new List<string>();
        //get columns
        foreach (string jp in jsonParts)
        {
            //only loop thru once to get column names
            string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1);
                    string v = rowData.Substring(idx + 1);
                    if (!dtColumns.Contains(n))
                    {
                        dtColumns.Add(n.Replace("\"", ""));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error Parsing Column Name : {0}", rowData));
                }
            }
            break; // TODO: might not be correct. Was : Exit For
        }
        //build dt
        foreach (string c in dtColumns)
        {
            dt.Columns.Add(c);
        }
        //get table data
        foreach (string jp in jsonParts)
        {
            string[] propData = Regex.Split(jp.Replace("{", "").Replace("}", ""), ",");
            DataRow nr = dt.NewRow();
            foreach (string rowData in propData)
            {
                try
                {
                    int idx = rowData.IndexOf(":");
                    string n = rowData.Substring(0, idx - 1).Replace("\"", "");
                    string v = rowData.Substring(idx + 1).Replace("\"", "");
                    nr[n] = v;
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dt.Rows.Add(nr);
        }
        return dt;
    }

    protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hdnData = (HiddenField)e.Item.FindControl("hdnData");
        HiddenField hdnstatus = (HiddenField)e.Item.FindControl("hdnstatus");

        DropDownList dllStatus = (DropDownList)e.Item.FindControl("dllStatus");
        Button btnUpdate = (Button)e.Item.FindControl("btnUpdate");

        dllStatus.SelectedValue = hdnstatus.Value;

        if (dllStatus.SelectedValue == "Success" || dllStatus.SelectedValue == "Refund" || dllStatus.SelectedValue == "Failed")
        {
            dllStatus.Enabled = false;
            btnUpdate.Visible = false;
        }
        GridView grdData = (GridView)e.Item.FindControl("grdData");
        DataTable dt = ConvertJsonToDatatable(hdnData.Value);
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Rows[0][dt.Columns[i]].ToString().Contains(".jpg") || dt.Rows[0][dt.Columns[i]].ToString().Contains(".gif") || dt.Rows[0][dt.Columns[i]].ToString().Contains(".png"))
            {
                dt.Rows[0][dt.Columns[i]] = "<a href='.././images/" + dt.Rows[0][dt.Columns[i]].ToString().Trim() + "' download><img src='.././images/" + dt.Rows[0][dt.Columns[i]].ToString().Trim() + "' download width='100' /></a>";
            }
            if (dt.Rows[0][dt.Columns[i]].ToString().Contains(".mp4"))
            {
                dt.Rows[0][dt.Columns[i]] = "<video width='320' height='240' controls><source src='.././images/" + dt.Rows[0][dt.Columns[i]].ToString().Trim() + "' type='video/mp4'></video>";

            }
        }
        grdData.DataSource = dt;
        grdData.DataBind();


        try
        {
            HiddenField hdnRequestData = (HiddenField)e.Item.FindControl("hdnRequestData");
            GridView grdRequestData = (GridView)e.Item.FindControl("grdRequestData");
            dt = ConvertJsonToDatatable(hdnRequestData.Value);
            grdRequestData.DataSource = dt;
            grdRequestData.DataBind();
        }
        catch (Exception ex)
        {
        }
    }
    protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            TableCellCollection cells = e.Row.Cells;

            foreach (TableCell cell in cells)
            {
                cell.Text = Server.HtmlDecode(cell.Text);
            }

        }
    }
    protected void rptData_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "UpdateStatus")
        {
            DropDownList dllStatus = (DropDownList)e.Item.FindControl("dllStatus");
            DataTable dthistory = Objconnection.select_data_dt("select * from BoxFeaturesTransaction where id=" + e.CommandArgument.ToString() + "");
            string MemberID = Objconnection.select_data_scalar_string("select Memberid from Member where msrno=" + dthistory.Rows[0]["msrno"].ToString() + "");
            string PAckageID = Objconnection.select_data_scalar_string("select packageid from Member where msrno=" + dthistory.Rows[0]["msrno"].ToString() + "");
            if (dthistory.Rows.Count > 0)
            {
                Int32 val = 0;
                if (dllStatus.SelectedValue == "Refund" || dllStatus.SelectedValue == "Failed")
                {
                    DataTable dtPayment = Objconnection.select_data_dt("exec [ProcMLM__EWalletTransaction]  '" + MemberID + "', " + Convert.ToDecimal(dthistory.Rows[0]["Amount"].ToString()) + ", 'Cr','Refund-Service TransID-" + dthistory.Rows[0]["TransID"].ToString() + "','WEB','" + dthistory.Rows[0]["TransID"].ToString() + "'");
                    if (dtPayment.Rows[0][0].ToString() == "1")
                    {
                        val = Objconnection.update_data("update BoxFeaturesTransaction set status='" + dllStatus.SelectedValue + "' where id='" + e.CommandArgument + "'");
                        if (val > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Update successfully');", true);
                            BindData();
                        }
                    }
                }
                else
                {
                    val = Objconnection.update_data("update BoxFeaturesTransaction set status='" + dllStatus.SelectedValue + "' where id='" + e.CommandArgument + "'");
                    if (val > 0)
                    {
                        if (dllStatus.SelectedValue == "Success")
                        {


                            DataTable dtCommission = Objconnection.select_data_dt("select * from BoxServiceCommission where PackageID=" + PAckageID + " and ProductID=" + dthistory.Rows[0]["BoxID"].ToString() + "");
                            DataTable dtBoxList = Objconnection.select_data_dt("select * from BoxList where ID=" + dthistory.Rows[0]["BoxID"].ToString() + " ");
                            if (dtBoxList.Rows.Count > 0)
                            {
                                //
                                JArray DataBox = JArray.Parse(dtBoxList.Rows[0]["BoxProperty"].ToString());
                                if (DataBox[0]["IsCashBack"] != null)
                                {

                                    if (DataBox[0]["CashBack"].ToString() != "")
                                    {
                                        decimal CashBack = Convert.ToDecimal(DataBox[0]["CashBack"].ToString());
                                        DataTable dtPayment = Objconnection.select_data_dt("exec [ProcMLM__EWalletTransaction]  '" + MemberID + "', " + CashBack + ", 'Cr','CashBack -Service TransID-" + dthistory.Rows[0]["TransID"].ToString() + "','WEB','" + dthistory.Rows[0]["TransID"].ToString() + "'");
                                        if (dtPayment.Rows[0][0].ToString() == "1")
                                        {

                                        }

                                    }

                                }
                            }
                            if (dtCommission.Rows.Count > 0)
                            {
                                if (Convert.ToBoolean(dtCommission.Rows[0]["IsFlat"]))
                                {
                                    DataTable dtPayment = Objconnection.select_data_dt("exec ProcBox_DistributeCommission '" + dthistory.Rows[0]["msrno"].ToString() + "', " + dthistory.Rows[0]["BoxID"].ToString() + "," + dtCommission.Rows[0]["Commission"].ToString() + ",'" + dthistory.Rows[0]["TransID"].ToString() + "'");
                                }
                                else
                                {
                                    decimal CAmount = ((Convert.ToDecimal(dtCommission.Rows[0]["Commission"].ToString()) * Convert.ToDecimal(dthistory.Rows[0]["Amount"].ToString())) / 100);
                                    DataTable dtPayment = Objconnection.select_data_dt("exec ProcBox_DistributeCommission '" + dthistory.Rows[0]["msrno"].ToString() + "', " + dthistory.Rows[0]["BoxID"].ToString() + "," + dthistory.Rows[0]["Amount"].ToString() + ",'" + dthistory.Rows[0]["TransID"].ToString() + "'");

                                }
                            }


                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Update successfully');", true);
                        BindData();
                    }
                }


            }
        }
    }
}