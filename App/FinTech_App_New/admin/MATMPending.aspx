<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="MATMPending.aspx.cs" Inherits="Admin_MATMPending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">MATM Pending History</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="box">

                                    <div class="box-body">
                                        <div class="col-md-12">

                                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                <thead>
                                                    <tr>
                                                        <th>S.No</th>
                                                        <th>Status</th>
                                                        <th>Member Id</th>
                                                        <th>RefID</th>
                                                        <th>Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody>

                                                    <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td>
                                                                    <span class="btn btn-danger"><%# Eval("Status") %></span>
                                                                </td>
                                                                <td><%# Eval("MemberID") %></td>
                                                                <td><%# Eval("RefID") %></td>
                                                                <td>
                                                                    <asp:Button runat="server" ID="btnChewckStatus" CommandName="CheckStatus" CommandArgument='<%# Eval("RefID") %>' Text="Check Satus" CssClass="btn btn-danger" />
                                                                </td>
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
                    </div>
                    </div>



                </ContentTemplate>
                <Triggers>
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

