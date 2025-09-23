<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ListOperator.aspx.cs" Inherits="Admin_ListOperator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Role Details</div>
                        <div class="panel-body">

                            <div class="form-group">
                                <label for="ddlService">Service : </label>
                                <asp:DropDownList runat="server" ID="ddlService" OnSelectedIndexChanged="ddlService_SelectedIndexChanged" AutoPostBack="true" ToolTip="Select Service" class="form-control ValiDationR">
                                </asp:DropDownList>
                            </div>


                            <table class="table table-hover">
                                <tbody>
                                    <tr>
                                        <th>Operator Name</th>
                                        <th>Operator Code</th>
                                        <th>ServiceName</th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td><span><%# Eval("Name") %></span></td>
                                                <td><span><%# Eval("OperatorCode") %></span></td>
                                                <td><span><%# Eval("ServiceName") %></span></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />--%>
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

