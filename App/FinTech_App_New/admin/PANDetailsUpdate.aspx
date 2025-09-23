<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="PANDetailsUpdate.aspx.cs" Inherits="Admin_PANDetailsUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="row">

                <div class="col-md-4">
                    <strong>MemberID</strong>
                    <asp:TextBox runat="server" CssClass="form-control ValidationVal" ID="txtMemberID" placeholder="Enter Member ID" AutoPostBack="true" OnTextChanged="txtMemberID_TextChanged"></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Name</strong>
                        <asp:TextBox runat="server" ID="txtName" placeholder="Name *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>

                 <div class="col-md-4">
                    <div class="form-group">
                        <strong>PSA Name</strong>
                        <asp:TextBox runat="server" ID="txtPSAName" placeholder="Name *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>
                

            </div>
            <div class="row">

                <div class="col-md-4">
                    <strong>DOB</strong>
                    <asp:TextBox runat="server" CssClass="form-control ValidationVal" ID="txtDOB" placeholder="Enter Member ID" ></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>PinCode</strong>
                        <asp:TextBox runat="server" ID="txtPincode" placeholder="Name *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>

                 <div class="col-md-4">
                    <div class="form-group">
                        <strong>Address</strong>
                        <asp:TextBox runat="server" ID="txtAddress" placeholder="Name *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>
                

            </div>
            <div class="row">

                <div class="col-md-4">
                    <strong>EmailID</strong>
                    <asp:TextBox runat="server" CssClass="form-control ValidationVal" ID="txtEmailID" placeholder="Enter EmailID" ></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Phone 1</strong>
                        <asp:TextBox runat="server" ID="txtPhone1" placeholder="Name *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>

                 <div class="col-md-4">
                    <div class="form-group">
                        <strong>Phone 2</strong>
                        <asp:TextBox runat="server" ID="txtPhone2" placeholder="Name *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>
                

            </div>
            <div class="row">

                <div class="col-md-4">
                    <strong>Aadhar</strong>
                    <asp:TextBox runat="server" CssClass="form-control ValidationVal" ID="txtadhar" placeholder="Enter adhar" ></asp:TextBox>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <strong>Pan</strong>
                        <asp:TextBox runat="server" ID="txtpan" placeholder="Pan *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>

                 <div class="col-md-4">
                    <div class="form-group">
                        <strong>PSAAGENTID</strong>
                        <asp:TextBox runat="server" ID="txtPSAAGENTID" placeholder="PSAAGENTID *" CssClass="form-control ValidationVal"></asp:TextBox>
                    </div>
                </div>
                

            </div>
            <div class="row">
                <asp:Button runat="server" ID="btnSubmit" Text="Saved" OnClick="btnSubmit_Click" />
            </div>
        </section>
    </div>
</asp:Content>

