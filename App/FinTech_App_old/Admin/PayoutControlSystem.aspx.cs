using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_PayoutControlSystem : System.Web.UI.Page
{
    DataTable dtMemberMaster = new DataTable();
    cls_connection cls = new cls_connection();
    DataTable dtExport = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["dtEmployee"] != null)
            {
                FillRole();
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    private void FillRole()
    {
        DataTable dtRole = cls.select_data_dt("Exec ManageRole @Action='RoleBasePermission',@ID=" + 2 + "");
        Common.BindDropDown(dllRole, dtRole, "Name", "ID");
        dllRole.Items.Insert(0, new ListItem("--select Role--", "0"));
    }

    private void BindData()
    {
        DataTable dt = new DataTable();
        dt = cls.select_data_dt("Proc_Manage_PayoutSetting " + dllRole.SelectedValue + "");

        if (dt.Rows.Count > 0)
        {
            gvTransactionHistory.DataSource = dt;
            gvTransactionHistory.DataBind();
        }
        else
        {
            gvTransactionHistory.DataSource = null;
            gvTransactionHistory.DataBind();
        }
    }

    private void AddSetting()
    {
        Int32 Val = 0;
        Val = cls.update_data("proc_Payout_Setting_ControL '" + hdnID.Value + "'," + dllRole.SelectedValue + ",'" + txtAmount.Text + "','" + dllPayoutFrom.Text + "'");

        if (Val > 0)
        {
            txtAmount.Text = "";            
            
            BindData();
        }

    }


    private void Delete()
    {
        Int32 Val = 0;
        Val = cls.update_data("delete from Payout_Setting_ControL where id='"+hdnID.Value+"'");

        if (Val > 0)
        {
            hdnID.Value = "0";
            BindData();
        }

    }
    private void Edit(string ID)
    {
        DataTable dt = cls.select_data_dt("select * from Payout_Setting_ControL where ID="+ID+"");
        if (dt.Rows.Count > 0)
        {
            txtAmount.Text = dt.Rows[0]["Amount"].ToString();
            dllPayoutFrom.SelectedValue = dt.Rows[0]["PayoutFrom"].ToString();
            dllRole.SelectedValue= dt.Rows[0]["RoleID"].ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        AddSetting();
    }

    protected void dllRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void gvTransactionHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        hdnID.Value = e.CommandArgument.ToString();
        if (e.CommandName == "Delete")
        {
            Delete();
        }
        if (e.CommandName == "Edit")
        {            
            Edit(e.CommandArgument.ToString());
        }
    }
}