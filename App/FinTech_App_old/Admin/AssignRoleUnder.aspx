<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AssignRoleUnder.aspx.cs" Inherits="Admin_AssignRoleUnder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #mytable {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #mytable td, #customers th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #mytable tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #mytable tr:hover {
                background-color: #ddd;
            }

            #mytable th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #4CAF50;
                color: white;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Assign Down ROle</div>
                        <div class="card-body">
                            <asp:HiddenField ID="hdnid" runat="server" />
                            <div class="form-group">
                                <label for="exampleInputPassword1">
                                    Select Role
                                </label>
                                <asp:DropDownList ID="dllRole" ClientIDMode="Static" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dllRole_SelectedIndexChanged" class="form-control m-t-xxs"></asp:DropDownList>
                                <asp:Label ID="lblMemberName" runat="server" CssClass="green"></asp:Label>
                                <asp:HiddenField ID="hidMsrNo" runat="server" />
                            </div>

                            <div class="form-group">
                                <label for="exampleInputPassword1">
                                    Down Role
                                </label>
                                <asp:DropDownList ID="ddlDownRole" ClientIDMode="Static" runat="server" class="form-control m-t-xxs"></asp:DropDownList>
                                <asp:Label ID="Label1" runat="server" CssClass="green"></asp:Label>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <table class="table table-responsive">
                                        <thead>
                                            <th>Sno</th>
                                            <th>Delete</th>
                                            <th>Role Name</th>
                                        </thead>

                                        <asp:Repeater runat="server" ID="rptAllPackage" OnItemCommand="rptAllPackage_ItemCommand">
                                            <ItemTemplate>
                                                <tr>
                                                    <td><%# Container.ItemIndex+1 %></td>
                                                    <td>
                                                        <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Delete this Role?');" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# ConstantsData.DeleteIcon  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                    </td>
                                                    <td><%# Eval("Name") %></td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>

                            <div class="form-group">
                                <asp:Button runat="server" ID="btnAssign" Text="Assign" CssClass="btn btn-warning" OnClick="btnAssign_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dllRole" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>


</asp:Content>

