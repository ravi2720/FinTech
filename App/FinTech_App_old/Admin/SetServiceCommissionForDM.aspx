<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="SetServiceCommissionForDM.aspx.cs" Inherits="Admin_SetServiceCommissionForDM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Service Commission Setting</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <label>Role</label>
                                    <asp:DropDownList runat="server" ID="dllRole" CssClass="form-control"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">
                                    <label>Service</label>
                                    <asp:DropDownList runat="server" ID="dllService" CssClass="form-control select2"></asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <label>Price</label>
                                    <asp:TextBox runat="server" ID="txtPrice" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <label>IsFlat</label>
                                    <asp:CheckBox runat="server" ID="chkData" CssClass="form-control"></asp:CheckBox>
                                </div>
                                 <div class="col-md-2">
                                    <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-danger" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
                                </div>
                            </div>
                            <div class="row">
                                <table class="table table-responsive">
                                    <tr>
                                        <th>SNO</th>
                                        <th>RoleName</th>
                                        <th>ServiceName</th>
                                        <th>Price</th>
                                        <th>IsFlat</th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Container.ItemIndex+1 %>
                                                </td>
                                                <td><%# Eval("RoleName") %></td>
                                                <td><%# Eval("ServiceName") %></td>
                                                <td>
                                                    <asp:Label runat="server" ID="txtPrice" Text='<%# Eval("price") %>' CssClass="form-control"></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label runat="server" ID="chk" Text='<%# Eval("IsFlat") %>' />
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btnDelete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' CssClass="btn btn-danger" Text="Delete" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

