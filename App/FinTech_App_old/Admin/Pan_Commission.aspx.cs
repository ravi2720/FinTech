using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
public partial class Admin_Pan_Commission : System.Web.UI.Page
{
    #region [Properties]
    DataTable dtDLLPANType = new DataTable();
    DataTable dtOperator = new DataTable();
    DataTable dtMember = new DataTable();
    cls_connection objconnection = new cls_connection();
    #endregion

    #region [PagePreInit]

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["dtEmployee"] == null)
        {
            Response.Redirect("Default.aspx");
        }
        else
        {
        }
    }

    #endregion

    #region [PageLoad]
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["mySurcharge"] = null;
            dtMember = (DataTable)Session["dtEmployee"];
            BindDropDown.FillDropDown(ddlPackage, "ActivePackage", "Name", "ID", "Select Package", dtMember.Rows[0]["ID"].ToString());
            BindDropDown.FillDropDown(ddlPANType, "PANTokenType", "PANCARDType", "PANCardID", "Select PanToken");

            gvOperator.Visible = false;

        }
    }

    #endregion

    #region [Insert | Update]
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        #region [Insert]
        Int32 intresult = 0;
        int po = 0;
        int i = objconnection.delete_data("delete from [tblPAN_Surcharge] where PANType=" + Convert.ToInt32(ddlPANType.SelectedItem.Value) + " and PackageID=" + Convert.ToInt32(ddlPackage.SelectedValue));
        DataTable dt = new DataTable();
        dt = (DataTable)Session["mySurcharge"];
        if (dt.Rows.Count == 1)
        {
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                intresult = intresult + objconnection.insert_data("insert into [tblPAN_Surcharge] (packageid, PANType, PANFees, surcharge, isFlat) values (" + ddlPackage.SelectedValue + ",'" + dt.Rows[j][2].ToString() + "','" + dt.Rows[j][3].ToString() + "','" + dt.Rows[j][4].ToString() + "','" + dt.Rows[j][5].ToString() + "')");
            }
        }
        else
        {
            po = objconnection.delete_data("delete from [tblPAN_Surcharge] where PackageID=" + Convert.ToInt32(ddlPackage.SelectedValue));
            for (int j = 0; j < dt.Rows.Count; j++)
            {
                intresult = intresult + objconnection.insert_data("insert into [tblPAN_Surcharge] (packageid, PANType, PANFees, surcharge, isFlat) values (" + ddlPackage.SelectedValue + ",'" + dt.Rows[j][2].ToString() + "','" + dt.Rows[j][3].ToString() + "','" + dt.Rows[j][4].ToString() + "','" + dt.Rows[j][5].ToString() + "')");
            }

        }
        if (intresult > 0)
        {
            //ScriptManager.RegisterStartupScript(this, typeof(string), "Key", String.Format("Dialog_Alert('{0}');", "Success|Plan save successfully !|"), true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Plan save successfully');location.replace('Pan_Commission.aspx');", true);
        }
        else
        {
            if (po > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Success|Plan save successfully');location.replace('Pan_Commission.aspx');", true);
            }
            else
            {
                //ScriptManager.RegisterStartupScript(this, typeof(string), "Key", String.Format("Dialog_Alert('{0}');", "Error|There are some problem, Please try again !"), true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Key", "alert('Error|There are some problem, Please try again !');", true);
            }
        }
        #endregion
    }
    #endregion

    #region [Reset]
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clear();
    }
    #endregion

    #region [All Functions]
    private void clear()
    {

    }



    #endregion
    protected void ddlPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlPackage.SelectedItem.Value) == 0)
        {
            gvOperator.DataSource = dtOperator;
            gvOperator.DataBind();

            gvOperator.Visible = false;

        }
        else
        {

            dtOperator = objconnection.select_data_dt("Exec [PAN_getSurcharge] 0,'" + ddlPackage.SelectedValue + "',3,0");
            DataView dv = dtOperator.DefaultView;
            //dv.Sort = "ServiceType desc";
            DataTable sortedDT = dv.ToTable();
            Session["mySurcharge"] = sortedDT;
            gvOperator.DataSource = sortedDT;
            gvOperator.DataBind();

            gvOperator.Visible = true;

        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (Session["mySurcharge"] != null)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["mySurcharge"];
            if (hdnidd.Value == "")
            {
                DataRow NewRow = dt.NewRow();
                NewRow[0] = "0";
                NewRow[1] = ddlPackage.SelectedValue;
                NewRow[2] = ddlPANType.SelectedItem.Value;
                NewRow[3] = txtPANFees.Text;
                NewRow[4] = txtsurcharged.Text;
                NewRow[5] = chkflatd.Checked;//.ToString();
                dt.Rows.Add(NewRow);
                Session["mySurcharge"] = dt;
                gvOperator.DataSource = dt;
                gvOperator.DataBind();
                ddlPANType.SelectedIndex = -1; txtPANFees.Text = "0"; txtsurcharged.Text = "0"; chkflatd.Checked = false;
            }
            else
            {
                //DataRow NewRow = dt.NewRow();
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(2, ddlPANType.SelectedItem.Value);
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(3, txtPANFees.Text);
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(4, txtsurcharged.Text);
                dt.Rows[Convert.ToInt32(hdnidd.Value)].SetField(5, chkflatd.Checked);
                Session["mySurcharge"] = dt;
                gvOperator.DataSource = dt;
                gvOperator.DataBind();
                hdnidd.Value = ""; ddlPANType.SelectedIndex = -1; txtPANFees.Text = "0"; txtsurcharged.Text = "0"; chkflatd.Checked = false;
            }
        }
    }

    protected void ddlPANType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPANType.SelectedIndex > 0)
        {
            if (Convert.ToInt32(ddlPANType.SelectedItem.Value) == 3)
            {
                txtPANFees.Text = "";
            }
            else
            {
                DataTable dtFees = new DataTable();
                dtFees = objconnection.select_data_dt("Select PANCARDType,PANCardID,PANFees from tbl_PANTokenType where PANCardID='" + ddlPANType.SelectedItem.Value + "'");
                txtPANFees.Text = dtFees.Rows[0]["PANFees"].ToString();
            }
        }
    }
    protected void gvOperator_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "medit")
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["mySurcharge"];
            int x = Convert.ToInt32(e.CommandArgument.ToString());
            x = x - 1;
            hdnidd.Value = x.ToString();
            ddlPANType.SelectedItem.Value = dt.Rows[x][2].ToString();
            txtPANFees.Text = dt.Rows[x][3].ToString();
            txtsurcharged.Text = dt.Rows[x][4].ToString();
            chkflatd.Checked = Convert.ToBoolean(dt.Rows[x][5]);
        }
        if (e.CommandName == "mdelete")
        {
            DataTable dt = new DataTable();
            dt = (DataTable)Session["mySurcharge"];
            int x = Convert.ToInt32(e.CommandArgument.ToString());
            x = x - 1;
            dt.Rows.RemoveAt(x);
            Session["mySurcharge"] = dt;
            gvOperator.DataSource = dt;
            gvOperator.DataBind();
        }
    }
}