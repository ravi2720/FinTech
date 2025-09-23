<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="LoanLimit.aspx.cs" Inherits="Admin_LoanLimit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">

            <div class="container-fluid">
                <asp:UpdatePanel runat="server" ID="updateRolePanel">
                    <ContentTemplate>
                        <asp:HiddenField ID="hdnID" runat="server" />
                        <div class="panel panel-primary">
                            <div class="panel-heading">Add IMPS Surcharge</div>
                            <div class="panel-body">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <label>Start Value</label>
                                        <asp:DropDownList ID="dllRole" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Start Value</label>
                                        <asp:TextBox ID="txtStartval" runat="server" CssClass="form-control" placeholder="0.00"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <label>End Value</label>
                                        <asp:TextBox ID="txtEndVal" runat="server" CssClass="form-control" placeholder="0.00"></asp:TextBox>
                                    </div>

                                    <div class="clearfix"></div>
                                    <div class="col-md-4">
                                        <br />
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="panel panel-primary">
                            <div class="panel-heading">IMPS Surcharge</div>
                            <div class="panel-body">
                                <div>
                                    <table class="table table-hover">
                                        <thead>
                                            <tr>
                                                <th>Start Value</th>
                                                <th>End Value</th>
                                                <th>Role</th>                                                
                                                <th>Edit</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="rptSurcharge" runat="server" OnItemCommand="rptSurcharge_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblstartval" runat="server" Text='<%#Eval("startval") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblendval" runat="server" Text='<%#Eval("endval") %>'></asp:Label></td>
                                                        <td>
                                                            <asp:Label ID="lblsurcharge" runat="server" Text='<%#Eval("RoleName") %>'></asp:Label></td>                                                        
                                                        <td>
                                                            <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" CommandArgument='<%#Eval("Roleid") %>' CommandName="Edit" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <%--<asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />--%>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </section>
    </div>
</asp:Content>

