<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DMTRecipt.aspx.cs" Inherits="DMTRecipt" %>

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
                                                        DMT - RECEIPT</h3>

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
                                                    <td>Shop Name
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("ShopName") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Sender Mobile
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("SenderMobile") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Beneficiary Name
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("BeneficiaryName") %>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Beneficiary Bank IFSC
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("IFSC") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Beneficiary Account Number
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("accountNumber") %>
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
                                                <tr style="display: none">
                                                    <td>Charges
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>Rs. <%# Eval("Surcharge") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>1 % Charges
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>Rs. <%# ((Convert.ToDecimal(Eval("AMOUNT").ToString()) *1)/100)%>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Transaction ID
                                                    </td>
                                                    <td style="text-align: center">:
                                                    </td>
                                                    <td>
                                                        <%# Eval("OrderID") %>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>RRN No
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
                                                        <%# Eval("AddDate") %>
                                                    </td>
                                                </tr>

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </asp:Panel>
                            
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </form>
</body>
</html>
