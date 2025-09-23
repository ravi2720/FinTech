using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Data;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public partial class Admin_AEPS_2_WalletInvoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["ID"] != null)
            {
                cls_connection cls = new cls_connection();
                DataTable dtWalletTransaction = cls.select_data_dt("Exec ManageAEPS_History '" + Request.QueryString["ID"].ToString() + "'");
                if (dtWalletTransaction.Rows.Count > 0)
                {

                    {
                        repSummarry.DataSource = dtWalletTransaction;
                        repSummarry.DataBind();
                        if (dtWalletTransaction.Rows[0]["status"].ToString().ToUpper() == "SUCCESS")
                        {
                            imgSet.Src = "../img/RightMark.png";
                            if (dtWalletTransaction.Rows[0]["transcationtype"].ToString().ToUpper() == "BE")
                            {
                                Message.InnerHtml = "Balance Inquiry Success";
                            }
                            else if (dtWalletTransaction.Rows[0]["transcationtype"].ToString().ToUpper() == "MS")
                            {
                                Message.InnerHtml = "Ministatement Success";
                            }
                            else
                            {
                                Message.InnerHtml = "Cash Withdraw Success";
                            }

                        }
                        else
                        {
                            imgSet.Src = "../img/Cross.png";
                            if (dtWalletTransaction.Rows[0]["transcationtype"].ToString().ToUpper() == "BE")
                            {
                                Message.InnerHtml = "Balance Inquiry failed";
                            }
                            else if (dtWalletTransaction.Rows[0]["transcationtype"].ToString().ToUpper() == "MS")
                            {
                                Message.InnerHtml = "Ministatement failed";
                            }
                            else
                            {
                                Message.InnerHtml = "Cash Withdraw failed";
                            }

                        }

                    }
                }
            }
        }
    }

   



}