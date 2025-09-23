<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ReceiptCashDeposit.aspx.cs" Inherits="Admin_ReceiptCashDeposit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                                    CashDeposit - RECEIPT</h3>
                                             
                                            </div>
                                        </td>
                                    </tr>
                                    <asp:Repeater ID="rptTransactionDetails" runat="server">
                                        <ItemTemplate>
                                           <tr>
                                                <td>Bank 
                                                </td>
                                                <td style="text-align: center">:
                                                </td>
                                                <td>
                                                    ICICI 
                                                </td>
                                            </tr>                                           

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
                                                <td>Mobile
                                                </td>
                                                <td style="text-align: center">:
                                                </td>
                                                <td>
                                                    <%# Eval("mobilenumber") %>
                                                </td>
                                            </tr> 
                                            <tr>
                                                <td>Account Number
                                                </td>
                                                <td style="text-align: center">:
                                                </td>
                                                <td>
                                                    <%# Eval("accountnumber") %>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Referenceno
                                                </td>
                                                <td style="text-align: center">:
                                                </td>
                                                <td>
                                                    <%# Eval("referenceno") %>
                                                </td>
                                            </tr>                                           
                                            <tr>
                                                <td>Amount
                                                </td>
                                                <td style="text-align: center">:
                                                </td>
                                                <td>Rs. <%# Eval("Amount") %>
                                                </td>
                                            </tr>                                           
                                            <tr>
                                                <td>Bank RRN ID
                                                </td>
                                                <td style="text-align: center">:
                                                </td>
                                                <td>
                                                    <%# Eval("bankrrn") %>
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
                        <div class="col-md-12">
                            <center>
                                Powered By    <img src="../img/powerdby.png" style="width:200px;"/>
                          <asp:Button ID="btnexportPdf" runat="server" Text="PRINT INVOICE" CssClass="btn btn-info"
                        CausesValidation="false" OnClick="btnexportPdf_Click" />
                    </center>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>

</asp:Content>



