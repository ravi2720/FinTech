using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_GSTDetails : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    public Company company;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                State();
                FillData(Convert.ToInt16(dtMember.Rows[0]["ID"].ToString()));
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }
    }
    private void State()
    {
        DataTable dt = cls.select_data_dt("select * from State where  countryid=26");
        if (dt.Rows.Count > 0)
        {
            ddlState.DataSource = dt;
            ddlState.DataTextField = "Name";
            ddlState.DataValueField = "StateCode";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("-- Select State --", "0"));
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int id = hdnid.Value == "0" ? 0 : Convert.ToInt32(hdnid.Value.ToString());
            dtMember = (DataTable)Session["dtMember"];
            DataTable dt = cls.select_data_dt("Exec AddEditGSTDetail " + id + ",'" + dtMember.Rows[0]["msrno"] + "','" + ddlState.SelectedValue + "','" + txtfirmname.Text + "','" + txtGSTNo.Text + "','" + txtAddress.Text + "','" + dllType.SelectedValue + "'");
            if (Convert.ToInt16(dt.Rows[0]["ID"].ToString()) > 0)
            {
                ErrorShow.SuccessNotify(page, "Record Inserted Successfully");
                Clear();
            }
            else
            {
                ErrorShow.ErrorNotify(page, "Record Already Exist");
            }
        }
        catch (Exception ex)
        {
            ErrorShow.ErrorNotify(page, "Something went wrong");
        }

    }

    protected void FillData(int id)
    {
        dtMember = (DataTable)Session["dtMember"];
        dt = cls.select_data_dt("EXEC ManageGSTDetail 'Get','" + 0 + "','" + dtMember.Rows[0]["msrno"] + "'");
        if (dt.Rows.Count > 0)
        {
            repeater1.DataSource = dt;
            repeater1.DataBind();
        }
        else
        {
            repeater1.DataSource = null;
            repeater1.DataBind();

        }
    }

    protected void repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            hdnid.Value = e.CommandArgument.ToString();
            FillNews(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }
        if (e.CommandName == "Active")
        {
            Active(id: Convert.ToInt16(e.CommandArgument.ToString()));
        }
    }

    private void FillNews(int id)
    {
        dt = cls.select_data_dt("EXEC ManageGSTDetail 'Get','" + id + "','" + dtMember.Rows[0]["msrno"] + "'");
        txtfirmname.Text = dt.Rows[0]["FirmName"].ToString();
        txtGSTNo.Text = dt.Rows[0]["GSTNo"].ToString();
        txtAddress.Text = dt.Rows[0]["Address"].ToString();
        dllType.SelectedValue = dt.Rows[0]["CompanyType"].ToString();
        ddlState.SelectedValue = dt.Rows[0]["StateID"].ToString();
    }


    private void Active(int id)
    {
        int val = cls.update_data("ManageGSTDetail 'IsActive','" + id + "','" + dtMember.Rows[0]["msrno"] + "'");

        if (val > 0)
        {
            FillData(Convert.ToInt16(dtMember.Rows[0]["ID"].ToString()));
        }

    }

    private void Clear()
    {
        txtAddress.Text = "";
        txtfirmname.Text = "";
        txtGSTNo.Text = "";

    }
}