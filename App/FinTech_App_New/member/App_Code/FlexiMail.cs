using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Data;
using System.Net.Mail;
using System.Configuration;

/// <summary>
/// Summary description for FlexiMail
/// </summary>
public class FlexiMail
{
    #region Constructors-Destructors
    public FlexiMail()
    {
        myEmail = new System.Net.Mail.MailMessage();
        _MailBodyManualSupply = false;
    }

    #endregion

    #region  Class Data
    private string _From;
    private string _FromName;
    private string _To;
    private string _ToList;
    private string _Subject;
    private string _CC;
    private string _CCList;
    private string _BCC;
    private string _TemplateDoc;
    private string[] _ArrValues;
    private string _BCCList;
    private bool _MailBodyManualSupply;
    private string _MailBody;
    private string _Attachment;
    private System.Net.Mail.MailMessage myEmail;

    #endregion

    #region Propertie
    public string From
    {
        set { _From = value; }
    }
    public string FromName
    {
        set { _FromName = value; }
    }
    public string To
    {
        set { _To = value; }
    }
    public string Subject
    {
        set { _Subject = value; }
    }
    public string CC
    {
        set { _CC = value; }
    }
    public string BCC
    {
        set { _BCC = value; }
    }
    public bool MailBodyManualSupply
    {
        set { _MailBodyManualSupply = value; }
    }
    public string MailBody
    {
        set { _MailBody = value; }
    }
    public string EmailTemplateFileName
    {
        set { _TemplateDoc = value; }
    }
    public string[] ValueArray
    {
        set { _ArrValues = value; }
    }
    public string AttachFile
    {
        set { _Attachment = value; }
    }

    #endregion

    #region SEND EMAIL

    public void Send()
    {
        myEmail.IsBodyHtml = true;

        if (_FromName == "")
            _FromName = _From;

        myEmail.From = new MailAddress(_From, _FromName);
        myEmail.Subject = _Subject;

        // TO
        _ToList = _To.Replace(";", ",");
        if (!string.IsNullOrEmpty(_ToList))
        {
            foreach (string address in _ToList.Split(','))
                myEmail.To.Add(new MailAddress(address.Trim()));
        }

        // CC
        _CCList = _CC.Replace(";", ",");
        if (!string.IsNullOrEmpty(_CCList))
        {
            foreach (string address in _CCList.Split(','))
                myEmail.CC.Add(new MailAddress(address.Trim()));
        }

        // BCC
        _BCCList = _BCC.Replace(";", ",");
        if (!string.IsNullOrEmpty(_BCCList))
        {
            foreach (string address in _BCCList.Split(','))
                myEmail.Bcc.Add(new MailAddress(address.Trim()));
        }

        // Body
        myEmail.Body = _MailBodyManualSupply ? _MailBody : GetHtml(_TemplateDoc);

        // Attachment
        if (_Attachment != null)
        {
            myEmail.Attachments.Add(new Attachment(_Attachment));
        }

        // SMTP setup
        SmtpClient client = new SmtpClient
        {
            Host = ConfigurationManager.AppSettings["SMTP"], // smtp.gmail.com
            Port = 587,
            EnableSsl = true,
            Credentials = new System.Net.NetworkCredential(
                ConfigurationManager.AppSettings["FROMEMAIL"],
                ConfigurationManager.AppSettings["FROMPWD"]
            )
        };

        client.Send(myEmail);
    }


    public Boolean Send(string MsrNo)
    {
        if (!string.IsNullOrEmpty(MsrNo))
        {
            DataTable dt = new DataTable();
            dt = new cls_connection().select_data_dt("SELECT * FROM tbl_EmailSetting WHERE MsrNo=" + Convert.ToInt32(MsrNo) + " and IsDelete=0 and IsActive = 1");
            if (dt.Rows.Count > 0)
            {
                myEmail.IsBodyHtml = true;
                _From = Convert.ToString(dt.Rows[0]["Email_From"]).Trim();
                _FromName = Convert.ToString(dt.Rows[0]["Email_FromName"]).Trim();
                _CC = Convert.ToString(dt.Rows[0]["Email_CC"]).Trim();
                _BCC = Convert.ToString(dt.Rows[0]["Email_BCC"]).Trim();

                //set mandatory properties 
                if (_FromName == "")
                    _FromName = _From;

                myEmail.From = new MailAddress(_From, _FromName);
                myEmail.Subject = _Subject;

                //---Set recipients in To List 
                _ToList = _To.Replace(";", ",");
                if (_ToList != "")
                {
                    string[] arr = _ToList.Split(',');
                    myEmail.To.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.To.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.To.Add(new MailAddress(_ToList));
                    }
                }

                //---Set recipients in CC List 
                _CCList = _CC.Replace(";", ",");
                if (_CCList != "")
                {
                    string[] arr = _CCList.Split(',');
                    myEmail.CC.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.CC.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.CC.Add(new MailAddress(_CCList));
                    }
                }

                //---Set recipients in BCC List 
                _BCCList = _BCC.Replace(";", ",");
                if (_BCCList != "")
                {
                    string[] arr = _BCCList.Split(',');
                    myEmail.Bcc.Clear();
                    if (arr.Length > 0)
                    {
                        foreach (string address in arr)
                        {
                            myEmail.Bcc.Add(new MailAddress(address));
                        }
                    }
                    else
                    {
                        myEmail.Bcc.Add(new MailAddress(_BCCList));
                    }
                }

                //set mail body 
                if (_MailBodyManualSupply)
                {
                    myEmail.Body = _MailBody;
                }
                else
                {
                    myEmail.Body = GetHtml(_TemplateDoc);
                }

                // set attachment 
                if (_Attachment != null)
                {
                    Attachment objAttach = new Attachment(_Attachment);
                    myEmail.Attachments.Add(objAttach);
                }

                //Send mail 
                SmtpClient client = new SmtpClient();
                client.Host = Convert.ToString(dt.Rows[0]["Email_SMTP"]).Trim();
                client.Credentials = new System.Net.NetworkCredential(Convert.ToString(dt.Rows[0]["Email_From"]).Trim(), Convert.ToString(dt.Rows[0]["Email_Password"]).Trim());
                client.Port = Convert.ToInt32(dt.Rows[0]["Email_PORT"]);
                client.EnableSsl = Convert.ToBoolean(dt.Rows[0]["IsSSL"]);
                client.Send(myEmail);
                return true;
            }
            else
                return false;
        }
        else
            return false;
    }
    #endregion

    #region GetHtml
    public string GetHtml(string argTemplateDocument)
    {
        int i;
        StreamReader filePtr;
        string fileData = "";
        filePtr = File.OpenText(HttpContext.Current.Server.MapPath("~/EmailTemplates") + "/" + argTemplateDocument);

        fileData = filePtr.ReadToEnd();

        if ((_ArrValues == null))
        {
            return fileData;
        }
        else
        {
            for (i = _ArrValues.GetLowerBound(0); i <= _ArrValues.GetUpperBound(0); i++)
            {
                fileData = fileData.Replace("@v" + i.ToString() + "@", (string)_ArrValues[i]);
            }
            return fileData;
        }
        filePtr.Close();
        filePtr = null;
    }
    #endregion
}
