<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BillReceipt.aspx.cs" Inherits="BillReceipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="content-wrapper">
            <section class="content">
                <div class="col-md-12" style="background-color: white; padding-bottom: 30px" align="center">
                    <asp:Panel ID="Panel1" runat="server">
                        <div class="col-md-12">
                            <table class="table table-bordered" border="1" style="font-family: Verdana; font-size: 10pt; width: 100%;"
                                cellpadding="10" cellspacing="0">
                                <tr>
                                    <td colspan="3">
                                        <div style="float: left; padding-right: 50px;">
                                            <img id="logoCompany" width="100" src='<%=  Application["URL"].ToString() %>' />
                                        </div>
                                        <div style="float: left; padding-top: 10px;" align="center">
                                            <h3>
                                                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label><br />
                                                Bill Pay - RECEIPT</h3>
                                        </div>
                                        <img src="../img/watch.gif" height="100" runat="server" id="imgShow" visible="false" />
                                    </td>
                                </tr>
                                <asp:Repeater ID="rptTransactionDetails" runat="server">
                                    <ItemTemplate>

                                        <tr>
                                            <td>Number
                                            </td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>
                                                <%# Eval("MobileNo") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Operator
                                            </td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>Rs. <%# Eval("OperatorName") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Service
                                            </td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>Rs. <%# Eval("ServiceName") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Total Amount
                                            </td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>Rs. <%# Eval("Amount") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Transaction ID
                                            </td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>
                                                <%# Eval("TransID") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Operator Ref. Number</td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>
                                                <%# Eval("APIMessage") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Time</td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>
                                                <%# Eval("createddate") %>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </asp:Panel>
                    <div class="col-md-12">
                        <center>
                          <asp:Button ID="btnexportPdf" runat="server" Text="PRINT INVOICE" CssClass="btn btn-info"
                        CausesValidation="false" OnClick="btnexportPdf_Click" />
                    </center>

                    </div>

                    <asp:Timer runat="server" Interval="1000" Enabled="false" ID="tic" OnTick="Unnamed_Tick"></asp:Timer>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
