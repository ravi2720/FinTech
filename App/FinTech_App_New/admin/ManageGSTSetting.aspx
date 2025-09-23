<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ManageGSTSetting.aspx.cs" Inherits="Admin_ManageMenu" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel runat="server" ID="updateRolePanel">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Manage GST Details</div>
                        <div class="card-body">
                            <asp:HiddenField ID="hdnID" runat="server" Value="0" />
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="txtAccountNo">State : </label>
                                        <asp:DropDownList ID="ddlState" class="form-control" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlState" InitialValue="0" Font-Bold="True" ForeColor="Red" ErrorMessage="Please select State" ValidationGroup="addmenu" />
                                    </div>

                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Owner Name</label>
                                        <asp:TextBox ID="txtOwnerName" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtOwnerName" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                    </div>

                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Name</label>
                                        <asp:TextBox ID="txtName" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtName" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                    </div>

                                </div>

                            </div>


                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Address</label>
                                        <asp:TextBox ID="txtAddress" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">CompanyType</label>
                                        <asp:DropDownList runat="server" ID="ddlCompanyType" CssClass="form-control">
                                            <asp:ListItem Text="Select Company" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Composition " Value="Composition"></asp:ListItem>
                                            <asp:ListItem Text="Regular" Value="Regular"></asp:ListItem>
                                            <asp:ListItem Text="Non Register" Value="Non Register"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                </div>

                            </div>


                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">Pan</label>
                                        <asp:TextBox ID="txtPan" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPan" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">GSTNo</label>
                                        <asp:TextBox ID="txtGSTNo" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtGSTNo" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                    </div>
                                    <div class="col-md-4">
                                        <label for="exampleInputEmail1">CIN</label>
                                        <asp:TextBox ID="txtCIN" class="form-control m-t-xxs" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCIN" Font-Bold="True" ForeColor="Red" ErrorMessage="*" ValidationGroup="addmenu" />
                                    </div>
                                </div>

                            </div>



                            <div class="form-group">
                                <div class="row">


                                    <div class="col-md-2">
                                        <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click" class="btn btn-primary mt-2" ValidationGroup="addmenu" />
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button runat="server" ID="btnDone" Visible="false" CssClass="btn btn-warning center-block mt-2" Style="text-align: center;" Text="Done" OnClick="btnDone_Click" />
                                    </div>
                                </div>
                            </div>

                            <asp:Label ID="lblMessage" runat="server" CssClass="center-block" Style="text-align: center; font-weight: 600" ForeColor="Red" Text=""></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
            </asp:UpdatePanel>

            <hr />
            <div class="container-fluid">
                <asp:UpdatePanel ID="updt1" runat="server">
                    <ContentTemplate>
                        <div class="card card-primary">
                            <div class="card-header">Manage GST Setting List</div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="ibox float-e-margins">
                                            <div class="ibox-content collapse show">
                                                <div class="widgets-container">
                                                    <div>
                                                        <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                            <thead>
                                                                <tr>
                                                                    <th>Action</th>
                                                                    <th>Owner Name</th>
                                                                    <th>Name</th>
                                                                    <th>Address</th>
                                                                    <th>Company Type</th>
                                                                    <th>Pan</th>
                                                                    <th>GST No</th>
                                                                    <th>CIN</th>                                                              
                                                                    <th>Add Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                     <th>Action</th>
                                                                    <th>Owner Name</th>
                                                                    <th>Name</th>
                                                                    <th>Address</th>
                                                                    <th>Company Type</th>
                                                                    <th>Pan</th>
                                                                    <th>GST No</th>
                                                                    <th>CIN</th>                                                              
                                                                    <th>Add Date</th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody>
                                                                <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr class="row1">
                                                                            <td>
                                                                               
                                                                                <asp:Button runat="server" ID="imgEdit" CssClass="btn btn-info" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' ToolTip="Edit" Text="Edit" />
                                                                            </td>
                                                                            <td><span><%# Eval("OwnerName") %></span></td>
                                                                            <td><span><%# Eval("Name") %></span></td>
                                                                            <td><span><%# Eval("Address") %></span></td>
                                                                            <td><span><%# Eval("CompanyType") %></span></td>
                                                                            <td><span><%# Eval("Pan") %></span></td>
                                                                            <td><span><%# Eval("GSTNo") %></span></td>
                                                                            <td><span><%# Eval("CIN") %></span></td>                                                                           
                                                                            <td><span><%# Eval("AddDate") %></span></td>
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
                </asp:UpdatePanel>
            </div>
        </section>
    </div>
    <script>
        $(function () {
            Action(DataObject);
        });

        var DataObject = {
            ID: "example1",
            iDisplayLength: 15,
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

