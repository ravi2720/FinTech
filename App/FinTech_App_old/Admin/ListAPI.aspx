<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ListAPI.aspx.cs" Inherits="Admin_ListAPI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>

                    <div class="card card-primary">
                        <div class="card-header">Complain List</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <label for="txtFrom">From Date:</label>
                                    <asp:TextBox runat="server" ID="txtFrom" ToolTip="Select From Date." class="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <label for="txtTo">To Date:</label>
                                    <asp:TextBox runat="server" ID="txtTo" ToolTip="Select To Date." class="form-control" MaxLength="10"></asp:TextBox>
                                </div>
                                <div class="col-md-3">
                                    <input type="submit" name="btnSearch" id="btnSearch" value="Search" class="btn btn-primary" title="Click to search.">
                                </div>
                            </div>
                            <div class="row mt-2">

                                <div class="table-responsive">
                                    <table class="table table-hover">
                                        <tbody>
                                            <asp:GridView ID="gvAPI" runat="server"
                                                CssClass="table table-bordered table-hover tablesorter gridView" AutoGenerateColumns="false"
                                                AllowPaging="True" DataKeyNames="APIID"
                                                PageSize="10" Width="100%" OnRowCommand="gvAPI_RowCommand"
                                                AllowSorting="true" ShowHeaderWhenEmpty="true">
                                                <Columns>
                                                    <asp:BoundField HeaderText="API ID" DataField="APIID" SortExpression="APIID" />
                                                    <asp:BoundField HeaderText="API Name" DataField="APIName" SortExpression="APIName" />
                                                    <asp:BoundField HeaderText="Recharge URL" DataField="URL" SortExpression="URL" />
                                                    <asp:TemplateField HeaderText="Prm-1" SortExpression="prm1">
                                                        <ItemTemplate>
                                                            <%#Eval("prm1")%>
                                            -
                                            <%#Eval("prm1val")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prm-2" SortExpression="prm2">
                                                        <ItemTemplate>
                                                            <%#Eval("prm2")%>
                                            -
                                            <%#Eval("prm2val")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Create Date" DataField="AddDate" SortExpression="AddDate" />
                                                    <asp:BoundField HeaderText="Last Update" DataField="LastUpdate" SortExpression="LastUpdate" />
                                                    <asp:TemplateField HeaderStyle-Width="16px">
                                                        <ItemTemplate>
                                                            <a href="AddAPI.aspx?id=<%#Eval("APIID") %>" title="Edit this record">
                                                                <img src='<%= ConstantsData.EditIcon %>' alt="Edit" />
                                                            </a>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="16px"></HeaderStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="16px">
                                                        <ItemTemplate>

                                                            <asp:ImageButton ID="btnView" runat="server" Width="20" ImageUrl='<%# ConstantsData.EyeOpen %>'
                                                                AlternateText="View" ToolTip="View this API" CommandName="View" CommandArgument='<%#Eval("APIID") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="16px"></HeaderStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="16px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnIsActive" runat="server" Width="20" ImageUrl='<%# Convert.ToBoolean(Eval("IsActive")) == true ? ConstantsData.ActiveIcon : ConstantsData.DeActiveIcon %>'
                                                                AlternateText="Active/Deactive this record" ToolTip='<%# Convert.ToBoolean(Eval("IsActive")) == true ? "Deactive this record" : "Active this record" %>'
                                                                CommandName="IsActive" CommandArgument='<%#Eval("APIID") %>' OnClientClick='return confirm("Are You Sure To Active/Deactive This Record?")' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="16px"></HeaderStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div class="EmptyDataTemplate">
                                                        No Record Found !
                                                    </div>
                                                </EmptyDataTemplate>

                                            </asp:GridView>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="toPopup">
                        <div class="close" onclick="disablePopup();">
                        </div>
                        <div id="popup_content">
                            <h2>
                                <b>
                                    <asp:Literal ID="litAPIName" runat="server"></asp:Literal></b></h2>
                            <hr />
                            <h3>Recharge API</h3>
                            <asp:Literal ID="litAPI" runat="server"></asp:Literal>
                            <hr />
                            <h3>Balance API</h3>
                            <asp:Literal ID="litBalanceAPI" runat="server"></asp:Literal>
                            <hr />
                            <h3>Status API</h3>
                            <asp:Literal ID="litStatusAPI" runat="server"></asp:Literal>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

