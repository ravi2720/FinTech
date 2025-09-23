<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ChangeLoginPin.aspx.cs" Inherits="Admin_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Change Login Pin</div>
                        <div class="panel-body">
                            <div class="form-group">
                                <asp:TextBox runat="server" ReadOnly="true" ID="txtOldPin" MaxLength="4" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:TextBox runat="server" MaxLength="4" ID="txtNewLoginPin" class="form-control" placeholder="New Password" data-toggle="tooltip" title="Please Enter New Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNewLoginPin" ForeColor="Red" ErrorMessage="Enter New Login Pin" Display="Dynamic" ValidationGroup="chkaddfund" />
                            </div>

                            <div class="form-group">
                                <asp:TextBox runat="server"  MaxLength="4" ID="txtcnfLoginPin" class="form-control" placeholder="Confirm Pin" data-toggle="tooltip" title="Please Enter Login Pin to confirm"></asp:TextBox>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtcnfLoginPin" ForeColor="Red" ControlToCompare="txtNewLoginPin" ErrorMessage="Pin Not Match .." Display="Dynamic" ValidationGroup="chkaddfund"></asp:CompareValidator>
                            </div>

                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" CssClass="btn btn-primary" ValidationGroup="chkaddfund" Text="Submit" />
                                <span id="wait_tip" style="display: none;">Please wait...<img src="images/ajax-loader2.gif" id="loading_img"></span>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

