<%@ Page Language="C#" AutoEventWireup="true" CodeFile="web_notification.aspx.cs" Inherits="Admin_web_notification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <marquee direction="up" scrolldelay="200" behavior="scroll" onmouseover="this.stop();" onmouseout="this.start()";
          
        <div class="row">
            <div class="col-md-12">
                <table>
                    <asp:Repeater ID="repData" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("Name") %></td>
                            </tr>
                            <tr>
                                <td><%# Eval("Description") %></td>
                            </tr>
                            <tr>
                                <td>
                                    <hr />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
        </marquee>
          
    </form>
</body>
</html>
