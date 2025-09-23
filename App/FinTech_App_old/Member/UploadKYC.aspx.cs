using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Member_BankDetails : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    DataTable dtMember;
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
        if (Session["dtMember"] != null)
        {
            dtMember = (DataTable)Session["dtMember"];
            if (!IsPostBack)
            {
                FillDocument();
                FillData(msrno: Convert.ToInt32(dtMember.Rows[0]["Msrno"]));
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void FillData(int msrno)
    {
        rptData.DataSource = cls.select_data_dt("select * from MEMBERKYCDOCUMENTS where msrno = " + msrno + " order by id desc ");
        rptData.DataBind();
    }

    private void FillDocument()
    {
        List<ListItem> deletedItems = new List<ListItem>();
        DataTable dt = cls.select_data_dt("select DocID from MEMBERKYCDOCUMENTS where Msrno = '" + Convert.ToInt32(dtMember.Rows[0]["Msrno"]) + "' and status <> 'Rejected'");
        ddlDocument.DataSource = cls.select_data_dt("exec PROC_MANAGEKYCDOCUMENTS 0,'GETACTIVE'");
        ddlDocument.DataTextField = "Name";
        ddlDocument.DataValueField = "ID";
        ddlDocument.DataBind();
        ddlDocument.Items.Insert(0, new ListItem("-- Select Document --", "0"));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            ddlDocument.Items.Remove(ddlDocument.Items.FindByValue(dt.Rows[i]["DocID"].ToString()));
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string imgFront, Front = "";
        Bitmap bmFront = null;
        string gdFront = Guid.NewGuid().ToString() + ".png";

        string imgBack, Back = "";
        Bitmap bmBack = null;
        string gdBack = Guid.NewGuid().ToString() + ".png";
        if (fuDoc.HasFile && Convert.ToInt32(hdnSide.Value) == 2 ? fuDocBack.HasFile : !fuDocBack.HasFile)
        {
            try
            {
                fuDoc.PostedFile.SaveAs(MapPath("./images/KYC/") + gdFront);
                Front = gdFront.ToString();

                if (Convert.ToInt32(hdnSide.Value) == 2)
                {
                    fuDocBack.PostedFile.SaveAs(MapPath("./images/KYC/") + gdBack);
                    Back = gdBack.ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error occured: " + ex.Message.ToString());
            }
            finally
            {
                imgFront = string.Empty;
                // bmFront.Dispose();
                fuDoc = new FileUpload();
                if (Convert.ToInt32(hdnSide.Value) == 2)
                {
                    imgBack = string.Empty;
                    // bmBack.Dispose();
                    fuDocBack = new FileUpload();
                }
            }
            try
            {
                DataTable dtSave = cls.select_data_dt("Exec Proc_AddEditMemberKYCDocument 0,'" + Convert.ToInt32(dtMember.Rows[0]["Msrno"]) + "','" + Convert.ToInt32(ddlDocument.SelectedValue) + "','" + txtDocNumber.Text.Trim() + "','" + Front + "','" + Back + "'");
                if (Convert.ToInt32(dtSave.Rows[0]["ID"]) > 0)
                {
                    ErrorShow.AlertMessage(page, "Record Inserted Successfully ..!!", ConstantsData.CompanyName);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error occured: " + ex.Message.ToString());
            }
        }
        FillDocument();
        FillData(msrno: Convert.ToInt32(dtMember.Rows[0]["Msrno"]));
    }



    private void Clear()
    {
        txtDocNumber.Text = string.Empty;
    }

    protected void ddlDocument_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlDocument.SelectedValue) > 0)
        {
            DataTable dtcheck = cls.select_data_dt("select * from MEMBERKYCDOCUMENTS where Docid=" + Convert.ToInt16(ddlDocument.SelectedValue) + " and Msrno = " + Convert.ToInt16(dtMember.Rows[0]["Msrno"]) + " and Status <> 'Rejected'");
            if (dtcheck.Rows.Count == 0)
            {
                int side = cls.select_data_scalar_int("select side from KYCDocumentsMaster where id = " + Convert.ToInt16(ddlDocument.SelectedValue) + "");
                if (side > 0)
                {
                    hdnSide.Value = side.ToString();
                    if (side > 1)
                    {
                        fuDocBack.Visible = true;
                        RequiredFieldValidator3.Visible = true;
                        fuDocBack.Enabled = true;
                        RequiredFieldValidator3.Enabled = true;
                    }
                    else
                    {
                        fuDocBack.Visible = false;
                        RequiredFieldValidator3.Visible = false;
                        fuDocBack.Enabled = false;
                        RequiredFieldValidator3.Enabled = false;
                    }
                    fuDoc.Enabled = true;
                    txtDocNumber.Enabled = true;
                    btnSave.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Something Went Wrong, try Again ..!!');window.location.replace('dashboard.aspx')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ddlDocument.SelectedItem.Text.ToString() + "' Already Uploaded ..!!');", true);
            }

        }
        else
        {
            fuDoc.Enabled = false;
            txtDocNumber.Enabled = false;
            btnSave.Visible = false;
            fuDocBack.Visible = false;
            RequiredFieldValidator3.Visible = false;
            fuDocBack.Enabled = false;
            RequiredFieldValidator3.Enabled = false;
        }
    }
}