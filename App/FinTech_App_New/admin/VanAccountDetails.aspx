<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="VanAccountDetails.aspx.cs" Inherits="Admin_VanAccountDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="card card-primary">
                        <div class="card-header">Van Details</div>
                        <div class="card-body">
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
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <div>
                                                    <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>S.No</th>
                                                                <th>Details</th>
                                                                <th>Member Name</th>
                                                                <th>Member Id</th>
                                                                <th>Account No</th>
                                                                <th>IFSC</th>
                                                                <th>Name</th>
                                                                <th>Mobile</th>
                                                                <th>Add Date</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Container.ItemIndex + 1 %></td>
                                                                        <td><a href='VanHistory.aspx?upiid=<%#Eval("msrno") %>' class="btn btn-warning">View</a></td>
                                                                        <td><%# Eval("Name") %></td>
                                                                        <td><%# Eval("MemberID") %></td>
                                                                        <td><%# Eval("AccountNo") %></td>
                                                                        <td><%# Eval("IFSC") %></td>
                                                                        <td><%# Eval("Name") %></td>
                                                                        <td><%# Eval("Mobile") %></td>
                                                                        <td><%# Eval("AddDate") %></td>

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

