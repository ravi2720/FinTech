<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="RechargeAPIBalance.aspx.cs" Inherits="Admin_RechargeAPIBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>

                    <div class="panel panel-primary">
                        <div class="panel-heading">API Balance List</div>
                        <div class="panel-body">

                            <div class="row">

                                <div class="table-responsive">
                                    <table class="table table-hover">
                                        <tbody>
                                            <asp:GridView ID="gvAPI" runat="server" class="table table-bordered table-dataTable" AutoGenerateColumns="false"
                                                DataKeyNames="APIID" Width="100%" ShowHeaderWhenEmpty="true" OnRowDataBound="gvAPI_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="API ID" SortExpression="APIID">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litAPIID" runat="server" Text='<%#Eval("APIID") %>'></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="API Name" DataField="APIName" SortExpression="APIName" />
                                                    <asp:TemplateField HeaderText="Balance URL" SortExpression="BalanceURL" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Literal ID="litBalanceURL" runat="server"></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Available Balance" SortExpression="Balance">
                                                        <ItemTemplate>
                                                            <span class="WebRupee">Rs.</span>
                                                            <asp:Literal ID="litBalance" runat="server"></asp:Literal>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Commission Setting">
                                                        <ItemTemplate>
                                                            <a href='Recharge_apiCommission.aspx?id=<%# Eval("APIID") %>' target="_blank">Manage Commission</a>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="EmptyDataTemplate">
                                                        No Record Found !
                                                    </div>
                                                </EmptyDataTemplate>
                                                <RowStyle CssClass="RowStyle" />
                                                <PagerStyle CssClass="PagerStyle" />
                                                <HeaderStyle CssClass="HeaderStyle" />
                                                <AlternatingRowStyle CssClass="AltRowStyle" />
                                                <PagerSettings Position="Bottom" />
                                            </asp:GridView>
                                        </tbody>
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

