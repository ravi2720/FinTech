<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="LoginSecurityForEmp.aspx.cs" Inherits="Admin_LoginSecurityForEmp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Employee Login List</div>
                        <div class="card-body">
                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <label>IP Address</label>
                                        <asp:TextBox ID="txtIP" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label>Employee</label>
                                        <asp:DropDownList ID="dllEMP" AutoPostBack="true" OnSelectedIndexChanged="dllEMP_SelectedIndexChanged" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-danger mt-2" Text="Search" OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="row mt-2">

                                <div class="col-md-12">

                                    <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Employee Name</th>
                                                <th>IP</th>
                                                <th>AddDate</th>
                                                <th>Remove</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                        <td><%# Eval("NAME") %><br />
                                                            <%# Eval("LOGINID") %></td>
                                                        <td><%# Eval("IP") %></td>
                                                        <td><%# Eval("AddDate") %></td>
                                                        <td>
                                                            <asp:Button runat="server" ID="btnRemove" Text="Remove" OnClientClick="return confirm('Do want to remove IP')" CssClass="btn btn-danger" CommandArgument='<%#  Eval("ID") %>' CommandName="Remove" />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tbody>
                                    </table>

                                </div>
                            </div>

                        </div>
                    </div>

                </ContentTemplate>
                <Triggers>
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

