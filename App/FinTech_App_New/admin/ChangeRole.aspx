<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ChangeRole.aspx.cs" Inherits="Admin_ChangeRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="card panel-primary">
                <div class="card-header">Role Change</div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="ddlMember">Member ID : </label>
                                <asp:DropDownList runat="server" ID="ddlMember" ToolTip="Select Member." AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged" class="form-control select2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="exampleInputPassword1">Role</label>
                                <asp:DropDownList ID="ddlRole" class="form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <asp:Button runat="server" Text="ChangeRole" CssClass="btn btn-danger" ID="btnChangeRole" OnClick="btnChangeRole_Click" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox float-e-margins">

                                <div class="ibox-content collapse show">
                                    <div class="widgets-container">
                                        <div>

                                            <table id="example1" class="display nowrap table responsive nowrap table-bordered" cellspacing="0" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>SL</th>
                                                        <th>MemberID</th>
                                                        <th>Name</th>
                                                        <th>ParentID</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptData">
                                                        <ItemTemplate>
                                                            <tr class="row1">
                                                                <td><%#Container.ItemIndex+1 %>
                                            
                                                                </td>
                                                                <td><%#Eval("LoginID") %></td>
                                                                <td><%#Eval("NAME") %></td>
                                                                <td><%#Eval("ParentDetails") %></td>

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

