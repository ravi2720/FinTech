<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="State.aspx.cs" Inherits="Admin_State" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>

                    <div class="panel panel-primary">
                        <div class="panel-heading">Insert State Name</div>
                        <div class="panel-body">
                            <div class="from-group">
                                <div class="col-md-6">
                                    <label for="txtStateName">State Name : </label>
                                    <asp:TextBox runat="server" ID="txtStateName" ToolTip="Enter State Name" class="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <label for="txtState">Circle Code : </label>
                                    <asp:TextBox runat="server" ID="txtStateCode" ToolTip="Enter Circle Code" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-6">
                                    <label for="chkActive">Active : </label>
                                    <asp:CheckBox runat="server" ID="chkActive" ToolTip="Active" class="form-control ValiDationR"></asp:CheckBox>
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

                                        <th>State Name</th>
                                        <th>Circle Code</th>
                                        <th>Edit</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                        <ItemTemplate>
                                            <tr class="row1">
                                                <td><span><%# Eval("Name") %></span></td>
                                                <td><span><%# Eval("StateCode") %></span></td>
                                                <td>
                                                    <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl="../images/Edit.PNG" Height="20" Width="20" ToolTip="Delete Row" />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </tbody>
                            </table>

                        </div>
                    </div>
                    <asp:HiddenField runat="server" ID="hdnStateID" Value="0" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
</asp:Content>

