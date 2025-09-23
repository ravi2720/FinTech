using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class API_FireNotification : System.Web.UI.Page
{
    cls_connection Objconnection = new cls_connection();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Msrno"] != null && Request.QueryString["Message"] != null)
            {
                string Message = Request.QueryString["Message"].ToString();
                string Msrno = Request.QueryString["Msrno"].ToString();
                string Token = Objconnection.select_data_scalar_string("select AppToken from Member where msrno=" + Msrno + "");
                Notification notification = new Notification();
                notification.body = HttpUtility.HtmlDecode(Message);
                notification.title = "AEPS Commission";
                notification.image = "";
                FireNotifictaion fireNotifictaion = new FireNotifictaion();
                fireNotifictaion.to = Token.ToString();
                fireNotifictaion.notification = notification;
                PostGetCallFireNotification.PostCall(fireNotifictaion.GetJson());
            }
        }
    }


}