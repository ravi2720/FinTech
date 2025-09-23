<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="ParentChange.aspx.cs" Inherits="Admin_ParentChange" %>



<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="content-wrapper">
                <section class="content">
                    <asp:UpdatePanel runat="server" ID="updateRolePanel">
                        <ContentTemplate>
                            <div class="card card-primary">
                                <div class="card-header">Parent Change</div>
                                <div class="card-body">
                                    <asp:HiddenField ID="hdnid" Value="0" runat="server" />
                                    <div class="form-group">
                                        <label for="ddlMember">MemberID :</label>
                                        <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Role" class="form-control ValiDationR select2" AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="ddlMember" InitialValue="0" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    </div>

                                    <div class="form-group">
                                        <label for="exampleInputPassword1">
                                            Current ParentID
                                        </label>

                                        <asp:Label ID="lblCurrentParent" runat="server"></asp:Label>

                                    </div>
                                    <div id="messagediv" class="form-group">
                                        <label for="ddlRole">Role </label>
                                        <asp:DropDownList runat="server" ID="ddlRole" ToolTip="Select Role" class="form-control ValiDationR" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnRoleID" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="ddlRole" InitialValue="0" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtPrentID">
                                            ParentID
                                        </label>
                                        <asp:DropDownList runat="server" ID="ddlParentID" ToolTip="Select ParentID" class="form-control ValiDationR select2" AutoPostBack="true" OnSelectedIndexChanged="ddlParentID_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnParentID" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Font-Bold="True" ForeColor="Red" ControlToValidate="ddlParentID" InitialValue="0" ValidationGroup="chkaddfund"></asp:RequiredFieldValidator>
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

                    <div class="card card-primary">
                        <div class="card-header">Parent Change List</div>
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
                                                                <th>S. No.</th>
                                                                <th>Member</th>
                                                                <th>Role</th>
                                                                <th>Role After Change</th>
                                                                <th>Parent</th>
                                                                <th>Parent After Change</th>
                                                                <th>AddDate</th>
                                                            </tr>
                                                        </thead>
                                                        <tfoot>
                                                            <tr>
                                                                <th>S. No.</th>
                                                                <th>Member</th>
                                                                <th>Role</th>
                                                                <th>Role After Change</th>
                                                                <th>Parent</th>
                                                                <th>Parent After Change</th>
                                                                <th>AddDate</th>

                                                            </tr>
                                                        </tfoot>
                                                        <tbody>
                                                            <asp:Repeater runat="server" ID="rptData">
                                                                <ItemTemplate>
                                                                    <tr class="row1">
                                                                        <td><%# Container.ItemIndex+1 %></td>

                                                                        <td><span><%# Eval("Name") %></span></td>
                                                                        <td><span><%# Eval("PreviousRoleName") %></span></td>
                                                                        <td><span><%# Eval("CurrentRoleName") %></span></td>
                                                                        <td><span><%# Eval("PreviousParent") %></span></td>

                                                                        <td><span><%# Eval("CurrentParent") %></span></td>

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

