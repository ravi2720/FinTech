<%@ Application Language="C#" %>

<script RunAt="server">


    cls_connection ObjAll = new cls_connection();

    void Application_Start(object sender, EventArgs e)
    {



    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }
    protected void Application_AuthenticateRequest(object sender, EventArgs e)
    {
        try
        {
            try
            {
                Application["URL"] = "";
                string url = HttpContext.Current.Request.Url.Host;
                {
                    Application["URL"] = "../images/Company/" + ObjAll.select_data_scalar_string("select logo from company where isactive=1");
                }
                Application["CompannyDetails"] = ObjAll.select_data_dt("select * from company where isactive=1");

            }
            catch (Exception ex)
            {

            }
           
        }
        catch { } // Not logged in does not make.
    }
    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
