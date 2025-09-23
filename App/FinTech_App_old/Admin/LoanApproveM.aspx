<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="LoanApproveM.aspx.cs" Inherits="Admin_LoanApproveM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">Filter</div>
                                <div class="panel-body">
                                    <table class="table table-hover mb-0">
                                        <tbody>
                                            <tr>
                                                <th scope="row">Member ID</th>
                                                <td>
                                                    <asp:TextBox ID="txtmemberid" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                <th scope="row">Name</th>
                                                <td>
                                                    <asp:TextBox ID="txtname" CssClass="form-control" runat="server"></asp:TextBox></td>
                                                <th scope="row">To Date</th>
                                                <td>
                                                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <!-- end col -->
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="panel panel-primary">
                                <div class="panel-heading">Loan</div>
                                <div class="panel-body">
                                    <table class="table table-hover mb-0">
                                        <tbody>
                                            <tr>
                                                <td colspan="2">Total Records:</td>
                                                <td>
                                                    <asp:Label ID="lblrecords" runat="server" Text=""></asp:Label></td>
                                                <td colspan="11"></td>
                                            </tr>
                                               <tr>
                                                <th>
                                                    <asp:CheckBox ID="chkall" OnCheckedChanged="chkall_CheckedChanged" AutoPostBack="true" runat="server" />All</th>
                                                <th>Sr No</th>
                                                <th>Member ID</th>
                                                <th>Application ID</th>
                                                <th>Member Name</th>
                                                <th>Mobile No</th>
                                                <th>Email ID</th>
                                                <th>Loan Category</th>
                                                <th>Loan Subcategory</th>
                                                <th>Loan Amount</th>
                                                <th>Loan Tenure</th>
                                                <th>Apply Date</th>
                                                <th>Document Status</th>
                                                <th>Documents</th>
                                                <th>EMI Chart</th>
                                                <th>Action</th>
                                            </tr>

                                            <asp:Repeater ID="rptdata" runat="server" OnItemCommand="rptdata_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkrpt" runat="server" /></td>
                                                        <td><%#Container.ItemIndex+1 %></td>
                                                        <td><%#Eval("MemberID") %></td>
                                                        <td><%#Eval("ApplicationID") %></td>
                                                        <td><%#Eval("Name") %></td>
                                                        <td><%#Eval("Mobile") %></td>
                                                        <td><%#Eval("Email") %></td>
                                                        <td><%#Eval("Loancatname") %></td>
                                                        <td><%#Eval("Loansubcatname") %></td>
                                                        <td><%# Eval("LoanAmt") %></td>
                                                        <td><%#Eval("Loan_tenure")%></td>
                                                        <td><%#Eval("LOAN_APPLY_DATE") %></td>
                                                        <td>
                                                            <asp:Label ID="lbldocstatus" runat="server" Text='<%#Eval("DOCSTATUS") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:LinkButton ID="btndoc" CommandName="Doc" CommandArgument='<%#Eval("ApplicationID") %>' ForeColor="Blue" runat="server" Text="View Doc"></asp:LinkButton></td>
                                                        <td>
                                                            <asp:LinkButton ID="btnemichart" CommandName="EMI" CommandArgument='<%#Eval("ApplicationID") %>' ForeColor="Blue" runat="server" Text="View"></asp:LinkButton></td>
                                                        <td>
                                                            <asp:Button ID="btnapprove" runat="server" CommandName="Approve" CommandArgument='<%#Eval("ApplicationID") %>' CssClass="btn btn-primary" Text="Approve" /></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end col -->

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="chkall" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>


