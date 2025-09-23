<%@ Page Title="" Language="C#" MasterPageFile="~/Member/DashBoard.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Member_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="row row-sm">
                        <div class="col-lg-12">
                            <div class="card">
                                <div class="card-header">
                                    <h3 class="card-title">Change Password
                                <a href="#collapseExample" class="font-weight-bold" aria-controls="collapseExample" aria-expanded="false" data-bs-toggle="collapse" role="button">
                                    <svg xmlns="http://www.w3.org/2000/svg" width="50" height="50" fill="currentColor" class="bi bi-filter" viewBox="0 0 16 16">
                                        <path d="M6 10.5a.5.5 0 0 1 .5-.5h3a.5.5 0 0 1 0 1h-3a.5.5 0 0 1-.5-.5zm-2-3a.5.5 0 0 1 .5-.5h7a.5.5 0 0 1 0 1h-7a.5.5 0 0 1-.5-.5zm-2-3a.5.5 0 0 1 .5-.5h11a.5.5 0 0 1 0 1h-11a.5.5 0 0 1-.5-.5z" />
                                    </svg>
                                </a>
                                    </h3>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-12 col-lg-4">
                                            <div class="form-group">
                                                <label for="email">Old Password:</label>
                                                <asp:TextBox runat="server" ID="txtOldPassword" placeholder="Old Password" data-toggle="tooltip" title="Please Enter Old Password" TextMode="Password" class="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="operator">New Password:</label><p class="hide"></p>
                                                <asp:TextBox runat="server" ID="txtNewPassword" class="form-control" placeholder="New Password" data-toggle="tooltip" title="Please Enter New Password" TextMode="Password"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <label for="operator">Confirm Password:</label><p class="hide"></p>
                                                <asp:TextBox runat="server" ID="txtCnfPassword" class="form-control" placeholder="Confirm Password" data-toggle="tooltip" title="Please Enter Confirm Password" TextMode="Password"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <asp:Button runat="server" ID="btnSubmit" class="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                                            </div>
                                        </div>
                                        <div class="col-12 col-lg-8">
                                            <p>
                                            </p>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

