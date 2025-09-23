<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="SettelmentTime.aspx.cs" Inherits="Admin_SettelmentTime" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:UpdatePanel runat="server" ID="updateRolePanel">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnID" runat="server" />
                        <div class="panel panel-primary">
                            <div class="panel-heading">Payout Setting</div>
                            <div class="panel-body">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <label>Start Time</label>
                                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label>End Time</label>
                                        <asp:TextBox ID="txtEndTime" runat="server" CssClass="form-control" placeholder="0.00"></asp:TextBox>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-md-4">
                                        <label>On OFF</label>
                                        <asp:CheckBox ID="chkOnoFF" runat="server" CssClass="form-control"></asp:CheckBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <label>Reason</label>
                                    <asp:TextBox ID="txtReason" runat="server" Rows="4" TextMode="MultiLine" CssClass="form-control" placeholder="0.00"></asp:TextBox>
                                </div>
                                <div class="col-md-12">
                                    <label>Payout From </label>
                                    <asp:DropDownList ID="dllPayoutFrom" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Razor Pay" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="InstantPay" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <br />
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="panel panel-primary">
                            <div class="panel-heading">IMPS Time</div>
                            <div class="panel-body">
                                <div>
                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th>Start Time</th>
                                                <th>End Time</th>
                                                <th>Reason</th>
                                                <th>OnOFF</th>
                                                <th>PayOut From</th>
                                                <th>Edit</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptTime" runat="server" OnItemCommand="rptTime_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblstartval" runat="server" Text='<%#Eval("startTime") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblendval" runat="server" Text='<%#Eval("endTime") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text='<%#Eval("Reason") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("Onoff") %>'></asp:Label></td>
                                                          <td>
                                                            <asp:Label ID="Label3" runat="server" Text='<%#Eval("PayOutFrom") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" CommandArgument='<%#Eval("id") %>' CommandName="Edit" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </section>
    </div>
</asp:Content>

