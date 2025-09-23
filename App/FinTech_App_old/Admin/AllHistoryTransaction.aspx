<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AllHistoryTransaction.aspx.cs" Inherits="Admin_AllHistoryTransaction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="panel panel-primary">
                                    <div class="panel-body">
                                        <table class="table table-responsive">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtFromDateA" CssClass="form-control"></asp:TextBox>
                                                        <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="MM-dd-yyyy" PopupButtonID="txtFromDateA"
                                                            TargetControlID="txtFromDateA">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtToDateA" CssClass="form-control"></asp:TextBox>
                                                        <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="MM-dd-yyyy" PopupButtonID="txtToDateA"
                                                            TargetControlID="txtToDateA">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger" Text="Search" OnClick="btnSearch_Click" />
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblMessage" ForeColor="Red" Font-Size="X-Large"></asp:Label>
                                                    </td>

                                                </tr>
                                                <asp:Repeater runat="server" ID="rptTotalHistoryShowByMember">
                                                    <ItemTemplate>
                                                         <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total AEPS Remainig Balance</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("RemainingAepsBalance") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total Main Remainig Balance</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("RemainingMainBalance") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total AEPS</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("TotalAEPS") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total Settlement</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("TotalSettlement") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total AEPSBE</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("aepsbe") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total AEPSMS</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("aepsMS") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total AEPSCW</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("aepsCW") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total Aadhar Pay</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("aepsACW") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                         <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total DMT</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("TotalDMT") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total DMT Surcharge</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("TotalDMTSurcharge") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total Send UPI</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("TotalSendUPI") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>Total UPI Surcharge</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("TotalSendUPISurcharge") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>MERCHANT Remaining MainBalance</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("MERCHANTRemainingMainBalance") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>DISTRIBUTOR Remaining MainBalance</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("DISTRIBUTORRemainingMainBalance") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>PARTNER Remaining MainBalance</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("PARTNERRemainingMainBalance") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div class="form-group">
                                                                    <label>DISTRIBUTOR Remaining MainBalance</label>
                                                                    <asp:Label runat="server" ForeColor="Green" Font-Size="X-Large" Text='<%# Eval("DISTRIBUTORRemainingMainBalance") %>'></asp:Label>
                                                                </div>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

