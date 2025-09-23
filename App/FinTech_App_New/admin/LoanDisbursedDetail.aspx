<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="LoanDisbursedDetail.aspx.cs" Inherits="Admin_LoanDisbursedDetail" %>

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
                                                        <td colspan="2">Total Records:</td>
                                                        <td>
                                                            <asp:Label ID="lblrecords" runat="server" Text=""></asp:Label></td>
                                                        <td colspan="7"></td>
                                                    </tr>
                                                    <tr>
                                                        <th>S.No.</th>
                                                        <th>MemberID</th>
                                                        <th>Name</th>
                                                        <th>Loan Given Date</th>
                                                        <th>Given Principal</th>
                                                        <th>Interest</th>
                                                        <th>Received Principal</th>
                                                        <th>Received Interest</th>
                                                        <th>Account Status</th>
                                                        <th>Statement</th>
                                                    </tr>

                                                    <asp:Repeater ID="rptdata" runat="server" OnItemCommand="rptdata_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%#Container.ItemIndex+1 %></td>
                                                                <td>
                                                                    <asp:Label ID="lblinstallmentamt" runat="server" Text='<%#Eval("MemberID") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblprincipal" runat="server" Text='<%#Eval("NAME") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblintramt" runat="server" Text='<%#Eval("Loan_Given_Date") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblprincafterpay" runat="server" Text='<%#Eval("LOANAMT") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblproposeddate" runat="server" Text='<%#Eval("INTEREST_AMOUNT") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblpenalty" runat="server" Text='<%#Eval("RECEIVEDPRINCIPAL") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="lblreceiptno" runat="server" Text='<%#Eval("RECEIVEDINTEREST") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("ACCOUNT_STATUS") %>'></asp:Label></td>
                                                                <td>
                                                                    <asp:LinkButton ID="LinkButton1" CommandName="View" ForeColor="Blue" CommandArgument='<%#Eval("ApplicationID") %>' runat="server">View</asp:LinkButton></td>
                                                            </tr>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                    <tr>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td><b>Total</b></td>
                                                        <td>
                                                            <asp:Label ID="lblgivenprincipal" runat="server" Text=""></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblinterestgenerate" runat="server" Text=""></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblreceivedprinc" runat="server" Text=""></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblreceivedintr" runat="server" Text=""></asp:Label></td>
                                                        <td></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </tbody>
                                            </table>
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

