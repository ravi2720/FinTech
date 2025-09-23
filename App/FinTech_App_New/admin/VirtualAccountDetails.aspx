<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="VirtualAccountDetails.aspx.cs" Inherits="Admin_UPIDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Virtual Account Details</div>
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
                                                        <th>Member Name</th>
                                                        <th>Member Id</th>
                                                        <th>Reference Number</th>
                                                        <th>UDF1</th>
                                                        <th>UDF2</th>
                                                        <th>UDF3</th>  
                                                        <th>Add Date</th> 
                                                        <th>Status</th>  
                                                        <th>Details</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Repeater ID="gvTransactionHistory" runat="server">
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td><%# Container.ItemIndex + 1 %></td>
                                                                <td><%# Eval("Name") %></td>
                                                                <td><%# Eval("MemberID") %></td>
                                                                <td><%# Eval("reference_number") %></td>
                                                                <td><%# Eval("udf1") %></td>
                                                                <td><%# Eval("udf2") %></td>
                                                                <td><%# Eval("udf3") %></td>
                                                                <td><%# Eval("AddDate") %></td
                                                                <td><%# Eval("status") %></td>
                                                                <td><a href='VirtualAccount_Details.aspx?vaid=<%#Eval("msrno") %>' class="btn btn-warning">View</a></td>
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
