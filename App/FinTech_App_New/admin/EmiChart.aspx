<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="EmiChart.aspx.cs" Inherits="Admin_EmiChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <h3 style="color: blue;">EMI Chart</h3>

                    <table class="table table-hover mb-0">
                        <tbody>
                            <tr>
                                <td colspan="6">
                                      <table class="table table-hover mb-0">
                                        <tr>
                                            <td>No Of Installment</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblnoinstallment" runat="server" Text=""></asp:Label></td>
                                            <td>Interest Amount</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblintramt" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Total Installment Amount</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lbltotalinstallment" runat="server" Text=""></asp:Label></td>
                                            <td>Installment Amount</td>
                                            <td>:</td>
                                            <td>
                                                <asp:Label ID="lblinstamt" runat="server" Text=""></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <th>Installment No</th>
                                <th>Interest Amount</th>
                                <th>Principal Amount</th>
                                <th>Installment Amount</th>
                                <th>Principal After Pay</th>
                            </tr>
                            <asp:Repeater ID="rptinstallment" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%#Eval("SNO") %></td>
                                        <td><%#Eval("InterestAMounT") %></td>
                                        <td><%#Eval("Principaltotal") %></td>
                                        <td><%#Eval("InstallmentAMounT") %></td>
                                        <td><%#Eval("PrincipalAfterPay") %></td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td colspan="4"></td>
                                <td>
                                    <asp:Button ID="btnback" Visible="false" runat="server" CssClass="btn btn-primary" OnClick="btnback_Click" Text="Back" /></td>
                            </tr>
                        </tbody>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnback" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>


