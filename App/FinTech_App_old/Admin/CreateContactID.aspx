<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="CreateContactID.aspx.cs" Inherits="Admin_CreateContactID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="container-fluid">
                        <div class="col-md-12">
                            <div class="panel panel-primary">
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="form-group">
                                            <asp:DropDownList runat="server" ID="dllMember" CssClass="form-control select2" AutoPostBack="true" OnSelectedIndexChanged="dllMember_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label runat="server" ID="lblMessage" CssClass="btn btn-success" Font-Size="X-Large"></asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button runat="server" ID="btnCOntact" Visible="false" CssClass="btn btn-danger" Text="Create Contact" OnClick="btnCOntact_Click" />
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

