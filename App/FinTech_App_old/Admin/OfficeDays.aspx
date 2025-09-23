<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="OfficeDays.aspx.cs" Inherits="Admin_OfficeDays" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnID" runat="server" />
                    <div class="panel panel-primary">
                        <div class="panel-heading">Office Working Days</div>
                        <div class="panel-body">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>Name</label>
                                        <asp:TextBox runat="server" ID="txtName" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label>Date</label>
                                        <asp:TextBox ID="txtHoliDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM/dd/yyyy" PopupButtonID="txtHoliDate"
                                            TargetControlID="txtHoliDate">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button runat="server" ID="btnAdd" Text="Add Holiday" CssClass="btn btn-danger" OnClick="btnAdd_Click" />
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="col-md-12">
                                <table class="table table-responsive">
                                    <tr>
                                        <th>Name</th>
                                        <th>Date</th>
                                        <th>Delete</th>
                                    </tr>

                                    <asp:Repeater ID="rptDay" runat="server" OnItemCommand="rptDay_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblDay" runat="server" Text='<%#Eval("Name") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="txtDay" runat="server" CssClass="btn btn-danger" Text='<%#Eval("HolidayDate") %>'></asp:Label></td>
                                                <td>
                                                <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClientClick="return confirm('do you want delete?')" CssClass="btn btn-danger" CommandName="Delete" CommandArgument='<%# Eval("ID") %>'    />
                                                </td>
                                            </tr>


                                        </ItemTemplate>

                                    </asp:Repeater>
                                </table>
                            </div>
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

