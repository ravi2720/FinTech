<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="PageList.aspx.cs" Inherits="Admin_PageList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:updatepanel runat="server" id="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">PageList Details</div>
                        <div class="card-body">

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">Page Name : </label>
                                        <asp:TextBox runat="server" ID="txtPageName" ToolTip="Enter PageName" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                     <div class="col-md-4">
                                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary mt-2" />
                                    </div>
                                </div>

                            </div>
                            <table class="table table-hover">
                                <tbody>
                                    <tr>
                                        <td>ID</td>
                                        <th>Active/DeActive</th>
                                        <th>Edit</th>
                                        <th>Page Name</th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td>
                                                    <%# Container.ItemIndex+1 %>
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("ID"))==true ? ConstantsData.ActiveIcon:ConstantsData.DeActiveIcon)  %>' Height="20" Width="20" ToolTip="Active/DeActive Row" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# ConstantsData.EditIcon %>' Height="20" Width="20" ToolTip="Edit Row" />
                                                </td>
                                                <td><span><%# Eval("Name") %></span></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnRoleID" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                </Triggers>
            </asp:updatepanel>
        </section>
    </div>

</asp:Content>



