<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="Role.aspx.cs" Inherits="Admin_Role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Role Details</div>
                        <div class="card-body">

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">Role : </label>
                                        <asp:TextBox runat="server" ID="txtRoleName" ToolTip="Enter Role" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtBranchName">Prefix : </label>
                                        <asp:TextBox runat="server" ID="txtPrefix" ToolTip="Enter Prefix" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtBranchName">Price : </label>
                                        <asp:TextBox runat="server" ID="txtPrice" ToolTip="Enter Price" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                </div>

                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtBranchName">StartID : </label>
                                        <asp:TextBox runat="server" ID="txtStartID" ToolTip="Enter StartID" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="txtBranchName">Registration Count : </label>
                                        <asp:TextBox runat="server" ID="txtResCount" ToolTip="Enter Registration Count" class="form-control ValiDationR"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <label for="chkActive">OutSide : </label>
                                        <asp:CheckBox runat="server" ID="chkOutSide" ToolTip="OutSide" class="form-control ValiDationR"></asp:CheckBox>
                                    </div>
                                </div>

                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="chkActive">Active : </label>
                                        <asp:CheckBox runat="server" Checked="true" ID="chkActive" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                                    </div>

                                    <div class="col-md-4">
                                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary mt-2" />
                                    </div>
                                </div>

                            </div>



                            <table class="table table-hover">
                                <tbody>
                                    <tr>
                                        <th>Active/DeActive</th>
                                        <th>OutSide Show</th>
                                        <th>Edit</th>
                                        <th>Role Name</th>
                                        <th>Role Price</th>
                                        <th>Prefix</th>
                                        <th>StartID</th>
                                        <th>Res Count</th>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? ConstantsData.ActiveIcon:ConstantsData.DeActiveIcon)  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgOutShow" OnClientClick="javascript:return confirm('Are you sure you want to OutShow or not this Role?');" CommandName="OutSide" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("OutSide"))==true ? ConstantsData.EyeOpen:ConstantsData.EyeClose)  %>' Height="20" Width="20" ToolTip="OutSide Show/Hide" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# ConstantsData.EditIcon %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                                <td><span><%# Eval("Name") %></span></td>
                                                <td><span><%# Eval("Price") %></span></td>
                                                <td><span><%# Eval("Prefix") %></span></td>
                                                <td><span><%# Eval("IDStarting") %></span></td>
                                                <td><span><%# Eval("ResCount") %></span></td>
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
            </asp:UpdatePanel>
        </section>
    </div>

</asp:Content>

