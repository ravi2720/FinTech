<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Admin_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Change Password</div>
                        <div class="panel-body">
                            <asp:TextBox runat="server" ID="txtOldPassword" placeholder="Old Password" data-toggle="tooltip" title="Please Enter Old Password" TextMode="Password" class="form-control"></asp:TextBox><br />
                            <asp:TextBox runat="server" ID="txtNewPassword" class="form-control" placeholder="New Password" data-toggle="tooltip" title="Please Enter New Password" TextMode="Password"></asp:TextBox><br />
                            <asp:TextBox runat="server" ID="txtCnfPassword" class="form-control" placeholder="Confirm Password" data-toggle="tooltip" title="Please Enter Confirm Password" TextMode="Password"></asp:TextBox>

                            <br>

                            <asp:Button runat="server" ID="btnSubmit" class="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />

                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

