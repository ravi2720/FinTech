using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;

public partial class Admin_SendNotificationUsingFire : System.Web.UI.Page
{
    cls_connection ObjConnection = new cls_connection();
    Page page;
    protected void Page_Load(object sender, EventArgs e)
    {
        page = HttpContext.Current.CurrentHandler as Page;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Page page = HttpContext.Current.CurrentHandler as Page; // <-- Add this

        string imgLarge, logo = "";
        Bitmap bmptImgLarge = null;
        string InputFile = string.Empty;

        if (fileUpload.HasFile)
        {
            InputFile = System.IO.Path.GetExtension(fileUpload.FileName);
            string gd = Guid.NewGuid().ToString() + InputFile;

            bmptImgLarge = Resize_Image(fileUpload.PostedFile.InputStream, 500, 700);

            imgLarge = Server.MapPath("./images/Notification/") + gd;
            logo = gd.ToString();
            bmptImgLarge.Save(imgLarge);
        }

        DataTable dt;
        if (!string.IsNullOrEmpty(txtMemberID.Text))
        {
            dt = ObjConnection.select_data_dt("select AppToken from MEMBER where AppToken<>'' and LoginID='" + txtMemberID.Text + "' and AppToken is not null");
        }
        else
        {
            dt = ObjConnection.select_data_dt("select AppToken from MEMBER where AppToken<>'' and AppToken is not null");
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Notification notification = new Notification();
            notification.body = ckNewsDesc.Text;
            notification.title = txtNewsName.Text;
            if (fileUpload.HasFile)
            {
                notification.image = "https://multiwayrecharge.in/images/Notification/" + logo;
            }
            else
            {
                notification.image = "";
            }
            FireNotifictaion fireNotifictaion = new FireNotifictaion();
            fireNotifictaion.to = dt.Rows[i]["AppToken"].ToString();
            fireNotifictaion.notification = notification;
            PostGetCallFireNotification.PostCall(fireNotifictaion.GetJson());
        }

        ObjConnection.update_data("AddEditNotification '" + txtNewsName.Text + "','" + ckNewsDesc.Text + "','https://multiwayrecharge.in/images/Notification/" + logo + "'");

        ErrorShow.AlertMessage(page, "Send Successfully", "Success"); // <-- Use page here
    }


    private Bitmap Resize_Image(Stream streamImage, int maxWidth, int maxHeight)
    {
        Bitmap originalImage = new Bitmap(streamImage);
        int newWidth = originalImage.Width;
        int newHeight = originalImage.Height;
        double aspectRatio = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height);

        newWidth = maxWidth;
        newHeight = maxHeight;
        return new Bitmap(originalImage, newWidth, newHeight);
    }
}