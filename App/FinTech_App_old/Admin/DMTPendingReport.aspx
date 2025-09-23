<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="DMTPendingReport.aspx.cs" Inherits="Admin_DMTPendingReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <div class="row">
                        <div class="col-md-12">
                            <h4>Transaction Details:</h4>
                            <table class="table table-bordered table-condensed table-responsive">
                                <tr>
                                    <td>From Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" ID="txtfromdate_ce" Format="dd-MM-yyyy" PopupButtonID="txtfromdate"
                                            TargetControlID="txtfromdate">
                                        </cc1:CalendarExtender>
                                    </td>
                                    <td>To Date
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttodate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <cc1:CalendarExtender runat="server" ID="txttodate_ce" Format="dd-MM-yyyy" Animated="False"
                                            PopupButtonID="txttodate" TargetControlID="txttodate">
                                        </cc1:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td>MerchantID
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMemberID" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>Member Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMemberName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Beneficiary Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBeneName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                    <td>Beneficiary Account No
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBeneAccount" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="right">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click" CausesValidation="false"
                                            class="btn btn-primary" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gvTransactionHistory" runat="server" CssClass="table table-bordered table-condensed table-responsive"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="MerchantID" DataField="MemberID" />
                                    <asp:BoundField HeaderText="Status" DataField="Status" />
                                    <asp:TemplateField HeaderText="Member Name">
                                        <ItemTemplate>
                                            <%# Eval("Name") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Beneficiary Details">
                                        <ItemTemplate>
                                            Sender Name: <%# Eval("SenderName") %><br />
                                            Bene. Name: <%# Eval("BeneficiaryName") %><br />
                                            Account No: <%# Eval("BeneficiaryAccountNo") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Narration" DataField="Narration" />
                                    <%--<asp:BoundField HeaderText="Transfer Mode" DataField="TransferMode" />--%>
                                    <asp:BoundField HeaderText="NetAmount" DataField="NetAmount" />
                                    <asp:TemplateField HeaderText="Date (DD/MM/YYYY)">
                                        <ItemTemplate>
                                            <%# Eval("AddDate").ToString().Substring(6,2) + "/" + Eval("AddDate").ToString().Substring(4,2) + "/" + Eval("AddDate").ToString().Substring(0,4)%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="View Receipt">
                                        <ItemTemplate>
                                            <a href='WalletInvoice_New.aspx?GUID=<%# Eval("GUID") %>' target="_blank">View Receipt</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </section>
            </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

