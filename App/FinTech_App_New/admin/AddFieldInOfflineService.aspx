<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddFieldInOfflineService.aspx.cs" Inherits="Admin_AddFieldInOfflineService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">OffLine Service Details</div>
                        <div class="card-body">
                            <div class="form-group">
                                <label for="txtPackageName">Form Name : </label>
                                <asp:DropDownList runat="server" ID="ddlFormName" AutoPostBack="true" OnSelectedIndexChanged="ddlFormName_SelectedIndexChanged" ToolTip="Enter FormName" class="form-control ValiDationP">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="txtPackageName">FieldName : </label>
                                <asp:TextBox runat="server" ID="txtFieldName" ToolTip="Enter Package" class="form-control ValiDationP"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="txtPackageName">FieldType : </label>
                                <asp:DropDownList runat="server" ID="dllFieldType" ToolTip="Enter Package" class="form-control ValiDationP">
                                    <asp:ListItem Text="Select Type" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="TextBox" Value="textbox"></asp:ListItem>
                                    <asp:ListItem Text="CheckBox" Value="checkbox"></asp:ListItem>
                                    <asp:ListItem Text="DropDownList" Value="dropdownlist"></asp:ListItem>
                                    <asp:ListItem Text="FileUpload" Value="fileupload"></asp:ListItem>

                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="chkActive">Active : </label>
                                <asp:CheckBox runat="server" ID="chkActive" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                            </div>

                            <div class="form-group">
                                <label for="txtPackageName">Max Length : </label>
                                <asp:TextBox runat="server" ID="txtMaxLength" Text="200" MaxLength="200" ToolTip="Enter Package" class="form-control ValiDationP"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtPackageName">Min Length : </label>
                                <asp:TextBox runat="server" ID="txtMinLength" Text="2" ToolTip="Enter Package" class="form-control ValiDationP"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="chkActive">Mandatory : </label>
                                <asp:CheckBox runat="server" ID="chkMandatory" ToolTip="Mandatory" class="form-control ValiDationR"></asp:CheckBox>
                            </div>

                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary" />


                            <table class="table table-hover">
                                <tbody>
                                    <tr>
                                        <th>Active/DeActive</th>
                                        <th>Edit</th>
                                        <th>FieldName </th>
                                        <th>FieldType</th>
                                        <th>Max Val</th>
                                        <th>Min Val</th>
                                        <th>Mandatory</th>
                                        <th>FormName</th>
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
                                                <td><span><%# Eval("FieldName") %></span></td>
                                                <td><span><%# Eval("FieldType") %></span></td>
                                                <td><span><%# Eval("MaxLen") %></span></td>
                                                <td><span><%# Eval("Min") %></span></td>
                                                <td><span><%# Eval("Mandatory") %></span></td>
                                                <td><span><%# Eval("FormName") %></span></td>
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
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

