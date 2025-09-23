using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_StikyNotes : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dtEmployee = (DataTable)HttpContext.Current.Session["dtEmployee"];
            DataTable dtNotes = cls.select_data_dt("Exec sp_StickyNotes 'GetNotes'," + Convert.ToInt16(dtEmployee.Rows[0]["ID"]) + "");
            if (dtNotes.Rows.Count > 0)
            {
                string divHtml = "<div id=\"create\" onclick=\"Add(this);\">+</div>";
                string notes="";
                for (int i = 0; i < dtNotes.Rows.Count; i++)
                {
                    notes = notes + "<div id='txtArea_"+i+ "' class='temp'><a href='#' class='closeNotes' onclick='Remove(txtArea1);'><i class='fa fa-close' style='font-size:15px;color:red;'></i></a><textarea onchange=\"find('add',this);\">" + dtNotes.Rows[i]["Notes"].ToString() + "</textarea></div>";
                }
                bindNotes.InnerHtml = notes + divHtml;
            }
            else
            {
                string divHtml = "<div id='txtArea_0' class='temp'><a href='#' class='closeNotes' onclick='Remove(txtArea1);'><i class='fa fa-close' style='font-size:15px;color:red;'></i></a><textarea onchange=\"find('add',this);\"></textarea></div><div id=\"create\" onclick=\"Add(this);\">+</div>";
                bindNotes.InnerHtml = divHtml;
            }
        }

    }
    [WebMethod]
    public static string SaveNotes(string notes,string ID)
    {
        string value = "";
        if (HttpContext.Current.Session["dtEmployee"] != null)
        {
            DataTable dt = (DataTable)HttpContext.Current.Session["dtEmployee"];

            cls_connection cls = new cls_connection();

            cls.insert_data("Exec sp_StickyNotes 'insert',1,'" + notes + "'," + ID + "");


        }
        return value;

    }
}