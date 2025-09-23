<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="UPISendReceipt.aspx.cs" Inherits="Member_UPISendReceipt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="main-container container-fluid"> 
        <div class="main-content-body">
    <div class="content-wrapper">
        <section class="content">
            <div class="col-md-12" style="background-color: white; padding-bottom: 30px" align="center">
                <asp:Panel ID="Panel1" runat="server">
                    <div class="col-md-12">
                        <table class="table table-bordered" border="1" style="font-family: Verdana; font-size: 10pt; width: 100%;"
                            cellpadding="10" cellspacing="0">
                            <tr>
                                <td colspan="3">

                                    <div style="float: left; padding-top: 10px;" align="center">
                                        <h3>
                                            <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label><br />
                                            UPI - RECEIPT</h3>
                                    </div>
                                    <img src="../img/watch.gif" height="100" runat="server" id="imgShow" visible="false" />
                                </td>
                            </tr>
                            <asp:Repeater ID="rptTransactionDetails" runat="server">
                                <ItemTemplate>

                                    <tr>
                                        <td>UPI ID
                                        </td>
                                        <td style="text-align: center">:
                                        </td>
                                        <td>
                                            <%# Eval("UPIID") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>TransID
                                        </td>
                                        <td style="text-align: center">:
                                        </td>
                                        <td><%# Eval("OrderID") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Reference Id
                                        </td>
                                        <td style="text-align: center">:
                                        </td>
                                        <td><%# Eval("VendorID") %>
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
                                        <td>Surcharge
                                        </td>
                                        <td style="text-align: center">:
                                        </td>
                                        <td>Rs. <%# Eval("Surcharge") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>UTR
                                        </td>
                                        <td style="text-align: center">:
                                        </td>
                                        <td>
                                            <%# Eval("rrn") %>
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
                          <asp:Button ID="btnexportPdf" runat="server" Text="PRINT INVOICE" CssClass="btn btn-success"
                        CausesValidation="false" OnClick="btnexportPdf_Click" />
                    </center>

                </div>


            </div>
        </section>
    </div>
            </div>
         </div>
</asp:Content>

