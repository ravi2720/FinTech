<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="MiniStateMentOnBoard.aspx.cs" Inherits="Admin_MiniStateMentOnBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="panel panel-primary">
                            <div class="panel-heading">Merchant Onboarding</div>
                            <div class="panel-body">
                                <div class="aeps_form">
                                    <div class="row">
                                        <div class="col-md-4">

                                            <label>MemberID</label>

                                            <asp:TextBox runat="server" ID="txtMemberID" OnTextChanged="txtMemberID_TextChanged" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>Name</label>
                                            <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label>firmname</label>
                                            <asp:TextBox runat="server" ID="txtfirmname" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Email</label>
                                            <asp:TextBox runat="server" ID="txtemail" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>Pincode</label>
                                            <asp:TextBox runat="server" ID="txtpincode" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Phone</label>
                                            <asp:TextBox runat="server" ID="txtphone" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                            <label>Pannumber</label>
                                            <asp:TextBox runat="server" ID="txtpannumber" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>Address</label>
                                            <asp:TextBox runat="server" ID="txtAddress" Rows="4" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label>State</label>
                                            <asp:DropDownList runat="server" ID="dllState" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="btn btn-danger" OnClick="btnSubmit_Click" />

                                        </div>
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

