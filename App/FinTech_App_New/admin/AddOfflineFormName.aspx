<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddOfflineFormName.aspx.cs" Inherits="Admin_AddOfflineFormName" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:updatepanel runat="server" id="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">OffLine Service Details</div>
                        <div class="card-body">

                            <div class="form-group">
                                <label for="txtPackageName">Form Name : </label>
                                <asp:TextBox runat="server" ID="txtFormName" ToolTip="Enter Package" class="form-control ValiDationP"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtPrice">Price : </label>
                                <asp:TextBox runat="server" ID="txtPrice" Text="0" ToolTip="Enter Price" class="form-control ValiDationP"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="chkActive">Active : </label>
                                <asp:CheckBox runat="server" ID="chkActive" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                            </div>
                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary" />


                            <table class="table table-hover">
                                <tbody>
                                    <tr>
                                        <th>Active/DeActive</th>
                                        <th>Edit</th>
                                        <th>Form Name</th>
                                        <th>Price</th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? "./images/Active.png":"./images/delete.PNG")  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl="./images/Edit.PNG" Height="20" Width="20" ToolTip="Edit Row" />
                                                </td>
                                                <td><span><%# Eval("Name") %></span></td>
                                                <td><span><%# Eval("Amount") %></span></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnPackageID" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                </Triggers>
            </asp:updatepanel>
        </section>
    </div>
</asp:Content>


