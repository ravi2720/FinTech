<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="AddSecurity.aspx.cs" Inherits="Admin_AddSecurity" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <asp:UpdatePanel runat="server" ID="updateRolePanel">
                        <ContentTemplate>
                            <div class="card card-primary">
                                <div class="card-header">Manage Security</div>
                                <div class="card-body">
                                    <asp:HiddenField ID="hdnid" Value="0" runat="server" />
                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Security Name</label>
                                        <asp:TextBox ID="txtNewsName" runat="server" class="form-control m-t-xxs"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="txtNewsName" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <label for="exampleInputPassword1">Description</label>

                                        <CKEditor:CKEditorControl ID="ckNewsDesc" runat="server" BasePath="~/CKEditor/"
                                            Height="250px" Width="90%">
                                        </CKEditor:CKEditorControl>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="ckNewsDesc" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="Save" OnClick="btnSubmit_Click" ValidationGroup="chkaddfund" />
                                        <asp:Label ID="lblMessage" CssClass="center-block" Style="text-align: center; font-weight: 600" ForeColor="Red" runat="server" />
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <hr />
                    <div class="container-fluid">
                        <div class="card card-primary">
                            <div class="card-header">Security List</div>
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
                                                                    <th>S.No</th>
                                                                    <th>Name</th>
                                                                    <th>Description</th>
                                                                    <th>Status</th>
                                                                    <th>Action</th>
                                                                    <th>Add Date</th>
                                                                </tr>
                                                            </thead>
                                                            <tfoot>
                                                                <tr>
                                                                    <th>S.No</th>
                                                                    <th>Name</th>
                                                                    <th>Description</th>
                                                                    <th>Status</th>
                                                                    <th>Action</th>
                                                                    <th>Add Date</th>
                                                                </tr>
                                                            </tfoot>
                                                            <tbody>
                                                                <asp:Repeater ID="repeater1" runat="server" OnItemCommand="repeater1_ItemCommand">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td><%# Container.ItemIndex+1 %></td>
                                                                            <td><%#Eval("Name") %></td>
                                                                            <td><%#Eval("Description") %></td>

                                                                            <td>

                                                                                <asp:Button ID="btnStatus" runat="server" CssClass='<%# Convert.ToInt16(Eval("IsActive")) == 1 ? "btn btn-success" : "btn btn-warning" %>' Text='<%#Eval("Status") %>' CommandArgument='<%#Eval("ID") %>' CommandName="Active" />
                                                                                <td>
                                                                                    <asp:Button ID="btnEdit" runat="server" CssClass="btn btn-primary" Text="Edit" CommandArgument='<%#Eval("ID") %>' CommandName="Edit" /></td>
                                                                                <td><%#Eval("AddDate") %></td>
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
                    </div>
                </section>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

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

