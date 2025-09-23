<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddBannerType.aspx.cs" Inherits="Admin_AddBannerType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Insert Banner Type</div>
                        <div class="card-body">
                            <div class="from-group">
                                <div class="col-md-6">
                                    <label for="txtBankName">Banner Type Name : </label>
                                    <asp:TextBox runat="server" ID="txtBannerTypeName" ToolTip="Enter Banner Type Name" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="chkActive">Active : </label>
                                    <asp:CheckBox runat="server" Checked="true" ID="chkActive" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
                                </div>
                                <div class="col-md-6">
                                    <br />
                                    <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary" />
                                </div>
                            </div>
                            <br />


                            <hr>
                            <table class="table table-hover">
                                <thead>
                                    <tr>

                                        <th>Banner Type Name</th>
                                        <th>Edit</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td><span><%# Eval("Name") %></span></td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl="./images/Edit.PNG" Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                            </table>

                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnBannerTypeID" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

