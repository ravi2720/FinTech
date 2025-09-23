<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="UploadDownLineKYC.aspx.cs" Inherits="Admin_UploadDownLineKYC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:UpdatePanel runat="server" ID="updateRolePanel">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnMsrno" runat="server" />
                        <asp:HiddenField ID="hdnID" runat="server" />
                        <div class="panel panel-primary">
                            <div class="panel-heading">Upload KYC Documents</div>
                            <br />
                            <div class="row">
                                <div class="col-md-4">
                                    <asp:TextBox runat="server" ID="txtMemberID" CssClass="form-control" placeholder="Enter MemberID"></asp:TextBox>
                                </div>
                                <div class="col-md-4">
                                    <asp:Button runat="server" ID="btnSearchMember" CssClass="btn btn-danger" Text="Search" OnClick="btnSearchMember_Click" /> 
                                </div>
                            </div>

                            <div class="panel-body">
                                <table class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                    <thead>
                                        <tr>
                                            <th>Document</th>
                                            <th>File</th>
                                            <th>Number</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hdnSide" runat="server" />
                                                <asp:DropDownList ID="ddlDocument" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDocument_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlDocument" ForeColor="Red" InitialValue="0" ErrorMessage="Select Document" Display="Dynamic" ValidationGroup="chkaddfund" />
                                            </td>
                                            <td>
                                                <asp:FileUpload ID="fuDoc" CssClass="form-control" Enabled="false" runat="server" />
                                                <asp:RequiredFieldValidator ID="rfvFuDoc" runat="server" ControlToValidate="fuDoc" ForeColor="Red" ErrorMessage="Select File" Display="Dynamic" ValidationGroup="chkaddfund" />
                                                <asp:FileUpload ID="fuDocBack" CssClass="form-control" Visible="false" Enabled="false" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Visible="false" Enabled="false" ControlToValidate="fuDocBack" ForeColor="Red" ErrorMessage="Select File" Display="Dynamic" ValidationGroup="chkaddfund" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDocNumber" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDocNumber" ForeColor="Red" ErrorMessage="Enter Document Number" Display="Dynamic" ValidationGroup="chkaddfund" />
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="btnSave" Visible="false" class="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" /></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                        <hr />
                        <div class="panel panel-primary" runat="server" id="manBody" visible="false">
                            <div class="panel-heading">KYC Documents</div>
                            <div class="panel-body">
                                <table id="example6" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                    <thead>
                                        <tr>
                                            <th>S.No.</th>
                                            <th>Document Name</th>
                                            <th>Status</th>
                                            <th>AddDate</th>
                                        </tr>
                                    </thead>
                                    <tfoot>
                                        <tr>
                                            <th>S.No.</th>
                                            <th>Document Name</th>
                                            <th>Status</th>
                                            <th>AddDate</th>
                                        </tr>
                                    </tfoot>
                                    <tbody>
                                        <asp:Repeater runat="server" ID="rptData">
                                            <ItemTemplate>
                                                <tr class="row1">
                                                    <td><%# Container.ItemIndex+1 %>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("DocName") %>'></asp:Label></td>
                                                    <td>
                                                        <asp:Label ID="lblSide" runat="server" Text='<%# Eval("Status") %>'></asp:Label></td>
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
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSave" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </section>
    </div>
</asp:Content>

