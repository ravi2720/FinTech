<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="CheckAEPSTransaction.aspx.cs" Inherits="Reatiler_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Search AEPS Transactions</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="txtTransactionID" class="form-control" placeholder="Transaction ID" data-toggle="tooltip" title="Please Enter New Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTransactionID" ForeColor="Red" ErrorMessage="Enter Transaction ID" Display="Dynamic" ValidationGroup="chkaddfund" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="chkaddfund" Text="Submit" />
                                <span id="wait_tip" style="display: none;">Please wait...<img src="images/ajax-loader2.gif" id="loading_img"></span>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="panel panel-primary">
                        <div class="panel-heading">AEPS Transactions</div>
                        <div class="panel-body">
                            <table id="example6" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>S.No</th>
                                        <th>MemberID</th>
                                        <th>Factor</th>
                                        <th>Amount</th>
                                        <th>Transaction ID</th>
                                        <th>Narration</th>
                                        <th>Add Date</th>
                                    </tr>
                                </thead>
                                <tfoot>
                                    <tr>
                                        <th>S.No</th>
                                        <th>MemberID</th>
                                        <th>Factor</th>
                                        <th>Amount</th>
                                        <th>Transaction ID</th>
                                        <th>Narration</th>
                                        <th>Add Date</th>
                                    </tr>
                                </tfoot>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptData">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td><%#Container.ItemIndex+1 %></td>
                                                <td><%#Eval("LoginID") %></td>
                                                <td><%#Eval("Factor") %></td>
                                                <td><%#Eval("Amount") %></td>
                                                <td><%#Eval("TRANSID") %></td>
                                                <td><%#Eval("NARRATION") %></td>
                                                <td><%#Eval("CREATEDATE") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

