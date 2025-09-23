using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_AddComplain : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    ComplainTicket complainTicket = new ComplainTicket();
    DataTable dt = new DataTable();
    DataTable dtMember = new DataTable();
    Page page;
    public Company company;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtMember"] != null)
        {
            company = Company.GetCompanyInfo();
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                BindDropDown.FillDropDown(ddlService, "ActiveService", "Name", "ID", "Select Service");
                FillData();
            }
        }
        else
        {
            Response.Redirect("logout.aspx");
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            complainTicket.ID = 0;
            complainTicket.ServiceID = Convert.ToInt16(ddlService.SelectedValue);
            complainTicket.Message = txtMessage.Text.Trim();
            dtMember = (DataTable)Session["dtMember"];


            if (complainTicket.IsAnyNullOrEmpty(complainTicket))
            {

                int Val = ObjConnection.update_data("EXEC Proc_AddEditComplainTicket " + complainTicket.ID + "," + complainTicket.ServiceID + ",'" + dtMember.Rows[0]["msrno"] + "','" + complainTicket.Message + "',"+ company.MemberID + "");
                if (Val > 0)
                {
                    ErrorShow.Success(page: page, Message: "Record Inserted Successfully");
                   ConstantsData.Clear(page.Controls);
                    FillData();
                }
                else
                {
                    ErrorShow.Error(page1: page, Message: "Record Not Inserted, Try Again ..");
                }
            }
            else
            {

            }

        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('something went wrong ..!!')</script>");
        }
    }

    private void Clear()
    {
        txtMessage.Text = string.Empty;
        ddlService.SelectedIndex = 0;
    }

    private void FillData()
    {
        DataTable dt = new DataTable();
        dt = ObjConnection.select_data_dt("Exec ManageComplainTicket @Action='GetByMsrno',@ID=" + Convert.ToInt16(dtMember.Rows[0]["msrno"].ToString()) + "");
        rptData.DataSource = dt;
        rptData.DataBind();
    }
}