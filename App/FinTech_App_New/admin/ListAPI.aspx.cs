using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ListAPI : System.Web.UI.Page
{
    cls_connection ObjData = new cls_connection();
    DataTable dt = new DataTable();
    Page page;

    protected void Page_Preint(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillAPI();
        }
    }

    protected void gvAPI_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        #region [IsDelete]

        if (e.CommandName == "IsDelete")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                ObjData.select_data_dt("ProcRecharge_ManageAPI 'IsDelete', , " + idno + "");               
                fillAPI();

            }
            catch
            {
                ErrorShow.Error(page,"Sorry.You cannot delete this Item.If you want to delete this fist delete child Items related this Item.!");
            }
        }

        #endregion

        #region [IsActive]
        if (e.CommandName == "IsActive")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                ObjData.select_data_dt("ProcRecharge_ManageAPI 'IsActive', , " + idno + "");                
                fillAPI();
            }
            catch (Exception ex)
            {
                ErrorShow.Error(Page, ex.InnerException.Message);
            }
        }
        #endregion

        #region [View]
        if (e.CommandName == "View")
        {

            try
            {
                int idno = 0;
                idno = Convert.ToInt32(e.CommandArgument);
                //objAPI.ManageAPI("IsActive", idno);
                //fillAPI();
                dt = ObjData.select_data_dt("ProcRecharge_ManageAPI 'Get', "+idno+""); 
                string str = "";
                str = dt.Rows[0]["URL"].ToString() + dt.Rows[0]["prm1"].ToString() + "=" + dt.Rows[0]["prm1val"].ToString() + "&";
                if (dt.Rows[0]["prm2"].ToString() != "" && dt.Rows[0]["prm2val"].ToString() != "")
                {
                    str = str + dt.Rows[0]["prm2"].ToString() + "=" + dt.Rows[0]["prm2val"].ToString() + "&";
                }
                if (dt.Rows[0]["prm3"].ToString() != "")
                {
                    str = str + dt.Rows[0]["prm3"].ToString() + "=XXXXXXXXXX&";
                }
                if (dt.Rows[0]["prm4"].ToString() != "")
                {
                    str = str + dt.Rows[0]["prm4"].ToString() + "=XX&";
                }
                if (dt.Rows[0]["prm5"].ToString() != "")
                {
                    str = str + dt.Rows[0]["prm5"].ToString() + "=XX&";
                }
                if (dt.Rows[0]["prm6"].ToString() != "")
                {
                    str = str + dt.Rows[0]["prm6"].ToString() + "=XX&";
                }
                if (dt.Rows[0]["prm7"].ToString() != "")
                {
                    str = str + dt.Rows[0]["prm7"].ToString() + "=XX&";
                }
                if (dt.Rows[0]["prm8"].ToString() != "")
                {
                    str = str + dt.Rows[0]["prm8"].ToString() + "=XX&";
                }
                if (dt.Rows[0]["prm9"].ToString() != "" && dt.Rows[0]["prm9val"].ToString() != "")
                {
                    str = str + dt.Rows[0]["prm9"].ToString() + "=" + dt.Rows[0]["prm9val"].ToString() + "&";
                }
                if (dt.Rows[0]["prm10"].ToString() != "" && dt.Rows[0]["prm10val"].ToString() != "")
                {
                    str = str + dt.Rows[0]["prm10"].ToString() + "=" + dt.Rows[0]["prm10val"].ToString() + "&";
                }
                if (str.EndsWith("&"))
                    str = str.Substring(0, str.Length - 1);
                litAPI.Text = str;
                litAPIName.Text = dt.Rows[0]["APIName"].ToString();


                if (dt.Rows[0]["BalanceURL"].ToString() != "")
                {
                    string strBalanceAPI = "";
                    if (dt.Rows[0]["B_prm1"].ToString() != "" && dt.Rows[0]["B_prm1val"].ToString() != "")
                    {
                        strBalanceAPI = dt.Rows[0]["BalanceURL"].ToString() + dt.Rows[0]["B_prm1"].ToString() + "=" + dt.Rows[0]["B_prm1val"].ToString() + "&";
                    }
                    if (dt.Rows[0]["B_prm2"].ToString() != "" && dt.Rows[0]["B_prm2val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dt.Rows[0]["B_prm2"].ToString() + "=" + dt.Rows[0]["B_prm2val"].ToString() + "&";
                    }
                    if (dt.Rows[0]["B_prm3"].ToString() != "" && dt.Rows[0]["B_prm3val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dt.Rows[0]["B_prm3"].ToString() + "=" + dt.Rows[0]["B_prm3val"].ToString() + "&";
                    }
                    if (dt.Rows[0]["B_prm4"].ToString() != "" && dt.Rows[0]["B_prm4val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dt.Rows[0]["B_prm4"].ToString() + "=" + dt.Rows[0]["B_prm4val"].ToString();
                    }
                    if (strBalanceAPI.EndsWith("&"))
                        strBalanceAPI = strBalanceAPI.Substring(0, strBalanceAPI.Length - 1);
                    litBalanceAPI.Text = strBalanceAPI;
                }


                if (dt.Rows[0]["StatusURL"].ToString() != "")
                {
                    string strBalanceAPI = "";
                    if (dt.Rows[0]["S_prm1"].ToString() != "" && dt.Rows[0]["S_prm1val"].ToString() != "")
                    {
                        strBalanceAPI = dt.Rows[0]["StatusURL"].ToString() + dt.Rows[0]["S_prm1"].ToString() + "=" + dt.Rows[0]["S_prm1val"].ToString() + "&";
                    }
                    if (dt.Rows[0]["S_prm2"].ToString() != "" && dt.Rows[0]["S_prm2val"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dt.Rows[0]["S_prm2"].ToString() + "=" + dt.Rows[0]["S_prm2val"].ToString() + "&";
                    }
                    if (dt.Rows[0]["S_prm3"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dt.Rows[0]["S_prm3"].ToString() + "=XXXXX&";
                    }
                    if (dt.Rows[0]["S_prm4"].ToString() != "")
                    {
                        strBalanceAPI = strBalanceAPI + dt.Rows[0]["S_prm4"].ToString() + "=XXXXX&";
                    }
                    if (strBalanceAPI.EndsWith("&"))
                        strBalanceAPI = strBalanceAPI.Substring(0, strBalanceAPI.Length - 1);

                    litStatusAPI.Text = strBalanceAPI;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "loadPopup()", true);
            }
            catch (Exception ex)
            {
                ErrorShow.Error(page,ex.Message);
            }
        }
        #endregion
    }
    private void fillAPI()
    {
        dt = ObjData.select_data_dt("ProcRecharge_ManageAPI 'GetAll', 0");
        gvAPI.DataSource = dt;
        gvAPI.DataBind();
    }
}