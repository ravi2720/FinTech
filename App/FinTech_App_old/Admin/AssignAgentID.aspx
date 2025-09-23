<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AssignAgentID.aspx.cs" Inherits="Admin_AssignAgentID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Assign AgentID Details</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="box">
                                    <!-- /.box-header -->
                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-9">
                                                <strong>Search</strong>
                                                <asp:TextBox ID="txtSearch" runat="server" placeholder="Member Id/BcRegistration Id/Mobile" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <br />
                                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search &gt;&gt;" OnClick="btnSearch_Click"
                                                    ValidationGroup="vg1" />
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="box">

                                    <div class="box-body">
                                        <div class="col-md-12">

                                            <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                <thead>
                                                    <tr>
                                                        <th>S.No</th>
                                                        <th>Member Id</th>
                                                        <th>Member Name</th>
                                                        <th>BC RegistrationId</th>
                                                        <th>Email</th>
                                                        <th>Mobile</th>
                                                        <th>Update</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="gvTransactionHistory" runat="server" OnItemCommand="gvTransactionHistory_ItemCommand">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td><%# Eval("Name") %></td>
                                                                <td><%# Eval("MemberID") %></td>
                                                                <td>
                                                                    <asp:HiddenField runat="server" ID="hdnMSrno" Value='<%# Eval("Msrno") %>' ></asp:HiddenField>
                                                                    <asp:TextBox runat="server" ID="txtAgentID" Text='<%# Eval("BCRegistrationId") %>' CssClass="form-control"></asp:TextBox>
                                                                </td>
                                                                <td><%# Eval("Email") %></td>
                                                                <td><%# Eval("Mobile") %></td>
                                                                <td>
                                                                    <asp:Button runat="server" ID="btnUpdate" Text="Update" CommandName="Update" CommandArgument='<%# Eval("msrno") %>' CssClass="btn btn-danger"></asp:Button>
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

