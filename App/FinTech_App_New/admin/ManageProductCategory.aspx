<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ManageProductCategory.aspx.cs" Inherits="Admin_ManageProductCategory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Manage Menu</div>
                        <div class="panel-body">
                            <asp:HiddenField ID="hdnMenuId" runat="server" Value="0" />
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="widgets-container">
                                        <%--<div class="form-group">
                                    <label for="">Select Role </label>
                                    <asp:DropDownList ID="ddlRole" runat="server" class="form-control ValiDationR">
                                    </asp:DropDownList>
                                </div>--%>

                                        <div class="form-group" style="display: none;">
                                            <label for="">Menu Type </label>
                                            <asp:DropDownList ID="ddlMenuType" class="form-control m-t-xxs" runat="server">
                                                <asp:ListItem Text="Admin" Value="Admin" />
                                                <asp:ListItem Text="Member" Value="Member" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Menu Name</label>
                                            <asp:TextBox ID="txtMenuName" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtMenuName" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                        </div>

                                        <div class="form-group">
                                            <label for="exampleInputPassword1">Menu Level</label>
                                            <asp:DropDownList ID="ddlMenuLevel" class="form-control m-t-xxs" OnSelectedIndexChanged="ddlMenuLevel_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                                <asp:ListItem Value="0">-Select Menu Level-</asp:ListItem>
                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                <asp:ListItem Value="3">3</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMenuLevel" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputPassword1">Select Main Menu</label>
                                            <asp:DropDownList ID="ddlMainMenu" class="form-control m-t-xxs" runat="server" OnSelectedIndexChanged="ddlMainMenu_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputPassword1">Select Sub Menu</label>
                                            <asp:DropDownList ID="ddlSubMenu" class="form-control m-t-xxs" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="widgets-container">



                                        <div class="form-group">
                                            <label for="exampleInputPassword1">Menu Link</label>
                                            <asp:TextBox ID="txtMenuLink" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtMenuLink" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Menu Position</label>
                                            <asp:TextBox ID="txtMenuPosition" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMenuPosition" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputPassword1">Menu Icom</label>
                                            <asp:TextBox ID="txtMenuIcon" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputPassword1">Menu Class</label>
                                            <asp:FileUpload runat="server" ID="fileUpload" CssClass="form-control" />
                                        </div>
                                        <%--<asp:Button ID="btn_submit" class="btn  mb-0 aqua m-t-xs bottom15-xs" OnClick="btn_submit_Click" runat="server" Text="Save" />--%>
                                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary" ValidationGroup="addmenu" />
                                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>



                                    </div>
                                </div>

                            </div>


                        </div>
                    </div>
                    <hr />
                    <div class="panel panel-primary">
                        <div class="panel-heading">Menu List</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">

                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <div>

                                                    <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                        <thead>
                                                            <tr>
                                                                <th>Active/DeActive</th>
                                                                <th>Edit</th>
                                                                <th>Name</th>
                                                                <th>ParentMenu</th>
                                                                <th>Level</th>
                                                            </tr>
                                                        </thead>
                                                        <tfoot>
                                                            <tr>
                                                                <th>Active/DeActive</th>
                                                                <th>Edit</th>
                                                                <th>Name</th>
                                                                <th>ParentMenu</th>
                                                                <th>Level</th>
                                                            </tr>
                                                        </tfoot>
                                                        <tbody>
                                                            <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr class="row1">
                                                                        <td>
                                                                            <asp:ImageButton runat="server" ID="imgDelete" OnClientClick="javascript:return confirm('Are you sure you want to Active/DeActive this Role?');" CommandName="Active" CommandArgument='<%# Eval("ID") %>' ImageUrl='<%# (Convert.ToBoolean(Eval("IsActive"))==true ? "./images/Active.png":"./images/delete.PNG")  %>' Height="20" Width="20" ToolTip="Delete Row" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton runat="server" ID="imgEdit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ImageUrl="./images/Edit.PNG" Height="20" Width="20" ToolTip="Delete Row" />
                                                                        </td>
                                                                        <td><span><%# Eval("Name") %></span></td>
                                                                        <td><span><%# Eval("ParentMenu") %></span></td>
                                                                        <td><span><%# Eval("Level") %></span></td>

                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>
        </section>
    </div>
     <script>
        $(function () {
            Action(DataObject);
        });

        var DataObject = {
            ID: "example1",
            iDisplayLength: 5,
            bPaginate: true,
            bFilter: true,
            bInfo: true,
            bLengthChange: true,
            searching: true
        };
        function LoadData() {

            Action(DataObject);
        }

        var req = Sys.WebForms.PageRequestManager.getInstance();
        req.add_beginRequest(function () {

        });
        req.add_endRequest(function () {
            LoadData();

        });
        function showModal() {
            $('#myModal').modal();
        }


    </script>
</asp:Content>



