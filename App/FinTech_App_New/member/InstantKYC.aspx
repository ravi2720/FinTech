<%@ Page Title="" Language="C#" MasterPageFile="~/DashBoard.master" AutoEventWireup="true" CodeFile="InstantKYC.aspx.cs" Inherits="Member_InstantKYC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-container container-fluid">
        <div class="main-content-body">
            <asp:UpdatePanel runat="server" ID="updateAEPSBE">
                <ContentTemplate>
                    <div class="content-wrapper">
                        <section class="content">
                            <div class="container-fluid">
                                <div class="card card-primary">
                                    <div class="card-header">KYC Verification</div>
                                    <div class="card card-body">
                                        <asp:MultiView runat="server" ID="mul" ActiveViewIndex="0">

                                            <!-- View 1 - KYC Inputs -->
                                            <asp:View runat="server" ID="view1">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label>Name</label>
                                                        <asp:Label runat="server" ID="lblName" CssClass="form-control"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Pan</label>
                                                        <asp:Label runat="server" ID="lblPan" CssClass="form-control"></asp:Label>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <label>Email</label>
                                                        <asp:Label runat="server" ID="lblEmail" CssClass="form-control"></asp:Label>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>Account Number</label>
                                                        <asp:TextBox runat="server" ID="txtAccountNumber" CssClass="form-control" MaxLength="20" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <label>IFSC</label>
                                                        <asp:TextBox runat="server" ID="txtIFSC" CssClass="form-control" MaxLength="15" />
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Button runat="server" ID="btnProccess" CssClass="btn btn-danger mt-4" OnClick="btnProccess_Click" Text="Process" />
                                                    </div>
                                                </div>
                                            </asp:View>

                                            <!-- View 2 - OTP Input -->
                                            <asp:View runat="server" ID="view2">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <label>OTP</label>
                                                        <asp:TextBox runat="server" ID="txtOTP" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Button runat="server" ID="btnVerify" CssClass="btn btn-danger mt-4" OnClick="btnpross_Click" Text="Verify" />
                                                    </div>
                                                </div>
                                            </asp:View>

                                            <!-- View 3 - Transaction History -->
                                            <asp:View runat="server" ID="view3">
                                                <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                    <ItemTemplate>
                                                        <div class="row">
                                                            <div class="col-md-4">
                                                                <label>Member Name</label>
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtName" ReadOnly="false" Text='<%# Eval("Name").ToString() %>'></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label>Mobile</label>
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="TextBox1" ReadOnly="false" Text='<%# Eval("Mobile").ToString() %>'></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <label>Address</label>
                                                                <asp:TextBox runat="server" CssClass="form-control" ID="TextBox2" ReadOnly="false" Text='<%# Eval("Address").ToString() %>'></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="row mt-4">
                                                            <div class="col-md-4">
                                                                <img height="100" width="100" src='<%# "data:image/png;base64, " + Eval("ProfilePic").ToString() %>' alt="Red dot" />
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </asp:View>
                                        </asp:MultiView>
                                    </div>
                                </div>
                            </div>
                        </section>
                    </div>

                    <!-- Hidden fields for OTP validation -->
                    <asp:HiddenField runat="server" ID="hdnHash" />
                    <asp:HiddenField runat="server" ID="hdnop" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
