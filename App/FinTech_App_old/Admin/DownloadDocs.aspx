<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="DownloadDocs.aspx.cs" Inherits="Admin_DownloadDocs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Manage Download Documents</div>
                        <div class="panel-body">
                            <asp:HiddenField ID="hdnid" Value="0" runat="server" />

                            <div class="form-group">
                                <label for="exampleInputPassword1">Heading</label>
                                <asp:TextBox ID="txtNewsName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtNewsName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputPassword1">Link</label>
                                <asp:TextBox ID="txtLinkName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="fileImage">Select Image : </label>
                                <asp:FileUpload runat="server" ID="fileUpload" ToolTip="Upload Image" class="form-control ValiDationP"></asp:FileUpload>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" />
                                <asp:Label ID="lblMessage" CssClass="center-block" Style="text-align: center; font-weight: 600" ForeColor="Red" runat="server" />
                                <table class="table table-hover">
                                    <tbody>
                                        <tr>
                                            <th>Active/DeActive</th>
                                            <th>Edit</th>
                                            <th>Header Name</th>
                                            <th>Image</th>
                                            <th>Delete</th>
                                        </tr>
                                        <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                            <ItemTemplate>
                                                <tr class="row1">
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="imgDelete" Height="20" Width="20" ImageUrl="../images/Active.PNG" ToolTip="Delete Row" />
                                                    </td>
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl="../images/Edit.PNG" Height="20" Width="20" ToolTip="Delete Row" />
                                                    </td>
                                                    <td><span><%# Eval("Header") %></span></td>
                                                    <td>
                                                        <img src='<%# Eval("Image") %>' />
                                                    </td>
                                                    <td>
                                                        <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger" Text="Delete"  OnClientClick="javascript:return confirm('Are you sure you want to Delete this Document?');" CommandName="Delete" CommandArgument='<%# Eval("ID") %>'  />
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>

        </section>
    </div>
</asp:Content>

