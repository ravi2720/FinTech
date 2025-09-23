<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="AEPSBalanceReport.aspx.cs" Inherits="Admin_AEPSBalanceReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <div class="container-fluid">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="box">
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <strong>Search</strong>
                                            <asp:TextBox ID="txtSearch" runat="server" placeholder="MemberID /PSAID /RequestID" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <br />
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" />
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
                                                    <th>AEPS Total Transaction</th>
                                                    <th>Total AEPS Request</th>
                                                    <th>Remainig Balance</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater ID="gvtData" runat="server">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td><%# Container.ItemIndex+1 %></td>
                                                            <td><%# Eval("MemberID") %></td>
                                                            <td><%# Eval("Name") %></td>
                                                            <td><%# Eval("AEPSAmount") %></td>
                                                            <td><%# Eval("RequestAmount") %></td>
                                                            <td><%# Eval("AEPSBalance") %></td>
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
