using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_OfficeDays : System.Web.UI.Page
{
    cls_connection cls = new cls_connection();
    Page page;  
    protected void Page_Load(object sender, EventArgs e)
    {
        
        page = HttpContext.Current.CurrentHandler as Page;
        if (!IsPostBack)
        {
            FillData();
            
            
        }       
    }
    
    private Hashtable Getholiday()
    {
        Hashtable holiday = new Hashtable();
        holiday["1/1/2021"] = "New Year";
        holiday["1/5/2021"] = "Guru Govind Singh Jayanti";
        holiday["1/8/2021"] = "Muharam (Al Hijra)";
        holiday["1/14/2021"] = "Pongal";
        holiday["1/26/2021"] = "Republic Day";
        holiday["2/23/2021"] = "Maha Shivaratri";
        holiday["3/10/2021"] = "Milad un Nabi (Birthday of the Prophet";
        holiday["3/21/2021"] = "Holi";
        holiday["3/21/2021"] = "Telugu New Year";
        holiday["4/3/2021"] = "Ram Navmi";
        holiday["4/7/2021"] = "Mahavir Jayanti";
        holiday["4/10/2021"] = "Good Friday";
        holiday["4/12/2021"] = "Easter";
        holiday["4/14/2021"] = "Tamil New Year and Dr Ambedkar Birth Day";
        holiday["5/1/2021"] = "May Day";
        holiday["5/9/2021"] = "Buddha Jayanti and Buddha Purnima";
        holiday["6/24/2021"] = "Rath yatra";
        holiday["8/13/2021"] = "Krishna Jayanthi";
        holiday["8/14/2021"] = "Janmashtami";
        holiday["8/15/2021"] = "Independence Day";
        holiday["8/19/2021"] = "Parsi New Year";
        holiday["8/23/2021"] = "Vinayaka Chaturthi";
        holiday["9/2/2021"] = "Onam";
        holiday["9/5/2021"] = "Teachers Day";
        holiday["9/21/2021"] = "Ramzan";
        holiday["9/27/2021"] = "Ayutha Pooja";
        holiday["9/28/2021"] = "Vijaya Dasami (Dusherra)";
        holiday["10/2/2021"] = "Gandhi Jayanti";
        holiday["10/17/2021"] = "Diwali & Govardhan Puja";
        holiday["10/19/2021"] = "Bhaidooj";
        holiday["11/2/2021"] = "Guru Nanak Jayanti";
        holiday["11/14/2021"] = "Children's Day";
        holiday["11/28/2021"] = "Bakrid";
        holiday["12/25/2021"] = "Christmas";
        holiday["12/28/2021"] = "Muharram";
        return holiday;
    }
    private void FillData()
    {
        DataTable dtdata = cls.select_data_dt("select * from Officedays");
        rptDay.DataSource = dtdata;
        rptDay.DataBind();

        //DateTime startOfWeek = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)DateTime.Today.DayOfWeek);
        //string result = string.Join("," + Environment.NewLine, Enumerable
        //  .Range(0, 7)
        //  .Select(i => startOfWeek
        //     .AddDays(i)
        //     .ToString("dd-MMMM-yyyy")));
        //string[] weekday = result.Split(',');
        //for (int item = 0; item < rptDay.Items.Count; item++)
        //{
        //    Label lblWeekDay = rptDay.Items[item].FindControl("lblWeekDay") as Label;
        //    lblWeekDay.Text = weekday[item].ToString();
        //}
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            for (int item = 0; item < rptDay.Items.Count; item++)
            {
                TextBox txtDay = rptDay.Items[item].FindControl("txtDay") as TextBox;
                HiddenField hdnID = rptDay.Items[item].FindControl("hdnID") as HiddenField;

                if (!string.IsNullOrEmpty(txtDay.Text.Trim()))
                {
                    cls.update_data("update Officedays set time = '" + txtDay.Text.Trim() + "' where id=" + Convert.ToInt32(hdnID.Value) + "");
                }
            }
            ErrorShow.Success(page: page, Message: "Record Updated Successfully ..");
            FillData();
        }
        catch (Exception ex)
        {
            ErrorShow.Error(page1: page, Message: "Something went wrong ..");
        }
    }


    //protected void rptSurcharge_ItemCommand(object source, RepeaterCommandEventArgs e)
    //{
    //    int id = Convert.ToInt32(e.CommandArgument);
    //    if (e.CommandName == "IsActive")
    //    {
    //        cls.update_data("update impssurcharge set isactive = iif(isactive=0,1,0) where id=" + id + "");
    //        BindData();
    //    }
    //    if (e.CommandName == "Edit")
    //    {
    //        FillData(id);
    //    }
    //}


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Int32 Val = 0;
        Val = cls.update_data("Proc_OfficeDays '"+txtName.Text+"','"+ txtHoliDate.Text + "'");
        if (Val > 0)
        {
            FillData();
        }
    }

    protected void rptDay_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            Int32 Val = 0;
            Val = cls.update_data("delete from OfficeDays where id='"+e.CommandArgument.ToString()+"'");
            FillData();

        }
    }
}