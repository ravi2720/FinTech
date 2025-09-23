using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["ctrl"] != null)
            {
                Control ctrl = (Control)Session["ctrl"];
                PrintHelper.PrintWebControl(ctrl);
                Session["ctrl"] = null;
            }
        }
    }
}