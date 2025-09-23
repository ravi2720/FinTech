<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WithdrawRequestReceipt.aspx.cs" Inherits="WithdrawRequestReceipt" %>

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
                <div class="container">
                    <div class="row">
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
                                                        Withdraw Request</h3>

                                                    <img src="../img/watch.gif" height="100" runat="server" id="imgShow" visible="false" />

                                                </div>
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="rptTransactionDetails" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>Merchant ID
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("LOGINID") %>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Name
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("AccountHolderName") %>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>IFSC
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("IFSCCode") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Account Number
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("AccountNumber") %>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Transaction Amount
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>Rs. <%# Eval("AMOUNT") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Charges
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>Rs. <%# Eval("Charge") %>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Transaction ID
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("RequestID") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>UTR
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("RRN") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Date</td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("requestdate") %>
                                                    </td>
                                                </tr>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </asp:Panel>
                            <div class="col-md-12">
                                <center>
                          <asp:Button ID="btnexportPdf" runat="server" Text="PRINT INVOICE" CssClass="btn btn-danger"
                        CausesValidation="false" OnClick="btnexportPdf_Click" />
                    </center>

                            </div>
                            <%--<asp:Timer runat="server" Interval="5000" ID="tic" Enabled="false" OnTick="Unnamed_Tick"></asp:Timer>--%>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
