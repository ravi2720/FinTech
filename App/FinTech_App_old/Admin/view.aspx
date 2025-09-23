<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="view.aspx.cs" Inherits="Admin_view" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">Details</div>
                                        <div class="panel-body">
                                            <table class="table table-hover mb-0">
                                                <tbody>
                                                    <tr>

                                                        <th>S.No.</th>
                                                        <th>Amount</th>
                                                        <th>Principal</th>
                                                        <th>Interest</th>
                                                        <th>O/S Principal</th>
                                                        <th>Date</th>
                                                        <th>Penalty</th>
                                                        <th>Receipt No.</th>
                                                        <th>Status</th>
                                                    </tr>

                                                    <asp:Repeater ID="rptdata" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%#Container.ItemIndex+1 %></td>
                                                                <td>
                                                                    <asp:Label ID="lblinstallmentamt" runat="server" Text='<%#Eval("Installment_AMOUNT") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblprincipal" runat="server" Text='<%#Eval("Principal") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblintramt" runat="server" Text='<%#Eval("Interest_Amount") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblprincafterpay" runat="server" Text='<%#Eval("Principal_after_pay") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblproposeddate" runat="server" Text='<%#Eval("Proposed_Date") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblpenalty" runat="server" Text='<%#Eval("Penalty_amount") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblreceiptno" runat="server" Text='<%#Eval("Receipt_No") %>'></asp:Label></td>
                                                                <td><%#Convert.ToBoolean(Eval("ISPAID"))==true?"Paid":"UnPaid" %></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                </tbody>
                                            </table>
                                        </div>
                                        <div style="text-align: right;">
                                            <asp:Button ID="btnback" OnClick="btnback_Click" CssClass="btn btn-primary" runat="server" Text="Back" />
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </div>
                        </div>
                        <!-- end col -->
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

