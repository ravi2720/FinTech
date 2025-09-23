using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_AssignService : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtEmployee = new DataTable();
    DataTable dt = new DataTable();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (SessionManager.IsSessionAdmin)
        {
            dtEmployee = (DataTable)SessionManager.CurrentSessionAdmin;
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlMemberList, "MemberList", "LoginID", "ID", "Select Member", "1");
            }
        }
        else
        {
            Response.Redirect(SessionManager.RedirectUrlA);
        }
    }

    private void FillService(string msrno)
    {
        try
        {
            dt = cls.select_data_dt("FillDropDown 'ActiveService'");
            rptAllService.DataSource = dt;
            rptAllService.DataBind();

            string strOld = cls.select_data_scalar_string("select service from MEMBERSERVICE where msrno=" + msrno + "");
            if (!string.IsNullOrEmpty(strOld))
            {
                foreach (RepeaterItem item in rptAllService.Items)
                {
                    CheckBox chkData = (CheckBox)item.FindControl("chkData");
                    HiddenField hdnVal = (HiddenField)item.FindControl("hdnVal");

                    for (int i = 0; i < strOld.Split(',').Length; i++)
                    {
                        if (hdnVal.Value == strOld.Split(',')[i])
                        {
                            chkData.Checked = true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    [WebMethod]
    public static List<string> GetMember(string Search)
    {
        List<string> empResult = new List<string>();
        cls_connection ObjData = new cls_connection();
        DataTable dt = new DataTable();
        dt = ObjData.select_data_dt("select top 10 LoginID from member where IsActive=1 and loginid like '%" + Search + "%'");
        foreach (DataRow dr in dt.Rows)
        {
            empResult.Add(dr["LoginID"].ToString());
        }
        return empResult;
    }

    protected void btnAssign_Click(object sender, EventArgs e)
    {
        string str = "";
        foreach (RepeaterItem item in rptAllService.Items)
        {
            CheckBox chkData = (CheckBox)item.FindControl("chkData");
            HiddenField hdnVal = (HiddenField)item.FindControl("hdnVal");
            if (chkData.Checked)
            {
                str = str + "," + hdnVal.Value;
            }
        }
        dt = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno='" + ddlMemberList.SelectedValue + "'");
        if (dt.Rows.Count > 0)
        {
            cls.update_data("PROC_ASSIGN_SERVICE '" + str + "'," + dt.Rows[0]["msrno"] + "");
            ErrorShow.AlertMessage(page, "Service Assign Successfully.", ConstantsData.CompanyName);

        }
    }

    protected void ddlMemberList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMemberList.SelectedIndex > 0)
        {

            dt = cls.select_data_dt("select * from VIEW_MEMBERDETAILS where msrno='" + ddlMemberList.SelectedValue + "'");
            if (dt.Rows.Count > 0)
            {
                hidMsrNo.Value = Convert.ToString(dt.Rows[0]["MsrNo"]);
                FillService(dt.Rows[0]["msrno"].ToString());
            }
            else
            {
                ErrorShow.AlertMessage(page, "Please Enter Valid MemberID  !", ConstantsData.CompanyName);
            }
        }
        else
        {
            ErrorShow.AlertMessage(page, "Please Enter Valid MemberID  !", ConstantsData.CompanyName);
        }
    }
}