<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="DownLineBalance.aspx.cs" Inherits="Admin_DownLineBalance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:UpdatePanel runat="server" ID="updateRolePanel">
                    <ContentTemplate>
                        <asp:MultiView ID="multiview1" ActiveViewIndex="0" runat="server">
                            <asp:View ID="View2" runat="server">
                                <div class="card card-primary">
                                    <div class="card-header">
                                        Credit/Debit
                                        <br />
                                        <br />
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-md-4">
                                                    <label for="exampleInputPassword1">Role </label>
                                                    <asp:DropDownList ID="ddlRole" class="form-control" runat="server"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-danger mt-4" Text="Search" OnClick="btnSubmit_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="table-responsive">
                                            <table id="example1" class="table table-striped table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th>User ID</th>
                                                        <th>Name</th>
                                                        <th>MemberID</th>
                                                        <th>Mobile</th>
                                                        <th>Parent</th>
                                                        <th>AEPS Balance</th>
                                                        <th>Balance</th>
                                                        <th>Show DownLine</th>


                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater runat="server" ID="rptData" OnItemCommand="rptData_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr class="row1">
                                                                <td><%# Container.ItemIndex+1 %></td>
                                                                <td><%#Eval("Name") %></td>
                                                                <td><%#Eval("LoginID") %></td>
                                                                <td><%#Eval("Mobile") %></td>
                                                                <td><%#Eval("Parent") %></td>
                                                                <td><%#Eval("AEPSBalance") %></td>
                                                                <td><%#Eval("MainBalance") %>
                                                                </td>

                                                                <td>
                                                                    <asp:Button ID="btnshow" runat="server" CssClass="btn btn-primary" Text="Show" CommandArgument='<%#Eval("msrno") %>' CommandName="show" /></td>
                                                            </tr>

                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                </tbody>
                                                <tfoot>
                                                    <tr>
                                                        <td colspan="6"><b>Total</b></td>
                                                        <td>
                                                            <asp:Label runat="server" Font-Bold="true" Font-Size="X-Large" ID="lblsubtotal" Text="Total">Total</asp:Label>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </tfoot>
                                            </table>

                                        </div>
                                    </div>
                                </div>
                            </asp:View>
                        </asp:MultiView>
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

