<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="KYCMaster.aspx.cs" Inherits="Reatiler_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <asp:HiddenField ID="hdnID" runat="server" />
                    <div class="card panel-primary">
                        <div class="card-header">KYC Documents</div>
                        <div class="card-body">
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="txtDocuments" class="form-control" placeholder="Document Name" data-toggle="tooltip" title="Document Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtDocuments" ForeColor="Red" ErrorMessage="Enter Document Name" Display="Dynamic" ValidationGroup="chkaddfund" />
                            </div>
                            <div class="form-group">
                                <asp:DropDownList runat="server" ID="ddlSide" class="form-control">
                                    <asp:ListItem Text="1" Selected="True" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="chkaddfund" Text="Submit" />
                                <span id="wait_tip" style="display: none;">Please wait...<img src="images/ajax-loader2.gif" id="loading_img"></span>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="card card-primary">
                        <div class="card-header">KYC Documents</div>
                        <div class="card-body">
                            <table id="example6" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                <thead>
                                    <tr>
                                        <th>S.No.</th>
                                        <th>Action</th>
                                        <th>Name</th>
                                        <th>Side</th>
                                        <th>AddDate</th>
                                    </tr>
                                </thead>
                                <tfoot>
                                    <tr>
                                        <th>S.No.</th>
                                        <th>Action</th>
                                        <th>Name</th>
                                        <th>Side</th>
                                        <th>AddDate</th>
                                    </tr>
                                </tfoot>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td><%# Container.ItemIndex+1 %>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnActive" runat="server" CommandName="IsActive" CommandArgument='<%# Eval("ID") %>' CssClass='<%# (Convert.ToInt16(Eval("IsActive"))==1 ? "btn btn-success":"btn btn-warning")  %>' Text='<%# (Convert.ToInt16(Eval("IsActive"))==1 ? "Active":"InActive")%>' OnClientClick="if (!confirm('Are you sure do you want to Activate/Deactivate it?'))" CausesValidation="false" />
                                                    <asp:Button ID="lbtnPwd" runat="server" CssClass="btn btn-info" Text="Edit" CommandName="Edit" CommandArgument='<%#Eval("ID") %>' />
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblSide" runat="server" Text='<%# Eval("Side") %>'></asp:Label></td>
                                                <td>
                                                    <asp:Label ID="lblAdddate" runat="server" Text='<%# Eval("Adddate") %>'></asp:Label></td>
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

