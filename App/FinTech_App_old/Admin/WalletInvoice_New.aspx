<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="WalletInvoice_New.aspx.cs" Inherits="Admin_WalletInvoice_New" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
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
                                                <%# Eval("MemberID") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Customer Name
                                            </td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>
                                                <%# Eval("SenderName") %>
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
                                            <td>Beneficiary Bank Name
                                            </td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>
                                                <%# Eval("BankName") %>
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
                                                <%# Eval("BeneficiaryAccountNo") %>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                        <td>Transfer Mode
                                        </td>
                                         <td style="text-align:center">
                                            :
                                        </td>
                                        <td>
                                            <%# Eval("TransferMode") %> Money Transfer
                                        </td>
                                    </tr>--%>
                                        <tr>
                                            <td>Transaction Amount
                                            </td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>Rs. <%# Eval("TotalAmount") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Charges
                                            </td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>Rs. <%# Eval("SurchargeAmount") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Total Amount
                                            </td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>Rs. <%# Eval("NetAmount") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Transaction ID
                                            </td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>
                                                <%# Eval("GroupTransID") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Date</td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>
                                                <%# Eval("TransactionDate") %>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Time</td>
                                            <td style="text-align: center">:
                                            </td>
                                            <td>
                                                <%# Eval("TransactionTime") %>
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
                </div>
            </div>
        </section>
    </div>
</asp:Content>

