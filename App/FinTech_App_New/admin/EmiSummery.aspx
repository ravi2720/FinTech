<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="EmiSummery.aspx.cs" Inherits="Admin_EmiSummery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body bootstrap-select-1">

                                    <h4 class="mt-0 header-title" style="color: blue;"><b>EMI Summery</b></h4>
                                    <br />
                                    <div class="row">
                                        <table class="table table-hover mb-0">
                                            <tbody>
                                                <tr>
                                                    <th scope="row">From Date</th>
                                                    <td>
                                                        <asp:TextBox ID="txtfromdate" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtfromdate"
                                                        TargetControlID="txtfromdate">
                                                    </cc1:CalendarExtender>
                                                    </td>
                                                    <th scope="row">To Date</th>
                                                    <td>
                                                        <asp:TextBox ID="txttodate" CssClass="form-control" runat="server"></asp:TextBox>
                                                        <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="MM-dd-yyyy" PopupButtonID="txttodate"
                                                        TargetControlID="txttodate">
                                                    </cc1:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-primary" OnClick="btnsearch_Click" Text="Search" /></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end col -->
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="mt-0 header-title"><b></b></h4>
                                    <div class="table-responsive">
                                        <table class="table table-hover mb-0">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2">Total Records:</td>
                                                    <td>
                                                        <asp:Label ID="lblrecords" runat="server" Text=""></asp:Label></td>
                                                    <td colspan="11"></td>
                                                </tr>
                                                <tr>
                                                    <th>Sr No</th>
                                                    <th>Loan Account No</th>
                                                    <th>Name</th>
                                                    <th>Emi Date</th>
                                                    <th>Amount</th>
                                                    <th>Mobile No</th>
                                                    <th>Email ID</th>
                                                </tr>

                                                <asp:Repeater ID="rptdata" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%#Container.ItemIndex+1%>.</td>
                                                            <td><%#Eval("LOANACCOUNTNO") %></td>
                                                            <td><%#Eval("FIRST_NAME") %></td>
                                                            <td><%#Eval("PROPOSED_DATE") %></td>
                                                            <td><%#Eval("INSTALLMENT_AMOUNT") %></td>
                                                            <td><%#Eval("MOBILE_NO") %></td>
                                                            <td><%#Eval("EMAILID") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

