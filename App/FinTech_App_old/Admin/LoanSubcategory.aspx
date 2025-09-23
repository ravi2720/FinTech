<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="LoanSubcategory.aspx.cs" Inherits="Admin_LoanSubcategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel panel-primary">
                            <div class="panel-heading">Member List</div>
                            <div class="panel-body">
                                <div class="row">
                                    <table class="table table-hover mb-0">
                                        <tbody>

                                            <tr>
                                                <th scope="row">Loan Category</th>
                                                <td>
                                                    <asp:DropDownList ID="ddlloancategory" CssClass="form-control" runat="server"></asp:DropDownList></td>
                                                <th scope="row">Loan subcategory</th>
                                                <td>
                                                    <asp:TextBox ID="txtloanscheme" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>

                                                <th scope="row">Interest Type</th>
                                                <td>
                                                    <asp:DropDownList ID="ddlinteresttype" CssClass="form-control" runat="server">
                                                        <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="SIM"> </asp:ListItem>
                                                        <asp:ListItem Value="2" Text="CIM"> </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <th scope="row">Interest Rate</th>
                                                <td>
                                                    <asp:TextBox ID="txtintrrate" CssClass="form-control" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <th scope="row">File Processing Charge</th>
                                                <td>
                                                    <asp:TextBox ID="txtfileprocessing" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" runat="server">
                                                        <asp:ListItem Selected="True" Text="Flat" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Percent" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <th scope="row">Other Services Charge</th>
                                                <td>
                                                    <asp:TextBox ID="txtotherservice" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RadioButtonList ID="RadioButtonList2" RepeatDirection="Horizontal" runat="server">
                                                        <asp:ListItem Selected="True" Text="Flat" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Percent" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th scope="row">Late Payment EMI Charge</th>
                                                <td>
                                                    <asp:TextBox ID="txtlatepayment" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RadioButtonList ID="RadioButtonList3" RepeatDirection="Horizontal" runat="server">
                                                        <asp:ListItem Selected="True" Text="Flat" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Percent" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <th scope="row">Cheque Bounce Charge</th>
                                                <td>
                                                    <asp:TextBox ID="txtbounce" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:RadioButtonList ID="RadioButtonList4" RepeatDirection="Horizontal" runat="server">
                                                        <asp:ListItem Selected="True" Text="Flat" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Percent" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" CssClass="btn btn-primary" Text="Submit" />
                                                    <asp:Button ID="btnreset" runat="server" CssClass="btn btn-danger" Text="Reset" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- end col -->

                    <br />
                    <br />
                    <div class="panel panel-primary">
                        <div class="panel panel-primary">
                            <div class="panel-heading">Member List</div>
                            <div class="panel-body">
                                <div class="row">
                                    <table class="table table-hover mb-0">
                                        <tr>
                                            <th>Sr. No</th>
                                            <th>Loan Caegory</th>
                                            <th>Loan Subcategory</th>
                                            <th>Interest Rate</th>
                                            <th>Interest Type</th>
                                            <th>Status</th>
                                            <th>Action</th>
                                        </tr>
                                        <asp:Repeater ID="rptdata" runat="server" OnItemCommand="rptdata_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%#Container.ItemIndex+1 %></td>
                                                    <td><%#Eval("LOANCATNAME")%></td>
                                                    <td><%#Eval("LOANSUBCATNAME")%></td>
                                                    <td><%#Eval("ANNUAL_INTEREST_RATE")%></td>
                                                    <td><%#Eval("INTEREST_TYPE")%></td>
                                                    <td><%#Eval("STATUS")%></td>
                                                    <td>
                                                        <asp:Button ID="btnedit" runat="server" CommandName="Delete" CommandArgument='<%# Eval("loansubcatid") %>' CssClass="btn btn-info" OnClientClick="return confirm('Are you sure do you want to delete')" Text="Delete" /></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

