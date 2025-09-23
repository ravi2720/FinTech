<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="PAN_TokenPurchaseReport.aspx.cs" Inherits="Admin_PAN_TokenPurchaseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="content-wrapper">
        <section class="content">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="panel panel-primary">
                        <div class="panel-heading">Pan Coupon History</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="box">
                                    <!-- /.box-header -->
                                    <div class="box-body">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <strong>Search</strong>
                                                <asp:TextBox ID="txtPSAID" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="col-md-3">
                                                <strong>Status</strong>
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Width="40%">
                                                    <asp:ListItem>Select Status</asp:ListItem>
                                                    <asp:ListItem>Pending</asp:ListItem>
                                                    <asp:ListItem>Rejected</asp:ListItem>
                                                    <asp:ListItem>Approved</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-md-3">
                                                <br />
                                                <asp:Button ID="btnSearch" runat="server" Text="Search &gt;&gt;" OnClick="btnSearch_Click" CausesValidation="false"
                                                    class="btn btn-primary" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="ibox float-e-margins">
                                        <asp:HiddenField ID="hfname" runat="server" />
                                        <div class="ibox-content collapse show">
                                            <div class="widgets-container">
                                                <div>

                                                    <table id="example1" class="table table-bordered table-striped glyphicon-hover">
                                                        <thead>
                                                            <tr>
                                                                <th>S.No</th>
                                                                <th>Member Id</th>
                                                                <th>Member Name</th>
                                                                <th>Email</th>
                                                                <th>Mobile</th>
                                                                <th>PSA Id</th>
                                                                <th>Physical Coupon Count</th>
                                                                <th>Digital Coupon Count</th>
                                                                <th>Coupon Purchase Date</th>
                                                                <th>Coupon Status</th>
                                                                <th>Coupon RequestID</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <asp:Repeater ID="gvPANReport" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td><%# Container.ItemIndex+1 %></td>
                                                                        <td><%# Eval("MemberID") %></td>
                                                                        <td><%# Eval("Name") %></td>
                                                                        <td><%# Eval("Email") %></td>
                                                                        <td><%# Eval("Mobile") %></td>
                                                                        <td><%# Eval("PsaIdPan") %></td>
                                                                        <td><%# Eval("Physical_Coupon") %></td>
                                                                        <td><%# Eval("Digital_Coupon") %></td>
                                                                        <td><%# Eval("CouponDate") %></td>
                                                                        <td><%# Eval("Status") %></td>
                                                                        <td><%# Eval("RequestID") %></td>
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
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
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

